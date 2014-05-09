using System;
using System.Collections.Generic;
using System.Configuration;
using EasyNetQ.Host.Core;
using EasyNetQ.Host.Service.Configuration;

namespace EasyNetQ.Host.Service.BusManagement
{
    public sealed class BusContainer : IBusContainer, IDisposable
    {
        private readonly Dictionary<string, IBus> _busses = new Dictionary<string, IBus>();
        private readonly IBus _defaultBus;

        public BusContainer(IRabbitFactory rabbitFactory)
        {
            if (HostConfig.ConnectionStrings != null && HostConfig.ConnectionStrings.Count > 0)
            {
                // initialize from configuration
                foreach (ConnectionStringSettings configBroker in HostConfig.ConnectionStrings)
                {
                    this.Register(configBroker.Name, rabbitFactory.CreateBus(configBroker.ConnectionString));
                }
            }
            else
            {
                // add a default connection string that will be used in every GetBus requests
                _defaultBus = rabbitFactory.CreateBus();
            }
        }

        public IBus this[string key]
        {
            get { return this.GetBus(key); }
            set { this.Register(key, value); }
        }

        public void Register(string key, IBus bus)
        {
            _busses.Add(key, bus);
        }

        public IBus GetBus(string key)
        {
            if (_busses.ContainsKey(key))
            {
                return _busses[key];
            }
            if (_defaultBus != null)
            {
                return _defaultBus;
            }
            throw new BusContainerException(string.Format("The requested bus with key '{0}' could not be found. Please check if the bus was correctly registered within the bus container.", key));
        }

        #region IDisposable Implementation

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    if (_busses != null)
                    {
                        foreach (var bus in _busses.Values)
                        {
                            if (bus != null) bus.Dispose();
                        }
                    }

                    if (_defaultBus != null)
                    {
                        _defaultBus.Dispose();
                    }
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                disposed = true;
            }
        }

        ~BusContainer()
        {
            Dispose(false);
        }

        #endregion
    }
}

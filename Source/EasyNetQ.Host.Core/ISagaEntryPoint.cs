namespace EasyNetQ.Host.Core
{
    /// <summary>
    /// A Saga could implement this interface to make some custom initialization and shutdown operations.
    /// </summary>
    public interface ISagaEntryPoint
    {
        /// <summary>
        /// Is called by SagaHost after the Saga is detected.
        /// </summary>
        void OnStart();

        /// <summary>
        /// Is called by SagaHost before shutting down the process hosting the saga.
        /// </summary>
        void OnStop();
    }
}

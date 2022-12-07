namespace RxBim.AutocadTestFramework.Console.Abstractions
{
    using CoreWCF;

    /// <summary>
    /// test
    /// </summary>
    [ServiceContract]
    public interface IService
    {
        /// <summary>
        /// test
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        void SendMessage(string message);
    }
}
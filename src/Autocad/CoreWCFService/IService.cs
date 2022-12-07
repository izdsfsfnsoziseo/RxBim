namespace CoreWCFService
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
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        string GetData(int value);

        /// <summary>
        /// test
        /// </summary>
        /// <param name="composite"></param>
        /// <returns></returns>
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}
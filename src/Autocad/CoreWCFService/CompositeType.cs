namespace CoreWCFService;

using System.Runtime.Serialization;

/// <summary>
/// Data
/// </summary>
[DataContract]
public class CompositeType
{
    /// <summary>
    /// Value
    /// </summary>
    [DataMember]
    public bool BoolValue { get; set; } = true;

    /// <summary>
    /// Value
    /// </summary>
    [DataMember]
    public string StringValue { get; set; } = "Hello ";
}
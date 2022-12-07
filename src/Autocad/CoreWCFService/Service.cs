namespace CoreWCFService;

using System;

/// <inheritdoc />
public class Service : IService
{
    /// <inheritdoc />
    public string GetData(int value)
    {
        return string.Format("You entered: {0}", value);
    }

    /// <inheritdoc />
    public CompositeType GetDataUsingDataContract(CompositeType composite)
    {
        if (composite == null)
        {
            throw new ArgumentNullException("composite");
        }

        if (composite.BoolValue)
        {
            composite.StringValue += "Suffix";
        }

        return composite;
    }
}
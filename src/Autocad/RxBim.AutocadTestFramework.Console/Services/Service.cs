namespace RxBim.AutocadTestFramework.Console.Services;

using System;
using Abstractions;

/// <inheritdoc />
public class Service : IService
{
    /// <inheritdoc />
    public void SendMessage(string message)
    {
        Console.WriteLine(message);
    }
}
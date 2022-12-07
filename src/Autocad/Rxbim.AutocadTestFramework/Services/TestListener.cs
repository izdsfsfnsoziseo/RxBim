namespace RxBim.AutocadTestFramework.Services
{
    using NUnit.Framework.Interfaces;
    using Service;

    /// <inheritdoc />
    public class TestListener : ITestListener
    {
        private readonly IService _service;
        private int _counter;

        /// <summary>
        /// ctr
        /// </summary>
        /// <param name="service"><see cref="IService"/></param>
        public TestListener(IService service)
        {
            _service = service;
        }

        /// <inheritdoc/>
        public void TestStarted(ITest test)
        {
            SendMessage($"Test started {test.FullName}");
        }

        /// <inheritdoc/>
        public void TestFinished(ITestResult result)
        {
            SendMessage($"Test finished {result.FullName} {result.Output} {result.Message}");
        }

        /// <inheritdoc/>
        public void TestOutput(TestOutput output)
        {
            SendMessage($"Test output {output.TestName} is \"{output.Text}\"");
        }

        /// <inheritdoc/>
        public void SendMessage(TestMessage message)
        {
            SendMessage($"Destination {message.Destination}/ Message {message.Message}");
        }

        private void SendMessage(string message)
        {
            _service.SendMessageAsync($"{++_counter}. {message}").GetAwaiter().GetResult();
        }
    }
}
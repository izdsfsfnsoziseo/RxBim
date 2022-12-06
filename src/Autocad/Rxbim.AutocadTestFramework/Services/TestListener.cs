namespace RxBim.AutocadTestFramework
{
    using System;
    using System.Linq;
    using System.Text;
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.EditorInput;
    using NUnit.Framework.Interfaces;

    /// <inheritdoc />
    public class TestListener : ITestListener
    {
        private readonly Editor _editor;

        /// <summary>
        /// ctr
        /// </summary>
        /// <param name="editor"><see cref="Editor"/></param>
        public TestListener(Editor editor)
        {
            _editor = editor;
        }

        /// <inheritdoc/>
        public void TestStarted(ITest test)
        {
            SendMessage($"Test started {test.FullName}");
        }

        /// <inheritdoc/>
        public void TestFinished(ITestResult result)
        {
            SendMessage($"Test finished {result.FullName}");
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
            /*_editor.WriteMessage($"\n{ToUnicode(message)}\n");
            _editor.WriteMessage("\n ");*/
        }

        private string ToUnicode(string from)
        {
            var bytes = Encoding.Unicode.GetBytes(from);
            return new string(bytes.Select(b => (char)b).ToArray());
        }
    }
}
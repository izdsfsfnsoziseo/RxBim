namespace RxBim.Di.Testing.Autocad
{
    using System;

    /// <inheritdoc />
    public class TestDrawingAttribute : Attribute
    {
        /// <inheritdoc />
        public TestDrawingAttribute(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Путь к тестовому чертежу
        /// </summary>
        public string Path { get; }
    }
}
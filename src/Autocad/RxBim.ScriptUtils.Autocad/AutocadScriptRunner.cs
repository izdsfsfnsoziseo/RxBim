namespace RxBim.ScriptUtils.Autocad
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading;
    using Abstractions;

    /// <inheritdoc />
    public class AutocadScriptRunner : IAutocadScriptRunner
    {
        private string? _templateFile;

        private bool _useConsole = false;

        private int Year { get; set; } = 2019;

        // $"C:\\Program Files\\Autodesk\\AutoCAD {year}\\acad.exe"
        private string AcadConsoleExePath => _useConsole
            ? $"C:\\Program Files\\Autodesk\\AutoCAD {Year}\\accoreconsole.exe"
            : $"C:\\Program Files\\Autodesk\\AutoCAD {Year}\\acad.exe";

        /// <inheritdoc />
        public void Run(Action<IAutocadScriptBuilder> action)
        {
            var scriptBuilder = new AutocadScriptBuilder();
            action(scriptBuilder);
            var script = scriptBuilder.ToString();
            var arguments = GetParams(script);

            var startInfo = new ProcessStartInfo()
            {
                FileName = AcadConsoleExePath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };
            using var process = new Process();
            process.StartInfo = startInfo;
            try
            {
                // Gather standard output in a string buffer
                var outSb = new StringBuilder();
                startInfo.RedirectStandardOutput = true;
                process.OutputDataReceived += (sender, e) => { outSb.AppendLine(e.Data); };

// Gather standard error in a string buffer    
                startInfo.RedirectStandardError = true;
                var errSb = new StringBuilder();
                process.ErrorDataReceived += (sender, e) => { outSb.AppendLine(e.Data); };
                var encoding = Encoding.Unicode;
                process.StartInfo.StandardOutputEncoding = encoding;
                process.StartInfo.StandardErrorEncoding = encoding;

                process.Start();
                if (!_useConsole)
                    process.WaitForInputIdle();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Close();
                Console.WriteLine(outSb.ToString());
                Console.WriteLine("end");
            }
            catch (Exception ex)
            {
                process.Kill();
                Console.WriteLine(ex.ToString());
            }

            /*var start = new ProcessStartInfo()
            {
                FileName = AcadConsoleExePath,
                RedirectStandardOutput = true,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = false
            };
            /*var process = new Process();
            process.StartInfo.FileName = AcadConsoleExePath;
            process.StartInfo.Arguments = GetParams(script);#1#
            using var process = Process.Start(start)!;
            try
            {
                // run it!
                // process.Start();
                if (!_useConsole)
                    process.WaitForInputIdle();
                using var outputStream = process.StandardOutput;
    
                // process.WaitForExit();
                while (process.WaitForExit(10000))
                {
                    if (outputStream.Peek() > -1)
                    {
                        Console.WriteLine(outputStream.ReadToEnd());
                    }
                }
    
                process.Close();
            }
            catch (Exception ex)
            {
                process.Kill();
                Console.WriteLine(ex.ToString());
            }*/
        }
        
         /// <inheritdoc />
        public void Run1(Action<IAutocadScriptBuilder> action)
        {
            var scriptBuilder = new AutocadScriptBuilder();
            action(scriptBuilder);
            var script = scriptBuilder.ToString();
            var arguments = GetParams(script);
            
            var start = new ProcessStartInfo()
            {
                FileName = AcadConsoleExePath,
                RedirectStandardOutput = true,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = false
            };
            /*var process = new Process();
            process.StartInfo.FileName = AcadConsoleExePath;
            process.StartInfo.Arguments = GetParams(script);*/
            using var process = Process.Start(start)!;
            try
            {
                // run it!
                // process.Start();
                if (!_useConsole)
                    process.WaitForInputIdle();
                using var outputStream = process.StandardOutput;
    
                // process.WaitForExit();
                while (process.WaitForExit(10000))
                {
                    if (outputStream.Peek() > -1)
                    {
                        Console.WriteLine(outputStream.ReadToEnd());
                    }
                }
    
                process.Close();
            }
            catch (Exception ex)
            {
                process.Kill();
                Console.WriteLine(ex.ToString());
            }
        }

        /// <inheritdoc />
        public IAutocadScriptRunner SetTemplateFile(string path)
        {
            _templateFile = path;
            return this;
        }

        private string GetParams(string script)
        {
            var tempScriptFilePath = Path.GetTempFileName();
            tempScriptFilePath = tempScriptFilePath.Replace(".tmp", ".scr");
            File.WriteAllText(tempScriptFilePath, script);
            File.Move(tempScriptFilePath, tempScriptFilePath);
            var param = new StringBuilder();

            param.Append("/language \"en-US\"");
            if (!string.IsNullOrWhiteSpace(_templateFile))
                param.Append($" /t \"{_templateFile}\"");
            if (!string.IsNullOrWhiteSpace(script))
                param.Append($" /{(_useConsole ? "s" : "b")} \"{tempScriptFilePath}\"");
            /*if (!string.IsNullOrWhiteSpace(fileName))
                param.AppendFormat(" /i \"{0}\"", fileName);*/
            return param.ToString();
        }
    }
}
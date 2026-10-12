using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioHTML.Services
{
    public class FileService
    {
        private readonly int _currentYear;

        public FileService(int year)
        {
            _currentYear = year;
        }

        public string GetProjectRoot()
        {
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (dir != null)
            {
                if (dir.GetFiles("*.csproj").Any())
                    return dir.FullName;

                dir = dir.Parent;
            }

            throw new DirectoryNotFoundException();
        }

        public bool SaveCalendarFile(StringBuilder htmlCalendar)
        {
            try
            {
                string ouputFolder = "Output";
                var rootPath = GetProjectRoot();
                var outputPath = Path.Combine(rootPath, ouputFolder);
                var assetsFolder = Path.Combine(rootPath, "Assets");

                if (!Directory.Exists(outputPath))
                    Directory.CreateDirectory(outputPath);

                string filepath = Path.Combine(outputPath, $"calendario{_currentYear}.html");
                File.WriteAllText(filepath, htmlCalendar.ToString());

                foreach (var doc in Directory.GetFiles(assetsFolder))
                {
                    File.Copy(doc,
                            Path.Combine(outputPath, Path.GetFileName(doc)),
                            true);
                }

                File.Copy(Path.Combine(assetsFolder, "calendar.css"),
                    Path.Combine(outputPath, "calendar.css"),
                    true);

                OpenFile(Path.Combine(outputPath, Path.GetFileName(filepath)));
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void OpenFile(string filepath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = filepath,
                UseShellExecute = true

                //FileName = "msedge",
                //Arguments = $"\"{filepath}\""
            });
        }
    }
}

using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioMobile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("################################################################");
            Console.WriteLine("##                                                            ##");
            Console.WriteLine("##                         Radio Mobile                       ##");
            Console.WriteLine("##                           Installer                        ##");
            Console.WriteLine("##                         by Red David                       ##");
            Console.WriteLine("##                                                            ##");
            Console.WriteLine("################################################################");
            Console.WriteLine("");
            Console.WriteLine("Here's a list of your available drives...");
            var drives = DriveInfo.GetDrives();
            for (int x = 0; x < drives.Count(); x++)
            {
                Console.WriteLine("[" + (x + 1) + "] {0} - {1}GB free of {2}GB ({3:0.##}%)", drives[x].Name, ConvertToGigs(drives[x].TotalFreeSpace), ConvertToGigs(drives[x].TotalSize), GetPercentage(ConvertToGigs(drives[x].TotalFreeSpace), ConvertToGigs(drives[x].TotalSize)));
            }

            GetLocation();
        }

        private static void GetLocation()
        {
            ConsoleKey response;
            Console.WriteLine("Choose install folder [1, 2, 3]:");
            response = Console.ReadKey(false).Key;
            var drives = DriveInfo.GetDrives();

            if (response == ConsoleKey.NumPad1 || response == ConsoleKey.D1)
            {
                BeginInstall(drives[0], 0);
            }
            else if (response == ConsoleKey.NumPad2 || response == ConsoleKey.D2)
            {
                BeginInstall(drives[1], 1);
            }
            else if (response == ConsoleKey.NumPad3 || response == ConsoleKey.D3)
            {
                BeginInstall(drives[2], 2);
            }
            else
            {
                Console.WriteLine("You chose something wrong. Try again...");
                GetLocation();
            }
        }

        private static void BeginInstall(DriveInfo driveInfo, int x)
        {
            Console.WriteLine("");
            CreateFolders(driveInfo, x);

            ExtractFiles(driveInfo, x);
        }

        private static void ExtractFiles(DriveInfo driveInfo, int x)
        {
            Console.WriteLine("Extracting files to drive " + driveInfo.Name + "...");
            string[] subFolders = new string[] { "srtm3", "srtm1", "srtmthird", "Landcover", "OpenStreetMap", "Terraserver", "Toporama" };

            DriveInfo[] drives = DriveInfo.GetDrives();
            if (drives[x] != null)
            {
                // Specify a name for your top-level folder.
                string folderName = drives[x].Name + @"\Radio_Mobile";
                string launcherPath = folderName + @"\rmweng.exe";
                string pathString = Path.Combine(folderName, "Geodata");

                // Check if launcher exists
                if (File.Exists(launcherPath))
                {
                    FileExists(driveInfo, x);
                }
                else
                {
                    Directory.CreateDirectory(pathString);

                    // Now create subfolders for Geodata
                    foreach (var sf in subFolders)
                    {
                        pathString = System.IO.Path.Combine(pathString, sf);
                        Directory.CreateDirectory(pathString);
                        // Reset
                        pathString = System.IO.Path.Combine(folderName, "Geodata");
                    }

                    // Extract first zip - core
                    var zipFileName = Environment.CurrentDirectory + @"\ZipFiles\rmwcore.zip";
                    var targetDir = folderName;
                    FastZip fastZip = new FastZip();
                    string fileFilter = null;

                    // Will always overwrite if target filenames already exist
                    fastZip.ExtractZip(zipFileName, targetDir, fileFilter);

                    // Extract second zip
                    zipFileName = Environment.CurrentDirectory + @"\ZipFiles\rmw1166eng.zip";
                    targetDir = folderName;

                    // Will always overwrite if target filenames already exist
                    fastZip.ExtractZip(zipFileName, targetDir, fileFilter);

                    // Check if launcher exists
                    if (File.Exists(launcherPath))
                    {
                        FileExists(driveInfo, x);
                    }
                }
            }
        }

        private static void FileExists(DriveInfo driveInfo, int x)
        {
            bool confirmed = false;
            DriveInfo[] drives = DriveInfo.GetDrives();
            string folderName = drives[x].Name + @"\Radio_Mobile";

            ConsoleKey response;

            Console.Write("Radio Mobile already exists in the selected location. Run the program? [y/n] ");
            response = Console.ReadKey(false).Key;   // true is intercept key (dont show), false is show
            if (response != ConsoleKey.Enter)
                Console.WriteLine();

            confirmed = response == ConsoleKey.Y;

            if (confirmed)
            {
                Process.Start(folderName + @"\rmweng.exe");
            }
            else
            {
                Console.WriteLine();
            }
        }

        private static void CreateFolders(DriveInfo driveInfo, int x)
        {
            Console.WriteLine("Creating folders...");
            string[] subFolders = new string[] { "srtm3", "srtm1", "srtmthird", "Landcover", "OpenStreetMap", "Terraserver", "Toporama" };

            DriveInfo[] drives = DriveInfo.GetDrives();
            if (drives[x] != null)
            {
                // Specify a name for your top-level folder.
                string folderName = drives[x].Name + @"\Radio_Mobile";
                string launcherPath = folderName + @"\rmweng.exe";
                string pathString = Path.Combine(folderName, "Geodata");
                Directory.CreateDirectory(pathString);

                // Now create subfolders for Geodata
                foreach (var sf in subFolders)
                {
                    pathString = Path.Combine(pathString, sf);
                    Directory.CreateDirectory(pathString);
                    // Reset
                    pathString = Path.Combine(folderName, "Geodata");
                }


            }
        }

        private static double GetPercentage(double totalFreeSpace, double totalSize)
        {
            return (totalFreeSpace / totalSize) * 100;
        }

        private static long ConvertToGigs(long size)
        {
            return (size / (1024 * 1024 * 1024));
        }
    }
}

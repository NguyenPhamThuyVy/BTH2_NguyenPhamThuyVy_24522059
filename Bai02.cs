using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BTTH2_BT2
{
    internal class Bai02
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            // 1) Khai báo
            string path = "";
            int choice;

            do
            {
                // 2) In menu
                Console.WriteLine("\n======= MENU =======");
                Console.WriteLine("1. Nhập đường dẫn thư mục");
                Console.WriteLine("2. Xuất danh sách thư mục con");
                Console.WriteLine("3. Xuất danh sách tệp tin");
                Console.WriteLine("4. Xuất toàn bộ nội dung (như lệnh DIR)");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");

                // 3) Đọc lựa chọn
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    choice = -1;
                    continue;
                }
                // 4) Xử lý bằng switch
                switch (choice)
                {
                    case 1:
                        path = InputPath();
                        break;
                    case 2:
                        if (CheckPath(path))
                            ShowDirectories(path);
                        break;
                    case 3:
                        if (CheckPath(path))
                            ShowFiles(path);
                        break;
                    case 4:
                        if (CheckPath(path))
                            ShowAll(path);
                        break;
                    case 0:
                        Console.WriteLine("Kết thúc chương trình.");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }

            } while (choice != 0);
        }

        // Nhập đường dẫn thư mục
        static string InputPath()
        {
            Console.Write("Nhập đường dẫn thư mục: ");
            string path = Console.ReadLine();
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Thư mục không tồn tại!");
                return "";
            }
            return path;
        }

        // Kiểm tra đã nhập đường dẫn chưa
        static bool CheckPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Chưa nhập đường dẫn! Vui lòng nhập lại.");
                return false;
            }
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Thư mục không tồn tại!");
                return false;
            }
            return true;
        }

        // Xuất thư mục con
        static void ShowDirectories(string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                Console.WriteLine($"\nDanh sách thư mục con trong: {path}\\");
                if (dirs.Length == 0)
                    Console.WriteLine("(Không có thư mục con)");
                else
                {
                    foreach (var dir in dirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        string date = di.LastWriteTime.ToString("dd/MM/yyyy  hh:mm tt", new CultureInfo("en-US"));
                        Console.WriteLine($"{date,25}    <DIR>          {di.Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi truy cập thư mục: {ex.Message}");
            }
        }

        // Xuất danh sách file
        static void ShowFiles(string path)
        {
            try
            {
                string[] files = Directory.GetFiles(path);
                Console.WriteLine($"\nDanh sách tệp tin trong: {path}");
                if (files.Length == 0)
                    Console.WriteLine("(Không có tệp tin)");
                else
                {
                    foreach (var file in files)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(file);
                            string date = fi.LastWriteTime.ToString("dd/MM/yyyy  hh:mm tt", new CultureInfo("en-US"));
                            Console.WriteLine($"{date,25}    {fi.Length,15:N0}    {fi.Name}");
                        }
                        catch
                        {
                            Console.WriteLine($"(Không thể truy cập tệp): {Path.GetFileName(file)}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi truy cập tệp: {ex.Message}");
            }
        }

        // Xuất đầy đủ như lệnh DIR
        static void ShowAll(string path)
        {
            DriveInfo drive = new DriveInfo(Path.GetPathRoot(path));
            string driveLetter = drive.Name.TrimEnd('\\');

            Console.WriteLine($"\n{driveLetter}>dir\n");

            Console.WriteLine($" Volume in drive {driveLetter} is {drive.VolumeLabel}");
            Console.WriteLine($" Volume Serial Number is {GetVolumeSerial(driveLetter)}\n");
            Console.WriteLine($" Directory of {path}\\\n");

            string[] dirs = Array.Empty<string>();
            string[] files = Array.Empty<string>();

            try { dirs = Directory.GetDirectories(path); } catch { }
            try { files = Directory.GetFiles(path); } catch { }

            int dirCount = 0;
            int fileCount = 0;
            long totalSize = 0;

            // Danh sách thư mục
            foreach (var dir in dirs)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    string date = di.LastWriteTime.ToString("dd/MM/yyyy  hh:mm tt", new CultureInfo("en-US"));
                    Console.WriteLine($"{date,25}    <DIR>          {di.Name}");
                    dirCount++;
                }
                catch
                {
                    // bỏ qua thư mục không truy cập được
                }
            }

            // Danh sách tệp
            foreach (var file in files)
            {
                try
                {
                    FileInfo fi = new FileInfo(file);
                    string date = fi.LastWriteTime.ToString("dd/MM/yyyy  hh:mm tt", new CultureInfo("en-US"));
                    Console.WriteLine($"{date,25}    {fi.Length,15:N0}    {fi.Name}");
                    totalSize += fi.Length;
                    fileCount++;
                }
                catch
                {
                    // bỏ qua file không truy cập được
                }
            }

            Console.WriteLine($"\n {fileCount} File(s)\t{totalSize:N0} bytes");
            Console.WriteLine($" {dirCount} Dir(s)\t{GetFreeSpace(path):N0} bytes free");
        }


        // Tổng kích thước file
        static long GetTotalFileSize(string[] files)
        {
            long total = 0;
            foreach (var file in files)
            {
                try
                {
                    FileInfo fi = new FileInfo(file);
                    total += fi.Length;
                }
                catch { }
            }
            return total;
        }

        // Dung lượng trống ổ đĩa
        static long GetFreeSpace(string path)
        {
            try
            {
                DriveInfo drive = new DriveInfo(Path.GetPathRoot(path));
                return drive.AvailableFreeSpace;
            }
            catch { return 0; }
        }
        // Lấy Volume Serial Number
        static string GetVolumeSerial(string driveLetter)
        {
            try
            {
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/c vol {driveLetter}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                int index = output.IndexOf("Serial Number is");
                if (index >= 0)
                    return output.Substring(index + 18).Trim();
            }
            catch { }
            return "0000-0000";
        }
    }
}

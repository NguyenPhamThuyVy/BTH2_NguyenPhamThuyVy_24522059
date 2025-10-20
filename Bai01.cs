using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BTTH2_BT1
{
    internal class Bai01
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            // 1) Nhập vào tháng và năm
            int month = ReadPositiveInt("Nhập tháng: ");
            int year = ReadPositiveInt("Nhập năm: ");
            int choice;
            do
            {
                // 2) In menu
                Console.WriteLine("\n=======MENU=======");
                Console.WriteLine("1. Lịch của tháng, năm vừa nhập");
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
                        PrintCalendar(month, year);
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
        // Đọc số nguyên dương
        static int ReadPositiveInt(string message)
        {
            int n;
            do
            {
                Console.Write(message);
            } while (!int.TryParse(Console.ReadLine(), out n) || n <= 0 || n > 1000000);
            return n;
        }
        // Kiểm tra năm nhuận
        static bool IsLeapYear(int y)
        {
            return (y % 4 == 0 && y % 100 != 0) || (y % 400 == 0);
        }
        // Hàm trả về số ngày trong tháng
        static int DaysInMonth(int m, int y)
        {
            int[] DaysinMonth = { 31, (IsLeapYear(y) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            return DaysinMonth[m - 1];
        }
        // Kiểm tra tính hợp lệ 
        static bool IsValidDate(int d, int m, int y)
        {
            if (y < 1 || m < 1 || m > 12 || d < 1)
            {
                return false;
            }
            int[] DaysinMonth = { 31, (IsLeapYear(y) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (d > DaysinMonth[m - 1])
            {
                return false;
            }
            return true;
        }
        // Hàm in lịch
        static void PrintCalendar(int m, int y)
        {
            Console.WriteLine($"\n Month: {m:D2}/{y}");
            Console.WriteLine("Sun Mon Tue Wed Thu Fri Sat");
            DateTime firstday = new DateTime(y, m, 1);
            int startday = (int)firstday.DayOfWeek;
            int days = DaysInMonth(m, y);
            for (int i = 0; i < startday; i++)
            {
                Console.Write("    ");
            }
            for (int day = 1; day <= days; day++)
            {
                Console.Write($"{day,3} ");
                if ((startday + day) % 7 == 0)
                    Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
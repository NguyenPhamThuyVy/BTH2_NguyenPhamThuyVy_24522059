using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BTTH2_BT3
{
    internal class Bai03
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            // 1) Khai báo ma trận
            int n = 0, m = 0;
            int[,] matrix = null;
            int choice;
            do
            {
                // 2) In menu
                Console.WriteLine("\n=======MENU=======");
                Console.WriteLine("1. Nhập ma trận");
                Console.WriteLine("2. Xuất ma trận");
                Console.WriteLine("3. Tìm một phần tử trong ma trận");
                Console.WriteLine("4. Xuất các phần tử là số nguyên tố trong ma trận");
                Console.WriteLine("5. Dòng nhiều số nguyên tố nhất trong ma trận");
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
                        InputMatrix(out matrix, out n, out m);
                        break;
                    case 2:
                        if (matrix != null)
                        {
                            PrintMatrix(matrix, n, m);
                        }
                        else
                        {
                            Console.WriteLine("Chưa có ma trận nào.");
                        }
                        break;
                    case 3:
                        if (matrix != null)
                        {
                            SearchElement(matrix, n, m);
                        }
                        else
                        {
                            Console.WriteLine("Chưa có ma trận nào.");
                        }
                        break;
                    case 4:
                        if (matrix != null)
                        {
                            PrintPrimes(matrix, n, m);
                        }
                        else
                        {
                            Console.WriteLine("Chưa có ma trận nào.");
                        }
                        break;
                    case 5:
                        if (matrix != null)
                        {
                            GetRowWithMostPrimes(matrix, n, m);
                        }
                        else
                        {
                            Console.WriteLine("Chưa có ma trận nào.");
                        }
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
        // (a) Nhập/Xuất ma trận
        static void InputMatrix(out int[,] a, out int n, out int m)
        {
            n = ReadPositiveInt("Nhập số dòng (n > 0): ");
            m = ReadPositiveInt("Nhập số cột (m > 0): ");
            a = new int[n, m];
            Console.WriteLine("Nhập các phần tử của ma trận:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int value;
                    bool valid;
                    do
                    {
                        Console.Write($"a[{i}, {j}] = ");
                        string input = Console.ReadLine();

                        // Kiểm tra nhập đúng kiểu số nguyên
                        valid = int.TryParse(input, out value);

                        if (!valid)
                        {
                            Console.WriteLine("Vui lòng nhập số nguyên hợp lệ!");
                        }

                    } while (!valid);

                    a[i, j] = value;
                }
            }
        }
        static void PrintMatrix(int[,] a, int n, int m)
        {
            Console.WriteLine("\nMa trận: ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write($"{a[i, j],5}");
                }
                Console.WriteLine();
            }
        }
        // (b) Tìm kiếm một phần tử trong ma trận
        static void SearchElement(int[,] a, int n, int m)
        {
            Console.WriteLine("Nhập phần tử cần tìm: ");
            int x = int.Parse(Console.ReadLine());
            bool found = false;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (a[i, j] == x)
                    {
                        Console.WriteLine($"Tìm thấy phần tử {x} tại vị trí ({i}, {j})");
                        found = true;
                    }
                }
            }
            if (!found)
            {
                Console.WriteLine($"Không tìm thấy phần tử {x} trong ma trận");
            }
        }
        // Kiểm tra số nguyên tố
        static bool IsPrime(int n)
        {
            if (n < 1) return false;
            for (int i = 2; i <= (int)Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        // (c) Xuất các phần tử là số nguyên tố
        static void PrintPrimes(int[,] a, int n, int m)
        {
            Console.WriteLine("Các số nguyên tố trong ma trận:");
            bool haschecked = false;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (IsPrime(a[i, j]))
                    {
                        Console.Write($"{a[i, j]} ");
                        haschecked = true;
                    }
                }
            }
            if (!haschecked)
            {
                Console.WriteLine("Không có số nguyên tố trong ma trận.");
            }
            else
            {
                Console.WriteLine();
            }
        }
        // (d) Dòng có nhiều số nguyên tố nhất
        static void GetRowWithMostPrimes(int[,] a, int n, int m)
        {
            int maxcount = 0;
            int[] primeCountPerRow = new int[n];
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < m; j++)
                {
                    if (IsPrime(a[i, j]))
                    {
                        count++;
                    }
                }
                primeCountPerRow[i] = count;
                if (count > maxcount)
                {
                    maxcount = count;
                }
            }
            if (maxcount == 0)
            {
                Console.WriteLine("Không có số nguyên tố trong ma trận.");
            }
            else
            {
                Console.WriteLine($"Dòng có nhiều số nguyên tố nhất ({maxcount} số nguyên tố):");
                for (int i = 0; i < n; i++)
                {
                    if (primeCountPerRow[i] == maxcount)
                    {
                        Console.Write($"Dòng {i}: ");
                        for (int j = 0; j < m; j++)
                        {
                            Console.Write($"{a[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
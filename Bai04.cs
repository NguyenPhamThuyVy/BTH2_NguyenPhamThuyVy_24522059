using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTTH2_BT4
{
    internal class Bai04
    {
        static void Main(string[] args)
        {
            // 1) Khai báo 2 phân số
            cPhanSo ps1 = new cPhanSo();
            cPhanSo ps2 = new cPhanSo();
            List<cPhanSo> list = new List<cPhanSo>();
            int choice;
            do
            {
                // 2) In menu
                Console.WriteLine("\n=======MENU=======");
                Console.WriteLine("1. Nhập 2 phân số");
                Console.WriteLine("2. Tổng, Hiệu, Tích, Thương 2 phân số");
                Console.WriteLine("3. Nhập/Xuất dãy phân số");
                Console.WriteLine("4. Phân số lớn nhất trong dãy phân số");
                Console.WriteLine("5. Dãy phân số khi sắp xếp tăng dần");
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
                        Console.WriteLine("Nhập phân số thứ nhất: ");
                        ps1 = NhapPhanSo();
                        Console.WriteLine("Nhập phân số thứ hai: ");
                        ps2 = NhapPhanSo();
                        break;
                    case 2:
                        Console.Write($"Tổng: {ps1 + ps2}" + "  ");
                        Console.Write($"Hiệu: {ps1 - ps2}" + "  ");
                        Console.Write($"Tích: {ps1 * ps2}" + "  ");
                        Console.WriteLine($"Thương: {ps1 / ps2}" + "  ");
                        break;
                    case 3:
                        list = NhapDayPhanSo();
                        Console.WriteLine("\nDãy phân số vừa nhập: ");
                        foreach (var ps in list)
                        {
                            Console.Write(ps + " ");
                        }
                        Console.WriteLine();
                        break;
                    case 4:
                        if (list.Count == 0)
                        {
                            Console.WriteLine("Chưa tồn tại dãy phân số! Vui lòng nhập");
                        }
                        else
                        {
                            Console.WriteLine($"Phân số lớn nhất: {PhanSoLonNhat(list)}");
                        }
                        break;
                    case 5:
                        if (list.Count == 0)
                        {
                            Console.WriteLine("Chưa tồn tại dãy phân số! Vui lòng nhập");
                        }
                        else
                        {
                            SapXepTang(list);
                            Console.WriteLine($"Dãy phân số tăng dần: ");
                            foreach (var ps in list)
                            {
                                Console.Write(ps + " ");
                            }
                            Console.WriteLine();
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
        // Nhập phân số
        static cPhanSo NhapPhanSo()
        {
            int tu, mau;
            while (true)
            {
                Console.Write("Nhập tử số: ");
                if (int.TryParse(Console.ReadLine(), out tu))
                {
                    break;
                }
                Console.WriteLine("Nhập số nguyên (âm/dương) hợp lệ!");
            }
            while (true)
            {
                Console.Write("Nhập mẫu số: ");
                if (int.TryParse(Console.ReadLine(), out mau))
                {
                    if (mau == 0)
                    {
                        Console.WriteLine("Mẫu số phải khác 0! Vui lòng nhập lại");
                        continue;
                    }
                    break;
                }
                Console.WriteLine("Nhập số nguyên (âm/dương) hợp lệ!");
            }
            return new cPhanSo(tu, mau);
        }
        // Nhập dãy phân số
        static List<cPhanSo> NhapDayPhanSo()
        {
            List<cPhanSo> ds = new List<cPhanSo>();
            int n = ReadPositiveInt("Nhập số lượng phân số: ");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Phân số {i + 1}:");
                ds.Add(NhapPhanSo());
            }
            return ds;
        }
        // Hàm tìm phân số lớn nhất trong dãy phân số
        static cPhanSo PhanSoLonNhat(List<cPhanSo> ds)
        {
            cPhanSo max = ds[0];
            foreach (var ps in ds)
            {
                if (ps > max)
                {
                    max = ps;
                }
            }
            return max;
        }
        // Hàm sắp xếp dãy phân số tăng dần
        static void SapXepTang(List<cPhanSo> ds)
        {
            if (ds == null || ds.Count == 0)
            {
                Console.WriteLine("Dãy phân số rỗng!");
                return;
            }
            ds.Sort((a, b) => (a.iTuSo / (double)a.iMauSo).CompareTo(b.iTuSo / (double)b.iMauSo));
        }
    }
    public class cPhanSo
    {
        // Khai báo thuộc tính
        public int iTuSo { get; set; }
        public int iMauSo { get; set; }
        // Constructor
        public cPhanSo()
        {
            iTuSo = 0; iMauSo = 1;
        }
        public cPhanSo(int tu)
        {
            iTuSo = tu;
            iMauSo = 1;
        }
        public cPhanSo(int tu, int mau = 1)
        {
            iTuSo = tu;
            iMauSo = mau;
            RutGon();
        }
        // Hàm rút gọn phân số
        private int UCLN(int a, int b)
        {
            while (b != 0) (a, b) = (b, a % b);
            return a;
        }
        private void RutGon()
        {
            int ucln = UCLN(Math.Abs(iTuSo), Math.Abs(iMauSo));
            iTuSo /= ucln;
            iMauSo /= ucln;
            if (iMauSo < 0)
            {
                iTuSo = -iTuSo;
                iMauSo = -iMauSo;
            }
        }
        // Hiển thị phân số
        public override string ToString()
        {
            return iMauSo == 1 ? $"{iTuSo}" : $"{iTuSo}/{iMauSo}";
        }
        // Các toán tử
        public static cPhanSo operator +(cPhanSo a, cPhanSo b)
            => new cPhanSo(a.iTuSo * b.iMauSo + a.iMauSo * b.iTuSo, a.iMauSo * b.iMauSo);
        public static cPhanSo operator -(cPhanSo a, cPhanSo b)
            => new cPhanSo(a.iTuSo * b.iMauSo - a.iMauSo * b.iTuSo, a.iMauSo * b.iMauSo);
        public static cPhanSo operator *(cPhanSo a, cPhanSo b)
            => new cPhanSo(a.iTuSo * b.iTuSo, a.iMauSo * b.iMauSo);
        public static cPhanSo operator /(cPhanSo a, cPhanSo b)
            => new cPhanSo(a.iTuSo * b.iMauSo, a.iMauSo * b.iTuSo);
        // So sánh
        public static bool operator >(cPhanSo a, cPhanSo b)
            => a.iTuSo * b.iMauSo > a.iMauSo * b.iTuSo;
        public static bool operator <(cPhanSo a, cPhanSo b)
            => a.iTuSo * b.iMauSo < a.iMauSo * b.iMauSo;
        public int CompareTo(cPhanSo other)
        {
            double val1 = (double)iTuSo / iMauSo;
            double val2 = (double)other.iTuSo / other.iMauSo;
            return val1.CompareTo(val2);
        }
    }
}

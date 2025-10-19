using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BTTH2_BT5
{
    internal class Bai05
    {
        // Đọc số nguyên dương
        public static int ReadPositiveInt(string message)
        {
            int n;
            do
            {
                Console.Write(message);
            } while (!int.TryParse(Console.ReadLine(), out n) || n <= 0 || n > 1000000);
            return n;
        }
        // Kiểm tra nhập chuỗi
        public static string NhapChuoi(string message)
        {
            string chuoi;
            do
            {
                Console.Write(message);
                chuoi = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(chuoi))
                {
                    Console.WriteLine("Không được để trống! Vui lòng nhập lại.");
                }
            } while (string.IsNullOrEmpty(chuoi));
            return chuoi;
        }
        // Kiểm tra nhập kiểu dữ liệu long
        public static long NhapLong(string message)
        {
            long value;
            string input;
            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Không được để trống! Vui lòng nhập lại.");
                    continue;
                }

                if (!long.TryParse(input, out value) || value <= 0)
                {
                    Console.WriteLine("Vui lòng nhập số nguyên dương!");
                    continue;
                }
                return value;
            }
        }
        // Kiểm tra nhập kiểu dữ liệu double
        public static double NhapDouble(string message)
        {
            double value;
            string input;
            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Không được để trống! Vui lòng nhập lại.");
                    continue;
                }

                if (!double.TryParse(input, out value) || value <= 0)
                {
                    Console.WriteLine("Vui lòng nhập số thực dương!");
                    continue;
                }
                return value;
            }
        }
        static void Main(string[] args)
        {
            // 1) Khai báo danh sách 
            List<BatDongSan> ds = new List<BatDongSan>();
            int choice;
            do
            {
                // 2) In menu
                Console.WriteLine("\n=======MENU=======");
                Console.WriteLine("1. Nhập/Xuất danh sách bất động sản cần quản lý");
                Console.WriteLine("2. Tổng giá bán cho 3 loại bất động sản");
                Console.WriteLine("3. Khu đất > 100m2 hoặc Nhà phố > 60m2 & năm xây dựng >= 2019 ");
                Console.WriteLine("4. Nhà phố/Chung cư thỏa yêu cầu (địa điểm, giá, diện tích)");
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
                        NhapDanhSach(ds);
                        XuatDanhSach(ds);
                        break;
                    case 2:
                        TongGiaBan(ds);
                        break;
                    case 3:
                        DanhSachThoaYeuCau(ds);
                        break;
                    case 4:
                        TimKiem(ds);
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
        // Nhập/Xuất danh sách bất động sản
        static void NhapDanhSach(List<BatDongSan> ds)
        {
            int n = ReadPositiveInt("Nhập số lượng bất động sản: ");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Bất động sản thứ {i + 1}");
                int loai = ReadPositiveInt("Loại (1: Khu đất, 2: Nhà phố, 3: Chung cư): ");
                BatDongSan bds = null;
                switch (loai)
                {
                    case 1:
                        bds = new KhuDat();
                        break;
                    case 2:
                        bds = new NhaPho();
                        break;
                    case 3:
                        bds = new ChungCu();
                        break;
                    default:
                        Console.WriteLine("Không hợp lệ!");
                        continue;
                }
                if (bds == null)
                {
                    Console.WriteLine("Không hợp lệ!");
                    i--;
                    continue;
                }
                bds.Nhap();
                ds.Add(bds);
            }
        }
        static void XuatDanhSach(List<BatDongSan>ds)
        {
            Console.WriteLine("\n=== DANH SÁCH BẤT ĐỘNG SẢN ===");
            foreach (var bds in ds)
            {
                bds.Xuat();
                Console.WriteLine("-----------------");
            }
        }
        // Tổng giá bán cho 3 loại (Khu đất, Nhà phố, Chung cư) của công ty Đại Phú
        static void TongGiaBan(List<BatDongSan> ds)
        {
            if (ds.Count == 0)
            {
                Console.WriteLine("Danh sách trống!");
                return;
            }
            long TongDat = 0, TongNha = 0, TongCC = 0, Tong = 0;
            foreach (var x in ds)
            {
                switch (x)
                {
                    case KhuDat: TongDat += x.GiaBan; break;
                    case NhaPho: TongNha += x.GiaBan; break;
                    case ChungCu: TongCC += x.GiaBan; break;
                }
            }
            Tong = TongDat + TongNha + TongCC;
            Console.WriteLine($"Tổng giá bán: {Tong}VND");
        }
        // Danh sách các khu đất có diện tích > 100m2 hoặc là nhà phố có diện tích > 60m2 và năm xây dựng >= 2019
        static void DanhSachThoaYeuCau(List<BatDongSan> ds)
        {
            var ketqua = ds.Where(bds => bds.ThoaDieuKien());
            Console.WriteLine("\n=== DANH SÁCH THỎA ĐIỀU KIỆN ===");
            foreach (var bds in ketqua)
            {
                bds.Xuat();
                Console.WriteLine("-----------------");
            }
            if (!ketqua.Any())
            {
                Console.WriteLine("Không có kết quả!");
            }
        }
        // Danh sách các nhà phố hoặc chung cư phù hợp với yêu cầu
        static void TimKiem(List<BatDongSan> ds)
        {
            if (ds.Count == 0)
            {
                Console.WriteLine("Danh sách trống!");
                return;
            }
            string dd = NhapChuoi("Nhập địa điểm cần tìm: ").ToLower();
            long maxGia = NhapLong("Nhập giá tối đa (VND): ");
            double minDT = NhapDouble("Nhập diện tích tối thiểu (m2): ");
            var ketqua = ds.Where(bds =>
             (bds is NhaPho || bds is ChungCu) &&
             bds.DiaDiem.ToLower().Contains(dd) &&
             bds.GiaBan <= maxGia &&
             bds.DienTich >= minDT
            );
            Console.WriteLine("\n=== KẾT QUẢ TÌM KIẾM ===");
            foreach (var bds in ketqua)
            {
                bds.Xuat();
                Console.WriteLine("-----------------");
            }
            if (!ketqua.Any())
            {
                Console.WriteLine("Không có kết quả!");
            }
        }
    }
    public class BatDongSan
    {
        // Khai báo thuộc tính
        public string DiaDiem { get; set; }
        public long GiaBan { get; set; }
        public double DienTich {  get; set; }
        // Khai báo phương thức 
        public virtual void Nhap()
        {
            DiaDiem = Bai05.NhapChuoi("Nhập địa điểm: ");
            GiaBan = Bai05.NhapLong("Nhập giá bán: ");
            DienTich = Bai05.NhapDouble("Nhập diện tích: ");
        }
        public virtual void Xuat()
        {
            Console.WriteLine($"Địa điểm: {DiaDiem} | Giá bán: {GiaBan}VND | Diện tích: {DienTich}m2");
        }
        public virtual bool ThoaDieuKien() => false;
    }
    class KhuDat : BatDongSan
    {
        // override
        public override void Xuat()
        {
            Console.WriteLine("---KHU ĐẤT---");    
            base.Xuat();
        }
        public override bool ThoaDieuKien() => DienTich > 100;
    }
    class NhaPho : BatDongSan
    {
        // Khai báo thuộc tính
        public int NamXayDung { get; set; }
        public byte SoTang { get; set; }
        // override
        public override void Nhap()
        {
            base.Nhap();
            NamXayDung = Bai05.ReadPositiveInt("Nhập năm xây dựng: ");
            SoTang = (byte)Bai05.ReadPositiveInt("Nhập số tầng: ");
        }
        public override void Xuat()
        {
            Console.WriteLine("---NHÀ PHỐ---");
            Console.WriteLine($"Địa điểm: {DiaDiem} | Giá bán: {GiaBan}VND | Diện tích: {DienTich}m2 | Năm xây dựng: {NamXayDung} | Số tầng: {SoTang}");
        }
        public override bool ThoaDieuKien() => DienTich > 60 && NamXayDung >= 2019;
    }
    class ChungCu : BatDongSan
    {
        // Khai báo thuộc tính
        public byte Tang { get; set; }
        // override
        public override void Nhap()
        {
            base.Nhap();
            Tang = (byte)Bai05.ReadPositiveInt("Nhập tầng: ");
        }
        public override void Xuat()
        {
            Console.WriteLine("---CHUNG CƯ---");
            Console.WriteLine($"Địa điểm: {DiaDiem} | Giá bán: {GiaBan}VND | Diện tích: {DienTich}m2 | Tầng: {Tang}");
        }
    }
}

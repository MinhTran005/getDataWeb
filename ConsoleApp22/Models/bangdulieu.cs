

namespace ConsoleApp22.Models
{
    //[Table("ThongTinNangLuong")]
    public class bangdulieu
    {
        //[Key]
        public int Id { get; set; }
        public string DonVi { get; set; }
        public int HienTai { get; set; }
        public int CongSuatMax { get; set; }
        public int ThietKe { get; set; }
        public int SanLuongNgay { get; set; }
    }
}

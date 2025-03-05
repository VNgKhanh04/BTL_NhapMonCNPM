namespace BTL_NMCNPM.Models
{
    public class BaiHat
    {
        public int Id { get; set; }
        public string TenBaiHat { get; set; } = string.Empty;
        public int FK_TheLoai { get; set; }
        public string LinkFile { get; set; } = string.Empty;
        public DateTime ThoiGian_Up { get; set; }
        public int FK_NguoiUp { get; set; }
        public bool Deleted { get; set; }
    }

}

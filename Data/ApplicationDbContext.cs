using BTL_NhapMonCNPM.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Taikhoan> TaiKhoans { get; set; }
    public DbSet<Baihat> BaiHats { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Binhluan> BinhLuans { get; set; }
    public DbSet<Theloai> Theloais { get; set; }
    public DbSet<Playlist_baihat> Playlist_Baihats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Taikhoan>().ToTable("tbl_Taikhoan");
        modelBuilder.Entity<Baihat>().ToTable("tbl_Baihat");
        modelBuilder.Entity<Playlist>().ToTable("tbl_Playlist");
        modelBuilder.Entity<Binhluan>().ToTable("tbl_Binhluan");
        modelBuilder.Entity<Theloai>().ToTable("tbl_Theloai");
        modelBuilder.Entity<Playlist_baihat>().ToTable("tbl_Playlist_baihat");
    }
}

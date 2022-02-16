using Microsoft.EntityFrameworkCore;


namespace ConsoleApp22.Models
{
    public class GetdataDbContext : DbContext
    {

        public DbSet<bangdulieu> datas { get; set; }


        private const string connectionString = "Data Source=DESKTOP-QOLS1P3\\SQLEXPRESS;Initial Catalog=getDataWeb;Integrated Security=true";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<bangdulieu>(entity =>
            {
                entity.ToTable("BangThuThapDuLieu");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DonVi)
                    .HasMaxLength(50)
                    .HasColumnName("donVi");
                entity.Property(e => e.HienTai)
                 .HasMaxLength(50)
                 .HasColumnName("hienTai");

                entity.Property(e => e.CongSuatMax)
                    .HasMaxLength(50)
                    .HasColumnName("congSuatLonNhat");

                entity.Property(e => e.ThietKe)
                    .HasMaxLength(50)
                    .HasColumnName("thietKe");

                entity.Property(e => e.SanLuongNgay)
                    .HasMaxLength(50)
                    .HasColumnName("sanLuongNgay");

            });


        }

    }
}

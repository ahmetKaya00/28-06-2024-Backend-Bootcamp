using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data{
    public class DataContext : DbContext{

        public DataContext(DbContextOptions<DataContext> options):base(options){}
        public DbSet<Bootcamp> Bootcamps => Set<Bootcamp>();
        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();
        public DbSet<BootcampKayit> KursKayitlari => Set<BootcampKayit>();
    }
}
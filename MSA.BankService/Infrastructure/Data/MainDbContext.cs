using MSA.Common.PostgresMassTransit.PostgresDB;
using MSA.BankService.Domain;
using Microsoft.EntityFrameworkCore;

namespace MSA.BankService.Infrastructure.Data
{
    public class MainDbContext : AppDbContextBase
    {
        private readonly string _uuidGenerator = "uuid-ossp";
        private readonly string _uuidAlgorithm = "uuid_generate_v4()";
        private readonly IConfiguration configuration;

        public MainDbContext(
            IConfiguration configuration,
            DbContextOptions<MainDbContext> options) : base(configuration, options)
        {
            this.configuration = configuration;
        }

        public DbSet<Bank> Banks { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension(_uuidGenerator);

            //Banks
            modelBuilder.Entity<Bank>().ToTable("Banks");
            modelBuilder.Entity<Bank>().HasKey(x => x.Id);
            modelBuilder.Entity<Bank>().Property(x => x.Id)
                .HasColumnType("uuid");
                //.HasDefaultValueSql(_uuidAlgorithm);
        }
    }
}
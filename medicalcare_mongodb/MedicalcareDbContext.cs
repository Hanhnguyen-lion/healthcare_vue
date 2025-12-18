using medicalcare_mongodb.models;
using Microsoft.EntityFrameworkCore;

namespace medicalcare_mongodb
{
    public class MedicalcareDbContext: DbContext
    {
        public DbSet<Account> m_account{get;set;}
        public DbSet<Hospital> m_hospital{get;set;}
        public DbSet<TreatmentCategory> m_treatment_category{get;set;}
        public DbSet<MedicineType> m_medicine_type{get;set;}
        public MedicalcareDbContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().HasKey(a=> a.id);
            modelBuilder.Entity<Hospital>().HasKey(a=> a.id);
            modelBuilder.Entity<MedicineType>().HasKey(a=> a.id);
            modelBuilder.Entity<TreatmentCategory>().HasKey(a=> a.id);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using MTCBackend.Models;

namespace MTCBackend.Data
{
    public class MTCContext : DbContext
    {
        public MTCContext(DbContextOptions<MTCContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public DbSet<FormSubmission> FormSubmissions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DigitalSignature> DigitalSignatures { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<LabResult> LabResults { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Appointment has a one-to-many relationship with Patient and Staff
            modelBuilder.Entity<Appointment>()
                 .HasOne(a => a.Patient)
                 .WithMany(p => p.PatientAppointments)
                 .HasForeignKey(a => a.PatientId)
                 .OnDelete(DeleteBehavior.SetNull);

            // Configure the relationship for User as Staff in Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Staff)
                .WithMany(u => u.StaffAppointments)  // StaffAppointments is the collection in User for staff members
                .HasForeignKey(a => a.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define Ledger entity with PatientId as primary key and foreign key
            modelBuilder.Entity<Ledger>()
                .HasKey(l => l.PatientId);

            modelBuilder.Entity<Ledger>()
                .HasOne(l => l.Patient)
                .WithMany()  // A patient can have many ledgers (or define inverse if needed)
                .HasForeignKey(l => l.PatientId);

            // Form has a one-to-many relationship with FormFields
            modelBuilder.Entity<Form>()
                .HasMany(f => f.Fields)
                .WithOne(ff => ff.Form)
                .HasForeignKey(ff => ff.FormId);

            // A Form is linked to a Patient
            modelBuilder.Entity<Form>()
                .HasOne(f => f.Patient)
                .WithMany(p => p.Forms)
                .HasForeignKey(f => f.PatientId);

            // A FormSubmission is linked to a Form and a Patient
            modelBuilder.Entity<FormSubmission>()
                .HasOne(fs => fs.Form)
                .WithMany(f => f.Submissions)
                .HasForeignKey(fs => fs.FormId);

            modelBuilder.Entity<FormSubmission>()
                .HasOne(fs => fs.Patient)
                .WithMany(p => p.Submissions)
                .HasForeignKey(fs => fs.PatientId)
                .OnDelete(DeleteBehavior.Restrict);  // Or use DeleteBehavior.NoAction to prevent cycles


            // A FormSubmission has an optional DigitalSignature
            modelBuilder.Entity<FormSubmission>()
                .HasOne(fs => fs.Signature)
                .WithOne(ds => ds.FormSubmission)
                .HasForeignKey<DigitalSignature>(ds => ds.FormSubmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // A FormSubmission has an optional UploadedFile (for PDF)
            modelBuilder.Entity<FormSubmission>()
                .HasMany(fs => fs.UploadedFiles)
                .WithOne(uf => uf.FormSubmission)
                .HasForeignKey(uf => uf.FormSubmissionId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete if necessary

            modelBuilder.Entity<UploadedFile>()
                .HasOne(uf => uf.Form)
                .WithMany(f => f.UploadedFiles)
                .HasForeignKey(uf => uf.FormId)
                .OnDelete(DeleteBehavior.Restrict);  // Or DeleteBehavior.NoAction

            modelBuilder.Entity<UploadedFile>()
                .HasOne(uf => uf.Patient)
                .WithMany(p => p.UploadedFiles)  // Assuming Patient has a collection of UploadedFiles
                .HasForeignKey(uf => uf.PatientId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete to avoid cycle

            modelBuilder.Entity<DigitalSignature>()
                .HasOne(ds => ds.Form)
                .WithMany(f => f.DigitalSignatures)  // Assuming Form has a collection of DigitalSignatures
                .HasForeignKey(ds => ds.FormId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete to avoid cycle

            modelBuilder.Entity<DigitalSignature>()
                .HasOne(ds => ds.Patient)
                .WithMany(p => p.DigitalSignatures)  // Assuming Patient has a collection of DigitalSignatures
                .HasForeignKey(ds => ds.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient -> LabResults relationship
            modelBuilder.Entity<LabResult>()
                .HasOne(lr => lr.Patient)
                .WithMany(p => p.LabResults)
                .HasForeignKey(lr => lr.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}


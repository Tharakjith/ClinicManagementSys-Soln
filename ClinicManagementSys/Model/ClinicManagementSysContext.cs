using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSys.Model;

public partial class ClinicManagementSysContext : DbContext
{
    public ClinicManagementSysContext()
    {
    }

    public ClinicManagementSysContext(DbContextOptions<ClinicManagementSysContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

    public virtual DbSet<Availability> Availabilities { get; set; }

    public virtual DbSet<BillStatus> BillStatuses { get; set; }

    public virtual DbSet<DailyAvailability> DailyAvailabilities { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<LabTestBill> LabTestBills { get; set; }

    public virtual DbSet<LabTestBillStatus> LabTestBillStatuses { get; set; }

    public virtual DbSet<LabTestReport> LabTestReports { get; set; }

    public virtual DbSet<Labtest> Labtests { get; set; }

    public virtual DbSet<LoginRegistration> LoginRegistrations { get; set; }

    public virtual DbSet<MedDistributionStatus> MedDistributionStatuses { get; set; }

    public virtual DbSet<MedicineBill> MedicineBills { get; set; }

    public virtual DbSet<MedicineBillStatus> MedicineBillStatuses { get; set; }

    public virtual DbSet<MedicineDetail> MedicineDetails { get; set; }

    public virtual DbSet<MedicineDistribution> MedicineDistributions { get; set; }

    public virtual DbSet<MedicineInventory> MedicineInventories { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientBill> PatientBills { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StartDiagnosy> StartDiagnosys { get; set; }

    public virtual DbSet<TestPrescription> TestPrescriptions { get; set; }

    public virtual DbSet<Timeslot> Timeslots { get; set; }

    public virtual DbSet<Weekday> Weekdays { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source =LAPTOP-1D8H5N1A\\SQLEXPRESS; Initial Catalog = ClinicManagementSys; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC25B32C811");

            entity.ToTable("Appointment");

            entity.HasIndex(e => e.TokenNumber, "UQ__Appointm__435734E1FD4D00A8");

            entity.Property(e => e.AppointmentDate).HasColumnType("date");
            entity.Property(e => e.ConsultationFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RegistrationFee)
                .HasDefaultValueSql("((150.00))")
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.AppointmentStatus).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AppointmentStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Appoi__619B8048");

            entity.HasOne(d => d.Availability).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AvailabilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Avail__5FB337D6");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Docto__5EBF139D");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Patie__5CD6CB2B");

            entity.HasOne(d => d.Specialization).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.SpecializationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Speci__5DCAEF64");
        });

        modelBuilder.Entity<AppointmentStatus>(entity =>
        {
            entity.HasKey(e => e.AppointmentStatusId).HasName("PK__Appointm__A619B6607B2275E9");

            entity.ToTable("AppointmentStatus");

            entity.HasIndex(e => e.AppointmentStatus1, "UQ__Appointm__BABC6966E460B9EF").IsUnique();

            entity.Property(e => e.AppointmentStatusId).ValueGeneratedNever();
            entity.Property(e => e.AppointmentStatus1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("AppointmentStatus");
        });

        modelBuilder.Entity<Availability>(entity =>
        {
            entity.HasKey(e => e.AvailabilityId).HasName("PK__availabi__DA3979B16B2AFE46");

            entity.ToTable("availability");

            entity.Property(e => e.Session)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Doctor).WithMany(p => p.Availabilities)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__availabil__Docto__4E88ABD4");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.Availabilities)
                .HasForeignKey(d => d.TimeSlotId)
                .HasConstraintName("FK__availabil__TimeS__4F7CD00D");
        });

        modelBuilder.Entity<BillStatus>(entity =>
        {
            entity.HasKey(e => e.BillStatusId).HasName("PK__BillStat__C3D1285184DAAA50");

            entity.ToTable("BillStatus");

            entity.HasIndex(e => e.BillStatus1, "UQ__BillStat__662BD8AEA1678C87").IsUnique();

            entity.Property(e => e.BillStatusId).ValueGeneratedNever();
            entity.Property(e => e.BillStatus1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("BillStatus");
        });

        modelBuilder.Entity<DailyAvailability>(entity =>
        {
            entity.HasKey(e => e.DailyAvailabilityId).HasName("PK__DailyAva__C0E8C4C578BE3862");

            entity.ToTable("DailyAvailability");

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Appointment).WithMany(p => p.DailyAvailabilities)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DailyAvai__Appoi__656C112C");

            entity.HasOne(d => d.Availability).WithMany(p => p.DailyAvailabilities)
                .HasForeignKey(d => d.AvailabilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DailyAvai__Avail__6477ECF3");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__departme__B2079BEDE9925BBC");

            entity.ToTable("department");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__doctor__2DC00EBF65EFA9D1");

            entity.ToTable("doctor");

            entity.Property(e => e.ConsultationFee).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Registration).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__doctor__Registra__46E78A0C");

            entity.HasOne(d => d.Specialization).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SpecializationId)
                .HasConstraintName("FK__doctor__Speciali__45F365D3");
        });

        modelBuilder.Entity<LabTestBill>(entity =>
        {
            entity.HasKey(e => e.LabTestBillId).HasName("PK__LabTestB__2DECF1FD1E1BD0C5");

            entity.ToTable("LabTestBill");

            entity.Property(e => e.LtreportId).HasColumnName("LTReportId");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.TestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TestPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.LabTestBillStatus).WithMany(p => p.LabTestBills)
                .HasForeignKey(d => d.LabTestBillStatusId)
                .HasConstraintName("FK__LabTestBi__LabTe__1AD3FDA4");

            entity.HasOne(d => d.Ltreport).WithMany(p => p.LabTestBills)
                .HasForeignKey(d => d.LtreportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTestBi__LTRep__18EBB532");
        });

        modelBuilder.Entity<LabTestBillStatus>(entity =>
        {
            entity.HasKey(e => e.LabTestBillStatusId).HasName("PK__LabTestB__224C5B0E1FFA65A3");

            entity.ToTable("LabTestBillStatus");

            entity.HasIndex(e => e.LabTestBillStatus1, "UQ__LabTestB__15FEC2E279B30535").IsUnique();

            entity.Property(e => e.LabTestBillStatusId).ValueGeneratedNever();
            entity.Property(e => e.LabTestBillStatus1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LabTestBillStatus");
        });

        modelBuilder.Entity<LabTestReport>(entity =>
        {
            entity.HasKey(e => e.LtreportId).HasName("PK__LabTestR__BF47B053003366D8");

            entity.ToTable("LabTestReport");

            entity.Property(e => e.LtreportId).HasColumnName("LTReportId");
            entity.Property(e => e.Remarks)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.Appointment).WithMany(p => p.LabTestReports)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTestRe__Appoi__114A936A");

            entity.HasOne(d => d.LabTest).WithMany(p => p.LabTestReports)
                .HasForeignKey(d => d.LabTestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabTestRe__LabTe__123EB7A3");
        });

        modelBuilder.Entity<Labtest>(entity =>
        {
            entity.HasKey(e => e.LabTestId).HasName("PK__Labtest__64D33925BD53E01B");

            entity.ToTable("Labtest");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("date");
            entity.Property(e => e.HighRange).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.LowRange).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Sample)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TestName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LoginRegistration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__LoginReg__6EF588108D8B0AFB");

            entity.ToTable("LoginRegistration");

            entity.HasIndex(e => e.Username, "UQ__LoginReg__536C85E416F563F6").IsUnique();

            entity.Property(e => e.Password)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.RegisteredDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("date");
            entity.Property(e => e.RisActive).HasColumnName("RIsActive");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.LoginRegistrations)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__LoginRegi__RoleI__403A8C7D");

            entity.HasOne(d => d.Staff).WithMany(p => p.LoginRegistrations)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__LoginRegi__Staff__3F466844");
        });

        modelBuilder.Entity<MedDistributionStatus>(entity =>
        {
            entity.HasKey(e => e.MedStatusId).HasName("PK__MedDistr__1DC29487AF1795A7");

            entity.ToTable("MedDistributionStatus");

            entity.HasIndex(e => e.MedStatusName, "UQ__MedDistr__F3A409D9F1AB15CE").IsUnique();

            entity.Property(e => e.MedStatusId).ValueGeneratedNever();
            entity.Property(e => e.MedStatusName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MedicineBill>(entity =>
        {
            entity.HasKey(e => e.MedicineBillId).HasName("PK__Medicine__EF3B0260442B5BF8");

            entity.ToTable("MedicineBill");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicineBills)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__MedicineB__Docto__0C85DE4D");

            entity.HasOne(d => d.MedDist).WithMany(p => p.MedicineBills)
                .HasForeignKey(d => d.MedDistId)
                .HasConstraintName("FK__MedicineB__MedDi__0B91BA14");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicineBills)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__MedicineB__Patie__0D7A0286");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.MedicineBills)
                .HasForeignKey(d => d.PaymentStatusId)
                .HasConstraintName("FK__MedicineB__Payme__0E6E26BF");
        });

        modelBuilder.Entity<MedicineBillStatus>(entity =>
        {
            entity.HasKey(e => e.PaymentStatusId).HasName("PK__Medicine__34F8AC3FD58801D1");

            entity.ToTable("MedicineBillStatus");

            entity.HasIndex(e => e.PaymentStatusName, "UQ__Medicine__BBAC58DBD56938A4").IsUnique();

            entity.Property(e => e.PaymentStatusId).ValueGeneratedNever();
            entity.Property(e => e.PaymentStatusName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MedicineDetail>(entity =>
        {
            entity.HasKey(e => e.MedicineId).HasName("PK__Medicine__4F212890E151A4BE");

            entity.Property(e => e.Category).HasMaxLength(10);
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExpiryDate).HasColumnType("date");
            entity.Property(e => e.ManufacturingDate).HasColumnType("date");
            entity.Property(e => e.MedicineName).HasMaxLength(100);
        });

        modelBuilder.Entity<MedicineDistribution>(entity =>
        {
            entity.HasKey(e => e.MedDistId).HasName("PK__Medicine__BC4B56A91C6F638D");

            entity.ToTable("MedicineDistribution");

            entity.Property(e => e.DistributionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MedStatus).WithMany(p => p.MedicineDistributions)
                .HasForeignKey(d => d.MedStatusId)
                .HasConstraintName("FK__MedicineD__MedSt__08B54D69");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicineDistributions)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__MedicineD__Medic__06CD04F7");

            entity.HasOne(d => d.Prescription).WithMany(p => p.MedicineDistributions)
                .HasForeignKey(d => d.PrescriptionId)
                .HasConstraintName("FK__MedicineD__Presc__05D8E0BE");
        });

        modelBuilder.Entity<MedicineInventory>(entity =>
        {
            entity.HasKey(e => e.MedicineStockId).HasName("PK__Medicine__FA952718B74B7BE4");

            entity.ToTable("MedicineInventory");

            entity.HasOne(d => d.Medicine).WithMany(p => p.MedicineInventories)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__MedicineI__Medic__7D439ABD");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC366EA22F529");

            entity.ToTable("Patient");

            entity.Property(e => e.BloodGroup)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.PatientAddress)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PatientName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientPhone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientBill>(entity =>
        {
            entity.HasKey(e => e.PatientBillId).HasName("PK__PatientB__B73F3B212A3A785A");

            entity.ToTable("PatientBill");

            entity.Property(e => e.AppointmentDate).HasColumnType("date");
            entity.Property(e => e.ConsultationFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RegistrationFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalFee)
                .HasComputedColumnSql("([ConsultationFee]+[RegistrationFee])", true)
                .HasColumnType("decimal(11, 2)");

            entity.HasOne(d => d.Appointment).WithMany(p => p.PatientBills)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientBi__Appoi__6C190EBB");

            entity.HasOne(d => d.BillStatus).WithMany(p => p.PatientBills)
                .HasForeignKey(d => d.BillStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientBi__BillS__6D0D32F4");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PK__Prescrip__40130832BB3E7E4D");

            entity.ToTable("Prescription");

            entity.Property(e => e.Dosage)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Frequency)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Appointment).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Prescript__Appoi__797309D9");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK__Prescript__Medic__7A672E12");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A93C658A9");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__speciali__5809D86FA0F4959C");

            entity.ToTable("specialization");

            entity.Property(e => e.SpecializationName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__staff__96D4AB17BF18A169");

            entity.ToTable("staff");

            entity.Property(e => e.Address)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("date");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Doj)
                .HasColumnType("date")
                .HasColumnName("DOJ");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StaffName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Staff)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__staff__Departmen__3B75D760");
        });

        modelBuilder.Entity<StartDiagnosy>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__StartDia__4D7B4ABD0BAF98A3");

            entity.Property(e => e.Diagnosis)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DiagnosysDate).HasColumnType("datetime");
            entity.Property(e => e.DoctorNote)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NextVisiting).HasColumnType("datetime");
            entity.Property(e => e.Symptoms)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Appointment).WithMany(p => p.StartDiagnosies)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__StartDiag__Appoi__70DDC3D8");

            entity.HasOne(d => d.ReferenceNavigation).WithMany(p => p.StartDiagnosies)
                .HasForeignKey(d => d.Reference)
                .HasConstraintName("FK__StartDiag__Refer__6FE99F9F");
        });

        modelBuilder.Entity<TestPrescription>(entity =>
        {
            entity.HasKey(e => e.TpId).HasName("PK__TestPres__8106F22408191B17");

            entity.ToTable("TestPrescription");

            entity.Property(e => e.TpId).HasColumnName("TP_Id");
            entity.Property(e => e.SampleItem)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Appointment).WithMany(p => p.TestPrescriptions)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__TestPresc__Appoi__73BA3083");

            entity.HasOne(d => d.LabTest).WithMany(p => p.TestPrescriptions)
                .HasForeignKey(d => d.LabTestId)
                .HasConstraintName("FK__TestPresc__LabTe__74AE54BC");
        });

        modelBuilder.Entity<Timeslot>(entity =>
        {
            entity.HasKey(e => e.TimeSlotId).HasName("PK__timeslot__41CC1F3299ED0EAD");

            entity.ToTable("timeslot");

            entity.HasOne(d => d.Weekdays).WithMany(p => p.Timeslots)
                .HasForeignKey(d => d.WeekdaysId)
                .HasConstraintName("FK__timeslot__Weekda__4BAC3F29");
        });

        modelBuilder.Entity<Weekday>(entity =>
        {
            entity.HasKey(e => e.WeekdaysId).HasName("PK__weekdays__D053E4AFF5D7FAD6");

            entity.ToTable("weekdays");

            entity.Property(e => e.WeekdaysName)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

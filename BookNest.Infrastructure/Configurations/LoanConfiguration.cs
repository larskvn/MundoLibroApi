using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookNest.Infrastructure.Configurations;

public class LoanConfiguration: IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("prestamos");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.LoanDate).HasColumnName("fecha_prestamo");
        builder.Property(x => x.ReturnDate).HasColumnName("fecha_devolucion");
        builder.Property(x => x.LoanStatus).HasColumnName("estado_prestamo");
        builder.Property(x => x.ApplicantId).HasColumnName("id_solicitante");
        builder.Property(x => x.RegistrationDate).HasColumnName("fecha_registro");
        builder.Property(x => x.Status).HasColumnName("estado");

       
        builder.HasOne(x => x.Applicant)
            .WithMany(a => a.Loans) 
            .HasForeignKey(x => x.ApplicantId)
            .HasConstraintName("fk_prestamos_id_solicitante");

        
        builder.HasMany(x => x.LoanDetails)
            .WithOne(ld => ld.Loan)
            .HasForeignKey(ld => ld.LoanId)
            .HasConstraintName("fk_prestamos_detalles_id_prestamo");
    }
}
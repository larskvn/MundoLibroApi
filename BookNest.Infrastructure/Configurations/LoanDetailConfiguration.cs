using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookNest.Infrastructure.Configurations;

public class LoanDetailConfiguration: IEntityTypeConfiguration<LoanDetail>
{
    public void Configure(EntityTypeBuilder<LoanDetail> builder)
    {
        builder.ToTable("prestamos_detalles");
        builder.HasKey(x => new { x.LoanId, x.BookId });

        builder.Property(x => x.LoanId).HasColumnName("id_prestamo");
        builder.Property(x => x.BookId).HasColumnName("id_libro");
        builder.Property(x => x.IsReturned).HasColumnName("devuelto");
        builder.Property(x => x.Penalty).HasColumnName("mora");
        builder.Property(x => x.RegistrationDate).HasColumnName("fecha_registro");
        builder.Property(x => x.Status).HasColumnName("estado");

        builder.HasOne(x => x.Loan)
            .WithMany(l => l.LoanDetails)
            .HasForeignKey(x => x.LoanId)
            .HasConstraintName("fk_prestamos_detalles_id_prestamo");

        builder.HasOne(x => x.Book)
            .WithMany()
            .HasForeignKey(x => x.BookId)
            .HasConstraintName("fk_prestamos_detalles_id_libro");

    }
}
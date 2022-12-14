// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payment.Persistence;

namespace Payment.Persistence.Migrations
{
    [DbContext(typeof(PaymentDbContext))]
    [Migration("20190224235305_fixes")]
    partial class fixes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Payment.Domain.Entities.Payments", b =>
                {
                    b.Property<string>("PaymentsId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BookingOrderId");

                    b.Property<int>("PaymentStatus");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("PaymentsId");

                    b.ToTable("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}

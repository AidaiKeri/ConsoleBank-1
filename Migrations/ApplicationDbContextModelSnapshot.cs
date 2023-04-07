﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Migration49;

namespace ConsoleBank_1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConsoleBank.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConfirmPassword")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("DateOfCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("ConsoleBank_1.Models.HistoryOfTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<double>("Commission")
                        .HasColumnType("float");

                    b.Property<string>("NameOfTransaction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recipient")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecipientBank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderBank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("HistoryOfTransactions");
                });

            modelBuilder.Entity("ConsoleBank_1.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PaidDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ServiceAddDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Services")
                        .HasColumnType("int");

                    b.Property<decimal>("Sum")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ConsoleBank.Admin", b =>
                {
                    b.HasBaseType("ConsoleBank.Person");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("ConsoleBank.Client", b =>
                {
                    b.HasBaseType("ConsoleBank.Person");

                    b.Property<int>("Bank")
                        .HasColumnType("int");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("ConsoleBank_1.Models.HistoryOfTransaction", b =>
                {
                    b.HasOne("ConsoleBank.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ConsoleBank_1.Models.Service", b =>
                {
                    b.HasOne("ConsoleBank.Client", "Client")
                        .WithMany("Service")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ConsoleBank.Client", b =>
                {
                    b.Navigation("Service");
                });
#pragma warning restore 612, 618
        }
    }
}

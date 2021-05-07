﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestePMWEB.Context;

namespace TestePMWEB.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210507163851_sp_AtualizaCons_RFV")]
    partial class sp_AtualizaCons_RFV
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TestePMWEB.Models.API_Log", b =>
                {
                    b.Property<int>("ID_MENSAGEM")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("DATA_REFERENCIA");

                    b.Property<int>("DETALHE");

                    b.Property<string>("MENSAGEM");

                    b.Property<short>("RESULTADO");

                    b.Property<string>("TIPO");

                    b.HasKey("ID_MENSAGEM");

                    b.ToTable("API_Logs");
                });

            modelBuilder.Entity("TestePMWEB.Models.Cliente", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("CIDADE")
                        .HasMaxLength(50);

                    b.Property<DateTime>("DATA_NASCIMENTO");

                    b.Property<string>("EMAIL")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NOME")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<short>("PERMISSAO_RECEBE_EMAIL");

                    b.Property<string>("UF")
                        .HasMaxLength(2);

                    b.HasKey("ID");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("TestePMWEB.Models.Cons_Cliente", b =>
                {
                    b.Property<int>("ID_CLIENTE");

                    b.Property<string>("CIDADE")
                        .HasMaxLength(50);

                    b.Property<DateTime>("DATA_NASCIMENTO");

                    b.Property<decimal?>("FAIXA")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnType("money");

                    b.Property<decimal?>("LTV")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnType("money");

                    b.Property<int?>("QTD_COMPRAS_12M");

                    b.Property<int?>("TEMPO_MEDIOCOMPRAS");

                    b.Property<string>("TIERS")
                        .HasMaxLength(10);

                    b.Property<string>("UF")
                        .HasMaxLength(2);

                    b.HasKey("ID_CLIENTE");

                    b.ToTable("Cons_Clientes");
                });

            modelBuilder.Entity("TestePMWEB.Models.Cons_RFV", b =>
                {
                    b.Property<int>("ID_CLIENTE");

                    b.Property<DateTime?>("DATA_ULTIMA_COMPRA");

                    b.Property<int?>("FREQUENCIA_COMPRA_12M");

                    b.Property<int?>("FREQUENCIA_COMPRA_ALL");

                    b.Property<string>("MEIO_PAGAMENTO_PREFER")
                        .HasMaxLength(30);

                    b.Property<string>("PARCELAMENTO_PREFER")
                        .HasMaxLength(20);

                    b.Property<decimal?>("TICKET_MEDIO_12M")
                        .HasColumnType("decimal(8, 3)");

                    b.Property<decimal?>("TICKET_MEDIO_ALL")
                        .HasColumnType("decimal(8, 3)");

                    b.Property<string>("TIER_ATUAL");

                    b.Property<string>("ULTIMO_DEPTO_COMPRA")
                        .HasMaxLength(30);

                    b.HasKey("ID_CLIENTE");

                    b.ToTable("CONS_RFV");
                });

            modelBuilder.Entity("TestePMWEB.Models.Pedido", b =>
                {
                    b.Property<int>("ID_PEDIDO");

                    b.Property<int>("ID_CLIENTE");

                    b.Property<int>("ID_PRODUTO");

                    b.Property<DateTime>("DATA_PEDIDO");

                    b.Property<string>("DEPARTAMENTO")
                        .HasMaxLength(50);

                    b.Property<string>("MEIO_PAGAMENTO")
                        .HasMaxLength(50);

                    b.Property<int>("PARCELAS");

                    b.Property<int>("QUANTIDADE");

                    b.Property<string>("STATUS_PAGAMENTO")
                        .HasMaxLength(50);

                    b.Property<decimal>("VALOR_UNITARIO")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnType("money");

                    b.HasKey("ID_PEDIDO", "ID_CLIENTE", "ID_PRODUTO");

                    b.HasAlternateKey("ID_CLIENTE", "ID_PEDIDO", "ID_PRODUTO");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("TestePMWEB.Models.Tier", b =>
                {
                    b.Property<string>("Faixa")
                        .HasMaxLength(10);

                    b.Property<int?>("ValorMax");

                    b.Property<int?>("ValorMin");

                    b.HasKey("Faixa");

                    b.ToTable("Tiers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestePMWEB.Models.Pedido", b =>
                {
                    b.HasOne("TestePMWEB.Models.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ID_CLIENTE")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

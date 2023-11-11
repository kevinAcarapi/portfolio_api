﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_portfolio.Data.DataContext;

#nullable disable

namespace api_portfolio.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("api_portafolio.Entities.Cards.Card", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Enlace")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Card");
                });

            modelBuilder.Entity("api_portafolio.Entities.Skills.SoftSkills.SoftSkill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SoftSkills");
                });

            modelBuilder.Entity("api_portafolio.Entities.Skills.TechnicalSkills.Technology", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("api_portafolio.Entities.TechnologiesCatalog.TechnologyByProject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<long>("TechnologyId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("TechnologiesByProject");
                });

            modelBuilder.Entity("api_portafolio.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Curriculum")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Profesion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProfilePhoto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("api_portafolio.Entities.Projects.Project", b =>
                {
                    b.HasBaseType("api_portafolio.Entities.Cards.Card");

                    b.Property<long?>("UserId1")
                        .HasColumnType("bigint");

                    b.HasIndex("UserId1");

                    b.HasDiscriminator().HasValue("Project");
                });

            modelBuilder.Entity("api_portafolio.Entities.Cards.Card", b =>
                {
                    b.HasOne("api_portafolio.Entities.Users.User", null)
                        .WithMany("Cards")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("api_portafolio.Entities.Skills.SoftSkills.SoftSkill", b =>
                {
                    b.HasOne("api_portafolio.Entities.Users.User", null)
                        .WithMany("SoftSkills")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("api_portafolio.Entities.Skills.TechnicalSkills.Technology", b =>
                {
                    b.HasOne("api_portafolio.Entities.Users.User", null)
                        .WithMany("Technologies")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("api_portafolio.Entities.TechnologiesCatalog.TechnologyByProject", b =>
                {
                    b.HasOne("api_portafolio.Entities.Projects.Project", null)
                        .WithMany("TechnologiesByProject")
                        .HasForeignKey("ProjectId");

                    b.HasOne("api_portafolio.Entities.Skills.TechnicalSkills.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("api_portafolio.Entities.Projects.Project", b =>
                {
                    b.HasOne("api_portafolio.Entities.Users.User", null)
                        .WithMany("Projects")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("api_portafolio.Entities.Users.User", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Projects");

                    b.Navigation("SoftSkills");

                    b.Navigation("Technologies");
                });

            modelBuilder.Entity("api_portafolio.Entities.Projects.Project", b =>
                {
                    b.Navigation("TechnologiesByProject");
                });
#pragma warning restore 612, 618
        }
    }
}

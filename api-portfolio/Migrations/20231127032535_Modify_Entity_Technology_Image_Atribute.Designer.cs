﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_portfolio.Data.DataContext;

#nullable disable

namespace api_portfolio.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231127032535_Modify_Entity_Technology_Image_Atribute")]
    partial class Modify_Entity_Technology_Image_Atribute
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("api_portafolio.Entities.Blogs.Blog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Enlace")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("ImagenId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ImagenId");

                    b.HasIndex("UserId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("api_portafolio.Entities.Common.Image", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UploadDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("api_portafolio.Entities.Projects.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Enlace")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("ImageId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("UserId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("api_portafolio.Entities.Skills.SoftSkills.SoftSkill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("ImageId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

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

                    b.Property<long?>("ImageId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

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

                    b.Property<long?>("ImageId")
                        .HasColumnType("bigint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Profesion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("api_portafolio.Entities.Blogs.Blog", b =>
                {
                    b.HasOne("api_portafolio.Entities.Common.Image", "Imagen")
                        .WithMany()
                        .HasForeignKey("ImagenId");

                    b.HasOne("api_portafolio.Entities.Users.User", null)
                        .WithMany("Blogs")
                        .HasForeignKey("UserId");

                    b.Navigation("Imagen");
                });

            modelBuilder.Entity("api_portafolio.Entities.Projects.Project", b =>
                {
                    b.HasOne("api_portafolio.Entities.Common.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("api_portafolio.Entities.Users.User", null)
                        .WithMany("Projects")
                        .HasForeignKey("UserId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("api_portafolio.Entities.Skills.SoftSkills.SoftSkill", b =>
                {
                    b.HasOne("api_portafolio.Entities.Common.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("api_portafolio.Entities.Users.User", null)
                        .WithMany("SoftSkills")
                        .HasForeignKey("UserId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("api_portafolio.Entities.Skills.TechnicalSkills.Technology", b =>
                {
                    b.HasOne("api_portafolio.Entities.Common.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.HasOne("api_portafolio.Entities.Users.User", null)
                        .WithMany("Technologies")
                        .HasForeignKey("UserId");

                    b.Navigation("Image");
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

            modelBuilder.Entity("api_portafolio.Entities.Users.User", b =>
                {
                    b.HasOne("api_portafolio.Entities.Common.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("api_portafolio.Entities.Projects.Project", b =>
                {
                    b.Navigation("TechnologiesByProject");
                });

            modelBuilder.Entity("api_portafolio.Entities.Users.User", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("Projects");

                    b.Navigation("SoftSkills");

                    b.Navigation("Technologies");
                });
#pragma warning restore 612, 618
        }
    }
}

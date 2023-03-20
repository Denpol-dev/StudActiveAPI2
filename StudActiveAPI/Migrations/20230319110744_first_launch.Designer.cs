﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudActiveAPI.Services;

#nullable disable

namespace StudActiveAPI.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230319110744_first_launch")]
    partial class first_launch
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudActiveAPI.Models.DutyList", b =>
                {
                    b.Property<Guid>("DutyListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateDuty")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DutyListCalendarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsDone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerification")
                        .HasColumnType("bit");

                    b.HasKey("DutyListId");

                    b.ToTable("DutyLists");
                });

            modelBuilder.Entity("StudActiveAPI.Models.DutyListCalendar", b =>
                {
                    b.Property<Guid>("DutyListCalendarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorFio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DutyListCalendarId");

                    b.ToTable("DutyListCalendars");
                });

            modelBuilder.Entity("StudActiveAPI.Models.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("StudActiveAPI.Models.HigherSchool", b =>
                {
                    b.Property<Guid>("HigherSchoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HigherSchoolId");

                    b.ToTable("HigherSchools");
                });

            modelBuilder.Entity("StudActiveAPI.Models.Inventory", b =>
                {
                    b.Property<Guid>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InventoryId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("StudActiveAPI.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("StudActiveAPI.Models.PermissionsUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("PermissionsUsers");
                });

            modelBuilder.Entity("StudActiveAPI.Models.RoleStudActive", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleStudActives");
                });

            modelBuilder.Entity("StudActiveAPI.Models.Student", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobilePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StudentId");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudActiveAPI.Models.StudentCouncil", b =>
                {
                    b.Property<Guid>("StudentCouncilId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentCouncilId");

                    b.ToTable("StudentCouncils");
                });

            modelBuilder.Entity("StudActiveAPI.Models.StudentStudActive", b =>
                {
                    b.Property<Guid>("StudActiveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LeavingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReEntryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("RoleActive")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentCouncilId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VkLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudActiveId");

                    b.ToTable("StudentStudActives");
                });

            modelBuilder.Entity("StudActiveAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Permission")
                        .HasColumnType("int");

                    b.Property<Guid?>("Role")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isValid")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StudActiveAPI.Models.UserRole", b =>
                {
                    b.Property<Guid>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserRoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("StudActiveAPI.Models.Student", b =>
                {
                    b.HasOne("StudActiveAPI.Models.User", "User")
                        .WithMany("Students")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Students_Users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudActiveAPI.Models.User", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}

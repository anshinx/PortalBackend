﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortalBackend.Data;

#nullable disable

namespace PortalBackend.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220725192246_AddedSaltToDTO")]
    partial class AddedSaltToDTO
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PortalBackend.Objects.Communities", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CategoryID")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasMaxLength(100)
                        .HasColumnType("bit");

                    b.Property<int>("RoleID")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<int?>("UserDTOId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WhenAttend")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WhenLeft")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("UserDTOId");

                    b.ToTable("Communities");
                });

            modelBuilder.Entity("PortalBackend.Objects.Subscriptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SubscriptedUserId")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<int>("SubscriptionId")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<string>("SubscriptionName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SubscriptionType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("WhenToSubbed")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Subscriments");
                });

            modelBuilder.Entity("PortalBackend.Objects.UserDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PortalBackend.Objects.Communities", b =>
                {
                    b.HasOne("PortalBackend.Objects.UserDTO", null)
                        .WithMany("Communities")
                        .HasForeignKey("UserDTOId");
                });

            modelBuilder.Entity("PortalBackend.Objects.UserDTO", b =>
                {
                    b.Navigation("Communities");
                });
#pragma warning restore 612, 618
        }
    }
}
﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mosaik.idAPI.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mosaik.idAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220418015041_UpdateMosaikParentKey")]
    partial class UpdateMosaikParentKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Mosaik.idAPI.Models.MosaikChild", b =>
                {
                    b.Property<int>("MosaikChildID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MosaikChildID");

                    b.ToTable("MosaikChildren");
                });

            modelBuilder.Entity("Mosaik.idAPI.Models.MosaikChildRestrict", b =>
                {
                    b.Property<int>("MosaikChildRestrictID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ChildID")
                        .HasColumnType("integer");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Notif")
                        .HasColumnType("boolean");

                    b.HasKey("MosaikChildRestrictID");

                    b.ToTable("MosaikChildRestricts");
                });

            modelBuilder.Entity("Mosaik.idAPI.Models.MosaikDateHistory", b =>
                {
                    b.Property<int>("MosaikDateHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("userID")
                        .HasColumnType("integer");

                    b.HasKey("MosaikDateHistoryID");

                    b.ToTable("MosaikDateHistories");
                });

            modelBuilder.Entity("Mosaik.idAPI.Models.MosaikHistory", b =>
                {
                    b.Property<int>("MosaikHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AccessedTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MosaikDateHistoryID")
                        .HasColumnType("integer");

                    b.Property<int>("userID")
                        .HasColumnType("integer");

                    b.HasKey("MosaikHistoryID");

                    b.ToTable("MosaikHistories");
                });

            modelBuilder.Entity("Mosaik.idAPI.Models.MosaikParent", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("MosaikParentID")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.ToTable("MosaikParents");
                });

            modelBuilder.Entity("Mosaik.idAPI.Models.MosaikParentChild", b =>
                {
                    b.Property<int>("MosaikParentChildID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Authorized")
                        .HasColumnType("boolean");

                    b.Property<int>("childID")
                        .HasColumnType("integer");

                    b.Property<int>("parentID")
                        .HasColumnType("integer");

                    b.HasKey("MosaikParentChildID");

                    b.ToTable("MosaikParentsChildren");
                });

            modelBuilder.Entity("Mosaik.idAPI.Models.MosaikUser", b =>
                {
                    b.Property<int>("MosaikUserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AccountStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MosaikUserID");

                    b.ToTable("MosaikUsers");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using FriendBookApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FriendBookApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FriendBookApp.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<bool>("IsMain");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.Property<int?>("UsersId");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("FriendBookApp.API.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Gender");

                    b.Property<string>("Introduction");

                    b.Property<string>("KnownAs");

                    b.Property<DateTime>("LastActive");

                    b.Property<string>("LookingFor");

                    b.Property<byte[]>("Passwordhash");

                    b.Property<byte[]>("Passwordsalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FriendBookApp.API.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Value");
                });

            modelBuilder.Entity("FriendBookApp.API.Models.Photo", b =>
                {
                    b.HasOne("FriendBookApp.API.Models.Users", "Users")
                        .WithMany("Photos")
                        .HasForeignKey("UsersId");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialLensApp.Data;

#nullable disable

namespace SocialLensApp.Migrations
{
    [DbContext(typeof(SocialLensDbContext))]
    [Migration("20241111165317_spr")]
    partial class spr
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SocialLensApp.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CommentId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("ReplyAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SocialLensApp.Entities.Invite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Invites");
                });

            modelBuilder.Entity("SocialLensApp.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CommentAmount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DislikeAmount")
                        .HasColumnType("int");

                    b.Property<int>("LikeAmount")
                        .HasColumnType("int");

                    b.Property<int>("PostCreatorId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("SocialLensApp.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FollowedAmount")
                        .HasColumnType("int");

                    b.Property<int?>("FollowersAmount")
                        .HasColumnType("int");

                    b.Property<int>("FriendsAmount")
                        .HasColumnType("int");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostAmount")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SocialLensApp.Entities.Comment", b =>
                {
                    b.HasOne("SocialLensApp.Entities.Comment", null)
                        .WithMany("ReplyList")
                        .HasForeignKey("CommentId");

                    b.HasOne("SocialLensApp.Entities.Post", null)
                        .WithMany("CommentList")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SocialLensApp.Entities.Invite", b =>
                {
                    b.HasOne("SocialLensApp.Entities.User", "InvitingUser")
                        .WithMany("InvitesList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InvitingUser");
                });

            modelBuilder.Entity("SocialLensApp.Entities.Post", b =>
                {
                    b.HasOne("SocialLensApp.Entities.User", null)
                        .WithMany("PostList")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SocialLensApp.Entities.Comment", b =>
                {
                    b.Navigation("ReplyList");
                });

            modelBuilder.Entity("SocialLensApp.Entities.Post", b =>
                {
                    b.Navigation("CommentList");
                });

            modelBuilder.Entity("SocialLensApp.Entities.User", b =>
                {
                    b.Navigation("InvitesList");

                    b.Navigation("PostList");
                });
#pragma warning restore 612, 618
        }
    }
}

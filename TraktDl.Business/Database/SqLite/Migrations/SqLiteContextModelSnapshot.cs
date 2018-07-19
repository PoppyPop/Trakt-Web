﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TraktDl.Business.Database.SqLite;

namespace Docker.AutoDl.Database.SqLite.Migrations
{
    [DbContext(typeof(SqLiteContext))]
    partial class SqLiteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("TraktDl.Business.Database.SqLite.BlackListShowSqLite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Entire");

                    b.Property<int?>("Season");

                    b.Property<string>("SerieName");

                    b.Property<uint>("TraktShowId");

                    b.HasKey("Id");

                    b.HasIndex("TraktShowId", "Season")
                        .IsUnique();

                    b.ToTable("BlackListShow");
                });

            modelBuilder.Entity("TraktDl.Business.Database.SqLite.EpisodeSqLite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Blacklisted");

                    b.Property<uint>("EpisodeNumber");

                    b.Property<string>("Name");

                    b.Property<string>("PosterUrl");

                    b.Property<Guid>("SeasonID");

                    b.HasKey("Id");

                    b.HasIndex("SeasonID", "EpisodeNumber")
                        .IsUnique();

                    b.ToTable("Episode");
                });

            modelBuilder.Entity("TraktDl.Business.Database.SqLite.SeasonSqLite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Blacklisted");

                    b.Property<uint>("SeasonNumber");

                    b.Property<Guid>("ShowID");

                    b.HasKey("Id");

                    b.HasIndex("ShowID", "SeasonNumber")
                        .IsUnique();

                    b.ToTable("Season");
                });

            modelBuilder.Entity("TraktDl.Business.Database.SqLite.ShowSqLite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Blacklisted");

                    b.Property<string>("Name");

                    b.Property<string>("PosterUrl");

                    b.Property<uint>("TraktShowId");

                    b.HasKey("Id");

                    b.ToTable("Show");
                });

            modelBuilder.Entity("TraktDl.Business.Database.SqLite.EpisodeSqLite", b =>
                {
                    b.HasOne("TraktDl.Business.Database.SqLite.SeasonSqLite", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TraktDl.Business.Database.SqLite.SeasonSqLite", b =>
                {
                    b.HasOne("TraktDl.Business.Database.SqLite.ShowSqLite", "Show")
                        .WithMany()
                        .HasForeignKey("ShowID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

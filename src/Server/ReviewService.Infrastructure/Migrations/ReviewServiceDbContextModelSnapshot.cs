﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReviewService.Infrastructure.Persistance;

namespace ReviewService.Infrastructure.Migrations
{
    [DbContext(typeof(ReviewServiceDbContext))]
    partial class ReviewServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AreaReviewTemplate", b =>
                {
                    b.Property<int>("AreasId")
                        .HasColumnType("int");

                    b.Property<int>("ReviewTemplatesId")
                        .HasColumnType("int");

                    b.HasKey("AreasId", "ReviewTemplatesId");

                    b.HasIndex("ReviewTemplatesId");

                    b.ToTable("AreaReviewTemplate");
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.AreaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("AreaItems");
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.EvaluationPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EvaluationPointsTemplateId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationPointsTemplateId");

                    b.ToTable("EvaluationPoints");
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.EvaluationPointsTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EvaluationPointsTemplates");
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.ImportanceLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ImportanceLevels");
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.ReviewTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EvaluationPointsTemplateId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EvaluationPointsTemplateId");

                    b.ToTable("ReviewTemplates");
                });

            modelBuilder.Entity("AreaReviewTemplate", b =>
                {
                    b.HasOne("ReviewService.Domain.Entites.Area", null)
                        .WithMany()
                        .HasForeignKey("AreasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReviewService.Domain.Entites.ReviewTemplate", null)
                        .WithMany()
                        .HasForeignKey("ReviewTemplatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.AreaItem", b =>
                {
                    b.HasOne("ReviewService.Domain.Entites.Area", null)
                        .WithMany("AreaItems")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.EvaluationPoint", b =>
                {
                    b.HasOne("ReviewService.Domain.Entites.EvaluationPointsTemplate", null)
                        .WithMany("EvaluationPoints")
                        .HasForeignKey("EvaluationPointsTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.ReviewTemplate", b =>
                {
                    b.HasOne("ReviewService.Domain.Entites.EvaluationPointsTemplate", null)
                        .WithMany("ReviewTemplates")
                        .HasForeignKey("EvaluationPointsTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.Area", b =>
                {
                    b.Navigation("AreaItems");
                });

            modelBuilder.Entity("ReviewService.Domain.Entites.EvaluationPointsTemplate", b =>
                {
                    b.Navigation("EvaluationPoints");

                    b.Navigation("ReviewTemplates");
                });
#pragma warning restore 612, 618
        }
    }
}

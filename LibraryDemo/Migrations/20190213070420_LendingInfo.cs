﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryDemo.Migrations
{
    public partial class LendingInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BooksDetail",
                columns: table => new
                {
                    ISBN = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    Press = table.Column<string>(nullable: false),
                    FetchBookNumber = table.Column<string>(nullable: false),
                    PublishDateTime = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    SoundCassettes = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true),
                    ImageMimeType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksDetail", x => x.ISBN);
                });

            migrationBuilder.CreateTable(
                name: "Bookshelves",
                columns: table => new
                {
                    BookshelfId = table.Column<int>(nullable: false),
                    Sort = table.Column<string>(nullable: false),
                    MinFetchNumber = table.Column<string>(nullable: false),
                    MaxFetchNumber = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookshelves", x => x.BookshelfId);
                });

            migrationBuilder.CreateTable(
                name: "RecommendedBooks",
                columns: table => new
                {
                    ISBN = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: false),
                    Press = table.Column<string>(nullable: false),
                    PublishDateTime = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    SoundCassettes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedBooks", x => x.ISBN);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Degree = table.Column<int>(nullable: false),
                    MaxBooksNumber = table.Column<int>(nullable: false),
                    AppointingBookBarCode = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 14, nullable: true),
                    Fine = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BarCode = table.Column<string>(nullable: false),
                    ISBN = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    FetchBookNumber = table.Column<string>(nullable: true),
                    BookshelfId = table.Column<int>(nullable: false),
                    BorrowTime = table.Column<DateTime>(nullable: true),
                    MatureTime = table.Column<DateTime>(nullable: true),
                    AppointedLatestTime = table.Column<DateTime>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    KeeperId = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Sort = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BarCode);
                    table.ForeignKey(
                        name: "FK_Books_Bookshelves_BookshelfId",
                        column: x => x.BookshelfId,
                        principalTable: "Bookshelves",
                        principalColumn: "BookshelfId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Students_KeeperId",
                        column: x => x.KeeperId,
                        principalTable: "Students",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentOrLendings",
                columns: table => new
                {
                    BookId = table.Column<string>(nullable: false),
                    StudentId = table.Column<string>(nullable: false),
                    AppointingDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentOrLendings", x => new { x.BookId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_AppointmentOrLendings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BarCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentOrLendings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentOrLendings_StudentId",
                table: "AppointmentOrLendings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookshelfId",
                table: "Books",
                column: "BookshelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_KeeperId",
                table: "Books",
                column: "KeeperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentOrLendings");

            migrationBuilder.DropTable(
                name: "BooksDetail");

            migrationBuilder.DropTable(
                name: "RecommendedBooks");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Bookshelves");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}

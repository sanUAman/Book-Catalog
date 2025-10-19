using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesBook.Migrations
{
    /// <inheritdoc />
    public partial class Feedback_AdminReply_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AdminRepliedAt",
                table: "Feedbacks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminRepliedBy",
                table: "Feedbacks",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminReplyBody",
                table: "Feedbacks",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminReplySubject",
                table: "Feedbacks",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminRepliedAt",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "AdminRepliedBy",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "AdminReplyBody",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "AdminReplySubject",
                table: "Feedbacks");
        }
    }
}

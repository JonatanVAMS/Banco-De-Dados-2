using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerInfoToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerCPF",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCPF",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Tickets");
        }
    }
}

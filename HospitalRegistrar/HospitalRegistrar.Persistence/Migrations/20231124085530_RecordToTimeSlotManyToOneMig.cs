using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalRegistrar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RecordToTimeSlotManyToOneMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Records_TimeSlotId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "IsVacant",
                table: "TimeSlots");

            migrationBuilder.CreateIndex(
                name: "IX_Records_TimeSlotId",
                table: "Records",
                column: "TimeSlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Records_TimeSlotId",
                table: "Records");

            migrationBuilder.AddColumn<bool>(
                name: "IsVacant",
                table: "TimeSlots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Records_TimeSlotId",
                table: "Records",
                column: "TimeSlotId",
                unique: true);
        }
    }
}

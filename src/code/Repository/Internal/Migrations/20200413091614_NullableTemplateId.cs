using Microsoft.EntityFrameworkCore.Migrations;

namespace RedSpartan.IntervalTraining.Repository.Internal.Migrations
{
    public partial class NullableTemplateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Intervals_TemplateId",
                table: "Histories");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "Histories",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Intervals_TemplateId",
                table: "Histories",
                column: "TemplateId",
                principalTable: "Intervals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Intervals_TemplateId",
                table: "Histories");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "Histories",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Intervals_TemplateId",
                table: "Histories",
                column: "TemplateId",
                principalTable: "Intervals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

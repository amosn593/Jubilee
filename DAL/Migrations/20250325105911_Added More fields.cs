using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedMorefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "Password",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "longtext")
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .OldAnnotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "AccountNo",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "AdditionalDetails",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "AgeBracket",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "BankBranch",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "BankDocument",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "BankName",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "Bob",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "IdAttachmentBack",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "IdAttachmentFront",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "InvestmentPeriod",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "InvestmentProducts",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AddColumn<string>(
            //    name: "KRAPin",
            //    table: "Users",
            //    type: "longtext",
            //    nullable: false)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PassportPhoto",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhysicalAddress",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PinAttachment",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PreviousInvestments",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Residency",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SourceOfFunds",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SwiftCode",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "TermsAndCondition",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UnderstandingInvestments",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Relationship = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Allocation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_UserId",
                table: "Beneficiaries",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AdditionalDetails",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AgeBracket",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BankBranch",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BankDocument",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Bob",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdAttachmentBack",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdAttachmentFront",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InvestmentPeriod",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InvestmentProducts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "KRAPin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PassportPhoto",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhysicalAddress",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PinAttachment",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PreviousInvestments",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Residency",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SourceOfFunds",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SwiftCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TermsAndCondition",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UnderstandingInvestments",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Password",
                keyValue: null,
                column: "Password",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}

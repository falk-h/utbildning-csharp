using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageApi.Migrations
{
    /// <inheritdoc />
    public partial class CaseInsensitiveCollation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                collation: "SQL_Latin1_General_CP1_CI_AS",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Product",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                collation: "SQL_Latin1_General_CP1_CI_AS",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Product",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");
        }
    }
}

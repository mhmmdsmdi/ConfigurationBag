using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfigurationBag.Infrastructure.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Base");

            migrationBuilder.CreateTable(
                name: "Collections",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configurations",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configurations_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalSchema: "Base",
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureFlags",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureFlags_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalSchema: "Base",
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigurationId = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalSchema: "Base",
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureFlagLabels",
                schema: "Base",
                columns: table => new
                {
                    FeatureFlagsId = table.Column<long>(type: "bigint", nullable: false),
                    LabelsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureFlagLabels", x => new { x.FeatureFlagsId, x.LabelsId });
                    table.ForeignKey(
                        name: "FK_FeatureFlagLabels_FeatureFlags_FeatureFlagsId",
                        column: x => x.FeatureFlagsId,
                        principalSchema: "Base",
                        principalTable: "FeatureFlags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureFlagLabels_Labels_LabelsId",
                        column: x => x.LabelsId,
                        principalSchema: "Base",
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                schema: "Base",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Values_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "Base",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValueLabels",
                schema: "Base",
                columns: table => new
                {
                    LabelsId = table.Column<long>(type: "bigint", nullable: false),
                    ValuesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueLabels", x => new { x.LabelsId, x.ValuesId });
                    table.ForeignKey(
                        name: "FK_ValueLabels_Labels_LabelsId",
                        column: x => x.LabelsId,
                        principalSchema: "Base",
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValueLabels_Values_ValuesId",
                        column: x => x.ValuesId,
                        principalSchema: "Base",
                        principalTable: "Values",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configurations_CollectionId",
                schema: "Base",
                table: "Configurations",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFlagLabels_LabelsId",
                schema: "Base",
                table: "FeatureFlagLabels",
                column: "LabelsId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureFlags_CollectionId",
                schema: "Base",
                table: "FeatureFlags",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ConfigurationId",
                schema: "Base",
                table: "Properties",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueLabels_ValuesId",
                schema: "Base",
                table: "ValueLabels",
                column: "ValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_PropertyId",
                schema: "Base",
                table: "Values",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureFlagLabels",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "ValueLabels",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "FeatureFlags",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "Labels",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "Values",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "Configurations",
                schema: "Base");

            migrationBuilder.DropTable(
                name: "Collections",
                schema: "Base");
        }
    }
}

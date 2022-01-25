using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashTalker.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecycleBins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    latit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    longit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecycleBins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    RecBinid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    typeOfWaste = table.Column<int>(type: "int", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
                    width = table.Column<float>(type: "real", nullable: false),
                    depth = table.Column<float>(type: "real", nullable: false),
                    avgGrowthOccupiedVolumePerDay = table.Column<double>(type: "float", nullable: false),
                    dateFull = table.Column<DateTime>(type: "datetime2", nullable: true),
                    currentPercOccupied = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Containers_RecycleBins_RecBinid",
                        column: x => x.RecBinid,
                        principalTable: "RecycleBins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    issue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    alertStatus = table.Column<int>(type: "int", nullable: false),
                    alertType = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employeeid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Alerts_Users_employeeid",
                        column: x => x.employeeid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    dateCriation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateBegin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    estimatedDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    distanceEstimatedKm = table.Column<int>(type: "int", nullable: false),
                    distanceTravelledKm = table.Column<float>(type: "real", nullable: true),
                    employeeid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    typeCreation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Routes_Users_employeeid",
                        column: x => x.employeeid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    distance = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    containerid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.id);
                    table.ForeignKey(
                        name: "FK_Measurements_Containers_containerid",
                        column: x => x.containerid,
                        principalTable: "Containers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pickings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    volumeRecolhido = table.Column<float>(type: "real", nullable: false),
                    containerid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    avgGrowthOccupiedPerDay = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pickings", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pickings_Containers_containerid",
                        column: x => x.containerid,
                        principalTable: "Containers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollectPoint",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    order = table.Column<int>(type: "int", nullable: false),
                    recycleBinid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    routeid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectPoint", x => x.id);
                    table.ForeignKey(
                        name: "FK_CollectPoint_RecycleBins_recycleBinid",
                        column: x => x.recycleBinid,
                        principalTable: "RecycleBins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollectPoint_Routes_routeid",
                        column: x => x.routeid,
                        principalTable: "Routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_employeeid",
                table: "Alerts",
                column: "employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_CollectPoint_recycleBinid",
                table: "CollectPoint",
                column: "recycleBinid");

            migrationBuilder.CreateIndex(
                name: "IX_CollectPoint_routeid",
                table: "CollectPoint",
                column: "routeid");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_RecBinid",
                table: "Containers",
                column: "RecBinid");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_containerid",
                table: "Measurements",
                column: "containerid");

            migrationBuilder.CreateIndex(
                name: "IX_Pickings_containerid",
                table: "Pickings",
                column: "containerid");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_employeeid",
                table: "Routes",
                column: "employeeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "CollectPoint");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Pickings");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RecycleBins");
        }
    }
}

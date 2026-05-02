using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chambres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumChambre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etage = table.Column<int>(type: "int", nullable: false),
                    CapaciteAccueil = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Disponible")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chambres", x => x.Id);
                    table.CheckConstraint("Ck_Chambre_CapaciteAccueil", "CapaciteAccueil >= 1 AND CapaciteAccueil <= 5");
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumPieceIdentite = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroTelephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EstActif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarifs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeChambre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Saison = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrixParNuit = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Courriel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstActif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChambreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateArrivee = table.Column<DateOnly>(type: "date", nullable: false),
                    DateDepart = table.Column<DateOnly>(type: "date", nullable: false),
                    NombrePersonnes = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeureArriveeEffective = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemiseAppliquee = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    PenaliteAnnulation = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.CheckConstraint("CK_Reservations_NombrePersonnes", "[NombrePersonnes] >= 1");
                    table.ForeignKey(
                        name: "FK_Reservations_Chambres_ChambreId",
                        column: x => x.ChambreId,
                        principalTable: "Chambres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChambreEquipement",
                columns: table => new
                {
                    ChambresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipementsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChambreEquipement", x => new { x.ChambresId, x.EquipementsId });
                    table.ForeignKey(
                        name: "FK_ChambreEquipement_Chambres_ChambresId",
                        column: x => x.ChambresId,
                        principalTable: "Chambres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChambreEquipement_Equipements_EquipementsId",
                        column: x => x.EquipementsId,
                        principalTable: "Equipements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MontantTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DateEmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontantRemise = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    MontantNuitee = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MontantServices = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "EnAttente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factures_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LignesFacture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PrixUnitaire = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LignesFacture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LignesFacture_Factures_FactureId",
                        column: x => x.FactureId,
                        principalTable: "Factures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChambreEquipement_EquipementsId",
                table: "ChambreEquipement",
                column: "EquipementsId");

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_NumChambre",
                table: "Chambres",
                column: "NumChambre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_NumPieceIdentite",
                table: "Clients",
                column: "NumPieceIdentite",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipements_Nom",
                table: "Equipements",
                column: "Nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factures_ReservationId",
                table: "Factures",
                column: "ReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LignesFacture_FactureId",
                table: "LignesFacture",
                column: "FactureId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ChambreId",
                table: "Reservations",
                column: "ChambreId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientId",
                table: "Reservations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifs_TypeChambre_Saison",
                table: "Tarifs",
                columns: new[] { "TypeChambre", "Saison" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Courriel",
                table: "Users",
                column: "Courriel",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChambreEquipement");

            migrationBuilder.DropTable(
                name: "LignesFacture");

            migrationBuilder.DropTable(
                name: "Tarifs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Equipements");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Chambres");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}

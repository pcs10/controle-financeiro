using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Migrations
{
    public partial class RetiradaIdBanco_Saldo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdBanco",
                table: "Saldos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataConsulta",
                table: "Saldos",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataConsulta",
                table: "Saldos");

            migrationBuilder.AddColumn<int>(
                name: "IdBanco",
                table: "Saldos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}

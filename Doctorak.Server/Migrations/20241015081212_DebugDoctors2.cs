﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctorak.Server.Migrations
{
    /// <inheritdoc />
    public partial class DebugDoctors2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qualification",
                table: "Doctors",
                newName: "Qualifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qualifications",
                table: "Doctors",
                newName: "Qualification");
        }
    }
}

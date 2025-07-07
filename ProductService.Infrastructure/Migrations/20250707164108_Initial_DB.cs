using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: true),
                    Availability = table.Column<bool>(type: "boolean", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "CreationDate", "Description", "Name", "Price", "UserId" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b1-111c-bc78-2d54a9991870"), true, new DateTime(2025, 7, 7, 16, 0, 0, 0, DateTimeKind.Utc), "LIGHTFORCE hybrid optical-mechanical primary switches, HERO 25K gaming sensor, compatible with PC - macOS/Windows - Black", "Logitech G502 X Wired Gaming Mouse", 149.90000000000001, new Guid("9c5af705-adc5-4e0a-8433-e5da0562a4a3") },
                    { new Guid("c9d4c053-49b2-430c-bc38-2d54a9991870"), false, new DateTime(2025, 7, 7, 16, 0, 0, 0, DateTimeKind.Utc), "Among Shakespeare's plays, \"Hamlet\" is considered by many his masterpiece. Among actors, the role of Hamlet, Prince of Denmark, is considered the jewel in the crown of a triumphant theatrical career. Now Kenneth Branagh plays the leading role and co-directs a brillant ensemble performance. Three generations of legendary leading actors, many of whom first assembled for the Oscar-winning film \"Henry V\", gather here to perform the rarely heard complete version of the play. This clear, subtly nuanced, stunning dramatization, presented by The Renaissance Theatre Company in association with \"Bbc\" Broadcasting, features such luminaries as Sir John Gielgud, Derek Jacobi, Emma Thompson and Christopher Ravenscroft. It combines a full cast with stirring music and sound effects to bring this magnificent Shakespearen classic vividly to life. Revealing new riches with each listening, this production of \"Hamlet\" is an invaluable aid for students, teachers and all true lovers of Shakespeare - a recording to be treasured for decades to come.", "Book 'Hamlet' by William Shakespeare", 16.899999999999999, new Guid("9c5af705-adc5-4e0a-8433-e5da0562a4a3") },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true, new DateTime(2025, 7, 7, 16, 0, 0, 0, DateTimeKind.Utc), "A kind of antipode of the second great dystopia of the 20th century - \"Brave New World\" by Aldous Huxley. What is, in essence, more terrible: \"consumer society\" taken to the point of absurdity - or \"idea society\" taken to the absolute? According to Orwell, there is and cannot be anything more terrible than total lack of freedom...", "Book '1984' by George Orwell", 14.9, new Guid("9c5af705-adc5-4e0a-8433-e5da0562a4a3") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

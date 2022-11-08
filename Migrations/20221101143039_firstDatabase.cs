using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreManage.Migrations
{
    public partial class firstDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorID);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    FieldID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FieldDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.FieldID);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FieldAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StripeID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BookName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfPublished = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FieldID = table.Column<int>(type: "int", nullable: false),
                    PublisherID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Fields_FieldID",
                        column: x => x.FieldID,
                        principalTable: "Fields",
                        principalColumn: "FieldID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "PublisherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Owner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(1024)", maxLength: 1024, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AccountAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    OrderStatus = table.Column<bool>(type: "bit", maxLength: 20, nullable: false),
                    DateOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[] { 2, "Customer" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[] { 3, "Staff" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountID", "AccountAddress", "AccountEmail", "Country", "Image", "Owner", "PasswordHash", "PasswordSalt", "Phone", "RefreshToken", "RoleID", "Status", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { 1, null, "dHRoYW5odHVuZzkyQGdtYWlsLmNvbQ==", null, null, null, new byte[] { 95, 236, 213, 190, 179, 205, 189, 66, 186, 120, 57, 234, 63, 229, 75, 163, 156, 142, 81, 227, 14, 71, 241, 176, 175, 152, 37, 171, 216, 22, 57, 140, 19, 0, 181, 221, 252, 36, 180, 101, 23, 160, 220, 221, 191, 79, 140, 85, 96, 244, 77, 37, 33, 99, 156, 66, 136, 253, 254, 38, 151, 184, 114, 82 }, new byte[] { 122, 50, 191, 80, 201, 18, 108, 124, 138, 50, 209, 60, 247, 1, 84, 51, 255, 30, 86, 23, 72, 231, 202, 162, 210, 209, 146, 189, 181, 10, 133, 155, 4, 135, 101, 193, 79, 100, 200, 44, 48, 85, 109, 104, 206, 60, 33, 21, 60, 178, 169, 228, 82, 219, 234, 35, 32, 207, 185, 150, 3, 27, 186, 62, 102, 23, 84, 19, 14, 241, 66, 218, 168, 34, 4, 4, 32, 101, 190, 221, 122, 149, 18, 184, 157, 5, 192, 133, 125, 55, 117, 133, 86, 255, 244, 151, 76, 106, 42, 204, 64, 201, 186, 130, 244, 205, 40, 70, 15, 95, 106, 78, 230, 57, 81, 197, 165, 102, 84, 120, 114, 115, 51, 147, 73, 254, 118, 19 }, null, null, 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, "dHVuZ3R0c2UxNDA5NjNAZnB0LmVkdS52bg==", null, null, null, new byte[] { 120, 133, 178, 245, 49, 115, 238, 253, 153, 195, 176, 148, 185, 194, 201, 255, 63, 206, 207, 131, 130, 90, 73, 17, 168, 245, 156, 28, 126, 139, 100, 158, 23, 70, 54, 151, 143, 51, 220, 193, 144, 21, 174, 61, 152, 123, 200, 251, 109, 176, 147, 195, 50, 46, 191, 239, 94, 13, 216, 145, 117, 133, 45, 108 }, new byte[] { 182, 109, 237, 58, 245, 211, 222, 208, 226, 59, 142, 68, 123, 109, 127, 39, 55, 133, 156, 109, 217, 40, 249, 64, 197, 200, 89, 104, 240, 255, 39, 127, 137, 136, 116, 181, 69, 207, 178, 143, 11, 220, 180, 111, 197, 5, 54, 25, 199, 213, 147, 219, 123, 194, 160, 177, 247, 14, 55, 219, 188, 158, 147, 126, 63, 84, 23, 175, 57, 174, 7, 24, 166, 126, 40, 160, 197, 66, 185, 249, 236, 192, 130, 151, 82, 181, 108, 187, 208, 218, 41, 103, 87, 28, 102, 206, 99, 217, 176, 54, 173, 125, 33, 1, 33, 252, 211, 33, 188, 80, 245, 64, 218, 247, 30, 65, 196, 146, 255, 19, 38, 71, 24, 115, 254, 152, 11, 116 }, null, null, 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, "aG9hbmduaHNlMTQwMTg0QGZwdC5lZHUudm4=", null, null, null, new byte[] { 77, 229, 43, 44, 174, 231, 162, 89, 122, 171, 12, 77, 79, 212, 120, 141, 138, 26, 72, 222, 126, 179, 34, 13, 74, 158, 180, 247, 245, 208, 183, 83, 239, 225, 232, 83, 71, 114, 253, 65, 170, 90, 89, 194, 43, 221, 9, 174, 56, 110, 177, 85, 216, 221, 226, 247, 243, 167, 252, 29, 77, 38, 132, 61 }, new byte[] { 32, 1, 8, 80, 235, 42, 62, 59, 152, 129, 221, 181, 58, 128, 142, 163, 108, 80, 6, 124, 150, 178, 76, 89, 38, 85, 147, 135, 101, 7, 29, 127, 17, 42, 95, 91, 82, 162, 173, 155, 177, 135, 48, 41, 151, 176, 124, 231, 21, 127, 242, 89, 21, 42, 199, 22, 25, 90, 187, 179, 4, 63, 221, 43, 158, 180, 209, 245, 250, 88, 233, 234, 206, 187, 49, 14, 226, 62, 41, 181, 81, 2, 244, 37, 235, 233, 202, 134, 186, 80, 167, 22, 214, 251, 57, 219, 144, 12, 147, 181, 229, 127, 136, 188, 226, 65, 155, 53, 194, 119, 72, 63, 101, 127, 66, 214, 190, 114, 83, 158, 68, 1, 62, 237, 146, 233, 23, 211 }, null, null, 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, "YWRtaW4=", null, null, null, new byte[] { 222, 42, 93, 17, 78, 7, 248, 88, 100, 191, 229, 190, 164, 30, 224, 134, 39, 37, 25, 41, 140, 232, 202, 56, 84, 102, 191, 27, 78, 106, 245, 10, 217, 52, 19, 208, 173, 95, 74, 148, 215, 238, 88, 28, 166, 35, 50, 251, 251, 11, 124, 26, 116, 107, 222, 0, 157, 128, 30, 128, 185, 28, 125, 37 }, new byte[] { 116, 20, 237, 236, 99, 223, 89, 138, 209, 185, 211, 199, 83, 194, 52, 203, 202, 159, 12, 85, 114, 224, 16, 211, 23, 86, 26, 242, 124, 1, 99, 227, 167, 157, 70, 49, 249, 197, 126, 66, 92, 87, 77, 82, 246, 154, 35, 105, 61, 185, 212, 198, 167, 69, 132, 151, 176, 165, 201, 214, 105, 24, 225, 220, 195, 192, 251, 168, 126, 235, 13, 172, 207, 213, 27, 33, 45, 195, 83, 194, 0, 0, 146, 194, 3, 168, 233, 176, 13, 145, 195, 116, 129, 123, 64, 125, 239, 58, 10, 254, 15, 242, 86, 143, 161, 113, 24, 110, 61, 46, 221, 31, 117, 201, 7, 147, 163, 203, 100, 57, 105, 239, 47, 216, 21, 239, 233, 85 }, null, null, 1, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, null, "c3RhZmY=", null, null, null, new byte[] { 10, 109, 232, 42, 57, 82, 174, 39, 143, 130, 135, 94, 25, 167, 100, 154, 194, 134, 60, 77, 114, 181, 82, 175, 65, 135, 145, 33, 96, 116, 182, 95, 100, 243, 232, 162, 243, 108, 63, 137, 222, 244, 19, 223, 185, 88, 241, 5, 31, 243, 127, 62, 178, 233, 228, 10, 63, 61, 145, 122, 181, 233, 185, 217 }, new byte[] { 153, 75, 221, 167, 97, 90, 164, 127, 27, 241, 142, 189, 161, 214, 168, 33, 135, 73, 70, 109, 128, 82, 92, 173, 74, 47, 8, 255, 93, 200, 73, 13, 35, 7, 148, 77, 24, 227, 63, 79, 234, 75, 209, 233, 13, 107, 107, 101, 160, 86, 217, 94, 91, 145, 8, 194, 37, 62, 245, 162, 14, 195, 200, 110, 250, 74, 6, 218, 6, 235, 42, 169, 137, 196, 40, 163, 85, 113, 84, 99, 23, 213, 121, 246, 84, 208, 30, 234, 30, 58, 204, 0, 121, 188, 224, 58, 64, 25, 107, 235, 28, 127, 208, 64, 54, 130, 125, 157, 212, 68, 137, 2, 64, 255, 60, 69, 171, 22, 87, 226, 4, 58, 186, 201, 208, 94, 53, 118 }, null, null, 3, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleID",
                table: "Accounts",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorID",
                table: "Books",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookName",
                table: "Books",
                column: "BookName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_FieldID",
                table: "Books",
                column: "FieldID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherID",
                table: "Books",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_BookID",
                table: "OrderDetails",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountID",
                table: "Orders",
                column: "AccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

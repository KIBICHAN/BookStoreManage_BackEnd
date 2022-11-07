using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreManage.Migrations
{
    public partial class sql : Migration
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
                    { 1, null, "dHRoYW5odHVuZzkyQGdtYWlsLmNvbQ==", null, null, null, new byte[] { 213, 139, 198, 59, 226, 159, 19, 177, 197, 78, 32, 19, 132, 126, 141, 167, 115, 153, 233, 242, 73, 12, 156, 155, 177, 236, 217, 70, 96, 81, 47, 0, 67, 252, 181, 100, 197, 146, 191, 210, 208, 120, 224, 81, 58, 229, 117, 106, 33, 173, 46, 235, 6, 82, 6, 55, 64, 223, 182, 92, 115, 166, 204, 241 }, new byte[] { 229, 72, 201, 147, 255, 26, 133, 5, 245, 162, 31, 66, 129, 117, 94, 249, 15, 86, 123, 99, 111, 75, 168, 36, 28, 62, 142, 56, 27, 49, 183, 220, 63, 82, 135, 69, 242, 78, 144, 219, 72, 86, 6, 215, 3, 64, 161, 117, 178, 86, 175, 178, 85, 248, 237, 179, 247, 86, 42, 168, 221, 32, 204, 62, 218, 212, 186, 179, 136, 118, 185, 139, 121, 76, 41, 73, 214, 168, 195, 239, 119, 87, 37, 39, 120, 209, 130, 69, 252, 223, 148, 230, 212, 107, 147, 224, 27, 132, 53, 158, 51, 74, 130, 86, 51, 228, 143, 241, 48, 29, 137, 94, 62, 158, 177, 146, 253, 35, 192, 142, 39, 45, 186, 214, 97, 133, 57, 119 }, null, null, 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, "dHVuZ3R0c2UxNDA5NjNAZnB0LmVkdS52bg==", null, null, null, new byte[] { 132, 131, 255, 98, 197, 185, 248, 148, 189, 21, 81, 217, 181, 230, 246, 242, 180, 114, 16, 70, 163, 175, 179, 253, 170, 245, 28, 65, 112, 29, 250, 92, 183, 165, 157, 33, 75, 189, 40, 137, 151, 228, 242, 125, 25, 26, 224, 188, 85, 142, 56, 160, 65, 39, 61, 193, 21, 170, 224, 243, 20, 173, 115, 89 }, new byte[] { 195, 245, 116, 11, 132, 165, 138, 171, 225, 165, 32, 143, 55, 239, 239, 251, 60, 254, 137, 121, 165, 88, 97, 185, 172, 94, 31, 125, 93, 217, 199, 162, 144, 82, 184, 27, 239, 219, 118, 115, 192, 83, 146, 72, 59, 68, 44, 18, 167, 16, 225, 64, 14, 31, 200, 225, 23, 3, 82, 203, 31, 141, 7, 58, 118, 139, 187, 20, 64, 38, 179, 88, 119, 139, 167, 131, 58, 112, 237, 244, 48, 110, 198, 115, 108, 214, 144, 189, 155, 250, 94, 177, 108, 33, 135, 237, 112, 60, 213, 123, 52, 165, 61, 155, 115, 201, 7, 24, 125, 225, 165, 221, 213, 130, 227, 63, 237, 132, 88, 232, 115, 54, 172, 61, 170, 30, 7, 192 }, null, null, 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, "aG9hbmduaHNlMTQwMTg0QGZwdC5lZHUudm4=", null, null, null, new byte[] { 232, 109, 164, 35, 24, 9, 56, 153, 175, 141, 81, 75, 153, 159, 69, 184, 244, 60, 241, 59, 134, 109, 217, 109, 13, 255, 19, 76, 66, 222, 197, 159, 24, 233, 166, 159, 243, 137, 223, 157, 223, 166, 164, 12, 173, 137, 102, 194, 78, 133, 150, 159, 143, 100, 138, 23, 104, 129, 177, 112, 241, 18, 14, 121 }, new byte[] { 248, 233, 203, 116, 223, 233, 23, 97, 32, 51, 225, 98, 113, 241, 140, 75, 152, 130, 39, 196, 153, 249, 218, 68, 164, 137, 206, 37, 208, 255, 134, 214, 195, 235, 151, 191, 245, 108, 215, 180, 66, 150, 236, 173, 31, 91, 50, 254, 242, 83, 102, 50, 168, 106, 222, 87, 64, 15, 243, 139, 194, 234, 129, 162, 157, 174, 52, 13, 1, 17, 232, 196, 70, 186, 247, 171, 191, 249, 30, 224, 107, 39, 12, 64, 145, 4, 191, 60, 226, 42, 236, 89, 131, 177, 118, 168, 231, 150, 190, 179, 147, 253, 107, 137, 107, 254, 73, 119, 34, 4, 41, 251, 111, 101, 84, 0, 203, 173, 83, 49, 169, 134, 133, 196, 139, 173, 42, 57 }, null, null, 2, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, "YWRtaW4=", null, null, null, new byte[] { 81, 200, 250, 2, 124, 64, 228, 58, 103, 223, 254, 40, 212, 210, 36, 238, 15, 146, 217, 163, 90, 189, 157, 73, 207, 212, 237, 79, 131, 161, 241, 65, 43, 72, 38, 215, 26, 58, 239, 105, 154, 238, 218, 99, 187, 1, 158, 186, 125, 252, 166, 93, 90, 194, 54, 191, 171, 10, 201, 134, 210, 194, 227, 111 }, new byte[] { 34, 215, 198, 85, 200, 238, 203, 56, 116, 177, 24, 33, 141, 51, 197, 155, 139, 131, 242, 114, 119, 142, 255, 79, 51, 86, 254, 21, 61, 220, 152, 128, 248, 163, 85, 246, 44, 44, 41, 18, 255, 152, 44, 190, 14, 28, 129, 130, 181, 247, 192, 235, 126, 96, 242, 24, 60, 115, 39, 126, 5, 1, 69, 2, 58, 36, 223, 200, 33, 93, 213, 15, 233, 223, 18, 112, 53, 177, 144, 68, 164, 113, 149, 23, 173, 222, 209, 46, 83, 203, 87, 160, 83, 78, 162, 204, 14, 85, 102, 40, 208, 86, 51, 72, 57, 128, 53, 70, 183, 153, 164, 42, 149, 74, 93, 11, 231, 253, 221, 47, 178, 233, 25, 21, 199, 36, 157, 204 }, null, null, 1, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, null, "c3RhZmY=", null, null, null, new byte[] { 172, 122, 194, 179, 38, 45, 159, 137, 91, 79, 100, 119, 150, 202, 113, 246, 97, 160, 228, 27, 225, 202, 74, 194, 143, 166, 16, 220, 108, 82, 246, 244, 64, 109, 243, 55, 220, 7, 175, 23, 10, 170, 183, 86, 172, 196, 70, 193, 147, 39, 70, 69, 215, 188, 44, 182, 83, 117, 210, 114, 87, 225, 61, 224 }, new byte[] { 199, 251, 83, 224, 140, 238, 175, 133, 248, 42, 98, 112, 164, 232, 159, 241, 105, 63, 152, 92, 200, 188, 195, 45, 73, 246, 72, 84, 198, 243, 221, 95, 142, 232, 195, 186, 214, 7, 135, 134, 129, 248, 4, 203, 115, 232, 83, 116, 37, 129, 18, 153, 84, 157, 151, 237, 57, 192, 21, 227, 91, 234, 142, 102, 61, 69, 74, 77, 3, 26, 136, 219, 239, 115, 96, 125, 103, 246, 1, 84, 156, 189, 190, 205, 158, 65, 8, 131, 21, 156, 100, 117, 158, 17, 233, 148, 220, 36, 210, 206, 176, 9, 191, 230, 120, 51, 161, 28, 205, 36, 93, 124, 251, 31, 126, 249, 170, 22, 182, 248, 205, 20, 220, 84, 4, 60, 229, 196 }, null, null, 3, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "Address", "ConcurrencyStamp", "FirstName", "LastName", "SecurityStamp" },
                values: new object[] { "X", "2cb6d045-7d23-4856-88cc-20bc011cac4f", "Mona", "Al-Taher", "584268d7-0498-48aa-a11c-ca18737d2a26" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "Address", "ConcurrencyStamp", "FirstName", "LastName", "SecurityStamp" },
                values: new object[] { "Y", "127a999f-4913-4b5a-b2fa-c909dcec9be6", "Ali", "Saeed", "b5da3a7d-1da1-4822-a8e5-3f8e89acccea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "Address", "ConcurrencyStamp", "FirstName", "LastName", "SecurityStamp" },
                values: new object[] { "Z", "b5e837f7-73f9-415b-b7d3-35fa7debd0f9", "Sara", "Hassan", "25679bb5-055c-4a94-a3bc-4b9c3780b018" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Household furniture and fittings", "Furniture" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Children's toys and games", "Toys" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Equipment for various sports", "Sports Equipment" });

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumns: new[] { "Id", "Order_Id", "Product_Id" },
                keyValues: new object[] { 1, 1, 1 },
                column: "Price",
                value: 499.99m);

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumns: new[] { "Id", "Order_Id", "Product_Id" },
                keyValues: new object[] { 2, 1, 4 },
                columns: new[] { "Price", "Quantity" },
                values: new object[] { 19.99m, 1m });

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumns: new[] { "Id", "Order_Id", "Product_Id" },
                keyValues: new object[] { 3, 2, 3 },
                columns: new[] { "Price", "Quantity" },
                values: new object[] { 14.99m, 2m });

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumns: new[] { "Id", "Order_Id", "Product_Id" },
                keyValues: new object[] { 4, 3, 2 },
                column: "Price",
                value: 299.99m);

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumns: new[] { "Id", "Order_Id", "Product_Id" },
                keyValues: new object[] { 5, 3, 5 },
                column: "Price",
                value: 24.99m);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "OrderStatus" },
                values: new object[] { new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "OrderStatus" },
                values: new object[] { new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Delivered" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OrderDate", "OrderStatus" },
                values: new object[] { new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cancelled" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Image", "Name", "Price" },
                values: new object[] { "sofa.jpg", "Sofa", 499.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Image", "Name", "Price" },
                values: new object[] { "dining_table.jpg", "Dining Table", 299.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Image", "Name", "Price" },
                values: new object[] { "action_figure.jpg", "Action Figure", 14.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Image", "Name", "Price" },
                values: new object[] { "football.jpg", "Football", 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Image", "Name", "Price" },
                values: new object[] { "basketball.jpg", "Basketball", 24.99m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "Address", "ConcurrencyStamp", "FirstName", "LastName", "SecurityStamp" },
                values: new object[] { "A", "380f6fe2-8aa7-42cb-b6a6-734e71692495", "Ahmed", "Haggag", "9014f8d2-fbb9-4a89-beb7-cffefa4cd09b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "Address", "ConcurrencyStamp", "FirstName", "LastName", "SecurityStamp" },
                values: new object[] { "B", "3c2b52a6-8037-43da-ac43-1ba9cba8b128", "Tarek", "Shraim", "2852bfa7-ee51-4fe3-985d-227b5973f578" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "Address", "ConcurrencyStamp", "FirstName", "LastName", "SecurityStamp" },
                values: new object[] { "c", "51bf535c-29a6-442e-971f-dae617af86a3", "Sami", "Ahmad", "9129205d-4bea-45b8-b756-7332b94a3cc9" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Devices and gadgets", "Electronics" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Books and literature", "Books" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Apparel and accessories", "Clothing" });

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumns: new[] { "Id", "Order_Id", "Product_Id" },
                keyValues: new object[] { 1, 1, 1 },
                column: "Price",
                value: 299.99m);

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumns: new[] { "Id", "Order_Id", "Product_Id" },
                keyValues: new object[] { 2, 1, 4 },
                columns: new[] { "Price", "Quantity" },
                values: new object[] { 9.99m, 2m });

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumns: new[] { "Id", "Order_Id", "Product_Id" },
                keyValues: new object[] { 3, 2, 3 },
                columns: new[] { "Price", "Quantity" },
                values: new object[] { 19.99m, 1m });

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumns: new[] { "Id", "Order_Id", "Product_Id" },
                keyValues: new object[] { 4, 3, 2 },
                column: "Price",
                value: 799.99m);

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumns: new[] { "Id", "Order_Id", "Product_Id" },
                keyValues: new object[] { 5, 3, 5 },
                column: "Price",
                value: 9.99m);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "OrderStatus" },
                values: new object[] { new DateTime(2023, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "OrderStatus" },
                values: new object[] { new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OrderDate", "OrderStatus" },
                values: new object[] { new DateTime(2023, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shipped" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Image", "Name", "Price" },
                values: new object[] { "smartphone.jpg", "Smartphone", 299.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Image", "Name", "Price" },
                values: new object[] { "laptop.jpg", "Laptop", 799.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Image", "Name", "Price" },
                values: new object[] { "novel.jpg", "Novel", 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Image", "Name", "Price" },
                values: new object[] { "tshirt.jpg", "T-Shirt", 9.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Image", "Name", "Price" },
                values: new object[] { "jeans.jpg", "Jeans", 49.99m });
        }
    }
}

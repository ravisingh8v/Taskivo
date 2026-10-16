using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskivoInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RBACDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { new Guid("28ca585b-008c-4c6d-9de5-3cdea4c0ffb3"), "Permission to create tasks.", "Tasks.Create" },
                    { new Guid("304ac39e-a347-4acb-a8c6-d0a2639c4a06"), "Permission to manage roles and permissions.", "Roles.Manage" },
                    { new Guid("4990d90f-c549-4653-a0b8-9ea74ba75154"), "Permission to read tasks.", "Tasks.Read" },
                    { new Guid("b436114f-7eb9-44b9-90f6-916de2b2da52"), "Permission to manage users.", "Users.Manage" },
                    { new Guid("bb933aaa-3d53-4777-99ec-6e51bda093df"), "Permission to update tasks.", "Tasks.Update" },
                    { new Guid("cf54f7b4-c5d9-426a-a163-de7c8637ce24"), "Permission to delete tasks.", "Tasks.Delete" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b"), "Standard user role with limited access to resources.", "User" },
                    { new Guid("bb10e887-c4eb-4473-85f8-556838100856"), "Administrator role with full access to all resources.", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { new Guid("28ca585b-008c-4c6d-9de5-3cdea4c0ffb3"), new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b") },
                    { new Guid("4990d90f-c549-4653-a0b8-9ea74ba75154"), new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b") },
                    { new Guid("bb933aaa-3d53-4777-99ec-6e51bda093df"), new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b") },
                    { new Guid("cf54f7b4-c5d9-426a-a163-de7c8637ce24"), new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b") },
                    { new Guid("28ca585b-008c-4c6d-9de5-3cdea4c0ffb3"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") },
                    { new Guid("304ac39e-a347-4acb-a8c6-d0a2639c4a06"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") },
                    { new Guid("4990d90f-c549-4653-a0b8-9ea74ba75154"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") },
                    { new Guid("b436114f-7eb9-44b9-90f6-916de2b2da52"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") },
                    { new Guid("bb933aaa-3d53-4777-99ec-6e51bda093df"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") },
                    { new Guid("cf54f7b4-c5d9-426a-a163-de7c8637ce24"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("bb10e887-c4eb-4473-85f8-556838100856"), new Guid("050f9135-b2ae-4fab-91c1-cac15141c95f") },
                    { new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b"), new Guid("62bee773-93bd-42f8-8774-a44161e833a0") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("28ca585b-008c-4c6d-9de5-3cdea4c0ffb3"), new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("4990d90f-c549-4653-a0b8-9ea74ba75154"), new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("bb933aaa-3d53-4777-99ec-6e51bda093df"), new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("cf54f7b4-c5d9-426a-a163-de7c8637ce24"), new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("28ca585b-008c-4c6d-9de5-3cdea4c0ffb3"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("304ac39e-a347-4acb-a8c6-d0a2639c4a06"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("4990d90f-c549-4653-a0b8-9ea74ba75154"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("b436114f-7eb9-44b9-90f6-916de2b2da52"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("bb933aaa-3d53-4777-99ec-6e51bda093df"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { new Guid("cf54f7b4-c5d9-426a-a163-de7c8637ce24"), new Guid("bb10e887-c4eb-4473-85f8-556838100856") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { new Guid("bb10e887-c4eb-4473-85f8-556838100856"), new Guid("050f9135-b2ae-4fab-91c1-cac15141c95f") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b"), new Guid("62bee773-93bd-42f8-8774-a44161e833a0") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: new Guid("28ca585b-008c-4c6d-9de5-3cdea4c0ffb3"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: new Guid("304ac39e-a347-4acb-a8c6-d0a2639c4a06"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: new Guid("4990d90f-c549-4653-a0b8-9ea74ba75154"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: new Guid("b436114f-7eb9-44b9-90f6-916de2b2da52"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: new Guid("bb933aaa-3d53-4777-99ec-6e51bda093df"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "id",
                keyValue: new Guid("cf54f7b4-c5d9-426a-a163-de7c8637ce24"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "id",
                keyValue: new Guid("8542204b-0233-459c-8049-d1a4aca1ff6b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "id",
                keyValue: new Guid("bb10e887-c4eb-4473-85f8-556838100856"));
        }
    }
}

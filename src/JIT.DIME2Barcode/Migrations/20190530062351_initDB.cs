using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JIT.DIME2Barcode.Migrations
{
    public partial class initDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
           
            migrationBuilder.CreateTable(
                name: "ICMOQualityRpt",
                columns: table => new
                {
                    FID = table.Column<string>(nullable: false),
                    FItemID = table.Column<int>(nullable: false),
                    FAuxQty = table.Column<decimal>(nullable: true),
                    FRemark = table.Column<string>(nullable: true),
                    FNote = table.Column<string>(nullable: true),
                    ICMOInspectBillID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICMOQualityRpt", x => x.FID);
                });

           

           
            

      
           
            migrationBuilder.CreateTable(
                name: "t_SubMessage",
                columns: table => new
                {
                    FInterID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FBrNo = table.Column<string>(nullable: true),
                    FID = table.Column<string>(nullable: true),
                    FParentID = table.Column<int>(nullable: false),
                    FName = table.Column<string>(nullable: true),
                    FTypeID = table.Column<int>(nullable: false),
                    FDetail = table.Column<int>(nullable: false),
                    FDeleted = table.Column<int>(nullable: false),
                    FModifyTime = table.Column<byte[]>(nullable: true),
                    FSystemType = table.Column<int>(nullable: false),
                    FSpec = table.Column<string>(nullable: true),
                    UUID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_SubMessage", x => x.FInterID);
                });

           

            
            

          
            
            

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "ICMOQualityRpt");

           
            migrationBuilder.DropTable(
                name: "t_SubMessage");

          
        }
    }
}

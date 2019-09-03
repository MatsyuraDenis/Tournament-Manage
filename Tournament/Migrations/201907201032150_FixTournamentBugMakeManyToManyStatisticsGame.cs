namespace Tournament.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTournamentBugMakeManyToManyStatisticsGame : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "TeamStatistic_Id", "dbo.TeamStatistics");
            DropIndex("dbo.Games", new[] { "TeamStatistic_Id" });
            RenameColumn(table: "dbo.Games", name: "TournamentTable_Id", newName: "TournamentTableId");
            RenameIndex(table: "dbo.Games", name: "IX_TournamentTable_Id", newName: "IX_TournamentTableId");
            CreateTable(
                "dbo.TeamStatisticGames",
                c => new
                    {
                        TeamStatistic_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamStatistic_Id, t.Game_Id })
                .ForeignKey("dbo.TeamStatistics", t => t.TeamStatistic_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.TeamStatistic_Id)
                .Index(t => t.Game_Id);
            
            DropColumn("dbo.Games", "TournamentId");
            DropColumn("dbo.Games", "TeamStatistic_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "TeamStatistic_Id", c => c.Int());
            AddColumn("dbo.Games", "TournamentId", c => c.Int());
            DropForeignKey("dbo.TeamStatisticGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.TeamStatisticGames", "TeamStatistic_Id", "dbo.TeamStatistics");
            DropIndex("dbo.TeamStatisticGames", new[] { "Game_Id" });
            DropIndex("dbo.TeamStatisticGames", new[] { "TeamStatistic_Id" });
            DropTable("dbo.TeamStatisticGames");
            RenameIndex(table: "dbo.Games", name: "IX_TournamentTableId", newName: "IX_TournamentTable_Id");
            RenameColumn(table: "dbo.Games", name: "TournamentTableId", newName: "TournamentTable_Id");
            CreateIndex("dbo.Games", "TeamStatistic_Id");
            AddForeignKey("dbo.Games", "TeamStatistic_Id", "dbo.TeamStatistics", "Id");
        }
    }
}

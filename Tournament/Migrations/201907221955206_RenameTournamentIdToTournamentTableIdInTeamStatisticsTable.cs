namespace Tournament.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTournamentIdToTournamentTableIdInTeamStatisticsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TeamStatistics", name: "TournamentTable_Id", newName: "TournamentTableId");
            RenameIndex(table: "dbo.TeamStatistics", name: "IX_TournamentTable_Id", newName: "IX_TournamentTableId");
            DropColumn("dbo.TeamStatistics", "TournamentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamStatistics", "TournamentId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.TeamStatistics", name: "IX_TournamentTableId", newName: "IX_TournamentTable_Id");
            RenameColumn(table: "dbo.TeamStatistics", name: "TournamentTableId", newName: "TournamentTable_Id");
        }
    }
}

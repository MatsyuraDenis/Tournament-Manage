namespace Tournament.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(),
                        TeamAScore = c.Int(),
                        TeamBScore = c.Int(),
                        IsForEdit = c.Boolean(nullable: false),
                        Round = c.Int(),
                        TournamentName = c.String(),
                        TournamentTable_Id = c.Int(),
                        TeamStatistic_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TournamentTables", t => t.TournamentTable_Id)
                .ForeignKey("dbo.TeamStatistics", t => t.TeamStatistic_Id)
                .Index(t => t.TournamentTable_Id)
                .Index(t => t.TeamStatistic_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsExist = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TournamentTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        TeamName = c.String(nullable: false),
                        TeamId = c.Int(nullable: false),
                        Win = c.Int(nullable: false),
                        Draw = c.Int(nullable: false),
                        Looses = c.Int(nullable: false),
                        Scored = c.Int(nullable: false),
                        Missed = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        TournamentTable_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.TournamentTables", t => t.TournamentTable_Id, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.TournamentTable_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TeamGames",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Game_Id })
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.TournamentTableTeams",
                c => new
                    {
                        TournamentTable_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TournamentTable_Id, t.Team_Id })
                .ForeignKey("dbo.TournamentTables", t => t.TournamentTable_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.TournamentTable_Id)
                .Index(t => t.Team_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TournamentTableTeams", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.TournamentTableTeams", "TournamentTable_Id", "dbo.TournamentTables");
            DropForeignKey("dbo.TeamStatistics", "TournamentTable_Id", "dbo.TournamentTables");
            DropForeignKey("dbo.TeamStatistics", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Games", "TeamStatistic_Id", "dbo.TeamStatistics");
            DropForeignKey("dbo.Games", "TournamentTable_Id", "dbo.TournamentTables");
            DropForeignKey("dbo.TeamGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.TeamGames", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TournamentTableTeams", new[] { "Team_Id" });
            DropIndex("dbo.TournamentTableTeams", new[] { "TournamentTable_Id" });
            DropIndex("dbo.TeamGames", new[] { "Game_Id" });
            DropIndex("dbo.TeamGames", new[] { "Team_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TeamStatistics", new[] { "TournamentTable_Id" });
            DropIndex("dbo.TeamStatistics", new[] { "TeamId" });
            DropIndex("dbo.Games", new[] { "TeamStatistic_Id" });
            DropIndex("dbo.Games", new[] { "TournamentTable_Id" });
            DropTable("dbo.TournamentTableTeams");
            DropTable("dbo.TeamGames");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TeamStatistics");
            DropTable("dbo.TournamentTables");
            DropTable("dbo.Teams");
            DropTable("dbo.Games");
        }
    }
}

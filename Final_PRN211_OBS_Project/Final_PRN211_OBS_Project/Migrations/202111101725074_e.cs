namespace Final_PRN211_OBS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 200),
                        wiki_url = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 200),
                        image_url = c.String(nullable: false, maxLength: 200),
                        author_id = c.Int(nullable: false),
                        description = c.String(nullable: false),
                        price = c.Double(nullable: false),
                        status = c.Boolean(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Author", t => t.author_id)
                .Index(t => t.author_id);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Orderline",
                c => new
                    {
                        bill_id = c.Int(nullable: false),
                        book_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.bill_id, t.book_id })
                .ForeignKey("dbo.Bill", t => t.bill_id)
                .ForeignKey("dbo.Book", t => t.book_id)
                .Index(t => t.bill_id)
                .Index(t => t.book_id);
            
            CreateTable(
                "dbo.Bill",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        address = c.String(nullable: false, maxLength: 200),
                        telephone = c.String(nullable: false, maxLength: 20),
                        date = c.DateTime(nullable: false, storeType: "date"),
                        payment = c.String(nullable: false, maxLength: 20),
                        status = c.String(nullable: false, maxLength: 20),
                        total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.User", t => t.user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false, maxLength: 100),
                        password = c.String(nullable: false, maxLength: 30),
                        username = c.String(nullable: false, maxLength: 100),
                        avatar_url = c.String(nullable: false, maxLength: 200),
                        role_id = c.Int(nullable: false),
                        status = c.Int(nullable: false)
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Role", t => t.role_id)
                .Index(t => t.role_id);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        book_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.User", t => t.user_id)
                .ForeignKey("dbo.Book", t => t.book_id)
                .Index(t => t.book_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        book_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.book_id)
                .ForeignKey("dbo.Book", t => t.book_id)
                .Index(t => t.book_id);
            
            CreateTable(
                "dbo.Book_Genre",
                c => new
                    {
                        book_id = c.Int(nullable: false),
                        genre_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.book_id, t.genre_id })
                .ForeignKey("dbo.Book", t => t.book_id, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.genre_id, cascadeDelete: true)
                .Index(t => t.book_id)
                .Index(t => t.genre_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book", "author_id", "dbo.Author");
            DropForeignKey("dbo.Stock", "book_id", "dbo.Book");
            DropForeignKey("dbo.Review", "book_id", "dbo.Book");
            DropForeignKey("dbo.Orderline", "book_id", "dbo.Book");
            DropForeignKey("dbo.User", "role_id", "dbo.Role");
            DropForeignKey("dbo.Review", "user_id", "dbo.User");
            DropForeignKey("dbo.Bill", "user_id", "dbo.User");
            DropForeignKey("dbo.Orderline", "bill_id", "dbo.Bill");
            DropForeignKey("dbo.Book_Genre", "genre_id", "dbo.Genre");
            DropForeignKey("dbo.Book_Genre", "book_id", "dbo.Book");
            DropIndex("dbo.Book_Genre", new[] { "genre_id" });
            DropIndex("dbo.Book_Genre", new[] { "book_id" });
            DropIndex("dbo.Stock", new[] { "book_id" });
            DropIndex("dbo.Review", new[] { "user_id" });
            DropIndex("dbo.Review", new[] { "book_id" });
            DropIndex("dbo.User", new[] { "role_id" });
            DropIndex("dbo.Bill", new[] { "user_id" });
            DropIndex("dbo.Orderline", new[] { "book_id" });
            DropIndex("dbo.Orderline", new[] { "bill_id" });
            DropIndex("dbo.Book", new[] { "author_id" });
            DropTable("dbo.Book_Genre");
            DropTable("dbo.Stock");
            DropTable("dbo.Role");
            DropTable("dbo.Review");
            DropTable("dbo.User");
            DropTable("dbo.Bill");
            DropTable("dbo.Orderline");
            DropTable("dbo.Genre");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}

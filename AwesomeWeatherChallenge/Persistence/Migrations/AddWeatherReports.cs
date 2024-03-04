using FluentMigrator;

namespace AwesomeWeatherChallenge.Persistence.Migrations;

[Migration(202443)]
public class AddWeatherReports : Migration
{
    public override void Down()
    {
        Delete.Table("Reports");
    }

    public override void Up()
    {
        Create.Table("Reports")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Data").AsString()
            .WithColumn("CreatedAt").AsDateTime2();
    }
}

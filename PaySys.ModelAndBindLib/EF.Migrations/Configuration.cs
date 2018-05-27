using System.Data.Entity.Migrations;
using PaySys.ModelAndBindLib.Engine;

namespace PaySys.ModelAndBindLib.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<PaySysContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PaySysContext context)
        {
	        #region MyRegion

	        //  This method will be called after migrating to the latest version.

	        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
	        //  to avoid creating duplicate seed data. E.g.
	        //
	        //    context.People.AddOrUpdate(
	        //      p => p.FullName,
	        //      new Person { FullName = "Andrew Peters" },
	        //      new Person { FullName = "Brice Lambson" },
	        //      new Person { FullName = "Rowan Miller" }
	        //    );
	        //

	        #endregion
        }
    }
}

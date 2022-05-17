using MeuDinheiro.Infra.Data.Context;
using System.Data.Entity.Migrations;

namespace MeuDinheiro.Infra.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MeuDinheiroDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MeuDinheiroDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

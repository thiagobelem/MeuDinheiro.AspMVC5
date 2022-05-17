using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MeuDinheiro.Bussiness.Models.Categorias;
using MeuDinheiro.Bussiness.Models.Lancamentos;
using MeuDinheiro.Infra.Data.Mappings;

namespace MeuDinheiro.Infra.Data.Context
{
    public class MeuDinheiroDBContext : DbContext
    {
        public MeuDinheiroDBContext() : base("MeuDinheiro")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Lancamento> Lancamentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Configurations.Add(new CategoriaConfig());
            modelBuilder.Configurations.Add(new LancamentoConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}

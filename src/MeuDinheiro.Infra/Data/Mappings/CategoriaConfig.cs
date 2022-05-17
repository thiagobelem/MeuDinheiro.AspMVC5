using System.Data.Entity.ModelConfiguration;
using MeuDinheiro.Bussiness.Models.Categorias;

namespace MeuDinheiro.Infra.Data.Mappings
{
    public class CategoriaConfig : EntityTypeConfiguration<Categoria>
    {
        public CategoriaConfig()
        {
            HasKey(f => f.Id);

            Property(f => f.Descricao).IsRequired().HasMaxLength(50);

            ToTable("Categorias");
        }
    }
}

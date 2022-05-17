using System.Data.Entity.ModelConfiguration;
using MeuDinheiro.Bussiness.Models.Lancamentos;

namespace MeuDinheiro.Infra.Data.Mappings
{
    public class LancamentoConfig : EntityTypeConfiguration<Lancamento>
    {
        public LancamentoConfig()
        {
            HasKey(f => f.Id);

            Property(f => f.Descricao).IsRequired().HasMaxLength(100);

            HasRequired(f => f.Categoria).WithMany(f => f.Lancamentos).HasForeignKey(f => f.CategoriaId);

            ToTable("Lancamentos");
        }
    }
}

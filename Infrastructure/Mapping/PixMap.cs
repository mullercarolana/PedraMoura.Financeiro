using ApplicationCore.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public sealed class PixMap : IEntityTypeConfiguration<Pix>
    {
        public void Configure(EntityTypeBuilder<Pix> builder)
        {
            builder.HasBaseType<PaymentOrder>();
            builder.OwnsOne(p => p.PixPayment, pixPayment =>
            {
                pixPayment.OwnsOne(p => p.Calendario, calendario =>
                {
                    calendario.Property(c => c.Criacao).HasColumnName("CalendarioCriacao").HasColumnType("datetime2");
                    calendario.Property(c => c.Expiracao).HasColumnName("CalendarioExpiracao").HasColumnType("int");
                });
                pixPayment.Property(p => p.Txid).HasColumnName("Txid").HasColumnType("varchar(max)");
                pixPayment.Property(p => p.Revisao).HasColumnName("Revisao").HasColumnType("int");
                pixPayment.OwnsOne(p => p.Loc, loc =>
                {
                    loc.Property(l => l.Id).HasColumnName("LocId").HasColumnType("int");
                    loc.Property(l => l.Location).HasColumnName("LocLocation").HasColumnType("varchar(max)");
                    loc.Property(l => l.TipoCob).HasColumnName("LocTipoCob").HasColumnType("varchar(max)");
                    loc.Property(l => l.Criacao).HasColumnName("LocCriacao").HasColumnType("datetime2");
                });
                pixPayment.Property(p => p.Location).HasColumnName("Location").HasColumnType("varchar(max)");
                pixPayment.Property(p => p.Status).HasColumnName("Status").HasColumnType("varchar(max)");
                pixPayment.OwnsOne(p => p.Devedor, devedor =>
                {
                    devedor.Property(d => d.Cpf).HasColumnName("DevedorCpf").HasColumnType("varchar(11)");
                    devedor.Property(d => d.Nome).HasColumnName("DevedorNome").HasColumnType("varchar(max)");
                });
                pixPayment.OwnsOne(p => p.Valor, valor =>
                {
                    valor.Property(v => v.Original).HasColumnName("ValorOriginal").HasColumnType("varchar(max)");
                });
                pixPayment.Property(p => p.Chave).HasColumnName("Chave").HasColumnType("varchar(max)");
                pixPayment.Property(p => p.SolicitacaoPagador).HasColumnName("SolicitacaoPagador").HasColumnType("varchar(max)");
            });
        }
    }
}

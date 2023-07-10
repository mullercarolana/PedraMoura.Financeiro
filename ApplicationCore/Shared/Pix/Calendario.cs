namespace ApplicationCore.Shared.Pix
{
    public class Calendario
    {
        public Calendario(int expiracao)
        {
            Expiracao = expiracao;
        }

        public int Expiracao { get; set; }
    }

    public class CalendarioCompleto
    {
        public CalendarioCompleto(DateTime criacao, int expiracao)
        {
            Criacao = criacao;
            Expiracao = expiracao;
        }

        public DateTime Criacao { get; set; }
        public int Expiracao { get; set; }
    }
}

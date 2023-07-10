namespace ApplicationCore.Shared.Pix
{
    public sealed class Body
    {
        public Body(Calendario calendario, Devedor devedor, Valor valor, string chave, string solicitacaoPagador)
        {
            Calendario = calendario;
            Devedor = devedor;
            Valor = valor;
            Chave = chave;
            SolicitacaoPagador = solicitacaoPagador;
        }

        public Calendario Calendario { get; set; }
        public Devedor Devedor { get; set; }
        public Valor Valor { get; set; }
        public string Chave { get; set; }
        public string SolicitacaoPagador { get; set; }
    }
}

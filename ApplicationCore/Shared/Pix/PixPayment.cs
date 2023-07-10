namespace ApplicationCore.Shared.Pix
{
    public sealed class PixPayment
    {
        public PixPayment() { }
        public PixPayment(
            CalendarioCompleto calendario,
            string txid,
            int revisao,
            Loc loc,
            string location,
            string status,
            Devedor devedor,
            Valor valor,
            string chave,
            string solicitacaoPagador
        )
        {
            Calendario = calendario;
            Txid = txid;
            Revisao = revisao;
            Loc = loc;
            Location = location;
            Status = status;
            Devedor = devedor;
            Valor = valor;
            Chave = chave;
            SolicitacaoPagador = solicitacaoPagador;
        }

        public CalendarioCompleto Calendario { get; set; }
        public string Txid { get; set; }
        public int Revisao { get; set; }
        public Loc Loc { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public Devedor Devedor { get; set; }
        public Valor Valor { get; set; }
        public string Chave { get; set; }
        public string SolicitacaoPagador { get; set; }
    }
}

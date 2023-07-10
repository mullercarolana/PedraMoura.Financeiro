namespace ApplicationCore.Shared.Pix
{
    public sealed class Loc
    {
        public Loc(int id, string location, string tipoCob, DateTime criacao)
        {
            Id = id;
            Location = location;
            TipoCob = tipoCob;
            Criacao = criacao;
        }

        public int Id { get; set; }
        public string Location { get; set; }
        public string TipoCob { get; set; }
        public DateTime Criacao { get; set; }
    }
}

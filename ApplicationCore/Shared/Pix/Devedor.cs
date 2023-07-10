namespace ApplicationCore.Shared.Pix
{
    public sealed class Devedor
    {
        public Devedor(string cpf, string nome)
        {
            Cpf = cpf;
            Nome = nome;
        }

        public string Cpf { get; set; }
        public string Nome { get; set; }
    }
}

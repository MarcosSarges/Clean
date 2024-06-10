namespace Limp
{
    internal class Config
    {
        // Config Banco
        public string Servidor { get; set; }
        public string Usuario { get; set; }
        public string NomeBanco { get; set; }
        public string Senha { get; set; }
        public int Intervalo { get; set; }
        public string PDV { get; set; }

        // Sobrescrevendo o método ToString
        public override string ToString()
        {
            return $"Servidor: {Servidor}, Usuario: {Usuario}, Nome do Banco: {NomeBanco}, Senha: {Senha}, Intervalo: {Intervalo}, PDV: {PDV}";
        }
    }
}

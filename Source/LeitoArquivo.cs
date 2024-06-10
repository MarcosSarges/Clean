using System;
using System.IO;

namespace Limp
{
    internal class LeitoArquivo
    {
        public static void Ler(Config config)
        {
            StreamReader read = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\Config.txt");
            config.Servidor = read.ReadLine().Split('=')[1];
            config.NomeBanco = read.ReadLine().Split('=')[1];
            config.Usuario = read.ReadLine().Split('=')[1];
            config.Senha = read.ReadLine().Split('=')[1];
            config.Intervalo = Convert.ToInt32(read.ReadLine().Split('=')[1]);
            config.PDV = read.ReadLine().Split('=')[1];
        }
    }
}

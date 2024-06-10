﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Limp
{
    internal class App
    {
        public static int Run() {
            Config config = new Config();
            LeitoArquivo.Ler(config);
            DateTime agora = DateTime.Now;
            string diretorio = "log";
            string nomeArquivo = agora.ToString("dd_MM_yyyy") + ".txt";
            string caminhoDiretorio = Path.Combine(Environment.CurrentDirectory, diretorio);
            if (!Directory.Exists(caminhoDiretorio))
            {
                Directory.CreateDirectory(caminhoDiretorio);
            }
                
            string caminhoArquivo = Path.Combine(caminhoDiretorio, nomeArquivo);
            if (!File.Exists(caminhoArquivo))
            {
                var file = File.CreateText(caminhoArquivo);
                file.Close();
            }
            DB db = new DB(config);
            float value = db.RunCount();
            db.RunDelete();
            db.Fechar();
            using (StreamWriter writer = new StreamWriter(caminhoArquivo, true))
            {
                if (value > 0)
                {
                    writer.WriteLine($"{DateTime.Now} - O valor total das vendas excluídas foi de R$ {value}");
                }
                else
                {
                    writer.WriteLine($"{DateTime.Now} - Nenhum pagamento encontrado");
                }
            }
            return config.Intervalo;
        }
    }
}
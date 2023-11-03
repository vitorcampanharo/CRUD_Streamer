using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    public class Program
    {
        static void Main(string[] args)
        {
            Streamer streamer = new Streamer();           
            while (true)
            {
                Console.WriteLine("Bem-vindo à plataforma de streaming!");
                Console.WriteLine("1 - Entrar como cliente");
                Console.WriteLine("2 - Entrar como administrador");
                Console.WriteLine("3 - Sair");
                Console.WriteLine("Escolha uma opção: ");

                string escolhaInicial = Console.ReadLine();

                switch (escolhaInicial)
                {
                    case "1":
                        streamer.MenuCliente();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        streamer.LoginAdministrador();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3":
                        Environment.Exit(0);
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }
        
    }
}

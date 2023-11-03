using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    public class Streamer
    {
        public Streamer()
        {
            CarregarTitulosDeArquivo();
            CarregarAssinantesDeArquivo();
            ExcluirTitulosArquivo();
        }

        public List<Titulo> titulos = new List<Titulo>();
        public List<Assinante> assinantes = new List<Assinante>();
        
        public enum ClassificacaoEtaria
        {
            Livre = 0,
            Dez = 10,
            Doze = 12,
            Quatorze = 14,
            Dezesseis = 16,
            Dezoito = 18
        }
        public void MenuCliente()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("\nMenu Cliente:");
                Console.WriteLine("1 - Fazer login");
                Console.WriteLine("2 - Criar conta");
                Console.WriteLine("3 - Voltar");
                Console.WriteLine("Escolha uma opção: ");

                string escolhaCliente = Console.ReadLine();

                switch (escolhaCliente)
                {
                    case "1":
                        FazerLoginCliente();
                        break;
                    case "2":
                        CriarContaCliente();
                        break;
                    case "3":
                        Console.WriteLine("\r\nSaindo ...");
                        Console.WriteLine("Tecle 'Enter' para voltar");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
        public void MenuAdministrador()
        {
            while (true)
            {
                Console.WriteLine("\nMenu Administrador:");
                Console.WriteLine("1 - Cadastrar Título");
                Console.WriteLine("2 - Consultar Título");
                Console.WriteLine("3 - Editar");
                Console.WriteLine("4 - Remover um título");
                Console.WriteLine("5 - Sair");
                Console.WriteLine("Escolha uma opção: ");

                string escolhaAdm = Console.ReadLine();

                switch (escolhaAdm)
                {
                    case "1":
                        CadastrarTitulo();
                        break;
                    case "2":
                        ConsultarTitulo();
                        break;
                    case "3":
                        Editar();
                        break;
                    case "4":
                        RemoverTitulo();
                        break;
                    case "5":
                        Console.WriteLine("\r\nSaindo ...");
                        Console.WriteLine("Tecle 'Enter' para voltar");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
        public void LoginAdministrador()
        {
            Console.Clear();
            Console.Write("Digite o nome de usuário do administrador (ou 'sair' para voltar)\r\n (Login: vitor; Senha:123): ");
            string nomeUsuario = Console.ReadLine();

            if (string.Equals(nomeUsuario, "sair", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Saindo ...");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
                return;
            }
            if (!string.Equals(nomeUsuario, "vitor", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Nome de usuário inválido");
                return;
            }
            else
            {
                Console.Write("Digite a senha do administrador: ");
                string senha = Console.ReadLine();
                if (senha != "123")
                {
                    Console.WriteLine("Senha incorreta");
                }
                else
                {
                    Console.WriteLine("Seja bem-vindo");
                    MenuAdministrador();
                }
            }
        }
        public void FazerLoginCliente()
        {
            Console.Clear();
            Console.Write("Digite o nome de usuário (ou 'sair' para voltar ao menu): ");
            string nomeUsuario = Console.ReadLine();
            var nomeUsuarioLogin = assinantes.FirstOrDefault(a => string.Equals(a.Nome, nomeUsuario, StringComparison.OrdinalIgnoreCase));
            if (nomeUsuario.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\r\nVoltando ...");
            }
            else
            {
                Console.Write("Digite a senha: ");
                string senha = Console.ReadLine();

                Assinante assinante = assinantes.FirstOrDefault(a => a.NomeUsuario == nomeUsuario && a.Senha == senha);
                if (assinante == null)
                {
                    Console.Clear();
                    Console.WriteLine("Login ou senha incorretos.");
                }
                else
                {
                    Console.WriteLine($"Bem-vindo, {assinante.Nome}! \r");
                    if (titulos.Count == 0)
                    {
                        Console.WriteLine("Nenhum título foi cadastrado ainda");
                    }
                    else
                    {
                        foreach (var titulo in titulos)
                        {
                            Console.WriteLine("Título: " + titulo.NomeTitulo + "; Classificação: " + titulo.Classificacao);
                        }
                    nomeTitulo:
                        Console.WriteLine("Digite o título que deseja assistir (ou 'sair' para voltar): ");
                        string nomeTitulo = Console.ReadLine();
                        var tituloParaAssistir = titulos.FirstOrDefault(t => string.Equals(t.NomeTitulo, nomeTitulo, StringComparison.OrdinalIgnoreCase));
                        if (nomeTitulo.Equals("sair", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Saindo ...");
                            Console.WriteLine("\r\nTecle 'Enter' para voltar");
                        }
                        else if (tituloParaAssistir == null)
                        {
                            Console.WriteLine("Erro: Título não encontrado");
                            goto nomeTitulo;
                        }
                        else if ((int)tituloParaAssistir.Classificacao >= assinante.Idade)
                        {
                            Console.WriteLine("Você não tem a idade mínima para assistir o título");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\r Título: " + tituloParaAssistir.NomeTitulo);
                            Console.WriteLine("Classificação: " + tituloParaAssistir.Classificacao);
                            Console.WriteLine("Sinopse: " + tituloParaAssistir.Sinopse);
                        }
                    }
                }
            }
        }
        public void CriarContaCliente()
        {
            Console.Clear();
        Nome:
            Console.Write("Digite o nome (ou 'sair' para voltar): ");
            string nome = Console.ReadLine();
            if (nome.ToString() == string.Empty)
            {
                Console.Clear();
                Console.WriteLine("Erro: É necessário digitar um nome");
                goto Nome;
            }
            else if (nome.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Saindo ...");
                return;
            }
            else if (!nome.All(char.IsLetter))
            {
                Console.Clear();
                Console.WriteLine("Nome inválido. Deve conter apenas letras.");
                goto Nome;
            }
            else if (assinantes.Any(a => a.Nome.ToLower().Trim() == nome.ToLower().Trim()))
            {
                Console.Clear();
                Console.WriteLine("Cliente já cadastrado anteriormente");
                goto Nome;
            }
        NomeUsuario:
            Console.Write("Digite o nome de usuário: ");
            string nomeUsuario = Console.ReadLine();
            if (nome.ToString() == string.Empty)
            {
                Console.Clear();
                Console.WriteLine("Erro: É necessário digitar um nome");
                goto NomeUsuario;
            }
            else if (nome.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Saindo ...");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
                return;
            }
            else if (assinantes.Any(c => c.Nome.ToLower().Trim() == nome.ToLower().Trim()))
            {
                Console.Clear();
                Console.WriteLine("Cliente já cadastrado anteriormente");
                goto Nome;
            }
        Senha:
            Console.Write("Digite a senha: ");
            string senha = Console.ReadLine();
            if (senha.ToString() == string.Empty)
            {
                Console.Clear();
                Console.WriteLine("Erro: É necessário digitar uma senha");
                goto Senha;
            }
            else if (nome.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Saindo ...");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
                return;
            }
        Idade:
            Console.WriteLine("Digite sua idade");
            int idade;
            if (!int.TryParse(Console.ReadLine(), out idade))
            {
                Console.Clear();
                Console.WriteLine("Erro: Idade deve ser um número válido.");
                goto Idade;
            }
            else if (idade <= 0)
            {
                Console.Clear();
                Console.WriteLine("Erro: Idade não pode ser negativo.");
                goto Idade;
            }
            else
            {
                Console.Clear();
                Assinante novoAssinante = new Assinante(nome, nomeUsuario, senha, idade);
                assinantes.Add(novoAssinante);
                Console.WriteLine("Conta criada com sucesso!");
                SalvarAssinantes();
            }
        }
        public void CadastrarTitulo()
        {
            Console.Clear();
        NomeTitulo:
            Console.Write("Digite o nome do título: ");
            string nomeTitulo = Console.ReadLine();
            if (nomeTitulo.ToString() == string.Empty)
            {
                Console.Clear();
                Console.WriteLine("Erro: É necessário digitar um nome");
                goto NomeTitulo;
            }
            else if (nomeTitulo.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Saindo ...");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
                return;
            }
            else if (titulos.Any(t => t.NomeTitulo.ToLower().Trim() == nomeTitulo.ToLower().Trim()))
            {
                Console.Clear();
                Console.WriteLine("Titulo já cadastrado anteriormente");
                goto NomeTitulo;
            }

        Sinopse:
            Console.Write("Escreva a sinopse do título: ");
            string sinopse = Console.ReadLine();
            if (sinopse.ToString() == string.Empty)
            {
                Console.Clear();
                Console.WriteLine("Erro: É necessário digitar alguma coisa");
                goto Sinopse;
            }
            else if (sinopse.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Saindo ...");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
                return;
            }
            else
            {
                Console.WriteLine("Escolha a classificação do título:");
                Console.WriteLine("1 - Livre");
                Console.WriteLine("2 - 10 anos");
                Console.WriteLine("3 - 12 anos");
                Console.WriteLine("4 - 14 anos");
                Console.WriteLine("5 - 16 anos");
                Console.WriteLine("6 - +18");
                string escolhaClassificacao = Console.ReadLine();
                ClassificacaoEtaria classificacao;
                switch (escolhaClassificacao)
                {
                    case "1":
                        classificacao = ClassificacaoEtaria.Livre;
                        break;
                    case "2":
                        classificacao = ClassificacaoEtaria.Dez;
                        break;
                    case "3":
                        classificacao = ClassificacaoEtaria.Doze;
                        break;
                    case "4":
                        classificacao = ClassificacaoEtaria.Quatorze;
                        break;
                    case "5":
                        classificacao = ClassificacaoEtaria.Dezesseis;
                        break;
                    case "6":
                        classificacao = ClassificacaoEtaria.Dezoito;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. A classificação será definida automaticamente como 'Livre'.");
                        classificacao = ClassificacaoEtaria.Livre;
                        break;
                }
                Console.Clear();
                Titulo titulo = new Titulo(nomeTitulo, sinopse, classificacao);
                titulos.Add(titulo);
                Console.WriteLine("Título cadastrado com sucesso!");
                SalvarTitulos();
            }
        }
        public void ConsultarTitulo()
        {
            Console.Clear();
            if (titulos.Count == 0)
            {
                Console.WriteLine("Nenhum titulo cadastrado ainda");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
            }
            else
            {
                foreach (var titulo in titulos)
                {
                    Console.WriteLine($"Titulo: {titulo.NomeTitulo}; Classificação: {titulo.Classificacao}");
                }

                Console.WriteLine("\r\nTecle 'Enter' para sair");
            }
        }
        public void Editar()
        {
            Console.Clear();
            if (titulos.Count == 0)
            {
                Console.WriteLine("Nenhum título cadastrado ainda");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
            }
            else
            {
                Console.WriteLine("Escolha qual ítem deseja editar:");
                Console.WriteLine("1 - Editar classificação do título");
                Console.WriteLine("2 - Editar sinopse do título");
                Console.WriteLine("3 - Voltar");
                string escolhaInicial = Console.ReadLine();

                switch (escolhaInicial)
                {
                    case "1":
                        EditarClassificacaoTitulo();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        EditarSinopseTitulo();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3":
                        Console.WriteLine("\r\nSaindo ...");
                        Console.WriteLine("Tecle 'Enter' para voltar");
                        return;
                }
            }

        }
        public void EditarClassificacaoTitulo()
        {
            Console.Clear();
            foreach (var titulo in titulos)
            {
                Console.WriteLine("Título: " + titulo.NomeTitulo + "; Classificação: " + titulo.Classificacao);
            }
        NomeTitulo:
            Console.WriteLine("\r\nInforme o nome do título a ser editado (ou 'sair' para voltar ao menu principal): ");
            string nomeTitulo = Console.ReadLine();
            var tituloParaeditar = titulos.FirstOrDefault(t => string.Equals(t.NomeTitulo, nomeTitulo, StringComparison.OrdinalIgnoreCase));
            if (nomeTitulo.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Saindo ...");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
            }
            else if (tituloParaeditar == null)
            {
                Console.WriteLine("Título não encontrado.");
                goto NomeTitulo;
            }
            else
            {
                Console.WriteLine("Informe a nova classificação:");
                Console.WriteLine("1 - Livre");
                Console.WriteLine("2 - 10 anos");
                Console.WriteLine("3 - 12 anos");
                Console.WriteLine("4 - 14 anos");
                Console.WriteLine("5 - 16 anos");
                Console.WriteLine("6 - +18");
                string escolhaClassificacao = Console.ReadLine();
                switch (escolhaClassificacao)
                {
                    case "1":
                        tituloParaeditar.Classificacao = ClassificacaoEtaria.Livre;
                        break;
                    case "2":
                        tituloParaeditar.Classificacao = ClassificacaoEtaria.Dez;
                        break;
                    case "3":
                        tituloParaeditar.Classificacao = ClassificacaoEtaria.Doze;
                        break;
                    case "4":
                        tituloParaeditar.Classificacao = ClassificacaoEtaria.Quatorze;
                        break;
                    case "5":
                        tituloParaeditar.Classificacao = ClassificacaoEtaria.Dezesseis;
                        break;
                    case "6":
                        tituloParaeditar.Classificacao = ClassificacaoEtaria.Dezoito;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. A classificação não foi alterada.");
                        break;
                }
                Console.WriteLine($"O título {nomeTitulo} foi atalizado com sucesso.");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
                SalvarTitulos();
            }
        }
        public void EditarSinopseTitulo()
        {
            Console.Clear();
            foreach (var titulo in titulos)
            {
                Console.WriteLine("Título: " + titulo.NomeTitulo + "; Salario: " + titulo.Classificacao);
            }
        NomeTitulo:
            Console.WriteLine("\r\nInforme o nome do título a ser editado (ou 'sair' para voltar ao menu principal): ");
            string nomeTitulo = Console.ReadLine();
            var tituloEitarSinopse = titulos.FirstOrDefault(t => string.Equals(t.NomeTitulo, nomeTitulo, StringComparison.OrdinalIgnoreCase));
            if (nomeTitulo.Equals("sair", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Saindo ...");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
            }
            else if (tituloEitarSinopse == null)
            {
                Console.WriteLine("Título não encontrado.");
                goto NomeTitulo;
            }
            else
            {
                Console.Clear();
                Console.Write("Digite a nova sinopse do título: ");
                string novaSinopse = Console.ReadLine();
                tituloEitarSinopse.Sinopse = novaSinopse;
                Console.WriteLine("Sinopse editada com susesso!");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
                SalvarTitulos();
            }          
        }
        public void RemoverTitulo()
        {
            Console.Clear();
            if (titulos.Count == 0)
            {
                Console.WriteLine("Nenhum titulo cadastrado ainda");
                Console.WriteLine("\r\nTecle 'Enter' para voltar");
            }
            else
            {
                foreach (var tituloRemover in titulos)
                {
                    Console.WriteLine("Título: " + tituloRemover.NomeTitulo + "; Classificação " + tituloRemover.Classificacao);
                }
            NomeTitulo:
                Console.WriteLine("Informe o nome do cliente a ser excluído (ou 'sair' para voltar ao menu iniciar): ");
                string nomeTitulo = Console.ReadLine();
                var tituloExclir = titulos.FirstOrDefault(t => string.Equals(t.NomeTitulo, nomeTitulo, StringComparison.OrdinalIgnoreCase));
                if (nomeTitulo.Equals("sair", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Saindo ...");
                    Console.WriteLine("\r\nTecle 'Enter' para voltar");
                }
                else if (tituloExclir == null)
                {
                    Console.WriteLine("Titulo não encontrado.");
                    goto NomeTitulo;
                }
                else
                {
                    Console.Clear();
                    titulos.Remove(tituloExclir);
                    Console.WriteLine("Título removido com sucesso!");
                    SalvarTitulos();
                }
            }
        }
        public void SalvarTitulos()
        {
            using (StreamWriter writer = new StreamWriter("dadosTitulos.txt"))
            {               
                foreach (var titulo in titulos)
                {
                    writer.WriteLine($"{titulo}");
                }
            }

            CarregarTitulosDeArquivo();
            ExcluirTitulosArquivo();

        }
        public void SalvarAssinantes()
        {
            using (StreamWriter writer = new StreamWriter("dadosAssinantes.txt"))
            {
                foreach (var assinante in assinantes)
                {
                    writer.WriteLine($"{assinante}");
                }
            }
            CarregarAssinantesDeArquivo();
        }
        public void CarregarTitulosDeArquivo()
        {
            string[] array = File.ReadAllLines("dadosTitulos.txt");
            for (int i = 0; i < array.Length; i++)
            {
                string[] auxiliar = array[i].Split(',');
                string nomeTitulo = auxiliar[0];
                ClassificacaoEtaria classificacao = (ClassificacaoEtaria)Enum.Parse(typeof(ClassificacaoEtaria), auxiliar[1]);
                string sinopse = auxiliar[2];
                Titulo titulo = new Titulo(nomeTitulo, sinopse, classificacao);
                if (!titulos.Any(t => t.NomeTitulo == nomeTitulo && t.Sinopse == sinopse && t.Classificacao == classificacao))
                    titulos.Add(titulo);
            }
        }
        public void CarregarAssinantesDeArquivo()
        {
            string[] array = File.ReadAllLines("dadosAssinantes.txt");
            for (int i = 0; i < array.Length; i++)
            {
                string[] auxiliar = array[i].Split(',');
                string nome = auxiliar[0];
                string nomeUsuario = auxiliar[1];
                string senha = auxiliar[2];
                int idade = int.Parse(auxiliar[3]);
                Assinante assinante = new Assinante(nome, nomeUsuario, senha, idade);
                if (!assinantes.Any(a => a.Nome == nome && a.NomeUsuario == nomeUsuario && a.Idade == idade && a.Senha == senha))
                    assinantes.Add(assinante);
            }
        }
        public void ExcluirTitulosArquivo()
        {
            string[] array = File.ReadAllLines("dadosAssinantes.txt");
            for (int i = 0; i < array.Length; i++)
            {
                string[] auxiliar = array[i].Split(',');
                string nome = auxiliar[0];
                string nomeUsuario = auxiliar[1];
                string senha = auxiliar[2];
                int idade = int.Parse(auxiliar[3]);
                Assinante assinante = new Assinante(nome, nomeUsuario, senha, idade);
                if (assinantes.Any(a => a.Nome == nome && a.NomeUsuario == nomeUsuario && a.Idade == idade && a.Senha == senha))
                    assinantes.Remove(assinante);
            }
        }
    }
}

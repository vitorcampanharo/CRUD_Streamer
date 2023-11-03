using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    public class Assinante
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public int Idade { get; set; }

        public Assinante(string nome, string nomeUsuario, string senha, int idade)
        {
            Nome = nome;
            NomeUsuario = nomeUsuario;
            Senha = senha;
            Idade = idade;
        }
        public override string ToString()
        {
            return $"{Nome},{NomeUsuario},{Senha},{Idade}";
        }
    }
}

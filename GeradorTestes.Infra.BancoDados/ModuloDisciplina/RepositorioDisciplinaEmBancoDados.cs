using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.BancoDados.ModuloDisciplina
{
    public class RepositorioDisciplinaEmBancoDados : IRepositorioDisciplina
    {
        public ValidationResult Inserir(Disciplina novoRegistro)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Editar(Disciplina registro)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Excluir(Disciplina registro)
        {
            throw new NotImplementedException();
        }

        public Disciplina SelecionarPorNumero(int numero)
        {
            throw new NotImplementedException();
        }

        public List<Disciplina> SelecionarTodos()
        {
            throw new NotImplementedException();
        }
    }
}

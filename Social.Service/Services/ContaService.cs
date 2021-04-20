using Social.DAL.Contas;
using Social.Model.Modelos;
using Social.Model.Extensoes;
using Social.Service.Interfaces;
using Social.Service.Models.Filtros;
using Social.Service.Models.Ordenacao;
using Social.Service.Models.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Social.Service.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _repoContaf;
        public ContaService(IContaRepository repoConta)
        {
            _repoContaf = repoConta;
        }

        public IEnumerable<ContaApi> RetornaListaConta()
        {
            return _repoContaf.All.Select(l => l.ToApi()).ToList();
        }

        public ContaPaginada RetornaListaContaPaginada(ContaFiltro filtro, ContaOrdem ordem, ContaPaginacao paginacao)
        {
            var lista = _repoContaf.All
                .AplicaFiltro(filtro)
                .AplicaOrdenacao(ordem)
                .Select(l => l.ToApi());

            return ContaPaginada.From(paginacao, lista);
        }

        public Conta RetornaConta(string contaId)
        {
            var model = _repoContaf.FindByConta(contaId);
            if (model == null)
            {
                return null;
            }
            return model;
        }

        public Conta CriarConta(ContaApi model)
        {
            var conta = model.ToConta();
            _repoContaf.Incluir(conta);
            return conta;
        }

        public Conta EditarConta(ContaApi model)
        {
            var conta = RetornaConta(model.Idenfifier);

            conta.Descricao = model.Description;
            conta.Nome = model.Name;
            conta.Status = model.Status.Equals("ACTIVE");

            _repoContaf.Alterar(conta);
            return conta;
        }

        public void ExcluirConta(Conta conta)
        {
            _repoContaf.Excluir(conta);
        }

        public void ExcluirPorContaId(string contaId)
        {
            var conta = RetornaConta(contaId);
            if (conta == null)
                throw new Exception("Conta não encontrada");

            _repoContaf.Excluir(conta);
        }

        public ContaApi RetornaContaApi(string contaId)
        {
            var model = RetornaConta(contaId);
            if (model != null)
                return model.ToApi();

            return null;
        }

    }
}

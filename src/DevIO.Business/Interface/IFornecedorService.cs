using DevIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Interface
{
    public interface IFornecedorService : IDisposable
    {
        Task Adicionar(Fornecedor fornecedor);

        Task Atualizar(Fornecedor fornecedor);

        Task AtualizarEndereco(Endereco endereco);

        Task Remover(Guid id);
    }
}

using SocialCare.DATA.Models;
using SocialCare.DATA.Services;

public class ProdutosFacade
{
    private readonly ProdutosService oProdutosService;

    public ProdutosFacade()
    {
        oProdutosService = new ProdutosService();
    }

    public List<Produtos> ObterTodosProdutos()
    {
        return oProdutosService.oRepositoryProdutos.SelecionarTodos();
    }

    public Produtos ObterProdutosPorId(int id)
    {
        return oProdutosService.oRepositoryProdutos.SelecionarPK(id);
    }

    public void CriarProdutos(Produtos model)
    {
        oProdutosService.oRepositoryProdutos.Incluir(model);
    }

    public void EditarProdutos(Produtos model)
    {
        oProdutosService.oRepositoryProdutos.Alterar(model);
    }

    public void ExcluirProdutos(int id)
    {
        oProdutosService.oRepositoryProdutos.Excluir(id);
    }
}
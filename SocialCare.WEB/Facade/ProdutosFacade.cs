using SocialCare.DATA.Models;
using SocialCare.DATA.Services;

public class ProdutosFacade
{
    private static readonly Lazy<ProdutosFacade> instance = new Lazy<ProdutosFacade>(() => new ProdutosFacade());

    private readonly ProdutosService oProdutosService;

    private ProdutosFacade()
    {
        oProdutosService = new ProdutosService();
    }

    public static ProdutosFacade Instance => instance.Value;

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
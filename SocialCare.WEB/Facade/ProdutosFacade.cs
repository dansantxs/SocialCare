using SocialCare.DATA.Models;
using SocialCare.DATA.Repositories;

public class ProdutosFacade
{
    private static readonly Lazy<ProdutosFacade> instance = new Lazy<ProdutosFacade>(() => new ProdutosFacade());

    private RepositoryProdutos oRepositoryProdutos { get; set; }

    private ProdutosFacade()
    {
        oRepositoryProdutos = new RepositoryProdutos();
    }

    public static ProdutosFacade Instance => instance.Value;

    public List<Produtos> ObterTodosProdutos()
    {
        return oRepositoryProdutos.SelecionarTodos();
    }

    public Produtos ObterProdutosPorId(int id)
    {
        return oRepositoryProdutos.SelecionarPorId(id);
    }

    public void CriarProdutos(Produtos model)
    {
        oRepositoryProdutos.Incluir(model);
    }

    public void EditarProdutos(Produtos model)
    {
        oRepositoryProdutos.Alterar(model);
    }

    public void ExcluirProdutos(int id)
    {
        oRepositoryProdutos.Excluir(id);
    }
}
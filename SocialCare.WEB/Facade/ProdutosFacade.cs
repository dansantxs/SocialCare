using SocialCare.DATA.Models;

public class ProdutosFacade
{
    private static readonly Lazy<ProdutosFacade> instance = new Lazy<ProdutosFacade>(() => new ProdutosFacade());

    private ProdutosDAO oProdutosDAO { get; set; }

    private ProdutosFacade()
    {
        oProdutosDAO = new ProdutosDAO();
    }

    public static ProdutosFacade Instance => instance.Value;

    public List<Produtos> ObterTodosProdutos()
    {
        return oProdutosDAO.SelecionarTodos();
    }

    public Produtos ObterProdutosPorId(int id)
    {
        return oProdutosDAO.SelecionarPorId(id);
    }

    public void CriarProdutos(Produtos model)
    {
        oProdutosDAO.Incluir(model);
    }

    public void EditarProdutos(Produtos model)
    {
        oProdutosDAO.Alterar(model);
    }

    public void ExcluirProdutos(int id)
    {
        oProdutosDAO.Excluir(id);
    }
}
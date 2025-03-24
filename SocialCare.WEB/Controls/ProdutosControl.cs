using SocialCare.DATA.Models;

public class ProdutosControl
{
    private static readonly Lazy<ProdutosControl> instance = new Lazy<ProdutosControl>(() => new ProdutosControl());

    private ProdutosDAO oProdutosDAO { get; set; }

    private ProdutosControl()
    {
        oProdutosDAO = new ProdutosDAO();
    }

    public static ProdutosControl Instance => instance.Value;

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
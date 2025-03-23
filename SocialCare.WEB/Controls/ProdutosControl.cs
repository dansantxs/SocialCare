using SocialCare.DATA.Models;
using SocialCare.DATA.Repositories;

public class ProdutosControl
{
    private static readonly Lazy<ProdutosControl> instance = new Lazy<ProdutosControl>(() => new ProdutosControl());

    private RepositoryProdutos oRepositoryProdutos { get; set; }

    private ProdutosControl()
    {
        oRepositoryProdutos = new RepositoryProdutos("Data Source=DANIEL;Initial Catalog=SocialCare;Persist Security Info=True;User ID=sa;Password=1928;Encrypt=True;TrustServerCertificate=True");
    }

    public static ProdutosControl Instance => instance.Value;

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
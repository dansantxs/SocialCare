using SocialCare.DATA.Models;
using SocialCare.DATA.Services;

public class ParametrizacaoFacade
{
    private readonly ParametrizacaoService oParametrizacaoService;

    public ParametrizacaoFacade()
    {
        oParametrizacaoService = new ParametrizacaoService();
    }

    public List<Parametrizacao> ObterTodasParametrizacoes()
    {
        return oParametrizacaoService.oRepositoryParametrizacao.SelecionarTodos();
    }

    public Parametrizacao ObterParametrizacaoPorId(int id)
    {
        return oParametrizacaoService.oRepositoryParametrizacao.SelecionarPK(id);
    }

    public void CriarParametrizacao(Parametrizacao model)
    {
        oParametrizacaoService.oRepositoryParametrizacao.Incluir(model);
    }

    public void EditarParametrizacao(Parametrizacao model)
    {
        oParametrizacaoService.oRepositoryParametrizacao.Alterar(model);
    }

    public void ExcluirParametrizacao(int id)
    {
        oParametrizacaoService.oRepositoryParametrizacao.Excluir(id);
    }
}
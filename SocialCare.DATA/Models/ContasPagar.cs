using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialCare.DATA.Models;

public class ContasPagar
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idPessoa")]
    public int IdPessoa { get; set; }

    [Column("idCompra")]
    public int? IdCompra { get; set; }

    [Column("data", TypeName = "datetime")]
    public DateTime Data { get; set; }

    [Column("valor", TypeName = "decimal(10, 2)")]
    public decimal Valor { get; set; }

    [Column("dataVencimento", TypeName = "datetime")]
    public DateTime DataVencimento { get; set; }

    [Column("dataPagamento", TypeName = "datetime")]
    public DateTime? DataPagamento { get; set; }

    [ForeignKey("IdPessoa")]
    [InverseProperty("ContasPagar")]
    public virtual Pessoas Pessoa { get; set; }

    public List<ContasPagar> SelecionarTodos(DBConnection _dbConnection)
    {
        ContasPagarDAO dao = new ContasPagarDAO();
        return dao.SelecionarTodos(_dbConnection);
    }

    public ContasPagar SelecionarPorId(int id, DBConnection _dbConnection)
    {
        ContasPagarDAO dao = new ContasPagarDAO();
        return dao.SelecionarPorId(id, _dbConnection);
    }

    public ContasPagar SelecionarPorIdCompra(int id, DBConnection _dbConnection)
    {
        ContasPagarDAO dao = new ContasPagarDAO();
        return dao.SelecionarPorIdCompra(id, _dbConnection);
    }

    public void Incluir(DBConnection _dbConnection)
    {
        ValidarDados();

        ContasPagarDAO dao = new ContasPagarDAO();
        dao.Incluir(this, _dbConnection);
    }

    public void Alterar(DBConnection _dbConnection)
    {
        ValidarDados();

        ContasPagarDAO dao = new ContasPagarDAO();
        dao.Alterar(this, _dbConnection);
    }

    public void Excluir(DBConnection _dbConnection)
    {
        ContasPagarDAO dao = new ContasPagarDAO();
        dao.Excluir(this.Id, _dbConnection);
    }

    private void ValidarDados()
    {
        if (IdPessoa <= 0)
        {
            throw new ValidationException("Pessoa não informada.");
        }

        if (Data == default)
        {
            throw new ValidationException("Data não informada.");
        }

        if (DataVencimento == default)
        {
            throw new ValidationException("Data de vencimento não informada.");
        }

        if (Valor <= 0)
        {
            throw new ValidationException("Valor inválido.");
        }
    }
}
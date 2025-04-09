using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialCare.DATA.Models;

public class ItensCompra
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idCompra")]
    public int IdCompra { get; set; }

    [Column("idProduto")]
    public int IdProduto { get; set; }

    [Column("quantidade")]
    public int Quantidade { get; set; }

    [Column("precoUnitario", TypeName = "decimal(10, 2)")]
    public decimal PrecoUnitario { get; set; }

    [Column("subtotal", TypeName = "decimal(10, 2)")]
    public decimal? Subtotal { get; set; }

    [ForeignKey("IdCompra")]
    [InverseProperty("ItensCompra")]
    public virtual Compras Compra { get; set; }

    [ForeignKey("IdProduto")]
    [InverseProperty("ItensCompra")]
    public virtual Produtos Produto { get; set; }

    public List<ItensCompra> SelecionarTodos(DBConnection _dbConnection)
    {
        ItensCompraDAO dao = new ItensCompraDAO();
        return dao.SelecionarTodos(_dbConnection);
    }

    public List<ItensCompra> SelecionarPorIdCompra(int idCompra, DBConnection _dbConnection)
    {
        ItensCompraDAO dao = new ItensCompraDAO();
        return dao.SelecionarPorIdCompra(idCompra, _dbConnection);
    }

    public ItensCompra SelecionarPorId(int id, DBConnection _dbConnection)
    {
        ItensCompraDAO dao = new ItensCompraDAO();
        return dao.SelecionarPorId(id, _dbConnection);
    }

    public void Incluir(DBConnection _dbConnection)
    {
        ValidarDados();

        ItensCompraDAO dao = new ItensCompraDAO();
        dao.Incluir(this, _dbConnection);
    }

    public void Alterar(DBConnection _dbConnection)
    {
        ValidarDados();

        ItensCompraDAO dao = new ItensCompraDAO();
        dao.Alterar(this, _dbConnection);
    }

    public void Excluir(DBConnection _dbConnection)
    {
        ItensCompraDAO dao = new ItensCompraDAO();
        dao.Excluir(this.Id, _dbConnection);
    }

    private void ValidarDados()
    {
        if (IdCompra <= 0)
        {
            throw new ValidationException("Compra não informada.");
        }

        if (IdProduto <= 0)
        {
            throw new ValidationException("Produto não informado.");
        }

        if (Quantidade <= 0)
        {
            throw new ValidationException("Quantidade inválida.");
        }

        if (PrecoUnitario <= 0)
        {
            throw new ValidationException("Preço unitário inválido.");
        }
    }
}
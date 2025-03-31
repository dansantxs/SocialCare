using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialCare.DATA.Models;

public partial class Compras
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idPessoa")]
    public int IdPessoa { get; set; }

    [Column("dataCompra", TypeName = "datetime")]
    public DateTime DataCompra { get; set; }

    [Column("total", TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }

    [ForeignKey("IdPessoa")]
    [InverseProperty("Compras")]
    public virtual Pessoas Pessoa { get; set; }

    [InverseProperty("IdCompraNavigation")]
    public virtual ICollection<ItensCompra> ItensCompra { get; set; } = new List<ItensCompra>();

    public List<Compras> SelecionarTodos(DBConnection _dbConnection)
    {
        ComprasDAO dao = new ComprasDAO();
        return dao.SelecionarTodos(_dbConnection);
    }

    public Compras SelecionarPorId(int id, DBConnection _dbConnection)
    {
        ComprasDAO dao = new ComprasDAO();
        return dao.SelecionarPorId(id, _dbConnection);
    }

    public void Incluir(DBConnection _dbConnection)
    {
        ComprasDAO dao = new ComprasDAO();
        dao.Incluir(this, _dbConnection);
    }

    public void Alterar(DBConnection _dbConnection)
    {
        ComprasDAO dao = new ComprasDAO();
        dao.Alterar(this, _dbConnection);
    }

    public void Excluir(DBConnection _dbConnection)
    {
        ComprasDAO dao = new ComprasDAO();
        dao.Excluir(this.Id, _dbConnection);
    }
}

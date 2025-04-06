using SocialCare.DATA.Models;

namespace SocialCare.DATA.Observer
{
    public interface IObservador
    {
        void Update(ItensCompra item, DBConnection _dbConnection);
    }
}
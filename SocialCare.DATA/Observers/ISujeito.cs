using SocialCare.DATA.Models;
using System.Collections.Generic;

namespace SocialCare.DATA.Observer
{
    public interface ISujeito
    {
        void AdicionarObservador(IObservador observador);
        void RemoverObservador(IObservador observador);
        void NotificarObservadores(ItensCompra item, DBConnection _dbConnection);
    }
}
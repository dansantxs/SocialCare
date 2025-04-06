using SocialCare.DATA.Models;

namespace SocialCare.DATA.Observer
{
    public class ComprasSujeito : ISujeito
    {
        private readonly List<IObservador> _observadores = new List<IObservador>();

        public void AdicionarObservador(IObservador observador)
        {
            _observadores.Add(observador);
        }

        public void RemoverObservador(IObservador observador)
        {
            _observadores.Remove(observador);
        }

        public void NotificarObservadores(ItensCompra item, DBConnection _dbConnection)
        {
            foreach (var observador in _observadores)
            {
                observador.Update(item, _dbConnection);
            }
        }
    }
}
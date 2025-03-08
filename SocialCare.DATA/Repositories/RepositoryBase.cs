using Microsoft.EntityFrameworkCore;
using SocialCare.DATA.Interfaces;
using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Repositories
{
    public class RepositoryBase<T> : IRepositoryModel<T>, IDisposable where T : class
    {
        protected SocialCareContext _Contexto;
        public bool _SaveChanges = true;

        public RepositoryBase(bool saveChanges = true)
        {
            _SaveChanges = saveChanges;
            _Contexto = new SocialCareContext();
        }

        public List<T> SelecionarTodos()
        {
            return _Contexto.Set<T>().ToList();
        }

        public T SelecionarPK(params object[] variavel)
        {
            return _Contexto.Set<T>().Find(variavel);
        }

        public T Incluir(T objeto)
        {
            _Contexto.Set<T>().Add(objeto);

            if (_SaveChanges)
            {
                _Contexto.SaveChanges();
            }

            return objeto;
        }

        public T Alterar(T objeto)
        {
            _Contexto.Entry(objeto).State = EntityState.Modified;

            if (_SaveChanges)
            {
                _Contexto.SaveChanges();
            }

            return objeto;
        }

        public void Excluir(T objeto)
        {
            _Contexto.Set<T>().Remove(objeto);

            if (_SaveChanges)
            {
                _Contexto.SaveChanges();
            }
        }

        public void Excluir(params object[] variavel)
        {
            var obj = SelecionarPK(variavel);
            Excluir(obj);
        }

        public void SaveChanges()
        {
            _Contexto.SaveChanges();
        }

        public void Dispose()
        {
            _Contexto.Dispose();
        }
    }
}
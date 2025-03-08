using SocialCare.DATA.Interfaces;
using SocialCare.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCare.DATA.Repositories
{
    namespace SocialCare.DATA.Repositories
    {
        public class RepositoryVwCompras : RepositoryBase<VwCompras>, IRepositoryVwCompras
        {
            public RepositoryVwCompras(bool SaveChanges = true) : base(SaveChanges)
            {

            }
        }
    }
}
using App.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string Name { get; set; }
        public ICollection<Portfolio> Portfolios { get; set; }
    }
}

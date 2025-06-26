using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Entities.Utils
{
    public class RequerirAlMenosUnElementoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var lista = value as IEnumerable<int>;
            return lista != null && lista.Any();
        }
       
    }
}

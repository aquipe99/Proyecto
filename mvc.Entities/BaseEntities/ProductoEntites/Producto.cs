﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.Entities.BaseEntities.ProductoEntites
{
    public partial class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }=string.Empty;
        public decimal Precio { get; set; }
    }
}

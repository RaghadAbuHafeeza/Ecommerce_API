using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }


        [ForeignKey(nameof(Orders))]
        public int Order_Id { get; set; }

        public virtual Orders? Order { get; set; }


        [ForeignKey(nameof(Products))]
        public int Product_Id { get; set; }
        public virtual Products? Product { get; set; }


    }
}

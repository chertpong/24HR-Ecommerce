using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
  public class Payment
    {
        [ForeignKey("Order")]
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool Paid { get; set; }
        public string Attachment { get; set; }
        public virtual Order Order { get; set; }
    }

 
}

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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool Paid { get; set; }
        public string Attachment { get; set; }
    }

    public enum PaymentMethod
    {
        PAYPAL,
        BANK_TRANSFER
    }
}

using System.ComponentModel.DataAnnotations;
using Model.Entity;

namespace Model.Dto
{
    public class CheckOutViewModel
    {
        public string Note { get; set; }
        public string Attachment { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public TransportationType TransportationType { get; set; }
    }
}
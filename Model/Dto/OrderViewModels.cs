using System.ComponentModel.DataAnnotations;

namespace Model.Dto
{
    public class OrderViewModels
    {
        public class PaymentSlip
        {
            [Required]
            [DataType(DataType.Upload)]
            public string ImageUrl { get; set; }

            [Required]
            public int PaymentId { get; set; }
        } 
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Note { get; set; }
        public Payment Payment { get; set; }
        public OderStatus Status { get; set; }
        public TransportationType TransportationType { get; set; }
        public string UserId { get; set; }

    }

    public enum OderStatus
    {
        PEDING,
        CONFIRMED,
        PAID,
        SENT

    }

    public enum TransportationType
    {
        THAILAND_POST,
        KERRY_EXPRESS
    }
}

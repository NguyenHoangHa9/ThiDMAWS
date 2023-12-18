using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineOrderingSystem.Models
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        public int ItemQty { get; set; }
        public DateTime OrderDelivery { get; set; }
        public string OrderAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}

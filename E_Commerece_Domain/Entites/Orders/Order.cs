using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Domain.Entites.Orders
{
    public class Order : BaseEntity<Guid>
    {

        public DateTime OrderDate { get;set; }
        public string Email { get; set; } = default!;
        public OrderAddress Address { get; set; } = default!; // Owned Entity Not REaltion
        public decimal SubTotal { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];
        public OrderPaymentStatus PaymentStatu { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }

        //Payment
        public string PaymentIntentId { get; set; } = default!;
    }

    public enum OrderPaymentStatus
    {
        Pending =0,
        Faild =1,
        Success=2,
    }
}

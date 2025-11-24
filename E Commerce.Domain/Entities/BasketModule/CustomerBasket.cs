using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } //GUID Created By FrontEnd

        public ICollection<BasketItem> Items { get; set; } = [];
    }
}

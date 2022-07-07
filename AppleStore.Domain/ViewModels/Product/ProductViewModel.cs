using System.Collections.Generic;

namespace AppleStore.Domain.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string Image { get; set; }

        public List<string> Names { get; set; }
    }
}

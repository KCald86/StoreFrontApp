using StoreFrontApp.DATA.EF.Models;//accessing Product

namespace StoreFrontApp.UI.MVC.Models
{
    public class CartItemViewModel
    {
        //Shopping Cart - Step 2
        //right clicked Models -> Add Class to create this file
        public int Qty { get; set; }
        public Product CartProd { get; set; }// Containment - Use of a complex datatype as a property/field in a class
        //complex data types are classes that can hold multiple values
        //vs primitive data types, which hold a single value

        //Added to track the inventory (checkboxes) when customizing the product for the cart
        // - this will be used later when submitting an order and updating the inventory table
        public List<int> InventoryIds { get; set; }

        public CartItemViewModel() { }

        public CartItemViewModel(int qty, Product product)
        {
            Qty = qty;
            CartProd = product;
            InventoryIds = new List<int>();
        }
        //CartItemViewModel civm = new CartItemViewModel(1, new Product() { });
    }
}

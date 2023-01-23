namespace Restaurant_pos_program
{
    public class Product
    {
        public Int64 id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal tax { get; set; }

        public Product(Int64 id, string name, string description, decimal price, decimal tax)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
            this.tax = tax;
        }

        public decimal GetPrice()
        {
            // Gets price including tax
            return this.price;
        }

        public decimal GetTaxAmount()
        {
            // Calculates price of product without tax [priceWithTax - (priceWithTax*1/taxRate) ]
            return this.price - this.price * (1 / (1.0m + this.tax));
        }

        public string GetStringPrice()
        {
            // Returns a string with the correct format of the price eg 250.00 SEK
            return string.Format("{0} SEK", GetPrice());
        }

    }

}

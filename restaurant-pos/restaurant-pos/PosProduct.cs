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
            // Calculates price of product with tax eg (100 * 1.25 -> 125 SEK)
            return this.price * (1.0m + this.tax);
        }

        public decimal GetPriceNoTax()
        {
            // Calculates price of product without tax
            return this.price;
        }

        public string GetStringPrice()
        {
            // Returns a string with the correct format of the price eg 250.00 SEK
            return string.Format("{0} SEK", GetPrice());
        }

    }

}

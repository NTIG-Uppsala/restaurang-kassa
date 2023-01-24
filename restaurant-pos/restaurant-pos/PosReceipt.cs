namespace Restaurant_pos_program
{
    public class Receipt
    {
        private long epochTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        private DateTime currentDateTime = DateTime.Now;
        private List<string> receipt = new List<string>();

        // Path related variables
        public string username { get; set; }
        public string path { get; set; }
        public string filename { get; set; }
        public string fullpath { get; set; }

        public Receipt()
        {
            this.username = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split("\\")[1];
            this.path = string.Format(@"C:\Users\{0}\Documents\restaurant-receipts", username);
            this.filename = string.Format(@"receipt_{0}.txt", epochTime.ToString());
            this.fullpath = path + @"\" + filename;

            // Create directory if it doesn't exist when creating new object instance
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }



        public List<string> CreateReceipt(Cart cart)
        {
            decimal vat25 = 0m;
            decimal vat12 = 0m;
            decimal vat0 = 0m;

            foreach (Product product in cart.GetCart())
            {
                switch (product.tax)
                {
                    case 0.25m:
                        vat25 += product.GetTaxAmount(); // 1.25m;
                        break;
                    case 0.12m:
                        vat12 += product.GetTaxAmount(); // 1.12m;
                        break;
                    default:
                        vat0 += product.GetTaxAmount();
                        break;
                }
            }

            decimal netPrice = vat25 + vat12 + vat0;
            string seller = "Bengan";

            /*
                Contact information and receipt information
             */
            receipt.Add("Bengans Burgeria");
            receipt.Add("Fjällgatan 32H");
            receipt.Add("981 39 Jönköping\n");
            receipt.Add("Tel: (+46)63-055 55 55");
            receipt.Add("Mail: info.bengans@gmail.com");
            receipt.Add("Org. Nr.: 234567-8901\n");
            receipt.Add($"{currentDateTime.ToString("s").Replace("T", " ")}");
            receipt.Add($"Seller: {seller}");
            receipt.Add($"Receipt Nr.: {epochTime}");
            receipt.Add("-----------------------------------------------------\n");

            /*
                Product information and price
             */
            foreach (Product product in cart.GetCart())
            {
                receipt.Add("\n-----------------------------------------------------\n");
                receipt.Add("\t1x " + product.name + " " + product.GetStringPrice() + " (with " + product.tax * 100 + "% tax)"); ;
            }
            receipt.Add("\n-----------------------------------------------------\n");

            /*
               Vat basis and total
             */
            receipt.Add("-----------------------------------------------------\n");
            receipt.Add("VAT basis:");
            receipt.Add($"VAT 25%\t{vat25.ToString("0.00")} SEK");
            receipt.Add($"VAT 12%\t{vat12.ToString("0.00")} SEK");
            receipt.Add($"No VAT\t{vat0.ToString("0.00")} SEK\n");
            receipt.Add($"VAT total\t{netPrice.ToString("0.00")} SEK\n");
            receipt.Add("-----------------------------------------------------\n");
            receipt.Add($"Total:\t\t{cart.GetTotalPrice().ToString("0.00")} SEK\n");
            receipt.Add("-----------------------------------------------------\n");

            // Write Receipt to file
            SaveReceiptToFile();
            return receipt;
        }

        void SaveReceiptToFile()
        {
            // Open ReadWrite Stream
            using (StreamWriter fs = File.CreateText(fullpath))
            {
                // Loop over recipet stringparts and write to file
                foreach (string stringPart in receipt)
                {
                    fs.WriteLine(stringPart);
                }

                // Close Stream
                fs.Close();
            }
        }
    }

}
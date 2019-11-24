namespace Spektro_API_Azure.Model
{
    public class ProductModel
    {
        private int id;
        private string productType;
        private string prodcutName;
        private string imgSrc;
        private double price;
        private string ingredients;
        private string allergens;

        public ProductModel() { }

        public ProductModel(int id, string productType, string prodcutName, string imgSrc, double price, string ingredients, string allergens)
        {
            this.id = id;
            this.productType = productType;
            this.prodcutName = prodcutName;
            this.imgSrc = imgSrc;
            this.price = price;
            this.ingredients = ingredients;
            this.allergens = allergens;
        }

        public int Id { get => id; set => id = value; }
        public string ProductType { get => productType; set => productType = value; }
        public string ProdcutName { get => prodcutName; set => prodcutName = value; }
        public string ImgSrc { get => imgSrc; set => imgSrc = value; }
        public double Price { get => price; set => price = value; }
        public string Ingredients { get => ingredients; set => ingredients = value; }
        public string Allergens { get => allergens; set => allergens = value; }
    }
}

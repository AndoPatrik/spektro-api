using System.Collections;
using System.Collections.Generic;

namespace spektro_api.Model
{
    public class Product : IEnumerable
    {
        private int id;
        private string name;
        private string imgSource;
        private int price;
        private IEnumerable<List<IngredientsType>> ingredients;
        private string[] allergens;

        public Product(int id, string name, string imgSource, int price, IEnumerable<List<IngredientsType>> ingredients)
        {
            this.id = id;
            this.name = name;
            this.imgSource = imgSource;
            this.price = price;
            this.ingredients = ingredients;
        }

        public Product(int id, string name, string imgSource, int price, IEnumerable<List<IngredientsType>> ingredients, string[] allergens)
        {
            this.id = id;
            this.name = name;
            this.imgSource = imgSource;
            this.price = price;
            this.ingredients = ingredients;
            this.allergens = allergens;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string ImgSource { get => imgSource; set => imgSource = value; }
        public int Price { get => price; set => price = value; }
        public IEnumerable<List<IngredientsType>> Ingredients { get => ingredients; set => ingredients = value; }
        public string[] Allergens1 { get => allergens; set => allergens = value; }

        public IEnumerator GetEnumerator()
        {
            return Ingredients.GetEnumerator();
        }
    }
}

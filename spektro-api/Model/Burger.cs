using System.Collections;
using System.Collections.Generic;

namespace spektro_api.Model
{
    public class Burger : Product
    {
        public Burger(int id, string name, string imgSource, int price, IEnumerable<List<IngredientsType>> ingredients) : base(id, name, imgSource, price, ingredients)
        {
        }
    }
}

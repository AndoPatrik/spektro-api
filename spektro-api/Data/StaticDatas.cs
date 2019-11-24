using spektro_api.Model;
using System.Collections.Generic;

namespace spektro_api.Data
{
    public class StaticDatas
    {

        static IngredientsType it = new IngredientsType("name", 3.3);
        //static IEnumerable<List<IngredientsType>> itL = new IEnumerable<List<IngredientsType>> { };
        string[] sLs = new string[] { "S", "l" };
        //Burger b = new Burger(1, "name", "source", 100, itL);
        

        static public List<Burger> burgers = new List<Burger>
        {
            new Burger(1, "name", "source", 100, []),
    };

        
    }
}

namespace spektro_api.Model
{
    public class IngredientsType
    {
        private string name;
        private double amount;

        public IngredientsType(string name, double amount)
        {
            this.name = name;
            this.amount = amount;
        }

        public string Name { get => name; set => name = value; }
        public double Amount { get => amount; set => amount = value; }
    }
}

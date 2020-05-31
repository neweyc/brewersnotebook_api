namespace Beer.Core.Entities
{
    public class FermentableAddition : Entity
    {
        public Fermentable Fermentable { get; set; }
        public double Amount { get; set; }
    }
}

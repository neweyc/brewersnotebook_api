namespace Beer.Core.Entities
{
    public class HopAddition: Entity
    {
        public Hop Hop { get; set; }
        public double Amount { get; set; }
        public double Time { get; set; }
    }
}

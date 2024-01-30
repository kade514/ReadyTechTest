namespace RTTest.Models
{
    public class CoffeeItem
    {
        public long Id { get; set; }
        public Guid BrewMachineGuid { get; set; }
        public ECoffeeType CoffeeType { get; set; }
        public DateTime BrewTime { get; set; }
    }
}

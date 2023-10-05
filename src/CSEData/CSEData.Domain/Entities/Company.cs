namespace CSEData.Domain.Entities
{
    public class Company : IEntity<int>
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }

        public List<Price> Prices { get; set; }
    }
}

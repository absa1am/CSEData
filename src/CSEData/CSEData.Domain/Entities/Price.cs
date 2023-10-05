namespace CSEData.Domain.Entities
{
    public class Price : IEntity<int>
    {
        public int Id { get; set; }
        public string PriceLTP { get; set; }
        public string Volume { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public DateTime Time { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

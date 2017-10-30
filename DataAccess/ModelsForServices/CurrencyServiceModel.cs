namespace DataAccess.ModelsForServices
{
    public class CurrencyServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CurrencyServiceModel Copy()
        {
            return new CurrencyServiceModel
            {
                Id = Id,
                Name = Name
            };
        }
    }
}

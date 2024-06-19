namespace MarketAssets.Domain.Models
{
    public class AssetProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AssetProvider(string name)
        {
            this.Name = name;
        }
    }
}

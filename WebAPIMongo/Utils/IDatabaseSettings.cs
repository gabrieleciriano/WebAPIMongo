namespace WebAPIMongo.Utils
{
    public interface IDatabaseSettings
    {
        string ClientCollectionName { get; set; } //colection do mongo
        string AddressCollectionName { get; set; }
        string ConnectionString { get; set; }    //conexão da colletion
        string DatabaseName { get; set; } //onde está a collection 
    }
}

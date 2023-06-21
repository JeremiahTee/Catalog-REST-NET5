namespace Catalog.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString 
        { 
            get // opening up the getter makes it read-only
            {
                return $"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }
}
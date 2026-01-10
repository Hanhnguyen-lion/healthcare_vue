namespace medicalcare_mongodb.models
{
    public class MongoDBSettings
    {
        public string? AtlasURI{get;set;}
        public string? DatabaseName{get;set;}

        public int? ExpireDay{get;set;}
    }
}
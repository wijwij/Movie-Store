namespace MovieStore.Core.Entities
{
    /*
     * Genre class represents our Domain Model, all properties in Genre Table
     */
    public class Genre
    {
        // By convention, EF is gonna consider any property in the entity class as Primary key
        public int  Id { get; set; }
        public string Name { get; set; }
    }
}
namespace Entities.DataTransferObject
{
    //  [Serializable]
    public record BookDto  //(int Id, string Title, decimal Price);


    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
         
    }
     
     
}

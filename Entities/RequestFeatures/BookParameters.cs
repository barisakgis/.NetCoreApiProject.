namespace Entities.RequestFeatures
{
    public class BookParameters : RequestParameters
	{
        public uint MinPrice { get; set; }
        public uint MaxPrice { get; set; } = 1000;   // uint.MaxValue;

        public bool ValidPriceRange => MaxPrice > MinPrice;  // readonly ifade olarak tanımladık


        public string? SearchTerm { get; set; }  // bunu  RequestParameters tanımlayıp başka şeyler içinde arama yapmasını sağlayabilirdik biz burada sadece book için yapıyoruz 


        public BookParameters()
        {
            OrderBy = "id";
        }


    }
}

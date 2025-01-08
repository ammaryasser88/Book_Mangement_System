namespace BookMangementSystemApi.Dtos.Request
{
    public class BookRequest
    {
        public string Title { get; set; }
        public int AutherID { get; set; }
        public IFormFile? Image { get; set; }

    }
}

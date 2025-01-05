namespace BookMangementSystemApi.Dtos.Request
{
    public class BookRequest
    {
        public string Title { get; set; }
        public string AutherName { get; set; }
        public IFormFile? Image { get; set; }

    }
}

using BookMangementSystemApi.Models;

namespace BookMangementSystemApi.Dtos.Response
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Auther Auther { get; set; }
        public DateTime InsertDate { get; set; }
        public string Status { get; set; }
        public string  ImagePath {  get; set; }


    }
}

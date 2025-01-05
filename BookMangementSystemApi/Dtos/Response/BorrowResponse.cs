using BookMangementSystemApi.Models;

namespace BookMangementSystemApi.Dtos.Response
{
    public class BorrowResponse
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnedDate { get; set; }
        public DateTime? AcuatalDate { get; set; }

        public int BookId { get; set; }

        public int ReaderId { get; set; }
    }
}

namespace BookMangementSystemApi.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnedDate { get; set; }
        public DateTime? AcuatalDate { get; set; }

       
        public int BookId { get; set; }
        public Book Book { get; set; }

       
        public int ReaderId { get; set; }
        public Reader Reader { get; set; }
    }

}

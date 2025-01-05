namespace BookMangementSystemApi.Dtos.Request
{
    public class BorrowRequest
    {
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public int BorrowDayes { get; set; }
    }
}

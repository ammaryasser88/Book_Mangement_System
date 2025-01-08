namespace BookMangementSystemApi.Models
{
    public class Book
    {
        public int  Id  { get; set; }
        public string ImagePath { get; set; }
        public string Title {  get; set; }
        public int AutherID { get; set; }
        public Auther Auther { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public string  Status { get; set; } = "Exist";

    }
}

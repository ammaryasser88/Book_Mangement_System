namespace BookMangementSystemApi.Models
{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Ssn { get; set; }
       
    }
}

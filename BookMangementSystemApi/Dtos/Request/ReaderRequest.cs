namespace BookMangementSystemApi.Dtos.Request
{
    public class ReaderRequest
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Ssn { get; set; }
    }
}

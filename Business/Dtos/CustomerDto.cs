namespace Business.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string CustomerType { get; set; } = "Alıcı";

        public string RoomPreference { get; set; } = string.Empty;

        public string? Note { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

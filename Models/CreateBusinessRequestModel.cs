namespace HalloDoc.Models
{
    public class CreateBusinessRequestModel
    {
        public string Id { get; set; }
        public string BUS_FirstName { get; set; }
        public string BUS_LastName { get; set; }
        public string BUS_Email { get; set; }
        public string BUS_PhoneNumber { get; set; }
        public string BUS_Property { get; set; }
        public string BUS_CaseNumber { get; set; }
        public string Symptoms { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string RoomSuite { get; set; }
    }
}

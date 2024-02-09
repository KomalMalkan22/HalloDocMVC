namespace HalloDoc.Models
{
    public class CreateFamilyFriendRequestModel
    {
        public string FF_FirstName { get; set; }
        public string FF_LastName { get; set;}
        public string FF_PhoneNumber { get; set;}
        public string FF_Email { get; set;}
        public string FF_RelationWithPatients { get; set;}
        public string ID { get; set;}
        public string Symptoms { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public DateTime DateOfBirth { get; set;}
        public string Email { get; set;}
        public string PhoneNumber { get; set;}
        public string Street { get; set;}
        public string City { get; set;}
        public string State { get; set;}
        public string ZipCode { get; set;}
        public string RoomSuite { get; set;}
    }
}

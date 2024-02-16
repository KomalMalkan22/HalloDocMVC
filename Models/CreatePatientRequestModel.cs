using System.ComponentModel.DataAnnotations;

namespace HalloDoc.Models
{
    public class CreatePatientRequestModel
    {
        public string Id { get; set; }
        public string Symptoms { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        [Compare("PassWord", ErrorMessage = "Password and confirm password must match")]
        public string ConfirmPassword { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string FF_RelationWithPatient { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string RoomSuite { get; set; }
        public string? UploadImage { get; set; }
        public IFormFile? UploadFile { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace HalloDoc.Models
{
    public class CreatePatientRequestModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Symptoms is required")]
        public string Symptoms { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        [Compare("PassWord", ErrorMessage = "Password and confirm password must match")]
        public string ConfirmPassword { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Contact number is required")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string FF_RelationWithPatient { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; }
        public string RoomSuite { get; set; }
        public string? UploadImage { get; set; }
        public IFormFile? UploadFile { get; set; }
    }
}

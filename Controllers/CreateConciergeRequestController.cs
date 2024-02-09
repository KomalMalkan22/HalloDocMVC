﻿using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace HalloDoc.Controllers
{
    public class CreateConciergeRequestController : Controller
    {
        private readonly HalloDocContext _context;

        public CreateConciergeRequestController(HalloDocContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("../Request/CreateConceirgeRequest");
        }
        public async Task<IActionResult> Create(CreateConciergeRequestModel createConciergeRequest)
        {
            var concierge = new Concierge
            {
                Conciergename = createConciergeRequest.C_FirstName + " " + createConciergeRequest.C_LastName,
                Street = createConciergeRequest.C_Street,
                City = createConciergeRequest.C_City,
                State = createConciergeRequest.C_State,
                Zipcode = createConciergeRequest.C_ZipCode,
                Createddate = DateTime.Now
            };
            _context.Concierges.Add(concierge);
            await _context.SaveChangesAsync();

            var request = new Request
            {
                Requesttypeid = 4,
                Status = 1,
                Firstname = createConciergeRequest.C_FirstName,
                Lastname = createConciergeRequest.C_LastName,
                Phonenumber = createConciergeRequest.C_PhoneNumber,
                Email = createConciergeRequest.C_Email,
                Createddate = DateTime.Now,
                Isurgentemailsent = new BitArray(1)
            };
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var requestClient = new Requestclient
            {
                Request = request,
                Requestid = request.Requestid,
                Notes = createConciergeRequest.Symptoms,
                Firstname = createConciergeRequest.FirstName,
                Lastname = createConciergeRequest.LastName,
                Strmonth = createConciergeRequest.DateOfBirth.ToString("MMMM"),
                Intdate = createConciergeRequest.DateOfBirth.Day,
                Intyear = createConciergeRequest.DateOfBirth.Year,
                Email = createConciergeRequest.Email,
                Phonenumber = createConciergeRequest.PhoneNumber,
                Location = createConciergeRequest.RoomSuite
            };
            _context.Requestclients.Add(requestClient);
            await _context.SaveChangesAsync();

            return View("../Request/SubmitRequestScreen");
        }
    }
}

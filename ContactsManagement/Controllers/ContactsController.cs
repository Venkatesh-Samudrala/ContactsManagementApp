using ContactsManagement.Model;
using ContactsManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContactsManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactService;

        public ContactsController(IContactsService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContacts()
        {
            var contacts = _contactService.GetContacts();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> GetContactById(int id)
        {
            var contact = _contactService.GetContactById(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] Contact newContact)
        {
            _contactService.AddContact(newContact);
            return CreatedAtAction(nameof(GetContactById), new { id = newContact.Id }, newContact);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] Contact updatedContact)
        {
            _contactService.UpdateContact(id, updatedContact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            _contactService.DeleteContact(id);
            return NoContent();
        }
    }
}

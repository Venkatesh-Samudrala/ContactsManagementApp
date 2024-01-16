using ContactsManagement.Model;
using System.Collections.Generic;

namespace ContactsManagement.Services
{
    public interface IContactsService
    {

        List<Contact> GetContacts();
        Contact GetContactById(int id);
        void AddContact(Contact newContact);
        void UpdateContact(int id, Contact updatedContact);
        void DeleteContact(int id);

    }
}

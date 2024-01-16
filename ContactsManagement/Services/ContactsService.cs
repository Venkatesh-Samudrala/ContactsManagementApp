using ContactsManagement.Model;
using System.Text.Json;

namespace ContactsManagement.Services
{
    public class ContactsService : IContactsService
    {
        private const string JsonFilePath = "contacts.json";

        public List<Contact> GetContacts()
        {
            return ReadContactsFromJson();
        }

        public Contact GetContactById(int id)
        {
            var contacts = ReadContactsFromJson();
            return contacts.FirstOrDefault(c => c.Id == id);
        }

        public void AddContact(Contact newContact)
        {
            var contacts = ReadContactsFromJson();
            newContact.Id = contacts.Count + 1;
            contacts.Add(newContact);
            WriteContactsToJson(contacts);
        }

        public void UpdateContact(int id, Contact updatedContact)
        {
            var contacts = ReadContactsFromJson();
            var existingContact = contacts.FirstOrDefault(c => c.Id == id);

            if (existingContact != null)
            {
                existingContact.FirstName = updatedContact.FirstName;
                existingContact.LastName = updatedContact.LastName;
                existingContact.Email = updatedContact.Email;
                WriteContactsToJson(contacts);
            }
        }

        public void DeleteContact(int id)
        {
            var contacts = ReadContactsFromJson();
            var contactToRemove = contacts.FirstOrDefault(c => c.Id == id);

            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
                WriteContactsToJson(contacts);
            }
        }

        private List<Contact> ReadContactsFromJson()
        {
            if (!File.Exists(JsonFilePath))
            {
                return new List<Contact>();
            }

            var json = File.ReadAllText(JsonFilePath);
            return JsonSerializer.Deserialize<List<Contact>>(json);
        }

        private void WriteContactsToJson(List<Contact> contacts)
        {
            var json = JsonSerializer.Serialize(contacts);
            File.WriteAllText(JsonFilePath, json);
        }
    }

}



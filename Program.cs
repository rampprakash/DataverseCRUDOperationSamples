using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataverseCRUDOperationSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = DataverseService.GetService();

            // CREATE RECORDS
            Guid contactId = CreateContact(service);

            // RETRIEVE RECORD
            RetrieveContact(service, contactId);

            // RETRIEVE MULTIPLE - RETIREVE ALL CONTACT RECORDS FROM CONTACT
            RetrieveMultipleContact(service);

            // UPDATE RECORD
            UpdateContact(service, contactId);

            // DELETE RECORD
            DeleteContact(service, contactId);
        }

        private static void DeleteContact(IOrganizationService service, Guid contactId)
        {
            service.Delete("contact", contactId);
            Console.WriteLine("Contact deleted");
        }

        private static void UpdateContact(IOrganizationService service, Guid contactId)
        {
            var contact = new Entity("contact", contactId);
            contact["telephone1"] = "1234567890";

            service.Update(contact);
            Console.WriteLine("Contact updated");
        }

        private static void RetrieveContact(IOrganizationService service, Guid contactId)
        {
            var contact = service.Retrieve("contact", contactId, new ColumnSet(true));
            Console.WriteLine($"Retrieved: {contact["firstname"]} {contact["lastname"]}");
        }

        private static void RetrieveMultipleContact(IOrganizationService service)
        {
            QueryExpression getAllContact = new QueryExpression("contact");
            getAllContact.ColumnSet = new ColumnSet(true);
            getAllContact.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            EntityCollection entityCollection = service.RetrieveMultiple(getAllContact);

            Console.WriteLine($"Total Contacts: {entityCollection.Entities.Count}");
        }

        private static Guid CreateContact(IOrganizationService service)
        {
            var contact = new Entity("contact");
            contact["firstname"] = "Ram";
            contact["lastname"] = "Prakash";
            contact["emailaddress1"] = "RamPrakash@test.com";

            Guid contatId = service.Create(contact);
            Console.WriteLine($"Contact created: {contatId}");
            return contatId;
        }
    }
}

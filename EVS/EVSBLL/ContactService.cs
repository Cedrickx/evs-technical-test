using EVSBLL.BusinessObjects;
using EVSDAL;
using EVSDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSBLL
{
    public class ContactService : IContactService
    {
        private readonly EVSDbContext _context;
        private readonly ITVAService _tvaService;


        public ContactService(EVSDbContext context, ITVAService tvaService)
        {
            _context = context;
            _tvaService = tvaService;
        }


        /// <summary>
        /// Crée un nouveau contact
        /// </summary>
        /// <param name="contactBO">Contact à créer</param>
        /// <returns>Contact créé</returns>
        /// <exception cref="Exception">Si les paramètres du contact ne sont pas valides</exception>
        public ContactBO CreateContact(ContactBO contactBO)
        {
            Validate(contactBO);

            Contact contact = new Contact
            {
                Name = contactBO.Name,
                Address = contactBO.Address,
                Companies = _context.Companies.Where(x => contactBO.Companies.Contains(x.Id)).ToList(),
                ContactType = contactBO.ContactType,
                TVANumber = contactBO.TVANumber,
            };

            if (contact.Companies.Count != contactBO.Companies.Count)
                throw new Exception("Company not found");

            _context.Contacts.Add(contact);
            _context.SaveChanges();

            return new ContactBO().GetFrom(contact);
        }

        /// <summary>
        /// Supprime un contact
        /// </summary>
        /// <param name="id">Identifiant du contact à supprimer</param>
        /// <exception cref="Exception">Si aucun contact avec cet identifiant n'est pas enregistré</exception>
        public void DeleteContact(int id)
        {
            Contact? contact = _context.Contacts.Find(id);
            if (contact == null)
                throw new Exception("Contact not found");

            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }

        /// <summary>
        /// Mets à jour un contact
        /// </summary>
        /// <param name="contactBO">Contact à mettre à jour</param>
        /// <returns>Le contact mis à jour</returns>
        /// <exception cref="Exception">Si les paramètres du contact ne sont pas valides</exception>
        public ContactBO UpdateContact(ContactBO contactBO)
        {
            Validate(contactBO);

            Contact contact = new Contact
            {
                Id = contactBO.Id,
                Name = contactBO.Name,
                Address = contactBO.Address,
                Companies = _context.Companies.Where(x => contactBO.Companies.Contains(x.Id)).ToList(),
                ContactType = contactBO.ContactType,
                TVANumber = contactBO.TVANumber,
            };

            if (contact.Companies.Count != contactBO.Companies.Count)
                throw new Exception("Company not found");

            _context.Entry(contact).State = EntityState.Modified;
            _context.SaveChanges();

            return new ContactBO().GetFrom(contact);
        }

        /// <summary>
        /// Obtient un contact
        /// </summary>
        /// <param name="id">Identifiant du contact à obtenir</param>
        /// <returns>Le contact</returns>
        /// <exception cref="Exception">Si aucun contact avec cet identifiant n'est trouvé</exception>
        public ContactBO GetContact(int id)
        {
            Contact? contact = _context.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact == null)
                throw new Exception("Contact not found");

            return new ContactBO().GetFrom(contact);
        }

        /// <summary>
        /// Valide les paramètres d'un contact
        /// </summary>
        /// <param name="contactBO">Contact à valider</param>
        /// <exception cref="Exception">Si au moins un paramètre n'est pas valide</exception>
        private void Validate(ContactBO contactBO)
        {
            if (contactBO.ContactType == ContactType.Freelancer
                && !_tvaService.IsValid(contactBO.TVANumber))
            {
                throw new Exception("A freelancer must have a valid TVA Number");
            }
        }
    }
}

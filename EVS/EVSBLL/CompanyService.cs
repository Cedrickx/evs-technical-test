using EVSBLL.BusinessObjects;
using EVSDAL;
using EVSDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EVSBLL
{
    public class CompanyService : ICompanyService
    {
        private readonly EVSDbContext _context;
        private readonly ITVAService _tvaService;

        public CompanyService(EVSDbContext context, ITVAService tvaService)
        {
            _context = context;
            _tvaService = tvaService;
        }


        /// <summary>
        /// Crée une nouvelle compagnie
        /// </summary>
        /// <param name="companyBO">Compagnie à créer</param>
        /// <returns>La compagnie créée</returns>
        public CompanyBO CreateCompany(CompanyBO companyBO)
        {
            Validate(companyBO);

            Company company = companyBO.Create();

            _context.Companies.Add(company);
            _context.SaveChanges();

            return new CompanyBO().GetFrom(company);
        }

        /// <summary>
        /// Obtient la liste des compagnies actuellement enregistrées
        /// </summary>
        /// <returns>La liste des compagnies</returns>
        public List<CompanyBO> GetCompanies()
        {
            return _context.Companies.Select(x => new CompanyBO().GetFrom(x)).ToList();
        }

        /// <summary>
        /// Obtient la liste des compagnies qui correspondent à une liste d'identifiants
        /// </summary>
        /// <param name="ids">Liste des identifiants</param>
        /// <returns>La liste des compagnies</returns>
        /// <exception cref="Exception">Si le nombre de compagnies récupérées est différent du nombre demandé</exception>
        public List<CompanyBO> GetCompanies(List<int> ids)
        {
            List<CompanyBO> companies = _context.Companies
                .Where(x => ids.Contains(x.Id))
                .Select(x => new CompanyBO().GetFrom(x))
                .ToList();
            if (companies.Count != ids.Count)
                throw new Exception("Company not found");

            return companies;
        }

        /// <summary>
        /// Obtient une compagnie spécifique
        /// </summary>
        /// <param name="id">Id de la compagnie à obtenir</param>
        /// <returns>La compagnie</returns>
        /// <exception cref="Exception">Si la compagnie n'est pas enregistrée</exception>
        public CompanyBO GetCompany(int id)
        {
            Company? company = _context.Companies.FirstOrDefault(x => x.Id == id);
            if (company == null)
                throw new Exception("Company not found");

            return new CompanyBO().GetFrom(company);
        }

        /// <summary>
        /// Mets à jour une compagnie
        /// </summary>
        /// <param name="companyBO">Compagnie à mettre à jour</param>
        /// <returns>La compagnie mise à jour</returns>
        public CompanyBO UpdateCompany(CompanyBO companyBO)
        {
            Validate(companyBO);

            _context.Entry(companyBO.Create()).State = EntityState.Modified;
            _context.SaveChanges();

            return companyBO;
        }


        /// <summary>
        /// Valide les paramètres d'une compagnie
        /// </summary>
        /// <param name="companyBO">Compagnie à valider</param>
        /// <exception cref="Exception">Si au moins un paramètre n'est pas valide</exception>
        private void Validate(CompanyBO companyBO)
        {
            if (String.IsNullOrEmpty(companyBO.Name))
                throw new Exception("A company must have a name");
            if (String.IsNullOrEmpty(companyBO.HeadQuarters))
                throw new Exception("A company must have a headquarters address");
            if (!_tvaService.IsValid(companyBO.TVANumber))
                throw new Exception("A company must have a valid TVA number");
        }
    }
}

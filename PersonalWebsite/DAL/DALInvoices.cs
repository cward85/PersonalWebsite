using Microsoft.Ajax.Utilities;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace PersonalWebsite.DAL
{
    public class DALInvoices
    {
        CWARDEntities _dbContext;
        public DALInvoices()
        {
            _dbContext = new CWARDEntities();
        }

        public List<clientVM> ListClients(int userId)
        {
            return _dbContext.clients.Where(x => x.userId == userId).ToList().Select(y => new clientVM()
            {
                ClientName = y.name,
                DateLastInvoiceSent = y.invoices.Where(z => z.clientId == y.id).Max(a => a.sentDate),
                TotalDollarAmount = y.invoices.Where(z => z.clientId == y.id).Sum(a => a.invoiceEntries.Where(b => b.invoiceId == a.id).Sum(c => c.totalAmount)),
                TotalInvoices = y.invoices.Where(z => z.clientId == y.id).Count(),
                id = y.id
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetStateDDL()
        {
            return _dbContext.states
                .OrderBy(x => x.id)
                .Select(x => new System.Web.Mvc.SelectListItem
                {
                    Value = x.id.ToString(),
                    Text = x.name
                });
        }

        public IEnumerable<SelectListItem> GetCountryDDL()
        {
            return _dbContext.countries
                .OrderBy(x => x.id)
                .Select(x => new System.Web.Mvc.SelectListItem
                {
                    Value = x.id.ToString(),
                    Text = x.name
                }).OrderBy(x => x.Value != "239")
                .ThenBy(x => x.Value);
        }

        public string GetAuthor(int authorId)
        {
            return _dbContext.authors.Find(authorId).name;
        }

        public List<invoiceVM> ListInvoicesByClientId(int clientId)
        {           
            return _dbContext.invoices.Where(x => x.clientId == clientId).Select(y => new invoiceVM()
            {
                id = y.id,
                invoiceNumber = y.invoiceNumber,
                invoiceDate = y.invoiceDate,
                invoiceDueDate = y.invoiceDueDate,
                sentDate = y.sentDate,
                totalAmount = y.invoiceEntries.Where(z => z.invoiceId == y.id).Sum(z => z.totalAmount)
            }).ToList();
            
        }

        private string FormatInvoiceNumber(int id)
        {
            string stringId = id.ToString();

            while (stringId.Length < 9)
            {                
                stringId = "0" + stringId;                
            }

            stringId = stringId.Insert(3, "-");
            stringId = stringId.Insert(7, "-");

            return stringId;
        }

        public string GetClientName(int id)
        {
            return _dbContext.clients.Find(id).name;
        }

        public List<invoiceEntryVM> ListInvoiceEntriesByInvoiceId(int invoiceId)
        {
            return _dbContext.invoiceEntries.Where(x => x.invoiceId == invoiceId).Select(y => new invoiceEntryVM()
            {
                id = y.id,
                Description = y.description,
                Rate = y.rate,
                RateUnit = y.rateUnit,
                TotalAmount = y.totalAmount,
                UnitAmount = y.unitAmount
            }).ToList();
        }

        public DateTime GetInvoiceDate(int id)
        {
            return _dbContext.invoices.Find(id).invoiceDate;
        }

        public void CreateClient(clientListVM model, int authorId)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.addresses.Add(new address()
                {
                    city = model.city,
                    countryId = model.countryId,
                    postCode = model.postCode,
                    stateId = model.stateId,
                    street1 = model.street1,
                    street2 = model.street2
                });

                var addressId = _dbContext.SaveChanges();

                _dbContext.clients.Add(new client()
                {
                    addressId = addressId,
                    creationDate = DateTime.Now,
                    name = model.name,
                    userId = authorId,                    
                });

                _dbContext.SaveChanges();

                transaction.Commit();
            }
        }

        public void EditClient(clientListVM model)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var client = _dbContext.clients.Find(model.id);
                client.name = model.name;
                
                var address = _dbContext.addresses.Find(client.addressId);
                
                address.street1 = model.street1;
                address.street2 = model.street2;
                address.countryId = model.countryId;
                address.stateId = model.stateId;
                address.postCode = model.postCode;
                address.city = model.city;

                _dbContext.SaveChanges();

                transaction.Commit();
            }
        }

        public clientListVM GetClient(int id)
        {
            var client = _dbContext.clients.Find(id);

            var address = _dbContext.addresses.Find(client.addressId);

            clientListVM clientVM = new clientListVM()
            {
                city = address.city,
                stateId = address.stateId,
                countryId = address.countryId,
                name = client.name,
                postCode = address.postCode,
                street1 = address.street1,
                street2 = address.street2,
                id = id
            };

            return clientVM;
        }
    }
}
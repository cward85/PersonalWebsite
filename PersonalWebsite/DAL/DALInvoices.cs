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
                totalAmount = y.invoiceEntries.Where(z => z.invoiceId == y.id).Count() > 0 ? y.invoiceEntries.Where(z => z.invoiceId == y.id).Sum(z => z.totalAmount) : 0
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
                invoiceEntryId = y.id,
                Description = y.description,
                Rate = y.rate,
                RateUnit = y.rateUnit,
                TotalAmount = y.totalAmount,
                UnitAmount = y.unitAmount
            }).ToList();
        }
        
        public string GetInvoiceNumber(int id)
        {
            return _dbContext.invoices.Find(id).invoiceNumber;
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

        public invoiceEntryListVM GetInvoiceEntry(int id)
        {
            var invoiceEntry = _dbContext.invoiceEntries.Find(id);

            invoiceEntryListVM invoiceEntryListVM = new invoiceEntryListVM()
            {
                description = invoiceEntry.description,
                rate = invoiceEntry.rate,
                rateUnit = invoiceEntry.rateUnit,
                unitAmount = invoiceEntry.unitAmount,
                invoiceId = invoiceEntry.invoiceId,
                invoiceEntryId = invoiceEntry.id
            };

            return invoiceEntryListVM;
        }

        public void CreateInvoiceEntry(invoiceEntryListVM model, int authorId)
        {
            _dbContext.invoiceEntries.Add(new invoiceEntry()
            {
                description = model.description,
                invoiceId = model.invoiceId,
                rate = model.rate.Value,
                rateUnit = model.rateUnit,
                unitAmount = model.unitAmount.Value,                
            });

            _dbContext.SaveChanges();
        }

        public void EditInvoiceEntry(invoiceEntryListVM model)
        {
            invoiceEntry entry = _dbContext.invoiceEntries.Find(model.invoiceEntryId);

            entry.description = model.description;
            entry.rate = model.rate.Value;
            entry.rateUnit = model.rateUnit;
            entry.unitAmount = model.unitAmount.Value;

            _dbContext.SaveChanges();
        }

        public void CreateInvoice(invoiceListVM model, int authorId)
        {
            _dbContext.invoices.Add(new invoice()
            {
                invoiceDate = model.invoiceDate,
                invoiceDueDate = model.invoiceDueDate,
                clientId = model.clientId,
                invoiceNumber = CreateInvoiceNumber(_dbContext.invoices.Max(x => x.invoiceNumberId)),
                invoiceNumberId = CreateInvoiceNumberId(_dbContext.invoices.Max(x => x.invoiceNumberId))
            });

            _dbContext.SaveChanges();
        }

        public void EditInvoice(invoiceListVM model)
        {
            invoice entry = _dbContext.invoices.Find(model.invoiceId);

            entry.invoiceDate = model.invoiceDate;
            entry.invoiceDueDate = model.invoiceDueDate;

            _dbContext.SaveChanges();
        }

        public invoicePDFVM GetInvoicePDF(int id)
        {
            invoicePDFVM model = new invoicePDFVM()
            {
                customerAddress = "PO BOX 667 Clarion, PA 16214",
                customerName = "Alfred H. and Janet L. Lander Family Charitable Foundation",
                invoiceDate = "September 1, 2026",
                invoiceDueDate = "September 30, 2026",
                invoiceNumber = "000-005",
                invoiceTotal = "$499.99",
                invoiceEntries = new List<invoiceEntry>()
                {
                    new invoiceEntry()
                    {
                        description = "Website Labor",
                        rate = 100,
                        rateUnit = "Hour",
                        unitAmount = 5
                    },
                    new invoiceEntry()
                    {
                        description = "Server Cost and Maintenance",
                        rate = 50,
                        rateUnit = "Month",
                        unitAmount = 1
                    },
                    new invoiceEntry()
                    {
                        description = "Yearly Domain Name Renewal",
                        rate = 23.99f,
                        rateUnit = "Year",
                        unitAmount = 1
                    }
                }
            };
                        

            return model;
        }

        private string CreateInvoiceNumber(int id)
        {            
            id += 1;
            string invoiceNumber = id.ToString();

            while (invoiceNumber.Length < 9)
            {
                invoiceNumber = "0" + invoiceNumber;                
            }

            invoiceNumber = invoiceNumber.Insert(6, "-");
            invoiceNumber = invoiceNumber.Insert(3, "-");

            return invoiceNumber;
        }

        private int CreateInvoiceNumberId(int id)
        {            
            id += 1;
            
            return id;
        }
    }
}
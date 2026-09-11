using PersonalWebsite.DAL;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebsite.Controllers
{
    public class InvoiceController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserId"] != null && (int)Session["UserId"] > 0)
            {
                clientListVM invoiceList = new clientListVM();

                DALInvoices dalInvoices = new DALInvoices();

                invoiceList.clients = dalInvoices.ListClients((int)Session["UserId"]);

                invoiceList.state = dalInvoices.GetStateDDL();

                invoiceList.country = dalInvoices.GetCountryDDL();
                invoiceList.author = dalInvoices.GetAuthor((int)Session["UserId"]);

                return View(invoiceList);
            }
            else
            {
                return RedirectToAction("Index", "About");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClient(clientListVM model)
        {
            if (Session["UserId"] != null && (int)Session["UserId"] > 0)
            {
                DALInvoices dalInvoice = new DALInvoices();
                if (ModelState.IsValid)
                {
                    if (model.id == 0)
                    {
                        dalInvoice.CreateClient(model, (int)Session["UserId"]);
                    }
                    else
                    {
                        dalInvoice.EditClient(model);
                    }
                }
                else
                {                   
                    model.state = dalInvoice.GetStateDDL();

                    model.country = dalInvoice.GetCountryDDL();
                    model.author = dalInvoice.GetAuthor((int)Session["UserId"]);
                    return PartialView("_Client", model);
                }

                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            }

            return RedirectToAction("Index", "About");
        }

        [HttpGet]
        public ActionResult GetClient(int id)
        {
            if (Request.IsAjaxRequest())
            {

                DALInvoices dalInvoice = new DALInvoices();

                clientListVM invoices = new clientListVM();

                if (id > 0)
                {
                    invoices = dalInvoice.GetClient(id);                    
                }

                invoices.state = dalInvoice.GetStateDDL();

                invoices.country = dalInvoice.GetCountryDDL();
                invoices.author = dalInvoice.GetAuthor((int)Session["UserId"]);

                return PartialView("_Client", invoices);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult ListInvoices(int id)
        {
            if (Request.IsAjaxRequest())
            {
                DALInvoices dalInvoice = new DALInvoices();

                invoiceListVM invoices = new invoiceListVM();
                invoices.invoiceEntries = dalInvoice.ListInvoicesByClientId(id);
                invoices.ClientName = dalInvoice.GetClientName(id);
                invoices.clientId = id;
                Session["ClientId"] = id;

                return PartialView("_Invoices", invoices);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult ListInvoiceEntries(int id)
        {
            if (Request.IsAjaxRequest())
            {
                DALInvoices dalInvoice = new DALInvoices();

                invoiceEntryListVM invoices = new invoiceEntryListVM();
                invoices.invoiceEntries = dalInvoice.ListInvoiceEntriesByInvoiceId(id);              
                invoices.invoiceNumber = dalInvoice.GetInvoiceNumber(id);
                invoices.invoiceEntryId = id;
                Session["InvoiceId"] = id;

                return PartialView("_InvoiceEntries", invoices);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInvoice(invoiceListVM model)
        {
            if (Session["UserId"] != null && (int)Session["UserId"] > 0)
            {
                DALInvoices dalInvoice = new DALInvoices();
                if (ModelState.IsValid)
                {
                    if (model.invoiceId == 0)
                    {
                        model.clientId = (int)Session["ClientId"];                       
                        dalInvoice.CreateInvoice(model, (int)Session["UserId"]);
                    }
                    else
                    {
                        dalInvoice.EditInvoice(model);
                    }
                }
                else
                {
                    return PartialView("_Invoices", model);
                }
                              
                return Json(new { success = true, redirectUrl = Url.Action("Index"), clientId = model.clientId });
            }

            return RedirectToAction("Index", "About");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInvoiceEntry(invoiceEntryListVM model)
        {

            if (Session["UserId"] != null && (int)Session["UserId"] > 0)
            {
                DALInvoices dalInvoice = new DALInvoices();
                if (ModelState.IsValid)
                {
                    if (model.invoiceEntryId == 0)
                    {
                        model.invoiceId = (int)Session["InvoiceId"];
                        dalInvoice.CreateInvoiceEntry(model, (int)Session["UserId"]);
                    }
                    else
                    {
                        dalInvoice.EditInvoiceEntry(model);
                    }
                }
                else
                {                   
                    return PartialView("_InvoiceEntriesModal", model);
                }
                int clientId = 0;
                if (Session["ClientId"] != null)
                {
                    clientId = (int)Session["ClientId"];
                }
                return Json(new { success = true, redirectUrl = Url.Action("Index"), invoiceId = model.invoiceId, clientId = clientId });
            }

            return RedirectToAction("Index", "About");
        }

        [HttpGet]
        public ActionResult GetInvoiceEntry(int id)
        {
            if (Request.IsAjaxRequest())
            {
                DALInvoices dalInvoice = new DALInvoices();

                invoiceEntryListVM invoices = new invoiceEntryListVM();

                if (id > 0)
                {
                    invoices = dalInvoice.GetInvoiceEntry(id);
                    Session["InvoiceEntryId"] = id;
                }               

                return PartialView("_InvoiceEntriesModal", invoices);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
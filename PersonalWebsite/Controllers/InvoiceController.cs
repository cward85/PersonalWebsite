using iText.Forms.Xfdf;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Action;
using iText.Layout.Borders;
using iText.Layout.Element;
using PersonalWebsite.DAL;
using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static iText.IO.Util.IntHashtable;
using Border = iText.Layout.Borders.Border;
using Image = iText.Layout.Element.Image;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;

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

        [HttpGet]
        public ActionResult ViewPDF(int id)
        {
            if (Session["UserId"] != null && (int)Session["UserId"] > 0)
            {
                if (id > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var writer = new iText.Kernel.Pdf.PdfWriter(memoryStream))
                        {
                            using (var pdf = new iText.Kernel.Pdf.PdfDocument(writer))
                            {
                                var document = new iText.Layout.Document(pdf);
                                const float PADDING_AMOUNT = 10;
                                DALInvoices dalInvoice = new DALInvoices();

                                var model = dalInvoice.GetInvoicePDF(id);

                                document.SetFont(PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.COURIER));
                                document.Add(new Paragraph("Invoice")
                                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                                    .SetFontSize(48)
                                    .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                                    .SetBorderBottom(new SolidBorder(1)));

                                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                float[] invoiceContactInfoTableColumnWidths = { 250f, 250f, 250f, 250f };

                                Table invoiceInfoTable = new Table(invoiceContactInfoTableColumnWidths).SetBorder(Border.NO_BORDER);

                                Link link = new Link("www.camiloward.com", PdfAction.CreateURI("https://camiloward.com"));
                                link.SetFontSize(10);

                                invoiceInfoTable.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE).SetBorderLeft(Border.NO_BORDER).SetBorderRight(new SolidBorder(1)).SetBorderTop(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).Add(new Paragraph("Camilo Ward").SetFontSize(14)).Add(new Paragraph(".NET Web Developer").SetFontSize(10)));
                                invoiceInfoTable.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE).SetBorderLeft(new SolidBorder(1)).SetBorderRight(new SolidBorder(1)).SetBorderTop(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).Add(new Paragraph("1920 Lawrence St NE,").SetFontSize(10)).Add(new Paragraph("Washington, DC 20018").SetFontSize(10)));
                                invoiceInfoTable.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE).SetBorderLeft(new SolidBorder(1)).SetBorderRight(new SolidBorder(1)).SetBorderTop(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).Add(new Paragraph("202-329-1294").SetFontSize(10)));
                                invoiceInfoTable.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE).SetBorderLeft(new SolidBorder(1)).SetBorderRight(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderBottom(Border.NO_BORDER).Add(new Paragraph("me@camiloward.com").SetFontSize(10)).Add(new Paragraph("").Add(link)));

                                document.Add(invoiceInfoTable);
                                document.Add(new Paragraph());

                                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                float[] invoiceInfoTableColumnWidths = { 500f, 10f, 500f };

                                invoiceInfoTable = new Table(invoiceInfoTableColumnWidths).SetBorder(Border.NO_BORDER);

                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBorder(Border.NO_BORDER).SetBackgroundColor(ColorConstants.LIGHT_GRAY).Add(new Paragraph().Add(new Text("Invoice #: ").SimulateBold().SetFontSize(12)).Add(new Text(model.invoiceNumber))));
                                invoiceInfoTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBorder(Border.NO_BORDER).SetBackgroundColor(ColorConstants.LIGHT_GRAY).Add(new Paragraph().Add(new Text("Invoice Date: ").SimulateBold().SetFontSize(12)).Add(new Text(model.invoiceDate))));

                                document.Add(invoiceInfoTable);

                                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                document.Add(new Paragraph("Billed to")
                                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                                    .SetFontSize(24));

                                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                float[] invoiceBillingInfoTableColumnWidths = { 200f, 800f };

                                invoiceInfoTable = new Table(invoiceBillingInfoTableColumnWidths).SetBorder(Border.NO_BORDER);

                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBorderTop(new SolidBorder(1)).SetBorderRight(new SolidBorder(1)).SetBorderLeft(Border.NO_BORDER).Add(new Paragraph("Customer's Name").SimulateBold().SetFontSize(12)));
                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBorderTop(new SolidBorder(1)).SetBorderRight(Border.NO_BORDER).Add(new Paragraph(model.customerName).SetFontSize(12)));

                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBorderTop(new SolidBorder(1)).SetBorderRight(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)).SetBorderLeft(Border.NO_BORDER).Add(new Paragraph("Address").SimulateBold().SetFontSize(12)));
                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBorderTop(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)).SetBorderRight(Border.NO_BORDER).Add(new Paragraph(model.customerAddress).SetFontSize(12)));

                                document.Add(invoiceInfoTable);
                                document.Add(new Paragraph());
                                document.Add(new Paragraph());
                                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                float[] invoiceEntriesTableColumnWidths = { 350f, 175f, 150f, 225f };

                                invoiceInfoTable = new Table(invoiceEntriesTableColumnWidths).SetBorder(Border.NO_BORDER);

                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorderLeft(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)).Add(new Paragraph("Description").SimulateBold().SetFontSize(16).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)));
                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorderLeft(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)).Add(new Paragraph("Rate (USD)").SimulateBold().SetFontSize(16).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)));
                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorderLeft(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)).Add(new Paragraph("Units").SimulateBold().SetFontSize(16).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)));
                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorderLeft(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1)).Add(new Paragraph("Total (USD)").SimulateBold().SetFontSize(16).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)));

                                foreach (invoiceEntry entry in model.invoiceEntries)
                                {
                                    invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBorderLeft(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)).Add(new Paragraph(entry.description).SetFontSize(12).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)));
                                    invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBorderLeft(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)).Add(new Paragraph("$" + entry.rate + "/" + entry.rateUnit).SetFontSize(12).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)));
                                    invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBorderLeft(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(new SolidBorder(1)).SetBorderBottom(new SolidBorder(1)).Add(new Paragraph(entry.unitAmount.ToString()).SetFontSize(12).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)));
                                    invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBorderLeft(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER).SetBorderBottom(new SolidBorder(1)).Add(new Paragraph("$" + (entry.unitAmount * entry.rate).ToString()).SetFontSize(12).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)));
                                }

                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(Border.NO_BORDER).Add(new Paragraph("").SimulateBold().SetFontSize(12)));
                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(Border.NO_BORDER).Add(new Paragraph("").SimulateBold().SetFontSize(12)));
                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(Border.NO_BORDER).SetBorderRight(new SolidBorder(1)).Add(new Paragraph("Grand Total").SimulateBold().SetFontSize(12).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)));
                                invoiceInfoTable.AddCell(new Cell().SetPaddingTop(PADDING_AMOUNT).SetPaddingBottom(PADDING_AMOUNT).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(Border.NO_BORDER).Add(new Paragraph(model.invoiceTotal).SimulateBold().SetFontSize(12).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)));

                                document.Add(invoiceInfoTable);

                                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                document.Add(new Paragraph("Payment Information")
                                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                                    .SetFontSize(24));

                                float[] invoicePaymentInfoTableColumnWidths = { 250f, 200f, 550f };
                                float[] innerTable = { 600f };
                                invoiceInfoTable = new Table(invoicePaymentInfoTableColumnWidths).SetBorder(new SolidBorder(1));

                                string imagePath = HttpContext.Server.MapPath("~/Content/images/qrcode.png");
                                var imageData = ImageDataFactory.Create(imagePath);
                                Image img = new Image(imageData).SetAutoScale(true);
                                var rowspanCell1 = new Cell(2, 1).Add(new Paragraph("Payment Information"));

                                invoiceInfoTable.AddCell(new Cell(2, 1).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE).SetBackgroundColor(ColorConstants.LIGHT_GRAY).Add(new Paragraph("Payment Due Date").SimulateBold().SetFontSize(12)).Add(new Paragraph(model.invoiceDueDate)));
                                invoiceInfoTable.AddCell(new Cell(2, 1).Add(img));
                                invoiceInfoTable.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE).Add(new Paragraph("Make Checks Payable To").SimulateBold()));
                                invoiceInfoTable.AddCell(new Cell().SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE).Add(new Paragraph("Camilo Ward")));


                                document.Add(invoiceInfoTable);

                                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



                                document.Close();

                                return File(memoryStream.ToArray(), "application/pdf",
                                "Invoice.pdf");
                            }
                        }
                    }
                }
                else
                {
                    return File(new byte[0], "application/pdf",
                                "Invoice.pdf");
                }
            }
            else
            {
                return RedirectToAction("Index", "About");
            }
        }

        [HttpPost]
        public ActionResult SendInvoice(int id)
        {
            if (Session["UserId"] != null && (int)Session["UserId"] > 0)
            {
                DALInvoices dalInvoice = new DALInvoices();

                int clientId = dalInvoice.SendInvoice(id);                

                return Json(new { success = true, redirectUrl = Url.Action("Index"), clientId = clientId,  });
            }
            else
            {
                return RedirectToAction("Index", "About");
            }
        }
    }        
}
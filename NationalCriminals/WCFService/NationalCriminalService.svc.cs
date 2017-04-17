using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Entities;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using WCFService.Models;
using System.Collections.Generic;

namespace WCFService
{
    public class NationalCriminalService : INationalCriminalService
    {
        /// <summary>
        /// Generate search results based on input parameters.
        /// </summary>
        /// <param name="model">Model with appropriate data.</param>
        public void GenerateSearchResult(PersonClientModels model)
        {
            var db = new NationalCriminalsEntities();

            var person = (from people in db.People
                          where (
                          (model.Name == null || people.Name.Contains(model.Name)) &&
                          (model.Nationality == null || people.Nationality == model.Nationality) &&
                          (people.Sex == model.Sex) &&
                          (people.Age >= model.AgeMin && (model.AgeMax == 0 || people.Age <= model.AgeMax)) &&
                          (people.Height >= model.HeightMin && (model.HeightMax == 0 || people.Height <= model.HeightMax)) &&
                          (people.Weight >= model.WeightMin && (model.WeightMax == 0 || people.Weight <= model.WeightMax)))
                          select new PersonModel
                          {
                              Name = people.Name,
                              Age = people.Age,
                              Sex = people.Sex,
                              Weight = people.Weight,
                              Height = people.Height,
                              Nationality = people.Nationality

                          }).Take(model.MaxResult).ToList();

            //Send Email asynchronous.
            Task.Run(async () =>
            {
                if (person.Count == 0)
                {
                    await CreateNoRecordsFoundPDF(model.RecipientEmail);
                }
                else
                {
                    await CreatePDF(person, model.RecipientEmail);
                }

            });

        }

        /// <summary>
        /// Creates PDF and sends email.
        /// </summary>
        /// <param name="persons">Model containing criminals data.</param>
        /// <param name="recipientEmail">Recipient email to send PDF.</param>
        public async Task CreatePDF(List<PersonModel> persons, string recipientEmail)
        {
            var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlWorker = new HTMLWorker(pdfDoc);
            byte[] bytes;
            PdfWriter writer;

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();

                    for (int i = 0; i < persons.Count; i++)
                    {
                        var template = GetHTMLTemplate(persons[i]);
                        htmlWorker.Parse(template);
                        pdfDoc.NewPage();

                        if ((i + 1) % 10 == 0)
                        {
                            pdfDoc.Close();
                            bytes = memoryStream.ToArray();
                            await SendEmailAsync(bytes, recipientEmail);

                            await CreatePDF(persons.Skip(i + 1).ToList(), recipientEmail);
                            persons.Clear();
                        }
                    }

                    if (persons.Count % 10 != 0)
                    {
                        pdfDoc.Close();
                        bytes = memoryStream.ToArray();
                        await SendEmailAsync(bytes, recipientEmail);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generates HTML Template.
        /// </summary>
        /// <param name="persons">Persons model data.</param>
        /// <returns>StringReader object with HTML format.</returns>
        public StringReader GetHTMLTemplate(PersonModel persons)
        {
            StringBuilder sb = new StringBuilder();
            StringReader sr = new StringReader(sb.ToString());

            sb.Append("<table border='1' cellspacing='0' cellpadding='10'>");
            sb.Append("<tbody>");
            sb.Append("<tr><td>Name</td><td>" + persons.Name + "</td></tr>");
            sb.Append("<tr><td>Sex</td><td>" + persons.Sex + "</td></tr>");
            sb.Append("<tr><td>Age(years)</td><td>" + persons.Age + "</td></tr>");
            sb.Append("<tr><td>Height(cms)</td><td>" + persons.Height + "</td></tr>");
            sb.Append("<tr><td>Weight(kg)</td><td>" + persons.Weight + "</td></tr>");
            sb.Append("<tr><td>Nationality</td><td>" + persons.Nationality + "</td></tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");

            return new StringReader(sb.ToString());
        }

        /// <summary>
        /// Creates PDF with "No Records Found" text.
        /// </summary>
        /// <param name="recipientEmail">Reciepient Email.</param>
        public async Task CreateNoRecordsFoundPDF(string recipientEmail)
        {
            var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var htmlWorker = new HTMLWorker(pdfDoc);
            byte[] bytes;
            PdfWriter writer;

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();

                    var template = new StringReader("No records found");
                    htmlWorker.Parse(template);
                    pdfDoc.NewPage();

                    pdfDoc.Close();
                    bytes = memoryStream.ToArray();
                    await SendEmailAsync(bytes, recipientEmail);              

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sends email
        /// </summary>
        /// <param name="bytes">PDF attachment as bytes.</param>
        /// <param name="recipientEmail">Recipient Email.</param>
        public async Task SendEmailAsync(byte[] bytes, string recipientEmail)
        {
            try
            {
                var fromEmail = ConfigurationManager.AppSettings["FromEmail"];
                var fromPassword = ConfigurationManager.AppSettings["FromPassword"];
                var host = ConfigurationManager.AppSettings["Host"];
                var port = int.Parse(ConfigurationManager.AppSettings["Port"]);

                MailMessage mm = new MailMessage(fromEmail, recipientEmail);
                mm.Subject = "National Criminal Search Result";
                mm.Body = "PFA file.";
                mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "NationalCriminal.pdf"));
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = host;
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = fromEmail;
                NetworkCred.Password = fromPassword;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = port;
                await smtp.SendMailAsync(mm);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

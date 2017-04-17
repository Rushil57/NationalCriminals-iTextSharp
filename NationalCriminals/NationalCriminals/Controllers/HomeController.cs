using System.Web.Mvc;
using NationalCriminals.Models;
using NationalCriminals.ServiceReference1;

namespace NationalCriminals.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Renders index view with search form.
        /// </summary>
        /// <returns>Index view.</returns>
        public ActionResult Index()
        {            
            return View();
        }

        /// <summary>
        /// Searches the criminals returns boolean value.
        /// </summary>
        /// <param name="model">Person ViewModel with appropriate data.</param>
        /// <returns>True or False</returns>
        public ActionResult Search(PersonViewModels model)
        {
            var objService = new NationalCriminalServiceClient();
            bool result = false;
            if (ModelState.IsValid)
            {
                var person = new PersonClientModels
                {
                    Name = model.Name,
                    Sex = model.Sex,
                    AgeMin = model.AgeMin,
                    AgeMax = model.AgeMax,
                    HeightMin = model.HeightMin,
                    HeightMax = model.HeightMax,
                    WeightMin = model.WeightMin,
                    WeightMax = model.WeightMax, 
                    MaxResult = model.MaxResult,
                    RecipientEmail = model.RecipientEmail
                };

                var tt = objService.GenerateSearchResultAsync(person);
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}

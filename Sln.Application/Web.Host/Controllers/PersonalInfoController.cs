using Application.Core;
using Application.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Web.Host.LIB;

namespace jQueryDatatables.Controllers
{
    public class PersonalInfoController : Controller
    {
        private readonly IBaseRepository _repository;
        public PersonalInfoController(IBaseRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataTabelData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int resultTotal = 0;

                var personalInfoData = (from tblObj in _repository.GetAll<PersonalInfo>() select tblObj);

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {
                    personalInfoData = _repository.GetAll<PersonalInfo>().OrderBy(sortColumn + " " + sortColumnAscDesc);
                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    personalInfoData = personalInfoData.Where(t => t.FirstName.Contains(searchValue)
                    || t.LastName.Contains(searchValue)
                    || t.City.Contains(searchValue)
                    || t.Country.Contains(searchValue)
                    || t.MobileNo.Contains(searchValue));
                }

                resultTotal = personalInfoData.Count();
                var result = personalInfoData.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public IActionResult AddEditPersonalInfo(int id)
        {
            PersonalInfo personalInfo = new PersonalInfo();
            if (id > 0) personalInfo = _repository.Find<PersonalInfo>(b => b.ID == id);
            return PartialView("_PersonalInfoForm", personalInfo);
        }

        [HttpPost]
        public async Task<string> AddEditPersonalInfo(PersonalInfo personalInfo)
        {
            if (ModelState.IsValid)
            {
                if (personalInfo.ID > 0)
                {
                    personalInfo.LastModifiedDate = DateTime.Now;
                    personalInfo.LastUpdateUser = "Admin";
                    _repository.Update<PersonalInfo>(personalInfo, personalInfo.ID);
                    return "Personal Info Updated Successfully";
                }
                else
                {
                    personalInfo.CreatedDate = DateTime.Now;
                    personalInfo.CreationUser = "Admin";
                    await _repository.AddAsyn<PersonalInfo>(personalInfo);
                    var result = await _repository.SaveAsync();

                    var successMessage = "Personal Info Created Successfully. Name: " + personalInfo.FirstName;
                    TempData["successAlert"] = successMessage;
                    return "Personal Info Created Successfully";
                }
            }
            return "Failed";
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            PersonalInfo personalInfo = _repository.Get<PersonalInfo>(id);
            _repository.Delete<PersonalInfo>(personalInfo);
            return RedirectToAction("Index");
        }


        public FileStreamResult ExportAllDatatoCSV()
        {
            var personalInfoData = (from tblObj in _repository.GetAll<PersonalInfo>() select tblObj).Take(100);
            var result = Common.WriteCsvToMemory(personalInfoData);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "Personal_Info_Data.csv" };
        }

    }
}

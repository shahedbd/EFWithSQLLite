using Application.Core;
using Application.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalInfoOLDController : Controller
    {
        private readonly IBaseRepository _repository;
        public PersonalInfoOLDController(IBaseRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IEnumerable<PersonalInfo>> GetAllPersonalInfo()
        {
            var result = await _repository.GetAllAsyn<PersonalInfo>();
            return result;
        }

    }
}
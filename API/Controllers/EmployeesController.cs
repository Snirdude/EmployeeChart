using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly DataContext _context;
        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeesPerMonthPerYearDto>>> GetAllEmployeesData()
        {
            return Ok(await DataHelper.GetEmployeesDataDtos(_context));
        }

        [HttpPost("generate")]
        public async Task<ActionResult<IEnumerable<Employee>>> GenerateData(GenerateDataParams dataParams)
        {
            await DataHelper.GenerateData(_context, dataParams.NumberOfEmployees);
            return Ok(_context.Employees.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAllData()
        {
            await DataHelper.DeleteEmployeesData(_context);
            return Ok();
        }
    }
}
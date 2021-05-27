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

        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployeesData()
        {
            return await _context.Employees.ToListAsync();
        }

        [HttpPost("generate")]
        public async Task<ActionResult<IEnumerable<Employee>>> GenerateData(GenerateDataDto generateDataDto)
        {
            await DataHelper.GenerateData(_context, generateDataDto.NumberOfEmployees);
            return Ok(_context.Employees.ToListAsync());
        }

        [HttpDelete("delete-all")]
        public async Task DeleteAllData()
        {
            await DataHelper.DeleteEmployeesData(_context);
        }
    }
}
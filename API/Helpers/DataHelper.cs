using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public static class DataHelper
    {
        public async static Task GenerateData(DataContext context, int numberOfEmployees)
        {
            Random rnd = new Random();
            List<Employee> employees = new List<Employee>();
            if (numberOfEmployees > 100)
            {
                numberOfEmployees = 100;
            }

            for (int i = 0; i < numberOfEmployees; i++)
            {
                TimeSpan timeSpan = DateTime.Now - DateTime.Now.AddYears(-3);
                TimeSpan newSpan = new TimeSpan(rnd.Next((int)timeSpan.TotalDays), 0, 0, 0);
                DateTime startDate = DateTime.Now.AddYears(-3) + newSpan;
                DateTime? endDate = null;
                if (rnd.Next(0, 2) == 0)
                {
                    timeSpan = DateTime.Now - startDate;
                    newSpan = new TimeSpan(rnd.Next((int)timeSpan.TotalDays), 0, 0, 0);
                    endDate = startDate + newSpan;
                }

                employees.Add(new Employee { StartDateOfEmployment = startDate, EndDateOfEmployment = endDate });
            }

            await context.AddRangeAsync(employees);
            await context.SaveChangesAsync();
        }

        public async static Task DeleteEmployeesData(DataContext context)
        {
            context.Employees.RemoveRange(context.Employees);
            await context.SaveChangesAsync();
        }

        public async static Task<IEnumerable<EmployeesPerMonthPerYearDto>> GetEmployeesDataDtos(DataContext context)
        {
            List<EmployeesPerMonthPerYearDto> dtos = new List<EmployeesPerMonthPerYearDto>();
            for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    EmployeesPerMonthPerYearDto dto = new EmployeesPerMonthPerYearDto();
                    dto.Year = i;
                    dto.Month = j;
                    if (new DateTime(i, j, 1) > DateTime.Now)
                    {
                        dto.Count = null;
                    }
                    else
                    {
                        dto.Count = await context.Employees.Where(x => x.StartDateOfEmployment <= new DateTime(i, j, DateTime.DaysInMonth(i, j))
                                                        && (x.EndDateOfEmployment == null || x.EndDateOfEmployment >= new DateTime(i, j, 1)))
                                                        .CountAsync();
                    }

                    dtos.Add(dto);
                }
            }

            return dtos;
        }
    }
}
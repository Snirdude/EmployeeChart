using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
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
                TimeSpan timeSpan = new DateTime(2021, 12, 31) - new DateTime(2019, 1, 1);
                TimeSpan newSpan = new TimeSpan(rnd.Next((int)timeSpan.TotalDays), 0, 0, 0);
                DateTime startDate = new DateTime(2018, 12, 31) + newSpan;
                DateTime? endDate = null;
                if (rnd.Next(0, 2) == 0)
                {
                    timeSpan = new DateTime(2021, 12, 31) - startDate;
                    newSpan = new TimeSpan(rnd.Next((int)timeSpan.TotalDays), 0, 0, 0);
                    endDate = new DateTime(2018, 12, 31) + newSpan;
                }

                employees.Add(new Employee { StartDateOfEmployment = startDate, EndDateOfEmployment = endDate });
            }

            await context.AddRangeAsync(employees);
            await context.SaveChangesAsync();
        }

        public async static Task DeleteEmployeesData(DataContext context)
        {
            var employees = await context.Employees.ToListAsync();
            context.Employees.RemoveRange(employees);
            await context.SaveChangesAsync();
        }
    }
}
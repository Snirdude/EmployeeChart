using System;

namespace API.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public DateTime StartDateOfEmployment { get; set; }
        public DateTime? EndDateOfEmployment { get; set; }
    }
}
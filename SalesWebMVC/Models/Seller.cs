using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models {
    public class Seller {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size must be [{2}, {1}]")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be [{1}, {2}]")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, double baseSalary, DateTime birthDate, Department department) {
            Id = id;
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
            Department = department;
        }

        public void AddSales(SalesRecord sr) {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr) {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime from, DateTime to) {
            //Version 1
            /*
            double total = 0.0;
            foreach(SalesRecord record in Sales) {
                if (record.Date >= from && record.Date <= to) {
                    total += record.Amount;
                }
            }
            return total;
            */

            //Version 2
            return Sales.Where(sr => sr.Date >= from && sr.Date <= to).Sum(sr => sr.Amount);
        }
    }
}

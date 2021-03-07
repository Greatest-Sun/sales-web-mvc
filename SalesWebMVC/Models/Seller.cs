using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebMVC.Models {
    public class Seller {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double BaseSalary { get; set; }
        public DateTime BirthDate { get; set; }
        public Department Department { get; set; }
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

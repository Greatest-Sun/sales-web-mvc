using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebMVC.Models {
    public class Department {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }
        public Department(int iD, string name) {
            ID = iD;
            Name = name;
        }

        public void AddSeller(Seller seller) {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime from, DateTime to) {
            return Sellers.Sum(seller => seller.TotalSales(from, to));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace SalesWebMVC.Services {
    public class SellerService {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context) {
            _context = context;
        }

        public async Task<Seller> FindByIdAsync(int id) {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task<List<Seller>> FindAllAsync() {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller) {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id) {
            try {
                Seller seller = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException e) {
                throw new IntegrityException("Can't delete a seller who made sales");
            }
        }

        public async Task UpdateAsync(Seller seller) {
            if (!(await _context.Seller.AnyAsync(x => x.Id == seller.Id))) {
                throw new NotFoundException("ID not found");
            }
            try {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}

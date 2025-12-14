using BumbAlexandruFlaviuLab7.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BumbAlexandruFlaviuLab7.Data
{
    public class ShopListDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public ShopListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
            _database.CreateTableAsync<Shop>().Wait();
        }

      
        public Task<int> SaveShopListAsync(ShopList list)
        {
            if (list.ID != 0)
                return _database.UpdateAsync(list);
            else
                return _database.InsertAsync(list);
        }

        public Task<int> DeleteShopListAsync(ShopList list)
        {
            return _database.DeleteAsync(list);
        }

        public Task<List<ShopList>> GetShopListsAsync()
        {
            return _database.Table<ShopList>().ToListAsync();
        }

      
        public Task<int> SaveProductAsync(Product product)
        {
            if (product.ID != 0)
                return _database.UpdateAsync(product);
            else
                return _database.InsertAsync(product);
        }

        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> SaveListProductAsync(ListProduct listp)
        {
            if (listp.ID != 0)
                return _database.UpdateAsync(listp);
            else
                return _database.InsertAsync(listp);
        }

        public Task<List<Product>> GetListProductsAsync(int shoplistid)
        {
            return _database.QueryAsync<Product>(
                "SELECT P.ID, P.Description " +
                "FROM Product P " +
                "INNER JOIN ListProduct LP " +
                "ON P.ID = LP.ProductID " +
                "WHERE LP.ShopListID = ?",
                shoplistid
            );
        }

        public Task<List<Shop>> GetShopsAsync()
        {
            return _database.Table<Shop>().ToListAsync();
        }

        public Task<int> SaveShopAsync(Shop shop)
        {
            if (shop.ID != 0)
            {
                return _database.UpdateAsync(shop);
            }
            else
            {
                return _database.InsertAsync(shop);
            }
        }

        public Task<int> DeleteShopAsync(Shop shop)
        {
            return _database.DeleteAsync(shop);
        }

    }
}

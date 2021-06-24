using CoreMongoBookAPI.Middleware;
using CoreMongoBookAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMongoBookAPI.DatabaseServices
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;

        public EmployeeService(IMongosettings settings)
        {
            var client = new MongoClient(settings.Connection);
            
            var database = client.GetDatabase(settings.DatabaseName);

            _employees = database.GetCollection<Employee>(settings.CollectionName);
        }

        public async Task<List<Employee>> Get() => 
            await _employees.Find(emp => true).ToListAsync();

        public async Task<Employee> Get(string id) =>
            await _employees.Find(emp => emp.Id == id).FirstOrDefaultAsync();


        public async Task<Employee> CreateAsync(Employee student)
        {
            await _employees.InsertOneAsync(student);
            return student;
        }
        public async Task UpdateAsync(string id, Employee student)
        {
            await _employees.ReplaceOneAsync(s => s.Id == id, student);
        }
        public async Task DeleteAsync(string id)
        {
            await _employees.DeleteOneAsync(s => s.Id == id);
        }
    }
}

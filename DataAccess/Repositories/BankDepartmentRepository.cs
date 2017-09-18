using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DataBase;
using DataAccess.Repositories.Interfacies;
using DataAccess.UnitOfWork;

namespace DataAccess.Repositories
{
    public class BankDepartmentRepository: IBankDepartmentRepository
    {
        private readonly DbSet<BankDepartment> _bankDepartments;

        public BankDepartmentRepository(IUnitOfWork unitOfWork)
        {
            _bankDepartments = unitOfWork.Context.Set<BankDepartment>();
        }

        public async Task<BankDepartment> GetById(int id)
        {
            return await _bankDepartments.FindAsync(id);
        }

        public async Task<BankDepartment> Find(string address, string name)
        {
            return await _bankDepartments.FirstOrDefaultAsync(x => x.Address == address && x.Name == name);
        }

        //public BankDepartment Add(BankDepartment bankDepartment)
        //{
        //    return _bankDepartments.Add(bankDepartment);
        //}

        //public async Task<BankDepartment> Update(int id, BankDepartment bankDepartment)
        //{
        //    var oldValue = await _bankDepartments.FindAsync(id);
        //    if (oldValue == null) return null;
        //    oldValue.Address = bankDepartment.Address;
        //    oldValue.Name = bankDepartment.Name;
        //    oldValue.BankId = bankDepartment.BankId;
        //    oldValue.CityId = bankDepartment.CityId;

        //    oldValue.CurrencyRateByTime = bankDepartment.CurrencyRateByTime;

        //    return oldValue;
        //}

        //private async Task AddCurrencyRate(int departmentId, CurrencyRateByTime rate)
        //{
        //    var department = await _bankDepartments.FindAsync(departmentId);
        //    if (department == null) throw new NullReferenceException();
        //    department.CurrencyRateByTime.Add(rate);
        //}

        //private async Task AddCurrencyRateRange(int departmentId, IEnumerable<CurrencyRateByTime> rates)
        //{
        //    var department = await _bankDepartments.FindAsync(departmentId);
        //    if (department == null) throw new NullReferenceException();

        //    foreach (var rate in rates)
        //    {
        //        department.CurrencyRateByTime.Add(rate);
        //    }
        //}

        //public async Task AddOrUpdatesRange(ICollection<BankDepartment> departments)
        //{
        //    foreach (var department in departments)
        //    {
        //        if (department.Id != 0)
        //        {
        //            await AddCurrencyRateRange(department.Id, department.CurrencyRateByTime);
        //        }
        //        else
        //        {
        //            Add(department);
        //        }
        //    }
        //}
    }
}
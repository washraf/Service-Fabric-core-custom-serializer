using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public interface IEmployeeService:IService
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int Id);
        Task<Result<string>> SaveEmployee(Employee employee);
    }
}

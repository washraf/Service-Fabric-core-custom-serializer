using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using Model;
using Utils;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService proxy;
        public EmployeeController(StatelessServiceContext context)
        {
            var proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory(
                serializationProvider: new ServiceRemotingJsonSerializationProvider());
            });
            string serviceUri =
                $"{context.CodePackageActivationContext.ApplicationName}"
                + "/StatelessBackEnd";

            proxy = proxyFactory.CreateServiceProxy<IEmployeeService>(new Uri(serviceUri));
        }

        // GET: api/Employee
        [HttpGet]
        public async Task< IEnumerable<Employee>> Get()
        {
            return await proxy.GetAllEmployees();
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Employee> Get(int id)
        {
            return await proxy.GetEmployeeById(id);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<Result<string>> Post([FromBody] Employee employee)
        {
            return await proxy.SaveEmployee(employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

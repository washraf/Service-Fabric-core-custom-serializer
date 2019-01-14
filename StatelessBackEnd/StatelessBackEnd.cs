using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Model;
using Utils;

namespace StatelessBackEnd
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatelessBackEnd : StatelessService,IEmployeeService
    {
        public StatelessBackEnd(StatelessServiceContext context)
            : base(context)
        { }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await Task.Run(() =>
            {
                return new List<Employee>() {
                    new Employee()
                    {
                        ID =1,
                        Name = "One",
                        HireDate = DateTime.Now
                    },
                    new Employee()
                    {
                        ID = 2,
                        Name = "Two",
                        HireDate = DateTime.Now
                    },
                    new Employee()
                    {
                        ID = 3,
                        Name = "Three",
                        HireDate = DateTime.Now
                    }
                };
            });

        }

        public async Task<Employee> GetEmployeeById(int Id)
        {
            return await Task.Run(() =>
            {
                return new Employee()
                {
                    ID = 1,
                    Name = "One",
                    HireDate = DateTime.Now
                };
            });

        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[]
    {
        new ServiceInstanceListener((c) =>
        {
            return new FabricTransportServiceRemotingListener(c, this,
                serializationProvider: new ServiceRemotingJsonSerializationProvider());
        })
    };
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            long iterations = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}

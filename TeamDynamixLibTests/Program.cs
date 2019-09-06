using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;

using TeamDynamixLib;
using TeamDynamix.Api.Auth;
using TeamDynamix.Api.Tickets;
using TeamDynamix.Api.PriorityFactors;



namespace TeamDynamixLibTests {

    class Program {

        static async Task Main(string[] args) {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            #region AppSettings
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.Development.json", true, true)
                .Build();

            var tdxConfig = new TDXSettings();
            config.GetSection("TeamDynamixSettings").Bind(tdxConfig);

            var proxyConfig = new ProxySettings();
            config.GetSection("ProxySettings").Bind(proxyConfig);
            #endregion 

            TDXEnvironment tDXEnvironment = new TDXEnvironment {
                ClientUrl = tdxConfig.ClientURL,
                IsSandboxEnvironment = tdxConfig.IsSandboxEnvironment
            };

            AdminTokenParameters adminTokenParameters = new AdminTokenParameters {
                BEID = tdxConfig.BEID,
                WebServicesKey = tdxConfig.WebServiceKey
            };

            AuthenticationLib authenticationLib = new AuthenticationLib();
            var JWT = await authenticationLib.GetAuthHeaderAsync(adminTokenParameters, tDXEnvironment);

            ImpactsLib impactsLib = new ImpactsLib();
            PrioritiesLib prioritiesLib = new PrioritiesLib();
            SourcesLib sourcesLib = new SourcesLib();

            var impacts = await impactsLib.GetTicketImpactsAsync(431, JWT, tDXEnvironment);
            var priorities = await prioritiesLib.GetTicketPrioritiesAsync(431, JWT, tDXEnvironment);
            var sources = await sourcesLib.GetTicketSourcesAsync(431, JWT, tDXEnvironment);

            //PeopleLib peopleLib = new PeopleLib();
            //var serg = await peopleLib.GetPersonLookupAsync("palomins@palmbeachstate.edu", JWT, tDXEnvironment);

            //TicketLib ticketLib = new TicketLib();
            //Ticket ticket = new Ticket() {
            //    TypeID = 20999,
            //    Title = "TEST API IGNORE",
            //    AccountID = 48319,
            //    StatusID = 17950,
            //    PriorityID = 3371,
            //    RequestorUid = serg[0].UID
            //};

            //TicketCreateOptions ticketCreateOptions = new TicketCreateOptions() {
            //    AllowRequestorCreation = false,
            //    EnableNotifyReviewer = false,
            //    NotifyRequestor = false,
            //    NotifyResponsible = false
            //};

            //var myTestTicket = await ticketLib.CreateTicketAsync(ticket, 431, ticketCreateOptions, JWT, tDXEnvironment);

            //foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(myTestTicket)) {
            //    string name = descriptor.Name;
            //    object value = descriptor.GetValue(myTestTicket);
            //    Console.WriteLine("{0} = {1}", name, value);
            //}

            Console.ReadLine();
        }
    }

    class TDXSettings {
        public Guid BEID {
            get; set;
        }
        public Guid WebServiceKey {
            get; set;
        }
        public string ClientURL {
            get; set;
        }
        public bool IsSandboxEnvironment {
            get; set;
        }
    }

    class ProxySettings {
        public string ProxyIP {
            get; set;
        }
        public short ProxyPort {
            get; set;
        }
    }

}

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using TeamDynamixLib;
using TeamDynamix.Api.Auth;

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

            TicketLib ticketLib = new TicketLib();
            



            var result1 = Task.WhenAll(
                ticketLib.GetTicketAsync(9757500, 431, JWT, tDXEnvironment),
                ticketLib.GetTicketAsync(9757500, 431, JWT, tDXEnvironment),
                ticketLib.GetTicketAsync(9757500, 431, JWT, tDXEnvironment),
                ticketLib.GetTicketAsync(9757500, 431, JWT, tDXEnvironment),
                ticketLib.GetTicketAsync(9757500, 431, JWT, tDXEnvironment),
                ticketLib.GetTicketAsync(9757500, 431, JWT, tDXEnvironment)
            );
            




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

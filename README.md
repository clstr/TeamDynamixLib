# TeamDynamixLib
Library of TeamDynamix function calls to make my life easier.

DotNet Core 2.1
RestSharp
Newtonsoft Json.NET

You will need the **TeamDynamix API** Library for these functions to work. You can obtain these in TDNext > Downloads > TeamDynamix API .DLL (.NET 4.5.2) 

I will continue to update this project as time permits.

#### To build the project
> dotnet build

You can always add the project sln to your existing solution too.

#### Example Usage
```csharp
public async Task<Ticket> GetTicket() {
            TeamDynamixLib.TeamDynamixLib teamDynamixLib = new TeamDynamixLib.TeamDynamixLib();
            TeamDynamixLib.TDXEnvironment tDXEnvironment = new TeamDynamixLib.TDXEnvironment();

            AdminTokenParameters adminTokenParameters = new AdminTokenParameters {
                BEID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                WebServicesKey = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };

            tDXEnvironment.ClientUrl = "https://yourSchool.teamDynamix.com/";
            tDXEnvironment.IsSandboxEnvironment = false;

            var JWT = await teamDynamixLib.GetAuthHeaderAsync(adminTokenParameters.BEID, adminTokenParameters.WebServicesKey, tDXEnvironment);

            var ticket = await teamDynamixLib.GetTicketAsync(892372, 233, JWT, tDXEnvironment);

            return ticket;
        }
```

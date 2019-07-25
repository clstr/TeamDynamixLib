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
public static async Task<Ticket> GetTicket() {

    TDXEnvironment tDXEnvironment = new TDXEnvironment {
        ClientUrl = "https://yourSchool.teamDynamix.com/",
        IsSandboxEnvironment = false
    };

    AdminTokenParameters adminTokenParameters = new AdminTokenParameters {
        BEID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
        WebServicesKey = Guid.Parse("00000000-0000-0000-0000-000000000000")
    };

    AuthenticationLib authenticationLib = new AuthenticationLib();
    var JWT = await authenticationLib.GetAuthHeaderAsync(adminTokenParameters, tDXEnvironment);

    TicketLib ticketLib = new TicketLib();
    var ticket = await ticketLib.GetTicketAsync(696969, 420, JWT, tDXEnvironment);

    return ticket;
}
```

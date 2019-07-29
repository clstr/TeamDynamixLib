# TeamDynamixLib
Library of TeamDynamix function calls to make my life easier. I will continue to update this project as time permits.

#### Dependencies
- DotNet Core 2.1
- RestSharp
- Newtonsoft Json.NET
- TeamDynamix.API

This project does not come with the TeamDynamix API DLL, you will need the **TeamDynamix API** Library for these functions to work. You can obtain these in TDNext > Downloads > TeamDynamix API .DLL (.NET 4.5.2) .


#### To build the project
> dotnet build

You can always add the project sln to your existing solution too or as a nuget package.

#### To install as a Nuget package
Set the project to Release and publish it. After it's published (Framework Dep, Portable) you will get a nupkg in your bin\Release\netcoreapp2.x\publish\ folder. Install the  nupkg file to your project.

https://stackoverflow.com/questions/10240029/how-do-i-install-a-nuget-package-nupkg-file-locally

After it installs you can verify by looking at your project's dependencies in the solution explorer.

#### Example Usage
```csharp
public static async Task Main(string[] args) {
    Console.WriteLine("Hello World!");

    TDXEnvironment tDXEnvironment = new TDXEnvironment {
        ClientUrl = "https://yourSchool.teamDynamix.com/",
        IsSandboxEnvironment = true
    };

    AdminTokenParameters adminTokenParameters = new AdminTokenParameters {
        BEID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
        WebServicesKey = Guid.Parse("00000000-0000-0000-0000-000000000000")
    };

    AuthenticationLib authenticationLib = new AuthenticationLib();
    var JWT = await authenticationLib.GetAuthHeaderAsync(adminTokenParameters, tDXEnvironment);

    TicketLib ticketLib = new TicketLib();
    var ticketDetailsExample1 = await ticketLib.GetTicketAsync(9280401, 431, JWT, tDXEnvironment);
    var ticketDetailsExample2 = await ticketLib.GetTicketAsync(696969, 500, JWT, tDXEnvironment);
    
    PeopleLib peopleLib = new PeopleLib();
    var personDetailsExample1 = await peopleLib.GetPersonByUIDAsync(
        Guid.Parse("00000000-0000-0000-0000-000000000000"), 
        JWT, tDXEnvironment
    );
    
    Ticket ticket = new Ticket() {
        TypeID = 20999,
        Title = "(TEST API IGNORE) Install IIS Latest Version",
        AccountID = 48319,
        StatusID = 17950,
        PriorityID = 3371,
        RequestorUid = Guid.Parse("00000000-0000-0000-0000-000000000000")
    };

    TicketCreateOptions ticketCreateOptions = new TicketCreateOptions() {
        EnableNotifyReviewer = false,
        NotifyRequestor = false,
        NotifyResponsible = false,
        AllowRequestorCreation = false
    };

    var myTicket = await ticketLib.CreateTicketAsync(ticket, 431, ticketCreateOptions, JWT, tDXEnvironment);
}
```
Note: To run the example above you will need to set your project to use C# version 7.1+ since I am using async in the main function.
http://techxposer.com/2017/11/18/enable-c-7-1-projects/

You only need to call GetAuthHeaderAsync() once to get the Json Web Token (JWT), which can be used until it expires. This means you can use the same JWT for all other TDX requests using the same token.

https://jwt.io/

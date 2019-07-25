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
}
```

You only need to call GetAuthHeaderAsync() once to get the Json Web Token (JWT), which can be used until it expires. This means you can use the same JWT for all other TDX requests using the same token.

https://jwt.io/

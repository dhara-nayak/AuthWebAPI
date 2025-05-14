API for Registration And Login by using UserName and Password.
In Authcontroller.cs Create Token, Generate Key for encryption.
In Data folder Added MyDbContext.cs for register DbContext.
In AppSettings.json Add Connection String.
Register DbContext in Program.cs
In Data Folder Added MyDbContextFactory.cs, //To Fix Adding a Design-Time Factory, //EF core tools dont know how to configure MyDbContext at design time that's wjhy added this extra class into data. 
Add Migration- initial

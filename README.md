1.API for Registration And Login by using UserName and Password.
2.In Authcontroller.cs Create Token, Generate Key for encryption.
3.In Data folder Added MyDbContext.cs for register DbContext.
4.In AppSettings.json Add Connection String.
5.Register DbContext in Program.cs
6.In Data Folder Added MyDbContextFactory.cs, //To Fix Adding a Design-Time Factory, //EF core tools dont know how to configure MyDbContext at design time that's wjhy added this extra class into data. 
7.Add Migration- initial

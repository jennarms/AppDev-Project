GROUP MEMBERS
Piñon, Tommy 
Ramos, Jenna Nadine 
Samia, Anhelov 
Silo, Lucky 
Villegas, Edel Leandro 

FERRY BOOKING SYSTEM - METROLAYAG
A required project in Applications Development and Emerging Technologies, C# and MySQL based system. An automated booking management system. A website miniture of our suppose capstone. 

FEATURES
Authenticate/Log-in
View Dashboard
View Passenger Management
Generate Report
Passenger Management
Disembarking Management
Boarding Management
Main Admin Account Management
Station Account Management

PRESENTATION LINK
https://www.canva.com/design/DAGriQkAmT0/vFVrtaN68977y4rJ9exj6g/edit?utm_content=DAGriQkAmT0&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton

WEBSITE SETUP

Install Core Packages
Solution Explorer > Manage NuGet Packages.
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools

---------------------------------------------------------------

Upload Database
Tools > NuGet Package Manager > Package Manager Console
Add-Migration InitialCreate
Update-Database


---------------------------------------------------------------

Install Pagination Library
Tools > NuGet Package Manager > Package Manager Console
Install-Package X.PagedList.Mvc.Core

---------------------------------------------------------------

Install PDF Export Support
Manage NuGet Packages
Rotativa.AspNetCore

---------------------------------------------------------------

Add another table to database
Tools > NuGet Package Manager > Package Manager Console
Add-Migration AddUserTable
Update-Database

---------------------------------------------------------------
Password Hashing
Install-Package BCrypt.Net-Next

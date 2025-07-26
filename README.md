![Metroayag Logo](https://raw.githubusercontent.com/jennarms/AppDev-Project/refs/heads/master/MetroLayag/wwwroot/appdevAssets/Logo.png)

# 🚢 MetroLayag Ferry Booking System

A group project for **Applications Development and Emerging Technologies**, developed using **C#**, **ASP.NET Core Razor Pages**, and **MySQL**. MetroLayag is a web-based ferry booking and passenger management system that automates the booking, boarding, and disembarking process—designed as a mini version of our proposed capstone system.

---

## Group Members

- Piñon, Tommy  
- Ramos, Jenna Nadine  
- Samia, Anhelov  
- Silo, Lucky  
- Villegas, Edel Leandro  

---

## Features

- User Authentication / Log-in  
- Dashboard (Total Passenger Count)  
- Passenger Management  
- Disembarking Management  
- Boarding Management  
- Passenger Report (PDF Export + Filters)  
- Main Admin Account Management  
- Station Admin Account Management  

---

## Presentation Link

[Click here to view the presentation on Canva](https://www.canva.com/design/DAGriQkAmT0/vFVrtaN68977y4rJ9exj6g/edit?utm_content=DAGriQkAmT0&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton)

---

## System Setup Instructions

### 1. Install Required Core Packages

In **Solution Explorer**, right-click the project > **Manage NuGet Packages**  
Install the following packages:

- `Microsoft.EntityFrameworkCore`  
- `Microsoft.EntityFrameworkCore.SqlServer`  
- `Microsoft.EntityFrameworkCore.Tools`  

---

### 2. Upload the Database

Open the **Package Manager Console** and run:
Add-Migration InitialCreate
Update-Database

---

### 3. Install Pagination Library
Tools > NuGet Package Manager > Package Manager Console
Install-Package X.PagedList.Mvc.Core

---

### 4. Install PDF Export Support
Manage NuGet Packages
Rotativa.AspNetCore

---

### 5. Add another table to database
Tools > NuGet Package Manager > Package Manager Console
Add-Migration AddUserTable
Update-Database

---

### 6. Password Hashing
Install-Package BCrypt.Net-Next

---

## Notes

- Developed using **Visual Studio 2022**.
- Built with **ASP.NET Core Razor Pages** and **Entity Framework Core**.
- Role-Based Access Control (RBAC) is implemented:
  - `MainAdmin`: Full access to all features.
  - `StationAdmin`: Limited to station-specific pages (Dashboard, Booking, Disembarking, Report).
- Passenger Report supports:
  - Date-based filtering
  - Station filtering
  - PDF export

---

## Final Thoughts

MetroLayag demonstrates a modular, scalable, and practical approach to digitizing ferry terminal operations. It simplifies manual logs into a user-friendly web-based solution tailored for Metro Manila’s river ferry stations.

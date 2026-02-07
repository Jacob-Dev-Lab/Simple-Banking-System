# 🚀 Simple Banking System - .NET Console Application

A clean, structured, and object‑oriented **banking system** built using **C# and .NET**.  
This project demonstrates my understanding of **software architecture**, **SOLID principles**, **file‑based persistence**, and **clean code practices** as part of my journey to becoming a professional .NET developer.

---

## 📌 Features
- Customer onboarding  
- Open Savings or Current accounts  
- Deposit and withdraw funds  
- View transaction history  
- Update customer profile (last name, email, address)  
- Check account balance  
- Activate or deactivate an account  
- Input validation and error handling  
- File‑based data storage (Accounts, Customers, Transactions)

---

## 🧠 Concepts Applied

### Object‑Oriented Programming
- Encapsulation  
- Abstraction  
- Inheritance  
- Polymorphism  
- Separation of concerns  

### SOLID Principles
- **SRP** – Each class has a single responsibility  
- **OCP** – System is extendable without modifying core logic  
- **LSP** – SavingsAccount and CurrentAccount behave consistently as Accounts  
- **ISP** – Interfaces are small and focused  
- **DIP** – Repositories abstract away data access  

---

## 🏗 Architecture Overview

```
┌──────────────────────────────┐
│        Presentation Layer    │
│  (Console UI, Input Helpers) │
└───────────────┬──────────────┘
                │
┌───────────────▼──────────────┐
│         Application Layer    │
│   (Services, Business Logic) │
└───────────────┬──────────────┘
                │
┌───────────────▼──────────────┐
│       Infrastructure Layer   │
│ (File Repositories, Logging) │
└───────────────┬──────────────┘
                │
┌───────────────▼──────────────┐
│          Domain Layer        │
│ (Accounts, Customer, Models) │
└──────────────────────────────┘
```

---

## 🛠 Technologies Used
- C#  
- .NET Console Application  
- File I/O for persistence  
- OOP and SOLID design  
- Custom validation utilities  

---

## ▶ How to Run the Project
1. Clone the repository  
```
git clone https://github.com/Jacob-Dev-Lab/Simple-Banking-System
```
2. Open the solution in Visual Studio  
3. Build and run the project  
4. Follow the on‑screen menu to interact with the system  

---

## 📘 What I Learned
This project helped me strengthen my understanding of:

- Designing a multi‑layered .NET application  
- Applying SOLID principles in real code  
- Building abstractions using interfaces  
- Implementing the Repository Pattern  
- Managing file‑based persistence  
- Structuring a clean and maintainable codebase  
- Handling user input and validation  
- Modeling real‑world domains in C#  

---

## 🔧 Future Improvements
- Dependency Injection (DI)
- JSON‑based storage instead of text files  
- A proper service layer  
- Improved error handling  
- A more interactive console UI  
- Migration to a database (SQL or EF Core)  
- A web‑based version using ASP.NET Core  

---

## 🎯 Purpose
This project is part of my continuous learning journey as I build real‑world applications to sharpen my skills and grow into a professional .NET developer.

---

## 🔗 Connect
YouTube:  
`https://www.youtube.com/@dotnetdevjourneywithjacob` [(youtube.com in Bing)](https://www.bing.com/search?q="https%3A%2F%2Fwww.youtube.com%2F%40dotnetdevjourneywithjacob")
LinkedIn:  
`https://www.linkedin.com/in/jacoboluwajuwon` [(linkedin.com in Bing)](https://www.bing.com/search?q="https%3A%2F%2Fwww.linkedin.com%2Fin%2Fjacoboluwajuwon")

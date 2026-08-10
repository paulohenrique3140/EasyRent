# 🚗 Easy Rent

Easy Rent is a console-based vehicle rental system developed in **C#** as a learning project focused on Object-Oriented Programming (OOP), collections, LINQ, input validation, and business rules.

The application allows users to manage clients and vehicles, create and complete rentals, cancel reservations, and search rental history.

## 📌 Features

### Client Management
- Register clients
- Update client information
- Delete clients
- Find clients by CPF
- List registered clients
- Minimum age validation

### Vehicle Management
- Register vehicles
- Update daily rates
- Delete vehicles
- Find vehicles by license plate
- List registered vehicles
- Update vehicle mileage

### Rental Management
- Create vehicle rentals
- Add optional insurance
- Complete rentals
- Cancel reservations
- Track open and finished rentals
- Search rental history by client
- Search rental history by vehicle
- Calculate rental costs
- Calculate excess mileage penalties

## 💰 Business Rules

- Clients must be at least 18 years old.
- Rental duration must be greater than zero.
- Optional insurance costs **$50.00 per rental day**.
- The mileage allowance is **100 km per rental day**.
- Excess mileage is charged at **$1.20 per kilometer**.
- A vehicle's ending mileage cannot be lower than its initial mileage.
- Rentals can have the following statuses:
  - `Open`
  - `Finished`
  - `Canceled`

## 🛠️ Technologies and Concepts

- C#
- .NET
- Object-Oriented Programming
- Classes and Objects
- Encapsulation
- Properties
- Constructors
- Enums
- Lists (`List<T>`)
- LINQ
- Lambda Expressions
- Nullable Reference Types
- Input Validation
- Exception Handling
- StringBuilder

## 🏗️ Project Structure

```text
EasyRent
│
├── Program.cs
├── Client.cs
├── Vehicle.cs
├── Rental.cs
├── RentalService.cs
└── Enums.cs
```

### `Client`
Represents a customer and stores personal and driver's license information.

### `Vehicle`
Represents a vehicle available in the rental system.

### `Rental`
Represents a rental agreement and contains the main rental business rules and calculations.

### `RentalService`
Manages the application's collections and provides operations for searching clients, vehicles, and rentals.

### `Enums`
Contains the `CarBody` and `RentStatus` enumerations.

### `Program`
Contains the console interface and application menus.

## ▶️ Running the Project

Requirements:

- .NET SDK
- Visual Studio, Visual Studio Code, or another C# compatible IDE

Clone the repository:

```bash
git clone <repository-url>
```

Enter the project directory and run:

```bash
dotnet run
```

## 🎯 Project Purpose

This project was developed for educational purposes to practice C# fundamentals and apply Object-Oriented Programming concepts in a practical scenario.

The main goal was to evolve from a simple console application into a structured system using classes, collections, LINQ queries, validation, and business rules.

## 📚 What I Practiced

During development, I practiced:

- Modeling real-world entities with classes
- Separating responsibilities between classes
- Managing collections of objects
- Searching and filtering collections using LINQ
- Using lambda expressions
- Creating reusable validation methods
- Implementing business rules
- Managing object state with enums
- Refactoring repeated code
- Working with Git branches and commits

---

Developed as a C# learning project.
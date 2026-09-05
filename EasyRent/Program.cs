// Services
RentalServices rentalServices = new RentalServices();
ClientServices clientServices = new ClientServices();
VehicleServices vehicleServices = new VehicleServices();

// Main loop
while (true) 
{
    // Main menu
    Console.Clear();
    Console.WriteLine(@"
       ______
  ____/|_||_\`.__
 (   _    _ _   _\
 =`-(_)--(_)-'
");
    Console.WriteLine("\n=-=-=-=-=-=-= MAIN MENU =-=-=-=-=-=-=");

    Console.WriteLine("\n[1] Client" +
                      "\n[2] Vehicle" +
                      "\n[3] Rental" +
                      "\n[0] Exit\n");
    int menuOption = ReadMenuOption(3);

    if (menuOption == 0)
    {
        Console.WriteLine("Closing...");
        break;
    }


    switch (menuOption)
    {
        case 1:
            Console.Clear();
            Console.WriteLine(@"
      .-----------------------.
      |        CLIENT         |
      |-----------------------|
      |   O               O   |
      |  /|\             /|\  |
      |  / \             / \  |
      '-----------------------'

=-=-=-=-=-=-= CLIENT MENU =-=-=-=-=-=-=
");
            Console.WriteLine("\n[1] Register client" +
                  "\n[2] Update client email" +
                  "\n[3] Delete client" +
                  "\n[4] Find client" +
                  "\n[5] List clients" +
                  "\n[0] Return to main menu\n");

            menuOption = ReadMenuOption(5);

            switch (menuOption)
            {
                // Client menu
                case 1:
                    Console.WriteLine("\n[1] Personal customer\n[2] Business customer\n[0] Return to main menu\n");
                    menuOption = ReadMenuOption(2);
                    Console.Write("\nEnter client email: ");
                    string email = Console.ReadLine()!;
                    Console.Write("\nEnter client phone number: ");
                    string phone = Console.ReadLine()!;
                    if (menuOption == 1)
                    {
                        Console.Write("\nEnter client name: ");
                        string name = Console.ReadLine()!;
                        Console.Write("\nEnter client CPF: ");
                        string cpf = Console.ReadLine()!;
                        Console.Write("\nEnter client CNH: ");
                        string cnh = Console.ReadLine()!;
                        Console.Write("\nEnter client birth date [YYYY-MM-DD]: ");
                        DateTime birthDate = DateTime.Parse(Console.ReadLine()!);
                        Client client = new PersonalCustomer(email, phone, name, cpf, cnh, birthDate);
                        clientServices.Clients.Add(client);
                        Console.WriteLine("\nClient registered successfully.");
                    }
                    else
                    {
                        Console.Write("\nEnter company name: ");
                        string companyName = Console.ReadLine();
                        Console.Write("\nEnter CNPJ: ");
                        string cnpj = Console.ReadLine();
                        Console.Write("\nEnter opening company date: [YYYY-MM-DD]: ");
                        DateTime openingdate = DateTime.Parse(Console.ReadLine()!);
                        Client client = new BusinessCustomer(email, phone, companyName, cnpj, openingdate);
                        clientServices.Clients.Add(client);
                        Console.WriteLine("\nClient registered successfully.");
                    }
                    break;

                case 2:
                    Client? clientToUpdate = clientServices.SearchClient();

                    if (clientToUpdate != null)
                    {
                        Console.WriteLine(clientToUpdate.ShowClient());

                        Console.Write("\nEnter new client email: ");
                        string newEmail = Console.ReadLine()!;

                        clientToUpdate.Email = newEmail;

                        Console.WriteLine("\nClient name updated successfully.");
                    }

                    break;
                case 3:
                    Client? clientToDelete = clientServices.SearchClient();

                    if (clientToDelete != null)
                    {
                        Console.WriteLine(clientToDelete.ShowClient());

                        Console.Write("\nConfirm client deletion? [y/n]: ");
                        string? confirm = Console.ReadLine()?.ToLower();

                        if (confirm == "y")
                        {
                            clientServices.Clients.Remove(clientToDelete);
                        }
                    }
                    break;
                case 4:
                    Client? clientFound = clientServices.SearchClient();

                    if (clientFound != null)
                    {
                        Console.WriteLine(clientFound.ShowClient());
                        Console.ReadKey();
                    }
                    break;
                case 5:
                    clientServices.ShowClientList();
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
            break;

        case 2: // Vehicle menu
            Console.Clear();
            Console.WriteLine(@"
             ___________
          __/___________\__
         /  |  _     _  |  \
        /___|___________|___\
       |                   |
       |   (O)         (O)   |
       |_______|___|_________|
          \___/     \___/

=-=-=-=-=-=-= VEHICLE MENU =-=-=-=-=-=-=
");
            Console.WriteLine("\n[1] Register car" +
                              "\n[2] Update daily rate" +
                              "\n[3] Delete car" +
                              "\n[4] Find car" +
                              "\n[5] List cars" +
                              "\n[0] Return to main menu\n");
            menuOption = ReadMenuOption(5);
            switch (menuOption)
            {
                case 1:
                    Console.Write("\nEnter car model: ");
                    string? model = Console.ReadLine();
                    Console.Write("\nEnter car license plate: ");
                    string? licensePlate = Console.ReadLine();
                    Console.Write("Body style [1-Hatch / 2-Sedan / 3-SUV / 4-Utility]: ");
                    int carBody = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nEnter car daily rate: $ ");
                    double dailyRate = Convert.ToDouble(Console.ReadLine());
                    Console.Write("\nEnter car current mileage [km]: ");
                    int currentMileage = Convert.ToInt32(Console.ReadLine());
                    Vehicle vehicle = new Vehicle(model, licensePlate, (CarBody)carBody, dailyRate, currentMileage); 
                    vehicleServices.Vehicles.Add(vehicle); 
                    break;
                case 2:
                    Vehicle? vehicleToUpdate = vehicleServices.SearchVehicle();

                    if (vehicleToUpdate != null)
                    {
                        Console.WriteLine(vehicleToUpdate.ShowVehicle());
                        Console.Write("\nEnter new car daily rate to update: ");
                        vehicleToUpdate.DailyRate = Convert.ToDouble(Console.ReadLine());
                    }
                    break;
                case 3:
                    Vehicle? vehicleToDelete = vehicleServices.SearchVehicle();

                    if (vehicleToDelete != null)
                    {
                        Console.WriteLine(vehicleToDelete.ShowVehicle());

                        Console.Write("\nConfirm vehicle deletion? [y/n]: ");
                        string? confirm = Console.ReadLine()?.ToLower();

                        if (confirm == "y")
                        {
                            vehicleServices.Vehicles.Remove(vehicleToDelete);
                        }
                    }
                    break;
                case 4:
                    Vehicle? vehicleFound = vehicleServices.SearchVehicle();

                    if (vehicleFound != null)
                    {
                        Console.WriteLine(vehicleFound.ShowVehicle());
                        Console.ReadKey();
                    }
                    break;
                case 5:
                    vehicleServices.ShowVehicleList();
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
            break;

        case 3: // Rental menu
            Console.Clear();
            Console.WriteLine(@"
       ___________________________
      |      E A S Y  R E N T™    |
      |___________________________|
      |                           |
      |     RENTAL AGREEMENT      |
      |                           |
      | Client:  ______________   |
      | Vehicle: ______________   |
      |                           |
      | Signature: ____________   |
      |___________________________|

=-=-=-=-=-=-= RENTAL MENU =-=-=-=-=-=-=
");
            Console.WriteLine("\n[1] Rent a car" +
                              "\n[2] Complete rental" +
                              "\n[3] Cancel reservation" +
                              "\n[4] List rentals by client" +
                              "\n[5] List rentals by vehicle" +
                              "\n[0] Return to main menu\n");
            menuOption = ReadMenuOption(5);
            switch (menuOption)
            {
                case 1:
                    Client? clientToRent = clientServices.SearchClient();
                    if (clientToRent != null)
                    {
                        Console.WriteLine(clientToRent.ShowClient());
                        Console.WriteLine("\n-=-=- Car available list -=-=-");
                        vehicleServices.ShowVehicleList();
                        Console.Write("\nChoose a car to rental");
                        Vehicle? vehicleToRent = vehicleServices.SearchVehicle();
                        if (vehicleToRent != null)
                        {
                            Console.WriteLine(vehicleToRent.ShowVehicle());
                            Console.Write("\nEnter the number of rental days: ");
                            int rentalDays = Convert.ToInt32(Console.ReadLine());
                            Console.Write("\nWould you like to add insurance for $50.00 per day? [y/n]: ");
                            var insurance = Console.ReadLine().ToLower();
                            bool hasInsurance = insurance == "y";
                            Rental rental = new Rental(clientToRent, vehicleToRent, rentalDays, hasInsurance, vehicleToRent.CurrentMileage, RentStatus.Open);
                            rentalServices.Rentals.Add(rental);
                            Console.WriteLine("\nContract signed!" + rental.ShowOpenRental());
                            Console.ReadKey();
                            break;
                        }
                    }
                    break;
                case 2:
                    foreach(Rental rental in rentalServices.FindOpenRentals())
                    {
                        Console.WriteLine(rental.ShowOpenRental());
                    }
                    Rental? rentalToClose = rentalServices.SearchRentalToClose();
                    if(rentalToClose == null)
                    {
                        break;
                    }
                    Console.Write("\nEnter current car mileage [km]: ");
                    int currentMileage = Convert.ToInt32(Console.ReadLine());
                    rentalToClose.CloseRental(currentMileage);
                    Console.WriteLine("\nRental closed!");
                    Console.WriteLine(rentalToClose.ShowOpenRental());
                    Console.WriteLine(rentalToClose.ShowSummary(rentalToClose.Vehicle.CurrentMileage));
                    Console.ReadKey();
                    break;
                case 3:
                    foreach (Rental rental in rentalServices.FindOpenRentals())
                    {
                        Console.WriteLine(rental.ShowOpenRental());
                    }
                    Rental? rentalToCancel = rentalServices.SearchRentalToClose();
                    if (rentalToCancel == null)
                    {
                        break;
                    }
                    rentalToCancel.CancelRental();
                    Console.WriteLine("\n" + rentalToCancel.ShowSummary(0));
                    Console.ReadKey();
                    break;
                case 4:
                    Client? clientToListRentals = clientServices.SearchClient();
                    if(clientToListRentals == null)
                    {
                        break;
                    }
                    foreach (Rental rental in rentalServices.FindFinishedRentalsByClient(clientToListRentals.Email))
                    {
                        Console.WriteLine(rental.ShowOpenRental());
                        Console.WriteLine(rental.ShowSummary(rental.Vehicle.CurrentMileage));
                    }
                    Console.ReadKey();
                    break;
                case 5:
                    Vehicle? vehicleToListRentals = vehicleServices.SearchVehicle();
                    if(vehicleToListRentals == null)
                    {
                        break;
                    }
                    foreach (Rental rental in rentalServices.FindFinishedRentalsByVehicle(vehicleToListRentals.LicencePlate))
                    {
                        Console.WriteLine(rental.ShowOpenRental());
                        Console.WriteLine(rental.ShowSummary(rental.Vehicle.CurrentMileage));
                    }
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
            break;
    }
}

// Helper methods
static int ReadMenuOption(int opcaoMaxima)
{
    while (true)
    {
        Console.Write("\nEnter the desired option: ");
        string? entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out int opcao))
        {
            Console.WriteLine("\nInvalid option! Please enter numbers only.");
            continue;
        }

        if (opcao < 0 || opcao > opcaoMaxima)
        {
            Console.WriteLine("\nInvalid option! Please enter one of the listed options.");
            continue;
        }

        return opcao;
    }
}



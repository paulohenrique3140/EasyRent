// INSTANTIATING RENTALSERVICE OBJECT
RentalService rentalService = new RentalService();



while (true)
{
    Console.Clear();
    Console.WriteLine(@"
       ______
  ____/|_||_\`.__
 (   _    _ _   _\
 =`-(_)--(_)-'
");
    // DISPLAYING MAIN MENU
    Console.WriteLine("\n=-=-=-=-=-=-= MAIN MENU =-=-=-=-=-=-=");

    Console.WriteLine("\n[1] Client" +
                      "\n[2] Vehicle" +
                      "\n[3] Rental" +
                      "\n[0] Exit\n");
    int menuOption = RentalService.ReadMenuOption(3); // input validation

    if (menuOption == 0) // Option to exit program
    {
        Console.WriteLine("Closing...");
        break;
    }


    switch (menuOption)
    {
        case 1: // DISPLAYING CLIENT MENU
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
                  "\n[2] Update client name" +
                  "\n[3] Delete client" +
                  "\n[4] Find client" +
                  "\n[5] List clients" +
                  "\n[0] Return to main menu\n");

            menuOption = RentalService.ReadMenuOption(5);

            switch (menuOption)
            {
                case 1: // Registering a client
                    Console.Write("\nEnter client name: ");
                    string name = Console.ReadLine()!;

                    Console.Write("\nEnter client CPF: ");
                    string cpf = Console.ReadLine()!;

                    Console.Write("\nEnter client CNH: ");
                    string cnh = Console.ReadLine()!;

                    Console.Write("\nEnter client birth date [YYYY-MM-DD]: ");
                    DateTime birthDate = DateTime.Parse(Console.ReadLine()!);

                    Client client = new Client(name, cpf, cnh, birthDate);

                    rentalService.Clients.Add(client);

                    Console.WriteLine("\nClient registered successfully.");
                    break;

                case 2: // Updating client name
                    Client? clientToUpdate = rentalService.SearchClient();

                    if (clientToUpdate != null)
                    {
                        Console.WriteLine(clientToUpdate.ShowClient());

                        Console.Write("\nEnter new client name: ");
                        string newName = Console.ReadLine()!;

                        clientToUpdate.Name = newName;

                        Console.WriteLine("\nClient name updated successfully.");
                    }

                    break;
                case 3: // Deleting client register
                    Client? clientToDelete = rentalService.SearchClient();

                    if (clientToDelete != null)
                    {
                        Console.WriteLine(clientToDelete.ShowClient());

                        Console.Write("\nConfirm client deletion? [y/n]: ");
                        string? confirm = Console.ReadLine()?.ToLower();

                        if (confirm == "y")
                        {
                            rentalService.Clients.Remove(clientToDelete);
                        }
                    }
                    break;
                case 4: // Searching a client by CPF
                    Client? clientFound = rentalService.SearchClient();

                    if (clientFound != null)
                    {
                        Console.WriteLine(clientFound.ShowClient());
                        Console.ReadKey();
                    }
                    break;
                case 5: // Showing client list
                    rentalService.ShowClientList();
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
            break;

        case 2: // Displaying vehicle menu
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
            menuOption = RentalService.ReadMenuOption(5);
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
                    Vehicle vehicle = new Vehicle(model, licensePlate, (CarBody)carBody, dailyRate, currentMileage); // register vehicle
                    rentalService.Vehicles.Add(vehicle); // Add vehicle to the vehicle program list
                    break;
                case 2:// Uptading car daily rate
                    Vehicle? vehicleToUpdate = rentalService.SearchVehicle();

                    if (vehicleToUpdate != null)
                    {
                        Console.WriteLine(vehicleToUpdate.ShowVehicle());
                        Console.Write("\nEnter new car daily rate to update: ");
                        vehicleToUpdate.DailyRate = Convert.ToDouble(Console.ReadLine());
                    }
                    break;
                case 3:// Deleting vehicle register
                    Vehicle? vehicleToDelete = rentalService.SearchVehicle();

                    if (vehicleToDelete != null)
                    {
                        Console.WriteLine(vehicleToDelete.ShowVehicle());

                        Console.Write("\nConfirm vehicle deletion? [y/n]: ");
                        string? confirm = Console.ReadLine()?.ToLower();

                        if (confirm == "y")
                        {
                            rentalService.Vehicles.Remove(vehicleToDelete);
                        }
                    }
                    break;
                case 4:// Searching a car by licence plate
                    Vehicle? vehicleFound = rentalService.SearchVehicle();

                    if (vehicleFound != null)
                    {
                        Console.WriteLine(vehicleFound.ShowVehicle());
                        Console.ReadKey();
                    }
                    break;
                case 5:// Showing vehicle list
                    rentalService.ShowVehicleList();
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
            break;

        case 3: // DISPLAYING RENTAL MENU
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
            menuOption = RentalService.ReadMenuOption(5);
            switch (menuOption)
            {
                case 1:// Signing a contract
                    Client? clientToRent = rentalService.SearchClient();
                    if (clientToRent != null)
                    {
                        Console.WriteLine(clientToRent.ShowClient());
                        Console.WriteLine("\n-=-=- Car available list -=-=-");
                        rentalService.ShowVehicleList();
                        Console.Write("\nChoose one car to rental");
                        Vehicle? vehicleToRent = rentalService.SearchVehicle();
                        if (vehicleToRent != null)
                        {
                            Console.WriteLine(vehicleToRent.ShowVehicle());
                            Console.Write("\nEnter the number of rental days: ");
                            int rentalDays = Convert.ToInt32(Console.ReadLine());
                            Console.Write("\nWould you like to add insurance for $50.00 per day? [y/n]: ");
                            var insurance = Console.ReadLine().ToLower();
                            bool hasInsurance = insurance == "y";
                            Rental rental = new Rental(clientToRent, vehicleToRent, rentalDays, hasInsurance, vehicleToRent.CurrentMileage, RentStatus.Open);
                            rentalService.Rentals.Add(rental);
                            Console.WriteLine("\nContract signed!" + rental.ShowOpenRental());
                            Console.ReadKey();
                            break;
                        }
                    }
                    break;
                case 2: // Closing a open rental
                    foreach(Rental rental in rentalService.FindOpenRentals())
                    {
                        Console.WriteLine(rental.ShowOpenRental());
                    }
                    Rental? rentalToClose = rentalService.SearchRentalToClose();
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
                case 3:// canceling reservation
                    foreach (Rental rental in rentalService.FindOpenRentals())
                    {
                        Console.WriteLine(rental.ShowOpenRental());
                    }
                    Rental? rentalToCancel = rentalService.SearchRentalToClose();
                    if (rentalToCancel == null)
                    {
                        break;
                    }
                    rentalToCancel.CancelRental();
                    Console.WriteLine("\n" + rentalToCancel.ShowSummary(0));
                    Console.ReadKey();
                    break;
                case 4:// showing rentals list by client
                    Client? clientToListRentals = rentalService.SearchClient();
                    if(clientToListRentals == null)
                    {
                        break;
                    }
                    foreach (Rental rental in rentalService.FindFinishedRentalsByClient(clientToListRentals.Cpf))
                    {
                        Console.WriteLine(rental.ShowOpenRental());
                        Console.WriteLine(rental.ShowSummary(rental.Vehicle.CurrentMileage));
                    }
                    Console.ReadKey();
                    break;
                case 5:// showing rentals list by vehicle
                    Vehicle? vehicleToListRentals = rentalService.SearchVehicle();
                    if(vehicleToListRentals == null)
                    {
                        break;
                    }
                    foreach (Rental rental in rentalService.FindFinishedRentalsByVehicle(vehicleToListRentals.LicencePlate))
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



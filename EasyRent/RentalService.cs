class RentalService
{
    public List<Client> Clients { get; } = new List<Client>();
    public List<Vehicle> Vehicles { get; } = new List<Vehicle>();
    public List<Rental> Rentals { get; } = new List<Rental>();
    

    public void ShowClientList()
    {
        foreach(var client in Clients)
        {
            Console.WriteLine(client.ShowClient());
        }

    }

    public void ShowVehicleList()
    {
        foreach (var vehicle in Vehicles)
        {
            Console.WriteLine(vehicle.ShowVehicle());
        }
    }

    public void ShowRents()
    {
        foreach(var rent in Rentals)
        {
            Console.WriteLine(rent.ShowSummary(rent.Vehicle.CurrentMileage));
        }
    }

    public static int ReadMenuOption(int opcaoMaxima)
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

    public Client? FindClient(string? cpf)
    {
        return Clients.FirstOrDefault(client => client.Cpf == cpf);
    }

    public Client? SearchClient()
    {
        while (true)
        {
            Console.Write("\nEnter the client CPF [type r to return]: ");
            string? cpfSearch = Console.ReadLine();

            if (cpfSearch?.ToLower() == "r")
                return null;

            Client? clientFound = FindClient(cpfSearch);

            if(clientFound != null)
                return clientFound;

            Console.WriteLine("\nThere's no client with this CPF. Please try again.");
        }
    }

    public Vehicle? FindVehicle(string? licencePlate)
    {
        return Vehicles.FirstOrDefault(vehicle => vehicle.LicencePlate == licencePlate);
    }

    public Vehicle? SearchVehicle()
    {
        while (true)
        {
            Console.Write("\nEnter the car licence plate [type r to return]: ");
            string? licensePlateSearch = Console.ReadLine();

            if (licensePlateSearch?.ToLower() == "r")
                return null;

            Vehicle? vehicleFound = FindVehicle(licensePlateSearch);

            if (vehicleFound != null)
                return vehicleFound;

            Console.WriteLine("\nThere's no car with this license plate. Please try again.");
        }
    }

    public List<Rental> FindOpenRentals()
    {
        return Rentals.Where(rental => rental.Status == RentStatus.Open).ToList();
    }

    public List<Rental> FindFinishedRentals()
    {
        return Rentals.Where(rental => rental.Status == RentStatus.Finished).ToList();
    }

    public Rental? FindOpenRentalByClient(string? cpf)
    {
        return Rentals.FirstOrDefault(rental =>
                rental.Status == RentStatus.Open &&
                rental.Client?.Cpf == cpf);
    }

    public Rental? SearchRentalToClose()
    {
        while (true)
        {
            Console.Write("\nEnter the client CPF [type r to return]: ");
            string? cpfToSearch = Console.ReadLine();

            if (cpfToSearch?.ToLower() == "r")
                return null;

            Rental? rentalFound = FindOpenRentalByClient(cpfToSearch);

            if (rentalFound != null)
                return rentalFound;

            Console.WriteLine("\nThere's no open rental for this client!");
        }
    }

    public List<Rental> FindFinishedRentalsByClient(string? cpf)
    {
        return Rentals
            .Where(rental =>
                rental.Status == RentStatus.Finished &&
                rental.Client?.Cpf == cpf)
            .ToList();
    }

    public List<Rental> FindFinishedRentalsByVehicle(string? licensePlate)
    {
        return Rentals
            .Where(rental =>
                rental.Status == RentStatus.Finished &&
                rental.Vehicle?.LicencePlate == licensePlate)
            .ToList();
    }
}

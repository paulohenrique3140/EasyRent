using System.ComponentModel;

class RentalService
{
    public List<Client> Clients { get; } = new List<Client>();
    public List<Vehicle> Vehicles { get; } = new List<Vehicle>();
    public List<Rental> Rentals { get; } = new List<Rental>();


    public void ShowClientList()
    {
        foreach (var client in Clients)
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
        foreach (var rent in Rentals)
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

    public List<Client> FindClientsByName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new List<Client>();

        return Clients.Where(client => client.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public Client? SearchClient()
    {
        while (true)
        {
            Console.Write("\nEnter the client name [type r to return]: ");
            string? nameSearch = Console.ReadLine();

            if (nameSearch?.ToLower() == "r")
                return null;

            List<Client> clientsFound = FindClientsByName(nameSearch);

            if (clientsFound.Count == 0)
            {
                Console.WriteLine("\nNo clients were found. Please try again.");
                continue;
            }

            if (clientsFound.Count == 1)
            {
                return clientsFound[0];
            }

            Console.WriteLine("\nClients found:");

            for (int i = 0; i < clientsFound.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {clientsFound[i].Name} - CPF: {clientsFound[i].Cpf}");
            }

            Console.WriteLine("\nSelect a client: ");

            if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= clientsFound.Count)
            {
                return clientsFound[option - 1];
            }

            Console.WriteLine("\nInvalid option. Please try again.");
        }
    }

    public List<Vehicle> FindVehicleByModel(string? model)
    {
        if (string.IsNullOrWhiteSpace(model))
            return new List<Vehicle>();

        return Vehicles.Where(vehicle => vehicle.Model.Contains(model, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public Vehicle? SearchVehicle()
    {
        while (true)
        {
            Console.Write("\nEnter the car model [type r to return]: ");
            string? modelSearch = Console.ReadLine();

            if (modelSearch?.ToLower() == "r")
                return null;

            List<Vehicle> vehiclesFound = FindVehicleByModel(modelSearch);

            if (vehiclesFound.Count == 0)
            {
                Console.WriteLine("\nNo vehicle were found. Please try again.");
                continue;
            }

            if (vehiclesFound.Count == 1)
            {
                return vehiclesFound[0];
            }

            Console.WriteLine("\nVehicles found:");

            for (int i = 0; i < vehiclesFound.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {vehiclesFound[i].Model} - License plate: {vehiclesFound[i].LicencePlate}");
            }

            Console.WriteLine("\nSelect a car: ");

            if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= vehiclesFound.Count)
            {
                return vehiclesFound[option - 1];
            }
            Console.WriteLine("\nInvalid option. Please try again.");

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

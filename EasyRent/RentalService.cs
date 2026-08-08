class RentalService
{
    public List<Client> Clients { get; } = new List<Client>();
    public List<Vehicle> Vehicles { get; } = new List<Vehicle>();
    public List<Rental> Rentals { get; } = new List<Rental>();

    public static void ShowClientList(List<Client> clients)
    {
        foreach(var client in clients)
        {
            Console.WriteLine(client.ShowClient());
        }
    }

    public static void ShowVehicleList(List<Vehicle> vehicles)
    {
        foreach (var vehicle in vehicles)
        {
            Console.WriteLine(vehicle.ShowVehicle());
        }
    }

    public static void ShowRents(List<Rental> rentals)
    {
        foreach(var rent in rentals)
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
}

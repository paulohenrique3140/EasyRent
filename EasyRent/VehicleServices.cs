public class VehicleServices
{
    // Properties
    public List<Vehicle> Vehicles { get; } = new List<Vehicle>();

    // Methods
    public void ShowVehicleList()
    {
        foreach (var vehicle in Vehicles)
        {
            Console.WriteLine(vehicle.ShowVehicle());
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
}

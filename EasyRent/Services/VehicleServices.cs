
public class VehicleServices
{
    // Properties
    VehicleRepository repository = new VehicleRepository();

    // Methods
    public void CreateVehicle(Vehicle vehicle)
    {
        repository.AddVehicle(vehicle);
    }

    public void UpdateVehicleDailyRate(string? licensePlate, double dailyRate)
    {
        if(FindVehicleByLicensePlate(licensePlate) == null)
        {
            Console.WriteLine("There's no vehicle with this license plate. Please try again.");
        }
        else
        {
            Vehicle vehicle = FindVehicleByLicensePlate(licensePlate);
            repository.UpdateVehicle(vehicle, dailyRate);
            Console.WriteLine(FindVehicleByLicensePlate(licensePlate).ShowVehicle());
        }
    }

    public Vehicle? FindVehicleByLicensePlate(string? licensePlate)
    {
        return repository.FindVehicleByLicensePlate(licensePlate);
    }

    public void DeleteVehicle(Vehicle vehicle)
    {
        repository.DeleteVehicle(vehicle);
    }
    public void GetVehicles()
    {
        foreach (var vehicle in repository.GetVehicles())
        {
            Console.WriteLine(vehicle.ShowVehicle());
        }
    }
}

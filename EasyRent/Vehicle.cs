public class Vehicle
{
    // vehicle properties
    public string? Model { get; set; }
    public string? LicencePlate { get; set; }
    public CarBody CarBody { get; set; }
    public double DailyRate { get; set; }
    public int CurrentMileage { get; set; }

    public Vehicle(string? model, string? placa, CarBody carBody, double dailyRate, int currentMileage) // vehicle constructor
    {
        Model = model;
        LicencePlate = placa;
        CarBody = carBody;
        DailyRate = dailyRate;
        CurrentMileage = currentMileage;
    }

    public Vehicle(){ } // Constructor to create an empty object

    public void UpdateMileage(int CurrentMileage) // Updates the vehicle's mileage when closing the rental
    {
        this.CurrentMileage = CurrentMileage;
    }

    public string ShowVehicle() // Method to return a vehicle object
    {
        return $"\nModel: {Model}\nLicense Plate: {LicencePlate}\nCar Body: {CarBody}\nDaily Rate: $ {DailyRate:F2}\nCurrent Mileage: {CurrentMileage} kms";
    }
}
public class Vehicle
{
    // Properties
    public int Id { get; set; }
    public string? Model { get; set; }
    public string? LicencePlate { get; set; }
    public CarBody CarBody { get; set; }
    public double DailyRate { get; set; }
    public int CurrentMileage { get; set; }

    // Constructors
    public Vehicle(string? model, string? placa, CarBody carBody, double dailyRate, int currentMileage)
    {
        Model = model;
        LicencePlate = placa;
        CarBody = carBody;
        DailyRate = dailyRate;
        CurrentMileage = currentMileage;
    }

    public Vehicle(){ }

    // Methods
    public void UpdateMileage(int CurrentMileage) 
    {
        this.CurrentMileage = CurrentMileage;
    }

    public string ShowVehicle()
    {
        return $"\nModel: {Model}\nLicense Plate: {LicencePlate}\nCar Body: {CarBody}\nDaily Rate: $ {DailyRate:F2}\nCurrent Mileage: {CurrentMileage} kms";
    }
}
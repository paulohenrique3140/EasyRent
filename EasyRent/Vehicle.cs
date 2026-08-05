class Vehicle
{
    // PROXIMOS PASSOS: MELHORAR O MÉTODO DE ATUALIZAR KM E CRIAR UM VALIDADOR DE PLACA

    // vehicle properties
    public string? Model { get; set; }
    public string? LicencePlate { get; set; }
    public CarBody CarBody { get; set; }
    public double DailyRate { get; set; }
    public int CurrentMileage { get; set; }

    public Vehicle(string? modelo, string? placa, CarBody carroceria, double valorDiaria, int kmAtual) // vehicle constructor
    {
        Model = modelo;
        LicencePlate = placa;
        CarBody = carroceria;
        DailyRate = valorDiaria;
        CurrentMileage = kmAtual;
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
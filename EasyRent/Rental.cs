using System.Text;

class Rental
{
    // rental properties
    public Client? Client { get; }
    public Vehicle? Vehicle { get; }
    public int RentalDays { get; private set; }
    public bool HasInsurance { get; private set; }
    public int InicialMileage { get; private set; }
    public RentStatus Status { get; private set; }

    public Rental(Client client, Vehicle vehicle, int rentalDays, bool hasInsurance, int currentMileage, RentStatus status) // rental constructor
    {
        Client = client;
        Vehicle = vehicle;
        RentalDays = rentalDays;
        HasInsurance = hasInsurance;
        InicialMileage = currentMileage;
        Status = status;
    }

    public Rental() { } // Constructor to create an empty object

    public double CalculateBaseValue(double daily) // Method to calculate the rental base value
    {
        double total = RentalDays * daily;
        return total;
    }

    public double CalculateInsurance() // Method to validate the insurance of the rental
    {
        return HasInsurance ? RentalDays * 50.00 : 0;
    }

    public double CalculatePenalty(int initialMileage) // Method to calculate penality
    {
        double penalty = 0;
        int totalMileage = initialMileage - InicialMileage;
        if (totalMileage / RentalDays > 100)
        {
            penalty = (totalMileage - 100 * RentalDays) * 1.2;
        }
        return penalty;
    }

    public double CalculateTotal(double daily, int currentMileage) // Method to calculate total value
    {
        return CalculateBaseValue(daily) + CalculateInsurance() + CalculatePenalty(currentMileage);
    }

    public void CloseRental(int kmFinal) // Method to close the rental
    {
        Vehicle.UpdateMileage(kmFinal);
        Status = RentStatus.Finished;
    }

    public void CancelRental() // Method to cancel the rental
    {
        Status = RentStatus.Canceled;
    }

    public string ShowSummary(int currentMileage) // Method to show the details of the rental
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"\n### RENTAL SUMMARY ###\n" +
                          $"\nClient name: {Client.Name}" +
                          $"\nVehicle: {Vehicle.Model}" +
                          $"\nRental days: {RentalDays}" +
                          $"\nDaily rate: $ {Vehicle.DailyRate:F2}" +
                          $"\nRental status: {Status}" +
                          $"\nVehicle initial mileage: {InicialMileage}");
        if (Status == RentStatus.Canceled)
        {
            sb.Clear();
            sb.Append("\nYour reservation has been canceled!");
        }
        else if (Status == RentStatus.Finished)
        {
            sb.Append($"\nVehicle final mileage: {Vehicle.CurrentMileage}");
            sb.Append($"\nBase amount: $ {CalculateBaseValue(Vehicle.DailyRate):F2}");
            if (CalculatePenalty(currentMileage) > 0)
            {
                sb.Append($"\nExcess mileage total [limit 100 km per day]: {((Vehicle.CurrentMileage - InicialMileage) - (100 * RentalDays))} km" +
                          $"\nTotal fine [$ 1.20 per excess km]: $ {CalculatePenalty(currentMileage):F2}");

            }
            if (HasInsurance)
            {
                sb.Append($"\nInsurance fee: $ {CalculateInsurance():F2}");
            }
            sb.Append($"\n\n### GRAND TOTAL: $ {CalculateTotal(Vehicle.DailyRate, Vehicle.CurrentMileage):F2} ###");
        }
        return sb.ToString();
    }
}
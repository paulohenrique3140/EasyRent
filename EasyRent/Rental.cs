using System.Text;

public class Rental
{
    // Properties
    public Client? Client { get; }
    public Vehicle? Vehicle { get; }
    private int rentalDays;
    public int RentalDays 
    {
        get { return rentalDays; }
        set
        {
            while (value <= 0) // Validation
            {
                throw new ArgumentException("It's not possible conclude your reservation with 0 dailys");
            }
            rentalDays = value;
        }
    }
    public bool HasInsurance { get; private set; }
    public int InicialMileage { get; private set; }
    public RentStatus Status { get; private set; }

    // Constructors
    public Rental(Client client, Vehicle vehicle, int rentalDays, bool hasInsurance, int currentMileage, RentStatus status) 
    {
        Client = client;
        Vehicle = vehicle;
        RentalDays = rentalDays;
        HasInsurance = hasInsurance;
        InicialMileage = currentMileage;
        Status = status;
    }

    public Rental() { }

    // Methods
    public double CalculateBaseValue(double daily)
    {
        double total = RentalDays * daily;
        return total;
    }

    public double CalculateInsurance()
    {
        return HasInsurance ? RentalDays * 50.00 : 0;
    }

    public double CalculatePenalty(int currentMileage)
    {
        double penalty = 0;
        int totalMileage = currentMileage - InicialMileage;
        if (totalMileage / RentalDays > 100)
        {
            penalty = (totalMileage - 100 * RentalDays) * 1.2;
        }
        return penalty;
    }

    public double CalculateTotal(double daily, int currentMileage)
    {
        return CalculateBaseValue(daily) + CalculateInsurance() + CalculatePenalty(currentMileage);
    }

    public bool CloseRental(int endingMileage)
    {
        if (endingMileage >= Vehicle.CurrentMileage)
        {
            Vehicle.UpdateMileage(endingMileage);
            Status = RentStatus.Finished;
            return true;
        }
        return false;
    }
    public void CancelRental()
    {
        Status = RentStatus.Canceled;
    }

    public string ShowOpenRental()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"\n### RENTAL SUMMARY ###\n" +
                          $"\nClient ID: {Client.Id}" +
                          $"\nVehicle: {Vehicle.Model}" +
                          $"\nRental days: {RentalDays}" +
                          $"\nDaily rate: $ {Vehicle.DailyRate:F2}" +
                          $"\nRental status: {Status}" +
                          $"\nVehicle initial mileage: {InicialMileage}");
        return sb.ToString();
    }

    public string ShowSummary(int currentMileage)
    {
        StringBuilder sb = new StringBuilder();
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
using System.Text;

public class DailyRental : Rental
{
    // Properties
    public int InitialMileage { get; private set; }
    public bool HasInsurance { get; private set; }

    // Constructor
    public DailyRental(Client client, Vehicle vehicle, int rentalDays, RentStatus status, int inicialMileage, bool hasInsurance) : base(client, vehicle, rentalDays, status)
    {
        InitialMileage = inicialMileage;
        HasInsurance = hasInsurance;
    }

    public DailyRental() { }

    // Methods
    public override double CalculateBaseValue()
    {
        return RentalDays * Vehicle.DailyRate;
    }

    public double CalculateInsurance()
    {
        return HasInsurance ? RentalDays * 50.00 : 0;
    }

    public double CalculatePenalty()
    {
        double penalty = 0;
        int totalMileage = Vehicle.CurrentMileage - InitialMileage;
        if (totalMileage / RentalDays > 100)
        {
            penalty = (totalMileage - 100 * RentalDays) * 1.2;
        }
        return penalty;
    }

    public override double CalculateTotal()
    {
        double total = CalculateBaseValue() + CalculateInsurance() + CalculatePenalty();
        if(Client is PersonalCustomer)
        {
            PersonalCustomer pc = (PersonalCustomer)Client;
            if (pc.RideshareDriver)
            {
                return total - total * 0.1;
            }
        }
        return total;
    }

    public override string ShowSummary(int currentMileage)
    {
        StringBuilder sb = new StringBuilder();
        if (Status == RentStatus.Canceled)
        {
            sb.Clear();
            sb.Append("\nThis reservation has been canceled!");
        }
        else if (Status == RentStatus.Finished)
        {
            sb.Append($"\nVehicle final mileage: {Vehicle.CurrentMileage}");
            sb.Append($"\nBase amount: $ {CalculateBaseValue():F2}");
            if (CalculatePenalty() > 0)
            {
                sb.Append($"\nExcess mileage total [limit 100 km per day]: {((Vehicle.CurrentMileage - InitialMileage) - (100 * RentalDays))} km" +
                          $"\nTotal fine [$ 1.20 per excess km]: $ {CalculatePenalty():F2}");

            }
            if (HasInsurance)
            {
                sb.Append($"\nInsurance fee: $ {CalculateInsurance():F2}");
            }
            sb.Append($"\n\n### GRAND TOTAL: $ {CalculateTotal():F2} ###");
        }

        return sb.ToString();
    }
}

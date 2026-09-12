using System.Text;

public class MonthlyRental : Rental
{
    // Properties
    public bool Extended { get; set; }

    // Constructors

    public MonthlyRental()
    {
    }

    public MonthlyRental(Client client, Vehicle vehicle, int rentalDays, RentStatus status, bool extended) : base(client, vehicle, rentalDays, status)
    {
        Extended = extended;
    }
    
    // Methods
    public override double CalculateBaseValue()
    {
        return RentalDays * (Vehicle.DailyRate - (Vehicle.DailyRate * 0.15));
    }

    public double CalculateExtended()
    {
        if (Extended)
        {
            return 0.05;
        }
        return 0;
    }

    public override double CalculateTotal()
    {
        double total = CalculateBaseValue() - (CalculateBaseValue() * CalculateExtended());
        if (Client is PersonalCustomer)
        {
            PersonalCustomer pc = (PersonalCustomer)Client;
            if (pc.RideshareDriver)
            {
                return total - (total * 0.1);
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
            sb.Append($"\nBase amount: $ {CalculateBaseValue():F2}");
            if (Extended)
            {
                Console.WriteLine($"Discount to extended contract (5%): - $ {CalculateBaseValue() * CalculateExtended():F2}");
            }
            
            sb.Append($"\n\n### GRAND TOTAL: $ {CalculateTotal():F2} ###");
        }

        return sb.ToString();
    }
}

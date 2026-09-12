using System.Text;

public abstract class Rental : IRental
{
    // Properties

    public int Id { get; set; }
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
    
    public RentStatus Status { get; private set; }

    // Constructors
    public Rental(Client client, Vehicle vehicle, int rentalDays, RentStatus status) 
    {
        Client = client;
        Vehicle = vehicle;
        RentalDays = rentalDays;
        Status = status;
    }

    protected Rental() { }

    // Methods
    public abstract double CalculateBaseValue();

    public abstract double CalculateTotal();

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
                          $"\nRental status: {Status}");
        return sb.ToString();
    }

    public abstract string ShowSummary(int currentMileage);
}
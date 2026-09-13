using System.Text;

public abstract class Rental : IRental
{
    // Properties

    public int Id { get; set; }
    public Client? Client { get; set; }
    public Vehicle? Vehicle { get; set; }
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
    
    public RentStatus Status { get; set; }

    VehicleRepository vehicleRepository = new VehicleRepository();
    RentalRepository rentalRepository = new RentalRepository();

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

    public abstract string ShowSummary();
}
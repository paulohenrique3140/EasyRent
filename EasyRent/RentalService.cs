public class RentalServices
{
    // Properties
    public List<Rental> Rentals { get; } = new List<Rental>();

    // Methods
    public void ShowRents()
    {
        foreach (var rent in Rentals)
        {
            Console.WriteLine(rent.ShowSummary(rent.Vehicle.CurrentMileage));
        }
    }  

    public List<Rental> FindOpenRentals()
    {
        return Rentals.Where(rental => rental.Status == RentStatus.Open).ToList();
    }

    public List<Rental> FindFinishedRentals()
    {
        return Rentals.Where(rental => rental.Status == RentStatus.Finished).ToList();
    }

    public Rental? FindOpenRentalByClient(string? email)
    {
        return Rentals.FirstOrDefault(rental =>
                rental.Status == RentStatus.Open &&
                rental.Client?.Email == email);
    }

    public Rental? SearchRentalToClose()
    {
        while (true)
        {
            Console.Write("\nEnter the client email [type r to return]: ");
            string? emailToSearch = Console.ReadLine();

            if (emailToSearch?.ToLower() == "r")
                return null;

            Rental? rentalFound = FindOpenRentalByClient(emailToSearch);

            if (rentalFound != null)
                return rentalFound;

            Console.WriteLine("\nThere's no open rental for this client!");
        }
    }

    public List<Rental> FindFinishedRentalsByClient(string? email)
    {
        return Rentals
            .Where(rental =>
                rental.Status == RentStatus.Finished &&
                rental.Client?.Email == email)
            .ToList();
    }

    public List<Rental> FindFinishedRentalsByVehicle(string? licensePlate)
    {
        return Rentals
            .Where(rental =>
                rental.Status == RentStatus.Finished &&
                rental.Vehicle?.LicencePlate == licensePlate)
            .ToList();
    }
}

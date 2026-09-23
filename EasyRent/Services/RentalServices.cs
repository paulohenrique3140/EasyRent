public class RentalServices
{
    // Properties
    public RentalRepository rentalRepository = new RentalRepository();
    public VehicleRepository vehicleRepository = new VehicleRepository();
    public List<Rental> Rentals { get; } = new List<Rental>();

    // Methods
    public void AddRental(Rental rent)
    {
        rentalRepository.CreateRental(rent);
    }

    public Rental? FindRentalToClose(Client client)
    {
        return rentalRepository.GetRentalToClose(client);
    }

    public bool CloseRental(int endingMileage, Vehicle? vehicle, Rental? rental)
    {
        if (endingMileage >= vehicle.CurrentMileage)
        {
            vehicleRepository.UpdateMileage(vehicle, endingMileage);
            rentalRepository.UpdateStatus(rental);
            rental.Status = RentStatus.Finished;
            return true;
        }
        return false;
    }

    public List<Rental>? FindOpenRentals()
    {
        return rentalRepository.GetOpenRentals();
    }

    public void CancelReservation(Rental rental)
    {
        rentalRepository.CancelRental(rental);
    }


    public void ShowRents()
    {
        foreach (var rent in Rentals)
        {
            Console.WriteLine(rent.ShowSummary());
        }
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

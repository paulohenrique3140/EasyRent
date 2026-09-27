public class RentalServices
{
    // Properties
    public RentalRepository rentalRepository = new RentalRepository();
    public VehicleRepository vehicleRepository = new VehicleRepository();
    
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

    public List<Rental>? ListFinishedRentals(int clientId)
    {
        return rentalRepository.GetRentalsByClient(clientId);
    }    
}

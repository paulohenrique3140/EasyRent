using Microsoft.Data.SqlClient;
public class RentalRepository
{
    private readonly string connectionString = "Server=PAULO;DataBase=EASY_RENT;Integrated Security=True;TrustServerCertificate=True;";

    public void CreateRental(Rental rental)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        if(rental is DailyRental)
        {
            DailyRental dr = (DailyRental)rental;
            string sqlCommand = @"
                INSERT INTO DAILY_RENTAL
                (ClientId, VehicleId, RentalDays, Status, InitialMileage, HasInsurance)
                OUTPUT INSERTED.Id                
                VALUES
                (@ClientId, @VehicleId, @RentalDays, @Status, @InitialMileage, @HasInsurance);
          ";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            command.Parameters.AddWithValue("@ClientId", dr.Client.Id);
            command.Parameters.AddWithValue("@VehicleId", dr.Vehicle.Id);
            command.Parameters.AddWithValue("@RentalDays", dr.RentalDays);
            command.Parameters.AddWithValue("@Status", Convert.ToInt32(dr.Status));
            command.Parameters.AddWithValue("@InitialMileage", dr.InitialMileage);
            command.Parameters.AddWithValue("@HasInsurance", dr.HasInsurance);

            int id = Convert.ToInt32(command.ExecuteScalar());
            dr.Id = id;
        }

        if(rental is MonthlyRental)
        {
            MonthlyRental mr = (MonthlyRental)rental;
            string sqlCommand = @"
                INSERT INTO DAILY_RENTAL
                (ClientId, VehicleId, RentalDays, Status, Extended)
                OUTPUT INSERTED.Id                
                VALUES
                (@ClientId, @VehicleId, @RentalDays, @Status, @Extended);
          ";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            command.Parameters.AddWithValue("@ClientId", mr.Client.Id);
            command.Parameters.AddWithValue("@VehicleId", mr.Vehicle.Id);
            command.Parameters.AddWithValue("@RentalDays", mr.RentalDays);
            command.Parameters.AddWithValue("@Status", Convert.ToInt32(mr.Status));
            command.Parameters.AddWithValue("@InitialMileage", mr.Extended);
            
            int id = Convert.ToInt32(command.ExecuteScalar());
            mr.Id = id;
        }
        
    }

}

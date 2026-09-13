using Microsoft.Data.SqlClient;
public class RentalRepository
{
    private readonly string connectionString = "Server=PAULO;DataBase=EASY_RENT;Integrated Security=True;TrustServerCertificate=True;";

    VehicleRepository vehicleRepository = new VehicleRepository();
    public void CreateRental(Rental rental)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        if (rental is DailyRental)
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

        if (rental is MonthlyRental)
        {
            MonthlyRental mr = (MonthlyRental)rental;
            string sqlCommand = @"
                INSERT INTO MONTHLY_RENTAL
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
            command.Parameters.AddWithValue("@Extended", mr.Extended);

            int id = Convert.ToInt32(command.ExecuteScalar());
            mr.Id = id;
        }
    }

    public Rental? GetRentalToClose(Client client)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        List<Rental> rentals = new List<Rental>();

        string sqlCommand = @"
            SELECT * FROM DAILY_RENTAL WHERE ClientId = @Id AND STATUS = 1;
        ";

        SqlCommand command = new SqlCommand(sqlCommand, connection);
        command.Parameters.AddWithValue("@Id", client.Id);
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            DailyRental dr = new DailyRental();
            dr.Id = Convert.ToInt32(reader["Id"]);
            dr.Client = client;
            dr.Vehicle = vehicleRepository.FindVehicleById(Convert.ToInt32(reader["VehicleId"]));
            dr.RentalDays = Convert.ToInt32(reader["RentalDays"]);
            dr.Status = (RentStatus)Convert.ToInt32(reader["Status"]);
            dr.InitialMileage = Convert.ToInt32(reader["InitialMileage"]);
            dr.HasInsurance = Convert.ToBoolean(reader["HasInsurance"]);
            return dr;
        }
        reader.Close();

        string sqlCommand2 = @"
            SELECT * FROM MONTHLY_RENTAL WHERE ClientId = @Id and STATUS = 1;
        ";

        SqlCommand command2 = new SqlCommand(sqlCommand2, connection);
        command2.Parameters.AddWithValue("@Id", client.Id);
        reader = command2.ExecuteReader();
        if (reader.Read())
        {
            MonthlyRental mr = new MonthlyRental();
            mr.Id = Convert.ToInt32(reader["Id"]);
            mr.Client = client;
            mr.Vehicle = vehicleRepository.FindVehicleById(Convert.ToInt32(reader["VehicleId"]));
            mr.RentalDays = Convert.ToInt32(reader["RentalDays"]);
            mr.Status = (RentStatus)Convert.ToInt32(reader["Status"]);
            mr.Extended = Convert.ToBoolean(reader["Extended"]);
            return mr;
        }

        return null;
    }

    public void UpdateStatus(Rental? rental)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        if (rental is DailyRental)
        {
            string sqlCommand = @"
              UPDATE DAILY_RENTAL
              SET Status = 2
              WHERE Id = @Id
             ";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            command.Parameters.AddWithValue("@Id", rental.Id);
            command.ExecuteNonQuery();
        }

        else if (rental is MonthlyRental)
        {
            string sqlCommand = @"
              UPDATE MONTHLY_RENTAL
              SET Status = 2
              WHERE Id = @Id
             ";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            command.Parameters.AddWithValue("@Id", rental.Id);
            command.ExecuteNonQuery();
        }
    }
}

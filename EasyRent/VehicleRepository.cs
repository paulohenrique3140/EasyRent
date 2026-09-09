using Microsoft.Data.SqlClient;

public class VehicleRepository
{
    private readonly string connectionString = "Server=PAULO;DataBase=EASY_RENT;Integrated Security=True;TrustServerCertificate=True;";

    public void AddVehicle(Vehicle vehicle)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlVehicle = @"
            INSERT INTO VEHICLE (Model, License_Plate, CarBody, DailyRate, CurrentMileage)
            OUTPUT INSERTED.Id
            VALUES (@Model, @License_Plate, @CarBody, @DailyRate, @CurrentMileage)
        ";

        SqlCommand sqlCommand = new SqlCommand(sqlVehicle, connection);
        sqlCommand.Parameters.AddWithValue("@Model", vehicle.Model);
        sqlCommand.Parameters.AddWithValue("@License_Plate", vehicle.LicencePlate);
        sqlCommand.Parameters.AddWithValue("@CarBody", (int)vehicle.CarBody);
        sqlCommand.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);
        sqlCommand.Parameters.AddWithValue("@CurrentMileage", vehicle.CurrentMileage);
        int id = Convert.ToInt32(sqlCommand.ExecuteScalar());
        vehicle.Id = id;
    }
}

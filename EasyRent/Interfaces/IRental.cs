public interface IRental
{
    public double CalculateBaseValue();
    public double CalculateTotal();
    public bool CloseRental(int endingMileage);
    public void CancelRental();
    public string ShowOpenRental();
    public string ShowSummary(int currentMileage);
}

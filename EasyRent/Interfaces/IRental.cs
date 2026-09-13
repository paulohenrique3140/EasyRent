public interface IRental
{
    public double CalculateBaseValue();
    public double CalculateTotal();
    public void CancelRental();
    public string ShowOpenRental();
    public string ShowSummary();
}

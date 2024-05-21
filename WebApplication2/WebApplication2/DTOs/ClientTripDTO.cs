namespace WebApplication2.DTOs;

public class ClientTripDTO
{
    public int IdClient { get; set; }
    public int IdTrip { get; set; }
    public DateTime RegisteredAt =  DateTime.Now;
    public DateTime? PaymentDate { get; set; }
}
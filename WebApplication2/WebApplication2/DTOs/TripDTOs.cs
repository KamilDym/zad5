namespace WebApplication2.DTOs;

public class TripDTOs
{
    // public int IdTrip { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public IEnumerable<CountriesDTO> Countries { get; set; }
    public IEnumerable<ClientsDTO> Clients { get; set; }
}

public class CountriesDTO
{
    public string Name { get; set; }
}
public class ClientsDTO
{
    public string FirstName { get; set; }
    public string LasttName { get; set; }
}


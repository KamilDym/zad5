using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace WebApplication2.DTOs;

public class ClientDTO
{
    public String firstName { get; set; }
    public String lastName { get; set; }
    public String email { get; set; }
    public  String telephone { get; set; }
    public  String pesel { get; set; }
    public String tripName { get; set; }
    public DateTime? paymentDate { get; set; }
}
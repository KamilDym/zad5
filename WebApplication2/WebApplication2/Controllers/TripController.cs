using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Context;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripController(S25005Context context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTripsAsync()
    {
        var trip = await context.Trips.Select(t => new TripDTOs
        {
            Name = t.Name,
            Description = t.Description,
            DateFrom = t.DateFrom,
            DateTo = t.DateTo,
            MaxPeople = t.MaxPeople,
            Countries = t.IdCountries.Select(c => new CountriesDTO
            {
                Name = c.Name
            }),
            Clients = t.ClientTrips.Select(cl => new ClientsDTO
            {
                FirstName = cl.IdClientNavigation.FirstName,
                LasttName = cl.IdClientNavigation.LastName,
            })
        }).OrderByDescending(t => t.DateFrom).ToListAsync();
            
        return Ok(trip);
    }

    [HttpDelete]
    [Route("clients/{idClient}")]
    public async Task<IActionResult> DeleteClientAsync(int idClient)
    {
        var tripAssigned = await context.ClientTrips.FirstOrDefaultAsync(ct =>ct.IdClient == idClient);
        if (tripAssigned is not null)
        {
            return BadRequest();
        }
        
        var clientCheck = await context.Clients.FindAsync(idClient);
        if (clientCheck is null)
        {
            return NotFound();
        }
        context.Clients.Remove(clientCheck);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    [Route("{idTrip}/clients")]
    public async Task<IActionResult> AssingsTripClient(int idTrip,[FromBody] ClientDTO dto )
    {
        
        var clientCheck = await context.Clients.CountAsync(c => c.Pesel == dto.pesel);
        if (clientCheck == 0)
        {
            var nextClientId = context.Clients.MaxAsync(c => c.IdClient).Result;
            await context.Clients.AddAsync(new Client
            {
                IdClient = nextClientId+1,
                FirstName = dto.firstName,
                LastName = dto.lastName,
                Email = dto.email,
                Telephone = dto.email,
                Pesel = dto.pesel
            });
            await context.SaveChangesAsync();
        }

        var getClientId = context.Clients.Where(c => c.Pesel == dto.pesel).Select(c=> c.IdClient).Single();
        var tripAssigned = await context.ClientTrips.Where(c=>c.IdClient == getClientId).Where(c=>c.IdTrip == idTrip).CountAsync();
        if (tripAssigned != 0)
        {
            return BadRequest();
        }
        
        
        
        
        
        return Created();
    }
    
    
    
}
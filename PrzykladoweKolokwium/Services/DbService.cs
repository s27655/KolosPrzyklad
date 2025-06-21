using Microsoft.EntityFrameworkCore;
using PrzykładoweKolokwium.Data;
using PrzykładoweKolokwium.DTOs;
using PrzykładoweKolokwium.Exceptions;
using PrzykładoweKolokwium.Models;

namespace PrzykładoweKolokwium.Services;


public interface IDbService
{
    public Task<IEnumerable<GetPolitykResponse>> GetPolitykDetails();
    public Task<GetPolitykResponse> CreatePolityk(CreatePolitykRequest request);
}

public class DbService(S27655Context data) : IDbService
{
    public async Task<IEnumerable<GetPolitykResponse>> GetPolitykDetails()
    {
        return await data.Polityks.Select(p => new GetPolitykResponse
        {
            Id = p.Id,
            Imie = p.Imie,
            Nazwisko = p.Nazwisko,
            Powiedzenie = p.Powiedzenie,
            Przynaleznosc = p.Przynaleznoscs.Select(przy => new GetPrzynaleznoscResponse()
            {
                Nazwa = przy.Partia.Nazwa,
                Skrot = przy.Partia.Skrot,
                DataZalozenia = przy.Partia.DataZalozenia,
                Od = przy.Od,
                Do = przy.Do,
            })
        }).ToListAsync();
    }

    public async Task<GetPolitykResponse> CreatePolityk(CreatePolitykRequest request)
    {
        List<Partium> partie = [];
        if (request.Przynaleznosc != null && request.Przynaleznosc.Any())
        {
            partie = await data.Partia.Where(p => request.Przynaleznosc.Contains(p.Id)).ToListAsync();
            foreach (var id in request.Przynaleznosc)
            {
                if (partie.FirstOrDefault(e=> e.Id == id) == null)
                {
                    throw new NotFoundException("NotFound");
                }
            }
        }

        var polityk = new Polityk
        {
            Imie = request.Imie,
            Nazwisko = request.Nazwisko,
            Powiedzenie = request.Powiedzenie,
            Przynaleznoscs = partie.Select(p => new Przynaleznosc()
            {
                Od = DateTime.Now,
                Partia = p,
                Do = null
            }).ToList()
        };
        await data.AddAsync(polityk);
        await data.SaveChangesAsync();

        return new GetPolitykResponse
        {
            Id = polityk.Id,
            Imie = polityk.Imie,
            Nazwisko = polityk.Nazwisko,
            Powiedzenie = polityk.Powiedzenie,
            Przynaleznosc = polityk.Przynaleznoscs.Select(prz => new GetPrzynaleznoscResponse()
            {
                DataZalozenia = prz.Partia.DataZalozenia,
                Od = prz.Od,
                Do = prz.Do,
                Nazwa = prz.Partia.Nazwa,
                Skrot = prz.Partia.Skrot
            })
        };
    }
}
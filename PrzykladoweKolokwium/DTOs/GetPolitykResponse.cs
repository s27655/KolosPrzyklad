namespace PrzykładoweKolokwium.DTOs;

public class GetPolitykResponse
{
    public int Id { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string? Powiedzenie { get; set; }
    public IEnumerable<GetPrzynaleznoscResponse> Przynaleznosc { get; set; }
}

public class GetPrzynaleznoscResponse
{
    public string Nazwa { get; set; }
    public string Skrot { get; set; }
    public DateTime DataZalozenia { get; set; }
    public DateTime Od { get; set; }
    public DateTime? Do { get; set; }
}
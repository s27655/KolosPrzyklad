using System.ComponentModel.DataAnnotations;

namespace PrzykładoweKolokwium.DTOs;

public class CreatePolitykRequest
{
    [Required]
    [MaxLength(50)]
    public string Imie { get; set; } = String.Empty;
    [Required]
    [MaxLength(100)]
    public string Nazwisko { get; set; } = String.Empty;
    [MaxLength(200)]
    public string? Powiedzenie { get; set; }
    public IEnumerable<int>? Przynaleznosc { get; set; }
}
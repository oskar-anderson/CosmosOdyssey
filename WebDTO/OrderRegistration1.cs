using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebDTO;

public class OrderRegistration1
{
    public required string? ErrorMsg { get; set; }
    
    [Display(Name = "From")]
    public required SelectList FromPlanets { get; set; }
    
    [Required]
    public Guid From { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebDTO;

public class OrderRegistration2
{
    public required Guid From { get; set; }
    
    [Display(Name = "To")]
    public required SelectList ToPlanets { get; set; }
    
    [Required]
    public Guid To { get; set; }
}
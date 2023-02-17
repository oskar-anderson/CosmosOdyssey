using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO;

public class ProvidedRoute
{
    public required Guid Id { get; set; }
    
    public required PriceList PriceList { get; set; }
    
    public required Location FromLocation { get; set; }
    
    public required Location DestinationLocation { get; set; }
    
    public required long Distance { get; set; }
    
    public required Company Company { get; set; }
    
    public required double Price { get; set; }
    
    [DisplayName("Flight start")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public required DateTime FlightStart { get; set; }
    [DisplayName("Flight end")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public required DateTime FlightEnd { get; set; }
}
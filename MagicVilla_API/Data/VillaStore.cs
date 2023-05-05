using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Data
{
    public class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto{ Id = 1,Name="Vista a la piscina", Occupants=12, SquareMeters=123},
            new VillaDto{ Id = 2,Name="Vista a la piscina 2",Occupants=2, SquareMeters=34}
        };
    }
}

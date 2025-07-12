namespace DOTNETPROJECT.DTOs
{
    public class UserDtosExpEntity
    {
        public required string CurrentCompanyName { get; set; } = null!;
        public required int CurrentCompanyExp { get; set; }
        public required string PrevCompanyName { get; set; } = null!;
        public required int TotalYearsExp { get; set; }
        public required bool AllowRelocate { get; set; }
        public required string NotciePeriod { get; set; } = null!;
        public required string CurrentLocation { get; set; } = null!;
    }
    
}
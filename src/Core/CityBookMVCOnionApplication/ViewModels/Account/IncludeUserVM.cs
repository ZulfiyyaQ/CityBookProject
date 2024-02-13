namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludeUserVM(string Id, string Name, string Surname, string UserName, string Img)
    {
        public string? Tvit { get; init; }
        public string? Inst { get; init; }
        public string? Face { get; init; }
        public string? Link { get; init; }
        public string? WebSite { get; init; }
        public int PositionId { get; init; }
        public IncludePositionVM Position { get; init; }
    }


}

namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludeUserVM(string Id, string Name, string Surname, string UserName, string Img)
    {
        public int PositionId { get; init; }
        public IncludePositionVM Position { get; init; }
    }


}

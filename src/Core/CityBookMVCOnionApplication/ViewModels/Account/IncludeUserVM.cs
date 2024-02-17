namespace CityBookMVCOnionApplication.ViewModels
{
    public record IncludeUserVM(string Name, string Surname, string UserName, string Image)
    {
        private string Id { get; init; }
        public string? Tvit { get; init; }
        public string? Inst { get; init; }
        public string? Face { get; init; }
        public string? Link { get; init; }
        public string? WebSite { get; init; }
        public string? Phone { get; init; }
        public string? Email { get; init; }
    }


}

namespace CityBookMVCOnionInfrastructure.Exceptions
{
    public class WrongRequestException : Exception
    {
        public WrongRequestException(string message) : base(message) { }
    }
}

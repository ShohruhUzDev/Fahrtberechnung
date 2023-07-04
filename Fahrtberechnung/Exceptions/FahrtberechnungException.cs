namespace Fahrtberechnung.Exceptions
{
    public class FahrtberechnungException : Exception
    {
        public int Code { get; set; }

        public FahrtberechnungException(int code, string message) :
            base(message) =>
            this.Code = code;
    }
}

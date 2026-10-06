namespace StockControl.Exceptions
{
    public class ProductInformationInvalidException : Exception
    {
        public ProductInformationInvalidException(string message) : base(message) { }
    }
}
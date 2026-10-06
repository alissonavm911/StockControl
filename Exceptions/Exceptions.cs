namespace StockControl.Exceptions
{
    public class ProductInformationInvalidException : Exception
    {
        public ProductInformationInvalidException(string message) : base(message)
        {
        }
    }

    public class ProductNotFoundByIdException : Exception
    {
        public ProductNotFoundByIdException(string message) : base(message)
        {
        }
    }

    public class OptionNotFoundException : Exception
    {
        public OptionNotFoundException(string message) : base(message)
        {
        }
    }
}
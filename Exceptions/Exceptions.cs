namespace StockControl.Exceptions
{
    public class ProductInformationInvalidException : Exception
    {
        public ProductInformationInvalidException(string message) : base("Product information is invalid: " + message) { }
    }
    public class OptionNotFoundException : Exception
    {
        public OptionNotFoundException(string message) : base("Option not found: " + message) { }
    }
    public class ErrorOperationException : Exception
    {
        public ErrorOperationException(string message) : base(message) { }
    }
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message) { }
    }
    public class InitializeStockException : Exception
    {
        public InitializeStockException(string message) : base("Error occurred while initializing stock: " + message) { }
    }
    public class InitializeSystemException : Exception
    {
        public InitializeSystemException(string message) : base("Error occurred while initializing system: " + message) { }
    }
    public class ExitException : Exception
    {
        public ExitException(string message) : base("Error occurred while exiting the program: " + message) { }
    }
    
}
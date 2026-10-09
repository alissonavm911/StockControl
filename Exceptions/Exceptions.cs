namespace StockControl.Exceptions
{
    public class ProductInformationInvalidException : Exception
    {
        public ProductInformationInvalidException(string message) : base(message) { }
    }
    public class OptionNotFoundException : Exception
    {
        public OptionNotFoundException(string message) : base(message) { }
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
        public InitializeStockException(string message) : base(message) { }
    }
    public class InitializeSystemException : Exception
    {
        public InitializeSystemException(string message) : base(message) { }
    }
    public class ExitException : Exception
    {
        public ExitException(string message) : base(message) { }
    }
    public class FileOperationException : Exception
    {
        public FileOperationException(string message) : base(message) { }
    }
    public class InvalidPathException : Exception
    {
        public InvalidPathException(string message) : base(message) { }
    }
    public class InitializeSalesException : Exception
    {
        public InitializeSalesException(string message) : base(message) { }
    }
    public class SaleNotFoundException : Exception
    {
        public SaleNotFoundException(string message) : base(message) { }
    }
}
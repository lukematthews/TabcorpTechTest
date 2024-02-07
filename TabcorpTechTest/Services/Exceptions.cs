namespace TabcorpTechTest.Services
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException() : base() { }
        public CustomerNotFoundException(string message) : base(message) { }
        public CustomerNotFoundException(string message, Exception inner) : base(message, inner) { }

    }

    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base() { }
        public ProductNotFoundException(string message) : base(message) { }
        public ProductNotFoundException(string message, Exception inner) : base(message, inner) { }
    }

}

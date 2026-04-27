using System;

namespace StockWise.Domain.Exceptions
{
    public class FrutaJaCadastradaException : ApplicationException
    {
        public FrutaJaCadastradaException(string message) : base(message) { }
    }
}

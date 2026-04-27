using System;

namespace StockWise.Domain.Exceptions
{
    public class FrutaNaoEncontradaException : ApplicationException
    {
        public FrutaNaoEncontradaException(string message) : base(message) { }
    }
}

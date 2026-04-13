using System;

namespace StockWiseNET.Exceptions
{
    public class FrutaNaoEncontradaException : ApplicationException
    {
        public FrutaNaoEncontradaException(string message) : base(message) { }
    }
}

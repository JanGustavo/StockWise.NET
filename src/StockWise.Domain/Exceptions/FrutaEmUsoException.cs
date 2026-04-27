using System;

namespace StockWise.Domain.Exceptions
{
    public class FrutaEmUsoException : InvalidOperationException
    {
        public FrutaEmUsoException(string message) : base(message) { }
    }
}

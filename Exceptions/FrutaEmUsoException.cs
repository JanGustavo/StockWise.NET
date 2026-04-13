using System;

namespace StockWiseNET.Exceptions
{
    public class FrutaEmUsoException : InvalidOperationException
    {
        public FrutaEmUsoException(string message) : base(message) { }
    }
}

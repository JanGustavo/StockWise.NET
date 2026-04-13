using System;

namespace StockWiseNET.Exceptions
{
    public class FrutaJaCadastradaException : ApplicationException
    {
        public FrutaJaCadastradaException(string message) : base(message) { }
    }
}

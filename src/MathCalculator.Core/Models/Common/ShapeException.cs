using System;

namespace MathCalculator.Core.Models.Common
{
    public sealed class ShapeException : Exception
    {
        public ShapeException(string message) : base(message)
        {
        }
    }
}
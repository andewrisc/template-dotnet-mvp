using System;

namespace API.Models.DTOs.Exceptions;

public class SzValidationException : Exception
{
    public SzValidationException(string message) : base(message)
    {
    }
}

using System;

namespace Sakura.Model
{
  public class ValidationException : Exception
  {
    public ValidationException(string message) : base(message) { }
  }
}

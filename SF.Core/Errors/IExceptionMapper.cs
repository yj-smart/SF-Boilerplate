using System;

namespace SF.Core.Errors
{
    public interface IExceptionMapper
    {
        Error Resolve(Exception exception);
    }
}

using System.Collections.Generic;

namespace OpenStandup.SharedKernel
{
    public interface IResult<out T>
    {
        ResultStatus Status { get; }
        IEnumerable<string> Errors { get; }
        T Value { get; }
    }
}

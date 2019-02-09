using System;

namespace Consensus.Models
{
    public class ErrorViewModel
    {
        public ErrorType ErrorCode { get; }
        public string Message { get; }
        public DateTimeOffset OccurredOn { get; }

        public ErrorViewModel(ErrorType errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
            OccurredOn = DateTimeOffset.UtcNow;
        }
    }
}

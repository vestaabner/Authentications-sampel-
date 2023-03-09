using System;

namespace Service.Contract
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}

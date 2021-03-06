using LowCostFligtsBrowser.Application.Common.Interfaces;
using System;

namespace LowCostFligtsBrowser.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}

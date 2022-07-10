using System;

namespace AspDotNetLab2.Services
{
    public class RandomService
    {
        protected internal ICounter Counter { get; }
        public RandomService(ICounter counter)
        {
            Counter = counter;
        }
    }
}

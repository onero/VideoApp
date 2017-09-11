using System;

namespace VideoAppBLL.BusinessObjects
{
    public class RentalBO
    {
        public int Id { get; set; }
        public DateTime From { get; set; } = DateTime.Now;
        public DateTime To { get; set; } = DateTime.Now.AddDays(7);

    }
}
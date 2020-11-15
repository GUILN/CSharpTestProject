using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Rate { get; set; }

        public IEnumerable<HotelFee> Fees { get; set; }

        public decimal GetDailyFee(DateTime dateTime, Client client)
        {
            var fee = Fees
                    .Where(f => f.ClientRate == client.Rate
                                && f.WeekendFee == this.isWeekend(dateTime))
                    .First()
                    .Value;

            return fee;
        }

        private bool isWeekend(DateTime dateTime) 
            => (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday);
    }
}

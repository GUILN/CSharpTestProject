using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class HotelFee
    {
        public int HotelId { get; set; }
        public bool WeekendFee { get; set; }
        public ClientRateEnum ClientRate { get; set; }
        public decimal Value { get; set; }
    }
}

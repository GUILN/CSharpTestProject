using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CSharpTestProject.Domain.Entities
{
    public class HotelTest
    {
        [Fact]
        public void GetDailyFee_ShouldReturnValueGreaterThenZero()
        {

            var sut = getHotelFor_GetDailyFee_ShouldReturnValueGreaterThenZero_Test();
            var regularClient = getRegularClient();


            Assert.True(sut.GetDailyFee(DateTime.Now, regularClient) > 0);
        }

        private Hotel getHotelFor_GetDailyFee_ShouldReturnValueGreaterThenZero_Test()
        {
            Hotel hotel = new Hotel() { Id = 1 };
            HotelFee[] fees = new HotelFee[] { new HotelFee() { HotelId = 1, ClientRate = ClientRateEnum.Prime, WeekendFee = true, Value = 50.5M },
                                                new HotelFee() { HotelId = 1, ClientRate = ClientRateEnum.Regular, WeekendFee = true, Value = 50.5M },
                                                new HotelFee() { HotelId = 1, ClientRate = ClientRateEnum.Prime, WeekendFee = false, Value = 50.5M },
                                                new HotelFee() { HotelId = 1, ClientRate = ClientRateEnum.Regular, WeekendFee = false, Value = 50.5M },};
            hotel.Fees = fees.ToList();

            return hotel;
        }

        private Client getRegularClient() => new Client() { Rate = ClientRateEnum.Prime };
    }
}

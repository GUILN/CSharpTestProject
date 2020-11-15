using ApplicationServices.AppServices;
using Entities;
using RepositoryContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CSharpTestProject.Application.ApplicationServices
{
    public class HotelAppServiceTest
    {
        [Fact]
        public void GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient()
        {
            //Arrange
            Hotel cheaperHotel = getCheperHotel_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient();
            HotelAppService sut = this.getSut_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient(cheaperHotel);
            Client client = getClient_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient();
            DateTime[] dates = getDates_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient();
            

            //Act
            Hotel resultHotel = sut.GetCheaperHotel(client, dates);


            Assert.True(resultHotel.Name == cheaperHotel.Name);
        }

        private Hotel getCheperHotel_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient()
            => new Hotel() { Name = "The Cheaper One", Rate = 5,
                Fees = getCheaperHotelFees_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient() };

        private IEnumerable<HotelFee> getCheaperHotelFees_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient()
            => new HotelFee[] {
                                        new HotelFee() { ClientRate = ClientRateEnum.Prime, WeekendFee = true, Value = -1M},
                                        new HotelFee() { ClientRate = ClientRateEnum.Prime, WeekendFee = false, Value = -1M},
                                        new HotelFee() { ClientRate = ClientRateEnum.Regular, WeekendFee = true, Value = -1M},
                                        new HotelFee() { ClientRate = ClientRateEnum.Regular, WeekendFee = false, Value = -1M}
                                   };

        private DateTime[] getDates_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient()
            => new DateTime[] { new DateTime(2020, 9, 1), new DateTime(2020, 9, 2), new DateTime(2020, 9, 3) };

        private Client getClient_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient()
            => new Client() { Rate = ClientRateEnum.Prime };

        private HotelAppService getSut_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient(Hotel cheaperHotel)
        {
            IHotelRepository mockedRepository =
                this.getHotelMockedRepo_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient(cheaperHotel);

            HotelAppService hotelAppService = new HotelAppService(mockedRepository);
            return hotelAppService;
        }

        private IHotelRepository getHotelMockedRepo_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient(Hotel cheaperHotel)
        {
            Hotel expensiveHotel = getExpensiveHotel_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient();
            List<Hotel> hotels = (new Hotel[] { cheaperHotel,
                                                 expensiveHotel})
                                        .ToList();
            Moq.Mock<IHotelRepository> mockedRepo = new Moq.Mock<IHotelRepository>();
            mockedRepo.Setup(m => m.GetAll()).Returns(hotels);
            return mockedRepo.Object;
        }

        private Hotel getExpensiveHotel_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient()
            => new Hotel()
            {
                Name = "The Expensive One",
                Rate = 5,
                Fees = getExpensiveHotelFees_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient()
            };

        private IEnumerable<HotelFee> getExpensiveHotelFees_For_GetCheaperHotel_ShouldReturnCheaperHotel_WithinAGivenClient()
            => new HotelFee[] {
                                        new HotelFee() { ClientRate = ClientRateEnum.Prime, WeekendFee = true, Value = 100M},
                                        new HotelFee() { ClientRate = ClientRateEnum.Prime, WeekendFee = false, Value = 100M},
                                        new HotelFee() { ClientRate = ClientRateEnum.Regular, WeekendFee = true, Value = 100M},
                                        new HotelFee() { ClientRate = ClientRateEnum.Regular, WeekendFee = false, Value = 100M}
                                   };
    }
}

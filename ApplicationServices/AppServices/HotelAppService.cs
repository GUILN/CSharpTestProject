using Entities;
using RepositoryContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationServices.AppServices
{
    public class HotelAppService
    {
        private readonly IHotelRepository _hotelRepository;
        public HotelAppService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public Hotel GetCheaperHotel(Client client, DateTime[] requiredDates)
        {
            List<Hotel> availableHotels = _hotelRepository.GetAll().ToList();
            Dictionary<Hotel, decimal> hotelIdSummedFeesDictionary = new Dictionary<Hotel, decimal>();

            availableHotels.ForEach(
                h => hotelIdSummedFeesDictionary.Add(h, getTotalSumOfFeesForHotel(h, client, requiredDates))
            );

            
            decimal lowerValue = hotelIdSummedFeesDictionary.Values.Min();
            List<Hotel> cheaperHotels = hotelIdSummedFeesDictionary
                                        .Where(d => d.Value == lowerValue)
                                        .Select(d => d.Key)
                                        .ToList();

            Hotel cheaperHotel = cheaperHotels.First();

            if(cheaperHotels.Count() > 1)
            {
                cheaperHotel = cheaperHotels
                                .OrderByDescending(h => h.Rate)
                                .FirstOrDefault();
            }

            return cheaperHotel;
        }

        private decimal getTotalSumOfFeesForHotel(Hotel h, Client client, DateTime[] requiredDates)
        {
            decimal totalSum = 0M;
            requiredDates.ToList().ForEach(d => totalSum += h.GetDailyFee(d, client));
            return totalSum;
        }
    }
}

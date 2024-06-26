﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using StationeryStore.Repository;
using StationeryStore.Services;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRateRepository _rateRepository;
        public RateController(IMapper mapper, IRateRepository rateRepository,
            UserInfoService userInfoService, IUserRepository userRepository) :
            base(userInfoService, userRepository)
        {
            _mapper = mapper;
            _rateRepository = rateRepository;
        }

        //[Authorize(Roles = "Customer")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddProductRate([FromBody] ReqRateDto rate)
        {
            if (rate == null)
                return BadRequest(ModelState);

            var rateMap = _mapper.Map<Rate>(rate);
            rateMap.UserId = base.GetActiveUser()!.Id;
            
            // chaeck if rate exist
            var isExisted = _rateRepository.GetRateByUserAndProduct(rateMap.UserId, rate.ProductId);
            // if no: add new rate
            if(isExisted == null)
            {
                rateMap.Date = DateTime.Now;
                if (!_rateRepository.AddRate(rateMap))
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return StatusCode(500, ModelState);
                }
            }
            else // if yes: update the rate
            {
                isExisted.Rating = rate.Rating;
                isExisted.Date = DateTime.Now;
                if (!_rateRepository.UpdateRate(isExisted))
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return StatusCode(500, ModelState);
                }
            }
            return Ok("Succsessfuly added");
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Rate>))]
        public IActionResult GetAvgRate(int productId)
        {
            var avgRating = _rateRepository.GetAverageRatingForProduct(productId);
            if(avgRating.HasValue)
            {
                return Ok(avgRating);
            }
            else return NotFound();
        }

        [HttpGet("{productId}/count")]
        public IActionResult GetRatingCounts(int productId)
        {
            var ratingCounts = GetRatingCountsForProduct(productId);

            if (ratingCounts != null)
            {
                return Ok(ratingCounts);
            }
            else
            {
                return NotFound();
            }
        }

        private Dictionary<int, int> GetRatingCountsForProduct(int productId)
        {
            var ratings = _rateRepository.GetRatingsForProduct(productId);

            // Calculate count for each rating
            var ratingCounts = new Dictionary<int, int>();

            // Initialize count for each rating to 0
            for (int i = 1; i <= 5; i++)
            {
                ratingCounts[i] = 0;
            }

            // Update count based on retrieved ratings
            foreach (var rating in ratings)
            {
                ratingCounts[rating.Rating]++;
            }

            return ratingCounts;
        }


    }
}

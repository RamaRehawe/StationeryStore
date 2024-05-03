using AutoMapper;
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
    public class ReviewController : BaseController
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        


        public ReviewController(IReviewRepository reviewRepository, 
            IMapper mapper, UserInfoService userInfoService, IUserRepository userRepository)
            : base(userInfoService, userRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviewsByProduct(int proId)
        {
            var review = _mapper.Map<List<ResReviewDto>>(_reviewRepository.GetReviews(proId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        //[Authorize(Roles = "Customer")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddReview([FromBody] ReqReviewDto reviewAdd)
        {
            if (reviewAdd == null)
                return BadRequest(ModelState);

            var user = base.GetActiveUser()!;
            var reviewMap = _mapper.Map<Review>(reviewAdd);
            var isExisted = _reviewRepository.GetReviewByUserAndProduct(user.Id, reviewAdd.ProductId);
            if (isExisted != null)
            {
                isExisted.Text = reviewAdd.Text;
                isExisted.Date = DateTime.Now;
                if (!_reviewRepository.UpdateReview(isExisted))
                {
                    ModelState.AddModelError("", "Something went wrong while updating");
                    return StatusCode(500, ModelState);
                }
            }
            else
            {
                reviewMap.Date = DateTime.Now;
                reviewMap.UserId = user.Id;            
                if (!_reviewRepository.CreateReview(reviewMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

            }    

            return Ok("Succesfully Added");
        }
    }
}

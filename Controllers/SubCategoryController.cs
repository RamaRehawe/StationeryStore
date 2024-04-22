using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using StationeryStore.Repository;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMapper _mapper;
        public SubCategoryController(ISubCategoryRepository subCategoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
        }

      //  [Authorize(Roles = "Item Manager")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSubCategory([FromBody] ReqSubCategoryDto subCategoryCreate)
        {
            if (subCategoryCreate == null)
                return BadRequest(ModelState);

            var subCategoryMap = _mapper.Map<SubCategory>(subCategoryCreate);
            var subCategory = _subCategoryRepository.GetSubCategories()
                .Where(c => c.CategoryId == subCategoryMap.CategoryId)
                .Where(c => c.Name.Trim().ToUpper().Equals(subCategoryCreate.Name.TrimEnd().ToUpper()))
                .FirstOrDefault();

            if (subCategory != null)
            {
                ModelState.AddModelError("", "SubCategory already exists");
                return StatusCode(422, ModelState);
            }

            if (!_subCategoryRepository.CreateSubCategory(subCategoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully Added");
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ResSubCategoryDto>))]
        public IActionResult GetSubCategories()
        {
            var subCategories = _mapper.Map<List<ResSubCategoryDto>>(_subCategoryRepository.GetSubCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(subCategories);
        }
    }
}

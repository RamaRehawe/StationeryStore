using AutoMapper;
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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SubCategory>))]
        public IActionResult GetSubCategories()
        {
            var subCategory = _mapper.Map<List<SubCategoryDto>>(_subCategoryRepository.GetSubCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(subCategory);
        }

        [HttpGet("{subCategoryId}")]
        [ProducesResponseType(200, Type = typeof(SubCategory))]
        [ProducesResponseType(400)]

        public IActionResult GetSubCategory(int subCategoryId)
        {
            if (!_subCategoryRepository.SubCategoryExists(subCategoryId))
                return NotFound();
            var subCategory = _mapper.Map<SubCategoryDto>(_subCategoryRepository.GetSubCategory(subCategoryId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(subCategory);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSubCategory([FromBody] SubCategoryDto subCategoryCreate)
        {
            if (subCategoryCreate == null)
                return BadRequest(ModelState);

            var subCategory = _subCategoryRepository.GetSubCategories()
                .Where(c => c.Name.Trim().ToUpper().Equals(subCategoryCreate.Name.TrimEnd().ToUpper()))
                .FirstOrDefault();

            if (subCategory != null)
            {
                ModelState.AddModelError("", "SubCategory already exists");
                return StatusCode(422, ModelState);
            }

            var subCategoryMap = _mapper.Map<SubCategory>(subCategoryCreate);
            if (!_subCategoryRepository.CreateSubCategory(subCategoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully Added");
        }
    }
}

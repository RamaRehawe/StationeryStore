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
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var category = _mapper.Map<List<ResCategoryDto>>(_categoryRepository.GetCategories());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId) 
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();
            var category = _mapper.Map<ResCategoryDto>(_categoryRepository.GetCategory(categoryId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(category);
        }

        [Authorize(Roles = "Item Manager")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] ReqCategoryDto categoryCreate)
        {
            if(categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper().Equals( categoryCreate.Name.TrimEnd().ToUpper()))
                .FirstOrDefault();

            if(category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully Added");
        }

    }
}

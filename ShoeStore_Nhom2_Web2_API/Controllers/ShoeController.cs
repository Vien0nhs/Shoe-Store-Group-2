using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeStore_Nhom2_Web2_API.Models;
using ShoeStore_Nhom2_Web2_API.Repositories;
#nullable disable
namespace ShoeStore_Nhom2_Web2_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoeController : ControllerBase
	{
		private readonly IShoeRepository _repository;
		private const int PageSize = 6;

		public ShoeController(IShoeRepository repository)
		{
			_repository = repository;
		}

		// GET: api/Shoe
		[HttpGet("get-shoe")]
		[Authorize(Roles = "Read")]
		public async Task<ActionResult<IEnumerable<Shoe_Model>>> GetShoe()
		{
			return Ok(await _repository.GetShoes());
		}

		// GET: api/Shoe/get-shoe (Pagination)
		[HttpGet("get-shoe-P")]
		public async Task<ActionResult<IEnumerable<Shoe_Model>>> GetShoes(int page = 1)
		{
			var shoes = await _repository.GetShoesPaged(page, PageSize);
			return Ok(shoes);
		}

		// GET: api/Shoe/filter (Filtering by brand and color)
		[HttpGet("get-shoe-F")]
		public async Task<ActionResult<IEnumerable<Shoe_Model>>> GetFilteredShoes([FromQuery] string brand = null, string color = null)
		{
			var shoes = await _repository.GetFilteredShoes(brand, color);
			return Ok(shoes);
		}

		// GET: api/Shoe/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Shoe_Model>> GetShoe_Model(int id)
		{
			var shoeModel = await _repository.GetShoeById(id);

			if (shoeModel == null)
			{
				return NotFound();
			}

			return Ok(shoeModel);
		}

		// PUT: api/Shoe/5
		[HttpPut("{id}")]
		[Authorize(Roles = "Write")]
		public async Task<IActionResult> PutShoe_Model(int id, Shoe_Model shoeModel)
		{
			if (id != shoeModel.Id)
			{
				return BadRequest();
			}

			await _repository.UpdateShoe(shoeModel);

			return NoContent();
		}

		// POST: api/Shoe
		[HttpPost]
		[Authorize(Roles = "Write")]
		public async Task<ActionResult<Shoe_Model>> PostShoe_Model(ShoeCreateDto shoeCreateDto)
		{
			var shoeModel = new Shoe_Model
			{
				ImageUrl = shoeCreateDto.ImageUrl,
				Name = shoeCreateDto.Name,
				Price = shoeCreateDto.Price,
				Old_Price = shoeCreateDto.Old_Price,
				Brand = shoeCreateDto.Brand,
				Color = shoeCreateDto.Color,
				Size = shoeCreateDto.Size,
				Remaining = shoeCreateDto.Remaining,
			};

			await _repository.AddShoe(shoeModel);

			return CreatedAtAction("GetShoe_Model", new { id = shoeModel.Id }, shoeModel);
		}

		// DELETE: api/Shoe/5
		[HttpDelete("{id}")]
		[Authorize(Roles = "Write")]
		public async Task<IActionResult> DeleteShoe_Model(int id)
		{
			var result = await _repository.DeleteShoe(id);

			if (!result)
			{
				return NotFound();
			}

			return NoContent();
		}

	}
}

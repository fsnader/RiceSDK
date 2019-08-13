using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rice.SDK.Business.Contract;
using Rice.SDK.Domain.Contract;
using Rice.SDK.Utils;
using RiceWebBase.Attributes;

namespace RiceWebBase.Controllers.Base
{
    /// <summary>
    /// A base controller that searches and returns entities of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Authorize]
    [ApiExceptionFilter]
    [Produces("application/json")]
    public class EntityController<T> : Controller
        where T : IIdentifiableEntity
    {
        private readonly IBusiness<T> _business;

        public EntityController(IBusiness<T> business)
        {
            _business = business;
        }

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns></returns>
        // GET: api/Entities
        [HttpGet("api/[controller]")]
        public async Task<IEnumerable<T>> GetEntities()
        {
            return await _business
                .ListAll();
        }

        /// <summary>
        /// Returns the entity with provided Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Entities/5
        [HttpGet("api/[controller]/{id}")]
        public async Task<IActionResult> GetEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var T = await _business.GetById(id);

            if (T == null)
                return NotFound();

            return Ok(T);
        }

        /// <summary>
        /// Finds entities based on a provided filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("api/[controller]/[action]")]
        public async Task<IEnumerable<T>> Search([FromBody] FilterParameters filter)
        {
            return await _business
                .ListByFilter(filter);
        }

        /// <summary>
        /// Update the specified entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        // PUT: api/Entities/5
        [HttpPut("api/[controller]/{id}")]
        public async Task<IActionResult> PutEntity([FromRoute] int id,
            [FromBody] T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != entity.Id)
                return BadRequest();

            try
            {
                await _business.Save(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // POST: api/Entities
        [HttpPost("api/[controller]")]
        public async Task<IActionResult> PostEntity([FromBody] T entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _business.Save(entity);

            return CreatedAtAction("GetEntity", new {id = entity.Id}, entity);
        }

        /// <summary>
        /// Deletes the specified entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Entities/5
        [HttpDelete("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _business.GetById(id);

            if (entity == null)
                return NotFound();

            await _business.Delete(entity);

            return Ok(entity);
        }

        private bool EntityExists(int id)
        {
            return _business.GetById(id) != null;
        }
    }
}
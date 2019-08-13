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
    /// A base controller that search from T entities and convert it to a TDataTransfer (DTO) type
    /// </summary>
    /// <typeparam name="TDataTransfer">The data transfer object type</typeparam>
    /// <typeparam name="TEntity">The entity type</typeparam>
    [Authorize]
    [ApiExceptionFilter]
    [Produces("application/json")]
    public class DataTransferController<TDataTransfer, TEntity> : Controller
        where TDataTransfer : IDataTransferEntity<TEntity>
        where TEntity : class, IIdentifiableEntity

    {
        protected readonly IDataTransferBusiness<TDataTransfer, TEntity> _business;

        public DataTransferController(IDataTransferBusiness<TDataTransfer, TEntity> business)
        {
            _business = business;
        }

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns></returns>
        // GET: api/Entities
        [HttpGet("api/[controller]")]
        public virtual async Task<IEnumerable<TDataTransfer>> GetEntities()
        {
            return await _business
                .ListAll(User);
        }

        /// <summary>
        /// Returns the entity with provided Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Entities/5
        [HttpGet("api/[controller]/{id}")]
        public virtual async Task<IActionResult> GetEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var T = await _business.GetById(id, User);

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
        public virtual async Task<IEnumerable<TDataTransfer>> Search([FromBody] FilterParameters filter)
        {
            return await _business
                .ListByFilter(filter, User);
        }

        /// <summary>
        /// Update the specified entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        // PUT: api/Entities/5
        [HttpPut("api/[controller]/{id}")]
        public virtual async Task<IActionResult> PutEntity([FromRoute] int id,
            [FromBody] TDataTransfer entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != entity.Id)
                return BadRequest();

            try
            {
                await _business.Save(entity, User);
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
        public virtual async Task<IActionResult> PostEntity([FromBody] TDataTransfer entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _business.Save(entity, User);

            return CreatedAtAction(nameof(GetEntity), new {id = entity.Id}, entity);
        }

        /// <summary>
        /// Deletes the specified entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Entities/5
        [HttpDelete("api/[controller]/{id}")]
        public virtual async Task<IActionResult> DeleteEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _business.GetById(id);

            if (entity == null)
                return NotFound();

            await _business.Delete(entity, User);

            return Ok(entity);
        }

        protected bool EntityExists(int id)
        {
            return _business.GetById(id, User) != null;
        }
    }
}
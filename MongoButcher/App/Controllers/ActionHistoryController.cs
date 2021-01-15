using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.ActionHistories;

namespace MongoDBDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionHistoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IActionHistoryService _service;
        private readonly ITransactionProvider _transactionProvider;

        public ActionHistoryController(ITransactionProvider transactionProvider, IMapper mapper,
            IActionHistoryService service)
        {
            _transactionProvider = transactionProvider;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ActionHistory>> GetById(string id)
        {
            ActionHistory? entity;
            if (string.IsNullOrWhiteSpace(id) || (entity = await this._service.GetEntityById(id)) == null)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<ActionHistory>>> GetAll()
        {
            IEnumerable<ActionHistory> posts = await this._service.GetAll();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActionHistory newEntity)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this
            if (string.IsNullOrWhiteSpace(newEntity.Description))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            var entity = await this._service.AddEntity(newEntity);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new {id = entity.Id.ToString()}, entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ActionHistory update)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this
            if (string.IsNullOrWhiteSpace(update.Description))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            var entity = await this._service.UpdateEntity(update);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new {id = entity.Id.ToString()}, entity);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            await this._service.DeleteEntity(new ObjectId(id));
            await transaction.CommitAsync();
            return Ok();
        }
    }
}
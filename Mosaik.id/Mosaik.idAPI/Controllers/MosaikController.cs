using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Interfaces;

namespace Mosaik.idAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MosaikController : ControllerBase
    {
        private readonly IMosaikRepository _repository;

        public MosaikController(IMosaikRepository mosaikRepository)
        {
            _repository = mosaikRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_repository.All);
        }
    }
}
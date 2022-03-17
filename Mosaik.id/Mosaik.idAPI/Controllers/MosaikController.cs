using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mosaik.idAPI.Models;
using Mosaik.idAPI.Interfaces;
using Mosaik.idAPI.Services;
using Mosaik.idAPI.Dtos;

namespace Mosaik.idAPI.Controllers
{
    public enum ErrorCode
    {
        TodoItemNameAndNotesRequired,
        TodoItemIDInUse,
        RecordNotFound,
        CouldNotCreateItem,
        CouldNotUpdateItem,
        CouldNotDeleteItem
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MosaikController : ControllerBase
    {
        private readonly IMosaikRepository _repository;
        private readonly IMosaikHistoryRepository _historyRepository;

        public MosaikController(IMosaikRepository mosaikRepository, IMosaikHistoryRepository mosaikHistoryRepository)
        {
            _repository = mosaikRepository;
            _historyRepository = mosaikHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MosaikItem>>> GetMosaikItems()
        {
            var mosaikItems = await _repository.getAll();
            return Ok(mosaikItems);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login (LoginAccountDto loginAccountDto)
        {
            await _repository.AuthenticateAccount(loginAccountDto.Email, loginAccountDto.Password);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccount (CreateAccountDto createAccountDto)
        {
            MosaikItem mosaikItem = new()
            {
                ID = createAccountDto.userID,
                FullName = createAccountDto.FullName,
                Email = createAccountDto.Email,
                Password = createAccountDto.Password
            };
            await _repository.InsertAccount(mosaikItem);
            return Ok();
        }

        [HttpPost("history")]
        public async Task<ActionResult> CreateHistory (CreateHistoryDto createHistoryDto)
        {
            MosaikHistory mosaikHistory = new()
            {
                MosaikHistoryID = createHistoryDto.historyID,
                userID = createHistoryDto.userID,
                Link = createHistoryDto.Link,
                AccessedTime = DateTime.Now.ToString()
            };
            await _historyRepository.InsertHistory(mosaikHistory);
            return Ok();
        }
    }
}
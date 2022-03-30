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
        private readonly IMosaikChildRepository _childRepository;
        private readonly IMosaikParentRepository _parentRepository;
        private readonly IMosaikRestrictRepository _restrictRepository;

        public MosaikController(IMosaikRepository mosaikRepository, 
                                IMosaikHistoryRepository mosaikHistoryRepository,
                                IMosaikChildRepository mosaikChildRepository,
                                IMosaikParentRepository mosaikParentRepository,
                                IMosaikRestrictRepository mosaikRestrictRepository)
        {
            _repository = mosaikRepository;
            _historyRepository = mosaikHistoryRepository;
            _childRepository = mosaikChildRepository;
            _parentRepository = mosaikParentRepository;
            _restrictRepository = mosaikRestrictRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MosaikParent>>> GetMosaikItems()
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

        [HttpPost("parent")]
        public async Task<ActionResult> CreateParentAccount (CreateAccountDto createAccountDto)
        {
            MosaikParent mosaikParent = new()
            {
                MosaikParentID = createAccountDto.userID,
                Username = createAccountDto.FullName,
                Email = createAccountDto.Email,
                Password = createAccountDto.Password
            };
            await _repository.InsertAccount(mosaikParent);
            return Ok();
        }

        [HttpPost("child")]
        public async Task<ActionResult> CreateChildAccount (CreateAccountDto createAccountDto)
        {
            MosaikChild mosaikChild = new()
            {
                MosaikChildID = createAccountDto.userID,
                Username = createAccountDto.FullName,
                Email = createAccountDto.Email,
                Password = createAccountDto.Password
            };
            await _repository.InsertAccount(mosaikChild);
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

        [HttpPost("restrict")]
        public async Task<ActionResult> CreateRestrict (CreateRestrictDto createRestrictDto)
        {
            MosaikChildRestrict mosaikChildRestrict = new()
            {
                MosaikChildRestrictID = createRestrictDto.MosaikChildRestrictID,
                ChildID = createRestrictDto.ChildID,
                Link = createRestrictDto.Link,
                Notif = createRestrictDto.Notif
            };
            await _restrictRepository.InsertRestrictedLink(mosaikChildRestrict);
            return Ok();
        }

        [HttpDelete("restrict/{id}")]
        public async Task<ActionResult> DeleteLink (int id)
        {
            await _restrictRepository.DeleteRestrictedLink(id);
            return Ok();
        }

        [HttpPut("restrict/{id}")]
        public async Task<ActionResult> UpdateNotif(bool notif, UpdateNotifDto updateNotifDto)
        {
            MosaikChildRestrict mosaikChildRestrict = new()
            {
                MosaikChildRestrictID = updateNotifDto.childRestrictID,
                ChildID = updateNotifDto.childID,
                Link = updateNotifDto.Link,
                Notif = notif
            };

            await _restrictRepository.DisableNotif(mosaikChildRestrict);
            return Ok();
        }

        [HttpPost("supervise/{email}")]
        public async Task<ActionResult> NewChildAccount(string Email, CreateSuperviseDto createSuperviseDto)
        {
            MosaikParentChild mosaikParentChild = new()
            {
                MosaikParentChildID = createSuperviseDto.MosaikChildRestrictID,
                parentID = createSuperviseDto.ParentID,
                childID = createSuperviseDto.ChildID
            };
            await _parentRepository.InsertChildAccount(Email, mosaikParentChild);
            return Ok();
        }

        [HttpDelete("supervise/{email}")]
        public async Task<ActionResult> DeleteChild (string Email) {
            await _parentRepository.DeleteChildAccount(Email);
            return Ok();
        }
    } 
}
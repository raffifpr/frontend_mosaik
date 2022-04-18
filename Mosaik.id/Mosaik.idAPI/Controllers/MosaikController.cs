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
        public class ErrorStatus
        {
            public int ErrorCode {get; set;}
            public string ErrorMessage {get; set;}
            public Tuple<int, String, String> Status {get; set;}
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MosaikParent>>> GetMosaikItems()
        {
            var mosaikItems = await _repository.getAll();
            return Ok(mosaikItems);
        }

        [HttpPost("login")]
        [Produces("application/json")]
        public async Task<ActionResult> Login (LoginAccountDto loginAccountDto)
        {
            String Status;
            var status = await _repository.AuthenticateAccount(loginAccountDto.Email, loginAccountDto.Password);
            if (status.Item2 == "false")
            {
                Status = "wrong";
                return new JsonResult(Status);
            } 
            else if (status.Item2 == "child")
            {
                var list = await _repository.GetSupervisedRequests(status.Item1);
                ChildAuthenticated childAuthenticated = new()
                {
                    Username = status.Item3,
                    Email = loginAccountDto.Email,
                    AccountStatus = "child",
                    SupervisedRequests = list,
                };
                return new JsonResult(childAuthenticated);
            }
            else if (status.Item2 == "supervisor")
            {
                var list = await _repository.GetSupervisorAccounts(status.Item1);   
                ParentAuthenticated parentAuthenticated = new()
                {
                    Username = status.Item3,
                    Email = loginAccountDto.Email,
                    AccountStatus = "supervisor",
                    SupervisorAccounts = list
                };
                return new JsonResult(parentAuthenticated);
            } else {
                Status = "failed";
                return new JsonResult(Status);
            }
        }

        [HttpPost("parent")]
        [Produces("application/json")]
        public async Task<ActionResult> CreateParentAccount (CreateAccountDtoParent createAccountDtoParent)
        {
            try
            {
                string Status;
                MosaikParent mosaikParent = new()
                {
                    Username = createAccountDtoParent.FullName,
                    Email = createAccountDtoParent.Email,
                    Password = createAccountDtoParent.Password
                };
                await _repository.InsertAccount(mosaikParent);

                MosaikParent mosaikParentReceive = await _parentRepository.Get(createAccountDtoParent.Email);

                foreach (var email in createAccountDtoParent.SupervisorEmails)
                {
                    MosaikChild mosaikChild = await _childRepository.GetChildAccount(email);
                    if (mosaikChild == null || mosaikParentReceive == null)
                    {
                        Status = "failed";
                        return new JsonResult(Status);
                    }
                    else
                    {
                        MosaikParentChild mosaikParentChild = new()
                        {
                            parentID = mosaikParentReceive.MosaikParentID,
                            childID = mosaikChild.MosaikChildID,
                            Authorized = false
                        };
                        await _parentRepository.InsertChildAccount(email, mosaikParentChild);
                    }
                }
                Status = "success";
                return new JsonResult(Status);
            }
            catch 
            {
                string Status = "failed";
                return new JsonResult(Status);
            }
            
        }

        [HttpPost("child")]
        public async Task<ActionResult> CreateChildAccount (CreateAccountDto createAccountDto)
        {
            try
            {
                MosaikChild mosaikChild = new()
                {
                    Username = createAccountDto.FullName,
                    Email = createAccountDto.Email,
                    Password = createAccountDto.Password
                };
                await _repository.InsertAccount(mosaikChild);
                string Status = "success";
                return new JsonResult(Status);
            }
            catch 
            {
                string Status = "failed ";
                return new JsonResult(Status);
            }
        }

        [HttpPost("history")]
        public async Task<ActionResult> CreateHistory (CreateHistoryDto createHistoryDto)
        {
            MosaikHistory mosaikHistory = new()
            {
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
                Link = updateNotifDto.Link,
                Notif = notif
            };

            await _restrictRepository.DisableNotif(mosaikChildRestrict);
            return Ok();
        }

        [HttpPost("supervise")]
        public async Task<ActionResult> NewChildAccount(string Email, CreateSuperviseDto createSuperviseDto)
        {
            MosaikParentChild mosaikParentChild = new()
            {
                parentID = createSuperviseDto.ParentID,
                childID = createSuperviseDto.ChildID
            };
            await _parentRepository.InsertChildAccount(Email, mosaikParentChild);
            return Ok();
        }

        [HttpDelete("supervise")]
        public async Task<ActionResult> DeleteChild (string Email) {
            await _parentRepository.DeleteChildAccount(Email);
            return Ok();
        }
    } 
}
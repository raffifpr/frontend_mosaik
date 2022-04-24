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
            Response response = new Dtos.Response();
            var status = await _repository.AuthenticateAccount(loginAccountDto.Email, loginAccountDto.Password);
            if (status.Item2 == "false")
            {
                response.Status = "wrong";
                return new JsonResult(response);
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
                response.Status = "failed";
                return new JsonResult(response);
            }
        }

        [HttpPost("parent")]
        [Produces("application/json")]
        public async Task<ActionResult> CreateParentAccount (CreateAccountDtoParent createAccountDtoParent)
        {
            try
            {
                MosaikParent mosaikParent = new()
                {
                    Username = createAccountDtoParent.Username,
                    Email = createAccountDtoParent.Email,
                    Password = createAccountDtoParent.Password
                };
                await _repository.InsertAccount(mosaikParent);

                MosaikParent mosaikParentReceive = await _parentRepository.Get(createAccountDtoParent.Email);

                if (createAccountDtoParent.SupervisorEmails.Length > 0)
                {
                    foreach (var email in createAccountDtoParent.SupervisorEmails)
                    {
                        MosaikChild mosaikChild = await _childRepository.GetChildAccount(email);
                        if (mosaikChild == null || mosaikParentReceive == null)
                        {
                            Response newresponse = new()
                            {
                                Status = "failed",
                            };
                            return new JsonResult(newresponse);
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
                    Response response2 = new()
                    {
                        Status = "success",
                    };
                    return new JsonResult(response2);
                }    
                Response response = new()
                {
                    Status = "success",
                };
                return new JsonResult(response);
            }
            catch 
            {
                Response response = new()
                {
                    Status = "failed",
                };
                return new JsonResult(response);
            }
            
        }

        [HttpPost("child")]
        [Produces("application/json")]
        public async Task<ActionResult> CreateChildAccount (CreateAccountDto createAccountDto)
        {
            try
            {
                MosaikChild mosaikChild = new()
                {
                    Username = createAccountDto.Username,
                    Email = createAccountDto.Email,
                    Password = createAccountDto.Password
                };
                await _repository.InsertAccount(mosaikChild);
                Response response = new()
                {
                    Status = "success",
                };
                return new JsonResult(response);
            }
            catch 
            {
                Response response = new()
                {
                    Status = "failed",
                };
                return new JsonResult(response);
            }
        }

        [HttpPost("changeuser")]
        [Produces("application/json")]
        public async Task<ActionResult> ChangeUsername (ChangeUsernameDto changeUsernameDto)
        {
            string _status = await _repository.Update(changeUsernameDto.Email, changeUsernameDto.Username);
            Response response = new()
            {
                Status = _status,
            };
            return new JsonResult(response);
        }

        [HttpPost("changepass")]
        [Produces("application/json")]
        public async Task<ActionResult> ChangePassword (ChangePasswordDto changePasswordDto)
        {
            string _status = await _repository.UpdatePass(changePasswordDto.Email, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
            Response response = new()
            {
                Status = _status,
            };
            return new JsonResult(response);
        }

        [HttpPost("addsupervise")]
        [Produces("application/json")]
        public async Task<ActionResult> NewChildAccount(CreateSuperviseDto createSuperviseDto)
        {
            string _status = await _parentRepository.InsertChildAccount(createSuperviseDto.Email, createSuperviseDto.ChildEmail);
            Response response = new()
            {
                Status = _status,
            };
            return new JsonResult(response);
        }

        [HttpPost("deletesupervise")]
        [Produces("application/json")]
        public async Task<ActionResult> DeleteChild (CreateSuperviseDto createSuperviseDto) {
            string _status = await _parentRepository.DeleteChildAccount(createSuperviseDto.Email, createSuperviseDto.ChildEmail);
            Response response = new()
            {
                Status = _status,
            };
            return new JsonResult(response);
        }

        [HttpPost("history")]
        [Produces("application/json")]
        public async Task<ActionResult> CreateHistory (CreateHistoryDto createHistoryDto)
        {
            string _status = await _historyRepository.InsertHistory(createHistoryDto.Email, createHistoryDto.Link, createHistoryDto.Time, createHistoryDto.Date);
            Response response = new()
            {
                Status = _status,
            };
            return new JsonResult(response);
        }

        
        [HttpPost("datehistory")]
        [Produces("application/json")]
        public async Task<ActionResult> DateOfHistoryData (DateHistoryRequest dateHistoryRequest)
        {
            DateHistoryResponse dateHistoryResponse = new()
            {
                Total = await _historyRepository.GetTotalDateHistory(dateHistoryRequest.Email),
                DateList = await _historyRepository.GetListDateHistory(dateHistoryRequest.Email)
            };
            return new JsonResult(dateHistoryResponse);
        }

        [HttpPost("historydataperdate")]
        [Produces("application/json")]
        public async Task<ActionResult> HistoryDataPerDate (HistoryDataPerDateRequest historyDataPerDateRequest)
        {
            HistoryDataPerDateResponse historyDataPerDateResponse = new()
            {
                Total = await _historyRepository.GetTotalHistoryPerDate(historyDataPerDateRequest.Email, historyDataPerDateRequest.Date),
                Data = await _historyRepository.GetListDateHistoryPerDate(historyDataPerDateRequest.Email, historyDataPerDateRequest.Date)
            };
            return new JsonResult(historyDataPerDateResponse);
        }

        [HttpPost("restrict")]
        [Produces("application/json")]
        public async Task<ActionResult> CreateRestrict (CreateRestrictDto createRestrictDto)
        {
            string _status = await _restrictRepository.InsertRestrictedLink(createRestrictDto.Email, createRestrictDto.Link);
            Response response = new()
            {
                Status = _status
            };
            return new JsonResult(response);
        }

        [HttpPost("deleterestrict")]
        [Produces("application/json")]
        public async Task<ActionResult> DeleteLink (CreateRestrictDto createRestrictDto)
        {
            string _status = await _restrictRepository.DeleteRestrictedLink(createRestrictDto.Email, createRestrictDto.Link);
            Response response = new()
            {
                Status = _status
            };
            return new JsonResult(response);
        }

        [HttpPost("restrictdata")]
        [Produces("application/json")]
        public async Task<ActionResult> RestrictData (DateHistoryRequest dateHistoryRequest)
        {
            LinkAndNotif linkAndNotif = await _restrictRepository.RestrictedLinkData(dateHistoryRequest.Email);
            int count = await _restrictRepository.GetTotalRestrictedLinkData(dateHistoryRequest.Email);
            RestrictDataResponse restrictDataResponse = new()
            {
                Total = count,
                LinkAndNotif = linkAndNotif
            };
            return new JsonResult(restrictDataResponse);
        }
        
        [HttpPost("authorizerequest")]
        [Produces("application/json")]
        public async Task<ActionResult> AuthorizeRequest (AuthorizeRequestDto authorizeRequestDto)
        {
            string result = await _childRepository.AuthorizeRequest(authorizeRequestDto.Email, authorizeRequestDto.EmailSupervisor, authorizeRequestDto.StatusAccept);
            Response response = new()
            {
                Status = result,
            };
            return new JsonResult(response);
        }

        [HttpGet("exists/{Email}")]
        [Produces("application/json")]
        public async Task<ActionResult> GetCheckEmail(string Email)
        {
            MosaikChild mosaikChild = await _childRepository.GetChildAccount(Email);
            Response response = new()
            {
                Status = mosaikChild == null ? "not exist" : "exist",
            };
            return new JsonResult(response);
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
    } 
}
using Mosaik.id.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mosaik.id.Service
{
    internal class MosaikAPIService
    {
        static string BaseUrl = "http://mosaik-id-ppl.herokuapp.com/api/Mosaik";
        // static string BaseUrl = "https://simple-books-api.glitch.me";
        static HttpClient client;

        static MosaikAPIService()
        {
            try
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(BaseUrl)
                };
            }
            catch
            {
            }
        }

        public static async Task<LoginResponse> PostLogin(string email, string password)
        {
            // Send request
            var loginRequest = new LoginRequest
            {
                email = email,
                password = password
            };
            var json = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("TODO", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<LoginResponse>(stringResponse);
            return jsonResponse;
        }

        public static async Task<RegisterResponse> PostRegisterChild(string username, string email, string password)
        {
            // Send request
            var registerChildRequest = new RegisterChildRequest
            {
                username = username,
                email = email,
                password = password
            };
            var json = JsonConvert.SerializeObject(registerChildRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("TODO", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<RegisterResponse>(stringResponse);
            return jsonResponse;
        }

        public static async Task<EmailCheckResponse> GetCheckEmail(string email)
        {
            var response = await client.GetStringAsync("TODO");
            EmailCheckResponse objResponse = JsonConvert.DeserializeObject<EmailCheckResponse>(response);
            return objResponse; 
        }

        public static async Task<RegisterSupervisorResponse> PostRegisterSupervisor(string username, string email, string password, string[] supervisedEmail)
        {
            // Send request
            var registerSupervisorRequest = new RegisterSupervisorRequest
            {
                username = username,
                email = email,
                password = password,
                supervisedEmail = supervisedEmail
            };
            var json = JsonConvert.SerializeObject(registerSupervisorRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("TODO", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<RegisterSupervisorResponse>(stringResponse);
            return jsonResponse;
        }

        public static async Task<SupervisorLinkResponse> PostSupervisorLinkAccept(string email, string emailSupervisor, string statusAccept)
        {
            // Send request
            var supervisorLinkRequest = new SupervisorLinkRequest
            {
                email = email,
                emailSupervisor = emailSupervisor,
                statusAccept = statusAccept
            };
            var json = JsonConvert.SerializeObject(supervisorLinkRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("TODO", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<SupervisorLinkResponse>(stringResponse);
            return jsonResponse;
        }

        public static async Task<ChangeUsernameResponse> PostChangeUsername(string email, string newUsername)
        {
            // Send request
            var changeUsernameRequest = new ChangeUsernameRequest
            {
                Email = email,
                NewUsername = newUsername
            };
            var json = JsonConvert.SerializeObject(changeUsernameRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("TODO", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<ChangeUsernameResponse>(stringResponse);
            return jsonResponse;
        }

        public static async Task<ChangePasswordResponse> PostChangePassword(string email, string oldPassword, string newPassword)
        {
            // Send request
            var changePasswordRequest = new ChangePasswordRequest
            {
                Email = email,
                OldPassword = oldPassword,
                NewPassword = newPassword
            };
            var json = JsonConvert.SerializeObject(changePasswordRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("TODO", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<ChangePasswordResponse>(stringResponse);
            return jsonResponse;
        }

        public static async Task<AddMoreChildResponse> PostAddMoreChild(string email, string childEmail)
        {
            // Send request
            var addMoreChildRequest = new AddMoreChildRequest
            {
                Email = email,
                ChildEmail = childEmail
            };
            var json = JsonConvert.SerializeObject(addMoreChildRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("TODO", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<AddMoreChildResponse>(stringResponse);
            return jsonResponse;
        }

        public static async Task<RemoveChildResponse> PostRemoveChild(string email, string childEmail)
        {
            // Send request
            var removeChildRequest = new RemoveChildRequest
            {
                Email = email,
                ChildEmail = childEmail
            };
            var json = JsonConvert.SerializeObject(removeChildRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("TODO", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<RemoveChildResponse>(stringResponse);
            return jsonResponse;
        }
    }
}

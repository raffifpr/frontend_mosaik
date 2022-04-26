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
        static string BaseUrl = "https://mosaik-id-ppl.herokuapp.com/api/Mosaik";
        //static string BaseUrl = "https://simple-books-api.glitch.me";
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

        public static async Task<String> PostTestRequest()
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer 392393485dab85fca3828ad54aa38f2b7ae3200badf261ee11956936c2b1aa4a");
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            // Send request
            var testRequest = new TestRequest
            {
                bookId = 5,
                customerName = "nicholas"
            };
            var json = JsonConvert.SerializeObject(testRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("orders", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<TestResponse>(stringResponse);
            return stringResponse;
        }

        public static async Task<LoginResponse> PostLogin(string email, string password)
        {
            // Set url
            try
            {
                var url = BaseUrl + "/login";
                client = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                //client.BaseAddress = new Uri(url);
            }
            catch (Exception ex)
            {
                //return ex.ToString();
            }
            
            // Send request
            var loginRequest = new LoginRequest
            {
                email = email,
                password = password
            };
            var json = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //var req = await content.ReadAsStringAsync();

            // Recieve response
            var response = await client.PostAsync("", content); // harusnya diisi disini '/login', tp ntah knp error
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<LoginResponse>(stringResponse);

            return jsonResponse;
        }

        public static async Task<RegisterResponse> PostRegisterChild(string username, string email, string password)
        {
            // Set url
            try
            {
                var url = BaseUrl + "/child";
                client = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                //client.BaseAddress = new Uri(url);
            }
            catch (Exception ex)
            {
            }

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
            var response = await client.PostAsync("", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<RegisterResponse>(stringResponse);
            return jsonResponse;
        }

        public static async Task<EmailCheckResponse> GetCheckEmail(string email)
        {
            // Set url
            try
            {
                var url = BaseUrl + "/exists/" + email;
                client = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                //client.BaseAddress = new Uri(url);
            }
            catch (Exception ex)
            {
            }

            var response = await client.GetStringAsync("");
            EmailCheckResponse objResponse = JsonConvert.DeserializeObject<EmailCheckResponse>(response);
            return objResponse; 
        }

        public static async Task<RegisterSupervisorResponse> PostRegisterSupervisor(string username, string email, string password, string[] supervisedEmail)
        {
            // Set url
            try
            {
                var url = BaseUrl + "/parent";
                client = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                //client.BaseAddress = new Uri(url);
            }
            catch (Exception ex)
            {
            }

            // Send request
            var registerSupervisorRequest = new RegisterSupervisorRequest
            {
                username = username,
                email = email,
                password = password,
                supervisorEmails = supervisedEmail
            };
            var json = JsonConvert.SerializeObject(registerSupervisorRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //var req = await content.ReadAsStringAsync();

            // Recieve response
            var response = await client.PostAsync("", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<RegisterSupervisorResponse>(stringResponse);
            //return req;
            return jsonResponse;
        }

        public static async Task<SupervisorLinkResponse> PostSupervisorLinkAccept(string email, string emailSupervisor, string statusAccept)
        {
            // Set url
            try
            {
                var url = BaseUrl + "/authorizerequest";
                client = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                //client.BaseAddress = new Uri(url);
            }
            catch (Exception ex)
            {
            }

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
            var response = await client.PostAsync(" ", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<SupervisorLinkResponse>(stringResponse);
            return jsonResponse;
        }

        public static async Task<ChangeUsernameResponse> PostChangeUsername(string email, string newUsername)
        {
            // Set url
            try
            {
                var url = BaseUrl + "/changeuser";
                client = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                //client.BaseAddress = new Uri(url);
            }
            catch (Exception ex)
            {
                //return ex.ToString();
            }

            // Send request
            var changeUsernameRequest = new ChangeUsernameRequest
            {
                email = email,
                username = newUsername
            };
            var json = JsonConvert.SerializeObject(changeUsernameRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<ChangeUsernameResponse>(stringResponse);
            return jsonResponse;
        }

        public static async Task<ChangePasswordResponse> PostChangePassword(string email, string oldPassword, string newPassword)
        {
            // Set url
            try
            {
                var url = BaseUrl + "/changepass";
                client = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                //client.BaseAddress = new Uri(url);
            }
            catch (Exception ex)
            {
                //return ex.ToString();
            }

            // Send request
            var changePasswordRequest = new ChangePasswordRequest
            {
                email = email,
                oldPassword = oldPassword,
                newPassword = newPassword
            };
            var json = JsonConvert.SerializeObject(changePasswordRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Recieve response
            var response = await client.PostAsync("", content);
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

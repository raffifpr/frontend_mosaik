using static Mosaik.idAPI.Services.MosaikRepository;
namespace Mosaik.idAPI.Dtos
{
    public class ChildAuthenticated
    {
        public string Username {get; set;}
        public string Email {get; set;}
        public string AccountStatus {get; set;}
        public List<Account> SupervisedRequests {get; set;}
    }
}
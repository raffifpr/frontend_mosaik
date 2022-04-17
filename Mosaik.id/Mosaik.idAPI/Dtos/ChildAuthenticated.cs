namespace Mosaik.idAPI.Dtos
{
    public class ChildAuthenticated
    {
        public string Username {get; set;}
        public string Email {get; set;}
        public string AccountStatus {get; set;}
        public Tuple<String, String>[] SupervisedRequests {get; set;}
    }
}
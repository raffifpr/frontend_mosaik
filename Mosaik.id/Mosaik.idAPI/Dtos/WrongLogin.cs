namespace Mosaik.idAPI.Dtos
{
    public class WrongLogin
    {
        public string status {get; set;}
        public string username {get; set;}
        public string email {get; set;}
        public string accountStatus {get; set;}
        public List<string> supervisorRequests {get; set;}
        public List<string> supervisedAccounts { get; set; }
    }
}
namespace Mosaik.idAPI.Dtos
{
    public class ParentAuthenticated
    {
        public string Username {get; set;}
        public string Email {get; set;}
        public string AccountStatus {get; set;}
        public Tuple<String, String>[] SupervisorAccounts {get; set;}
    }
}
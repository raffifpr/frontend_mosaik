namespace Mosaik.idAPI.Dtos
{
    public class CreateAccountDtoParent
    {
        public string FullName {get; set;}

        public string Email {get; set;}

        public string Password {get; set;}
        public string[] SupervisorEmails {get; set;}
    }
}
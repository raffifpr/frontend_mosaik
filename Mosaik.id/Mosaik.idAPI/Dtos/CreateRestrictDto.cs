namespace Mosaik.idAPI.Dtos
{
    public class CreateRestrictDto
    {
        public int MosaikChildRestrictID {get; set;}
        public int ChildID {get; set;}
        public string Link {get; set;}
        public Boolean Notif {get; set;}
    }
}
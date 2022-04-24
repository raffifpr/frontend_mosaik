namespace Mosaik.idAPI.Models
{
    public class MosaikParentChild
    {
        public int MosaikParentChildID {get; set;}
        public int parentID {get; set;}
        public int childID {get; set;}
        public bool Authorized {get; set;}
    }
}
namespace Mosaik.idAPI.Dtos
{
    public class ErrorStatus
        {
            public int ErrorCode {get; set;}
            public string ErrorMessage {get; set;}
            public Tuple<int, String, String> Status {get; set;}
        }
}
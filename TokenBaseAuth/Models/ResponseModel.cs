using TokenBaseAuth.DTOs;

namespace TokenBaseAuth.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public Token? Token { get; set; }
    }
}

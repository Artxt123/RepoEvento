using System;

namespace Evento.Infrastructure.DTO
{
    public class TokenDto : JwtDto
    {
        public string Role { get; set; }
    }
}

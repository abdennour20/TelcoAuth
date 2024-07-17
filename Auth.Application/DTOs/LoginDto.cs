using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTOs
{
    public record LoginDto(


        [Required][EmailAddress] string email,

        [Required][StringLength(100, MinimumLength = 8)] string password,

        [Required][StringLength(6, MinimumLength = 6)] string code


        );
    
}

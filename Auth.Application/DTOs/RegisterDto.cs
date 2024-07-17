using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTOs
{
    public record class RegisterDto(


      [Required][StringLength(100)] string firstName,
      [Required][StringLength(100)] string lastName,
      [Required][StringLength(15)] string phoneNumber,
      [Required][EmailAddress] string email,
      [Required][StringLength(100, MinimumLength = 8)] string password
   
  );
}

using Auth.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Contracts
{
    public  interface IUser
    {
      Task<RegisterResponse> RegisterAsync(RegisterDto registerDto);
       Task<LoginResponse> LoginAsync(LoginDto loginDto);
    }
}

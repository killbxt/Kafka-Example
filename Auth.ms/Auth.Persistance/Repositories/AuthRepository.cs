using Auth.Domain.Models;
using Auth.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistance.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;
        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<string> Login(LoginRequest loginRequest, CancellationToken cancellation)
        {
            var user = await _context.Users.Where(u => u.Username == loginRequest.Username).FirstOrDefaultAsync(cancellation);

            if (user != null)
            {
                if(user.Password == loginRequest.Password)
                {
                    return user.Id.ToString();
                }
            }

            return string.Empty;
        }

        public async Task<string> Register(RegisterRequest registerRequest, CancellationToken cancellation)
        {
            if (registerRequest != null) 
            {
                try
                {
                    var user = await _context.AddAsync(new User {Id = Guid.NewGuid(),Name = registerRequest.Name, Username = registerRequest.Username,Password = registerRequest.Password}, cancellation );
                    await _context.SaveChangesAsync(cancellation);
                    //generatejwt and return it to login automatically
                    return user.Entity.Id.ToString();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return string.Empty;
        }
    }
}

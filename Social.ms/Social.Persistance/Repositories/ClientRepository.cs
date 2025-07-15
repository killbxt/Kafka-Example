using Social.Domain.Models;
using Social.Persistance.Context;

namespace Social.Persistance.Repositories
{
    public class ClientRepository : IClientRepository
    {
       private readonly SocialDbContext _context;
       public ClientRepository(SocialDbContext context)
       {
           _context = context;
       }

        public async Task<Client> AddAsync(Client client, CancellationToken cancellation)
        {
           var resultClient = await _context.AddAsync(client, cancellation);
           await _context.SaveChangesAsync(cancellation);
           return resultClient.Entity;
        }

        public async Task<Client> GetById(Guid id, CancellationToken cancellation)
        {
            var resultClient = await _context.Clients.FindAsync(id, cancellation);

            if (resultClient != null) 
            {
                return resultClient;
            }
            return null;
        }
    }
}

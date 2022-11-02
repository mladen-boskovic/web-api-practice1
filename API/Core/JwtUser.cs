using Domain;

namespace API.Core
{
    public class JwtUser : IApplicationUser
    {
        public int Id { get; set; }
        public string Identity { get; set; }
        public string Email { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; }
    }
}

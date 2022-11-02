using Domain;

namespace API.Core
{
    public class AnonymousUser : IApplicationUser
    {
        public int Id => 0;
        public string Identity => "Anonymous";
        public string Email => "anonymous@user.com";
        public IEnumerable<int> UseCaseIds => new List<int> { 1, 2 };
    }
}

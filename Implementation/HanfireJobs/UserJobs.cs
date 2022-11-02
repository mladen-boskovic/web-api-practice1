using DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.HanfireJobs
{
    public class UserJobs
    {
        private readonly WebApiPracticeContext _context;

        public UserJobs(WebApiPracticeContext context)
        {
            _context = context;
        }

        public void GetAllUsers()
        {
            var users = _context.Users.ToList();

            Console.WriteLine("GetAllUsers job started");

            foreach(var user in users)
            {
                Console.WriteLine($"Username: {user.Username}");
            }

            Console.WriteLine("GetAllUsers job ended");
        }
    }
}

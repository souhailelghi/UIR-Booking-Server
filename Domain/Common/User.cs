using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Method to simulate user login
        public bool Login(string email, string password)
        {
            // Logic for user login
            return true; // Placeholder return
        }                                   
    }
}

using Microsoft.AspNetCore.Identity;

namespace SampleIdentity.Entites
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

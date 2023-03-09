using Microsoft.AspNetCore.Identity;

namespace SampleIdentity.Entites
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace StudentResultAppMvcAndApi.Models.Entities
{
    public class ApplicationUser :IdentityUser
    {
        public string FullName { get; set; }
        public DateOnly CreatedDate { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using TempoMapRepository.Models.Domain;

namespace TempoMapRepository.Models.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Map> Maps { get; set;} =  new List<Map>();
    }
}

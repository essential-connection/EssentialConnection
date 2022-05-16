using Microsoft.AspNetCore.Identity;

namespace EssentialConnection.Models
{
    public class Usuario : IdentityUser
    {
        public Usuario()
        {
            Chats = new HashSet<Chat>();
        }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}

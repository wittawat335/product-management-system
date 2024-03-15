using Ecommerce.Web.Models.Position;
namespace Ecommerce.Web.Models.User
{
    public class UserViewModel
    {
        public User User { get; set; }
        public string Action { get; set; }
        public UserViewModel()
        {
            User = new User();
        }
    }
}

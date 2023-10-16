namespace Ecommerce.Web.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public List<Position> listPosition { get; set; }
        public string Action { get; set; }
        public UserViewModel()
        {
            User = new User();
            listPosition = new List<Position>();
        }
    }
}

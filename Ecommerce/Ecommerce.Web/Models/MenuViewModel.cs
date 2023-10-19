namespace Ecommerce.Web.Models
{
    public class MenuViewModel
    {
        public Menu menu { get; set; }
        public List<Menu> listMenu { get; set; }
        public MenuViewModel()
        {
            listMenu = new List<Menu>();
            menu = new Menu();
        }
    }
}

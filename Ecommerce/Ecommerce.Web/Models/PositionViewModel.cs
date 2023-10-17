namespace Ecommerce.Web.Models
{
    public class PositionViewModel
    {
        public Position Position { get; set; }
        public string Action { get; set; }
        public PositionViewModel()
        {
            Position = new Position();
        }
    }
}

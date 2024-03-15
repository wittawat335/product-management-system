namespace Ecommerce.Web.Models.Position
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

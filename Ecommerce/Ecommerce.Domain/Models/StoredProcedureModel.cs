namespace Ecommerce.Domain.Models
{
    public class StoredProcedureModel
    {
    }
    public partial class SP_GET_PERMISSION_BY_POSITION_RESULT
    {
        public string PERM_ID { get; set; }
        public string PERM_PARENT { get; set; }
        public string PERM_TEXT { get; set; }
        public string PERM_SELECT { get; set; }
    }
    public partial class SP_GET_MENU_BY_POSITION_RESULT
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public int MenuLevel { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        public int MenuOrder { get; set; }
        public string Icon { get; set; }
    }
}

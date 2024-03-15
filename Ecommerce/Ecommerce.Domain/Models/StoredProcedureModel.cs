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
}

namespace Ecommerce.Web.Models
{
    public class JsTree
    {
        public List<TreeViewNode> TreeViewNodes { get; set; }
        public List<DataPermissionJsonList> DataPermissionJson { get; set; }
        public List<DataPermissionJsonList> DataPermissionJsonInsert { get; set; }
    }
    public class TreeViewNode
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string select { get; set; }
    }
    public class DataPermissionJsonList
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        //public string state { get; set; }
        public OptionState state { get; set; }
    }
    public class OptionState
    {
        public bool opened { get; set; }
        public bool selected { get; set; }
    }

    public class DataPermissionJsonInsertList
    {
        public string id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public liattrInsert li_attr { get; set; }
        public attrInsert a_attr { get; set; }
        public dataInsert data { get; set; }
        public string parent { get; set; }
        public stateInsert state { get; set; }
        public string type { get; set; }
        public List<DataPermissionJsonInsertList> children { get; set; }

    }
    public class liattrInsert
    {
        public string id { get; set; }
    }
    public class attrInsert
    {
        public string href { get; set; }
        public string id { get; set; }
    }
    public class dataInsert
    {

    }
    public class stateInsert
    {
        public bool loaded { get; set; }
        public bool opened { get; set; }
        public bool selected { get; set; }
        public bool disabled { get; set; }
    }
}

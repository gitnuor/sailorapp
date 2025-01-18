namespace PostgreCRUD.Models
{
    public class Grandparent
    {
        public int GrandparentId { get; set; }
        public string GrandparentName { get; set; }
    }
    public class Parent
    {
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public int GrandparentId { get; set; }
    }
    public class Child
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public int ParentId { get; set; }
    }
    public class GreatGrandchild
    {
        public int GreatGrandchildId { get; set; }
        public string GreatGrandchildName { get; set; }
        public int ChildId { get; set; }
    }
    //public class InsertDataViewModel
    //{
    //    public Grandparent Grandparent { get; set; }
    //    public List<Parent> Parent { get; set; }
    //    public List<Child> Child { get; set; }
    //    public List<GreatGrandchild> GreatGrandchild { get; set; }
    //}
    public class InsertDataViewModel
    {
        public Grandparent Grandparent { get; set; }
        public Parent Parent { get; set; }
        public Child Child { get; set; }
        public GreatGrandchild GreatGrandchild { get; set; }
    }
}

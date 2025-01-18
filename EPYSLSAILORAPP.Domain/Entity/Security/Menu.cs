using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Security
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Menu")]
    public class Menu :DapperExt
    {
        #region Table properties
        [Key]
        public Int64? menu_id { get; set; }
        public Int64? parent_id { get; set; }

        //public string DockPanel { get; set; }
        public string? menu_caption { get; set; }

        public string? navigate_url { get; set; }

        public string? image_url { get; set; }


        public Int64? seq_no { get; set; }

        public bool? is_visible { get; set; }

        //public bool is_api { get; set; }

        #endregion

        #region Additional properties
        //[Write(false)]
        //public override bool IsModified => menu_id > 0 || EntityState == EntityState.Modified;

        //[Write(false)]
        //public virtual ICollection<MenuDependence> MenuDependences { get; set; }

        [Write(false)]
        public virtual List<MenuParam> MenuParams { get; set; }

        //[Write(false)]
        //public virtual ICollection<MessageQueue> MessageQueues { get; set; }

        //[Write(false)]
        //public virtual ICollection<SecurityRuleMenu> SecurityRuleMenus { get; set; }

        [Write(false)]
        public List<Menu> Childs { get; set; }

        #endregion

        public Menu()
        {
            menu_id = 0;
            seq_no = 0;
            is_visible = true;
          //  EntityState = EntityState.Added;
           // MenuDependences = new List<MenuDependence>();
            MenuParams = new List<MenuParam>();
          //  MessageQueues = new List<MessageQueue>();
           // SecurityRuleMenus = new List<SecurityRuleMenu>();
        }
    }
}

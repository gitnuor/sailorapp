
using BDO.Core.Base;
using EPYSLSAILORAPP.Domain.Entity;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using System.ComponentModel.DataAnnotations;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class owin_role_DTO :BaseEntity
    {

        public Int64? owin_role_id { get; set; }

        [Required]
        public String role_name { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public List<owin_role_permission_DTO> Role_Permission_List { get; set; }

        public List<menu_action_entity> MenuAction_List { get; set; }
        public List<menu_entity> Menu_List { get; set; }

    }
}

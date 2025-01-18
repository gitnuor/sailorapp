
using BDO.Core.Base;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.Entity;
using Postgrest.Attributes;
using Postgrest.Models;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_item_structure_group_sub_DTO:BaseEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public Int64? gen_item_structure_group_sub_id { get; set; }

        [Required]
        public Int64? item_structure_group_id { get; set; }
        [Required]
        public String sub_group_name { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }
        [Required]
        public Int64? measurement_unit_id { get; set; }
        [Required]
        public Int64? default_measurement_unit_detail_id { get; set; }

        public Int64? secondary_measurement_unit_id { get; set; }

        public Int64? secondary_measurement_unit_detail_id { get; set; }

        public String measurement_unit { get; set; }
        public String default_measurement_unit_detail { get; set; }
        public String secondary_measurement_unit { get; set; }
        public String secondary_measurement_unit_detail { get; set; }
        public List<gen_measurement_unit_DTO> List_Measurement_Unit {  get; set; }
        public List<gen_measurement_unit_detail_DTO> List_Measurement_Unit_Detail { get; set; }

        public List<gen_item_structure_group_DTO> List_Item_Structure_Group { get; set; }

        public List<gen_item_structure_group_sub_segment_mapping_DTO> List_Mapping { get; set; }

    }
}

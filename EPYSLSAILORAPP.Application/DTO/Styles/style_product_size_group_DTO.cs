
using Postgrest.Attributes;
using Postgrest.Models;
using System.ComponentModel.DataAnnotations;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class style_product_size_group_DTO
    {

        public Int64? style_product_size_group_id { get; set; }
        [Required]
        public bool? is_separate_price { get; set; }
        [Required]
        public String style_product_size_group_name { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public List<string> DeletedDetList {  get; set; }
        public List<style_product_size_group_details_DTO> DetList { get; set; }

        public string size_group_details_json { get; set; }


    }
}

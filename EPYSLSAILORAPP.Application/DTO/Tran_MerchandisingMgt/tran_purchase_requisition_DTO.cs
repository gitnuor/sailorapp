using BDO.Core.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;


namespace EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt
{

    public class tran_purchase_requisition_DTO : BaseEntity
    {

        public long? pr_id { get; set; }
        public long? item_structure_group_id { get; set; }

        // [Required]
        public string? pr_no { get; set; }
        [Required]
        public DateTime? pr_date { get; set; }
        [GreaterThan("pr_date", ErrorMessage = "Delivery date cannot be smaller than PR date.")]
        public DateTime? delivery_date { get; set; }

        public long? event_id { get; set; }

        public long? techpack_id { get; set; }

        public long? designer_id { get; set; }
        public string? designer_name { get; set; }

        public long? merchandiser_id { get; set; }
        public string? merchandiser_name { get; set; }
        public string? techpack_number { get; set; }
        public string? techpack_name { get; set; }

        public long? currency_id { get; set; }

        public long? delivery_unit_id { get; set; }
        public string? delivery_unit_name { get; set; }

        public string? delivery_unit_address { get; set; }

        public string? remarks { get; set; }

        public long? supplier_id { get; set; }

        public string? supplier_address { get; set; }
        public string? supplier_name { get; set; }

        public string? supplier_concern_person { get; set; }

        public string? terms_condition_list { get; set; }

        public string? test_requirements_list { get; set; }

        public string? document_list { get; set; }
        public string? del_list { get; set; }

        public int? is_submitted { get; set; }

        public int? is_approved { get; set; }

        public long? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public string? approve_remarks { get; set; }

        public long? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }
        public long? gen_item_structure_group_id { get; set; }
        public DateTime? date_updated { get; set; }
        public dropdown_entity ddlsupplier_info { get; set; }
        public dropdown_TechpackEntity ddlTechpack_info { get; set; }
        public List<file_upload> List_Files { get; set; }
        public List<tran_purchase_requisition_dtl_DTO> details { get; set; }
        public List<string?> DeleteList { get; set; }

        public bool? is_acknowledged { get; set; }
        public long? acknowledged_by { get; set; }
        public DateTime? acknowledged_date { get; set; }
        public string? acknowledged_remarks { get; set; }

        public String suggested_supplier_name { get; set; }
    }

    public class GreaterThanAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _otherProperty;

        public GreaterThanAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherPropertyValue = validationContext.ObjectType.GetProperty(_otherProperty).GetValue(validationContext.ObjectInstance);
            if (value == null || otherPropertyValue == null) return ValidationResult.Success;

            if ((DateTime)value < (DateTime)otherPropertyValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-greaterthan", ErrorMessage);
            context.Attributes.Add("data-val-greaterthan-otherproperty", _otherProperty);
        }
    }

}

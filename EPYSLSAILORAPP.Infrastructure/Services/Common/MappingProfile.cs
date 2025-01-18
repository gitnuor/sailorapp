using AutoMapper;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Application.DTO.TranTables;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.GenTables;

using EPYSLSAILORAPP.Domain.Entity.SupplyChain;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.Entity.Tran_Tables;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Domain.Security;
using ServiceStack;

namespace EPYSLSAILORAPP.Infrastructure.Services.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Default mapping when property names are same
          

          

            CreateMap<owin_user_entity, owin_user_DTO>();

            CreateMap<owin_user_DTO, owin_user_entity>();
            #region Setup
            CreateMap<gen_fiscal_year_dto, gen_fiscal_year>();
           

            CreateMap<gen_outlet_DTO, gen_outlet_entity>();
            CreateMap<gen_outlet_entity, gen_outlet_DTO>();

           

            #endregion

            #region Style
            CreateMap<style_item_product_DTO, style_item_product_entity>();
            CreateMap<style_item_product_entity, style_item_product_DTO>();

            CreateMap<style_item_product_sub_category_DTO, style_item_product_sub_category_entity>();
            CreateMap<style_item_product_sub_category_entity, style_item_product_sub_category_DTO>();

            CreateMap<style_category_DTO, style_category_entity>();
            CreateMap<style_category_entity, style_category_DTO>();

            CreateMap<style_gender_DTO, style_gender_entity>();
            CreateMap<style_gender_entity, style_gender_DTO>();

            CreateMap<style_item_origin_DTO, style_item_origin_entity>();
            CreateMap<style_item_origin_entity, style_item_origin_DTO>();

            CreateMap<style_item_type_DTO, style_item_type_entity>();
            CreateMap<style_item_type_entity, style_item_type_DTO>();

            CreateMap<style_master_category_DTO, style_master_category_entity>();
            CreateMap<style_master_category_entity, style_master_category_DTO>();

            CreateMap<style_product_size_group_DTO, style_product_size_group_entity>();
            CreateMap<style_product_size_group_entity, style_product_size_group_DTO>();

            CreateMap<style_product_size_group_details_DTO, style_product_size_group_details_entity>();
            CreateMap<style_product_size_group_details_entity, style_product_size_group_details_DTO>();

            CreateMap<style_product_type_DTO, style_product_type_entity>();
            CreateMap<style_product_type_entity, style_product_type_DTO>();

            CreateMap<style_item_product_separate_entity, style_item_product_entity>();
            CreateMap<style_item_product_entity, style_item_product_separate_entity>();//tran_plan_allocate_style_DTO


            #endregion //

            #region Business Plan
            CreateMap<tran_bp_year_dto, tran_bp_year>();
            CreateMap<tran_bp_year, tran_bp_year_dto>();

            

            CreateMap<tran_bp_event_month_dto, tran_bp_event_month>();
            CreateMap<tran_bp_event_month, tran_bp_event_month_dto>();

            

            //tran_bp_event_month
         

            CreateMap<BpEventOutletDTO, tran_bp_event_month>();
            CreateMap<tran_bp_event_month, BpEventOutletDTO>();

            

            CreateMap<BusinessPlanDTO, tran_bp_year_dto>();
            CreateMap<tran_bp_year_dto, BusinessPlanDTO>();

            CreateMap<GenSeasonEventConfigurationDTO, gen_season_event_config>();
            CreateMap<gen_season_event_config, GenSeasonEventConfigurationDTO>();
            #endregion

            #region Range Plan
            CreateMap<tran_range_plan_details_DTO, tran_range_plan_details_entity>();
            CreateMap<tran_range_plan_details_entity, tran_range_plan_details_DTO>();

            CreateMap<tran_range_plan_entity, tran_range_plan_DTO>();
            CreateMap<tran_range_plan_DTO, tran_range_plan_entity>();

            CreateMap<tran_range_plan_details_size_DTO, tran_range_plan_details_size_entity>();
            CreateMap<tran_range_plan_details_size_entity, tran_range_plan_details_size_DTO>();


            CreateMap<tran_range_plan_events_DTO, tran_range_plan_events_entity>();
            CreateMap<tran_range_plan_events_entity, tran_range_plan_events_DTO>();


            CreateMap<tran_va_plan_entity, tran_va_plan_DTO>();
            CreateMap<tran_va_plan_DTO, tran_va_plan_entity>();

            CreateMap<tran_va_plan_events_entity, tran_va_plan_events_DTO>();
            CreateMap<tran_va_plan_events_DTO, tran_va_plan_events_entity>();

            CreateMap<tran_plan_allocate_entity, tran_plan_allocate_DTO>();
            CreateMap<tran_plan_allocate_DTO, tran_plan_allocate_entity>();

            CreateMap<tran_plan_allocate_style_DTO, tran_plan_allocate_style_entity>();
            CreateMap<tran_plan_allocate_style_entity, tran_plan_allocate_style_DTO>();

            CreateMap<tran_plan_allocate_style_color_size_entity, tran_plan_allocate_style_color_size_DTO>();
            CreateMap<tran_plan_allocate_style_color_size_DTO, tran_plan_allocate_style_color_size_entity>();

            CreateMap<tran_plan_allocate_style_color_entity, tran_plan_allocate_style_color_DTO>();
            CreateMap<tran_plan_allocate_style_color_DTO, tran_plan_allocate_style_color_entity>();

            CreateMap<tran_va_plan_detl_duration_plan_entity, tran_va_plan_detl_duration_plan_DTO>();
            CreateMap<tran_va_plan_detl_duration_plan_DTO, tran_va_plan_detl_duration_plan_entity>();


            CreateMap<rpc_range_plan_getfor_createupdate_DTO, rpc_range_plan_getfor_view_DTO>();
            CreateMap<rpc_range_plan_getfor_view_DTO, rpc_range_plan_getfor_createupdate_DTO>();

            #endregion

            CreateMap<tran_va_plan_Filter_DTO, tran_plan_allocate_DTO>();
            CreateMap<tran_plan_allocate_DTO, tran_va_plan_Filter_DTO>();

            CreateMap<tran_plan_allocate_style_color_DTO, rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>();
            CreateMap<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO, tran_plan_allocate_style_color_DTO>();

            CreateMap<style_master_category_structure_subgroup_mapping_DTO, style_master_category_structure_subgroup_mapping_entity>();
            CreateMap<style_master_category_structure_subgroup_mapping_entity, style_master_category_structure_subgroup_mapping_DTO>();

            CreateMap<gen_item_structure_group_DTO, gen_item_structure_group_entity>();
            CreateMap<gen_item_structure_group_entity, gen_item_structure_group_DTO>();

            CreateMap<gen_item_structure_group_sub_DTO, gen_item_structure_group_sub_entity>();
            CreateMap<gen_item_structure_group_sub_entity, gen_item_structure_group_sub_DTO>();

            CreateMap<tran_sample_order_DTO, tran_sample_order_entity>();
            CreateMap<tran_sample_order_entity, tran_sample_order_DTO>();

            CreateMap<gen_unit_DTO, gen_unit_entity>();
            CreateMap<gen_unit_entity, gen_unit_DTO>();

            CreateMap<tran_va_plan_Filter_DTO, tran_sample_order_DTO>();
            CreateMap<tran_sample_order_DTO, tran_va_plan_Filter_DTO>();


            CreateMap<tran_sample_order_embellishment_entity, tran_sample_order_embellishment_DTO>();
            CreateMap<tran_sample_order_embellishment_DTO, tran_sample_order_embellishment_entity>();


            CreateMap<tran_sample_order_subgroup_DTO, tran_sample_order_subgroup_entity>();
            CreateMap<tran_sample_order_subgroup_entity, tran_sample_order_subgroup_DTO>();

            CreateMap<tran_sample_order_detl_entity, tran_sample_order_detl_DTO>();
            CreateMap<tran_sample_order_detl_DTO, tran_sample_order_detl_entity>();

            CreateMap<gen_segment_detl_DTO, gen_segment_detl_entity>();
            CreateMap<gen_segment_detl_entity, gen_segment_detl_DTO>();

            CreateMap<gen_segment_entity, gen_segment_DTO>();
            CreateMap<gen_segment_DTO, gen_segment_entity>();

            CreateMap<style_fabric_DTO, style_fabric_entity>();
            CreateMap<style_fabric_entity, style_fabric_DTO>();

            CreateMap<style_fabric_detl_DTO, style_fabric_detl_entity>();
            CreateMap<style_fabric_detl_entity, style_fabric_detl_DTO>();

            CreateMap<style_product_unit_DTO, style_product_unit_entity>();
            CreateMap<style_product_unit_entity, style_product_unit_DTO>();

            CreateMap<tran_pre_costing_DTO, tran_pre_costing_entity>();
            CreateMap<tran_pre_costing_entity, tran_pre_costing_DTO>();

            CreateMap<gen_measurement_unit_DTO, gen_measurement_unit_entity>();
            CreateMap<gen_measurement_unit_entity, gen_measurement_unit_DTO>();

            CreateMap<gen_process_master_DTO, gen_process_master_entity>();
            CreateMap<gen_process_master_entity, gen_process_master_DTO>();

            CreateMap<tran_pre_costing_item_detail_DTO, tran_pre_costing_item_detail_entity>()
                .ForMember(dest => dest.current_state, opt => opt.MapFrom(src => src.current_state));
            

            CreateMap<tran_pre_costing_item_detail_entity, tran_pre_costing_item_detail_DTO>()
                 .ForMember(dest => dest.current_state, opt => opt.MapFrom(src => src.current_state));



            CreateMap<tran_pre_costing_item_subcontract_detail_DTO, tran_pre_costing_item_subcontract_detail_entity>()
               .ForMember(dest => dest.current_state, opt => opt.MapFrom(src => src.current_state));

            CreateMap<tran_pre_costing_item_subcontract_detail_entity, tran_pre_costing_item_subcontract_detail_DTO>()
                 .ForMember(dest => dest.current_state, opt => opt.MapFrom(src => src.current_state));



            CreateMap<tran_pre_costing_item_embellishment_detail_DTO, tran_pre_costing_item_embellishment_detail_entity>()
               .ForMember(dest => dest.current_state, opt => opt.MapFrom(src => src.current_state));

            CreateMap<tran_pre_costing_item_embellishment_detail_entity, tran_pre_costing_item_embellishment_detail_DTO>()
                 .ForMember(dest => dest.current_state, opt => opt.MapFrom(src => src.current_state));

            CreateMap<gen_item_structure_group_sub_segment_mapping_entity, gen_item_structure_group_sub_segment_mapping_DTO>();
            CreateMap<gen_item_structure_group_sub_segment_mapping_DTO, gen_item_structure_group_sub_segment_mapping_entity>();

            CreateMap<gen_garment_part_DTO, gen_garment_part_entity>();
            CreateMap<gen_garment_part_entity, gen_garment_part_DTO>();

            
            CreateMap<tran_pre_costing_DTO, tran_sample_order_DTO>();
            CreateMap<tran_sample_order_DTO, tran_pre_costing_DTO>();

            CreateMap<tran_tech_pack_DTO, tran_tech_pack_entity>();
            CreateMap<tran_tech_pack_entity, tran_tech_pack_DTO>();

            CreateMap<tran_mcd_requisition_slip_DTO, tran_mcd_requisition_slip_entity>();
            CreateMap<tran_mcd_requisition_slip_entity, tran_mcd_requisition_slip_DTO>();

            CreateMap<tran_mcd_requisition_slip_detail_DTO, tran_mcd_requisition_slip_detail_entity>();
            CreateMap<tran_mcd_requisition_slip_detail_entity, tran_mcd_requisition_slip_detail_DTO>();

            CreateMap<tran_tech_pack_embellishment_info_DTO, tran_tech_pack_embellishment_info_entity>();
            CreateMap<tran_tech_pack_embellishment_info_entity, tran_tech_pack_embellishment_info_DTO>();

            CreateMap<tran_tech_pack_embellishment_det_DTO, tran_tech_pack_embellishment_det_entity>();
            CreateMap<tran_tech_pack_embellishment_det_entity, tran_tech_pack_embellishment_det_DTO>();

            CreateMap<tran_tech_pack_color_DTO, tran_tech_pack_color_entity>();
            CreateMap<tran_tech_pack_color_entity, tran_tech_pack_color_DTO>();

            CreateMap<tran_tech_pack_color_size_DTO, tran_tech_pack_color_size_entity>();
            CreateMap<tran_tech_pack_color_size_entity, tran_tech_pack_color_size_DTO>();



           

            CreateMap<rpc_proc_sp_get_sample_order_data_edit_DTO, tran_sample_order_DTO>();
            CreateMap<tran_sample_order_DTO, rpc_proc_sp_get_sample_order_data_edit_DTO>();

            CreateMap<rpc_proc_sp_get_pre_costing_data_DTO, tran_sample_order_DTO>();
            CreateMap<tran_sample_order_DTO, rpc_proc_sp_get_pre_costing_data_DTO>();

            CreateMap<rpc_proc_sp_get_pre_costing_data_edit_DTO, tran_pre_costing_DTO>();
            CreateMap<tran_pre_costing_DTO, rpc_proc_sp_get_pre_costing_data_edit_DTO>();

            CreateMap<rpc_proc_sp_get_pre_costing_data_edit_DTO, tran_sample_order_DTO>();
            CreateMap<tran_sample_order_DTO, rpc_proc_sp_get_pre_costing_data_edit_DTO>();


            CreateMap<rpc_proc_sp_get_tech_pack_data_DTO, tran_pre_costing_DTO>();
            CreateMap<tran_pre_costing_DTO, rpc_proc_sp_get_tech_pack_data_DTO>();

            CreateMap<rpc_proc_sp_get_tech_pack_data_DTO, tran_sample_order_DTO>();
            CreateMap<tran_sample_order_DTO, rpc_proc_sp_get_tech_pack_data_DTO>();


            CreateMap<rpc_proc_sp_get_tech_pack_data_edit_DTO, tran_pre_costing_DTO>();
            CreateMap<tran_pre_costing_DTO, rpc_proc_sp_get_tech_pack_data_edit_DTO>();

            CreateMap<rpc_proc_sp_get_tech_pack_data_edit_DTO, tran_sample_order_DTO>();
            CreateMap<tran_sample_order_DTO, rpc_proc_sp_get_tech_pack_data_edit_DTO>();

            CreateMap<rpc_proc_sp_get_tech_pack_data_edit_DTO, tran_tech_pack_DTO>();
            CreateMap<tran_tech_pack_DTO, rpc_proc_sp_get_tech_pack_data_edit_DTO>();

            CreateMap<gen_supplier_information_DTO, gen_supplier_information_entity>();
            CreateMap<gen_supplier_information_entity, gen_supplier_information_DTO>();

            CreateMap<gen_supplier_information_DTO, rpc_proc_get_supplier_new_DTO>();
            CreateMap<rpc_proc_get_supplier_new_DTO, gen_supplier_information_DTO>();

            CreateMap<tran_purchase_requisition_DTO, tran_purchase_requisition_entity>();
            CreateMap<tran_purchase_requisition_entity, tran_purchase_requisition_DTO>();
			CreateMap<tran_purchase_requisition_dtl_DTO, tran_purchase_requisition_dtl_entity>();
			CreateMap<tran_purchase_requisition_dtl_entity, tran_purchase_requisition_dtl_DTO>();
			CreateMap<gen_item_master_DTO, gen_item_master_entity>();
			CreateMap<gen_item_master_entity, gen_item_master_DTO>();
            CreateMap<tran_scm_po_details_DTO, tran_scm_po_details_entity>();
            CreateMap<tran_scm_po_details_entity, tran_scm_po_details_DTO>();
            CreateMap<tran_scm_po_DTO, tran_scm_po_entity>();
            CreateMap<tran_scm_po_entity, tran_scm_po_DTO>();

            CreateMap<tran_service_work_order_DTO, tran_service_work_order_entity>();
            CreateMap<tran_service_work_order_entity, tran_service_work_order_DTO>();
            CreateMap<tran_service_work_order_detail_DTO, tran_service_work_order_detail_entity>();
            CreateMap<tran_service_work_order_detail_entity, tran_service_work_order_detail_DTO>();



            CreateMap<tran_scm_bill_submission_DTO, tran_scm_bill_submission_entity>();
            CreateMap<tran_scm_bill_submission_entity, tran_scm_bill_submission_DTO>();


            CreateMap<tran_draft_purchase_requisition_DTO, tran_draft_purchase_requisition_entity>();
            CreateMap<tran_draft_purchase_requisition_entity, tran_draft_purchase_requisition_DTO>();
            CreateMap<tran_draft_purchase_requisition_dtl_DTO, tran_draft_purchase_requisition_dtl_entity>();
            CreateMap<tran_draft_purchase_requisition_dtl_entity, tran_draft_purchase_requisition_dtl_DTO>();

        }

    }
}

create function sp_get_pre_costing_details_by_masterid(pre_costing_id_filter bigint)
    returns TABLE(item_structure_group_id bigint, all_segment_text text, measurement_unit text, tran_pre_costing_item_detail_id bigint, tran_pre_costing_id bigint, gen_item_structure_group_sub_id bigint, segment_det1_id bigint, segment_det2_id bigint, segment_det3_id bigint, segment_det4_id bigint, segment_det5_id bigint, segment_det6_id bigint, segment_det7_id bigint, segment_det8_id bigint, segment_det9_id bigint, segment_det10_id bigint, segment_det11_id bigint, segment_det12_id bigint, segment_det13_id bigint, segment_det14_id bigint, segment_det15_id bigint, segment_det1_text text, segment_det2_text text, segment_det3_text text, segment_det4_text text, segment_det5_text text, segment_det6_text text, segment_det7_text text, segment_det8_text text, segment_det9_text text, segment_det10_text text, segment_det11_text text, segment_det12_text text, segment_det13_text text, segment_det14_text text, segment_det15_text text, measurement_unit_detail_id bigint, order_quantity numeric, wastage numeric, total_order_quantity numeric, price_per_unit numeric, total_price numeric, remarks text, added_by bigint, date_added timestamp with time zone, updated_by bigint, date_updated timestamp with time zone)
    language plpgsql
as
$$
DECLARE 
 
Begin

	
RETURN QUERY 	
		
SELECT gisgs.item_structure_group_id,
CONCAT_WS(' ', COALESCE(ms.segment_det1_text,'') ,
COALESCE(ms.segment_det2_text,'') ,
COALESCE(ms.segment_det3_text,'') ,
COALESCE(ms.segment_det4_text,'') ,
COALESCE(ms.segment_det5_text,'') ,
COALESCE(ms.segment_det6_text,'') ,
COALESCE(ms.segment_det7_text,'') ,
COALESCE(ms.segment_det8_text,'') ,
COALESCE(ms.segment_det9_text,'') ,
COALESCE(ms.segment_det10_text,'') ,
COALESCE(ms.segment_det11_text,'') ,
COALESCE(ms.segment_det12_text,'') ,
COALESCE(ms.segment_det13_text,'') ,
COALESCE(ms.segment_det14_text,'') ,
COALESCE(ms.segment_det15_text,''))  as all_segment_text,
det.unit_detail_title as measurement_unit,
ms.tran_pre_costing_item_detail_id,
ms.tran_pre_costing_id,
ms.gen_item_structure_group_sub_id,
ms.segment_det1_id,
ms.segment_det2_id,
ms.segment_det3_id,
ms.segment_det4_id,
ms.segment_det5_id,
ms.segment_det6_id,
ms.segment_det7_id,
ms.segment_det8_id,
ms.segment_det9_id,
ms.segment_det10_id,
ms.segment_det11_id,
ms.segment_det12_id,
ms.segment_det13_id,
ms.segment_det14_id,
ms.segment_det15_id,
ms.segment_det1_text,
ms.segment_det2_text,
ms.segment_det3_text,
ms.segment_det4_text,
ms.segment_det5_text,
ms.segment_det6_text,
ms.segment_det7_text,
ms.segment_det8_text,
ms.segment_det9_text,
ms.segment_det10_text,
ms.segment_det11_text,
ms.segment_det12_text,
ms.segment_det13_text,
ms.segment_det14_text,
ms.segment_det15_text,
ms.measurement_unit_detail_id,
ms.order_quantity,
ms.wastage,
ms.total_order_quantity,
ms.price_per_unit,
ms.total_price,
ms.remarks,
ms.added_by,
ms.date_added,
ms.updated_by,
ms.date_updated

FROM public.tran_pre_costing_item_detail ms
inner join public.gen_measurement_unit_detail det on ms.measurement_unit_detail_id =det.gen_measurement_unit_detail_id
inner join public.gen_item_structure_group_sub gisgs on gisgs.gen_item_structure_group_sub_id = ms.gen_item_structure_group_sub_id
where ms.tran_pre_costing_id=pre_costing_id_filter;
  		
END;
$$;

alter function sp_get_pre_costing_details_by_masterid(bigint) owner to postgres;

grant execute on function sp_get_pre_costing_details_by_masterid(bigint) to anon;

grant execute on function sp_get_pre_costing_details_by_masterid(bigint) to authenticated;

grant execute on function sp_get_pre_costing_details_by_masterid(bigint) to service_role;


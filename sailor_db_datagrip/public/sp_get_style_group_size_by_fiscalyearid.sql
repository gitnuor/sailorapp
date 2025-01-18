create function sp_get_style_group_size_by_fiscalyearid(_fiscal_year_id bigint, _event_id bigint)
    returns TABLE(range_plan_detail_size_id bigint, range_plan_detail_id bigint, is_separate_price boolean, style_product_size_group_id bigint, style_product_size_group_detid bigint, style_product_size text, size_quantity bigint, size_per_pc_mrp_value numeric, style_item_product_id bigint, size_per_pc_cpu_value numeric)
    language plpgsql
as
$$
DECLARE  
 
 Begin

		RETURN QUERY 
		Select
		rpds.range_plan_detail_size_id,
		rpds.range_plan_detail_id,
		sg.is_separate_price, sg.style_product_size_group_id,
		sgd.style_product_size_group_detid,sgd.style_product_size
		,rpds.size_quantity, rpds.size_per_pc_mrp_value,
		rpd.style_item_product_id,
		rpds.size_per_pc_cpu_value
		
		
		From public.style_product_size_group sg
		
		inner join public.style_product_size_group_details sgd
		on sg.style_product_size_group_id=sgd.style_product_size_group_id
		
		
		left outer join public.tran_range_plan_details_size rpds
		on rpds.style_product_size_group_detid=sgd.style_product_size_group_detid
		
		
		left outer join public.tran_range_plan_details rpd 
		on rpd.range_plan_detail_id=rpds.range_plan_detail_id
		
		left outer join public.tran_bp_event tbe on
		tbe.tran_bp_event_id=rpd.tran_bp_event_id
		
		left outer join public.tran_range_plan rp 
		on rp.range_plan_id=rpd.range_plan_id
		
		left outer join public.tran_bp_year bp 
		on bp.tran_bp_year_id=rp.tran_bp_year_id
		
		where bp.fiscal_year_id=_fiscal_year_id  and tbe.event_id=_event_id;
		
		
		
END;
$$;

alter function sp_get_style_group_size_by_fiscalyearid(bigint, bigint) owner to postgres;

grant execute on function sp_get_style_group_size_by_fiscalyearid(bigint, bigint) to anon;

grant execute on function sp_get_style_group_size_by_fiscalyearid(bigint, bigint) to authenticated;

grant execute on function sp_get_style_group_size_by_fiscalyearid(bigint, bigint) to service_role;


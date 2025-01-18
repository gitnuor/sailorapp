create function sp_get_va_plan_events(_fiscal_year_id bigint)
    returns TABLE(tran_va_plan_event_id bigint, tran_range_plan_event_id bigint, range_plan_id bigint, yearly_gross_sales numeric, tran_bp_event_id bigint, event_gross_sales numeric, readygoods_qnty bigint, readygoods_value numeric, event_id bigint, fiscal_year_id bigint, event_title text, total_range_plan_quantity bigint, total_mrp_value numeric, total_cpu_value numeric, is_finalised boolean, added_product bigint)
    language plpgsql
as
$$
DECLARE 
 
Begin

		RETURN QUERY 	
		
		SELECT 
tvpe.tran_va_plan_event_id,
tvpe.tran_range_plan_event_id, trp.range_plan_id, tby.gross_sales yearly_gross_sales, tbe.tran_bp_event_id,
tbe.event_gross_sales,tbe.readygoods_qnty,tbe.readygoods_value,
tbe.event_id,tby.fiscal_year_id,gsc.event_title,
COALESCE(trpe.total_range_plan_quantity,0) as total_range_plan_quantity,
COALESCE(trpe.total_mrp_value,0)as total_mrp_Value,
 COALESCE(trpe.total_cpu_value,0) as total_cpu_value,
 tvpe.is_finalised,
 (
	 select count(det.tran_va_plan_detl_id) from  public.tran_va_plan_detl det
	  inner join public.tran_va_plan_events det_event 
	 on det_event.tran_va_plan_event_id=det.tran_va_plan_event_id
     inner join public.tran_va_plan master 
	 on master.tran_va_plan_id=det_event.tran_va_plan_id
     where  master.tran_range_plan_id=trp.range_plan_id
	 and det_event.tran_range_plan_event_id=trpe.tran_range_plan_event_id
 ) added_product
FROM public.tran_bp_event tbe

inner join public.tran_bp_year tby on tby.tran_bp_year_id=tbe.tran_bp_year_id

inner join public.gen_season_event_config gsc on gsc.event_id=tbe.event_id 

left outer join public.tran_range_plan_events trpe on trpe.tran_bp_event_id=tbe.tran_bp_event_id

left outer join public.tran_range_plan trp on trp.tran_bp_year_id=tbe.tran_bp_year_id

left outer join public.tran_va_plan_events tvpe on tvpe.tran_range_plan_event_id=
trpe.tran_range_plan_event_id

where tby.fiscal_year_id=_fiscal_year_id;

		
END;
$$;

alter function sp_get_va_plan_events(bigint) owner to postgres;

grant execute on function sp_get_va_plan_events(bigint) to anon;

grant execute on function sp_get_va_plan_events(bigint) to authenticated;

grant execute on function sp_get_va_plan_events(bigint) to service_role;


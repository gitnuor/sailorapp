create function sp_get_event_details_allphase(_fiscal_year_id_filter bigint, _event_id_filter bigint)
    returns TABLE(tran_bp_year_id bigint, tran_bp_event_id bigint, range_plan_id bigint, tran_va_plan_event_id bigint)
    language plpgsql
as
$$
DECLARE 
 
Begin

RETURN QUERY 	
		
select tby.tran_bp_year_id,tbe.tran_bp_event_id, trpe.range_plan_id, tvpe.tran_va_plan_event_id

from public.tran_bp_year tby 
inner join public.tran_bp_event tbe on tbe.tran_bp_year_id=tby.tran_bp_year_id
left outer join public.tran_range_plan_events trpe on trpe.tran_bp_event_id=tbe.tran_bp_event_id
left outer join public.tran_va_plan_events tvpe on tvpe.tran_range_plan_event_id=trpe.tran_range_plan_event_id

where tby.fiscal_year_id=_fiscal_year_id_filter and tbe.event_id=_event_id_filter;
	
END;
$$;

alter function sp_get_event_details_allphase(bigint, bigint) owner to postgres;

grant execute on function sp_get_event_details_allphase(bigint, bigint) to anon;

grant execute on function sp_get_event_details_allphase(bigint, bigint) to authenticated;

grant execute on function sp_get_event_details_allphase(bigint, bigint) to service_role;


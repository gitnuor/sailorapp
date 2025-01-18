create function sp_get_tran_va_plan_summary()
    returns TABLE(tran_va_plan_id bigint, year_name text, fiscal_year_id bigint, yearly_gross_sales numeric, yearly_total_mrp numeric, yearly_total_cpu numeric, yearly_total_quantity bigint, range_plan_id bigint, tran_bp_year_id bigint, remarks text, is_submitted boolean, is_approved boolean, approved_by bigint, approve_date timestamp with time zone, approve_remarks text)
    language plpgsql
as
$$
DECLARE 
 
Begin

	
		RETURN QUERY 	
		
		SELECT tvp.tran_va_plan_id, fy.year_name, tby.fiscal_year_id, 
	tby.gross_sales yearly_gross_sales,

(select CAST(COALESCE(sum(total_mrp_value),0) as numeric) from public.tran_range_plan_events d where d.range_plan_id=trp.range_plan_id)
as yearly_total_mrp,
(select  CAST(COALESCE(sum(total_cpu_value),0) as numeric) from public.tran_range_plan_events d where d.range_plan_id=trp.range_plan_id)
as yearly_total_cpu,
(select  CAST(COALESCE(sum(total_range_plan_quantity),0) as bigint) from public.tran_range_plan_events d where d.range_plan_id=trp.range_plan_id)
as yearly_total_quantity

,trp.range_plan_id, trp.tran_bp_year_id, tvp.remarks, tvp.is_submitted, tvp.is_approved, 
tvp.approved_by, tvp.approve_date, tvp.approve_remarks

  
FROM public.tran_range_plan trp

right outer join public.tran_bp_year tby on tby.tran_bp_year_id=trp.tran_bp_year_id 

left outer join public.gen_fiscal_year fy on fy.fiscal_year_id= tby.fiscal_year_id

left outer join public.tran_va_plan tvp 
on tvp.tran_range_plan_id=trp.range_plan_id

where  tby.is_approved=true

order by  tby.fiscal_year_id desc;

		
END;
$$;

alter function sp_get_tran_va_plan_summary() owner to postgres;

grant execute on function sp_get_tran_va_plan_summary() to anon;

grant execute on function sp_get_tran_va_plan_summary() to authenticated;

grant execute on function sp_get_tran_va_plan_summary() to service_role;


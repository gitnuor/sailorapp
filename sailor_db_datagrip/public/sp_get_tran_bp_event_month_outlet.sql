create function sp_get_tran_bp_event_month_outlet(filter_fiscal_year_id bigint)
    returns TABLE(tran_bp_event_month_outlet_id bigint, tran_bp_event_month_id bigint, outlet_id bigint, outlet_gross_sales numeric, tran_bp_year_id bigint, tran_bp_event_id bigint, month_id bigint, event_id bigint, yearly_gross_sales numeric, event_gross_sales numeric, readygoods_qnty bigint, readygoods_value numeric, monthly_gross_sales numeric, added_by bigint, date_added timestamp with time zone)
    language plpgsql
as
$$

 Begin
 RETURN QUERY
 
 
	SELECT 
	tvmo.tran_bp_event_month_outlet_id ,
     tvmo.tran_bp_event_month_id ,
	 tvmo.outlet_id ,
     tvmo.outlet_gross_sales,
	 ty.tran_bp_year_id,
	 tse.tran_bp_event_id,
	 tvm.month_id,
	 tse.event_id,
	 ty.gross_sales as yearly_gross_sales,
	 tse.event_gross_sales,
	 tse.readygoods_qnty,
	 tse.readygoods_value,
	 tvm.monthly_gross_sales, ty.added_by,ty.date_added
	 
	FROM 
	 public.gen_fiscal_year y 
	inner join public.tran_bp_year ty on ty.fiscal_year_id=y.fiscal_year_id
	inner join public.tran_bp_event tse on tse.tran_bp_year_id=ty.tran_bp_year_id
	inner join public.tran_bp_event_month tvm on tvm.tran_bp_event_id=tse.tran_bp_event_id
	inner join public.tran_bp_event_month_outlet tvmo 
	on tvmo.tran_bp_event_month_id=tvm.tran_bp_event_month_id
	where y.fiscal_year_id=filter_fiscal_year_id;
	END;
$$;

alter function sp_get_tran_bp_event_month_outlet(bigint) owner to postgres;

grant execute on function sp_get_tran_bp_event_month_outlet(bigint) to anon;

grant execute on function sp_get_tran_bp_event_month_outlet(bigint) to authenticated;

grant execute on function sp_get_tran_bp_event_month_outlet(bigint) to service_role;


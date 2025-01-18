create function event_tran_data_getall(filter_fiscal_year_id bigint)
    returns TABLE(sequence bigint, season_name text, year_name text, gross_sales numeric, event_gross_sales numeric, readygoods_qnty bigint, readygoods_value numeric, tran_bp_event_id bigint, event_id bigint, season_id bigint, fiscal_year_id bigint, start_date timestamp with time zone, end_date timestamp with time zone, start_month_id bigint, end_month_id bigint, event_title text, is_active boolean, added_by bigint, updated_by bigint, date_added timestamp with time zone, date_updated timestamp without time zone)
    language plpgsql
as
$$

 Begin
 RETURN QUERY
 
SELECT s.sequence, s.season_name,y.year_name
	,ty.gross_sales
	,tse.event_gross_sales 
	,tse.readygoods_qnty 
	,tse.readygoods_value 
	,tse.tran_bp_event_id
	
	 ,tr.event_id ,
 tr.season_id ,
 tr.fiscal_year_id,
 tr.start_date ,
 tr.end_date,
    tr.start_month_id ,
    tr.end_month_id ,
    
    tr.event_title  ,
    tr.is_active ,
    tr.added_by ,
    tr.updated_by ,
    tr.date_added  ,
    tr.date_updated
	FROM 
	public.gen_season_event_config tr 
	inner join public.gen_season s on s.season_id=tr.season_id
	inner join public.gen_fiscal_year y on y.fiscal_year_id=tr.fiscal_year_id
	left outer join public.tran_bp_year ty on ty.fiscal_year_id=tr.fiscal_year_id
	left outer join public.tran_bp_event tse on tse.event_id=tr.event_id
	
	where tr.fiscal_year_id=filter_fiscal_year_id;
	
	END;
$$;

alter function event_tran_data_getall(bigint) owner to postgres;

grant execute on function event_tran_data_getall(bigint) to anon;

grant execute on function event_tran_data_getall(bigint) to authenticated;

grant execute on function event_tran_data_getall(bigint) to service_role;


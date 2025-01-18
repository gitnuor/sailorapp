create function get_bp_year_event_month_data(p_tran_bp_year_id bigint)
    returns TABLE(tran_bp_event_month_id bigint, tran_bp_event_id bigint, month_id bigint, monthly_gross_sales numeric)
    language plpgsql
as
$$
BEGIN
    RETURN QUERY
    SELECT
      
        m.tran_bp_event_month_id,m.tran_bp_event_id,m.month_id,
		m.monthly_gross_sales
    FROM
        tran_bp_year tbp_y
    INNER JOIN
        tran_bp_event e ON tbp_y.tran_bp_year_id = e.tran_bp_year_id
    INNER JOIN
        tran_bp_event_month m ON e.tran_bp_event_id = m.tran_bp_event_id
	inner join 
		gen_fiscal_year y on tbp_y.fiscal_year_id=y.fiscal_year_id
    WHERE
        y.fiscal_year_id= p_tran_bp_year_id;
END;
$$;

alter function get_bp_year_event_month_data(bigint) owner to postgres;

grant execute on function get_bp_year_event_month_data(bigint) to anon;

grant execute on function get_bp_year_event_month_data(bigint) to authenticated;

grant execute on function get_bp_year_event_month_data(bigint) to service_role;


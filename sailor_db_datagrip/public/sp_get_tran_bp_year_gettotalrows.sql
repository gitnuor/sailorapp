create function sp_get_tran_bp_year_gettotalrows()
    returns TABLE(totalcount bigint)
    language plpgsql
as
$$
DECLARE 
 
 Begin


		RETURN QUERY 
		Select count(mn.*) TotalCount
		From public.tran_bp_year mn;
		
END;
$$;

alter function sp_get_tran_bp_year_gettotalrows() owner to postgres;

grant execute on function sp_get_tran_bp_year_gettotalrows() to anon;

grant execute on function sp_get_tran_bp_year_gettotalrows() to authenticated;

grant execute on function sp_get_tran_bp_year_gettotalrows() to service_role;


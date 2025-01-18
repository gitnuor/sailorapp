create function sp_get_measurement_unit_details_by_masterid(gen_measurement_unit_id_filter bigint)
    returns TABLE(gen_measurement_unit_id bigint, unit_name text, unit_detail_title text, unit_detail_display text, relative_factor numeric, gen_measurement_unit_detail_id bigint)
    language plpgsql
as
$$
DECLARE  
 
 Begin

		RETURN QUERY 
		
		select master.gen_measurement_unit_id , master.unit_name,
		detl.unit_detail_title,detl.unit_detail_display,detl.relative_factor,
		detl.gen_measurement_unit_detail_id
		
		
		from public.gen_measurement_unit master inner join public.gen_measurement_unit_detail detl
		on master.gen_measurement_unit_id=detl.gen_measurement_unit_id
		
		where master.gen_measurement_unit_id=gen_measurement_unit_id_filter;
		
END;
$$;

alter function sp_get_measurement_unit_details_by_masterid(bigint) owner to postgres;

grant execute on function sp_get_measurement_unit_details_by_masterid(bigint) to anon;

grant execute on function sp_get_measurement_unit_details_by_masterid(bigint) to authenticated;

grant execute on function sp_get_measurement_unit_details_by_masterid(bigint) to service_role;


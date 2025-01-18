create function rpc_get_table_data_count_for_select2()
    returns TABLE(gen_segment_detl_count bigint, gen_supplier_information_count bigint, gen_garment_part_count bigint)
    language plpgsql
as
$$
BEGIN
    RETURN QUERY
    SELECT
    (SELECT count(*) FROM public.gen_segment_detl WHERE is_active = true) AS gen_segment_detl_count,
    (SELECT count(*) FROM public.gen_supplier_information) AS gen_supplier_information_count,
    (SELECT count(*) FROM public.gen_garment_part) AS gen_garment_part_count;


END;
$$;

alter function rpc_get_table_data_count_for_select2() owner to postgres;

grant execute on function rpc_get_table_data_count_for_select2() to anon;

grant execute on function rpc_get_table_data_count_for_select2() to authenticated;

grant execute on function rpc_get_table_data_count_for_select2() to service_role;


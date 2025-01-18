create function sp_get_order_subgroup_json(tran_va_plan_detl_id_filter bigint, tran_sample_order_id_filter bigint)
    returns TABLE(subgroup_json text)
    language plpgsql
as
$$
DECLARE  
 
 Begin

		RETURN QUERY 
	
		select 
	   cast( json_agg(json_build_object(
        'tran_sample_order_id', details_table.tran_sample_order_id,
        'gen_item_structure_group_sub_id', details_table.gen_item_structure_group_sub_id
       
    )) as text) AS subgroup_json
	 
FROM
    public.tran_sample_order main_table
LEFT JOIN
   public.tran_sample_order_subgroup details_table   
   ON main_table.tran_sample_order_id = details_table.tran_sample_order_id
   where main_table.tran_va_plan_detl_id=tran_va_plan_detl_id_filter
   and  main_table.tran_sample_order_id=tran_sample_order_id_filter
   GROUP BY
    main_table.tran_sample_order_id;
END;
$$;

alter function sp_get_order_subgroup_json(bigint, bigint) owner to postgres;

grant execute on function sp_get_order_subgroup_json(bigint, bigint) to anon;

grant execute on function sp_get_order_subgroup_json(bigint, bigint) to authenticated;

grant execute on function sp_get_order_subgroup_json(bigint, bigint) to service_role;


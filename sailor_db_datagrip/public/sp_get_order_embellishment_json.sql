create function sp_get_order_embellishment_json(tran_va_plan_detl_id_filter bigint, tran_sample_order_id_filter bigint)
    returns TABLE(embellishment_json text)
    language plpgsql
as
$$
DECLARE  
 
 Begin

		RETURN QUERY 
	
		select 
	   cast(   json_agg(json_build_object(
        'tran_sample_order_id', details_table2.tran_sample_order_id,
        'embellishment_id', details_table2.embellishment_id
       
    )) as text) AS embellishment_json

	 
FROM
    public.tran_sample_order main_table
 LEFT JOIN
   public.tran_sample_order_embellishment details_table2   
   ON main_table.tran_sample_order_id = details_table2.tran_sample_order_id
   where main_table.tran_va_plan_detl_id=tran_va_plan_detl_id_filter
      and  main_table.tran_sample_order_id=tran_sample_order_id_filter
   GROUP BY
    main_table.tran_sample_order_id;
END;
$$;

alter function sp_get_order_embellishment_json(bigint, bigint) owner to postgres;

grant execute on function sp_get_order_embellishment_json(bigint, bigint) to anon;

grant execute on function sp_get_order_embellishment_json(bigint, bigint) to authenticated;

grant execute on function sp_get_order_embellishment_json(bigint, bigint) to service_role;


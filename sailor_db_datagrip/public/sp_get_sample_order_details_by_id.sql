create function sp_get_sample_order_details_by_id(tran_sample_order_id_filter bigint)
    returns TABLE(tran_va_plan_detl_style_id bigint, tran_sample_order_id bigint, tran_va_plan_detl_id bigint, tran_sample_order_number text, order_date timestamp with time zone, delivery_date timestamp with time zone, delivery_unit_id bigint, order_quantity bigint, subgroup_json text, embellishment_json text)
    language plpgsql
as
$$
DECLARE 
 
Begin

	
		RETURN QUERY 	
		
		SELECT main_table.tran_va_plan_detl_style_id,
	
	   main_table.tran_sample_order_id ,main_table.tran_va_plan_detl_id,
	main_table.tran_sample_order_number ,
	main_table.order_date ,
	main_table.delivery_date ,
	main_table.delivery_unit_id ,
	main_table.order_quantity ,

	sp_get_order_subgroup_json(main_table.tran_va_plan_detl_id, main_table.tran_sample_order_id) subgroup_json,
	sp_get_order_embellishment_json(main_table.tran_va_plan_detl_id, main_table.tran_sample_order_id) embellishment_json
	 
FROM
    public.tran_sample_order main_table

where
 main_table.tran_sample_order_id=tran_sample_order_id_filter
    
GROUP BY main_table.tran_va_plan_detl_style_id,
    main_table.tran_sample_order_id,
	 main_table.tran_va_plan_detl_id,
	main_table.tran_sample_order_number ,
	main_table.order_date ,
	main_table.delivery_date ,
	main_table.delivery_unit_id ,
	main_table.order_quantity;
  

		
END;
$$;

alter function sp_get_sample_order_details_by_id(bigint) owner to postgres;

grant execute on function sp_get_sample_order_details_by_id(bigint) to anon;

grant execute on function sp_get_sample_order_details_by_id(bigint) to authenticated;

grant execute on function sp_get_sample_order_details_by_id(bigint) to service_role;


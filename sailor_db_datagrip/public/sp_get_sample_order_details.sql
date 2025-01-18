create function sp_get_sample_order_details(designer_member_id_filter bigint)
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

where main_table.tran_va_plan_detl_id in 
 (
 select tmp2.tran_va_plan_detl_id from public.tran_va_plan_detl tmp2
	 inner join public.tran_va_plan_detl_style tmp3 on tmp2.tran_va_plan_detl_id=tmp3.tran_va_plan_detl_id
	 and tmp3.designer_member_id=designer_member_id_filter
 )
    
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

alter function sp_get_sample_order_details(bigint) owner to postgres;

grant execute on function sp_get_sample_order_details(bigint) to anon;

grant execute on function sp_get_sample_order_details(bigint) to authenticated;

grant execute on function sp_get_sample_order_details(bigint) to service_role;


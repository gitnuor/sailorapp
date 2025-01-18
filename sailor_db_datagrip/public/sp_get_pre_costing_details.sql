create function sp_get_pre_costing_details(designer_member_id_filter bigint)
    returns TABLE(tran_pre_costing_id bigint, tran_sample_order_id bigint, tran_va_plan_detl_id bigint, tran_sample_order_number text, order_date timestamp with time zone, delivery_date timestamp with time zone, delivery_unit_id bigint, order_quantity bigint)
    language plpgsql
as
$$
DECLARE 
 
Begin

	
		RETURN QUERY 	
		
		SELECT
		detl.tran_pre_costing_id,
	   main_table.tran_sample_order_id ,main_table.tran_va_plan_detl_id,
	main_table.tran_sample_order_number ,
	main_table.order_date ,
	main_table.delivery_date ,
	main_table.delivery_unit_id ,
	main_table.order_quantity 
 
FROM
    public.tran_sample_order main_table
	left outer join public.tran_pre_costing detl on 
	main_table.tran_sample_order_id=detl.tran_sample_order_id

where main_table.tran_va_plan_detl_id in 
 (
 select tmp2.tran_va_plan_detl_id from public.tran_va_plan_detl tmp2
	 inner join public.tran_va_plan_detl_style tmp3 on tmp2.tran_va_plan_detl_id=tmp3.tran_va_plan_detl_id
	 and tmp3.designer_member_id=designer_member_id_filter
 );
  		
END;
$$;

alter function sp_get_pre_costing_details(bigint) owner to postgres;

grant execute on function sp_get_pre_costing_details(bigint) to anon;

grant execute on function sp_get_pre_costing_details(bigint) to authenticated;

grant execute on function sp_get_pre_costing_details(bigint) to service_role;


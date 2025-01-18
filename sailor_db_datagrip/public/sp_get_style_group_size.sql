create function sp_get_style_group_size()
    returns TABLE(is_separate_price boolean, style_product_size_group_id bigint, style_product_size_group_detid bigint, style_product_size text)
    language plpgsql
as
$$
DECLARE  
 
 Begin

		RETURN QUERY 
		Select sg.is_separate_price, sg.style_product_size_group_id,
		sgd.style_product_size_group_detid,sgd.style_product_size
		From public.style_product_size_group sg inner join public.style_product_size_group_details sgd
		on sg.style_product_size_group_id=sgd.style_product_size_group_id;
		--where sg.style_product_size_group_id=group_id;
		
END;
$$;

alter function sp_get_style_group_size() owner to postgres;

grant execute on function sp_get_style_group_size() to anon;

grant execute on function sp_get_style_group_size() to authenticated;

grant execute on function sp_get_style_group_size() to service_role;


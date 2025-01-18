create function sp_get_style_product_item(fiscal_year_id_filter bigint DEFAULT NULL::bigint, style_master_category_id_filter bigint DEFAULT NULL::bigint, style_gender_id_filter bigint DEFAULT NULL::bigint, style_item_origin_id_filter bigint DEFAULT NULL::bigint, style_item_type_id_filter bigint DEFAULT NULL::bigint, style_product_type_id_filter bigint DEFAULT NULL::bigint)
    returns TABLE(style_product_size_group_name text, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint, fiscal_year_id bigint, style_product_size_group_id bigint)
    language plpgsql
as
$$
 
 Begin
 RETURN QUERY
 
    
 SELECT vw.style_product_size_group_name, vw.style_item_product_name,
    vw.style_item_type_name,
    vw.style_product_type_name,
    vw.style_item_origin_name,
    vw.style_gender_name,
    vw.master_category_name,

    vw.style_item_product_id,
    vw.style_item_type_id,
    vw.style_product_type_id,
    vw.style_item_origin_id,
    vw.style_gender_id,
    vw.style_master_category_id,
    vw.fiscal_year_id,
    vw.style_product_size_group_id
   FROM public.vw_style_item_product vw
	
	where 
	
	(CASE WHEN fiscal_year_id_filter is NULL THEN 1 WHEN vw.fiscal_year_id  = fiscal_year_id_filter THEN 1 ELSE 0 END = 1)
	AND (CASE WHEN style_master_category_id_filter is NULL THEN 1 WHEN vw.style_master_category_id  = style_master_category_id_filter THEN 1 ELSE 0 END = 1)
	AND (CASE WHEN style_gender_id_filter is NULL THEN 1 WHEN vw.style_gender_id  = style_gender_id_filter THEN 1 ELSE 0 END = 1)
	AND (CASE WHEN style_item_origin_id_filter is NULL THEN 1 WHEN vw.style_item_origin_id  = style_item_origin_id_filter THEN 1 ELSE 0 END = 1)
	AND (CASE WHEN style_item_type_id_filter is NULL THEN 1 WHEN vw.style_product_type_id  = style_item_type_id_filter THEN 1 ELSE 0 END = 1)
	AND (CASE WHEN style_product_type_id_filter is NULL THEN 1 WHEN vw.style_product_type_id  = style_product_type_id_filter THEN 1 ELSE 0 END = 1);
	
	
	END;
$$;

alter function sp_get_style_product_item(bigint, bigint, bigint, bigint, bigint, bigint) owner to postgres;

grant execute on function sp_get_style_product_item(bigint, bigint, bigint, bigint, bigint, bigint) to anon;

grant execute on function sp_get_style_product_item(bigint, bigint, bigint, bigint, bigint, bigint) to authenticated;

grant execute on function sp_get_style_product_item(bigint, bigint, bigint, bigint, bigint, bigint) to service_role;


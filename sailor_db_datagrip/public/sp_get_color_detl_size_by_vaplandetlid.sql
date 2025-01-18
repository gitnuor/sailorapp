create function sp_get_color_detl_size_by_vaplandetlid(va_plan_detl_id_filter bigint)
    returns TABLE(style_product_size_details text)
    language plpgsql
as
$$
DECLARE
    _styleproductsizedetails text;
Begin

        SELECT  jsonb_agg(jsonb_build_object(
                                  'designer_member_id',vw.designer_member_id,
                                  'style_item_product_sub_category_id',vw.style_item_product_sub_category_id,
                                  'sub_category_name',vw.sub_category_name,
                                  'style_product_size',vw.style_product_size,
                                  'style_embellishment_ids',vw.style_embellishment_ids::text,
                                  'style_item_product_name',vw.style_item_product_name,
                                  'tran_va_plan_detl_style_color_size_id',vw.tran_va_plan_detl_style_color_size_id,
                                  'tran_va_plan_detl_style_color_id',vw.tran_va_plan_detl_style_color_id,
                                  'style_product_size_group_detid',vw.style_product_size_group_detid,
                                  'style_color_size_quantity',vw.style_color_size_quantity,
                                  'style_color_quantity',vw.style_color_quantity,
                                  'color_code',vw.color_code,
                                  'tran_va_plan_detl_style_id',vw.tran_va_plan_detl_style_id,
                                  'color_code_gen',vw.color_code_gen,
                                  'no_of_color',vw.no_of_color,
                                  'style_quantity',vw.style_quantity,
                                  'style_code',vw.style_code,
                                  'tran_va_plan_detl_id',vw.tran_va_plan_detl_id,
                                  'style_code_gen',vw.style_code_gen,
                                  'no_of_style',vw.no_of_style,
                                  'style_item_product_id',vw.style_item_product_id,
                                  'range_plan_detail_id',vw.range_plan_detail_id,
                                  'tran_va_plan_event_id',vw.tran_va_plan_event_id
                              ))
        into _styleproductsizedetails
        FROM public.vw_va_detl_style vw
        where vw.tran_va_plan_detl_id = va_plan_detl_id_filter;

        RETURN QUERY
            select _styleproductsizedetails style_product_size_details;
END;
$$;

alter function sp_get_color_detl_size_by_vaplandetlid(bigint) owner to postgres;

grant execute on function sp_get_color_detl_size_by_vaplandetlid(bigint) to anon;

grant execute on function sp_get_color_detl_size_by_vaplandetlid(bigint) to authenticated;

grant execute on function sp_get_color_detl_size_by_vaplandetlid(bigint) to service_role;


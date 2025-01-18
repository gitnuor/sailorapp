create procedure update_tran_va_plan_detl_style_color_size(IN p_tran_va_plan_detl_style_color_size_id bigint, IN p_tran_va_plan_detl_style_color_id bigint, IN p_style_product_size_group_detid bigint, IN p_style_color_size_quantity bigint, IN p_updated_by bigint)
    language plpgsql
as
$$
BEGIN
    -- Update tran_va_plan_detl_style_color_size table
    UPDATE tran_va_plan_detl_style_color_size
    SET
        tran_va_plan_detl_style_color_id = p_tran_va_plan_detl_style_color_id,
        style_product_size_group_detid = p_style_product_size_group_detid,
        style_color_size_quantity = p_style_color_size_quantity,
        updated_by = p_updated_by,    
        date_updated = now()
    WHERE
        tran_va_plan_detl_style_color_size_id = p_tran_va_plan_detl_style_color_size_id;
END;
$$;

alter procedure update_tran_va_plan_detl_style_color_size(bigint, bigint, bigint, bigint, bigint) owner to postgres;

grant execute on procedure update_tran_va_plan_detl_style_color_size(bigint, bigint, bigint, bigint, bigint) to anon;

grant execute on procedure update_tran_va_plan_detl_style_color_size(bigint, bigint, bigint, bigint, bigint) to authenticated;

grant execute on procedure update_tran_va_plan_detl_style_color_size(bigint, bigint, bigint, bigint, bigint) to service_role;


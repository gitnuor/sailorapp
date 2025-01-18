create procedure update_tran_va_plan_detl_style(IN p_tran_va_plan_detl_style_id bigint, IN p_style_code text, IN p_style_quantity bigint, IN p_no_of_color bigint, IN p_color_code_gen text, IN p_updated_by bigint, IN p_style_embellishment_ids text, IN p_style_item_product_sub_category_id bigint)
    language plpgsql
as
$$
BEGIN
    -- Update tran_va_plan_detl_style table
    UPDATE tran_va_plan_detl_style
    SET
        style_code = p_style_code,
        style_quantity = p_style_quantity,
        no_of_color = p_no_of_color,
        color_code_gen = p_color_code_gen,
        updated_by = p_updated_by,
        date_updated = now(),
        style_embellishment_ids = p_style_embellishment_ids::json,
       
        style_item_product_sub_category_id = p_style_item_product_sub_category_id
    WHERE
        tran_va_plan_detl_style_id = p_tran_va_plan_detl_style_id;
END;
$$;

alter procedure update_tran_va_plan_detl_style(bigint, text, bigint, bigint, text, bigint, text, bigint) owner to postgres;

grant execute on procedure update_tran_va_plan_detl_style(bigint, text, bigint, bigint, text, bigint, text, bigint) to anon;

grant execute on procedure update_tran_va_plan_detl_style(bigint, text, bigint, bigint, text, bigint, text, bigint) to authenticated;

grant execute on procedure update_tran_va_plan_detl_style(bigint, text, bigint, bigint, text, bigint, text, bigint) to service_role;


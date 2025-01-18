create procedure insert_tran_va_plan_detl_style(IN p_tran_va_plan_detl_id bigint, IN p_style_code text, IN p_style_quantity bigint, IN p_no_of_color bigint, IN p_color_code_gen text, IN p_added_by bigint, IN p_style_embellishment_ids text, IN p_style_item_product_sub_category_id bigint, OUT p_tran_va_plan_detl_style_id bigint)
    language plpgsql
as
$$
BEGIN
    -- Insert into tran_va_plan_detl_style table
    INSERT INTO tran_va_plan_detl_style (
        tran_va_plan_detl_id,
        style_code,
        style_quantity,
        no_of_color,
        color_code_gen,
        added_by,
     
        date_added,
        
        style_embellishment_ids,
       
        style_item_product_sub_category_id
    )
    VALUES (
        p_tran_va_plan_detl_id,
        p_style_code,
        p_style_quantity,
        p_no_of_color,
        p_color_code_gen,
        p_added_by,
      	NOW(),
        p_style_embellishment_ids ::json,
       
        p_style_item_product_sub_category_id
    )
    RETURNING tran_va_plan_detl_style_id INTO p_tran_va_plan_detl_style_id;
END;
$$;

alter procedure insert_tran_va_plan_detl_style(bigint, text, bigint, bigint, text, bigint, text, bigint, out bigint) owner to postgres;

grant execute on procedure insert_tran_va_plan_detl_style(bigint, text, bigint, bigint, text, bigint, text, bigint, out bigint) to anon;

grant execute on procedure insert_tran_va_plan_detl_style(bigint, text, bigint, bigint, text, bigint, text, bigint, out bigint) to authenticated;

grant execute on procedure insert_tran_va_plan_detl_style(bigint, text, bigint, bigint, text, bigint, text, bigint, out bigint) to service_role;


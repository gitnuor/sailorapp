create procedure update_tran_va_plan_detl(IN p_tran_va_plan_detl_id bigint, IN p_style_item_product_id bigint, IN p_no_of_style bigint, IN p_style_code_gen text, IN p_updated_by bigint)
    language plpgsql
as
$$
BEGIN
    -- Update tran_va_plan_detl table
    UPDATE tran_va_plan_detl
    SET
       
        style_item_product_id = p_style_item_product_id,
        no_of_style = p_no_of_style,
        style_code_gen = p_style_code_gen,
      
        updated_by = p_updated_by,
       
        date_updated = NoW()
    WHERE
        tran_va_plan_detl_id = p_tran_va_plan_detl_id;
END;
$$;

alter procedure update_tran_va_plan_detl(bigint, bigint, bigint, text, bigint) owner to postgres;

grant execute on procedure update_tran_va_plan_detl(bigint, bigint, bigint, text, bigint) to anon;

grant execute on procedure update_tran_va_plan_detl(bigint, bigint, bigint, text, bigint) to authenticated;

grant execute on procedure update_tran_va_plan_detl(bigint, bigint, bigint, text, bigint) to service_role;


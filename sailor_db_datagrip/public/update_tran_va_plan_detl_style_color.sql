create procedure update_tran_va_plan_detl_style_color(IN p_tran_va_plan_detl_style_color_id bigint, IN p_color_code text, IN p_style_color_quantity bigint, IN p_updated_by bigint)
    language plpgsql
as
$$
BEGIN
    -- Update tran_va_plan_detl_style_color table
    UPDATE tran_va_plan_detl_style_color
    SET
        color_code = p_color_code,
        style_color_quantity = p_style_color_quantity,
        updated_by = p_updated_by,
        date_updated = NOW()
    WHERE
        tran_va_plan_detl_style_color_id = p_tran_va_plan_detl_style_color_id;
END;
$$;

alter procedure update_tran_va_plan_detl_style_color(bigint, text, bigint, bigint) owner to postgres;

grant execute on procedure update_tran_va_plan_detl_style_color(bigint, text, bigint, bigint) to anon;

grant execute on procedure update_tran_va_plan_detl_style_color(bigint, text, bigint, bigint) to authenticated;

grant execute on procedure update_tran_va_plan_detl_style_color(bigint, text, bigint, bigint) to service_role;


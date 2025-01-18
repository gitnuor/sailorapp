create procedure insert_tran_va_plan_detl_style_color(IN p_tran_va_plan_detl_style_id bigint, IN p_color_code text, IN p_style_color_quantity bigint, IN p_added_by bigint, OUT p_tran_va_plan_detl_style_color_id bigint)
    language plpgsql
as
$$
BEGIN
    -- Insert into tran_va_plan_detl_style_color table
    INSERT INTO tran_va_plan_detl_style_color (
        tran_va_plan_detl_style_id,
        color_code,
        style_color_quantity,
        added_by,
		date_added
    )
    VALUES (
        p_tran_va_plan_detl_style_id,
        p_color_code,
        p_style_color_quantity,
        p_added_by,
		NOW()
    )
    RETURNING tran_va_plan_detl_style_color_id INTO p_tran_va_plan_detl_style_color_id;
END;
$$;

alter procedure insert_tran_va_plan_detl_style_color(bigint, text, bigint, bigint, out bigint) owner to postgres;

grant execute on procedure insert_tran_va_plan_detl_style_color(bigint, text, bigint, bigint, out bigint) to anon;

grant execute on procedure insert_tran_va_plan_detl_style_color(bigint, text, bigint, bigint, out bigint) to authenticated;

grant execute on procedure insert_tran_va_plan_detl_style_color(bigint, text, bigint, bigint, out bigint) to service_role;


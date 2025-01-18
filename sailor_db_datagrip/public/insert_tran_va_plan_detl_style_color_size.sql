create procedure insert_tran_va_plan_detl_style_color_size(IN p_tran_va_plan_detl_style_color_id bigint, IN p_style_product_size_group_detid bigint, IN p_style_color_size_quantity bigint, IN p_added_by bigint)
    language plpgsql
as
$$
BEGIN
    -- Insert into tran_va_plan_detl_style_color_size table
    INSERT INTO tran_va_plan_detl_style_color_size (
        tran_va_plan_detl_style_color_id,
        style_product_size_group_detid,
        style_color_size_quantity,
        added_by,
		date_added
    )
    VALUES (
        p_tran_va_plan_detl_style_color_id,
        p_style_product_size_group_detid,
        p_style_color_size_quantity,
        p_added_by,
		NOW()
    );
    
END;
$$;

alter procedure insert_tran_va_plan_detl_style_color_size(bigint, bigint, bigint, bigint) owner to postgres;

grant execute on procedure insert_tran_va_plan_detl_style_color_size(bigint, bigint, bigint, bigint) to anon;

grant execute on procedure insert_tran_va_plan_detl_style_color_size(bigint, bigint, bigint, bigint) to authenticated;

grant execute on procedure insert_tran_va_plan_detl_style_color_size(bigint, bigint, bigint, bigint) to service_role;


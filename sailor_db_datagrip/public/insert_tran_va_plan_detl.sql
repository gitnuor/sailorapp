create procedure insert_tran_va_plan_detl(IN p_tran_va_plan_event_id bigint, IN p_range_plan_detail_id bigint, IN p_style_item_product_id bigint, IN p_no_of_style bigint, IN p_style_code_gen text, IN p_added_by bigint, OUT p_tran_va_plan_detl_id bigint)
    language plpgsql
as
$$
BEGIN
    -- Insert into tran_va_plan_detl table
    INSERT INTO tran_va_plan_detl (
       
        tran_va_plan_event_id,
        range_plan_detail_id,
        style_item_product_id,
        no_of_style,
        style_code_gen,
		date_added,
        added_by
    )
    VALUES (

        p_tran_va_plan_event_id,
        p_range_plan_detail_id,
        p_style_item_product_id,
        p_no_of_style,
        p_style_code_gen,NOW(),
        p_added_by
    )
	RETURNING tran_va_plan_detl_id INTO p_tran_va_plan_detl_id BIGINT;
END;
$$;

alter procedure insert_tran_va_plan_detl(bigint, bigint, bigint, bigint, text, bigint, out bigint) owner to postgres;

grant execute on procedure insert_tran_va_plan_detl(bigint, bigint, bigint, bigint, text, bigint, out bigint) to anon;

grant execute on procedure insert_tran_va_plan_detl(bigint, bigint, bigint, bigint, text, bigint, out bigint) to authenticated;

grant execute on procedure insert_tran_va_plan_detl(bigint, bigint, bigint, bigint, text, bigint, out bigint) to service_role;


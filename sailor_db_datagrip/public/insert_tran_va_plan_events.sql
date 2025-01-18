create procedure insert_tran_va_plan_events(IN p_tran_va_plan_id bigint, IN p_is_finalised boolean, IN p_tran_range_plan_event_id bigint, IN p_added_by bigint, OUT p_tran_va_plan_event_id bigint)
    language plpgsql
as
$$
BEGIN
    -- Insert into tran_va_plan_events table
    INSERT INTO tran_va_plan_events (
        tran_va_plan_id,
        is_finalised,
        tran_range_plan_event_id,
        added_by
      
    )
    VALUES (
        p_tran_va_plan_id,
        p_is_finalised,
        p_tran_range_plan_event_id,
        p_added_by
    )
	RETURNING tran_va_plan_event_id INTO p_tran_va_plan_event_id;
END;
$$;

alter procedure insert_tran_va_plan_events(bigint, boolean, bigint, bigint, out bigint) owner to postgres;

grant execute on procedure insert_tran_va_plan_events(bigint, boolean, bigint, bigint, out bigint) to anon;

grant execute on procedure insert_tran_va_plan_events(bigint, boolean, bigint, bigint, out bigint) to authenticated;

grant execute on procedure insert_tran_va_plan_events(bigint, boolean, bigint, bigint, out bigint) to service_role;


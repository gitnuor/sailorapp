create procedure insert_tran_range_plan(IN p_is_submitted boolean, IN p_is_approved boolean, IN p_tran_bp_year_id bigint, IN p_remarks text, IN p_added_by bigint, IN p_updated_by bigint, IN p_date_added timestamp without time zone, IN p_date_updated timestamp without time zone, IN p_approved_by bigint, IN p_approve_date timestamp without time zone, IN p_approve_remarks text, IN p_event_detail_list jsonb, IN p_det_list jsonb, OUT p_range_plan_id bigint)
    language plpgsql
as
$$
DECLARE
    event_detail_record JSONB;
    det_record JSONB;
BEGIN
    -- Insert into tran_range_plan table
    INSERT INTO tran_range_plan (
        is_submitted,
        is_approved,
        tran_bp_year_id,
        remarks,
        added_by,
        updated_by,
        date_added,
        date_updated,
        approved_by,
        approve_date,
        approve_remarks
    )
    VALUES (
        p_is_submitted,
        p_is_approved,
        p_tran_bp_year_id,
        p_remarks,
        p_added_by,
        p_updated_by,
        p_date_added,
        p_date_updated,
        p_approved_by,
        p_approve_date,
        p_approve_remarks
    )
    RETURNING range_plan_id INTO p_range_plan_id;

    -- Insert into tran_range_plan_events table
    FOR event_detail_record IN SELECT * FROM jsonb_array_elements(p_event_detail_list->'Event_Detail')
    LOOP
        INSERT INTO tran_range_plan_events (
            range_plan_id,
            tran_bp_event_id,
            total_mrp_value,
            total_cpu_value,
            total_range_plan_quantity,
            added_by,
            updated_by,
            date_added,
            date_updated,
            is_finalized
        )
        VALUES (
            p_range_plan_id,
            (event_detail_record->>'tran_bp_event_id')::BIGINT,
            (event_detail_record->>'total_mrp_value')::NUMERIC,
            (event_detail_record->>'total_cpu_value')::NUMERIC,
            (event_detail_record->>'total_range_plan_quantity')::BIGINT,
            (event_detail_record->>'added_by')::BIGINT,
            (event_detail_record->>'updated_by')::BIGINT,
            (event_detail_record->>'date_added')::TIMESTAMP,
            (event_detail_record->>'date_updated')::TIMESTAMP,
            (event_detail_record->>'is_finalized')::BOOLEAN
        );
    END LOOP;

    -- Insert into tran_range_plan_details table
    FOR det_record IN SELECT * FROM jsonb_array_elements(p_det_list->'DetList')
    LOOP
        INSERT INTO tran_range_plan_details (
            range_plan_id,
            tran_bp_event_id,
            style_item_product_id,
            range_plan_quantity,
            mrp_per_pc_value,
            mrp_value,
            cpu_per_pc_value,
            cpu_value,
            priority_id,
            prev_year_quantity,
            prev_year_efficiency,
            added_by,
            updated_by,
            date_added,
            date_updated
        )
        VALUES (
            p_range_plan_id,
            (det_record->>'tran_bp_event_id')::BIGINT,
            (det_record->>'style_item_product_id')::BIGINT,
            (det_record->>'range_plan_quantity')::BIGINT,
            (det_record->>'mrp_per_pc_value')::NUMERIC,
            (det_record->>'mrp_value')::NUMERIC,
            (det_record->>'cpu_per_pc_value')::NUMERIC,
            (det_record->>'cpu_value')::NUMERIC,
            (det_record->>'priority_id')::BIGINT,
            (det_record->>'prev_year_quantity')::BIGINT,
            (det_record->>'prev_year_efficiency')::NUMERIC,
            (det_record->>'added_by')::BIGINT,
            (det_record->>'updated_by')::BIGINT,
            (det_record->>'date_added')::TIMESTAMP,
            (det_record->>'date_updated')::TIMESTAMP
        );
    END LOOP;
END;
$$;

alter procedure insert_tran_range_plan(boolean, boolean, bigint, text, bigint, bigint, timestamp, timestamp, bigint, timestamp, text, jsonb, jsonb, out bigint) owner to postgres;

grant execute on procedure insert_tran_range_plan(boolean, boolean, bigint, text, bigint, bigint, timestamp, timestamp, bigint, timestamp, text, jsonb, jsonb, out bigint) to anon;

grant execute on procedure insert_tran_range_plan(boolean, boolean, bigint, text, bigint, bigint, timestamp, timestamp, bigint, timestamp, text, jsonb, jsonb, out bigint) to authenticated;

grant execute on procedure insert_tran_range_plan(boolean, boolean, bigint, text, bigint, bigint, timestamp, timestamp, bigint, timestamp, text, jsonb, jsonb, out bigint) to service_role;


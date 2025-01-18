create procedure tran_bp_insert(IN p_fiscal_year_id bigint, IN p_gross_sales numeric, IN p_added_by bigint, IN p_updated_by bigint, IN p_event_list_json text, IN p_remarks text DEFAULT NULL::text)
    language plpgsql
as
$$
DECLARE
    bp_new_id BIGINT;
    event_new_id BIGINT;
    month_new_id BIGINT;

    row_event RECORD;
    row_month RECORD;
BEGIN
    -- Attempt to insert data
    BEGIN
        INSERT INTO tran_bp_year (
            fiscal_year_id,
            gross_sales,
            added_by,
            remarks,
            event_list_json
        )
        VALUES (
            p_fiscal_year_id,
            p_gross_sales,
            p_added_by,
            p_remarks,
            p_event_list_json::json
        )
        RETURNING tran_bp_year_id INTO bp_new_id;

        FOR row_event IN (
            SELECT
                tranbp_event.event_id,
                tranbp_event.event_gross_sales,
                tranbp_event.readygoods_qnty,
                tranbp_event.readygoods_value,
                tranbp_event.added_by,
                tranbp_event.date_added,
				tranbp_event.event_month_list_json
            FROM
                LATERAL json_array_elements(p_event_list_json::json) AS t(doc),
                LATERAL json_populate_record(null::tran_bp_event, t.doc) AS tranbp_event
        )
        LOOP
            INSERT INTO tran_bp_event (
                tran_bp_year_id,
                event_id,
                event_gross_sales,
                readygoods_qnty,
                readygoods_value,
                added_by,
                date_added,
				event_month_list_json
            )
            VALUES (
                bp_new_id,
                row_event.event_id,
                row_event.event_gross_sales,
                row_event.readygoods_qnty,
                row_event.readygoods_value,
                row_event.added_by,
                row_event.date_added,
                row_event.event_month_list_json
				
            )
            RETURNING tran_bp_event_id INTO event_new_id;

            FOR row_month IN (
                SELECT
                    tranbpev_month.month_id,
                    tranbpev_month.monthly_gross_sales,
                     tranbpev_month.added_by,
                    tranbpev_month.date_added,
					tranbpev_month.event_month_outlet_list_json
                FROM
                    LATERAL json_array_elements(row_event.event_month_list_json) AS u(doc2),
                    LATERAL json_populate_record(null::public.tran_bp_event_month, u.doc2) AS tranbpev_month
            )
            LOOP
                INSERT INTO tran_bp_event_month (
                    tran_bp_event_id,
                    month_id,
                    monthly_gross_sales,
                    added_by,
                    date_added,
					event_month_outlet_list_json
                )
                VALUES (
                    event_new_id,
                    row_month.month_id,
                    row_month.monthly_gross_sales,
                    row_month.added_by,
                    row_month.date_added,
					row_month.event_month_outlet_list_json
                )
                RETURNING tran_bp_event_month_id INTO month_new_id;

                INSERT INTO tran_bp_event_month_outlet (
                    tran_bp_event_month_id,
                    outlet_id,
                    outlet_gross_sales,
                    added_by,
                    date_added
                )
                SELECT
                    month_new_id,
                    tranbpevmonth_outlet.outlet_id,
                    tranbpevmonth_outlet.outlet_gross_sales,
                      tranbpevmonth_outlet.added_by,
                    tranbpevmonth_outlet.date_added
                FROM
                    LATERAL json_array_elements(row_month.event_month_outlet_list_json) AS v(doc3),
                    LATERAL json_populate_record(null::public.tran_bp_event_month_outlet, v.doc3) AS tranbpevmonth_outlet;
            END LOOP;
        END LOOP;
    EXCEPTION
        WHEN OTHERS THEN
            -- Handle the exception by raising a custom error
            RAISE EXCEPTION 'Error inserting data into tran_bp_year: %', SQLERRM;
    END;
END;
$$;

alter procedure tran_bp_insert(bigint, numeric, bigint, bigint, text, text) owner to postgres;

grant execute on procedure tran_bp_insert(bigint, numeric, bigint, bigint, text, text) to anon;

grant execute on procedure tran_bp_insert(bigint, numeric, bigint, bigint, text, text) to authenticated;

grant execute on procedure tran_bp_insert(bigint, numeric, bigint, bigint, text, text) to service_role;


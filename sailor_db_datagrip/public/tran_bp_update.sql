create procedure tran_bp_update(IN p_tran_bp_year_id bigint, IN p_fiscal_year_id bigint, IN p_gross_sales numeric, IN p_is_approved boolean, IN p_is_submitted boolean, IN p_updated_by bigint, IN p_event_list_json text, IN p_remarks text DEFAULT NULL::text)
    language plpgsql
as
$$
DECLARE   

    row_event RECORD;
    row_month RECORD;
BEGIN
    -- Attempt to update data
    BEGIN
        update tran_bp_year 
            
           set 
		   is_approved=p_is_approved,
		   is_submitted=p_is_submitted,
		   gross_sales=p_gross_sales,
            updated_by=p_updated_by,
			date_updated=now(),
            remarks=p_remarks,
            event_list_json=p_event_list_json::json
        
        
        where tran_bp_year_id = p_tran_bp_year_id;

        FOR row_event IN (
            SELECT
                tranbp_event.tran_bp_event_id,
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
            UPDATE tran_bp_event 
                SET
                event_gross_sales=row_event.event_gross_sales,
                readygoods_qnty=row_event.readygoods_qnty,
                readygoods_value= row_event.readygoods_value,
                 updated_by=p_updated_by,
				date_updated=now(),
				event_month_list_json= row_event.event_month_list_json
          where tran_bp_event_id = row_event.tran_bp_event_id;

            FOR row_month IN (
                SELECT
                    tranbpev_month.month_id,
                    tranbpev_month.tran_bp_event_month_id,
                    tranbpev_month.monthly_gross_sales,
                     tranbpev_month.added_by,
                    tranbpev_month.date_added,
					tranbpev_month.event_month_outlet_list_json
                FROM
                    LATERAL json_array_elements(row_event.event_month_list_json) AS u(doc2),
                    LATERAL json_populate_record(null::public.tran_bp_event_month, u.doc2) AS tranbpev_month
            )
            LOOP
				UPDATE tran_bp_event_month 
				 set

					monthly_gross_sales= row_month.monthly_gross_sales,
					updated_by=p_updated_by,
					date_updated=now(),
					event_month_outlet_list_json=row_month.event_month_outlet_list_json
			   where  tran_bp_event_month_id = row_month.tran_bp_event_month_id;

			  UPDATE tran_bp_event_month_outlet mainoutlet
				SET
					outlet_gross_sales = tranbpevmonth_outlet.outlet_gross_sales,
					updated_by = p_updated_by,
					date_updated = NOW()
				FROM (
					SELECT *
					FROM LATERAL json_array_elements(row_month.event_month_outlet_list_json) AS v(doc3),
						 LATERAL json_populate_record(null::public.tran_bp_event_month_outlet, v.doc3)
				) AS tranbpevmonth_outlet
				WHERE
					tranbpevmonth_outlet.tran_bp_event_month_outlet_id = mainoutlet.tran_bp_event_month_outlet_id;
					
            END LOOP;
        END LOOP;
    EXCEPTION
        WHEN OTHERS THEN
            -- Handle the exception by raising a custom error
            RAISE EXCEPTION 'Error inserting data into tran_bp_year: %', SQLERRM;
    END;
END;
$$;

alter procedure tran_bp_update(bigint, bigint, numeric, boolean, boolean, bigint, text, text) owner to postgres;

grant execute on procedure tran_bp_update(bigint, bigint, numeric, boolean, boolean, bigint, text, text) to anon;

grant execute on procedure tran_bp_update(bigint, bigint, numeric, boolean, boolean, bigint, text, text) to authenticated;

grant execute on procedure tran_bp_update(bigint, bigint, numeric, boolean, boolean, bigint, text, text) to service_role;


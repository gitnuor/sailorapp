create procedure tran_range_update(IN p_range_plan_id bigint, IN p_tran_bp_event_id bigint, IN p_updated_by bigint, IN p_range_plan_details_list_update text, IN p_range_plan_details_list_insert text, IN p_range_plan_details_list text, IN p_total_mrp_value numeric, IN p_total_cpu_value numeric, IN p_is_plan_submitted boolean, IN p_is_event_finalized boolean, IN p_total_range_plan_quantity bigint, IN p_remarks text DEFAULT NULL::text, IN p_tran_range_plan_event_id bigint DEFAULT NULL::bigint)
    language plpgsql
as
$$
DECLARE
    new_range_id BIGINT;
    new_detail_id BIGINT;

    update_row_plan_detail RECORD;
    insert_row_plan_detail RECORD;
    update_row_size RECORD;
    insert_row_size RECORD;
BEGIN
    -- Attempt to update data in ****** tran_range_plan
    BEGIN
		update  tran_range_plan
		set	remarks=p_remarks,
			updated_by=p_updated_by,
			date_updated=now(),
			is_submitted=p_is_plan_submitted,
			range_plan_details_list_json=p_range_plan_details_list::json
			where range_plan_id = p_range_plan_id;
 		-- Attempt to update data in ****** tran_range_plan_events

		if p_tran_range_plan_event_id is not null then
			update  tran_range_plan_events
			set total_mrp_value=p_total_mrp_value,
			total_cpu_value=p_total_cpu_value,
			total_range_plan_quantity=p_total_range_plan_quantity,
			is_finalized=p_is_event_finalized,
			updated_by=p_updated_by,
			date_updated=now()
			where tran_range_plan_event_id = p_tran_range_plan_event_id;
		else
			 INSERT INTO public.tran_range_plan_events(
			range_plan_id,	tran_bp_event_id,	total_mrp_value,
			total_cpu_value,total_range_plan_quantity,is_finalized,added_by,date_added
			)
	 		VALUES(
			p_range_plan_id,p_tran_bp_event_id,p_total_mrp_value
			,p_total_cpu_value,p_total_range_plan_quantity,p_is_event_finalized
			,p_updated_by,now()
			);
		end if;


	 -- Attempt to update data in ****** tran_range_plan_details
     FOR update_row_plan_detail IN (
            SELECT
                range_plan_details.range_plan_detail_id,
                range_plan_details.range_plan_quantity,
                range_plan_details.mrp_per_pc_value,
                range_plan_details.mrp_value,
                range_plan_details.cpu_per_pc_value,
				range_plan_details.cpu_value,
				range_plan_details.priority_id,
				range_plan_details.prev_year_quantity,
				range_plan_details.prev_year_efficiency,
				range_plan_details.size_list
            FROM
                LATERAL json_array_elements(p_range_plan_details_list::json) AS t(doc),
                LATERAL json_populate_record(null::tran_range_plan_details, t.doc) AS range_plan_details
        )
		LOOP
           update tran_range_plan_details
			set
				range_plan_quantity=update_row_plan_detail.range_plan_quantity,
				mrp_per_pc_value=update_row_plan_detail.mrp_per_pc_value,
				mrp_value=update_row_plan_detail.mrp_value,
				cpu_per_pc_value=update_row_plan_detail.cpu_per_pc_value,
				cpu_value=update_row_plan_detail.cpu_value,
				priority_id=update_row_plan_detail.priority_id,
				prev_year_quantity=update_row_plan_detail.prev_year_quantity,
				prev_year_efficiency=update_row_plan_detail.prev_year_efficiency,
				updated_by=p_updated_by,
				date_updated=now(),
				size_list=update_row_plan_detail.size_list
            where range_plan_detail_id = update_row_plan_detail.range_plan_detail_id;
				-- Attempt to update data in ****** tran_range_plan_details_size
			FOR update_row_size IN (
                SELECT
					 range_plan_details_size.range_plan_detail_size_id
					,range_plan_details_size.style_product_size_group_detid
					,range_plan_details_size.size_quantity
					,range_plan_details_size.size_per_pc_mrp_value
					,range_plan_details_size.size_per_pc_cpu_value
                FROM
                    LATERAL json_array_elements(update_row_plan_detail.size_list) AS u(doc2),
                    LATERAL json_populate_record(null::public.tran_range_plan_details_size, u.doc2) AS range_plan_details_size
            )
            LOOP
              update tran_range_plan_details_size
			  set
					style_product_size_group_detid=update_row_size.style_product_size_group_detid,
					size_quantity=update_row_size.size_quantity,
					size_per_pc_mrp_value=update_row_size.size_per_pc_mrp_value,
					updated_by=p_updated_by,
					date_updated=now(),
					size_per_pc_cpu_value=update_row_size.size_per_pc_cpu_value
				 where range_plan_detail_size_id = update_row_size.range_plan_detail_size_id;
			 END LOOP;
        END LOOP;

		-- Attempt to insert data in ****** tran_range_plan_details
           FOR insert_row_plan_detail IN (
            SELECT
                range_plan_details.tran_bp_event_id,
                range_plan_details.style_item_product_id,
                range_plan_details.range_plan_quantity,
                range_plan_details.mrp_per_pc_value,
                range_plan_details.mrp_value,
                range_plan_details.cpu_per_pc_value,
				range_plan_details.cpu_value,
				range_plan_details.priority_id,
				range_plan_details.prev_year_quantity,
				range_plan_details.prev_year_efficiency,
				range_plan_details.size_list
            FROM
                LATERAL json_array_elements(p_range_plan_details_list_insert::json) AS t(doc),
                LATERAL json_populate_record(null::tran_range_plan_details, t.doc) AS range_plan_details
        )
		LOOP
            INSERT INTO public.tran_range_plan_details (
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
				date_added,
				size_list
			)
			VALUES (
				 p_range_plan_id
				,insert_row_plan_detail.tran_bp_event_id
				,insert_row_plan_detail.style_item_product_id
				,insert_row_plan_detail.range_plan_quantity
				,insert_row_plan_detail.mrp_per_pc_value
				,insert_row_plan_detail.mrp_value
				,insert_row_plan_detail.cpu_per_pc_value
				,insert_row_plan_detail.cpu_value
				,insert_row_plan_detail.priority_id
				,insert_row_plan_detail.prev_year_quantity
				,insert_row_plan_detail.prev_year_efficiency
				,p_updated_by
				,now()
				,insert_row_plan_detail.size_list)
            RETURNING range_plan_detail_id INTO new_detail_id;

			FOR insert_row_size IN (
                SELECT
					 range_plan_details_size.style_product_size_group_detid
					,range_plan_details_size.size_quantity
					,range_plan_details_size.size_per_pc_mrp_value
					,range_plan_details_size.added_by
					,now()
					,range_plan_details_size.size_per_pc_cpu_value
                FROM
                    LATERAL json_array_elements(insert_row_plan_detail.size_list) AS u(doc2),
                    LATERAL json_populate_record(null::public.tran_range_plan_details_size, u.doc2) AS range_plan_details_size
            )
            LOOP
              INSERT INTO public.tran_range_plan_details_size (
					range_plan_detail_id,
					style_product_size_group_detid,
					size_quantity,
					size_per_pc_mrp_value,
					added_by,
					date_added,
					size_per_pc_cpu_value
				)
				VALUES (
					new_detail_id
					,insert_row_size.style_product_size_group_detid
					,insert_row_size.size_quantity
					,insert_row_size.size_per_pc_mrp_value
					,p_updated_by
					,now()
					,insert_row_size.size_per_pc_cpu_value
				);
			 END LOOP;
        END LOOP;
EXCEPTION
        WHEN OTHERS THEN
            -- Handle the exception by raising a custom error
            RAISE EXCEPTION 'Error inserting data : %', SQLERRM;
    END;
END;
$$;

alter procedure tran_range_update(bigint, bigint, bigint, text, text, text, numeric, numeric, boolean, boolean, bigint, text, bigint) owner to postgres;

grant execute on procedure tran_range_update(bigint, bigint, bigint, text, text, text, numeric, numeric, boolean, boolean, bigint, text, bigint) to anon;

grant execute on procedure tran_range_update(bigint, bigint, bigint, text, text, text, numeric, numeric, boolean, boolean, bigint, text, bigint) to authenticated;

grant execute on procedure tran_range_update(bigint, bigint, bigint, text, text, text, numeric, numeric, boolean, boolean, bigint, text, bigint) to service_role;


create procedure tran_range_insert(IN p_tran_bp_year_id bigint, IN p_added_by bigint, IN p_range_plan_details_list_json text, IN p_tran_bp_event_id bigint, IN p_total_mrp_value numeric, IN p_total_cpu_value numeric, IN p_total_range_plan_quantity bigint, IN p_remarks text DEFAULT NULL::text)
    language plpgsql
as
$$
DECLARE
    new_range_id BIGINT;
    new_detail_id BIGINT;
    month_new_id BIGINT;

    row_plan_detail RECORD;
    row_size RECORD;
BEGIN
    -- Attempt to insert data
    BEGIN
		INSERT INTO public.tran_range_plan (		
		tran_bp_year_id,
		remarks,
		added_by,	
		date_added,		
		is_submitted,
		is_approved,
		range_plan_details_list_json
		)
	VALUES (	
		p_tran_bp_year_id
		,p_remarks
		,p_added_by		
		,now()		
		,false
		,false
		,p_range_plan_details_list_json::json
	)RETURNING range_plan_id INTO new_range_id;

     INSERT INTO public.tran_range_plan_events(		
			range_plan_id,
			tran_bp_event_id,
			total_mrp_value,
			total_cpu_value,
			total_range_plan_quantity,
			is_finalized,
			added_by,	
			date_added
		)
	 VALUES(			
			new_range_id
			,p_tran_bp_event_id
			,p_total_mrp_value
			,p_total_cpu_value
			,p_total_range_plan_quantity
			,false
			,p_added_by		
			,now()		
			); 
 
     FOR row_plan_detail IN (
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
                LATERAL json_array_elements(p_range_plan_details_list_json::json) AS t(doc),
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
				 new_range_id
				,row_plan_detail.tran_bp_event_id
				,row_plan_detail.style_item_product_id
				,row_plan_detail.range_plan_quantity
				,row_plan_detail.mrp_per_pc_value
				,row_plan_detail.mrp_value
				,row_plan_detail.cpu_per_pc_value
				,row_plan_detail.cpu_value
				,row_plan_detail.priority_id
				,row_plan_detail.prev_year_quantity
				,row_plan_detail.prev_year_efficiency
				,p_added_by
				,now()
				,row_plan_detail.size_list)
            RETURNING range_plan_detail_id INTO new_detail_id;
				
			FOR row_size IN (
                SELECT
					 range_plan_details_size.style_product_size_group_detid
					,range_plan_details_size.size_quantity
					,range_plan_details_size.size_per_pc_mrp_value
					,range_plan_details_size.added_by
					,now()
					,range_plan_details_size.size_per_pc_cpu_value
                FROM
                    LATERAL json_array_elements(row_plan_detail.size_list) AS u(doc2),
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
					,row_size.style_product_size_group_detid
					,row_size.size_quantity
					,row_size.size_per_pc_mrp_value
					,p_added_by
					,now()
					,row_size.size_per_pc_cpu_value
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

alter procedure tran_range_insert(bigint, bigint, text, bigint, numeric, numeric, bigint, text) owner to postgres;

grant execute on procedure tran_range_insert(bigint, bigint, text, bigint, numeric, numeric, bigint, text) to anon;

grant execute on procedure tran_range_insert(bigint, bigint, text, bigint, numeric, numeric, bigint, text) to authenticated;

grant execute on procedure tran_range_insert(bigint, bigint, text, bigint, numeric, numeric, bigint, text) to service_role;


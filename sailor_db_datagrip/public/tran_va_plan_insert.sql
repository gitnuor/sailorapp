create procedure tran_va_plan_insert(IN p_tran_range_plan_id bigint, IN p_is_submitted boolean, IN p_added_by bigint, OUT p_tran_va_plan_id bigint, IN p_remarks text DEFAULT NULL::text, IN p_plan_event_details text DEFAULT NULL::text)
    language plpgsql
as
$$
DECLARE
    tv_plan_new_id BIGINT;
    va_event_new_id BIGINT;
    va_detl_new_id BIGINT;
    va_detl_style_new_id BIGINT;
    va_detl_style_color_new_id BIGINT;

    row_event RECORD;
    row_va_detl RECORD;
    row_va_detl_style RECORD;
    row_va_detl_style_color RECORD;


BEGIN
    -- Insert into tran_va_plan table
    INSERT INTO tran_va_plan (
        tran_range_plan_id,
        remarks,
        is_submitted,
        added_by,date_added, plan_event_details
    )
    VALUES (
        p_tran_range_plan_id,
        p_remarks,
        p_is_submitted,

        p_added_by,now(), p_plan_event_details::json
    )

    RETURNING tran_va_plan_id INTO tv_plan_new_id;

         FOR row_event IN (
            SELECT
                tv_plan_new_id,
                tran_va_event.tran_range_plan_event_id,
                tran_va_event.is_finalised,
                tran_va_event.added_by,
                tran_va_event.date_added,
				tran_va_event.plan_detl_details
            FROM
                LATERAL json_array_elements(p_plan_event_details::json) AS t(doc),
                LATERAL json_populate_record(null::tran_va_plan_events, t.doc) AS tran_va_event
        )
        LOOP
            INSERT INTO tran_va_plan_events (
                 tran_va_plan_id
                ,tran_range_plan_event_id
                ,is_finalised
                ,added_by,
                date_added,
				plan_detl_details
            )
            VALUES (

                row_event.tv_plan_new_id,
                row_event.tran_range_plan_event_id,
                row_event.is_finalised,
                p_added_by,now(),
                row_event.plan_detl_details::json
            )
            RETURNING tran_va_plan_event_id INTO va_event_new_id;

				 FOR row_va_detl IN (SELECT  va_event_new_id,
                                             tran_va_detl.range_plan_detail_id,
                                             tran_va_detl.style_item_product_id,
                                             tran_va_detl.no_of_style,
                                              tran_va_detl.style_code_gen,
                                             tran_va_detl.style_details
                                  FROM LATERAL json_array_elements(row_event.plan_detl_details) AS u(doc2),
                                       LATERAL json_populate_record(null::tran_va_plan_detl, u.doc2) AS tran_va_detl)
                    LOOP
                        INSERT INTO tran_va_plan_detl (tran_va_plan_event_id,
                                                         range_plan_detail_id,
                                                         style_item_product_id,no_of_style,style_code_gen,
                                                         added_by,
                                                         date_added,
                                                         style_details)
                        VALUES (row_va_detl.va_event_new_id,
                                row_va_detl.range_plan_detail_id,
                                row_va_detl.style_item_product_id,row_va_detl.no_of_style,row_va_detl.style_code_gen,
                                 p_added_by,now(),
                                row_va_detl.style_details)

                        RETURNING tran_va_plan_detl_id INTO va_detl_new_id;

                         FOR row_va_detl_style IN (SELECT va_detl_new_id,
                                         tran_va_detl_style.style_code,tran_va_detl_style.style_quantity,
                                         tran_va_detl_style.no_of_color,tran_va_detl_style.color_code_gen,
                                         tran_va_detl_style.style_embellishment_ids,
                                         tran_va_detl_style.style_item_product_sub_category_id,
                                         tran_va_detl_style.added_by,
                                         tran_va_detl_style.date_added,
                                         tran_va_detl_style.style_color_details
                                  FROM LATERAL json_array_elements(row_va_detl.style_details) AS u(doc3),
                                       LATERAL json_populate_record(null::tran_va_plan_detl_style, u.doc3) AS tran_va_detl_style)
                         LOOP
                                INSERT INTO tran_va_plan_detl_style (tran_va_plan_detl_id,style_code,style_quantity,
                                                                 no_of_color,color_code_gen,style_embellishment_ids,
                                                                 style_item_product_sub_category_id,
                                                                 added_by,
                                                                 date_added,
                                                                 style_color_details)
                                VALUES (row_va_detl_style.va_detl_new_id,
                                        row_va_detl_style.style_code,
                                        row_va_detl_style.style_quantity,
                                         row_va_detl_style.no_of_color,
                                        row_va_detl_style.color_code_gen,
                                         row_va_detl_style.style_embellishment_ids,
                                        row_va_detl_style.style_item_product_sub_category_id,
                                         p_added_by,now(),
                                        row_va_detl_style.style_color_details)
                                RETURNING tran_va_plan_detl_style_id INTO va_detl_style_new_id;

                                     FOR row_va_detl_style_color IN (SELECT va_detl_style_new_id,
                                                 tranva_detl_style_color.color_code,
                                                 tranva_detl_style_color.style_color_quantity,
                                                 tranva_detl_style_color.added_by,
                                                 tranva_detl_style_color.date_added,
                                                 tranva_detl_style_color.style_color_size_details
                                          FROM LATERAL json_array_elements(row_va_detl_style.style_color_details) AS u(doc4),
                                               LATERAL json_populate_record(null::tran_va_plan_detl_style_color , u.doc4) AS tranva_detl_style_color)
                                    LOOP
                                        INSERT INTO tran_va_plan_detl_style_color (tran_va_plan_detl_style_id,
                                                                         color_code,
                                                                         style_color_quantity,
                                                                         added_by,
                                                                         date_added,
                                                                         style_color_size_details)
                                        VALUES (row_va_detl_style_color.va_detl_style_new_id,
                                                row_va_detl_style_color.color_code,
                                                row_va_detl_style_color.style_color_quantity,
                                                    p_added_by,now(),
                                                row_va_detl_style_color.style_color_size_details)

                                        RETURNING tran_va_plan_detl_style_color_id INTO va_detl_style_color_new_id;
                                        
                                        INSERT INTO  tran_va_plan_detl_style_color_size(tran_va_plan_detl_style_color_id,
                                                                style_product_size_group_detid,
                                                                style_color_size_quantity,
                                                                added_by,
                                                                date_added)
                                        SELECT va_detl_style_color_new_id,
                                               tranva_detlstyle_colorsize.style_product_size_group_detid,
                                               tranva_detlstyle_colorsize.style_color_size_quantity,
                                               p_added_by,now()
                                            FROM LATERAL json_array_elements(row_va_detl_style_color.style_color_size_details) AS v(doc5),
                                             LATERAL json_populate_record(null::tran_va_plan_detl_style_color_size,
                                                                          v.doc5) AS tranva_detlstyle_colorsize;    

                                    END LOOP;
                        END LOOP;
                 END LOOP;
        END LOOP;
END;
$$;

alter procedure tran_va_plan_insert(bigint, boolean, bigint, out bigint, text, text) owner to postgres;

grant execute on procedure tran_va_plan_insert(bigint, boolean, bigint, out bigint, text, text) to anon;

grant execute on procedure tran_va_plan_insert(bigint, boolean, bigint, out bigint, text, text) to authenticated;

grant execute on procedure tran_va_plan_insert(bigint, boolean, bigint, out bigint, text, text) to service_role;


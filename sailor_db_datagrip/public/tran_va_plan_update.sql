create procedure tran_va_plan_update(IN p_tran_va_plan_id bigint, IN p_tran_range_plan_id bigint, IN p_is_submitted boolean, IN p_added_by bigint, OUT p_tran_va_plan_id_out bigint, IN p_remarks text DEFAULT NULL::text, IN p_plan_event_details text DEFAULT NULL::text)
    language plpgsql
as
$$
DECLARE
    tv_plan_new_id             BIGINT;
    va_event_new_id            BIGINT;
    va_detl_new_id             BIGINT;
    va_detl_style_new_id       BIGINT;
    va_detl_style_color_new_id BIGINT;
    row_event                  RECORD;
    row_va_detl                RECORD;
    row_va_detl_style          RECORD;
    row_va_detl_style_color    RECORD;
    row_va_detl_style_color_size    RECORD;


BEGIN
    p_tran_va_plan_id_OUT:=p_tran_va_plan_id;

    FOR row_event IN (SELECT tran_va_event.tran_va_plan_event_id,
                             tran_va_event.tran_range_plan_event_id,
                             tran_va_event.is_finalised,
                             tran_va_event.added_by,
                             tran_va_event.date_added,
                             tran_va_event.plan_detl_details
                      FROM LATERAL json_array_elements(p_plan_event_details::json) AS t(doc),
                           LATERAL json_populate_record(null::tran_va_plan_events, t.doc) AS tran_va_event)
        LOOP

            if row_event.tran_va_plan_event_id is not null then

                UPDATE tran_va_plan_events
                SET is_finalised=row_event.is_finalised,plan_detl_details=row_event.plan_detl_details,
                    date_updated=now(),
                    updated_by=p_added_by
                WHERE tran_va_plan_event_id=row_event.tran_va_plan_event_id;

                FOR row_va_detl IN (SELECT tran_va_detl.tran_va_plan_detl_id,
                                           tran_va_detl.tran_va_plan_event_id,
                                           tran_va_detl.range_plan_detail_id,
                                           tran_va_detl.style_item_product_id,
                                           tran_va_detl.no_of_style,
                                           tran_va_detl.style_code_gen,
                                           tran_va_detl.style_details
                                    FROM LATERAL json_array_elements(row_event.plan_detl_details) AS u(doc2),
                                         LATERAL json_populate_record(null::tran_va_plan_detl, u.doc2) AS tran_va_detl)
                    LOOP

                            IF row_va_detl.tran_va_plan_detl_id is not null then

                                UPDATE tran_va_plan_detl
                                    SET no_of_style=row_va_detl.no_of_style,
                                        style_code_gen=row_va_detl.style_code_gen,style_details=row_va_detl.style_details,
                                        updated_by=p_added_by,
                                        date_updated=now()
                                        where tran_va_plan_detl_id=row_va_detl.tran_va_plan_detl_id;

                                FOR row_va_detl_style IN (SELECT
                                                                 tran_va_detl_style.tran_va_plan_detl_style_id,
                                                                 row_va_detl.tran_va_plan_detl_id,
                                                                 tran_va_detl_style.style_code,
                                                                 tran_va_detl_style.style_quantity,
                                                                 tran_va_detl_style.no_of_color,
                                                                 tran_va_detl_style.color_code_gen,
                                                                 tran_va_detl_style.style_embellishment_ids,
                                                                 tran_va_detl_style.style_item_product_sub_category_id,
                                                                 tran_va_detl_style.added_by,
                                                                 tran_va_detl_style.date_added,
                                                                 tran_va_detl_style.style_color_details,
                                                                tran_va_detl_style.is_delete
                                                          FROM LATERAL json_array_elements(row_va_detl.style_details) AS u(doc3),
                                                               LATERAL json_populate_record(null::tran_va_plan_detl_style, u.doc3) AS tran_va_detl_style)
                                    LOOP

                                        if row_va_detl_style.tran_va_plan_detl_style_id is null then
                                            --insert ALL tran_va_plan_detl_style, tran_va_plan_detl_style_color, tran_va_plan_detl_style_color_size
                                            INSERT INTO tran_va_plan_detl_style (tran_va_plan_detl_id, style_code, style_quantity,
                                                                                 no_of_color, color_code_gen, style_embellishment_ids,
                                                                                 style_item_product_sub_category_id,
                                                                                 added_by,
                                                                                 date_added,
                                                                                 style_color_details)

                                            VALUES (row_va_detl.tran_va_plan_detl_id,
                                                    row_va_detl_style.style_code,
                                                    row_va_detl_style.style_quantity,
                                                    row_va_detl_style.no_of_color,
                                                    row_va_detl_style.color_code_gen,
                                                    row_va_detl_style.style_embellishment_ids::json,
                                                    row_va_detl_style.style_item_product_sub_category_id,
                                                    p_added_by, now(),
                                                    row_va_detl_style.style_color_details::json)

                                            RETURNING tran_va_plan_detl_style_id INTO va_detl_style_new_id;

                                                 FOR row_va_detl_style_color IN (SELECT va_detl_style_new_id,
                                                                                   tranva_detl_style_color.color_code,
                                                                                   tranva_detl_style_color.style_color_quantity,
                                                                                   tranva_detl_style_color.added_by,
                                                                                   tranva_detl_style_color.date_added,
                                                                                   tranva_detl_style_color.style_color_size_details
                                                                            FROM LATERAL json_array_elements(row_va_detl_style.style_color_details) AS u(doc4),
                                                                                 LATERAL json_populate_record(null::tran_va_plan_detl_style_color, u.doc4) AS tranva_detl_style_color)
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
                                                            p_added_by, now(),
                                                            row_va_detl_style_color.style_color_size_details::json)

                                                    RETURNING tran_va_plan_detl_style_color_id INTO va_detl_style_color_new_id;

                                                    INSERT INTO tran_va_plan_detl_style_color_size(tran_va_plan_detl_style_color_id,
                                                                                                   style_product_size_group_detid,
                                                                                                   style_color_size_quantity,
                                                                                                   added_by,
                                                                                                   date_added)
                                                    SELECT va_detl_style_color_new_id,
                                                           tranva_detlstyle_colorsize.style_product_size_group_detid,
                                                           tranva_detlstyle_colorsize.style_color_size_quantity,
                                                           p_added_by,
                                                           now()
                                                    FROM LATERAL json_array_elements(row_va_detl_style_color.style_color_size_details) AS v(doc5),
                                                         LATERAL json_populate_record(null::tran_va_plan_detl_style_color_size,
                                                                                      v.doc5) AS tranva_detlstyle_colorsize;

                                                END LOOP;

                                        ELSIF row_va_detl_style.tran_va_plan_detl_style_id is not null and row_va_detl_style.is_delete=0 THEN
                                            --update tran_va_plan_detl_style,
                                                -- INSERT tran_va_plan_detl_style_color
                                                    -- INSERT tran_va_plan_detl_style_color_SIZE
                                                -- UPDATE tran_va_plan_detl_style_color
                                                    -- INSERT tran_va_plan_detl_style_color_SIZE
                                                    -- UPDATE tran_va_plan_detl_style_color_SIZE

                                            UPDATE tran_va_plan_detl_style SET
                                             style_code= row_va_detl_style.style_code,
                                             style_quantity= row_va_detl_style.style_quantity,
                                             no_of_color= row_va_detl_style.no_of_color,
                                             color_code_gen= row_va_detl_style.color_code_gen,
                                             style_embellishment_ids= row_va_detl_style.style_embellishment_ids::json,
                                             style_item_product_sub_category_id= row_va_detl_style.style_item_product_sub_category_id,
                                             style_color_details= row_va_detl_style.style_color_details,
                                             updated_by= p_added_by,
                                             date_updated= now()

                                            WHERE tran_va_plan_detl_style_id=row_va_detl_style.tran_va_plan_detl_style_id;

                                            FOR row_va_detl_style_color IN (SELECT tranva_detl_style_color.tran_va_plan_detl_style_color_id,
                                                                                  tranva_detl_style_color.tran_va_plan_detl_style_id,
                                                                                   tranva_detl_style_color.color_code,
                                                                                   tranva_detl_style_color.style_color_quantity,
                                                                                   tranva_detl_style_color.added_by,
                                                                                   tranva_detl_style_color.date_added,
                                                                                   tranva_detl_style_color.style_color_size_details
                                                                            FROM LATERAL json_array_elements(row_va_detl_style.style_color_details) AS u(doc4),
                                                                                 LATERAL json_populate_record(null::tran_va_plan_detl_style_color, u.doc4) AS tranva_detl_style_color)
                                                LOOP

                                                    if row_va_detl_style_color.tran_va_plan_detl_style_color_id is null then --insert color and size

                                                            INSERT INTO tran_va_plan_detl_style_color (tran_va_plan_detl_style_id,
                                                                                               color_code,
                                                                                               style_color_quantity,
                                                                                               added_by,
                                                                                               date_added,
                                                                                               style_color_size_details)
                                                            VALUES (row_va_detl_style_color.tran_va_plan_detl_style_id,
                                                                    row_va_detl_style_color.color_code,
                                                                    row_va_detl_style_color.style_color_quantity,
                                                                    p_added_by, now(),
                                                                    row_va_detl_style_color.style_color_size_details::json)

                                                            RETURNING tran_va_plan_detl_style_color_id INTO va_detl_style_color_new_id;

                                                            INSERT INTO tran_va_plan_detl_style_color_size(tran_va_plan_detl_style_color_id,
                                                                                                           style_product_size_group_detid,
                                                                                                           style_color_size_quantity,
                                                                                                           added_by,
                                                                                                           date_added)
                                                            SELECT va_detl_style_color_new_id,
                                                                   tranva_detlstyle_colorsize.style_product_size_group_detid,
                                                                   tranva_detlstyle_colorsize.style_color_size_quantity,
                                                                   p_added_by,
                                                                   now()
                                                            FROM LATERAL json_array_elements(row_va_detl_style_color.style_color_size_details) AS v(doc5),
                                                                 LATERAL json_populate_record(null::tran_va_plan_detl_style_color_size,
                                                                                              v.doc5) AS tranva_detlstyle_colorsize;
                                                    else --update color
                                                            update tran_va_plan_detl_style_color set
                                                                color_code=row_va_detl_style_color.color_code,
                                                                style_color_quantity=row_va_detl_style_color.style_color_quantity,
                                                                style_color_size_details=row_va_detl_style_color.style_color_size_details::json
                                                            where  tran_va_plan_detl_style_color_id=row_va_detl_style_color.tran_va_plan_detl_style_color_id

                                                            RETURNING tran_va_plan_detl_style_id INTO va_detl_style_new_id;

                                                                 FOR row_va_detl_style_color_size IN (SELECT tranva_detl_style_colorsize.tran_va_plan_detl_style_color_size_id,
                                                                                                   tranva_detl_style_colorsize.tran_va_plan_detl_style_color_id,
                                                                                                   tranva_detl_style_colorsize.style_product_size_group_detid,
                                                                                                   tranva_detl_style_colorsize.style_color_size_quantity

                                                                                            FROM LATERAL json_array_elements(row_va_detl_style_color.style_color_size_details) AS u(doc6),
                                                                                                 LATERAL json_populate_record(null::tran_va_plan_detl_style_color_size, u.doc6) AS tranva_detl_style_colorsize)
                                                                    LOOP
                                                                        IF row_va_detl_style_color_size.tran_va_plan_detl_style_color_size_id IS NULL THEN

                                                                            INSERT INTO tran_va_plan_detl_style_color_size(tran_va_plan_detl_style_color_id,
                                                                                                           style_product_size_group_detid,
                                                                                                           style_color_size_quantity,
                                                                                                           added_by,
                                                                                                           date_added)
                                                                            SELECT row_va_detl_style_color.tran_va_plan_detl_style_color_id,
                                                                                       tranva_detlstyle_colorsize.style_product_size_group_detid,
                                                                                       tranva_detlstyle_colorsize.style_color_size_quantity,
                                                                                       p_added_by, now()
                                                                                FROM LATERAL json_array_elements(row_va_detl_style_color.style_color_size_details) AS v(doc7),
                                                                                     LATERAL json_populate_record(null::tran_va_plan_detl_style_color_size,
                                                                                                                  v.doc7) AS tranva_detlstyle_colorsize;
                                                                        ELSE
                                                                            UPDATE tran_va_plan_detl_style_color_size SET
                                                                                   style_product_size_group_detid=row_va_detl_style_color_size.style_product_size_group_detid,
                                                                                    style_color_size_quantity=row_va_detl_style_color_size.style_color_size_quantity,
                                                                                    updated_by= p_added_by,
                                                                                    date_updated=NOW()
                                                                            WHERE tran_va_plan_detl_style_color_size_id=row_va_detl_style_color_size.tran_va_plan_detl_style_color_size_id;

                                                                        END IF;
                                                                    END LOOP;

                                                    end if;


                                                END LOOP;

                                        ELSIF row_va_detl_style.tran_va_plan_detl_style_id is not null and row_va_detl_style.is_delete=1 THEN --delete

                                            DELETE FROM tran_va_plan_detl_style WHERE tran_va_plan_detl_style_id=row_va_detl_style.tran_va_plan_detl_style_id;

                                        end if;


                                    END LOOP;

                            ELSE
                            ---- new va_plan_detl
                                  INSERT INTO tran_va_plan_detl (tran_va_plan_event_id,
                                                             range_plan_detail_id,
                                                             style_item_product_id,no_of_style,style_code_gen,
                                                             added_by,
                                                             date_added,
                                                             style_details)
                            VALUES (row_va_detl.tran_va_plan_event_id,
                                    row_va_detl.range_plan_detail_id,
                                    row_va_detl.style_item_product_id,row_va_detl.no_of_style,row_va_detl.style_code_gen,
                                     p_added_by,now(),
                                    row_va_detl.style_details::json)

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
                                                     row_va_detl_style.style_embellishment_ids::json,
                                                    row_va_detl_style.style_item_product_sub_category_id,
                                                     p_added_by,now(),
                                                    row_va_detl_style.style_color_details::json)
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
                                                            row_va_detl_style_color.style_color_size_details::json)

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

                                ---end new va_plan_detl
                            ----
                            END IF;
                    END LOOP;
            else
                     INSERT INTO tran_va_plan_events (
                     tran_va_plan_id
                    ,tran_range_plan_event_id
                    ,is_finalised
                    ,added_by,
                    date_added,
                    plan_detl_details
                )
                VALUES (

                    p_tran_va_plan_id,
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
                                    row_va_detl.style_details::json)

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
                                             row_va_detl_style.style_embellishment_ids::json,
                                            row_va_detl_style.style_item_product_sub_category_id,
                                             p_added_by,now(),
                                            row_va_detl_style.style_color_details::json)
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
                                                    row_va_detl_style_color.style_color_size_details::json)

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
            end if;
        END LOOP;
END;
$$;

alter procedure tran_va_plan_update(bigint, bigint, boolean, bigint, out bigint, text, text) owner to postgres;

grant execute on procedure tran_va_plan_update(bigint, bigint, boolean, bigint, out bigint, text, text) to anon;

grant execute on procedure tran_va_plan_update(bigint, bigint, boolean, bigint, out bigint, text, text) to authenticated;

grant execute on procedure tran_va_plan_update(bigint, bigint, boolean, bigint, out bigint, text, text) to service_role;


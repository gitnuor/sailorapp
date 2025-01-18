create view vw_product_value_add_plan
            (style_item_product_name, style_item_type_name, style_product_type_name, style_item_origin_name,
             style_gender_name, master_category_name, style_item_product_id, range_plan_detail_id, tran_range_plan_id,
             remarks, is_submitted, is_approved, approved_by, approve_date, approve_remarks, tran_va_plan_detl_id,
             tran_va_plan_event_id, no_of_style, style_code_gen)
as
SELECT stp.style_item_product_name,
       sit.style_item_type_name,
       spt.style_product_type_name,
       sio.style_item_origin_name,
       sg.style_gender_name,
       smc.master_category_name,
       trpd.style_item_product_id,
       trpd.range_plan_detail_id,
       tvp.tran_range_plan_id,
       tvp.remarks,
       tvp.is_submitted,
       tvp.is_approved,
       tvp.approved_by,
       tvp.approve_date,
       tvp.approve_remarks,
       tvpd.tran_va_plan_detl_id,
       tvpd.tran_va_plan_event_id,
       tvpd.no_of_style,
       tvpd.style_code_gen
FROM tran_range_plan_details trpd
         LEFT JOIN tran_va_plan_detl tvpd ON tvpd.range_plan_detail_id = trpd.range_plan_detail_id
         LEFT JOIN tran_va_plan tvp ON tvp.tran_va_plan_id = tvpd.tran_va_plan_event_id
         LEFT JOIN style_item_product stp ON stp.style_item_product_id = trpd.style_item_product_id
         LEFT JOIN style_item_type sit ON sit.style_item_type_id = stp.style_item_type_id
         LEFT JOIN style_product_type spt ON spt.style_product_type_id = stp.style_product_type_id
         LEFT JOIN style_item_origin sio ON sio.style_item_origin_id = stp.style_item_origin_id
         LEFT JOIN style_gender sg ON sg.style_gender_id = stp.style_gender_id
         LEFT JOIN style_master_category smc ON smc.style_master_category_id = stp.style_master_category_id;

alter table vw_product_value_add_plan
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on vw_product_value_add_plan to anon;

grant delete, insert, references, select, trigger, truncate, update on vw_product_value_add_plan to authenticated;

grant delete, insert, references, select, trigger, truncate, update on vw_product_value_add_plan to service_role;


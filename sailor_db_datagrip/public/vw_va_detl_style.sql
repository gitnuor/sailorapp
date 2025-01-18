create view vw_va_detl_style
            (style_product_size, tran_va_plan_detl_style_color_size_id, tran_va_plan_detl_style_color_id,
             style_product_size_group_detid, style_color_size_quantity, style_color_quantity, color_code,
             tran_va_plan_detl_style_id, color_code_gen, no_of_color, style_quantity, style_code, tran_va_plan_detl_id,
             style_code_gen, no_of_style, style_item_product_id, range_plan_detail_id, tran_va_plan_event_id,
             style_item_product_name, style_embellishment_ids, style_item_product_sub_category_id, sub_category_name,
             designer_member_id)
as
SELECT spsgd.style_product_size,
       vpdscs.tran_va_plan_detl_style_color_size_id,
       vpdscs.tran_va_plan_detl_style_color_id,
       vpdscs.style_product_size_group_detid,
       vpdscs.style_color_size_quantity,
       vpdsc.style_color_quantity,
       vpdsc.color_code,
       vpdsc.tran_va_plan_detl_style_id,
       vpds.color_code_gen,
       vpds.no_of_color,
       vpds.style_quantity,
       vpds.style_code,
       vpds.tran_va_plan_detl_id,
       vpd.style_code_gen,
       vpd.no_of_style,
       vpd.style_item_product_id,
       vpd.range_plan_detail_id,
       vpd.tran_va_plan_event_id,
       sip.style_item_product_name,
       vpds.style_embellishment_ids,
       vpds.style_item_product_sub_category_id,
       sipsc.sub_category_name,
       vpds.designer_member_id
FROM tran_va_plan_detl_style_color_size vpdscs
         JOIN tran_va_plan_detl_style_color vpdsc
              ON vpdsc.tran_va_plan_detl_style_color_id = vpdscs.tran_va_plan_detl_style_color_id
         JOIN tran_va_plan_detl_style vpds ON vpds.tran_va_plan_detl_style_id = vpdsc.tran_va_plan_detl_style_id
         JOIN tran_va_plan_detl vpd ON vpd.tran_va_plan_detl_id = vpds.tran_va_plan_detl_id
         JOIN style_item_product sip ON sip.style_item_product_id = vpd.style_item_product_id
         JOIN style_product_size_group_details spsgd
              ON spsgd.style_product_size_group_detid = vpdscs.style_product_size_group_detid
         LEFT JOIN style_item_product_sub_category sipsc
                   ON vpds.style_item_product_sub_category_id = sipsc.style_item_product_sub_category_id;

alter table vw_va_detl_style
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on vw_va_detl_style to anon;

grant delete, insert, references, select, trigger, truncate, update on vw_va_detl_style to authenticated;

grant delete, insert, references, select, trigger, truncate, update on vw_va_detl_style to service_role;


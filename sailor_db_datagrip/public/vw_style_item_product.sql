create view vw_style_item_product
            (style_product_size_group_name, style_item_product_name, style_item_type_name, style_product_type_name,
             style_item_origin_name, style_gender_name, master_category_name, added_by, date_added, updated_by,
             date_updated, style_item_product_id, style_item_type_id, style_product_type_id, style_item_origin_id,
             style_gender_id, style_master_category_id, fiscal_year_id, style_product_size_group_id)
as
SELECT spsgd.style_product_size_group_name,
       stp.style_item_product_name,
       sit.style_item_type_name,
       spt.style_product_type_name,
       sio.style_item_origin_name,
       sg.style_gender_name,
       smc.master_category_name,
       stp.added_by,
       stp.date_added,
       stp.updated_by,
       stp.date_updated,
       stp.style_item_product_id,
       stp.style_item_type_id,
       stp.style_product_type_id,
       stp.style_item_origin_id,
       stp.style_gender_id,
       stp.style_master_category_id,
       stp.fiscal_year_id,
       stp.style_product_size_group_id
FROM style_item_product stp
         JOIN style_item_type sit ON sit.style_item_type_id = stp.style_item_type_id
         JOIN style_product_type spt ON spt.style_product_type_id = stp.style_product_type_id
         JOIN style_item_origin sio ON sio.style_item_origin_id = stp.style_item_origin_id
         JOIN style_gender sg ON sg.style_gender_id = stp.style_gender_id
         JOIN style_master_category smc ON smc.style_master_category_id = stp.style_master_category_id
         JOIN style_product_size_group spsgd ON spsgd.style_product_size_group_id = stp.style_product_size_group_id;

alter table vw_style_item_product
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on vw_style_item_product to anon;

grant delete, insert, references, select, trigger, truncate, update on vw_style_item_product to authenticated;

grant delete, insert, references, select, trigger, truncate, update on vw_style_item_product to service_role;


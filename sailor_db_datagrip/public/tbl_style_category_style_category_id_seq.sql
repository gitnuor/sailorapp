create sequence tbl_style_category_style_category_id_seq;

alter sequence tbl_style_category_style_category_id_seq owner to postgres;

alter sequence tbl_style_category_style_category_id_seq owned by style_category.style_category_id;

grant select, update, usage on sequence tbl_style_category_style_category_id_seq to anon;

grant select, update, usage on sequence tbl_style_category_style_category_id_seq to authenticated;

grant select, update, usage on sequence tbl_style_category_style_category_id_seq to service_role;


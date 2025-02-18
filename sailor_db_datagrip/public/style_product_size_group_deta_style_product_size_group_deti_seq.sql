create sequence style_product_size_group_deta_style_product_size_group_deti_seq;

alter sequence style_product_size_group_deta_style_product_size_group_deti_seq owner to postgres;

alter sequence style_product_size_group_deta_style_product_size_group_deti_seq owned by style_product_size_group_details.style_product_size_group_detid;

grant select, update, usage on sequence style_product_size_group_deta_style_product_size_group_deti_seq to anon;

grant select, update, usage on sequence style_product_size_group_deta_style_product_size_group_deti_seq to authenticated;

grant select, update, usage on sequence style_product_size_group_deta_style_product_size_group_deti_seq to service_role;


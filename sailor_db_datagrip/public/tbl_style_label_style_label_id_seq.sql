create sequence tbl_style_label_style_label_id_seq;

alter sequence tbl_style_label_style_label_id_seq owner to postgres;

alter sequence tbl_style_label_style_label_id_seq owned by style_label.style_label_id;

grant select, update, usage on sequence tbl_style_label_style_label_id_seq to anon;

grant select, update, usage on sequence tbl_style_label_style_label_id_seq to authenticated;

grant select, update, usage on sequence tbl_style_label_style_label_id_seq to service_role;


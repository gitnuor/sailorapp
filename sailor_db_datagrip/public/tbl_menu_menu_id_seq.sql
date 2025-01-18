create sequence tbl_menu_menu_id_seq;

alter sequence tbl_menu_menu_id_seq owner to postgres;

alter sequence tbl_menu_menu_id_seq owned by menu.menu_id;

grant select, update, usage on sequence tbl_menu_menu_id_seq to anon;

grant select, update, usage on sequence tbl_menu_menu_id_seq to authenticated;

grant select, update, usage on sequence tbl_menu_menu_id_seq to service_role;


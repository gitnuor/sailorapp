create sequence tbl_department_department_id_seq;

alter sequence tbl_department_department_id_seq owner to postgres;

alter sequence tbl_department_department_id_seq owned by gen_department.department_id;

grant select, update, usage on sequence tbl_department_department_id_seq to anon;

grant select, update, usage on sequence tbl_department_department_id_seq to authenticated;

grant select, update, usage on sequence tbl_department_department_id_seq to service_role;


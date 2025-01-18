create sequence tbl_tran_business_plan_year_data_id_seq;

alter sequence tbl_tran_business_plan_year_data_id_seq owner to postgres;

alter sequence tbl_tran_business_plan_year_data_id_seq owned by tran_bp_year.tran_bp_year_id;

grant select, update, usage on sequence tbl_tran_business_plan_year_data_id_seq to anon;

grant select, update, usage on sequence tbl_tran_business_plan_year_data_id_seq to authenticated;

grant select, update, usage on sequence tbl_tran_business_plan_year_data_id_seq to service_role;


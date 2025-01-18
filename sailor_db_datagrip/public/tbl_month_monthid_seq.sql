create sequence tbl_month_monthid_seq;

alter sequence tbl_month_monthid_seq owner to postgres;

alter sequence tbl_month_monthid_seq owned by month.month_id;

grant select, update, usage on sequence tbl_month_monthid_seq to anon;

grant select, update, usage on sequence tbl_month_monthid_seq to authenticated;

grant select, update, usage on sequence tbl_month_monthid_seq to service_role;


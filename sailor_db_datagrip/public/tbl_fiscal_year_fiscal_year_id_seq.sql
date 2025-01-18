create sequence tbl_fiscal_year_fiscal_year_id_seq;

alter sequence tbl_fiscal_year_fiscal_year_id_seq owner to postgres;

alter sequence tbl_fiscal_year_fiscal_year_id_seq owned by gen_fiscal_year.fiscal_year_id;

grant select, update, usage on sequence tbl_fiscal_year_fiscal_year_id_seq to anon;

grant select, update, usage on sequence tbl_fiscal_year_fiscal_year_id_seq to authenticated;

grant select, update, usage on sequence tbl_fiscal_year_fiscal_year_id_seq to service_role;


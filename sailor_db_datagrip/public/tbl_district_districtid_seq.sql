create sequence tbl_district_districtid_seq;

alter sequence tbl_district_districtid_seq owner to postgres;

alter sequence tbl_district_districtid_seq owned by gen_district.district_id;

grant select, update, usage on sequence tbl_district_districtid_seq to anon;

grant select, update, usage on sequence tbl_district_districtid_seq to authenticated;

grant select, update, usage on sequence tbl_district_districtid_seq to service_role;


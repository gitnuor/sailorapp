create sequence gen_supplier_branch_gen_supplier_branch_id_seq;

alter sequence gen_supplier_branch_gen_supplier_branch_id_seq owner to postgres;

alter sequence gen_supplier_branch_gen_supplier_branch_id_seq owned by gen_map_supplier_branch.gen_map_supplier_branch_id;

grant select, update, usage on sequence gen_supplier_branch_gen_supplier_branch_id_seq to anon;

grant select, update, usage on sequence gen_supplier_branch_gen_supplier_branch_id_seq to authenticated;

grant select, update, usage on sequence gen_supplier_branch_gen_supplier_branch_id_seq to service_role;


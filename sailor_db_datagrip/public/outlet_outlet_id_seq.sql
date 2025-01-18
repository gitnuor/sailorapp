create sequence outlet_outlet_id_seq;

alter sequence outlet_outlet_id_seq owner to postgres;

alter sequence outlet_outlet_id_seq owned by gen_outlet.outlet_id;

grant select, update, usage on sequence outlet_outlet_id_seq to anon;

grant select, update, usage on sequence outlet_outlet_id_seq to authenticated;

grant select, update, usage on sequence outlet_outlet_id_seq to service_role;


create sequence serial_number_seq;

alter sequence serial_number_seq owner to postgres;

grant select, update, usage on sequence serial_number_seq to anon;

grant select, update, usage on sequence serial_number_seq to authenticated;

grant select, update, usage on sequence serial_number_seq to service_role;


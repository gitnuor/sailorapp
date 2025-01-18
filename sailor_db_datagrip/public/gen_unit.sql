create table gen_unit
(
    gen_unit_id  bigint generated always as identity
        primary key,
    unit_name    text,
    remarks      text,
    added_by     bigint                   not null,
    date_added   timestamp with time zone not null,
    updated_by   bigint,
    date_updated timestamp with time zone
);

alter table gen_unit
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on gen_unit to anon;

grant delete, insert, references, select, trigger, truncate, update on gen_unit to authenticated;

grant delete, insert, references, select, trigger, truncate, update on gen_unit to service_role;


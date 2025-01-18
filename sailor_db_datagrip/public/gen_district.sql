create table gen_district
(
    district_id bigint generated always as identity
        constraint district_pkey
            primary key,
    name        text,
    divisionid  bigint
        constraint divisionid
            references gen_division
);

alter table gen_district
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on gen_district to anon;

grant delete, insert, references, select, trigger, truncate, update on gen_district to authenticated;

grant delete, insert, references, select, trigger, truncate, update on gen_district to service_role;


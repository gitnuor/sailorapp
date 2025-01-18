create table gen_division
(
    division_id bigint not null
        constraint divisionid
            primary key,
    name        text
);

alter table gen_division
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on gen_division to anon;

grant delete, insert, references, select, trigger, truncate, update on gen_division to authenticated;

grant delete, insert, references, select, trigger, truncate, update on gen_division to service_role;


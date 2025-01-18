create table month
(
    month_id   bigint generated always as identity
        constraint tbl_month_pkey
            primary key,
    name       text,
    short_name text
);

alter table month
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on month to anon;

grant delete, insert, references, select, trigger, truncate, update on month to authenticated;

grant delete, insert, references, select, trigger, truncate, update on month to service_role;


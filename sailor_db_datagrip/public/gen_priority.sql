create table gen_priority
(
    priority_id   bigint generated always as identity
        primary key,
    priority_name text,
    added_by      bigint,
    updated_by    bigint,
    date_added    timestamp with time zone,
    date_updated  timestamp with time zone
);

alter table gen_priority
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on gen_priority to anon;

grant delete, insert, references, select, trigger, truncate, update on gen_priority to authenticated;

grant delete, insert, references, select, trigger, truncate, update on gen_priority to service_role;


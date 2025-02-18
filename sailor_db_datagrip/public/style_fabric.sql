create table style_fabric
(
    style_fabric_id   bigint generated by default as identity
        primary key,
    style_fabric_name text                     not null,
    is_active         boolean,
    added_by          bigint                   not null,
    date_added        timestamp with time zone not null,
    updated_by        bigint,
    date_updated      timestamp with time zone
);

alter table style_fabric
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on style_fabric to anon;

grant delete, insert, references, select, trigger, truncate, update on style_fabric to authenticated;

grant delete, insert, references, select, trigger, truncate, update on style_fabric to service_role;


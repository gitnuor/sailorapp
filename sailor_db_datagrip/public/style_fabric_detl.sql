create table style_fabric_detl
(
    style_fabric_detl_id bigint generated by default as identity
        primary key,
    style_fabric_id      bigint                   not null
        constraint fk_detl
            references style_fabric
            on delete cascade,
    gen_segment_id       bigint                   not null,
    segment_display_name text,
    is_active            boolean,
    added_by             bigint                   not null,
    date_added           timestamp with time zone not null,
    updated_by           bigint,
    date_updated         timestamp with time zone
);

alter table style_fabric_detl
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on style_fabric_detl to anon;

grant delete, insert, references, select, trigger, truncate, update on style_fabric_detl to authenticated;

grant delete, insert, references, select, trigger, truncate, update on style_fabric_detl to service_role;


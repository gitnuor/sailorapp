create table gen_item_structure_group_sub
(
    gen_item_structure_group_sub_id    bigint generated by default as identity
        primary key,
    item_structure_group_id            bigint                   not null
        constraint fk_detl
            references gen_item_structure_group,
    sub_group_name                     text,
    added_by                           bigint                   not null,
    date_added                         timestamp with time zone not null,
    updated_by                         bigint,
    date_updated                       timestamp with time zone,
    measurement_unit_id                bigint,
    default_measurement_unit_detail_id bigint
);

alter table gen_item_structure_group_sub
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on gen_item_structure_group_sub to anon;

grant delete, insert, references, select, trigger, truncate, update on gen_item_structure_group_sub to authenticated;

grant delete, insert, references, select, trigger, truncate, update on gen_item_structure_group_sub to service_role;


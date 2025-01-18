create table gen_item_structure_group
(
    item_structure_group_id bigint generated always as identity
        primary key,
    structure_group_name    text,
    added_by                bigint                   not null,
    date_added              timestamp with time zone not null,
    updated_by              bigint,
    date_updated            timestamp with time zone
);

alter table gen_item_structure_group
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on gen_item_structure_group to anon;

grant delete, insert, references, select, trigger, truncate, update on gen_item_structure_group to authenticated;

grant delete, insert, references, select, trigger, truncate, update on gen_item_structure_group to service_role;


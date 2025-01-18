create table style_master_category_structure_subgroup_mapping
(
    master_category_structure_subgroup_mapping_id bigint generated always as identity
        primary key,
    style_master_category_id                      bigint                   not null
        constraint fk_mstrcat
            references style_master_category,
    gen_item_structure_group_sub_id               bigint                   not null,
    added_by                                      bigint                   not null,
    date_added                                    timestamp with time zone not null,
    updated_by                                    bigint,
    date_updated                                  timestamp with time zone
);

alter table style_master_category_structure_subgroup_mapping
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on style_master_category_structure_subgroup_mapping to anon;

grant delete, insert, references, select, trigger, truncate, update on style_master_category_structure_subgroup_mapping to authenticated;

grant delete, insert, references, select, trigger, truncate, update on style_master_category_structure_subgroup_mapping to service_role;


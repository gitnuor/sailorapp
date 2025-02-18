create table gen_map_supplier_concern_person
(
    gen_map_supplier_concern_person_id bigint generated by default as identity
        constraint gen_supplier_concern_person_pkey
            primary key,
    gen_concern_person_information_id  bigint                                 not null,
    gen_general_information_id         bigint                                 not null
        constraint fk_gen_supp_con_per_to_general_info
            references gen_supplier_information
            on delete cascade,
    added_by                           bigint                                 not null,
    updated_by                         bigint,
    date_added                         timestamp with time zone default now() not null,
    date_updated                       timestamp with time zone default now()
);

alter table gen_map_supplier_concern_person
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on gen_map_supplier_concern_person to anon;

grant delete, insert, references, select, trigger, truncate, update on gen_map_supplier_concern_person to authenticated;

grant delete, insert, references, select, trigger, truncate, update on gen_map_supplier_concern_person to service_role;


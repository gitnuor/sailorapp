create table gen_measurement_unit_detail
(
    gen_measurement_unit_detail_id bigint generated always as identity
        primary key,
    gen_measurement_unit_id        bigint                   not null
        constraint detl
            references gen_measurement_unit,
    unit_detail_title              text                     not null,
    unit_detail_display            text                     not null,
    relative_factor                numeric                  not null,
    no_fraction                    boolean                  not null,
    remarks                        text,
    added_by                       bigint                   not null,
    date_added                     timestamp with time zone not null,
    updated_by                     bigint,
    date_updated                   timestamp with time zone
);

alter table gen_measurement_unit_detail
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on gen_measurement_unit_detail to anon;

grant delete, insert, references, select, trigger, truncate, update on gen_measurement_unit_detail to authenticated;

grant delete, insert, references, select, trigger, truncate, update on gen_measurement_unit_detail to service_role;


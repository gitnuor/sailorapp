create table tran_sample_order
(
    tran_sample_order_id       bigint generated always as identity
        primary key,
    tran_va_plan_detl_id       bigint                   not null,
    tran_sample_order_number   text,
    order_date                 timestamp with time zone not null,
    delivery_date              timestamp with time zone not null,
    delivery_unit_id           bigint                   not null
        constraint fk_unit
            references gen_unit,
    order_quantity             bigint                   not null,
    designer_member_id         bigint                   not null,
    remarks                    text,
    is_submit                  boolean,
    is_approved                boolean,
    approved_by                bigint,
    date_approved              timestamp with time zone,
    added_by                   bigint                   not null,
    date_added                 timestamp with time zone not null,
    updated_by                 bigint,
    date_updated               timestamp with time zone,
    sample_photos              json,
    tran_va_plan_detl_style_id bigint
        constraint fk_det_style
            references tran_va_plan_detl_style
);

alter table tran_sample_order
    owner to postgres;

create trigger set_serial_number
    before insert
    on tran_sample_order
    for each row
execute procedure generate_serial_number();

grant delete, insert, references, select, trigger, truncate, update on tran_sample_order to anon;

grant delete, insert, references, select, trigger, truncate, update on tran_sample_order to authenticated;

grant delete, insert, references, select, trigger, truncate, update on tran_sample_order to service_role;


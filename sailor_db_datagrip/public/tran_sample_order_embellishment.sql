create table tran_sample_order_embellishment
(
    tran_sample_order_embellishment_id bigint generated always as identity
        primary key,
    tran_sample_order_id               bigint                   not null
        constraint fk_det
            references tran_sample_order
            on delete cascade,
    embellishment_id                   bigint                   not null
        constraint fk_embeli
            references gen_process_master,
    remarks                            text,
    added_by                           bigint                   not null,
    date_added                         timestamp with time zone not null,
    updated_by                         bigint,
    date_updated                       timestamp with time zone
);

alter table tran_sample_order_embellishment
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on tran_sample_order_embellishment to anon;

grant delete, insert, references, select, trigger, truncate, update on tran_sample_order_embellishment to authenticated;

grant delete, insert, references, select, trigger, truncate, update on tran_sample_order_embellishment to service_role;


create table tran_va_plan
(
    tran_va_plan_id    bigint generated by default as identity (maxvalue 2147483647)
        primary key,
    tran_range_plan_id bigint
        constraint fk_rangeplan
            references tran_range_plan,
    remarks            text,
    is_submitted       boolean,
    is_approved        boolean,
    approved_by        bigint,
    approve_date       timestamp with time zone,
    approve_remarks    text,
    added_by           bigint                                 not null,
    updated_by         bigint,
    date_added         timestamp with time zone default now() not null,
    date_updated       timestamp with time zone,
    plan_event_details json
);

alter table tran_va_plan
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on tran_va_plan to anon;

grant delete, insert, references, select, trigger, truncate, update on tran_va_plan to authenticated;

grant delete, insert, references, select, trigger, truncate, update on tran_va_plan to service_role;


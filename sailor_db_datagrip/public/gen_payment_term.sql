create table gen_payment_term
(
    gen_payment_term_id bigint generated by default as identity
        primary key,
    payment_term        text,
    added_by            bigint                                 not null,
    updated_by          bigint,
    date_added          timestamp with time zone default now() not null,
    date_updated        timestamp with time zone default now()
);

alter table gen_payment_term
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on gen_payment_term to anon;

grant delete, insert, references, select, trigger, truncate, update on gen_payment_term to authenticated;

grant delete, insert, references, select, trigger, truncate, update on gen_payment_term to service_role;


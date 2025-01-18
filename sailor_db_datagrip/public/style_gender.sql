create table style_gender
(
    style_gender_id   bigint  not null
        constraint tbl_style_gender_pkey
            primary key,
    style_gender_name text    not null,
    style_gender_code text    not null,
    is_active         boolean not null,
    added_by          bigint  not null,
    date_added        timestamp,
    updated_by        bigint,
    date_updated      timestamp
);

alter table style_gender
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on style_gender to anon;

grant delete, insert, references, select, trigger, truncate, update on style_gender to authenticated;

grant delete, insert, references, select, trigger, truncate, update on style_gender to service_role;


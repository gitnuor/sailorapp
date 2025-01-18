create table menu
(
    menu_id      integer generated always as identity
        constraint pkey
            primary key,
    parent_id    integer,
    menu_caption varchar,
    navigate_url varchar,
    image_url    varchar,
    seq_no       integer,
    is_visible   boolean default false,
    added_by     integer default 0,
    updated_by   integer default 0,
    date_added   timestamp,
    date_updated timestamp
);

alter table menu
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on menu to anon;

grant delete, insert, references, select, trigger, truncate, update on menu to authenticated;

grant delete, insert, references, select, trigger, truncate, update on menu to service_role;


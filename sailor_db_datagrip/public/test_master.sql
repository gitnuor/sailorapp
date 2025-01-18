create table test_master
(
    test_master_id bigint not null
        primary key,
    name           text
);

alter table test_master
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on test_master to anon;

grant delete, insert, references, select, trigger, truncate, update on test_master to authenticated;

grant delete, insert, references, select, trigger, truncate, update on test_master to service_role;


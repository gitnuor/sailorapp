create table test_master_det_det
(
    test_master_det_det_id bigint not null,
    test_master_det_id     bigint not null,
    name                   text
);

alter table test_master_det_det
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on test_master_det_det to anon;

grant delete, insert, references, select, trigger, truncate, update on test_master_det_det to authenticated;

grant delete, insert, references, select, trigger, truncate, update on test_master_det_det to service_role;


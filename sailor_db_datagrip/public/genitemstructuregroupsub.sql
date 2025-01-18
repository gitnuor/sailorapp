create table genitemstructuregroupsub
(
    jsonb_agg jsonb
);

alter table genitemstructuregroupsub
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on genitemstructuregroupsub to anon;

grant delete, insert, references, select, trigger, truncate, update on genitemstructuregroupsub to authenticated;

grant delete, insert, references, select, trigger, truncate, update on genitemstructuregroupsub to service_role;


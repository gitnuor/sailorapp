create table sampleordersubgrouplist
(
    jsonb_agg jsonb
);

alter table sampleordersubgrouplist
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on sampleordersubgrouplist to anon;

grant delete, insert, references, select, trigger, truncate, update on sampleordersubgrouplist to authenticated;

grant delete, insert, references, select, trigger, truncate, update on sampleordersubgrouplist to service_role;


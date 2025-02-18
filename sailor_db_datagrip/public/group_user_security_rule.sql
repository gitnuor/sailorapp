create table group_user_security_rule
(
    child_id           bigint generated by default as identity
        constraint "Security.GroupUserSecurityRule_pkey"
            primary key,
    group_code         bigint not null,
    security_rule_code bigint,
    added_by           bigint,
    updated_by         bigint,
    date_added         timestamp,
    date_updated       timestamp
);

alter table group_user_security_rule
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on group_user_security_rule to anon;

grant delete, insert, references, select, trigger, truncate, update on group_user_security_rule to authenticated;

grant delete, insert, references, select, trigger, truncate, update on group_user_security_rule to service_role;


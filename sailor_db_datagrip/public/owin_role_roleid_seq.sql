create sequence owin_role_roleid_seq;

alter sequence owin_role_roleid_seq owner to postgres;

alter sequence owin_role_roleid_seq owned by owin_role.owin_role_id;

grant select, update, usage on sequence owin_role_roleid_seq to anon;

grant select, update, usage on sequence owin_role_roleid_seq to authenticated;

grant select, update, usage on sequence owin_role_roleid_seq to service_role;


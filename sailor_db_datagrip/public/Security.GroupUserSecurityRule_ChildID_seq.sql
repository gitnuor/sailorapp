create sequence "Security.GroupUserSecurityRule_ChildID_seq";

alter sequence "Security.GroupUserSecurityRule_ChildID_seq" owner to postgres;

alter sequence "Security.GroupUserSecurityRule_ChildID_seq" owned by group_user_security_rule.child_id;

grant select, update, usage on sequence "Security.GroupUserSecurityRule_ChildID_seq" to anon;

grant select, update, usage on sequence "Security.GroupUserSecurityRule_ChildID_seq" to authenticated;

grant select, update, usage on sequence "Security.GroupUserSecurityRule_ChildID_seq" to service_role;


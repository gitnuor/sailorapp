create sequence "Security.LoginUserAttachedWithGroupUser_ChildID_seq";

alter sequence "Security.LoginUserAttachedWithGroupUser_ChildID_seq" owner to postgres;

alter sequence "Security.LoginUserAttachedWithGroupUser_ChildID_seq" owned by login_user_attached_with_group_user.child_id;

grant select, update, usage on sequence "Security.LoginUserAttachedWithGroupUser_ChildID_seq" to anon;

grant select, update, usage on sequence "Security.LoginUserAttachedWithGroupUser_ChildID_seq" to authenticated;

grant select, update, usage on sequence "Security.LoginUserAttachedWithGroupUser_ChildID_seq" to service_role;


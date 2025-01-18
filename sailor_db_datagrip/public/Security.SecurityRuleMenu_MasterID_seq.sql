create sequence "Security.SecurityRuleMenu_MasterID_seq";

alter sequence "Security.SecurityRuleMenu_MasterID_seq" owner to postgres;

alter sequence "Security.SecurityRuleMenu_MasterID_seq" owned by security_rule_menu.master_id;

grant select, update, usage on sequence "Security.SecurityRuleMenu_MasterID_seq" to anon;

grant select, update, usage on sequence "Security.SecurityRuleMenu_MasterID_seq" to authenticated;

grant select, update, usage on sequence "Security.SecurityRuleMenu_MasterID_seq" to service_role;


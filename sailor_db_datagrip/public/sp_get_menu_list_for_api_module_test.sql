create function sp_get_menu_list_for_api_module_test()
    returns TABLE(menu_id bigint)
    language plpgsql
as
$$
DECLARE 
 _is_super_user BOOLEAN:=false;
 _is_admin BOOLEAN := false; 
 _user_code integer:= 1;
 Begin
 
 SELECT  is_super_user,is_admin INTO _is_super_user,_is_admin
 FROM PUBLIC.tbl_login_user WHERE user_code = _user_code;
	
    IF(_is_admin=false and _is_super_user=false) then
	
		RETURN QUERY 
		With pm 
		AS(
			SELECT srm.menu_id
			FROM public.tbl_security_rule_menu srm
			Inner Join public.tbl_group_user_security_rule guser On guser.security_rule_code = srm.security_rule_code
			Inner Join public.tbl_login_user_attached_with_group_user luawgu On luawgu.group_code = guser.group_code
			Inner Join public.tbl_menu M On M.menu_id = SRM.menu_id
			Where luawgu.user_code = _user_code And SRM.can_select = 1 
			Group By srm.menu_id, M.application_id, srm.can_select, srm.can_insert, srm.can_update, srm.can_delete
		), app As(
			Select application_id
			From pm
			Group By application_id
		)
		
-- 		RETURN QUERY Select m.menu_id, COALESCE(pm.can_select,0) can_select, COALESCE(pm.can_insert,0) can_insert, COALESCE(pm.can_update,0) can_update, COALESCE(pm.can_delete,0) can_delete
		
-- 		RETURN QUERY Select pm.menu_id
-- 		Inner Join app On app.application_id = m.application_id
-- 		Inner Join pm On pm.menu_id = m.menu_id
-- 		Where m.is_api=1 And m.is_visible = 1
-- 		Order By m.seq_no, m.menu_id;
		
		 Select mn.menu_id
		From public.tbl_menu mn
		Where mn.is_visible = true
		Order By mn.seq_no, mn.menu_id;
		
	Else
-- 		RETURN QUERY Select mn.menu_id, 1::BOOLEAN can_select, '1'::BOOLEAN can_insert, '1':: BOOLEAN can_update, 1:: BOOLEAN can_delete 
		RETURN QUERY Select mn.menu_id
		From public.tbl_menu mn
		Where mn.is_visible = true
		Order By mn.seq_no, mn.menu_id;
		
		
		
	End if;
	
	END;
$$;

alter function sp_get_menu_list_for_api_module_test() owner to postgres;

grant execute on function sp_get_menu_list_for_api_module_test() to anon;

grant execute on function sp_get_menu_list_for_api_module_test() to authenticated;

grant execute on function sp_get_menu_list_for_api_module_test() to service_role;


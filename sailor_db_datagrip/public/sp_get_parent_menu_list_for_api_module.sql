create function sp_get_parent_menu_list_for_api_module(user_id bigint)
    returns TABLE(menu_id integer, parent_id integer, menu_caption character varying, seq_no integer)
    language plpgsql
as
$$

 Begin
 RETURN QUERY
 
 	Select m.menu_id, m.parent_id, m.menu_caption, m.seq_no 
	From menu m

	Where m.menu_id = m.parent_id --
	and m.menu_id in (
	select rp.menu_id from public.owin_role_permission rp
	inner join  public.owin_user us on us.role_id=rp.role_id and  rp.menu_id=m.menu_id and us.userid=user_id
	    )
	Group By m.menu_id, m.parent_id, m.menu_caption, m.seq_no 
	Order By m.seq_no;
	
	END;
$$;

alter function sp_get_parent_menu_list_for_api_module(bigint) owner to postgres;

grant execute on function sp_get_parent_menu_list_for_api_module(bigint) to anon;

grant execute on function sp_get_parent_menu_list_for_api_module(bigint) to authenticated;

grant execute on function sp_get_parent_menu_list_for_api_module(bigint) to service_role;

create function sp_get_parent_menu_list_for_api_module()
    returns TABLE(menu_id integer, parent_id integer, menu_caption character varying, seq_no integer)
    language plpgsql
as
$$

 Begin
 RETURN QUERY
 
 	Select m.menu_id, m.parent_id, m.menu_caption, m.seq_no 
	From menu m
	Where m.menu_id = m.parent_id
	Group By m.menu_id, m.parent_id, m.menu_caption, m.seq_no 
	Order By m.seq_no;
	
	END;
$$;

alter function sp_get_parent_menu_list_for_api_module() owner to postgres;

grant execute on function sp_get_parent_menu_list_for_api_module() to anon;

grant execute on function sp_get_parent_menu_list_for_api_module() to authenticated;

grant execute on function sp_get_parent_menu_list_for_api_module() to service_role;


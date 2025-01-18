create function sp_get_menu_list_for_api_module(user_id bigint)
    returns TABLE(menu_id integer, parent_id integer, menu_caption character varying, navigate_url character varying, image_url character varying, seq_no integer, is_visible boolean, added_by integer, updated_by integer, date_added timestamp without time zone, date_updated timestamp without time zone)
    language plpgsql
as
$$
DECLARE
    _is_super_user BOOLEAN := false;
    _is_admin      BOOLEAN := false;
    _user_code     integer := 1;
Begin

    -- 		RETURN QUERY Select mn.menu_id, 1::BOOLEAN can_select, '1'::BOOLEAN can_insert, '1':: BOOLEAN can_update, 1:: BOOLEAN can_delete
    RETURN QUERY Select mn.*
                 From public.menu mn

                 Where mn.is_visible = true
                   And mn.menu_id <> mn.parent_id
                   --
                   and mn.menu_id in (select rp.menu_id
                                      from public.owin_role_permission rp

                                               inner join public.owin_user us on us.role_id = rp.role_id
                                          and us.userid = user_id)
                 Order By mn.seq_no, mn.menu_id;


END;
$$;

alter function sp_get_menu_list_for_api_module(bigint) owner to postgres;

grant execute on function sp_get_menu_list_for_api_module(bigint) to anon;

grant execute on function sp_get_menu_list_for_api_module(bigint) to authenticated;

grant execute on function sp_get_menu_list_for_api_module(bigint) to service_role;


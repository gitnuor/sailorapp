create function va_plan_get_designer_assign_details_det(tran_va_plan_event_id_filter bigint)
    returns TABLE(style_item_product_id bigint, designer_member_id bigint, team_member_marketing_name text, no_of_style bigint, total_style_quantity bigint)
    language plpgsql
as
$$

 Begin
 RETURN QUERY
 select master.style_item_product_id,
det.designer_member_id,gtm.team_member_marketing_name,
cast( count(det.style_quantity) as bigint) as no_of_style,
cast (sum(det.style_quantity) as bigint) as total_style_quantity   from public.tran_va_plan_detl_style det
inner join public.tran_va_plan_detl master on master.tran_va_plan_detl_id=det.tran_va_plan_detl_id
inner join public.gen_team_members gtm on gtm.gen_team_members_id=det.designer_member_id
inner join public.tran_va_plan_events tvpe 
on tvpe.tran_va_plan_event_id=master.tran_va_plan_event_id
where det.designer_member_id!=0 and 
tvpe.tran_va_plan_event_id=tran_va_plan_event_id_filter
group by master.style_item_product_id,det.designer_member_id,gtm.team_member_marketing_name;

END;
$$;

alter function va_plan_get_designer_assign_details_det(bigint) owner to postgres;

grant execute on function va_plan_get_designer_assign_details_det(bigint) to anon;

grant execute on function va_plan_get_designer_assign_details_det(bigint) to authenticated;

grant execute on function va_plan_get_designer_assign_details_det(bigint) to service_role;


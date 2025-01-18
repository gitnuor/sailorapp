create function va_plan_get_designer_assign_details(tran_va_plan_event_id_filter bigint)
    returns TABLE(no_of_style bigint, tran_va_plan_event_id bigint, tran_va_plan_id bigint, tran_va_plan_detl_id bigint, style_code_gen text, is_separate_price boolean, event_id bigint, tran_range_plan_event_id bigint, is_finalized boolean, tran_bp_event_id bigint, tran_bp_year_id bigint, total_rangeplan_mrp_value numeric, total_rangeplan_quantity bigint, total_rangeplan_cpu_value numeric, style_product_size_group_id bigint, is_submitted boolean, is_approved boolean, approved_by bigint, approve_date timestamp with time zone, approve_remarks text, remarks text, bp_yearly_gross_sales numeric, event_gross_sales numeric, range_plan_id bigint, range_plan_detail_id bigint, range_plan_quantity bigint, mrp_per_pc_value numeric, mrp_value numeric, cpu_per_pc_value numeric, cpu_value numeric, priority_id bigint, prev_year_quantity bigint, prev_year_efficiency numeric, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, added_by bigint, date_added timestamp with time zone, updated_by bigint, date_updated timestamp with time zone, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint)
    language plpgsql
as
$$

 Begin
 RETURN QUERY
  select 

  tvpd.no_of_style,
  tvpe.tran_va_plan_event_id,
  tvp.tran_va_plan_id,
  tvpd.tran_va_plan_detl_id,
 
	tvpd.style_code_gen,

spsg.is_separate_price,
tbe.event_id,
trpe.tran_range_plan_event_id,trpe.is_finalized,
trpe.tran_bp_event_id,
tby.tran_bp_year_id,
trpe.total_mrp_value as total_rangeplan_mrp_value ,
trpe.total_range_plan_quantity as total_rangeplan_quantity,
trpe.total_cpu_value as total_rangeplan_cpu_value,
vp.style_product_size_group_id,
rp.is_submitted,
    rp.is_approved,
    rp.approved_by,
    rp.approve_date,
    rp.approve_remarks,
	rp.remarks,

tby.gross_sales as bp_yearly_gross_sales,
tbe.event_gross_sales,

rpd.range_plan_id,rpd.range_plan_detail_id,

rpd.range_plan_quantity,
rpd.mrp_per_pc_value,
rpd.mrp_value,
rpd.cpu_per_pc_value,
rpd.cpu_value,
rpd.priority_id,
rpd.prev_year_quantity,
rpd.prev_year_efficiency,

vp.style_item_product_name, 
vp.style_item_type_name, 
vp.style_product_type_name, 
vp.style_item_origin_name, 
vp.style_gender_name, 

vp.master_category_name, 
rpd.added_by, rpd.date_added, 
rpd.updated_by, 
rpd.date_updated, 
vp.style_item_product_id, 
vp.style_item_type_id, 
vp.style_product_type_id, 
vp.style_item_origin_id, 
vp.style_gender_id, 
vp.style_master_category_id

from vw_style_item_product vp
inner join public.style_product_size_group spsg 
on spsg.style_product_size_group_id=vp.style_product_size_group_id
inner  join tran_range_plan_details rpd on rpd.style_item_product_id=vp.style_item_product_id

inner  join tran_range_plan rp on rp.range_plan_id=rpd.range_plan_id
inner  join tran_bp_year tby on tby.fiscal_year_id=vp.fiscal_year_id
inner  join tran_bp_event tbe on tbe.tran_bp_year_id=tby.tran_bp_year_id
and rpd.tran_bp_event_id=tbe.tran_bp_event_id
inner  join public.tran_range_plan_events trpe 
on  trpe.tran_bp_event_id=tbe.tran_bp_event_id
	
	

inner join 
public.tran_va_plan_detl tvpd on
	tvpd.style_item_product_id=vp.style_item_product_id
	and tvpd.range_plan_detail_id=rpd.range_plan_detail_id
left outer join 
(select  detl_single.style_item_product_id from public.tran_va_plan_detl detl_single 
group by detl_single.style_item_product_id
) det_single
on det_single.style_item_product_id=vp.style_item_product_id
inner join public.tran_va_plan_events tvpe on
	tvpe.tran_va_plan_event_id=tvpd.tran_va_plan_event_id
inner join public.tran_va_plan tvp on
	tvp.tran_va_plan_id=tvpe.tran_va_plan_id
	

where 

tvpe.tran_va_plan_event_id=tran_va_plan_event_id_filter;

END;
$$;

alter function va_plan_get_designer_assign_details(bigint) owner to postgres;

grant execute on function va_plan_get_designer_assign_details(bigint) to anon;

grant execute on function va_plan_get_designer_assign_details(bigint) to authenticated;

grant execute on function va_plan_get_designer_assign_details(bigint) to service_role;


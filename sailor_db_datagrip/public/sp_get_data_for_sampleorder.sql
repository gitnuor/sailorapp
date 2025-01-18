create function sp_get_data_for_sampleorder(tran_va_plan_event_id_filter bigint, user_id_filter integer)
    returns TABLE(tran_sample_order_id bigint, tran_va_plan_detl_style_id bigint, style_code text, total_sample_count bigint, style_quantity bigint, color_code_gen text, no_of_color bigint, tran_va_plan_event_id bigint, tran_va_plan_id bigint, tran_va_plan_detl_id bigint, no_of_style bigint, style_code_gen text, is_separate_price boolean, event_id bigint, tran_range_plan_event_id bigint, is_finalized boolean, tran_bp_event_id bigint, tran_bp_year_id bigint, total_rangeplan_mrp_value numeric, total_rangeplan_quantity bigint, total_rangeplan_cpu_value numeric, style_product_size_group_id bigint, is_submitted boolean, is_approved boolean, approved_by bigint, approve_date timestamp with time zone, approve_remarks text, remarks text, bp_yearly_gross_sales numeric, event_gross_sales numeric, range_plan_id bigint, range_plan_detail_id bigint, range_plan_quantity bigint, mrp_per_pc_value numeric, mrp_value numeric, cpu_per_pc_value numeric, cpu_value numeric, priority_id bigint, prev_year_quantity bigint, prev_year_efficiency numeric, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, added_by bigint, date_added timestamp with time zone, updated_by bigint, date_updated timestamp with time zone, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint, sample_order_details text)
    language plpgsql
as
$$

Begin

     RETURN QUERY
     select
         tso.tran_sample_order_id,
         tvapds.tran_va_plan_detl_style_id,
               tvapds.style_code,
               (select cast(count(tmp1.tran_sample_order_id) as bigint)
                from public.tran_sample_order tmp1
                where tmp1.tran_va_plan_detl_id = tvpd.tran_va_plan_detl_id
                  and tmp1.tran_va_plan_detl_style_id = tvapds.tran_va_plan_detl_style_id) as total_sample_count,
               tvapds.style_quantity,
               tvapds.color_code_gen,
               tvapds.no_of_color,
               tvpe.tran_va_plan_event_id,
               tvp.tran_va_plan_id,
               tvpd.tran_va_plan_detl_id,
               tvpd.no_of_style,
               tvpd.style_code_gen,
               spsg.is_separate_price,
               tbe.event_id,
               trpe.tran_range_plan_event_id,
               trpe.is_finalized,
               trpe.tran_bp_event_id,
               tby.tran_bp_year_id,
               trpe.total_mrp_value                                                        as total_rangeplan_mrp_value,
               trpe.total_range_plan_quantity                                              as total_rangeplan_quantity,
               trpe.total_cpu_value                                                        as total_rangeplan_cpu_value,
               vp.style_product_size_group_id,
               rp.is_submitted,
               rp.is_approved,
               rp.approved_by,
               rp.approve_date,
               rp.approve_remarks,
               rp.remarks,
               tby.gross_sales                                                             as bp_yearly_gross_sales,
               tbe.event_gross_sales,
               rpd.range_plan_id,
               rpd.range_plan_detail_id,
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
               rpd.added_by,
               rpd.date_added,
               rpd.updated_by,
               rpd.date_updated,
               vp.style_item_product_id,
               vp.style_item_type_id,
               vp.style_product_type_id,
               vp.style_item_origin_id,
               vp.style_gender_id,
               vp.style_master_category_id,

                row_to_json(sp_get_sample_order_details_by_id( tso.tran_sample_order_id))::TEXT as sample_order_details

        FROM vw_style_item_product vp
                 INNER JOIN public.style_product_size_group spsg
                 ON spsg.style_product_size_group_id = vp.style_product_size_group_id
                 INNER JOIN tran_range_plan_details rpd ON rpd.style_item_product_id = vp.style_item_product_id
                 INNER JOIN tran_range_plan rp ON rp.range_plan_id = rpd.range_plan_id
                 INNER JOIN tran_bp_year tby ON tby.fiscal_year_id = vp.fiscal_year_id
                 INNER JOIN tran_bp_event tbe ON tbe.tran_bp_year_id = tby.tran_bp_year_id
            AND rpd.tran_bp_event_id = tbe.tran_bp_event_id
                 INNER JOIN public.tran_range_plan_events trpe ON trpe.tran_bp_event_id = tbe.tran_bp_event_id
                 INNER JOIN public.tran_va_plan_detl tvpd ON tvpd.style_item_product_id = vp.style_item_product_id
            AND tvpd.range_plan_detail_id = rpd.range_plan_detail_id
                 INNER JOIN
             (SELECT detl_single.style_item_product_id
              FROM public.tran_va_plan_detl detl_single
              GROUP BY detl_single.style_item_product_id) det_single
             ON det_single.style_item_product_id = vp.style_item_product_id
                 INNER JOIN public.tran_va_plan_events tvpe ON tvpe.tran_va_plan_event_id = tvpd.tran_va_plan_event_id
                 INNER JOIN public.tran_va_plan tvp ON tvp.tran_va_plan_id = tvpe.tran_va_plan_id
                 INNER JOIN public.tran_va_plan_detl_style tvapds
                            on tvapds.tran_va_plan_detl_id = tvpd.tran_va_plan_detl_id
                 INNER JOIN public.gen_team_members tm ON tvapds.designer_member_id = tm.gen_team_members_id
                LEFT OUTER JOIN tran_sample_order tso on tvapds.tran_va_plan_detl_style_id = tso.tran_va_plan_detl_style_id
        where tvpd.tran_va_plan_event_id = tran_va_plan_event_id_filter
        and tm.user_id = user_id_filter
    union
    select
         tso.tran_sample_order_id,
         tvapds.tran_va_plan_detl_style_id,
               tvapds.style_code,
               (select cast(count(tmp1.tran_sample_order_id) as bigint)
                from public.tran_sample_order tmp1
                where tmp1.tran_va_plan_detl_id = tvpd.tran_va_plan_detl_id
                  and tmp1.tran_va_plan_detl_style_id = tvapds.tran_va_plan_detl_style_id) as total_sample_count,
               tvapds.style_quantity,
               tvapds.color_code_gen,
               tvapds.no_of_color,
               tvpe.tran_va_plan_event_id,
               tvp.tran_va_plan_id,
               tvpd.tran_va_plan_detl_id,
               tvpd.no_of_style,
               tvpd.style_code_gen,
               spsg.is_separate_price,
               tbe.event_id,
               trpe.tran_range_plan_event_id,
               trpe.is_finalized,
               trpe.tran_bp_event_id,
               tby.tran_bp_year_id,
               trpe.total_mrp_value                                                        as total_rangeplan_mrp_value,
               trpe.total_range_plan_quantity                                              as total_rangeplan_quantity,
               trpe.total_cpu_value                                                        as total_rangeplan_cpu_value,
               vp.style_product_size_group_id,
               rp.is_submitted,
               rp.is_approved,
               rp.approved_by,
               rp.approve_date,
               rp.approve_remarks,
               rp.remarks,
               tby.gross_sales                                                             as bp_yearly_gross_sales,
               tbe.event_gross_sales,
               rpd.range_plan_id,
               rpd.range_plan_detail_id,
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
               rpd.added_by,
               rpd.date_added,
               rpd.updated_by,
               rpd.date_updated,
               vp.style_item_product_id,
               vp.style_item_type_id,
               vp.style_product_type_id,
               vp.style_item_origin_id,
               vp.style_gender_id,
               vp.style_master_category_id,

               ''::TEXT as sample_order_details

        FROM vw_style_item_product vp
                 INNER JOIN public.style_product_size_group spsg
                 ON spsg.style_product_size_group_id = vp.style_product_size_group_id
                 INNER JOIN tran_range_plan_details rpd ON rpd.style_item_product_id = vp.style_item_product_id
                 INNER JOIN tran_range_plan rp ON rp.range_plan_id = rpd.range_plan_id
                 INNER JOIN tran_bp_year tby ON tby.fiscal_year_id = vp.fiscal_year_id
                 INNER JOIN tran_bp_event tbe ON tbe.tran_bp_year_id = tby.tran_bp_year_id
            AND rpd.tran_bp_event_id = tbe.tran_bp_event_id
                 INNER JOIN public.tran_range_plan_events trpe ON trpe.tran_bp_event_id = tbe.tran_bp_event_id
                 INNER JOIN public.tran_va_plan_detl tvpd ON tvpd.style_item_product_id = vp.style_item_product_id
            AND tvpd.range_plan_detail_id = rpd.range_plan_detail_id
                 INNER JOIN
             (SELECT detl_single.style_item_product_id
              FROM public.tran_va_plan_detl detl_single
              GROUP BY detl_single.style_item_product_id) det_single
             ON det_single.style_item_product_id = vp.style_item_product_id
                 INNER JOIN public.tran_va_plan_events tvpe ON tvpe.tran_va_plan_event_id = tvpd.tran_va_plan_event_id
                 INNER JOIN public.tran_va_plan tvp ON tvp.tran_va_plan_id = tvpe.tran_va_plan_id
                 INNER JOIN public.tran_va_plan_detl_style tvapds
                            on tvapds.tran_va_plan_detl_id = tvpd.tran_va_plan_detl_id
                 INNER JOIN public.gen_team_members tm ON tvapds.designer_member_id = tm.gen_team_members_id
                LEFT OUTER JOIN tran_sample_order tso on tvapds.tran_va_plan_detl_style_id = tso.tran_va_plan_detl_style_id
        where tvpd.tran_va_plan_event_id = tran_va_plan_event_id_filter AND tso.tran_sample_order_id IS NULL
        and tm.user_id = user_id_filter;


END;
$$;

alter function sp_get_data_for_sampleorder(bigint, integer) owner to postgres;

grant execute on function sp_get_data_for_sampleorder(bigint, integer) to anon;

grant execute on function sp_get_data_for_sampleorder(bigint, integer) to authenticated;

grant execute on function sp_get_data_for_sampleorder(bigint, integer) to service_role;


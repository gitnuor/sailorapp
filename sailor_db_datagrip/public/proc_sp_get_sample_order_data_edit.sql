create function proc_sp_get_sample_order_data_edit(tran_sample_order_id_filter bigint, style_item_product_id_filter bigint, fiscal_year_id_filter bigint, team_category_id_filter bigint)
    returns TABLE(tran_sample_order_id bigint, tran_va_plan_detl_id bigint, tran_sample_order_number text, order_date timestamp with time zone, delivery_date timestamp with time zone, delivery_unit_id bigint, order_quantity bigint, designer_member_id bigint, sample_photos json, tran_va_plan_detl_style_id bigint, style_code text, style_quantity bigint, no_of_color bigint, color_code_gen text, style_item_product_sub_category_id bigint, style_product_size_group_name text, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint, fiscal_year_id bigint, style_product_size_group_id bigint, designer_list text, mapping_structure_list text, gen_itemstructure_groupsub_list text, gen_unit_list text, style_product_unit_list text, style_product_sizegroupdetails_list text, sample_order_embellishmentlist text, sample_order_detaillist text, sampleorder_subgroup_list text)
    language plpgsql
as
$$
DECLARE
    _style_item_product_id bigint;
     _style_item_type_id bigint;
     _style_product_type_id bigint;
     _style_item_origin_id bigint;
     _style_gender_id bigint;
     _style_master_category_id bigint;
     _style_product_size_group_id bigint;

    designerlist text;
    mappingstructurelist text;
    genitemstructuregroupsub text;
    genunitlist text;
    styleproductunitlist text;
    styleproductsizegroupdetailslist text;
    sampleorderembellishmentlist text;
    sampleorderdetaillist text;
    sampleordersubgrouplist text;

BEGIN

     SELECT
          p.style_item_product_id  ,
          p.style_item_type_id  ,
          p.style_product_type_id  ,
          p.style_item_origin_id  ,
          p.style_gender_id  ,
          p.style_master_category_id  ,
          p.style_product_size_group_id
         INTO _style_item_product_id,_style_item_type_id,_style_product_type_id,_style_item_origin_id,
             _style_gender_id,_style_master_category_id,_style_product_size_group_id

        FROM vw_style_item_product p
        where p.style_item_product_id = style_item_product_id_filter
               and p.fiscal_year_id=fiscal_year_id_filter;

     SELECT
        jsonb_agg(jsonb_build_object(
        'gen_team_members_id', t.gen_team_members_id,
        'team_member_marketing_name', t.team_member_marketing_name,
        'team_member_marketing_code',t.team_member_marketing_code,
        'team_member_designation',t.team_member_designation,
        'phone_number',t.phone_number,
        'email',t.email,
        'photo',t.photo,
        'user_id',t.user_id
    ))
    INTO designerlist
    FROM gen_team_members t
    WHERE t.gen_team_group_id=team_category_id_filter;

     --genitemstructuregroupsub list
     SELECT
        jsonb_agg(jsonb_build_object(
        'gen_item_structure_group_sub_id', t.gen_item_structure_group_sub_id,
        'item_structure_group_id', t.item_structure_group_id,
        'sub_group_name',t.sub_group_name,
        'measurement_unit_id',t.measurement_unit_id,
        'default_measurement_unit_detail_id',t.default_measurement_unit_detail_id,
        'structure_group_name',u.structure_group_name,
        'unit_name',gmu.unit_name,
        'unit_detail_title',gmud.unit_detail_title,
        'unit_detail_display',gmud.unit_detail_display
    ))
    INTO genitemstructuregroupsub
    FROM gen_item_structure_group_sub t
    inner join gen_item_structure_group u on t.item_structure_group_id=u.item_structure_group_id
    inner join gen_measurement_unit_detail gmud  on t.default_measurement_unit_detail_id=gmud.gen_measurement_unit_detail_id
    inner join gen_measurement_unit gmu on gmu.gen_measurement_unit_id=gmud.gen_measurement_unit_id;

    ---mapped structurelist
     select jsonb_agg(jsonb_build_object('item_structure_group_id', gisg.item_structure_group_id,
                                         'structure_group_name', gisg.structure_group_name,
                                         'sub_group_name', gisgs.sub_group_name,
                                         'gen_item_structure_group_sub_id', gisgs.gen_item_structure_group_sub_id
         ))
     INTO mappingstructurelist
     from public.gen_item_structure_group gisg
              inner join public.gen_item_structure_group_sub gisgs on
         gisgs.item_structure_group_id = gisg.item_structure_group_id
              inner join public.style_master_category_structure_subgroup_mapping smcssm
                         on smcssm.gen_item_structure_group_sub_id = gisgs.gen_item_structure_group_sub_id
     where smcssm.style_master_category_id = _style_master_category_id;

     --gen unit all office
     select jsonb_agg(jsonb_build_object('unit_name', gisg.unit_name,
                                         'gen_unit_id', gisg.gen_unit_id
         ))
     INTO genunitlist
     from public.gen_unit gisg;

     -- --style_product_unit
     select jsonb_agg(jsonb_build_object('style_product_unit_id', gisg.style_product_unit_id,
                                         'style_product_unit_name', gisg.style_product_unit_name,
                                         'gen_unit_id', gisg.unit_type
         ))
     INTO styleproductunitlist
     from public.style_product_unit gisg;

      -- --style_product_size_group_details
     select jsonb_agg(jsonb_build_object('style_product_size_group_detid', gisg.style_product_size_group_detid,
                                         'style_product_size_group_id', gisg.style_product_size_group_id,
                                         'style_product_size', gisg.style_product_size
         ))
     INTO styleproductsizegroupdetailslist
     from public.style_product_size_group_details gisg
     where gisg.style_product_size_group_id=_style_product_size_group_id;

     ---sample order embellishment det
     select jsonb_agg(jsonb_build_object(
             'gen_measurement_unit_id', MSU.gen_measurement_unit_id,
             'measurement_unit_detail_id', b.measurement_unit_detail_id,
             'process_name', b.process_name,
             'tran_sample_order_embellishment_id', a.tran_sample_order_embellishment_id,
             'embellishment_id', a.embellishment_id
         ))
    INTO sampleorderembellishmentlist
     from public.tran_sample_order_embellishment a
              inner join public.gen_process_master b on
         b.gen_process_master_id = a.embellishment_id
              INNER JOIN public.gen_measurement_unit_detail MSU
                         on MSU.gen_measurement_unit_detail_id = b.measurement_unit_detail_id
     where a.tran_sample_order_id = tran_sample_order_id_filter;

      ---sample order  details
     select jsonb_agg(jsonb_build_object(
             'tran_sample_order_detl_id', tsod.tran_sample_order_detl_id,
             'tran_sample_order_id', tsod.tran_sample_order_id,
             'color_code', tsod.color_code,
             'style_product_size_group_detid', tsod.style_product_size_group_detid,
             'order_quantity', tsod.order_quantity,
            'style_product_unit_id', tsod.style_product_unit_id
         ))
    INTO sampleorderdetaillist
     from tran_sample_order_detl tsod
     where tsod.tran_sample_order_id = tran_sample_order_id_filter;

     ---sample order sub group  details
     select jsonb_agg(jsonb_build_object(
                              'item_structure_group_id',b.item_structure_group_id,
                              'gen_item_structure_group_sub_id',a.gen_item_structure_group_sub_id,
                              'sub_group_name',b.sub_group_name,
                              'tran_sample_order_subgroup_id',a.tran_sample_order_subgroup_id,
                              'measurement_unit_id',b.measurement_unit_id,
                              'default_measurement_unit_detail_id',b.default_measurement_unit_detail_id
                          ))
     INTO sampleordersubgrouplist
     from public.tran_sample_order_subgroup a
              inner join public.gen_item_structure_group_sub b on
         b.gen_item_structure_group_sub_id = a.gen_item_structure_group_sub_id

     where a.tran_sample_order_id = tran_sample_order_id_filter;

    RETURN QUERY
        SELECT
            tso.tran_sample_order_id,
            tso.tran_va_plan_detl_id,
               tso.tran_sample_order_number,
               tso.order_date,
               tso.delivery_date,
               tso.delivery_unit_id,
               tso.order_quantity,
               tso.designer_member_id,
               tso.sample_photos,
               tso.tran_va_plan_detl_style_id,
               tvpds.style_code,
               tvpds.style_quantity,
               tvpds.no_of_color,
               tvpds.color_code_gen,
               tvpds.style_item_product_sub_category_id,
               vw.style_product_size_group_name,
               vw.style_item_product_name,
               vw.style_item_type_name,
               vw.style_product_type_name,
               vw.style_item_origin_name,
               vw.style_gender_name,
               vw.master_category_name,
               vw.style_item_product_id,
               vw.style_item_type_id,
               vw.style_product_type_id,
               vw.style_item_origin_id,
               vw.style_gender_id,
               vw.style_master_category_id,
               vw.fiscal_year_id,
               vw.style_product_size_group_id,
               designerlist                     designer_list,
               mappingstructurelist             mapping_structure_List,
               genitemstructuregroupsub         gen_itemstructure_groupsub_list,
               genunitlist                      gen_unit_list,
               styleproductunitlist             style_product_unit_list,
               styleproductsizegroupdetailslist style_product_sizegroupdetails_list,
               sampleorderembellishmentlist     sample_order_embellishmentlist,
               sampleorderdetaillist            sample_order_detaillist,
               sampleordersubgrouplist          sampleorder_subgroup_list
        FROM vw_style_item_product vw
                 inner join tran_va_plan_detl tvpd on tvpd.style_item_product_id = vw.style_item_product_id
                 inner join tran_va_plan_detl_style tvpds on tvpds.tran_va_plan_detl_id = tvpd.tran_va_plan_detl_id
                 inner join tran_sample_order tso on tso.tran_va_plan_detl_style_id = tvpds.tran_va_plan_detl_style_id
        where tso.tran_sample_order_id=tran_sample_order_id_filter
          and vw.fiscal_year_id = fiscal_year_id_filter;

        -- Output the result record
       -- RAISE NOTICE 'Result Record: %', result_record;

END;
$$;

alter function proc_sp_get_sample_order_data_edit(bigint, bigint, bigint, bigint) owner to postgres;

grant execute on function proc_sp_get_sample_order_data_edit(bigint, bigint, bigint, bigint) to anon;

grant execute on function proc_sp_get_sample_order_data_edit(bigint, bigint, bigint, bigint) to authenticated;

grant execute on function proc_sp_get_sample_order_data_edit(bigint, bigint, bigint, bigint) to service_role;


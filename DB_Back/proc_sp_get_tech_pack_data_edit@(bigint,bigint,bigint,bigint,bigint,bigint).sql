create function proc_sp_get_tech_pack_data_edit(tran_tech_pack_id_filter bigint, tran_pre_costing_id_filter bigint, tran_sample_order_id_filter bigint, style_item_product_id_filter bigint, fiscal_year_id_filter bigint, team_category_id_filter bigint)
    returns TABLE(tran_designer_review_id bigint, tran_sample_order_id bigint, tran_va_plan_detl_id bigint, tran_sample_order_number text, order_date timestamp with time zone, delivery_date timestamp with time zone, delivery_unit_id bigint, order_quantity bigint, designer_member_id bigint, sample_photos json, tran_va_plan_detl_style_id bigint, style_code text, style_quantity bigint, no_of_color bigint, color_code_gen text, style_item_product_sub_category_id bigint, style_product_size_group_name text, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint, fiscal_year_id bigint, style_product_size_group_id bigint, designer_list text, mapping_structure_list text, gen_itemstructure_groupsub_list text, gen_unit_list text, style_product_unit_list text, style_product_sizegroupdetails_list text, sample_order_embellishmentlist text, sample_order_detaillist text, sampleorder_subgroup_list text, color_detl_size_list text, team_member_marketing_name text, gen_process_master_list text, pre_costing_date timestamp with time zone, total_raw_material_cost numeric, total_raw_material_percentage numeric, factory_overhead_cost numeric, sales_marketing_distribution_cost numeric, depreciation_amortization_cost numeric, total_overhead_cost numeric, total_production_cost numeric, floor_price_percentage numeric, floor_price_per_pc numeric, desired_markup_percentage numeric, estimated_markup_price numeric, desired_markup_price numeric, final_mrp numeric, total_style_quantity_mrp numeric, suggested_mrp_with_cost numeric, smv text, remarks text, precosting_colorwisesize_quantity text, pre_costing_quantity bigint, pre_costing_detail_list text, pre_costing_embellishment_list text, pre_costing_subcontract_list text, style_color_details text, style_embellishment_ids text, style_color_size_details text, gen_garment_partlist text, gen_process_master_detail_list text, techpack_number text, techpack_date timestamp with time zone, costing_smv numeric, teckpack_style_code text, aop_style text, merchandiser_id bigint, production_availability_path text, vat text, photoshoot text, e_com text, sample_ok text, follow_style text, need_production_approval text, iron text, fabric_allocation text, additional_comments text, photos json, size_details text, color_wise_size_quantity text, tech_pack_costing_quantity bigint, tran_techpack_embellishmentinfo_list text, is_ack bigint, ack_date timestamp with time zone)
    language plpgsql
as
$$
DECLARE
    _style_item_product_id            bigint;
    _style_item_type_id               bigint;
    _style_product_type_id            bigint;
    _style_item_origin_id             bigint;
    _style_gender_id                  bigint;
    _style_master_category_id         bigint;
    _style_product_size_group_id      bigint;
    designerlist                      text;
    mappingstructurelist              text;
    genitemstructuregroupsub          text;
    genunitlist                       text;
    styleproductunitlist              text;
    styleproductsizegroupdetailslist  text;
    sampleorderembellishmentlist      text;
    sampleorderdetaillist             text;
    sampleordersubgrouplist           text;
    genprocessmasterlist              text;
    precostingitemdetaildetlist       text;
    precostingembellishmentlist       text;
    precostingsubcontractlist         text;
    gengarmentpartlist                text;
    genprocessmasterdetaillist        text;
    trantechpackembellishmentinfolist text;
BEGIN

    SELECT p.style_item_product_id,
           p.style_item_type_id,
           p.style_product_type_id,
           p.style_item_origin_id,
           p.style_gender_id,
           p.style_master_category_id,
           p.style_product_size_group_id
    INTO _style_item_product_id,_style_item_type_id,_style_product_type_id,_style_item_origin_id,
        _style_gender_id,_style_master_category_id,_style_product_size_group_id

    FROM vw_style_item_product p
    where p.style_item_product_id = style_item_product_id_filter
      and p.fiscal_year_id = fiscal_year_id_filter;

    --gen techpack_embellishment_det
    select jsonb_agg(jsonb_build_object('tran_tech_pack_embellishment_info_id',
                                        gisg.tran_tech_pack_embellishment_info_id,
                                        'tran_tech_pack_id', gisg.tran_tech_pack_id,
                                        'gen_process_master_id', gisg.gen_process_master_id,
                                        'supplier_id', gisg.supplier_id,
                                        'is_garment_form', gisg.is_garment_form,
                                        'embellishment_details', gisg.embellishment_details::text,
                                        'supplier_info', gisg.supplier_info::text
        ))
    INTO trantechpackembellishmentinfolist
    from public.tran_tech_pack_embellishment_info gisg
    where tran_tech_pack_id = tran_tech_pack_id_filter;

    --gen process masterdetails
    select jsonb_agg(jsonb_build_object('gen_process_master_detail_id', gisg.gen_process_master_detail_id,
                                        'gen_process_master_id', gisg.gen_process_master_id,
                                        'sub_process_name', gisg.sub_process_name
        ))
    INTO genprocessmasterdetaillist
    from public.gen_process_master_detail gisg;

    --gen garment
    select jsonb_agg(jsonb_build_object('gen_garment_part_id', gisg.gen_garment_part_id,
                                        'garment_part_name', gisg.garment_part_name,
                                        'short_code', gisg.short_code,
                                        'multiplier', gisg.multiplier
        ))
    INTO gengarmentpartlist
    from public.gen_garment_part gisg;

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

    ---sample order sub group  details
    select jsonb_agg(jsonb_build_object(
            'item_structure_group_id', b.item_structure_group_id,
            'gen_item_structure_group_sub_id', a.gen_item_structure_group_sub_id,
            'sub_group_name', b.sub_group_name,
            'tran_sample_order_subgroup_id', a.tran_sample_order_subgroup_id,
            'measurement_unit_id', b.measurement_unit_id,
            'default_measurement_unit_detail_id', b.default_measurement_unit_detail_id
        ))
    INTO sampleordersubgrouplist
    from public.tran_sample_order_subgroup a
             inner join public.gen_item_structure_group_sub b on
        b.gen_item_structure_group_sub_id = a.gen_item_structure_group_sub_id

    where a.tran_sample_order_id = tran_sample_order_id_filter;

    --genitemstructuregroupsub list
    SELECT jsonb_agg(jsonb_build_object(
            'gen_item_structure_group_sub_id', t.gen_item_structure_group_sub_id,
            'item_structure_group_id', t.item_structure_group_id,
            'sub_group_name', t.sub_group_name,
            'measurement_unit_id', t.measurement_unit_id,
            'default_measurement_unit_detail_id', t.default_measurement_unit_detail_id,
            'structure_group_name', u.structure_group_name,
            'unit_name', gmu.unit_name,
            'unit_detail_title', gmud.unit_detail_title,
            'unit_detail_display', gmud.unit_detail_display
        ))
    INTO genitemstructuregroupsub
    FROM gen_item_structure_group_sub t
             inner join gen_item_structure_group u on t.item_structure_group_id = u.item_structure_group_id
             inner join gen_measurement_unit_detail gmud
                        on t.default_measurement_unit_detail_id = gmud.gen_measurement_unit_detail_id
             inner join gen_measurement_unit gmu on gmu.gen_measurement_unit_id = gmud.gen_measurement_unit_id;

    --gen unit all office
    select jsonb_agg(jsonb_build_object('unit_name', gisg.unit_name,
                                        'gen_unit_id', gisg.gen_unit_id
        ))
    INTO genunitlist
    from public.gen_unit gisg;

    --gen process master
    select jsonb_agg(jsonb_build_object('process_name', gisg.process_name,
                                        'gen_process_master_id', gisg.gen_process_master_id
        ))
    INTO genprocessmasterlist
    from public.gen_process_master gisg;

    -- --style_product_size_group_details
    select jsonb_agg(jsonb_build_object('style_product_size_group_detid', gisg.style_product_size_group_detid,
                                        'style_product_size_group_id', gisg.style_product_size_group_id,
                                        'style_product_size', gisg.style_product_size
        ))
    INTO styleproductsizegroupdetailslist
    from public.style_product_size_group_details gisg
    where gisg.style_product_size_group_id = _style_product_size_group_id;

    --style_product_unit
    select jsonb_agg(jsonb_build_object('style_product_unit_id', gisg.style_product_unit_id,
                                        'style_product_unit_name', gisg.style_product_unit_name,
                                        'gen_unit_id', gisg.unit_type
        ))
    INTO styleproductunitlist
    from public.style_product_unit gisg;

    -- designerlist
    SELECT jsonb_agg(jsonb_build_object(
            'gen_team_members_id', t.gen_team_members_id,
            'team_member_marketing_name', t.team_member_marketing_name,
            'team_member_marketing_code', t.team_member_marketing_code,
            'team_member_designation', t.team_member_designation,
            'phone_number', t.phone_number,
            'email', t.email,
            'photo', t.photo,
            'user_id', t.user_id
        ))
    INTO designerlist
    FROM gen_team_members t
    WHERE t.gen_team_group_id = team_category_id_filter;

    ---sample order embellishment det
    select jsonb_agg(jsonb_build_object(
            'gen_measurement_unit_id', MSU.gen_measurement_unit_id,
            'measurement_unit_detail_id', b.measurement_unit_detail_id,
            'style_embellishment_name', b.process_name,
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

    ---sample order  details subcontract
    select jsonb_agg(jsonb_build_object(
            'tran_pre_costing_item_subcontract_detail_id', tsod.tran_pre_costing_item_subcontract_detail_id,
            'tran_pre_costing_id', tsod.tran_pre_costing_id,
            'process_master_id', tsod.process_master_id,
            'measurement_unit_detail_id', tsod.measurement_unit_detail_id,
            'order_quantity', tsod.order_quantity,
            'wastage', tsod.wastage,
            'total_order_quantity', tsod.total_order_quantity,
            'price_per_unit', tsod.price_per_unit,
            'total_price', tsod.total_price,
            'CurrentState', 2
        ))
    INTO precostingsubcontractlist
    from tran_pre_costing_item_subcontract_detail tsod
    where tsod.tran_pre_costing_id = tran_pre_costing_id_filter;

    ---sample order  details embellishment
    select jsonb_agg(jsonb_build_object(
            'tran_sample_order_id', tsod.tran_pre_costing_item_embellishment_detail_id,
            'tran_pre_costing_id', tsod.tran_pre_costing_id,
            'style_embellishment_id', tsod.style_embellishment_id,
            'measurement_unit_detail_id', tsod.measurement_unit_detail_id,
            'order_quantity', tsod.order_quantity,
            'wastage', tsod.wastage,
            'total_order_quantity', tsod.total_order_quantity,
            'price_per_unit', tsod.price_per_unit,
            'total_price', tsod.total_price,
            'CurrentState', 2
        ))
    INTO precostingembellishmentlist
    from tran_pre_costing_item_embellishment_detail tsod
    where tsod.tran_pre_costing_id = tran_pre_costing_id_filter;

    --pre costing det
    select jsonb_agg(jsonb_build_object(
            'tran_pre_costing_item_detail_id', tpcd.tran_pre_costing_item_detail_id,
            'tran_pre_costing_id', tpcd.tran_pre_costing_id,
            'gen_item_structure_group_sub_id', tpcd.gen_item_structure_group_sub_id,
            'segment_det1_id', tpcd.segment_det1_id,
            'segment_det2_id', tpcd.segment_det2_id,
            'segment_det3_id', tpcd.segment_det3_id,
            'segment_det4_id', tpcd.segment_det4_id,
            'segment_det5_id', tpcd.segment_det5_id,
            'segment_det6_id', tpcd.segment_det6_id,
            'segment_det7_id', tpcd.segment_det7_id,
            'segment_det8_id', tpcd.segment_det8_id,
            'segment_det9_id', tpcd.segment_det9_id,
            'segment_det10_id', tpcd.segment_det10_id,
            'segment_det11_id', tpcd.segment_det11_id,
            'segment_det12_id', tpcd.segment_det12_id,
            'segment_det13_id', tpcd.segment_det13_id,
            'segment_det14_id', tpcd.segment_det14_id,
            'segment_det15_id', tpcd.segment_det15_id,
            'segment_det1_', tpcd.segment_det1_text,
            'segment_det2_', tpcd.segment_det2_text,
            'segment_det3_', tpcd.segment_det3_text,
            'segment_det4_', tpcd.segment_det4_text,
            'segment_det5_', tpcd.segment_det5_text,
            'segment_det6_', tpcd.segment_det6_text,
            'segment_det7_', tpcd.segment_det7_text,
            'segment_det8_', tpcd.segment_det8_text,
            'segment_det9_', tpcd.segment_det9_text,
            'segment_det10_', tpcd.segment_det10_text,
            'segment_det11_', tpcd.segment_det11_text,
            'segment_det12_', tpcd.segment_det12_text,
            'segment_det13_', tpcd.segment_det13_text,
            'segment_det14_', tpcd.segment_det14_text,
            'segment_det15_', tpcd.segment_det15_text,
            'all_segment_text', CONCAT_WS(' ', COALESCE(tpcd.segment_det1_text, ''),
                                          COALESCE(tpcd.segment_det2_text, ''),
                                          COALESCE(tpcd.segment_det3_text, ''),
                                          COALESCE(tpcd.segment_det4_text, ''),
                                          COALESCE(tpcd.segment_det5_text, ''),
                                          COALESCE(tpcd.segment_det6_text, ''),
                                          COALESCE(tpcd.segment_det7_text, ''),
                                          COALESCE(tpcd.segment_det8_text, ''),
                                          COALESCE(tpcd.segment_det9_text, ''),
                                          COALESCE(tpcd.segment_det10_text, ''),
                                          COALESCE(tpcd.segment_det11_text, ''),
                                          COALESCE(tpcd.segment_det12_text, ''),
                                          COALESCE(tpcd.segment_det13_text, ''),
                                          COALESCE(tpcd.segment_det14_text, ''),
                                          COALESCE(tpcd.segment_det15_text, '')),
            'measurement_unit_detail_id', tpcd.measurement_unit_detail_id,
            'order_quantity', tpcd.order_quantity,
            'wastage', tpcd.wastage,
            'total_order_quantity', tpcd.total_order_quantity,
            'price_per_unit', tpcd.price_per_unit,
            'total_price', tpcd.total_price,
            'measurement_unit', det.unit_detail_title,
            'item_structure_group_id', gisgs.item_structure_group_id,
            'CurrentState', 2
        ))
    INTO precostingitemdetaildetlist
    from public.tran_pre_costing_item_detail tpcd
             inner join public.gen_measurement_unit_detail det
                        on tpcd.measurement_unit_detail_id = det.gen_measurement_unit_detail_id
             inner join public.gen_item_structure_group_sub gisgs
                        on gisgs.gen_item_structure_group_sub_id = tpcd.gen_item_structure_group_sub_id

    where tpcd.tran_pre_costing_id = tran_pre_costing_id_filter;

    RETURN QUERY
        SELECT
            gs.season_name, gfy.year_name,

            tdr.tran_designer_review_id,

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

               designerlist                                                                         designer_list,
               mappingstructurelist                                                                 mapping_structure_List,
               genitemstructuregroupsub                                                             gen_itemstructure_groupsub_list,
               genunitlist                                                                          gen_unit_list,
               styleproductunitlist                                                                 style_product_unit_list,
               styleproductsizegroupdetailslist                                                     style_product_sizegroupdetails_list,
               sampleorderembellishmentlist                                                         sample_order_embellishmentlist,
               sampleorderdetaillist                                                                sample_order_detaillist,
               sampleordersubgrouplist                                                              sampleorder_subgroup_list,

               sp_get_color_detl_size_by_vaplandetlid(tvpds.tran_va_plan_detl_id)::text
                                                                                                 as color_detl_size_List,
               gtm.team_member_marketing_name,
               genprocessmasterlist                                                                 gen_process_master_list,


               tpc.pre_costing_date,
               tpc.total_raw_material_cost,
               tpc.total_raw_material_percentage,
               tpc.factory_overhead_cost,
               tpc.sales_marketing_distribution_cost,
               tpc.depreciation_amortization_cost,
               tpc.total_overhead_cost,
               tpc.total_production_cost,
               tpc.floor_price_percentage,
               tpc.floor_price_per_pc,
               tpc.desired_markup_percentage,
               tpc.estimated_markup_price,
               tpc.desired_markup_price,
               tpc.final_mrp,
               tpc.total_style_quantity_mrp,
               tpc.suggested_mrp_with_cost,
               tpc.smv,
               tpc.remarks,
               tpc.color_wise_size_quantity::text                                                as precosting_colorwisesize_quantity,
               tpc.pre_costing_quantity,

               precostingitemdetaildetlist                                                          pre_costing_detail_list,
               precostingembellishmentlist                                                          pre_costing_embellishment_list,
               precostingsubcontractlist                                                            pre_costing_subcontract_list,


               tvpds.style_color_details::text,
               tvpds.style_embellishment_ids::text,
               (select jsonb_agg(jsonb_build_object(
                       'tran_va_plan_detl_style_color_id', color.tran_va_plan_detl_style_color_id,
                       'tran_va_plan_detl_style_id', color.tran_va_plan_detl_style_id,
                       'color_code', color.color_code,
                       'style_color_quantity', color.style_color_quantity,
                       'style_color_size_details',
                       (select jsonb_agg(jsonb_build_object('tran_va_plan_detl_style_color_size_id',
                                                            colro_size.tran_va_plan_detl_style_color_size_id,
                                                            'tran_va_plan_detl_style_color_id',
                                                            colro_size.tran_va_plan_detl_style_color_id,
                                                            'style_product_size_group_detid',
                                                            colro_size.style_product_size_group_detid,
                                                            'style_color_size_quantity',
                                                            colro_size.style_color_size_quantity
                           ))

                        from public.tran_va_plan_detl_style_color_size colro_size
                        where colro_size.tran_va_plan_detl_style_color_id = color.tran_va_plan_detl_style_color_id)
                   ))
                from public.tran_va_plan_detl_style_color color
                where color.tran_va_plan_detl_style_id = tvpds.tran_va_plan_detl_style_id)::text as style_color_size_details,
               gengarmentpartlist                                                                   gen_garment_partlist,
               genprocessmasterdetaillist                                                           gen_process_master_detail_list,

               ttp.techpack_number,
               ttp.techpack_date,
               ttp.costing_smv,
               ttp.teckpack_style_code,
               ttp.aop_style,
               ttp.merchandiser_id,
               ttp.production_availability_path,
               ttp.vat,
               ttp.photoshoot,
               ttp.e_com,
               ttp.sample_ok,
               ttp.follow_style,
               ttp.need_production_approval,
               ttp.iron,
               ttp.fabric_allocation,
               ttp.additional_comments,
               ttp.photos,
               ttp.size_details::text,
               ttp.color_wise_size_quantity::text,
               ttp.tech_pack_costing_quantity,
               trantechpackembellishmentinfolist                                                    tran_techpack_embellishmentinfo_list,
               ttp.is_ack, ttp.ack_date:: timestamp with time zone


        FROM vw_style_item_product vw
                 inner join tran_va_plan_detl tvpd on tvpd.style_item_product_id = vw.style_item_product_id
                 inner join tran_va_plan_detl_style tvpds on tvpds.tran_va_plan_detl_id = tvpd.tran_va_plan_detl_id
                 inner join tran_sample_order tso on tso.tran_va_plan_detl_style_id = tvpds.tran_va_plan_detl_style_id
                 inner join gen_team_members gtm on gtm.gen_team_members_id = tvpds.designer_member_id
                 inner join tran_pre_costing tpc on tpc.tran_sample_order_id = tso.tran_sample_order_id
                 inner join tran_designer_review tdr on tdr.tran_pre_costing_id = tpc.tran_pre_costing_id
                 inner join tran_tech_pack ttp on ttp.tran_designer_review_id = tdr.tran_designer_review_id
                 inner join tran_va_plan_events tvpe on tvpd.tran_va_plan_event_id = tvpe.tran_va_plan_event_id
                 inner join tran_range_plan_events trpe on trpe.tran_range_plan_event_id = tvpe.tran_range_plan_event_id
                 inner join tran_bp_event tbp on tbp.tran_bp_event_id = trpe.tran_bp_event_id
                 inner join gen_season_event_config gsec on gsec.event_id = tbp.event_id
                inner join gen_season gs on gs.season_id=gsec.season_id
                inner join gen_fiscal_year gfy on gsec.fiscal_year_id = gfy.fiscal_year_id
        where tpc.tran_pre_costing_id = tran_pre_costing_id_filter
          and vw.fiscal_year_id = fiscal_year_id_filter;

    -- Output the result record
    -- RAISE NOTICE 'Result Record: %', result_record;

END;
$$;

alter function proc_sp_get_tech_pack_data_edit(bigint, bigint, bigint, bigint, bigint, bigint) owner to postgres;

grant execute on function proc_sp_get_tech_pack_data_edit(bigint, bigint, bigint, bigint, bigint, bigint) to anon;

grant execute on function proc_sp_get_tech_pack_data_edit(bigint, bigint, bigint, bigint, bigint, bigint) to authenticated;

grant execute on function proc_sp_get_tech_pack_data_edit(bigint, bigint, bigint, bigint, bigint, bigint) to service_role;


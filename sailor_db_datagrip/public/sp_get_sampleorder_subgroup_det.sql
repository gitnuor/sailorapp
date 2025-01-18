create function sp_get_sampleorder_subgroup_det(tran_sample_order_id_filter bigint)
    returns TABLE(item_structure_group_id bigint, gen_item_structure_group_sub_id bigint, sub_group_name text, tran_sample_order_subgroup_id bigint, measurement_unit_id bigint, default_measurement_unit_detail_id bigint)
    language plpgsql
as
$$

 Begin
 RETURN QUERY
 select b.item_structure_group_id,
 a.gen_item_structure_group_sub_id,b.sub_group_name,
 a.tran_sample_order_subgroup_id,
b.measurement_unit_id,b.default_measurement_unit_detail_id
 from public.tran_sample_order_subgroup a
inner join public.gen_item_structure_group_sub b on
b.gen_item_structure_group_sub_id=a.gen_item_structure_group_sub_id
where a.tran_sample_order_id=tran_sample_order_id_filter;

END;
$$;

alter function sp_get_sampleorder_subgroup_det(bigint) owner to postgres;

grant execute on function sp_get_sampleorder_subgroup_det(bigint) to anon;

grant execute on function sp_get_sampleorder_subgroup_det(bigint) to authenticated;

grant execute on function sp_get_sampleorder_subgroup_det(bigint) to service_role;


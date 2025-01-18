create function sp_get_mapped_segment_by_gen_item_structure_group_sub_id(gen_item_structure_group_sub_id_filter bigint)
    returns TABLE(gen_item_structure_group_sub_segment_mapping_id bigint, gen_item_structure_group_sub_id bigint, gen_segment_id bigint, gen_segment_name text, sub_group_name text)
    language plpgsql
as
$$
DECLARE  
 
 Begin

		RETURN QUERY 
		SELECT 
detl.gen_item_structure_group_sub_segment_mapping_id,
detl.gen_item_structure_group_sub_id,
detl.gen_segment_id,
gs.gen_segment_name, gss.sub_group_name
FROM public.gen_item_structure_group_sub_segment_mapping detl
inner join public.gen_item_structure_group_sub gss on gss.gen_item_structure_group_sub_id=
detl.gen_item_structure_group_sub_id
inner join public.gen_segment gs on gs.gen_segment_id=detl.gen_segment_id
where detl.gen_item_structure_group_sub_id=gen_item_structure_group_sub_id_filter;


		
END;
$$;

alter function sp_get_mapped_segment_by_gen_item_structure_group_sub_id(bigint) owner to postgres;

grant execute on function sp_get_mapped_segment_by_gen_item_structure_group_sub_id(bigint) to anon;

grant execute on function sp_get_mapped_segment_by_gen_item_structure_group_sub_id(bigint) to authenticated;

grant execute on function sp_get_mapped_segment_by_gen_item_structure_group_sub_id(bigint) to service_role;


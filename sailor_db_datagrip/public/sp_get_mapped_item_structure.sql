create function sp_get_mapped_item_structure(style_master_category_id_filter bigint)
    returns TABLE(item_structure_group_id bigint, structure_group_name text, sub_group_name text, gen_item_structure_group_sub_id bigint)
    language plpgsql
as
$$
DECLARE  
 
 Begin

		RETURN QUERY 
		select 
gisg.item_structure_group_id,
gisg.structure_group_name,
gisgs.sub_group_name,
gisgs.gen_item_structure_group_sub_id
from public.gen_item_structure_group gisg
inner join public.gen_item_structure_group_sub gisgs on
gisgs.item_structure_group_id=gisg.item_structure_group_id
inner join public.style_master_category_structure_subgroup_mapping smcssm
on smcssm.gen_item_structure_group_sub_id =gisgs.gen_item_structure_group_sub_id
where smcssm.style_master_category_id=style_master_category_id_filter;

		
		
		
END;
$$;

alter function sp_get_mapped_item_structure(bigint) owner to postgres;

grant execute on function sp_get_mapped_item_structure(bigint) to anon;

grant execute on function sp_get_mapped_item_structure(bigint) to authenticated;

grant execute on function sp_get_mapped_item_structure(bigint) to service_role;


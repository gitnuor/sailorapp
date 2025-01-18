create function sp_get_sampleorder_embellishment_det(tran_sample_order_id_filter bigint)
    returns TABLE(measurement_unit_id bigint, default_measurement_unit_detail_id bigint, style_embellishment_name text, tran_sample_order_embellishment_id bigint, embellishment_id bigint)
    language plpgsql
as
$$

 Begin
 RETURN QUERY
 select 
MSU.gen_measurement_unit_id measurement_unit_id,
 
 b.measurement_unit_detail_id default_measurement_unit_detail_id,
 b.process_name style_embellishment_name,
 a.tran_sample_order_embellishment_id,
 a.embellishment_id
 from public.tran_sample_order_embellishment a
inner join public.gen_process_master b on
b.gen_process_master_id=a.embellishment_id
INNER JOIN public.gen_measurement_unit_detail MSU on MSU.gen_measurement_unit_detail_id= b.measurement_unit_detail_id
where a.tran_sample_order_id=tran_sample_order_id_filter;

END;
$$;

alter function sp_get_sampleorder_embellishment_det(bigint) owner to postgres;

grant execute on function sp_get_sampleorder_embellishment_det(bigint) to anon;

grant execute on function sp_get_sampleorder_embellishment_det(bigint) to authenticated;

grant execute on function sp_get_sampleorder_embellishment_det(bigint) to service_role;


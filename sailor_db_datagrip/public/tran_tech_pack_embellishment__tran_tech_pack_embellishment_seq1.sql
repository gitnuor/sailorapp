create sequence tran_tech_pack_embellishment__tran_tech_pack_embellishment_seq1;

alter sequence tran_tech_pack_embellishment__tran_tech_pack_embellishment_seq1 owner to postgres;

alter sequence tran_tech_pack_embellishment__tran_tech_pack_embellishment_seq1 owned by tran_tech_pack_embellishment_det.tran_tech_pack_embellishment_det_id;

grant select, update, usage on sequence tran_tech_pack_embellishment__tran_tech_pack_embellishment_seq1 to anon;

grant select, update, usage on sequence tran_tech_pack_embellishment__tran_tech_pack_embellishment_seq1 to authenticated;

grant select, update, usage on sequence tran_tech_pack_embellishment__tran_tech_pack_embellishment_seq1 to service_role;


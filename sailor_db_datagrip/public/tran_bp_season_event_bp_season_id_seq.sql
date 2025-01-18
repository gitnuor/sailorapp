create sequence tran_bp_season_event_bp_season_id_seq;

alter sequence tran_bp_season_event_bp_season_id_seq owner to postgres;

alter sequence tran_bp_season_event_bp_season_id_seq owned by tran_bp_event.tran_bp_event_id;

grant select, update, usage on sequence tran_bp_season_event_bp_season_id_seq to anon;

grant select, update, usage on sequence tran_bp_season_event_bp_season_id_seq to authenticated;

grant select, update, usage on sequence tran_bp_season_event_bp_season_id_seq to service_role;


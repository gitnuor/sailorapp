create sequence tran_bp_season_month_outlet_bp_season_month_outlet_id_seq;

alter sequence tran_bp_season_month_outlet_bp_season_month_outlet_id_seq owner to postgres;

alter sequence tran_bp_season_month_outlet_bp_season_month_outlet_id_seq owned by tran_bp_event_month_outlet.tran_bp_event_month_outlet_id;

grant select, update, usage on sequence tran_bp_season_month_outlet_bp_season_month_outlet_id_seq to anon;

grant select, update, usage on sequence tran_bp_season_month_outlet_bp_season_month_outlet_id_seq to authenticated;

grant select, update, usage on sequence tran_bp_season_month_outlet_bp_season_month_outlet_id_seq to service_role;


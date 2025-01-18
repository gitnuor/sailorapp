create sequence tbl_season_month_config_id_seq;

alter sequence tbl_season_month_config_id_seq owner to postgres;

alter sequence tbl_season_month_config_id_seq owned by gen_season_event_config.event_id;

grant select, update, usage on sequence tbl_season_month_config_id_seq to anon;

grant select, update, usage on sequence tbl_season_month_config_id_seq to authenticated;

grant select, update, usage on sequence tbl_season_month_config_id_seq to service_role;


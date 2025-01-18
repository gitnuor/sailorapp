create sequence tran_va_plan_events_tran_va_plan_events_id_seq
    maxvalue 2147483647;

alter sequence tran_va_plan_events_tran_va_plan_events_id_seq owner to postgres;

alter sequence tran_va_plan_events_tran_va_plan_events_id_seq owned by tran_va_plan_events.tran_va_plan_event_id;

grant select, update, usage on sequence tran_va_plan_events_tran_va_plan_events_id_seq to anon;

grant select, update, usage on sequence tran_va_plan_events_tran_va_plan_events_id_seq to authenticated;

grant select, update, usage on sequence tran_va_plan_events_tran_va_plan_events_id_seq to service_role;


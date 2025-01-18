create function generate_serial_number() returns trigger
    language plpgsql
as
$$
BEGIN
    NEW.tran_sample_order_number := 'SO' || LPAD(NEW.tran_sample_order_id::TEXT, 10, '0');
    RETURN NEW;
END;
$$;

alter function generate_serial_number() owner to postgres;

grant execute on function generate_serial_number() to anon;

grant execute on function generate_serial_number() to authenticated;

grant execute on function generate_serial_number() to service_role;


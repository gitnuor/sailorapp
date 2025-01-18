create function generate_techpack_serial_number() returns trigger
    language plpgsql
as
$$
BEGIN
    NEW.techpack_number := 'TP' || LPAD(NEW.tran_techpack_id::TEXT, 10, '0');
    RETURN NEW;
END;
$$;

alter function generate_techpack_serial_number() owner to postgres;

grant execute on function generate_techpack_serial_number() to anon;

grant execute on function generate_techpack_serial_number() to authenticated;

grant execute on function generate_techpack_serial_number() to service_role;


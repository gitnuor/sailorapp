

create function proc_get_supplier_new()
    returns TABLE
            (
                gen_contact_category_list      text,
                gen_geographical_location_list text,

                gencountry_list                text,
                genbank_list                   text,
                genbankbranch_list             text,

                genincoterm_list               text,
                genpaymentterm_list            text,
                genpaymentmethod_list          text,
                genmodeofransaction_list       text,
                genitemstructuregroupsub_list  text

            )
    language plpgsql
as
$$
DECLARE
    gencontactcategorylist       text;
    gengeographicallocationlist  text;
    gencountrylist               text;
    genbanklist                  text;
    genbankbranchlist            text;
    genincotermlist              text;
    genpaymenttermlist           text;
    genpaymentmethodlist         text;
    genmodeofransactionlist      text;
    genitemstructuregroupsublist text;

BEGIN

    select jsonb_agg(jsonb_build_object('contact_category', gisg.contact_category,
                                        'gen_contact_category_id', gisg.gen_contact_category_id
        ))
    INTO gencontactcategorylist
    from public.gen_contact_category gisg;

    select jsonb_agg(jsonb_build_object('location_title', gisg.location_title,
                                        'gen_geographical_location_id', gisg.gen_geographical_location_id
        ))
    INTO gengeographicallocationlist
    from public.gen_geographical_location gisg;

    select jsonb_agg(jsonb_build_object('country_name', gisg.country_name,
                                        'gen_country_id', gisg.gen_country_id
        ))
    INTO gencountrylist
    from public.gen_country gisg;

    select jsonb_agg(jsonb_build_object('bank_name', gisg.bank_name,
                                        'gen_bank_id', gisg.gen_bank_id
        ))
    INTO genbanklist
    from public.gen_bank gisg;

    select jsonb_agg(jsonb_build_object('branch_name', gisg.branch_name,
                                        'gen_bank_branch_id', gisg.gen_bank_branch_id
        ))
    INTO genbankbranchlist
    from public.gen_bank_branch gisg;

    select jsonb_agg(jsonb_build_object('inco_term', gisg.inco_term,
                                        'gen_inco_term_id', gisg.gen_inco_term_id
        ))
    INTO genincotermlist
    from public.gen_inco_term gisg;

    select jsonb_agg(jsonb_build_object('payment_term', gisg.payment_term,
                                        'gen_payment_term_id', gisg.gen_payment_term_id
        ))
    INTO genpaymenttermlist
    from public.gen_payment_term gisg;

    select jsonb_agg(jsonb_build_object('payment_method', gisg.payment_method,
                                        'gen_payment_method_id', gisg.gen_payment_method_id
        ))
    INTO genpaymentmethodlist
    from public.gen_payment_method gisg;

    select jsonb_agg(jsonb_build_object('mode_of_transaction', gisg.mode_of_transaction,
                                        'gen_mode_of_transaction_id', gisg.gen_mode_of_transaction_id
        ))
    INTO genmodeofransactionlist
    from public.gen_mode_of_transaction gisg;

    select jsonb_agg(jsonb_build_object('mode_of_transaction', gisg.sub_group_name,
                                        'gen_mode_of_transaction_id', gisg.gen_item_structure_group_sub_id,
                                        'item_structure_group_id', gisg.item_structure_group_id,
                                        'measurement_unit_id', gisg.measurement_unit_id,
                                        'default_measurement_unit_detail_id', gisg.default_measurement_unit_detail_id
        ))
    INTO genitemstructuregroupsublist
    from public.gen_item_structure_group_sub gisg;


    RETURN QUERY
        SELECT gencontactcategorylist       gen_contact_category_list,
               gengeographicallocationlist  gen_geographical_location_list,
               gencountrylist               gencountry_list,
               genbanklist                  genbank_list,
               genbankbranchlist            genbankbranch_list,
               genincotermlist              genincoterm_list,

               genpaymenttermlist           genpaymentterm_list,
               genpaymentmethodlist         genpaymentmethod_list,
               genmodeofransactionlist      genmodeofransaction_list,
               genitemstructuregroupsublist genitemstructuregroupsub_list;

END;
$$;



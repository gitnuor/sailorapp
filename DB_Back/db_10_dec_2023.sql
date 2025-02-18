PGDMP              
        {            postgres     15.1 (Ubuntu 15.1-1.pgdg20.04+1)    16.0 R   �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    5    postgres    DATABASE     p   CREATE DATABASE postgres WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'C.UTF-8';
    DROP DATABASE postgres;
                postgres    false            �           0    0    DATABASE postgres    COMMENT     N   COMMENT ON DATABASE postgres IS 'default administrative connection database';
                   postgres    false    4546            �           0    0    postgres    DATABASE PROPERTIES     �   ALTER DATABASE postgres SET "app.settings.jwt_secret" TO 'WUbXShBL/HjCcXAYpoNArm/O5nL9MiY4VcfNDoaL04btGfSd2IHHgWAZDut+i11jrWRtHxyoiE53s1bmgd4AVg==';
ALTER DATABASE postgres SET "app.settings.jwt_exp" TO '3600';
                     postgres    false                        2615    16488    auth    SCHEMA        CREATE SCHEMA auth;
    DROP SCHEMA auth;
                supabase_admin    false                        2615    16387 
   extensions    SCHEMA        CREATE SCHEMA extensions;
    DROP SCHEMA extensions;
                postgres    false                        2615    16618    graphql    SCHEMA        CREATE SCHEMA graphql;
    DROP SCHEMA graphql;
                supabase_admin    false                        2615    16607    graphql_public    SCHEMA        CREATE SCHEMA graphql_public;
    DROP SCHEMA graphql_public;
                supabase_admin    false                        2615    16385 	   pgbouncer    SCHEMA        CREATE SCHEMA pgbouncer;
    DROP SCHEMA pgbouncer;
             	   pgbouncer    false                        2615    16645    pgsodium    SCHEMA        CREATE SCHEMA pgsodium;
    DROP SCHEMA pgsodium;
                supabase_admin    false                        3079    16646    pgsodium 	   EXTENSION     >   CREATE EXTENSION IF NOT EXISTS pgsodium WITH SCHEMA pgsodium;
    DROP EXTENSION pgsodium;
                   false    18            �           0    0    EXTENSION pgsodium    COMMENT     \   COMMENT ON EXTENSION pgsodium IS 'Pgsodium is a modern cryptography library for Postgres.';
                        false    2                        2615    16599    realtime    SCHEMA        CREATE SCHEMA realtime;
    DROP SCHEMA realtime;
                supabase_admin    false                        2615    16536    storage    SCHEMA        CREATE SCHEMA storage;
    DROP SCHEMA storage;
                supabase_admin    false                        2615    16949    vault    SCHEMA        CREATE SCHEMA vault;
    DROP SCHEMA vault;
                supabase_admin    false                        3079    29799 
   pg_graphql 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS pg_graphql WITH SCHEMA graphql;
    DROP EXTENSION pg_graphql;
                   false    22            �           0    0    EXTENSION pg_graphql    COMMENT     B   COMMENT ON EXTENSION pg_graphql IS 'pg_graphql: GraphQL support';
                        false    4                        3079    29810    pg_stat_statements 	   EXTENSION     J   CREATE EXTENSION IF NOT EXISTS pg_stat_statements WITH SCHEMA extensions;
 #   DROP EXTENSION pg_stat_statements;
                   false    14            �           0    0    EXTENSION pg_stat_statements    COMMENT     u   COMMENT ON EXTENSION pg_stat_statements IS 'track planning and execution statistics of all SQL statements executed';
                        false    5                        3079    29841    pgcrypto 	   EXTENSION     @   CREATE EXTENSION IF NOT EXISTS pgcrypto WITH SCHEMA extensions;
    DROP EXTENSION pgcrypto;
                   false    14            �           0    0    EXTENSION pgcrypto    COMMENT     <   COMMENT ON EXTENSION pgcrypto IS 'cryptographic functions';
                        false    6                        3079    29878    pgjwt 	   EXTENSION     =   CREATE EXTENSION IF NOT EXISTS pgjwt WITH SCHEMA extensions;
    DROP EXTENSION pgjwt;
                   false    6    14            �           0    0    EXTENSION pgjwt    COMMENT     C   COMMENT ON EXTENSION pgjwt IS 'JSON Web Token API for Postgresql';
                        false    7                        3079    16950    supabase_vault 	   EXTENSION     A   CREATE EXTENSION IF NOT EXISTS supabase_vault WITH SCHEMA vault;
    DROP EXTENSION supabase_vault;
                   false    2    20            �           0    0    EXTENSION supabase_vault    COMMENT     C   COMMENT ON EXTENSION supabase_vault IS 'Supabase Vault Extension';
                        false    3                        3079    29885 	   uuid-ossp 	   EXTENSION     C   CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA extensions;
    DROP EXTENSION "uuid-ossp";
                   false    14            �           0    0    EXTENSION "uuid-ossp"    COMMENT     W   COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';
                        false    8            l           1247    29897 	   aal_level    TYPE     K   CREATE TYPE auth.aal_level AS ENUM (
    'aal1',
    'aal2',
    'aal3'
);
    DROP TYPE auth.aal_level;
       auth          postgres    false    16            o           1247    29904    code_challenge_method    TYPE     L   CREATE TYPE auth.code_challenge_method AS ENUM (
    's256',
    'plain'
);
 &   DROP TYPE auth.code_challenge_method;
       auth          postgres    false    16            r           1247    29910    factor_status    TYPE     M   CREATE TYPE auth.factor_status AS ENUM (
    'unverified',
    'verified'
);
    DROP TYPE auth.factor_status;
       auth          postgres    false    16            u           1247    29916    factor_type    TYPE     E   CREATE TYPE auth.factor_type AS ENUM (
    'totp',
    'webauthn'
);
    DROP TYPE auth.factor_type;
       auth          postgres    false    16            _           1255    29921    email()    FUNCTION       CREATE FUNCTION auth.email() RETURNS text
    LANGUAGE sql STABLE
    AS $$
  select 
  coalesce(
    nullif(current_setting('request.jwt.claim.email', true), ''),
    (nullif(current_setting('request.jwt.claims', true), '')::jsonb ->> 'email')
  )::text
$$;
    DROP FUNCTION auth.email();
       auth          postgres    false    16            �           0    0    FUNCTION email()    COMMENT     X   COMMENT ON FUNCTION auth.email() IS 'Deprecated. Use auth.jwt() -> ''email'' instead.';
          auth          postgres    false    607            `           1255    29922    jwt()    FUNCTION     �   CREATE FUNCTION auth.jwt() RETURNS jsonb
    LANGUAGE sql STABLE
    AS $$
  select 
    coalesce(
        nullif(current_setting('request.jwt.claim', true), ''),
        nullif(current_setting('request.jwt.claims', true), '')
    )::jsonb
$$;
    DROP FUNCTION auth.jwt();
       auth          postgres    false    16            a           1255    29923    role()    FUNCTION        CREATE FUNCTION auth.role() RETURNS text
    LANGUAGE sql STABLE
    AS $$
  select 
  coalesce(
    nullif(current_setting('request.jwt.claim.role', true), ''),
    (nullif(current_setting('request.jwt.claims', true), '')::jsonb ->> 'role')
  )::text
$$;
    DROP FUNCTION auth.role();
       auth          postgres    false    16            �           0    0    FUNCTION role()    COMMENT     V   COMMENT ON FUNCTION auth.role() IS 'Deprecated. Use auth.jwt() -> ''role'' instead.';
          auth          postgres    false    609            b           1255    29924    uid()    FUNCTION     �   CREATE FUNCTION auth.uid() RETURNS uuid
    LANGUAGE sql STABLE
    AS $$
  select 
  coalesce(
    nullif(current_setting('request.jwt.claim.sub', true), ''),
    (nullif(current_setting('request.jwt.claims', true), '')::jsonb ->> 'sub')
  )::uuid
$$;
    DROP FUNCTION auth.uid();
       auth          postgres    false    16            �           0    0    FUNCTION uid()    COMMENT     T   COMMENT ON FUNCTION auth.uid() IS 'Deprecated. Use auth.jwt() -> ''sub'' instead.';
          auth          postgres    false    610            f           1255    16591    grant_pg_cron_access()    FUNCTION     �  CREATE FUNCTION extensions.grant_pg_cron_access() RETURNS event_trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
  IF EXISTS (
    SELECT
    FROM pg_event_trigger_ddl_commands() AS ev
    JOIN pg_extension AS ext
    ON ev.objid = ext.oid
    WHERE ext.extname = 'pg_cron'
  )
  THEN
    grant usage on schema cron to postgres with grant option;

    alter default privileges in schema cron grant all on tables to postgres with grant option;
    alter default privileges in schema cron grant all on functions to postgres with grant option;
    alter default privileges in schema cron grant all on sequences to postgres with grant option;

    alter default privileges for user supabase_admin in schema cron grant all
        on sequences to postgres with grant option;
    alter default privileges for user supabase_admin in schema cron grant all
        on tables to postgres with grant option;
    alter default privileges for user supabase_admin in schema cron grant all
        on functions to postgres with grant option;

    grant all privileges on all tables in schema cron to postgres with grant option;
    revoke all on table cron.job from postgres;
    grant select on table cron.job to postgres with grant option;
  END IF;
END;
$$;
 1   DROP FUNCTION extensions.grant_pg_cron_access();
    
   extensions          postgres    false    14            �           0    0    FUNCTION grant_pg_cron_access()    COMMENT     U   COMMENT ON FUNCTION extensions.grant_pg_cron_access() IS 'Grants access to pg_cron';
       
   extensions          postgres    false    614            �           1255    16612    grant_pg_graphql_access()    FUNCTION     i	  CREATE FUNCTION extensions.grant_pg_graphql_access() RETURNS event_trigger
    LANGUAGE plpgsql
    AS $_$
DECLARE
    func_is_graphql_resolve bool;
BEGIN
    func_is_graphql_resolve = (
        SELECT n.proname = 'resolve'
        FROM pg_event_trigger_ddl_commands() AS ev
        LEFT JOIN pg_catalog.pg_proc AS n
        ON ev.objid = n.oid
    );

    IF func_is_graphql_resolve
    THEN
        -- Update public wrapper to pass all arguments through to the pg_graphql resolve func
        DROP FUNCTION IF EXISTS graphql_public.graphql;
        create or replace function graphql_public.graphql(
            "operationName" text default null,
            query text default null,
            variables jsonb default null,
            extensions jsonb default null
        )
            returns jsonb
            language sql
        as $$
            select graphql.resolve(
                query := query,
                variables := coalesce(variables, '{}'),
                "operationName" := "operationName",
                extensions := extensions
            );
        $$;

        -- This hook executes when `graphql.resolve` is created. That is not necessarily the last
        -- function in the extension so we need to grant permissions on existing entities AND
        -- update default permissions to any others that are created after `graphql.resolve`
        grant usage on schema graphql to postgres, anon, authenticated, service_role;
        grant select on all tables in schema graphql to postgres, anon, authenticated, service_role;
        grant execute on all functions in schema graphql to postgres, anon, authenticated, service_role;
        grant all on all sequences in schema graphql to postgres, anon, authenticated, service_role;
        alter default privileges in schema graphql grant all on tables to postgres, anon, authenticated, service_role;
        alter default privileges in schema graphql grant all on functions to postgres, anon, authenticated, service_role;
        alter default privileges in schema graphql grant all on sequences to postgres, anon, authenticated, service_role;

        -- Allow postgres role to allow granting usage on graphql and graphql_public schemas to custom roles
        grant usage on schema graphql_public to postgres with grant option;
        grant usage on schema graphql to postgres with grant option;
    END IF;

END;
$_$;
 4   DROP FUNCTION extensions.grant_pg_graphql_access();
    
   extensions          supabase_admin    false    14            �           0    0 "   FUNCTION grant_pg_graphql_access()    COMMENT     [   COMMENT ON FUNCTION extensions.grant_pg_graphql_access() IS 'Grants access to pg_graphql';
       
   extensions          supabase_admin    false    408            c           1255    29925    grant_pg_net_access()    FUNCTION     �  CREATE FUNCTION extensions.grant_pg_net_access() RETURNS event_trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
  IF EXISTS (
    SELECT 1
    FROM pg_event_trigger_ddl_commands() AS ev
    JOIN pg_extension AS ext
    ON ev.objid = ext.oid
    WHERE ext.extname = 'pg_net'
  )
  THEN
    IF NOT EXISTS (
      SELECT 1
      FROM pg_roles
      WHERE rolname = 'supabase_functions_admin'
    )
    THEN
      CREATE USER supabase_functions_admin NOINHERIT CREATEROLE LOGIN NOREPLICATION;
    END IF;

    GRANT USAGE ON SCHEMA net TO supabase_functions_admin, postgres, anon, authenticated, service_role;

    ALTER function net.http_get(url text, params jsonb, headers jsonb, timeout_milliseconds integer) SECURITY DEFINER;
    ALTER function net.http_post(url text, body jsonb, params jsonb, headers jsonb, timeout_milliseconds integer) SECURITY DEFINER;

    ALTER function net.http_get(url text, params jsonb, headers jsonb, timeout_milliseconds integer) SET search_path = net;
    ALTER function net.http_post(url text, body jsonb, params jsonb, headers jsonb, timeout_milliseconds integer) SET search_path = net;

    REVOKE ALL ON FUNCTION net.http_get(url text, params jsonb, headers jsonb, timeout_milliseconds integer) FROM PUBLIC;
    REVOKE ALL ON FUNCTION net.http_post(url text, body jsonb, params jsonb, headers jsonb, timeout_milliseconds integer) FROM PUBLIC;

    GRANT EXECUTE ON FUNCTION net.http_get(url text, params jsonb, headers jsonb, timeout_milliseconds integer) TO supabase_functions_admin, postgres, anon, authenticated, service_role;
    GRANT EXECUTE ON FUNCTION net.http_post(url text, body jsonb, params jsonb, headers jsonb, timeout_milliseconds integer) TO supabase_functions_admin, postgres, anon, authenticated, service_role;
  END IF;
END;
$$;
 0   DROP FUNCTION extensions.grant_pg_net_access();
    
   extensions          postgres    false    14            �           0    0    FUNCTION grant_pg_net_access()    COMMENT     S   COMMENT ON FUNCTION extensions.grant_pg_net_access() IS 'Grants access to pg_net';
       
   extensions          postgres    false    611            �           1255    16603    pgrst_ddl_watch()    FUNCTION     >  CREATE FUNCTION extensions.pgrst_ddl_watch() RETURNS event_trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
  cmd record;
BEGIN
  FOR cmd IN SELECT * FROM pg_event_trigger_ddl_commands()
  LOOP
    IF cmd.command_tag IN (
      'CREATE SCHEMA', 'ALTER SCHEMA'
    , 'CREATE TABLE', 'CREATE TABLE AS', 'SELECT INTO', 'ALTER TABLE'
    , 'CREATE FOREIGN TABLE', 'ALTER FOREIGN TABLE'
    , 'CREATE VIEW', 'ALTER VIEW'
    , 'CREATE MATERIALIZED VIEW', 'ALTER MATERIALIZED VIEW'
    , 'CREATE FUNCTION', 'ALTER FUNCTION'
    , 'CREATE TRIGGER'
    , 'CREATE TYPE', 'ALTER TYPE'
    , 'CREATE RULE'
    , 'COMMENT'
    )
    -- don't notify in case of CREATE TEMP table or other objects created on pg_temp
    AND cmd.schema_name is distinct from 'pg_temp'
    THEN
      NOTIFY pgrst, 'reload schema';
    END IF;
  END LOOP;
END; $$;
 ,   DROP FUNCTION extensions.pgrst_ddl_watch();
    
   extensions          supabase_admin    false    14            �           1255    16604    pgrst_drop_watch()    FUNCTION       CREATE FUNCTION extensions.pgrst_drop_watch() RETURNS event_trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
  obj record;
BEGIN
  FOR obj IN SELECT * FROM pg_event_trigger_dropped_objects()
  LOOP
    IF obj.object_type IN (
      'schema'
    , 'table'
    , 'foreign table'
    , 'view'
    , 'materialized view'
    , 'function'
    , 'trigger'
    , 'type'
    , 'rule'
    )
    AND obj.is_temporary IS false -- no pg_temp objects
    THEN
      NOTIFY pgrst, 'reload schema';
    END IF;
  END LOOP;
END; $$;
 -   DROP FUNCTION extensions.pgrst_drop_watch();
    
   extensions          supabase_admin    false    14            �           1255    16614    set_graphql_placeholder()    FUNCTION     r  CREATE FUNCTION extensions.set_graphql_placeholder() RETURNS event_trigger
    LANGUAGE plpgsql
    AS $_$
    DECLARE
    graphql_is_dropped bool;
    BEGIN
    graphql_is_dropped = (
        SELECT ev.schema_name = 'graphql_public'
        FROM pg_event_trigger_dropped_objects() AS ev
        WHERE ev.schema_name = 'graphql_public'
    );

    IF graphql_is_dropped
    THEN
        create or replace function graphql_public.graphql(
            "operationName" text default null,
            query text default null,
            variables jsonb default null,
            extensions jsonb default null
        )
            returns jsonb
            language plpgsql
        as $$
            DECLARE
                server_version float;
            BEGIN
                server_version = (SELECT (SPLIT_PART((select version()), ' ', 2))::float);

                IF server_version >= 14 THEN
                    RETURN jsonb_build_object(
                        'errors', jsonb_build_array(
                            jsonb_build_object(
                                'message', 'pg_graphql extension is not enabled.'
                            )
                        )
                    );
                ELSE
                    RETURN jsonb_build_object(
                        'errors', jsonb_build_array(
                            jsonb_build_object(
                                'message', 'pg_graphql is only available on projects running Postgres 14 onwards.'
                            )
                        )
                    );
                END IF;
            END;
        $$;
    END IF;

    END;
$_$;
 4   DROP FUNCTION extensions.set_graphql_placeholder();
    
   extensions          supabase_admin    false    14            �           0    0 "   FUNCTION set_graphql_placeholder()    COMMENT     |   COMMENT ON FUNCTION extensions.set_graphql_placeholder() IS 'Reintroduces placeholder function for graphql_public.graphql';
       
   extensions          supabase_admin    false    407            �           1255    16386    get_auth(text)    FUNCTION     J  CREATE FUNCTION pgbouncer.get_auth(p_usename text) RETURNS TABLE(username text, password text)
    LANGUAGE plpgsql SECURITY DEFINER
    AS $$
BEGIN
    RAISE WARNING 'PgBouncer auth request: %', p_usename;

    RETURN QUERY
    SELECT usename::TEXT, passwd::TEXT FROM pg_catalog.pg_shadow
    WHERE usename = p_usename;
END;
$$;
 2   DROP FUNCTION pgbouncer.get_auth(p_usename text);
    	   pgbouncer          postgres    false    12            d           1255    29926    event_tran_data_getall(bigint)    FUNCTION     �  CREATE FUNCTION public.event_tran_data_getall(filter_fiscal_year_id bigint) RETURNS TABLE(sequence bigint, season_name text, year_name text, gross_sales numeric, event_gross_sales numeric, readygoods_qnty bigint, readygoods_value numeric, tran_bp_event_id bigint, event_id bigint, season_id bigint, fiscal_year_id bigint, start_date timestamp with time zone, end_date timestamp with time zone, start_month_id bigint, end_month_id bigint, event_title text, is_active boolean, added_by bigint, updated_by bigint, date_added timestamp with time zone, date_updated timestamp without time zone)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 
SELECT s.sequence, s.season_name,y.year_name
	,ty.gross_sales
	,tse.event_gross_sales 
	,tse.readygoods_qnty 
	,tse.readygoods_value 
	,tse.tran_bp_event_id
	
	 ,tr.event_id ,
 tr.season_id ,
 tr.fiscal_year_id,
 tr.start_date ,
 tr.end_date,
    tr.start_month_id ,
    tr.end_month_id ,
    
    tr.event_title  ,
    tr.is_active ,
    tr.added_by ,
    tr.updated_by ,
    tr.date_added  ,
    tr.date_updated
	FROM 
	public.gen_season_event_config tr 
	inner join public.gen_season s on s.season_id=tr.season_id
	inner join public.gen_fiscal_year y on y.fiscal_year_id=tr.fiscal_year_id
	left outer join public.tran_bp_year ty on ty.fiscal_year_id=tr.fiscal_year_id
	left outer join public.tran_bp_event tse on tse.event_id=tr.event_id
	
	where tr.fiscal_year_id=filter_fiscal_year_id;
	
	END; 
$$;
 K   DROP FUNCTION public.event_tran_data_getall(filter_fiscal_year_id bigint);
       public          postgres    false            e           1255    29927    generate_serial_number()    FUNCTION     �   CREATE FUNCTION public.generate_serial_number() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    NEW.tran_sample_order_number := 'SO' || LPAD(NEW.tran_sample_order_id::TEXT, 10, '0');
    RETURN NEW;
END;
$$;
 /   DROP FUNCTION public.generate_serial_number();
       public          postgres    false            g           1255    29928 .   range_plan_getfor_createupdate(bigint, bigint)    FUNCTION     w  CREATE FUNCTION public.range_plan_getfor_createupdate(year_id_filter bigint, event_id_filter bigint) RETURNS TABLE(is_separate_price boolean, event_id bigint, tran_range_plan_event_id bigint, is_finalized boolean, tran_bp_event_id bigint, tran_bp_year_id bigint, total_rangeplan_mrp_value numeric, total_rangeplan_quantity bigint, total_rangeplan_cpu_value numeric, style_product_size_group_id bigint, is_submitted boolean, is_approved boolean, approved_by bigint, approve_date timestamp with time zone, approve_remarks text, remarks text, bp_yearly_gross_sales numeric, event_gross_sales numeric, range_plan_id bigint, range_plan_detail_id bigint, range_plan_quantity bigint, mrp_per_pc_value numeric, mrp_value numeric, cpu_per_pc_value numeric, cpu_value numeric, priority_id bigint, prev_year_quantity bigint, prev_year_efficiency numeric, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, added_by bigint, date_added timestamp with time zone, updated_by bigint, date_updated timestamp with time zone, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 
WITH cte_saved AS (
    select
	
spsg.is_separate_price,
tbe.event_id,
trpe.tran_range_plan_event_id,
	trpe.is_finalized,
trpe.tran_bp_event_id,
tby.tran_bp_year_id,
trpe.total_mrp_value as total_rangeplan_mrp_value ,
trpe.total_range_plan_quantity as total_rangeplan_quantity,
trpe.total_cpu_value as total_rangeplan_cpu_value,
vp.style_product_size_group_id,
rp.is_submitted,
    rp.is_approved,
    rp.approved_by,
    rp.approve_date,
    rp.approve_remarks,
	rp.remarks,

tby.gross_sales as bp_yearly_gross_sales,
tbe.event_gross_sales,

rpd.range_plan_id,rpd.range_plan_detail_id,

rpd.range_plan_quantity,
rpd.mrp_per_pc_value,
rpd.mrp_value,
rpd.cpu_per_pc_value,
rpd.cpu_value,
rpd.priority_id,
rpd.prev_year_quantity,
rpd.prev_year_efficiency,

vp.style_item_product_name, 
vp.style_item_type_name, 
vp.style_product_type_name, 
vp.style_item_origin_name, 
vp.style_gender_name, 

vp.master_category_name, 
rpd.added_by, rpd.date_added, 
rpd.updated_by, 
rpd.date_updated, 
vp.style_item_product_id, 
vp.style_item_type_id, 
vp.style_product_type_id, 
vp.style_item_origin_id, 
vp.style_gender_id, 
vp.style_master_category_id

from vw_style_item_product vp
inner join public.style_product_size_group spsg 
on spsg.style_product_size_group_id=vp.style_product_size_group_id
left outer join tran_range_plan_details rpd on rpd.style_item_product_id=vp.style_item_product_id

left outer join tran_range_plan rp on rp.range_plan_id=rpd.range_plan_id
left outer join tran_bp_year tby on tby.fiscal_year_id=vp.fiscal_year_id
left outer join tran_bp_event tbe on tbe.tran_bp_year_id=tby.tran_bp_year_id
and rpd.tran_bp_event_id=tbe.tran_bp_event_id
left outer join public.tran_range_plan_events trpe 
on  trpe.tran_bp_event_id=tbe.tran_bp_event_id

where vp.fiscal_year_id	=year_id_filter

and tbe.event_id=event_id_filter
)

select * from cte_saved
union
select
spsg.is_separate_price,
tbe.event_id,
(select inr.tran_range_plan_event_id from public.tran_range_plan_events inr 
 where inr.tran_bp_event_id=tbe.tran_bp_event_id) tran_range_plan_event_id,
NULL as is_finalized,
tbe.tran_bp_event_id,
tby.tran_bp_year_id,
0 as total_rangeplan_mrp_value ,
0 as total_rangeplan_quantity,
0 as total_rangeplan_cpu_value,
vp.style_product_size_group_id,
NULL as is_submitted,
   NULL as is_approved,
    NULL as approved_by,
    NULL as approve_date,
    '' as approve_remarks,
	'' as remarks,

tby.gross_sales as bp_yearly_gross_sales,
tbe.event_gross_sales,

NULL as range_plan_id,NULL as range_plan_detail_id,

NULL as range_plan_quantity,
NULL as mrp_per_pc_value,
NULL as mrp_value,
NULL as cpu_per_pc_value,
NULL as cpu_value,
NULL as priority_id,
NULL as prev_year_quantity,
NULL as prev_year_efficiency,

vp.style_item_product_name, 
vp.style_item_type_name, 
vp.style_product_type_name, 
vp.style_item_origin_name, 
vp.style_gender_name, 

vp.master_category_name, 
NULL as  added_by, NULL as date_added, 
NULL as updated_by, 
NULL as date_updated, 
vp.style_item_product_id, 
vp.style_item_type_id, 
vp.style_product_type_id, 
vp.style_item_origin_id, 
vp.style_gender_id, 
vp.style_master_category_id

from vw_style_item_product vp
inner join public.style_product_size_group spsg 
on spsg.style_product_size_group_id=vp.style_product_size_group_id

left outer join tran_bp_year tby on tby.fiscal_year_id=vp.fiscal_year_id
left outer join tran_bp_event tbe on tbe.tran_bp_year_id=tby.tran_bp_year_id

where vp.fiscal_year_id	=year_id_filter

and tbe.event_id=event_id_filter and vp.style_item_product_id not in (select t.style_item_product_id from cte_saved t);
	
END; 
$$;
 d   DROP FUNCTION public.range_plan_getfor_createupdate(year_id_filter bigint, event_id_filter bigint);
       public          postgres    false            h           1255    29929 &   range_plan_getfor_view(bigint, bigint)    FUNCTION     �  CREATE FUNCTION public.range_plan_getfor_view(year_id_filter bigint, event_id_filter bigint) RETURNS TABLE(is_separate_price boolean, total_product bigint, event_id bigint, tran_range_plan_event_id bigint, is_finalized boolean, tran_bp_event_id bigint, tran_bp_year_id bigint, total_rangeplan_mrp_value numeric, total_rangeplan_quantity bigint, total_rangeplan_cpu_value numeric, style_product_size_group_id bigint, is_submitted boolean, is_approved boolean, approved_by bigint, approve_date timestamp with time zone, approve_remarks text, remarks text, bp_yearly_gross_sales numeric, event_gross_sales numeric, range_plan_id bigint, range_plan_detail_id bigint, range_plan_quantity bigint, mrp_per_pc_value numeric, mrp_value numeric, cpu_per_pc_value numeric, cpu_value numeric, priority_id bigint, prev_year_quantity bigint, prev_year_efficiency numeric, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, added_by bigint, date_added timestamp with time zone, updated_by bigint, date_updated timestamp with time zone, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 
select 
spsg.is_separate_price,
CAST(COALESCE((select count(*) from style_item_product),0) as bigint) total_product,
tbe.event_id,
trpe.tran_range_plan_event_id,trpe.is_finalized,
tbe.tran_bp_event_id,
tby.tran_bp_year_id,
trpe.total_mrp_value as total_rangeplan_mrp_value ,
trpe.total_range_plan_quantity as total_rangeplan_quantity,
trpe.total_cpu_value as total_rangeplan_cpu_value,
vp.style_product_size_group_id,
rp.is_submitted,
    rp.is_approved,
    rp.approved_by,
    rp.approve_date,
    rp.approve_remarks,
	rp.remarks,

tby.gross_sales as bp_yearly_gross_sales,
tbe.event_gross_sales,

rpd.range_plan_id,rpd.range_plan_detail_id,

rpd.range_plan_quantity,
rpd.mrp_per_pc_value,
rpd.mrp_value,
rpd.cpu_per_pc_value,
rpd.cpu_value,
rpd.priority_id,
rpd.prev_year_quantity,
rpd.prev_year_efficiency,

vp.style_item_product_name, 
vp.style_item_type_name, 
vp.style_product_type_name, 
vp.style_item_origin_name, 
vp.style_gender_name, 

vp.master_category_name, 
rpd.added_by, rpd.date_added, 
rpd.updated_by, 
rpd.date_updated, 
vp.style_item_product_id, 
vp.style_item_type_id, 
vp.style_product_type_id, 
vp.style_item_origin_id, 
vp.style_gender_id, 
vp.style_master_category_id

from vw_style_item_product vp
inner join public.style_product_size_group spsg 
on spsg.style_product_size_group_id=vp.style_product_size_group_id
inner join tran_range_plan_details rpd on rpd.style_item_product_id=vp.style_item_product_id

inner join tran_range_plan rp on rp.range_plan_id=rpd.range_plan_id
inner join tran_bp_year tby on tby.fiscal_year_id=vp.fiscal_year_id
inner join tran_bp_event tbe on tbe.tran_bp_year_id=tby.tran_bp_year_id
and rpd.tran_bp_event_id=tbe.tran_bp_event_id
inner join public.tran_range_plan_events trpe 
on  trpe.tran_bp_event_id=tbe.tran_bp_event_id

where vp.fiscal_year_id	=year_id_filter

and tbe.event_id=event_id_filter;
	
END; 
$$;
 \   DROP FUNCTION public.range_plan_getfor_view(year_id_filter bigint, event_id_filter bigint);
       public          postgres    false            i           1255    29930 .   sp_get_color_detl_size_by_vaplandetlid(bigint)    FUNCTION     8  CREATE FUNCTION public.sp_get_color_detl_size_by_vaplandetlid(va_plan_detl_id_filter bigint) RETURNS TABLE(designer_member_id bigint, style_item_product_sub_category_id bigint, sub_category_name character varying, style_product_size text, style_embellishment_ids text, style_item_product_name character varying, tran_va_plan_detl_style_color_size_id bigint, tran_va_plan_detl_style_color_id bigint, style_product_size_group_detid bigint, style_color_size_quantity bigint, style_color_quantity bigint, color_code text, tran_va_plan_detl_style_id bigint, color_code_gen text, no_of_color bigint, style_quantity bigint, style_code text, tran_va_plan_detl_id bigint, style_code_gen text, no_of_style bigint, style_item_product_id bigint, range_plan_detail_id bigint, tran_va_plan_event_id bigint)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 
SELECT vw.designer_member_id,
vw.style_item_product_sub_category_id,
vw.sub_category_name,
vw.style_product_size,
CAST( vw.style_embellishment_ids as text) as str_style_embellishment_ids,
	
vw.style_item_product_name ,
vw.tran_va_plan_detl_style_color_size_id, vw.tran_va_plan_detl_style_color_id,
vw.style_product_size_group_detid, vw.style_color_size_quantity, vw.style_color_quantity,
vw.color_code, vw.tran_va_plan_detl_style_id, vw.color_code_gen, vw.no_of_color,
vw.style_quantity, vw.style_code, vw.tran_va_plan_detl_id, vw.style_code_gen, vw.no_of_style, 
vw.style_item_product_id, vw.range_plan_detail_id, vw.tran_va_plan_event_id
	FROM public.vw_va_detl_style vw 
	
	where vw.tran_va_plan_detl_id=va_plan_detl_id_filter;
	
	END; 
$$;
 \   DROP FUNCTION public.sp_get_color_detl_size_by_vaplandetlid(va_plan_detl_id_filter bigint);
       public          postgres    false            j           1255    29931 ,   sp_get_data_for_sampleorder(bigint, integer)    FUNCTION     6  CREATE FUNCTION public.sp_get_data_for_sampleorder(tran_va_plan_event_id_filter bigint, user_id_filter integer) RETURNS TABLE(total_sample_count bigint, total_assigned_style bigint, total_assigned_quantity bigint, tran_va_plan_event_id bigint, tran_va_plan_id bigint, tran_va_plan_detl_id bigint, no_of_style bigint, style_code_gen text, is_separate_price boolean, event_id bigint, tran_range_plan_event_id bigint, is_finalized boolean, tran_bp_event_id bigint, tran_bp_year_id bigint, total_rangeplan_mrp_value numeric, total_rangeplan_quantity bigint, total_rangeplan_cpu_value numeric, style_product_size_group_id bigint, is_submitted boolean, is_approved boolean, approved_by bigint, approve_date timestamp with time zone, approve_remarks text, remarks text, bp_yearly_gross_sales numeric, event_gross_sales numeric, range_plan_id bigint, range_plan_detail_id bigint, range_plan_quantity bigint, mrp_per_pc_value numeric, mrp_value numeric, cpu_per_pc_value numeric, cpu_value numeric, priority_id bigint, prev_year_quantity bigint, prev_year_efficiency numeric, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, added_by bigint, date_added timestamp with time zone, updated_by bigint, date_updated timestamp with time zone, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 

     select 
	 (select  cast( count(tmp1.tran_sample_order_id) as bigint) from public.tran_sample_order tmp1 
	  where tmp1.tran_va_plan_detl_id= tvpd.tran_va_plan_detl_id ) as total_sample_count, 
	  (select cast( count(tmp1.designer_member_id)as bigint) from public.tran_va_plan_detl_style tmp1 
	   inner join public.gen_team_members tm on 
	   tmp1.designer_member_id=tm.gen_team_members_id
  where tmp1.tran_va_plan_detl_id= tvpd.tran_va_plan_detl_id
	  and tm.user_id=user_id_filter) as total_assigned_style,
	    (select cast(sum(tmp1.style_quantity) as bigint) from public.tran_va_plan_detl_style tmp1 
	   inner join public.gen_team_members tm on 
	   tmp1.designer_member_id=tm.gen_team_members_id
  where tmp1.tran_va_plan_detl_id= tvpd.tran_va_plan_detl_id
	  and tm.user_id=user_id_filter) as total_assigned_quantity,
  tvpe.tran_va_plan_event_id,
  tvp.tran_va_plan_id,
  tvpd.tran_va_plan_detl_id,
  tvpd.no_of_style,
	tvpd.style_code_gen,
	
spsg.is_separate_price,
tbe.event_id,
trpe.tran_range_plan_event_id,trpe.is_finalized,
trpe.tran_bp_event_id,
tby.tran_bp_year_id,
trpe.total_mrp_value as total_rangeplan_mrp_value ,
trpe.total_range_plan_quantity as total_rangeplan_quantity,
trpe.total_cpu_value as total_rangeplan_cpu_value,
vp.style_product_size_group_id,
rp.is_submitted,
    rp.is_approved,
    rp.approved_by,
    rp.approve_date,
    rp.approve_remarks,
	rp.remarks,

tby.gross_sales as bp_yearly_gross_sales,
tbe.event_gross_sales,

rpd.range_plan_id,rpd.range_plan_detail_id,

rpd.range_plan_quantity,
rpd.mrp_per_pc_value,
rpd.mrp_value,
rpd.cpu_per_pc_value,
rpd.cpu_value,
rpd.priority_id,
rpd.prev_year_quantity,
rpd.prev_year_efficiency,

vp.style_item_product_name, 
vp.style_item_type_name, 
vp.style_product_type_name, 
vp.style_item_origin_name, 
vp.style_gender_name, 

vp.master_category_name, 
rpd.added_by, rpd.date_added, 
rpd.updated_by, 
rpd.date_updated, 
vp.style_item_product_id, 
vp.style_item_type_id, 
vp.style_product_type_id, 
vp.style_item_origin_id, 
vp.style_gender_id, 
vp.style_master_category_id

from vw_style_item_product vp
inner join public.style_product_size_group spsg 
on spsg.style_product_size_group_id=vp.style_product_size_group_id
inner  join tran_range_plan_details rpd on rpd.style_item_product_id=vp.style_item_product_id

inner  join tran_range_plan rp on rp.range_plan_id=rpd.range_plan_id
inner  join tran_bp_year tby on tby.fiscal_year_id=vp.fiscal_year_id
inner  join tran_bp_event tbe on tbe.tran_bp_year_id=tby.tran_bp_year_id
and rpd.tran_bp_event_id=tbe.tran_bp_event_id
inner  join public.tran_range_plan_events trpe 
on  trpe.tran_bp_event_id=tbe.tran_bp_event_id
	
	

inner join 
public.tran_va_plan_detl tvpd on
	tvpd.style_item_product_id=vp.style_item_product_id
	and tvpd.range_plan_detail_id=rpd.range_plan_detail_id
left outer join 
(select  detl_single.style_item_product_id from public.tran_va_plan_detl detl_single 
group by detl_single.style_item_product_id
) det_single
on det_single.style_item_product_id=vp.style_item_product_id
inner join public.tran_va_plan_events tvpe on
	tvpe.tran_va_plan_event_id=tvpd.tran_va_plan_event_id
inner join public.tran_va_plan tvp on
	tvp.tran_va_plan_id=tvpe.tran_va_plan_id

where tvpd.tran_va_plan_event_id=tran_va_plan_event_id_filter;
	 

END; 
$$;
 o   DROP FUNCTION public.sp_get_data_for_sampleorder(tran_va_plan_event_id_filter bigint, user_id_filter integer);
       public          postgres    false            k           1255    29933 $   sp_get_mapped_item_structure(bigint)    FUNCTION     7  CREATE FUNCTION public.sp_get_mapped_item_structure(style_master_category_id_filter bigint) RETURNS TABLE(item_structure_group_id bigint, structure_group_name text, sub_group_name text, gen_item_structure_group_sub_id bigint)
    LANGUAGE plpgsql
    AS $$
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
 [   DROP FUNCTION public.sp_get_mapped_item_structure(style_master_category_id_filter bigint);
       public          postgres    false            l           1255    29934 !   sp_get_menu_list_for_api_module()    FUNCTION     (  CREATE FUNCTION public.sp_get_menu_list_for_api_module() RETURNS TABLE(menu_id integer, parent_id integer, menu_caption character varying, navigate_url character varying, image_url character varying, seq_no integer, is_visible boolean, is_api boolean, added_by integer, updated_by integer, date_added timestamp without time zone, date_updated timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
DECLARE 
 _is_super_user BOOLEAN:=false;
 _is_admin BOOLEAN := false; 
 _user_code integer:= 1;
 Begin

-- 		RETURN QUERY Select mn.menu_id, 1::BOOLEAN can_select, '1'::BOOLEAN can_insert, '1':: BOOLEAN can_update, 1:: BOOLEAN can_delete 
		RETURN QUERY Select mn.*
		From public.menu mn
		Where mn.is_visible = true And mn.menu_id <> mn.parent_id
		Order By mn.seq_no, mn.menu_id;
		
		
		

	
	END; 
$$;
 8   DROP FUNCTION public.sp_get_menu_list_for_api_module();
       public          postgres    false            m           1255    29935 &   sp_get_menu_list_for_api_module_test()    FUNCTION     :  CREATE FUNCTION public.sp_get_menu_list_for_api_module_test() RETURNS TABLE(menu_id bigint)
    LANGUAGE plpgsql
    AS $$
DECLARE 
 _is_super_user BOOLEAN:=false;
 _is_admin BOOLEAN := false; 
 _user_code integer:= 1;
 Begin
 
 SELECT  is_super_user,is_admin INTO _is_super_user,_is_admin
 FROM PUBLIC.tbl_login_user WHERE user_code = _user_code;
	
    IF(_is_admin=false and _is_super_user=false) then
	
		RETURN QUERY 
		With pm 
		AS(
			SELECT srm.menu_id
			FROM public.tbl_security_rule_menu srm
			Inner Join public.tbl_group_user_security_rule guser On guser.security_rule_code = srm.security_rule_code
			Inner Join public.tbl_login_user_attached_with_group_user luawgu On luawgu.group_code = guser.group_code
			Inner Join public.tbl_menu M On M.menu_id = SRM.menu_id
			Where luawgu.user_code = _user_code And SRM.can_select = 1 
			Group By srm.menu_id, M.application_id, srm.can_select, srm.can_insert, srm.can_update, srm.can_delete
		), app As(
			Select application_id
			From pm
			Group By application_id
		)
		
-- 		RETURN QUERY Select m.menu_id, COALESCE(pm.can_select,0) can_select, COALESCE(pm.can_insert,0) can_insert, COALESCE(pm.can_update,0) can_update, COALESCE(pm.can_delete,0) can_delete
		
-- 		RETURN QUERY Select pm.menu_id
-- 		Inner Join app On app.application_id = m.application_id
-- 		Inner Join pm On pm.menu_id = m.menu_id
-- 		Where m.is_api=1 And m.is_visible = 1
-- 		Order By m.seq_no, m.menu_id;
		
		 Select mn.menu_id
		From public.tbl_menu mn
		Where mn.is_visible = true
		Order By mn.seq_no, mn.menu_id;
		
	Else
-- 		RETURN QUERY Select mn.menu_id, 1::BOOLEAN can_select, '1'::BOOLEAN can_insert, '1':: BOOLEAN can_update, 1:: BOOLEAN can_delete 
		RETURN QUERY Select mn.menu_id
		From public.tbl_menu mn
		Where mn.is_visible = true
		Order By mn.seq_no, mn.menu_id;
		
		
		
	End if;
	
	END; $$;
 =   DROP FUNCTION public.sp_get_menu_list_for_api_module_test();
       public          postgres    false            n           1255    29936 /   sp_get_order_embellishment_json(bigint, bigint)    FUNCTION     W  CREATE FUNCTION public.sp_get_order_embellishment_json(tran_va_plan_detl_id_filter bigint, tran_sample_order_id_filter bigint) RETURNS TABLE(embellishment_json text)
    LANGUAGE plpgsql
    AS $$
DECLARE  
 
 Begin

		RETURN QUERY 
	
		select 
	   cast(   json_agg(json_build_object(
        'tran_sample_order_id', details_table2.tran_sample_order_id,
        'embellishment_id', details_table2.embellishment_id
       
    )) as text) AS embellishment_json

	 
FROM
    public.tran_sample_order main_table
 LEFT JOIN
   public.tran_sample_order_embellishment details_table2   
   ON main_table.tran_sample_order_id = details_table2.tran_sample_order_id
   where main_table.tran_va_plan_detl_id=tran_va_plan_detl_id_filter
      and  main_table.tran_sample_order_id=tran_sample_order_id_filter
   GROUP BY
    main_table.tran_sample_order_id;
END; 
$$;
 ~   DROP FUNCTION public.sp_get_order_embellishment_json(tran_va_plan_detl_id_filter bigint, tran_sample_order_id_filter bigint);
       public          postgres    false            o           1255    29937 *   sp_get_order_subgroup_json(bigint, bigint)    FUNCTION     V  CREATE FUNCTION public.sp_get_order_subgroup_json(tran_va_plan_detl_id_filter bigint, tran_sample_order_id_filter bigint) RETURNS TABLE(subgroup_json text)
    LANGUAGE plpgsql
    AS $$
DECLARE  
 
 Begin

		RETURN QUERY 
	
		select 
	   cast( json_agg(json_build_object(
        'tran_sample_order_id', details_table.tran_sample_order_id,
        'gen_item_structure_group_sub_id', details_table.gen_item_structure_group_sub_id
       
    )) as text) AS subgroup_json
	 
FROM
    public.tran_sample_order main_table
LEFT JOIN
   public.tran_sample_order_subgroup details_table   
   ON main_table.tran_sample_order_id = details_table.tran_sample_order_id
   where main_table.tran_va_plan_detl_id=tran_va_plan_detl_id_filter
   and  main_table.tran_sample_order_id=tran_sample_order_id_filter
   GROUP BY
    main_table.tran_sample_order_id;
END; 
$$;
 y   DROP FUNCTION public.sp_get_order_subgroup_json(tran_va_plan_detl_id_filter bigint, tran_sample_order_id_filter bigint);
       public          postgres    false            p           1255    29938 (   sp_get_parent_menu_list_for_api_module()    FUNCTION     �  CREATE FUNCTION public.sp_get_parent_menu_list_for_api_module() RETURNS TABLE(menu_id integer, parent_id integer, menu_caption character varying, seq_no integer)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 
 	Select m.menu_id, m.parent_id, m.menu_caption, m.seq_no 
	From menu m 
	Where m.menu_id = m.parent_id
	Group By m.menu_id, m.parent_id, m.menu_caption, m.seq_no 
	Order By m.seq_no;
	
	END; 
$$;
 ?   DROP FUNCTION public.sp_get_parent_menu_list_for_api_module();
       public          postgres    false            q           1255    29939 #   sp_get_sample_order_details(bigint)    FUNCTION     �  CREATE FUNCTION public.sp_get_sample_order_details(designer_member_id_filter bigint) RETURNS TABLE(tran_sample_order_id bigint, tran_va_plan_detl_id bigint, tran_sample_order_number text, order_date timestamp with time zone, delivery_date timestamp with time zone, delivery_unit_id bigint, order_quantity bigint, subgroup_json text, embellishment_json text)
    LANGUAGE plpgsql
    AS $$
DECLARE 
 
Begin

	
		RETURN QUERY 	
		
		SELECT
	
	   main_table.tran_sample_order_id ,main_table.tran_va_plan_detl_id,
	main_table.tran_sample_order_number ,
	main_table.order_date ,
	main_table.delivery_date ,
	main_table.delivery_unit_id ,
	main_table.order_quantity ,

	sp_get_order_subgroup_json(main_table.tran_va_plan_detl_id, main_table.tran_sample_order_id) subgroup_json,
	sp_get_order_embellishment_json(main_table.tran_va_plan_detl_id, main_table.tran_sample_order_id) embellishment_json
	 
FROM
    public.tran_sample_order main_table

where main_table.tran_va_plan_detl_id in 
 (
 select tmp2.tran_va_plan_detl_id from public.tran_va_plan_detl tmp2
	 inner join public.tran_va_plan_detl_style tmp3 on tmp2.tran_va_plan_detl_id=tmp3.tran_va_plan_detl_id
	 and tmp3.designer_member_id=designer_member_id_filter
 )
    
GROUP BY
    main_table.tran_sample_order_id,
	 main_table.tran_va_plan_detl_id,
	main_table.tran_sample_order_number ,
	main_table.order_date ,
	main_table.delivery_date ,
	main_table.delivery_unit_id ,
	main_table.order_quantity;
  

		
END; 
$$;
 T   DROP FUNCTION public.sp_get_sample_order_details(designer_member_id_filter bigint);
       public          postgres    false            r           1255    29940 ,   sp_get_sampleorder_embellishment_det(bigint)    FUNCTION     =  CREATE FUNCTION public.sp_get_sampleorder_embellishment_det(tran_sample_order_id_filter bigint) RETURNS TABLE(style_embellishment_name character varying, tran_sample_order_embellishment_id bigint, embellishment_id bigint)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 select b.style_embellishment_name,
 a.tran_sample_order_embellishment_id,
 a.embellishment_id
 from public.tran_sample_order_embellishment a
inner join public.style_embellishment b on
b.style_embellishment_id=a.embellishment_id
where a.tran_sample_order_id=tran_sample_order_id_filter;

END; 
$$;
 _   DROP FUNCTION public.sp_get_sampleorder_embellishment_det(tran_sample_order_id_filter bigint);
       public          postgres    false            s           1255    29941 '   sp_get_sampleorder_subgroup_det(bigint)    FUNCTION     �  CREATE FUNCTION public.sp_get_sampleorder_subgroup_det(tran_sample_order_id_filter bigint) RETURNS TABLE(item_structure_group_id bigint, gen_item_structure_group_sub_id bigint, sub_group_name text, tran_sample_order_subgroup_id bigint)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 select b.item_structure_group_id,
 a.gen_item_structure_group_sub_id,b.sub_group_name,
 a.tran_sample_order_subgroup_id
 from public.tran_sample_order_subgroup a
inner join public.gen_item_structure_group_sub b on
b.gen_item_structure_group_sub_id=a.gen_item_structure_group_sub_id
where a.tran_sample_order_id=tran_sample_order_id_filter;

END; 
$$;
 Z   DROP FUNCTION public.sp_get_sampleorder_subgroup_det(tran_sample_order_id_filter bigint);
       public          postgres    false            t           1255    29942    sp_get_style_group_size()    FUNCTION     b  CREATE FUNCTION public.sp_get_style_group_size() RETURNS TABLE(is_separate_price boolean, style_product_size_group_id bigint, style_product_size_group_detid bigint, style_product_size text)
    LANGUAGE plpgsql
    AS $$
DECLARE  
 
 Begin

		RETURN QUERY 
		Select sg.is_separate_price, sg.style_product_size_group_id,
		sgd.style_product_size_group_detid,sgd.style_product_size
		From public.style_product_size_group sg inner join public.style_product_size_group_details sgd
		on sg.style_product_size_group_id=sgd.style_product_size_group_id;
		--where sg.style_product_size_group_id=group_id;
		
END; 
$$;
 0   DROP FUNCTION public.sp_get_style_group_size();
       public          postgres    false            u           1255    29943 7   sp_get_style_group_size_by_fiscalyearid(bigint, bigint)    FUNCTION     .  CREATE FUNCTION public.sp_get_style_group_size_by_fiscalyearid(_fiscal_year_id bigint, _event_id bigint) RETURNS TABLE(range_plan_detail_size_id bigint, range_plan_detail_id bigint, is_separate_price boolean, style_product_size_group_id bigint, style_product_size_group_detid bigint, style_product_size text, size_quantity bigint, size_per_pc_mrp_value numeric, style_item_product_id bigint, size_per_pc_cpu_value numeric)
    LANGUAGE plpgsql
    AS $$
DECLARE  
 
 Begin

		RETURN QUERY 
		Select
		rpds.range_plan_detail_size_id,
		rpds.range_plan_detail_id,
		sg.is_separate_price, sg.style_product_size_group_id,
		sgd.style_product_size_group_detid,sgd.style_product_size
		,rpds.size_quantity, rpds.size_per_pc_mrp_value,
		rpd.style_item_product_id,
		rpds.size_per_pc_cpu_value
		
		
		From public.style_product_size_group sg
		
		inner join public.style_product_size_group_details sgd
		on sg.style_product_size_group_id=sgd.style_product_size_group_id
		
		
		left outer join public.tran_range_plan_details_size rpds
		on rpds.style_product_size_group_detid=sgd.style_product_size_group_detid
		
		
		left outer join public.tran_range_plan_details rpd 
		on rpd.range_plan_detail_id=rpds.range_plan_detail_id
		
		left outer join public.tran_bp_event tbe on
		tbe.tran_bp_event_id=rpd.tran_bp_event_id
		
		left outer join public.tran_range_plan rp 
		on rp.range_plan_id=rpd.range_plan_id
		
		left outer join public.tran_bp_year bp 
		on bp.tran_bp_year_id=rp.tran_bp_year_id
		
		where bp.fiscal_year_id=_fiscal_year_id  and tbe.event_id=_event_id;
		
		
		
END; 
$$;
 h   DROP FUNCTION public.sp_get_style_group_size_by_fiscalyearid(_fiscal_year_id bigint, _event_id bigint);
       public          postgres    false            v           1255    29944 I   sp_get_style_product_item(bigint, bigint, bigint, bigint, bigint, bigint)    FUNCTION     �  CREATE FUNCTION public.sp_get_style_product_item(fiscal_year_id_filter bigint DEFAULT NULL::bigint, style_master_category_id_filter bigint DEFAULT NULL::bigint, style_gender_id_filter bigint DEFAULT NULL::bigint, style_item_origin_id_filter bigint DEFAULT NULL::bigint, style_item_type_id_filter bigint DEFAULT NULL::bigint, style_product_type_id_filter bigint DEFAULT NULL::bigint) RETURNS TABLE(style_product_size_group_name text, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint, fiscal_year_id bigint, style_product_size_group_id bigint)
    LANGUAGE plpgsql
    AS $$
 
 Begin
 RETURN QUERY
 
    
 SELECT vw.style_product_size_group_name, vw.style_item_product_name,
    vw.style_item_type_name,
    vw.style_product_type_name,
    vw.style_item_origin_name,
    vw.style_gender_name,
    vw.master_category_name,

    vw.style_item_product_id,
    vw.style_item_type_id,
    vw.style_product_type_id,
    vw.style_item_origin_id,
    vw.style_gender_id,
    vw.style_master_category_id,
    vw.fiscal_year_id,
    vw.style_product_size_group_id
   FROM public.vw_style_item_product vw
	
	where 
	
	(CASE WHEN fiscal_year_id_filter is NULL THEN 1 WHEN vw.fiscal_year_id  = fiscal_year_id_filter THEN 1 ELSE 0 END = 1)
	AND (CASE WHEN style_master_category_id_filter is NULL THEN 1 WHEN vw.style_master_category_id  = style_master_category_id_filter THEN 1 ELSE 0 END = 1)
	AND (CASE WHEN style_gender_id_filter is NULL THEN 1 WHEN vw.style_gender_id  = style_gender_id_filter THEN 1 ELSE 0 END = 1)
	AND (CASE WHEN style_item_origin_id_filter is NULL THEN 1 WHEN vw.style_item_origin_id  = style_item_origin_id_filter THEN 1 ELSE 0 END = 1)
	AND (CASE WHEN style_item_type_id_filter is NULL THEN 1 WHEN vw.style_product_type_id  = style_item_type_id_filter THEN 1 ELSE 0 END = 1)
	AND (CASE WHEN style_product_type_id_filter is NULL THEN 1 WHEN vw.style_product_type_id  = style_product_type_id_filter THEN 1 ELSE 0 END = 1);
	
	
	END; 
$$;
    DROP FUNCTION public.sp_get_style_product_item(fiscal_year_id_filter bigint, style_master_category_id_filter bigint, style_gender_id_filter bigint, style_item_origin_id_filter bigint, style_item_type_id_filter bigint, style_product_type_id_filter bigint);
       public          postgres    false            w           1255    29945 )   sp_get_tran_bp_event_month_outlet(bigint)    FUNCTION     V  CREATE FUNCTION public.sp_get_tran_bp_event_month_outlet(filter_fiscal_year_id bigint) RETURNS TABLE(tran_bp_event_month_outlet_id bigint, tran_bp_event_month_id bigint, outlet_id bigint, outlet_gross_sales numeric, tran_bp_year_id bigint, tran_bp_event_id bigint, month_id bigint, event_id bigint, yearly_gross_sales numeric, event_gross_sales numeric, readygoods_qnty bigint, readygoods_value numeric, monthly_gross_sales numeric, added_by bigint, date_added timestamp with time zone)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 
 
	SELECT 
	tvmo.tran_bp_event_month_outlet_id ,
     tvmo.tran_bp_event_month_id ,
	 tvmo.outlet_id ,
     tvmo.outlet_gross_sales,
	 ty.tran_bp_year_id,
	 tse.tran_bp_event_id,
	 tvm.month_id,
	 tse.event_id,
	 ty.gross_sales as yearly_gross_sales,
	 tse.event_gross_sales,
	 tse.readygoods_qnty,
	 tse.readygoods_value,
	 tvm.monthly_gross_sales, ty.added_by,ty.date_added
	 
	FROM 
	 public.gen_fiscal_year y 
	inner join public.tran_bp_year ty on ty.fiscal_year_id=y.fiscal_year_id
	inner join public.tran_bp_event tse on tse.tran_bp_year_id=ty.tran_bp_year_id
	inner join public.tran_bp_event_month tvm on tvm.tran_bp_event_id=tse.tran_bp_event_id
	inner join public.tran_bp_event_month_outlet tvmo 
	on tvmo.tran_bp_event_month_id=tvm.tran_bp_event_month_id
	where y.fiscal_year_id=filter_fiscal_year_id;
	END;  
$$;
 V   DROP FUNCTION public.sp_get_tran_bp_event_month_outlet(filter_fiscal_year_id bigint);
       public          postgres    false            x           1255    29946 "   sp_get_tran_bp_year_gettotalrows()    FUNCTION     �   CREATE FUNCTION public.sp_get_tran_bp_year_gettotalrows() RETURNS TABLE(totalcount bigint)
    LANGUAGE plpgsql
    AS $$
DECLARE 
 
 Begin


		RETURN QUERY 
		Select count(mn.*) TotalCount
		From public.tran_bp_year mn;
		
END; 
$$;
 9   DROP FUNCTION public.sp_get_tran_bp_year_gettotalrows();
       public          postgres    false            y           1255    29947 %   sp_get_tran_range_plan_events(bigint)    FUNCTION     1  CREATE FUNCTION public.sp_get_tran_range_plan_events(_fiscal_year_id bigint) RETURNS TABLE(range_plan_id bigint, yearly_gross_sales numeric, tran_bp_event_id bigint, event_gross_sales numeric, readygoods_qnty bigint, readygoods_value numeric, event_id bigint, fiscal_year_id bigint, event_title text, total_range_plan_quantity bigint, total_mrp_value numeric, total_cpu_value numeric, is_finalized boolean, added_product bigint)
    LANGUAGE plpgsql
    AS $$
DECLARE 
 
Begin

	
		RETURN QUERY 	
		
		SELECT trp.range_plan_id, tby.gross_sales yearly_gross_sales, tbe.tran_bp_event_id,
tbe.event_gross_sales,tbe.readygoods_qnty,tbe.readygoods_value,
tbe.event_id,tby.fiscal_year_id,gsc.event_title,
COALESCE(trpe.total_range_plan_quantity,0) as total_range_plan_quantity,
COALESCE(trpe.total_mrp_value,0)as total_mrp_Value,
 COALESCE(trpe.total_cpu_value,0) as total_cpu_value,trpe.is_finalized,
 (
	 select count(tmp.range_plan_detail_id) from tran_range_plan_details tmp
     inner join public.tran_bp_event tmp2 on tmp2.tran_bp_event_id=tmp.tran_bp_event_id
     where tmp.range_plan_id=trpe.range_plan_id and tmp2.event_id=tbe.event_id
 ) added_product
FROM public.tran_bp_event tbe

inner join public.tran_bp_year tby on tby.tran_bp_year_id=tbe.tran_bp_year_id

inner join public.gen_season_event_config gsc on gsc.event_id=tbe.event_id 

left outer join public.tran_range_plan_events trpe on trpe.tran_bp_event_id=tbe.tran_bp_event_id

left outer join public.tran_range_plan trp on trp.tran_bp_year_id=tbe.tran_bp_year_id

where tby.fiscal_year_id=_fiscal_year_id;

		
END; 
$$;
 L   DROP FUNCTION public.sp_get_tran_range_plan_events(_fiscal_year_id bigint);
       public          postgres    false            z           1255    29948     sp_get_tran_range_plan_summary()    FUNCTION     �  CREATE FUNCTION public.sp_get_tran_range_plan_summary() RETURNS TABLE(year_name text, fiscal_year_id bigint, yearly_gross_sales numeric, yearly_total_mrp numeric, yearly_total_cpu numeric, yearly_total_quantity bigint, yearly_total_product bigint, range_plan_id bigint, tran_bp_year_id bigint, remarks text, is_submitted boolean, is_approved boolean, approved_by bigint, approve_date timestamp with time zone, approve_remarks text)
    LANGUAGE plpgsql
    AS $$
DECLARE 
 
Begin

	
		RETURN QUERY 	
		
		SELECT fy.year_name, tby.fiscal_year_id, tby.gross_sales yearly_gross_sales,

(select CAST(COALESCE(sum(total_mrp_value),0) as numeric) from public.tran_range_plan_events d where d.range_plan_id=trp.range_plan_id)
as yearly_total_mrp,
(select  CAST(COALESCE(sum(total_cpu_value),0) as numeric) from public.tran_range_plan_events d where d.range_plan_id=trp.range_plan_id)
as yearly_total_cpu,
(select  CAST(COALESCE(sum(total_range_plan_quantity),0) as bigint) from public.tran_range_plan_events d where d.range_plan_id=trp.range_plan_id)
as yearly_total_quantity,
(select  CAST(COALESCE(count(range_plan_detail_id),0) as bigint) from public.tran_range_plan_details d where d.range_plan_id=trp.range_plan_id)
as yearly_total_product

,trp.range_plan_id, trp.tran_bp_year_id, trp.remarks, trp.is_submitted, trp.is_approved, 
trp.approved_by, trp.approve_date, trp.approve_remarks
  
FROM public.tran_range_plan trp

right outer join public.tran_bp_year tby on tby.tran_bp_year_id=trp.tran_bp_year_id 

left outer join public.gen_fiscal_year fy on fy.fiscal_year_id= tby.fiscal_year_id

where  tby.is_approved=true

order by  tby.fiscal_year_id desc;

		
END; 
$$;
 7   DROP FUNCTION public.sp_get_tran_range_plan_summary();
       public          postgres    false            {           1255    29949    sp_get_tran_va_plan_summary()    FUNCTION     A  CREATE FUNCTION public.sp_get_tran_va_plan_summary() RETURNS TABLE(tran_va_plan_id bigint, year_name text, fiscal_year_id bigint, yearly_gross_sales numeric, yearly_total_mrp numeric, yearly_total_cpu numeric, yearly_total_quantity bigint, range_plan_id bigint, tran_bp_year_id bigint, remarks text, is_submitted boolean, is_approved boolean, approved_by bigint, approve_date timestamp with time zone, approve_remarks text)
    LANGUAGE plpgsql
    AS $$
DECLARE 
 
Begin

	
		RETURN QUERY 	
		
		SELECT tvp.tran_va_plan_id, fy.year_name, tby.fiscal_year_id, 
	tby.gross_sales yearly_gross_sales,

(select CAST(COALESCE(sum(total_mrp_value),0) as numeric) from public.tran_range_plan_events d where d.range_plan_id=trp.range_plan_id)
as yearly_total_mrp,
(select  CAST(COALESCE(sum(total_cpu_value),0) as numeric) from public.tran_range_plan_events d where d.range_plan_id=trp.range_plan_id)
as yearly_total_cpu,
(select  CAST(COALESCE(sum(total_range_plan_quantity),0) as bigint) from public.tran_range_plan_events d where d.range_plan_id=trp.range_plan_id)
as yearly_total_quantity

,trp.range_plan_id, trp.tran_bp_year_id, tvp.remarks, tvp.is_submitted, tvp.is_approved, 
tvp.approved_by, tvp.approve_date, tvp.approve_remarks

  
FROM public.tran_range_plan trp

right outer join public.tran_bp_year tby on tby.tran_bp_year_id=trp.tran_bp_year_id 

left outer join public.gen_fiscal_year fy on fy.fiscal_year_id= tby.fiscal_year_id

left outer join public.tran_va_plan tvp 
on tvp.tran_range_plan_id=trp.range_plan_id

where  tby.is_approved=true

order by  tby.fiscal_year_id desc;

		
END; 
$$;
 4   DROP FUNCTION public.sp_get_tran_va_plan_summary();
       public          postgres    false            |           1255    29950    sp_get_va_plan_events(bigint)    FUNCTION     �  CREATE FUNCTION public.sp_get_va_plan_events(_fiscal_year_id bigint) RETURNS TABLE(tran_va_plan_event_id bigint, tran_range_plan_event_id bigint, range_plan_id bigint, yearly_gross_sales numeric, tran_bp_event_id bigint, event_gross_sales numeric, readygoods_qnty bigint, readygoods_value numeric, event_id bigint, fiscal_year_id bigint, event_title text, total_range_plan_quantity bigint, total_mrp_value numeric, total_cpu_value numeric, is_finalised boolean, added_product bigint)
    LANGUAGE plpgsql
    AS $$
DECLARE 
 
Begin

		RETURN QUERY 	
		
		SELECT 
tvpe.tran_va_plan_event_id,
tvpe.tran_range_plan_event_id, trp.range_plan_id, tby.gross_sales yearly_gross_sales, tbe.tran_bp_event_id,
tbe.event_gross_sales,tbe.readygoods_qnty,tbe.readygoods_value,
tbe.event_id,tby.fiscal_year_id,gsc.event_title,
COALESCE(trpe.total_range_plan_quantity,0) as total_range_plan_quantity,
COALESCE(trpe.total_mrp_value,0)as total_mrp_Value,
 COALESCE(trpe.total_cpu_value,0) as total_cpu_value,
 tvpe.is_finalised,
 (
	 select count(det.tran_va_plan_detl_id) from  public.tran_va_plan_detl det
	  inner join public.tran_va_plan_events det_event 
	 on det_event.tran_va_plan_event_id=det.tran_va_plan_event_id
     inner join public.tran_va_plan master 
	 on master.tran_va_plan_id=det_event.tran_va_plan_id
     where  master.tran_range_plan_id=trp.range_plan_id
	 and det_event.tran_range_plan_event_id=trpe.tran_range_plan_event_id
 ) added_product
FROM public.tran_bp_event tbe

inner join public.tran_bp_year tby on tby.tran_bp_year_id=tbe.tran_bp_year_id

inner join public.gen_season_event_config gsc on gsc.event_id=tbe.event_id 

left outer join public.tran_range_plan_events trpe on trpe.tran_bp_event_id=tbe.tran_bp_event_id

left outer join public.tran_range_plan trp on trp.tran_bp_year_id=tbe.tran_bp_year_id

left outer join public.tran_va_plan_events tvpe on tvpe.tran_range_plan_event_id=
trpe.tran_range_plan_event_id

where tby.fiscal_year_id=_fiscal_year_id;

		
END; 
$$;
 D   DROP FUNCTION public.sp_get_va_plan_events(_fiscal_year_id bigint);
       public          postgres    false            }           1255    29951 +   va_plan_get_designer_assign_details(bigint)    FUNCTION     �  CREATE FUNCTION public.va_plan_get_designer_assign_details(tran_va_plan_event_id_filter bigint) RETURNS TABLE(no_of_style bigint, tran_va_plan_event_id bigint, tran_va_plan_id bigint, tran_va_plan_detl_id bigint, style_code_gen text, is_separate_price boolean, event_id bigint, tran_range_plan_event_id bigint, is_finalized boolean, tran_bp_event_id bigint, tran_bp_year_id bigint, total_rangeplan_mrp_value numeric, total_rangeplan_quantity bigint, total_rangeplan_cpu_value numeric, style_product_size_group_id bigint, is_submitted boolean, is_approved boolean, approved_by bigint, approve_date timestamp with time zone, approve_remarks text, remarks text, bp_yearly_gross_sales numeric, event_gross_sales numeric, range_plan_id bigint, range_plan_detail_id bigint, range_plan_quantity bigint, mrp_per_pc_value numeric, mrp_value numeric, cpu_per_pc_value numeric, cpu_value numeric, priority_id bigint, prev_year_quantity bigint, prev_year_efficiency numeric, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, added_by bigint, date_added timestamp with time zone, updated_by bigint, date_updated timestamp with time zone, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
  select 

  tvpd.no_of_style,
  tvpe.tran_va_plan_event_id,
  tvp.tran_va_plan_id,
  tvpd.tran_va_plan_detl_id,
 
	tvpd.style_code_gen,

spsg.is_separate_price,
tbe.event_id,
trpe.tran_range_plan_event_id,trpe.is_finalized,
trpe.tran_bp_event_id,
tby.tran_bp_year_id,
trpe.total_mrp_value as total_rangeplan_mrp_value ,
trpe.total_range_plan_quantity as total_rangeplan_quantity,
trpe.total_cpu_value as total_rangeplan_cpu_value,
vp.style_product_size_group_id,
rp.is_submitted,
    rp.is_approved,
    rp.approved_by,
    rp.approve_date,
    rp.approve_remarks,
	rp.remarks,

tby.gross_sales as bp_yearly_gross_sales,
tbe.event_gross_sales,

rpd.range_plan_id,rpd.range_plan_detail_id,

rpd.range_plan_quantity,
rpd.mrp_per_pc_value,
rpd.mrp_value,
rpd.cpu_per_pc_value,
rpd.cpu_value,
rpd.priority_id,
rpd.prev_year_quantity,
rpd.prev_year_efficiency,

vp.style_item_product_name, 
vp.style_item_type_name, 
vp.style_product_type_name, 
vp.style_item_origin_name, 
vp.style_gender_name, 

vp.master_category_name, 
rpd.added_by, rpd.date_added, 
rpd.updated_by, 
rpd.date_updated, 
vp.style_item_product_id, 
vp.style_item_type_id, 
vp.style_product_type_id, 
vp.style_item_origin_id, 
vp.style_gender_id, 
vp.style_master_category_id

from vw_style_item_product vp
inner join public.style_product_size_group spsg 
on spsg.style_product_size_group_id=vp.style_product_size_group_id
inner  join tran_range_plan_details rpd on rpd.style_item_product_id=vp.style_item_product_id

inner  join tran_range_plan rp on rp.range_plan_id=rpd.range_plan_id
inner  join tran_bp_year tby on tby.fiscal_year_id=vp.fiscal_year_id
inner  join tran_bp_event tbe on tbe.tran_bp_year_id=tby.tran_bp_year_id
and rpd.tran_bp_event_id=tbe.tran_bp_event_id
inner  join public.tran_range_plan_events trpe 
on  trpe.tran_bp_event_id=tbe.tran_bp_event_id
	
	

inner join 
public.tran_va_plan_detl tvpd on
	tvpd.style_item_product_id=vp.style_item_product_id
	and tvpd.range_plan_detail_id=rpd.range_plan_detail_id
left outer join 
(select  detl_single.style_item_product_id from public.tran_va_plan_detl detl_single 
group by detl_single.style_item_product_id
) det_single
on det_single.style_item_product_id=vp.style_item_product_id
inner join public.tran_va_plan_events tvpe on
	tvpe.tran_va_plan_event_id=tvpd.tran_va_plan_event_id
inner join public.tran_va_plan tvp on
	tvp.tran_va_plan_id=tvpe.tran_va_plan_id
	

where 

tvpe.tran_va_plan_event_id=tran_va_plan_event_id_filter;

END; 
$$;
 _   DROP FUNCTION public.va_plan_get_designer_assign_details(tran_va_plan_event_id_filter bigint);
       public          postgres    false            ~           1255    29952 /   va_plan_get_designer_assign_details_det(bigint)    FUNCTION       CREATE FUNCTION public.va_plan_get_designer_assign_details_det(tran_va_plan_event_id_filter bigint) RETURNS TABLE(style_item_product_id bigint, designer_member_id bigint, team_member_marketing_name text, no_of_style bigint, total_style_quantity bigint)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 select master.style_item_product_id,
det.designer_member_id,gtm.team_member_marketing_name,
cast( count(det.style_quantity) as bigint) as no_of_style,
cast (sum(det.style_quantity) as bigint) as total_style_quantity   from public.tran_va_plan_detl_style det
inner join public.tran_va_plan_detl master on master.tran_va_plan_detl_id=det.tran_va_plan_detl_id
inner join public.gen_team_members gtm on gtm.gen_team_members_id=det.designer_member_id
inner join public.tran_va_plan_events tvpe 
on tvpe.tran_va_plan_event_id=master.tran_va_plan_event_id
where det.designer_member_id!=0 and 
tvpe.tran_va_plan_event_id=tran_va_plan_event_id_filter
group by master.style_item_product_id,det.designer_member_id,gtm.team_member_marketing_name;

END; 
$$;
 c   DROP FUNCTION public.va_plan_get_designer_assign_details_det(tran_va_plan_event_id_filter bigint);
       public          postgres    false                       1255    29953 3   va_plan_getfor_createupdate(bigint, bigint, bigint)    FUNCTION     �  CREATE FUNCTION public.va_plan_getfor_createupdate(year_id_filter bigint, event_id_filter bigint, range_plan_id_filter bigint) RETURNS TABLE(total_assigned bigint, tran_va_plan_event_id bigint, tran_va_plan_id bigint, tran_va_plan_detl_id bigint, no_of_style bigint, style_code_gen text, is_separate_price boolean, event_id bigint, tran_range_plan_event_id bigint, is_finalized boolean, tran_bp_event_id bigint, tran_bp_year_id bigint, total_rangeplan_mrp_value numeric, total_rangeplan_quantity bigint, total_rangeplan_cpu_value numeric, style_product_size_group_id bigint, is_submitted boolean, is_approved boolean, approved_by bigint, approve_date timestamp with time zone, approve_remarks text, remarks text, bp_yearly_gross_sales numeric, event_gross_sales numeric, range_plan_id bigint, range_plan_detail_id bigint, range_plan_quantity bigint, mrp_per_pc_value numeric, mrp_value numeric, cpu_per_pc_value numeric, cpu_value numeric, priority_id bigint, prev_year_quantity bigint, prev_year_efficiency numeric, style_item_product_name character varying, style_item_type_name character varying, style_product_type_name character varying, style_item_origin_name text, style_gender_name text, master_category_name character varying, added_by bigint, date_added timestamp with time zone, updated_by bigint, date_updated timestamp with time zone, style_item_product_id bigint, style_item_type_id bigint, style_product_type_id bigint, style_item_origin_id bigint, style_gender_id bigint, style_master_category_id bigint)
    LANGUAGE plpgsql
    AS $$

 Begin
 RETURN QUERY
 
WITH cte_saved AS (
     select 
	  (select count(tmp1.designer_member_id) from public.tran_va_plan_detl_style tmp1 
  where tmp1.tran_va_plan_detl_id= tvpd.tran_va_plan_detl_id) as total_assigned,
  tvpe.tran_va_plan_event_id,
  tvp.tran_va_plan_id,
  tvpd.tran_va_plan_detl_id,
  tvpd.no_of_style,
	tvpd.style_code_gen,
	
spsg.is_separate_price,
tbe.event_id,
trpe.tran_range_plan_event_id,trpe.is_finalized,
trpe.tran_bp_event_id,
tby.tran_bp_year_id,
trpe.total_mrp_value as total_rangeplan_mrp_value ,
trpe.total_range_plan_quantity as total_rangeplan_quantity,
trpe.total_cpu_value as total_rangeplan_cpu_value,
vp.style_product_size_group_id,
rp.is_submitted,
    rp.is_approved,
    rp.approved_by,
    rp.approve_date,
    rp.approve_remarks,
	rp.remarks,

tby.gross_sales as bp_yearly_gross_sales,
tbe.event_gross_sales,

rpd.range_plan_id,rpd.range_plan_detail_id,

rpd.range_plan_quantity,
rpd.mrp_per_pc_value,
rpd.mrp_value,
rpd.cpu_per_pc_value,
rpd.cpu_value,
rpd.priority_id,
rpd.prev_year_quantity,
rpd.prev_year_efficiency,

vp.style_item_product_name, 
vp.style_item_type_name, 
vp.style_product_type_name, 
vp.style_item_origin_name, 
vp.style_gender_name, 

vp.master_category_name, 
rpd.added_by, rpd.date_added, 
rpd.updated_by, 
rpd.date_updated, 
vp.style_item_product_id, 
vp.style_item_type_id, 
vp.style_product_type_id, 
vp.style_item_origin_id, 
vp.style_gender_id, 
vp.style_master_category_id

from vw_style_item_product vp
inner join public.style_product_size_group spsg 
on spsg.style_product_size_group_id=vp.style_product_size_group_id
inner  join tran_range_plan_details rpd on rpd.style_item_product_id=vp.style_item_product_id

inner  join tran_range_plan rp on rp.range_plan_id=rpd.range_plan_id
inner  join tran_bp_year tby on tby.fiscal_year_id=vp.fiscal_year_id
inner  join tran_bp_event tbe on tbe.tran_bp_year_id=tby.tran_bp_year_id
and rpd.tran_bp_event_id=tbe.tran_bp_event_id
inner  join public.tran_range_plan_events trpe 
on  trpe.tran_bp_event_id=tbe.tran_bp_event_id
	
	

inner join 
public.tran_va_plan_detl tvpd on
	tvpd.style_item_product_id=vp.style_item_product_id
	and tvpd.range_plan_detail_id=rpd.range_plan_detail_id
left outer join 
(select  detl_single.style_item_product_id from public.tran_va_plan_detl detl_single 
group by detl_single.style_item_product_id
) det_single
on det_single.style_item_product_id=vp.style_item_product_id
inner join public.tran_va_plan_events tvpe on
	tvpe.tran_va_plan_event_id=tvpd.tran_va_plan_event_id
inner join public.tran_va_plan tvp on
	tvp.tran_va_plan_id=tvpe.tran_va_plan_id

where vp.fiscal_year_id	=year_id_filter

and tbe.event_id=event_id_filter

and rp.range_plan_id=range_plan_id_filter
)

select * from cte_saved
union
select
0,
  (select inr.tran_va_plan_event_id from public.tran_va_plan_events inr 
 where inr.tran_range_plan_event_id=trpe.tran_range_plan_event_id)  as tran_va_plan_event_id,
 (select inr.tran_va_plan_id from public.tran_va_plan inr 
 where inr.tran_range_plan_id=rp.range_plan_id) as tran_va_plan_id,
 NULL as tran_va_plan_detl_id,
   NULL as no_of_style,
	'' as style_code_gen,
	
spsg.is_separate_price,
tbe.event_id,
trpe.tran_range_plan_event_id,trpe.is_finalized,
trpe.tran_bp_event_id,
tby.tran_bp_year_id,
trpe.total_mrp_value as total_rangeplan_mrp_value ,
trpe.total_range_plan_quantity as total_rangeplan_quantity,
trpe.total_cpu_value as total_rangeplan_cpu_value,
vp.style_product_size_group_id,
rp.is_submitted,
    rp.is_approved,
    rp.approved_by,
    rp.approve_date,
    rp.approve_remarks,
	rp.remarks,

tby.gross_sales as bp_yearly_gross_sales,
tbe.event_gross_sales,

rpd.range_plan_id,rpd.range_plan_detail_id,

rpd.range_plan_quantity,
rpd.mrp_per_pc_value,
rpd.mrp_value,
rpd.cpu_per_pc_value,
rpd.cpu_value,
rpd.priority_id,
rpd.prev_year_quantity,
rpd.prev_year_efficiency,

vp.style_item_product_name, 
vp.style_item_type_name, 
vp.style_product_type_name, 
vp.style_item_origin_name, 
vp.style_gender_name, 

vp.master_category_name, 
rpd.added_by, rpd.date_added, 
rpd.updated_by, 
rpd.date_updated, 
vp.style_item_product_id, 
vp.style_item_type_id, 
vp.style_product_type_id, 
vp.style_item_origin_id, 
vp.style_gender_id, 
vp.style_master_category_id

from vw_style_item_product vp
inner join public.style_product_size_group spsg 
on spsg.style_product_size_group_id=vp.style_product_size_group_id
inner  join tran_range_plan_details rpd on rpd.style_item_product_id=vp.style_item_product_id

inner  join tran_range_plan rp on rp.range_plan_id=rpd.range_plan_id
inner  join tran_bp_year tby on tby.fiscal_year_id=vp.fiscal_year_id
inner  join tran_bp_event tbe on tbe.tran_bp_year_id=tby.tran_bp_year_id
and rpd.tran_bp_event_id=tbe.tran_bp_event_id
inner  join public.tran_range_plan_events trpe 
on  trpe.tran_bp_event_id=tbe.tran_bp_event_id

where vp.fiscal_year_id	=year_id_filter

and rp.range_plan_id=range_plan_id_filter

and tbe.event_id=event_id_filter and vp.style_item_product_id not in (select t.style_item_product_id from cte_saved t);
	
END; 
$$;
 ~   DROP FUNCTION public.va_plan_getfor_createupdate(year_id_filter bigint, event_id_filter bigint, range_plan_id_filter bigint);
       public          postgres    false            �           1255    29955 *   can_insert_object(text, text, uuid, jsonb)    FUNCTION     �  CREATE FUNCTION storage.can_insert_object(bucketid text, name text, owner uuid, metadata jsonb) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
  INSERT INTO "storage"."objects" ("bucket_id", "name", "owner", "metadata") VALUES (bucketid, name, owner, metadata);
  -- hack to rollback the successful insert
  RAISE sqlstate 'PT200' using
  message = 'ROLLBACK',
  detail = 'rollback successful insert';
END
$$;
 _   DROP FUNCTION storage.can_insert_object(bucketid text, name text, owner uuid, metadata jsonb);
       storage          postgres    false    15            �           1255    29956    extension(text)    FUNCTION     T  CREATE FUNCTION storage.extension(name text) RETURNS text
    LANGUAGE plpgsql
    AS $$
DECLARE
_parts text[];
_filename text;
BEGIN
    select string_to_array(name, '/') into _parts;
    select _parts[array_length(_parts,1)] into _filename;
    -- @todo return the last part instead of 2
    return split_part(_filename, '.', 2);
END
$$;
 ,   DROP FUNCTION storage.extension(name text);
       storage          postgres    false    15            �           1255    29957    filename(text)    FUNCTION     �   CREATE FUNCTION storage.filename(name text) RETURNS text
    LANGUAGE plpgsql
    AS $$
DECLARE
_parts text[];
BEGIN
    select string_to_array(name, '/') into _parts;
    return _parts[array_length(_parts,1)];
END
$$;
 +   DROP FUNCTION storage.filename(name text);
       storage          postgres    false    15            �           1255    29958    foldername(text)    FUNCTION     �   CREATE FUNCTION storage.foldername(name text) RETURNS text[]
    LANGUAGE plpgsql
    AS $$
DECLARE
_parts text[];
BEGIN
    select string_to_array(name, '/') into _parts;
    return _parts[1:array_length(_parts,1)-1];
END
$$;
 -   DROP FUNCTION storage.foldername(name text);
       storage          postgres    false    15            �           1255    29959    get_size_by_bucket()    FUNCTION        CREATE FUNCTION storage.get_size_by_bucket() RETURNS TABLE(size bigint, bucket_id text)
    LANGUAGE plpgsql
    AS $$
BEGIN
    return query
        select sum((metadata->>'size')::int) as size, obj.bucket_id
        from "storage".objects as obj
        group by obj.bucket_id;
END
$$;
 ,   DROP FUNCTION storage.get_size_by_bucket();
       storage          postgres    false    15            �           1255    29960 ?   search(text, text, integer, integer, integer, text, text, text)    FUNCTION     F  CREATE FUNCTION storage.search(prefix text, bucketname text, limits integer DEFAULT 100, levels integer DEFAULT 1, offsets integer DEFAULT 0, search text DEFAULT ''::text, sortcolumn text DEFAULT 'name'::text, sortorder text DEFAULT 'asc'::text) RETURNS TABLE(name text, id uuid, updated_at timestamp with time zone, created_at timestamp with time zone, last_accessed_at timestamp with time zone, metadata jsonb)
    LANGUAGE plpgsql STABLE
    AS $_$
declare
  v_order_by text;
  v_sort_order text;
begin
  case
    when sortcolumn = 'name' then
      v_order_by = 'name';
    when sortcolumn = 'updated_at' then
      v_order_by = 'updated_at';
    when sortcolumn = 'created_at' then
      v_order_by = 'created_at';
    when sortcolumn = 'last_accessed_at' then
      v_order_by = 'last_accessed_at';
    else
      v_order_by = 'name';
  end case;

  case
    when sortorder = 'asc' then
      v_sort_order = 'asc';
    when sortorder = 'desc' then
      v_sort_order = 'desc';
    else
      v_sort_order = 'asc';
  end case;

  v_order_by = v_order_by || ' ' || v_sort_order;

  return query execute
    'with folders as (
       select path_tokens[$1] as folder
       from storage.objects
         where objects.name ilike $2 || $3 || ''%''
           and bucket_id = $4
           and array_length(regexp_split_to_array(objects.name, ''/''), 1) <> $1
       group by folder
       order by folder ' || v_sort_order || '
     )
     (select folder as "name",
            null as id,
            null as updated_at,
            null as created_at,
            null as last_accessed_at,
            null as metadata from folders)
     union all
     (select path_tokens[$1] as "name",
            id,
            updated_at,
            created_at,
            last_accessed_at,
            metadata
     from storage.objects
     where objects.name ilike $2 || $3 || ''%''
       and bucket_id = $4
       and array_length(regexp_split_to_array(objects.name, ''/''), 1) = $1
     order by ' || v_order_by || ')
     limit $5
     offset $6' using levels, prefix, search, bucketname, limits, offsets;
end;
$_$;
 �   DROP FUNCTION storage.search(prefix text, bucketname text, limits integer, levels integer, offsets integer, search text, sortcolumn text, sortorder text);
       storage          postgres    false    15            �           1255    29961    update_updated_at_column()    FUNCTION     �   CREATE FUNCTION storage.update_updated_at_column() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    NEW.updated_at = now();
    RETURN NEW; 
END;
$$;
 2   DROP FUNCTION storage.update_updated_at_column();
       storage          postgres    false    15            :           1255    16974    secrets_encrypt_secret_secret()    FUNCTION     (  CREATE FUNCTION vault.secrets_encrypt_secret_secret() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
		BEGIN
		        new.secret = CASE WHEN new.secret IS NULL THEN NULL ELSE
			CASE WHEN new.key_id IS NULL THEN NULL ELSE pg_catalog.encode(
			  pgsodium.crypto_aead_det_encrypt(
				pg_catalog.convert_to(new.secret, 'utf8'),
				pg_catalog.convert_to((new.id::text || new.description::text || new.created_at::text || new.updated_at::text)::text, 'utf8'),
				new.key_id::uuid,
				new.nonce
			  ),
				'base64') END END;
		RETURN new;
		END;
		$$;
 5   DROP FUNCTION vault.secrets_encrypt_secret_secret();
       vault          supabase_admin    false    20            �            1259    29962    audit_log_entries    TABLE     �   CREATE TABLE auth.audit_log_entries (
    instance_id uuid,
    id uuid NOT NULL,
    payload json,
    created_at timestamp with time zone,
    ip_address character varying(64) DEFAULT ''::character varying NOT NULL
);
 #   DROP TABLE auth.audit_log_entries;
       auth         heap    postgres    false    16            �           0    0    TABLE audit_log_entries    COMMENT     R   COMMENT ON TABLE auth.audit_log_entries IS 'Auth: Audit trail for user actions.';
          auth          postgres    false    251            �            1259    29968 
   flow_state    TABLE     �  CREATE TABLE auth.flow_state (
    id uuid NOT NULL,
    user_id uuid,
    auth_code text NOT NULL,
    code_challenge_method auth.code_challenge_method NOT NULL,
    code_challenge text NOT NULL,
    provider_type text NOT NULL,
    provider_access_token text,
    provider_refresh_token text,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    authentication_method text NOT NULL
);
    DROP TABLE auth.flow_state;
       auth         heap    postgres    false    16    1391            �           0    0    TABLE flow_state    COMMENT     G   COMMENT ON TABLE auth.flow_state IS 'stores metadata for pkce logins';
          auth          postgres    false    252            �            1259    29973 
   identities    TABLE     �  CREATE TABLE auth.identities (
    provider_id text NOT NULL,
    user_id uuid NOT NULL,
    identity_data jsonb NOT NULL,
    provider text NOT NULL,
    last_sign_in_at timestamp with time zone,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    email text GENERATED ALWAYS AS (lower((identity_data ->> 'email'::text))) STORED,
    id uuid DEFAULT gen_random_uuid() NOT NULL
);
    DROP TABLE auth.identities;
       auth         heap    postgres    false    16            �           0    0    TABLE identities    COMMENT     U   COMMENT ON TABLE auth.identities IS 'Auth: Stores identities associated to a user.';
          auth          postgres    false    253            �           0    0    COLUMN identities.email    COMMENT     �   COMMENT ON COLUMN auth.identities.email IS 'Auth: Email is a generated column that references the optional email property in the identity_data';
          auth          postgres    false    253            �            1259    29980 	   instances    TABLE     �   CREATE TABLE auth.instances (
    id uuid NOT NULL,
    uuid uuid,
    raw_base_config text,
    created_at timestamp with time zone,
    updated_at timestamp with time zone
);
    DROP TABLE auth.instances;
       auth         heap    postgres    false    16            �           0    0    TABLE instances    COMMENT     Q   COMMENT ON TABLE auth.instances IS 'Auth: Manages users across multiple sites.';
          auth          postgres    false    254            �            1259    29985    mfa_amr_claims    TABLE     �   CREATE TABLE auth.mfa_amr_claims (
    session_id uuid NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone NOT NULL,
    authentication_method text NOT NULL,
    id uuid NOT NULL
);
     DROP TABLE auth.mfa_amr_claims;
       auth         heap    postgres    false    16            �           0    0    TABLE mfa_amr_claims    COMMENT     ~   COMMENT ON TABLE auth.mfa_amr_claims IS 'auth: stores authenticator method reference claims for multi factor authentication';
          auth          postgres    false    255                        1259    29990    mfa_challenges    TABLE     �   CREATE TABLE auth.mfa_challenges (
    id uuid NOT NULL,
    factor_id uuid NOT NULL,
    created_at timestamp with time zone NOT NULL,
    verified_at timestamp with time zone,
    ip_address inet NOT NULL
);
     DROP TABLE auth.mfa_challenges;
       auth         heap    postgres    false    16            �           0    0    TABLE mfa_challenges    COMMENT     _   COMMENT ON TABLE auth.mfa_challenges IS 'auth: stores metadata about challenge requests made';
          auth          postgres    false    256                       1259    29995    mfa_factors    TABLE     3  CREATE TABLE auth.mfa_factors (
    id uuid NOT NULL,
    user_id uuid NOT NULL,
    friendly_name text,
    factor_type auth.factor_type NOT NULL,
    status auth.factor_status NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone NOT NULL,
    secret text
);
    DROP TABLE auth.mfa_factors;
       auth         heap    postgres    false    16    1397    1394            �           0    0    TABLE mfa_factors    COMMENT     L   COMMENT ON TABLE auth.mfa_factors IS 'auth: stores metadata about factors';
          auth          postgres    false    257                       1259    30000    refresh_tokens    TABLE     8  CREATE TABLE auth.refresh_tokens (
    instance_id uuid,
    id bigint NOT NULL,
    token character varying(255),
    user_id character varying(255),
    revoked boolean,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    parent character varying(255),
    session_id uuid
);
     DROP TABLE auth.refresh_tokens;
       auth         heap    postgres    false    16            �           0    0    TABLE refresh_tokens    COMMENT     n   COMMENT ON TABLE auth.refresh_tokens IS 'Auth: Store of tokens used to refresh JWT tokens once they expire.';
          auth          postgres    false    258                       1259    30005    refresh_tokens_id_seq    SEQUENCE     |   CREATE SEQUENCE auth.refresh_tokens_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE auth.refresh_tokens_id_seq;
       auth          postgres    false    16    258            �           0    0    refresh_tokens_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE auth.refresh_tokens_id_seq OWNED BY auth.refresh_tokens.id;
          auth          postgres    false    259                       1259    30006    saml_providers    TABLE     /  CREATE TABLE auth.saml_providers (
    id uuid NOT NULL,
    sso_provider_id uuid NOT NULL,
    entity_id text NOT NULL,
    metadata_xml text NOT NULL,
    metadata_url text,
    attribute_mapping jsonb,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    CONSTRAINT "entity_id not empty" CHECK ((char_length(entity_id) > 0)),
    CONSTRAINT "metadata_url not empty" CHECK (((metadata_url = NULL::text) OR (char_length(metadata_url) > 0))),
    CONSTRAINT "metadata_xml not empty" CHECK ((char_length(metadata_xml) > 0))
);
     DROP TABLE auth.saml_providers;
       auth         heap    postgres    false    16            �           0    0    TABLE saml_providers    COMMENT     ]   COMMENT ON TABLE auth.saml_providers IS 'Auth: Manages SAML Identity Provider connections.';
          auth          postgres    false    260                       1259    30014    saml_relay_states    TABLE     z  CREATE TABLE auth.saml_relay_states (
    id uuid NOT NULL,
    sso_provider_id uuid NOT NULL,
    request_id text NOT NULL,
    for_email text,
    redirect_to text,
    from_ip_address inet,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    flow_state_id uuid,
    CONSTRAINT "request_id not empty" CHECK ((char_length(request_id) > 0))
);
 #   DROP TABLE auth.saml_relay_states;
       auth         heap    postgres    false    16            �           0    0    TABLE saml_relay_states    COMMENT     �   COMMENT ON TABLE auth.saml_relay_states IS 'Auth: Contains SAML Relay State information for each Service Provider initiated login.';
          auth          postgres    false    261                       1259    30020    schema_migrations    TABLE     U   CREATE TABLE auth.schema_migrations (
    version character varying(255) NOT NULL
);
 #   DROP TABLE auth.schema_migrations;
       auth         heap    postgres    false    16            �           0    0    TABLE schema_migrations    COMMENT     X   COMMENT ON TABLE auth.schema_migrations IS 'Auth: Manages updates to the auth system.';
          auth          postgres    false    262                       1259    30023    sessions    TABLE     T  CREATE TABLE auth.sessions (
    id uuid NOT NULL,
    user_id uuid NOT NULL,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    factor_id uuid,
    aal auth.aal_level,
    not_after timestamp with time zone,
    refreshed_at timestamp without time zone,
    user_agent text,
    ip inet,
    tag text
);
    DROP TABLE auth.sessions;
       auth         heap    postgres    false    1388    16            �           0    0    TABLE sessions    COMMENT     U   COMMENT ON TABLE auth.sessions IS 'Auth: Stores session data associated to a user.';
          auth          postgres    false    263            �           0    0    COLUMN sessions.not_after    COMMENT     �   COMMENT ON COLUMN auth.sessions.not_after IS 'Auth: Not after is a nullable column that contains a timestamp after which the session should be regarded as expired.';
          auth          postgres    false    263                       1259    30028    sso_domains    TABLE       CREATE TABLE auth.sso_domains (
    id uuid NOT NULL,
    sso_provider_id uuid NOT NULL,
    domain text NOT NULL,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    CONSTRAINT "domain not empty" CHECK ((char_length(domain) > 0))
);
    DROP TABLE auth.sso_domains;
       auth         heap    postgres    false    16            �           0    0    TABLE sso_domains    COMMENT     t   COMMENT ON TABLE auth.sso_domains IS 'Auth: Manages SSO email address domain mapping to an SSO Identity Provider.';
          auth          postgres    false    264            	           1259    30035    sso_providers    TABLE       CREATE TABLE auth.sso_providers (
    id uuid NOT NULL,
    resource_id text,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    CONSTRAINT "resource_id not empty" CHECK (((resource_id = NULL::text) OR (char_length(resource_id) > 0)))
);
    DROP TABLE auth.sso_providers;
       auth         heap    postgres    false    16            �           0    0    TABLE sso_providers    COMMENT     x   COMMENT ON TABLE auth.sso_providers IS 'Auth: Manages SSO identity provider information; see saml_providers for SAML.';
          auth          postgres    false    265            �           0    0     COLUMN sso_providers.resource_id    COMMENT     �   COMMENT ON COLUMN auth.sso_providers.resource_id IS 'Auth: Uniquely identifies a SSO provider according to a user-chosen resource ID (case insensitive), useful in infrastructure as code.';
          auth          postgres    false    265            
           1259    30041    users    TABLE       CREATE TABLE auth.users (
    instance_id uuid,
    id uuid NOT NULL,
    aud character varying(255),
    role character varying(255),
    email character varying(255),
    encrypted_password character varying(255),
    email_confirmed_at timestamp with time zone,
    invited_at timestamp with time zone,
    confirmation_token character varying(255),
    confirmation_sent_at timestamp with time zone,
    recovery_token character varying(255),
    recovery_sent_at timestamp with time zone,
    email_change_token_new character varying(255),
    email_change character varying(255),
    email_change_sent_at timestamp with time zone,
    last_sign_in_at timestamp with time zone,
    raw_app_meta_data jsonb,
    raw_user_meta_data jsonb,
    is_super_admin boolean,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    phone text DEFAULT NULL::character varying,
    phone_confirmed_at timestamp with time zone,
    phone_change text DEFAULT ''::character varying,
    phone_change_token character varying(255) DEFAULT ''::character varying,
    phone_change_sent_at timestamp with time zone,
    confirmed_at timestamp with time zone GENERATED ALWAYS AS (LEAST(email_confirmed_at, phone_confirmed_at)) STORED,
    email_change_token_current character varying(255) DEFAULT ''::character varying,
    email_change_confirm_status smallint DEFAULT 0,
    banned_until timestamp with time zone,
    reauthentication_token character varying(255) DEFAULT ''::character varying,
    reauthentication_sent_at timestamp with time zone,
    is_sso_user boolean DEFAULT false NOT NULL,
    deleted_at timestamp with time zone,
    CONSTRAINT users_email_change_confirm_status_check CHECK (((email_change_confirm_status >= 0) AND (email_change_confirm_status <= 2)))
);
    DROP TABLE auth.users;
       auth         heap    postgres    false    16            �           0    0    TABLE users    COMMENT     W   COMMENT ON TABLE auth.users IS 'Auth: Stores user login data within a secure schema.';
          auth          postgres    false    266            �           0    0    COLUMN users.is_sso_user    COMMENT     �   COMMENT ON COLUMN auth.users.is_sso_user IS 'Auth: Set this column to true when the account comes from SSO. These accounts can have duplicate emails.';
          auth          postgres    false    266                       1259    30055    group_user_security_rule    TABLE       CREATE TABLE public.group_user_security_rule (
    child_id bigint NOT NULL,
    group_code bigint NOT NULL,
    security_rule_code bigint,
    added_by bigint,
    updated_by bigint,
    date_added timestamp without time zone,
    date_updated timestamp without time zone
);
 ,   DROP TABLE public.group_user_security_rule;
       public         heap    postgres    false                       1259    30058 *   Security.GroupUserSecurityRule_ChildID_seq    SEQUENCE       ALTER TABLE public.group_user_security_rule ALTER COLUMN child_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Security.GroupUserSecurityRule_ChildID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    267                       1259    30059 #   login_user_attached_with_group_user    TABLE     (  CREATE TABLE public.login_user_attached_with_group_user (
    child_id bigint NOT NULL,
    user_code bigint NOT NULL,
    group_code bigint NOT NULL,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone,
    date_updated timestamp without time zone
);
 7   DROP TABLE public.login_user_attached_with_group_user;
       public         heap    postgres    false                       1259    30062 3   Security.LoginUserAttachedWithGroupUser_ChildID_seq    SEQUENCE       ALTER TABLE public.login_user_attached_with_group_user ALTER COLUMN child_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Security.LoginUserAttachedWithGroupUser_ChildID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    269                       1259    30063    security_rule_menu    TABLE     l  CREATE TABLE public.security_rule_menu (
    master_id bigint NOT NULL,
    menu_id bigint NOT NULL,
    security_rule_code bigint,
    can_select boolean,
    can_insert boolean,
    can_update boolean,
    can_delete boolean,
    added_by bigint,
    updated_by bigint,
    date_added timestamp without time zone,
    date_updated timestamp without time zone
);
 &   DROP TABLE public.security_rule_menu;
       public         heap    postgres    false                       1259    30066 &   Security.SecurityRuleMenu_MasterID_seq    SEQUENCE     �   ALTER TABLE public.security_rule_menu ALTER COLUMN master_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Security.SecurityRuleMenu_MasterID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    271                       1259    30067    gen_department    TABLE     �  CREATE TABLE public.gen_department (
    department_id bigint NOT NULL,
    department_code character varying NOT NULL,
    department_name character varying NOT NULL,
    department_description text,
    is_default boolean,
    department_logo_path character varying,
    sequence_no smallint,
    added_by bigint DEFAULT 0,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone
);
 "   DROP TABLE public.gen_department;
       public         heap    postgres    false                       1259    30073    gen_district    TABLE     l   CREATE TABLE public.gen_district (
    district_id bigint NOT NULL,
    name text,
    divisionid bigint
);
     DROP TABLE public.gen_district;
       public         heap    postgres    false                       1259    30078    gen_division    TABLE     U   CREATE TABLE public.gen_division (
    division_id bigint NOT NULL,
    name text
);
     DROP TABLE public.gen_division;
       public         heap    postgres    false                       1259    30083    gen_fiscal_year    TABLE     �  CREATE TABLE public.gen_fiscal_year (
    fiscal_year_id bigint NOT NULL,
    year_no bigint NOT NULL,
    year_name text NOT NULL,
    start_date date NOT NULL,
    end_date date NOT NULL,
    lock boolean NOT NULL,
    is_used boolean NOT NULL,
    added_by bigint,
    date_added timestamp without time zone NOT NULL,
    update_by bigint,
    date_updated timestamp without time zone
);
 #   DROP TABLE public.gen_fiscal_year;
       public         heap    postgres    false                       1259    30088    gen_item_structure_group    TABLE       CREATE TABLE public.gen_item_structure_group (
    item_structure_group_id bigint NOT NULL,
    structure_group_name text,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
 ,   DROP TABLE public.gen_item_structure_group;
       public         heap    postgres    false                       1259    30093 4   gen_item_structure_group_item_structure_group_id_seq    SEQUENCE       ALTER TABLE public.gen_item_structure_group ALTER COLUMN item_structure_group_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.gen_item_structure_group_item_structure_group_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    277                       1259    30094    gen_item_structure_group_sub    TABLE     B  CREATE TABLE public.gen_item_structure_group_sub (
    gen_item_structure_group_sub_id bigint NOT NULL,
    item_structure_group_id bigint NOT NULL,
    sub_group_name text,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
 0   DROP TABLE public.gen_item_structure_group_sub;
       public         heap    postgres    false                       1259    30099 ?   gen_item_structure_group_sub_gen_item_structure_group_sub_i_seq    SEQUENCE     ,  ALTER TABLE public.gen_item_structure_group_sub ALTER COLUMN gen_item_structure_group_sub_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.gen_item_structure_group_sub_gen_item_structure_group_sub_i_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    279                       1259    30100 
   gen_outlet    TABLE     �  CREATE TABLE public.gen_outlet (
    outlet_id bigint NOT NULL,
    outlet_code text,
    outlet_name character varying NOT NULL,
    outlet_description character varying,
    district_id bigint,
    division_id bigint,
    outlet_address character varying,
    is_active boolean,
    outlet_logo_path character varying,
    sequence_no bigint,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone
);
    DROP TABLE public.gen_outlet;
       public         heap    postgres    false                       1259    30105    gen_priority    TABLE     �   CREATE TABLE public.gen_priority (
    priority_id bigint NOT NULL,
    priority_name text,
    added_by bigint,
    updated_by bigint,
    date_added timestamp with time zone,
    date_updated timestamp with time zone
);
     DROP TABLE public.gen_priority;
       public         heap    postgres    false                       1259    30110    gen_priority_priority_id_seq    SEQUENCE     �   ALTER TABLE public.gen_priority ALTER COLUMN priority_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.gen_priority_priority_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    282                       1259    30111 
   gen_season    TABLE     C  CREATE TABLE public.gen_season (
    season_id bigint NOT NULL,
    season_name text NOT NULL,
    short_name text NOT NULL,
    is_active boolean,
    added_by bigint NOT NULL,
    date_added timestamp without time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp without time zone,
    sequence bigint
);
    DROP TABLE public.gen_season;
       public         heap    postgres    false                       1259    30116    gen_season_event_config    TABLE     �  CREATE TABLE public.gen_season_event_config (
    event_id bigint NOT NULL,
    season_id bigint,
    fiscal_year_id bigint,
    start_date timestamp with time zone NOT NULL,
    start_month_id bigint NOT NULL,
    end_month_id bigint NOT NULL,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp without time zone,
    event_title text,
    is_active boolean,
    end_date timestamp with time zone NOT NULL
);
 +   DROP TABLE public.gen_season_event_config;
       public         heap    postgres    false            �           1259    30955    gen_segment    TABLE     R  CREATE TABLE public.gen_segment (
    gen_segment_id bigint NOT NULL,
    gen_segment_name text NOT NULL,
    display_name text NOT NULL,
    is_item_segment boolean,
    is_active boolean,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
    DROP TABLE public.gen_segment;
       public         heap    postgres    false            �           1259    30929    gen_segment_detl    TABLE     @  CREATE TABLE public.gen_segment_detl (
    gen_segment_detl_id bigint NOT NULL,
    gen_segment_id bigint NOT NULL,
    segment_value text NOT NULL,
    is_active boolean,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
 $   DROP TABLE public.gen_segment_detl;
       public         heap    postgres    false            �           1259    30928 (   gen_segment_detl_gen_segment_detl_id_seq    SEQUENCE       ALTER TABLE public.gen_segment_detl ALTER COLUMN gen_segment_detl_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.gen_segment_detl_gen_segment_detl_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    385            �           1259    30954    gen_segment_gen_segment_id_seq    SEQUENCE     �   ALTER TABLE public.gen_segment ALTER COLUMN gen_segment_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.gen_segment_gen_segment_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    391                       1259    30121    gen_team_group    TABLE     9  CREATE TABLE public.gen_team_group (
    gen_team_group_id bigint NOT NULL,
    department_id bigint,
    team_group_name text,
    team_head_name text,
    team_head_id_number text,
    added_by bigint,
    updated_by bigint,
    date_added timestamp with time zone,
    date_updated timestamp with time zone
);
 "   DROP TABLE public.gen_team_group;
       public         heap    postgres    false                       1259    30126 $   gen_team_group_gen_team_group_id_seq    SEQUENCE     �   ALTER TABLE public.gen_team_group ALTER COLUMN gen_team_group_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.gen_team_group_gen_team_group_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    286                        1259    30127    gen_team_members    TABLE     �  CREATE TABLE public.gen_team_members (
    gen_team_members_id bigint NOT NULL,
    gen_team_group_id bigint,
    team_member_name text,
    team_member_marketing_name text,
    team_member_marketing_code text,
    team_member_designation text,
    phone_number text,
    email text,
    office_extension_number text,
    added_by bigint,
    updated_by bigint,
    date_added timestamp with time zone,
    date_updated timestamp with time zone,
    photo text,
    user_id bigint
);
 $   DROP TABLE public.gen_team_members;
       public         heap    postgres    false            !           1259    30132 (   gen_team_members_gen_team_members_id_seq    SEQUENCE     �   ALTER TABLE public.gen_team_members ALTER COLUMN gen_team_members_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.gen_team_members_gen_team_members_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    288            "           1259    30133    gen_unit    TABLE     �   CREATE TABLE public.gen_unit (
    gen_unit_id bigint NOT NULL,
    unit_name text,
    remarks text,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
    DROP TABLE public.gen_unit;
       public         heap    postgres    false            #           1259    30138    gen_unit_gen_unit_id_seq    SEQUENCE     �   ALTER TABLE public.gen_unit ALTER COLUMN gen_unit_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.gen_unit_gen_unit_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    290            $           1259    30139    menu    TABLE     �  CREATE TABLE public.menu (
    menu_id integer NOT NULL,
    parent_id integer,
    menu_caption character varying,
    navigate_url character varying,
    image_url character varying,
    seq_no integer,
    is_visible boolean DEFAULT false,
    is_api boolean DEFAULT false,
    added_by integer DEFAULT 0,
    updated_by integer DEFAULT 0,
    date_added timestamp without time zone,
    date_updated timestamp without time zone
);
    DROP TABLE public.menu;
       public         heap    postgres    false            %           1259    30148    month    TABLE     `   CREATE TABLE public.month (
    month_id bigint NOT NULL,
    name text,
    short_name text
);
    DROP TABLE public.month;
       public         heap    postgres    false            &           1259    30153    outlet_outlet_id_seq    SEQUENCE     �   ALTER TABLE public.gen_outlet ALTER COLUMN outlet_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.outlet_outlet_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    281            '           1259    30154    owin_formaction    TABLE     N  CREATE TABLE public.owin_formaction (
    formactionid bigint NOT NULL,
    appformid bigint,
    actionname text NOT NULL,
    displaynamear text,
    displayname text,
    methodtype bigint,
    actiontype text,
    isview bit(1),
    isapi bit(1),
    isshowonmenu bit(1),
    classicon text,
    isitem bit(1),
    eventname text,
    sequence bigint,
    profiletypeformodule bigint,
    transid text NOT NULL,
    ipaddress text,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone
);
 #   DROP TABLE public.owin_formaction;
       public         heap    postgres    false            (           1259    30159     owin_formaction_formactionid_seq    SEQUENCE     �   ALTER TABLE public.owin_formaction ALTER COLUMN formactionid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.owin_formaction_formactionid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    295            )           1259    30160    owin_forminfo    TABLE       CREATE TABLE public.owin_forminfo (
    formid bigint NOT NULL,
    formname text NOT NULL,
    parentid bigint,
    levelid bigint,
    menulevel text,
    formnamear text,
    hasdirectchild bit(1),
    icon text,
    classicon text,
    sequence integer,
    url text,
    isview boolean,
    isdynamic boolean,
    issuperadmin boolean,
    isvisibleinmenu boolean,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone
);
 !   DROP TABLE public.owin_forminfo;
       public         heap    postgres    false            *           1259    30165    owin_forminfo_formid_seq    SEQUENCE     �   ALTER TABLE public.owin_forminfo ALTER COLUMN formid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.owin_forminfo_formid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    297            +           1259    30166 	   owin_role    TABLE     �   CREATE TABLE public.owin_role (
    roleid bigint NOT NULL,
    rolename text NOT NULL,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone
);
    DROP TABLE public.owin_role;
       public         heap    postgres    false            ,           1259    30171    owin_role_permission    TABLE     �  CREATE TABLE public.owin_role_permission (
    role_permission_id bigint NOT NULL,
    rolename text NOT NULL,
    roleid bigint NOT NULL,
    formactionid bigint NOT NULL,
    appformid bigint NOT NULL,
    status bit(1) NOT NULL,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone
);
 (   DROP TABLE public.owin_role_permission;
       public         heap    postgres    false            -           1259    30176 +   owin_role_permission_role_permission_id_seq    SEQUENCE       ALTER TABLE public.owin_role_permission ALTER COLUMN role_permission_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.owin_role_permission_role_permission_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    300            .           1259    30177    owin_role_roleid_seq    SEQUENCE     �   ALTER TABLE public.owin_role ALTER COLUMN roleid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.owin_role_roleid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    299            /           1259    30178 	   owin_user    TABLE     A  CREATE TABLE public.owin_user (
    userid bigint NOT NULL,
    name character varying NOT NULL,
    user_name character varying NOT NULL,
    password character varying NOT NULL,
    email character varying,
    email_password character varying,
    is_super_user boolean,
    is_admin boolean,
    is_active boolean NOT NULL,
    is_loggedin boolean,
    logon_time timestamp without time zone,
    employee_code bigint,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone
);
    DROP TABLE public.owin_user;
       public         heap    postgres    false            0           1259    30183    owin_user_userid_seq    SEQUENCE     �   ALTER TABLE public.owin_user ALTER COLUMN userid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.owin_user_userid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    303            1           1259    30184    owin_userrole    TABLE     5  CREATE TABLE public.owin_userrole (
    userroleid bigint NOT NULL,
    userid bigint NOT NULL,
    roleid bigint NOT NULL,
    isenable boolean NOT NULL,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone
);
 !   DROP TABLE public.owin_userrole;
       public         heap    postgres    false            2           1259    30187    owin_userrole_userroleid_seq    SEQUENCE     �   ALTER TABLE public.owin_userrole ALTER COLUMN userroleid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.owin_userrole_userroleid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    305            3           1259    30188    serial_number_seq    SEQUENCE     z   CREATE SEQUENCE public.serial_number_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.serial_number_seq;
       public          postgres    false            4           1259    30189    style_category    TABLE       CREATE TABLE public.style_category (
    style_category_id bigint NOT NULL,
    style_master_category_id bigint NOT NULL,
    style_category_name character varying NOT NULL,
    style_category_code character varying NOT NULL,
    has_sleeve boolean NOT NULL,
    has_hoody boolean NOT NULL,
    has_length boolean NOT NULL,
    has_fit boolean NOT NULL,
    is_active boolean NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp without time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp without time zone
);
 "   DROP TABLE public.style_category;
       public         heap    postgres    false            5           1259    30194    style_embellishment    TABLE       CREATE TABLE public.style_embellishment (
    style_embellishment_id bigint NOT NULL,
    style_embellishment_name character varying NOT NULL,
    added_by bigint,
    date_added timestamp without time zone,
    updated_by bigint,
    date_updated timestamp without time zone
);
 '   DROP TABLE public.style_embellishment;
       public         heap    postgres    false            6           1259    30199 .   style_embellishment_style_embellishment_id_seq    SEQUENCE       ALTER TABLE public.style_embellishment ALTER COLUMN style_embellishment_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_embellishment_style_embellishment_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    309            �           1259    30935    style_fabric    TABLE       CREATE TABLE public.style_fabric (
    style_fabric_id bigint NOT NULL,
    style_fabric_name text NOT NULL,
    is_active boolean,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
     DROP TABLE public.style_fabric;
       public         heap    postgres    false            �           1259    30941    style_fabric_detl    TABLE     e  CREATE TABLE public.style_fabric_detl (
    style_fabric_detl_id bigint NOT NULL,
    style_fabric_id bigint NOT NULL,
    gen_segment_id bigint NOT NULL,
    segment_display_name text,
    is_active boolean,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
 %   DROP TABLE public.style_fabric_detl;
       public         heap    postgres    false            �           1259    30940 *   style_fabric_detl_style_fabric_detl_id_seq    SEQUENCE       ALTER TABLE public.style_fabric_detl ALTER COLUMN style_fabric_detl_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_fabric_detl_style_fabric_detl_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    389            �           1259    30934     style_fabric_style_fabric_id_seq    SEQUENCE     �   ALTER TABLE public.style_fabric ALTER COLUMN style_fabric_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_fabric_style_fabric_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    387            7           1259    30200    style_gender    TABLE     C  CREATE TABLE public.style_gender (
    style_gender_id bigint NOT NULL,
    style_gender_name text NOT NULL,
    style_gender_code text NOT NULL,
    is_active boolean NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp without time zone,
    updated_by bigint,
    date_updated timestamp without time zone
);
     DROP TABLE public.style_gender;
       public         heap    postgres    false            8           1259    30205    style_item_origin    TABLE     6  CREATE TABLE public.style_item_origin (
    style_item_origin_id bigint NOT NULL,
    style_item_origin_name text NOT NULL,
    is_active boolean NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp without time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp without time zone
);
 %   DROP TABLE public.style_item_origin;
       public         heap    postgres    false            9           1259    30210 *   style_item_origin_style_item_origin_id_seq    SEQUENCE       ALTER TABLE public.style_item_origin ALTER COLUMN style_item_origin_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_item_origin_style_item_origin_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    312            :           1259    30211    style_item_product    TABLE     E  CREATE TABLE public.style_item_product (
    style_item_product_id bigint NOT NULL,
    style_item_product_name character varying NOT NULL,
    style_item_type_id bigint NOT NULL,
    style_product_type_id bigint NOT NULL,
    style_item_origin_id bigint NOT NULL,
    style_gender_id bigint NOT NULL,
    style_master_category_id bigint NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone,
    fiscal_year_id bigint NOT NULL,
    style_product_size_group_id bigint NOT NULL
);
 &   DROP TABLE public.style_item_product;
       public         heap    postgres    false            ;           1259    30216 ,   style_item_product_style_item_product_id_seq    SEQUENCE     	  ALTER TABLE public.style_item_product ALTER COLUMN style_item_product_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_item_product_style_item_product_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    314            <           1259    30217    style_item_product_sub_category    TABLE     _  CREATE TABLE public.style_item_product_sub_category (
    style_item_product_sub_category_id bigint NOT NULL,
    style_item_product_id bigint NOT NULL,
    sub_category_name character varying NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
 3   DROP TABLE public.style_item_product_sub_category;
       public         heap    postgres    false            =           1259    30222 ?   style_item_product_sub_catego_style_item_product_sub_catego_seq    SEQUENCE     6  ALTER TABLE public.style_item_product_sub_category ALTER COLUMN style_item_product_sub_category_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_item_product_sub_catego_style_item_product_sub_catego_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    316            >           1259    30223    style_item_type    TABLE     i  CREATE TABLE public.style_item_type (
    style_item_type_id bigint NOT NULL,
    style_item_type_name character varying NOT NULL,
    style_item_type_code character varying,
    is_active boolean NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp without time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp without time zone
);
 #   DROP TABLE public.style_item_type;
       public         heap    postgres    false            ?           1259    30228 &   style_item_type_style_item_type_id_seq    SEQUENCE     �   ALTER TABLE public.style_item_type ALTER COLUMN style_item_type_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_item_type_style_item_type_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    318            @           1259    30229    style_label    TABLE     �  CREATE TABLE public.style_label (
    style_label_id bigint NOT NULL,
    style_label_name character varying NOT NULL,
    short_name character varying NOT NULL,
    label_code character varying,
    label_description character varying,
    is_active boolean,
    added_by bigint NOT NULL,
    date_added timestamp without time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp without time zone
);
    DROP TABLE public.style_label;
       public         heap    postgres    false            A           1259    30234    style_master_category    TABLE     I  CREATE TABLE public.style_master_category (
    style_master_category_id bigint NOT NULL,
    master_category_name character varying NOT NULL,
    is_active boolean NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp without time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp without time zone
);
 )   DROP TABLE public.style_master_category;
       public         heap    postgres    false            B           1259    30239 0   style_master_category_structure_subgroup_mapping    TABLE     �  CREATE TABLE public.style_master_category_structure_subgroup_mapping (
    master_category_structure_subgroup_mapping_id bigint NOT NULL,
    style_master_category_id bigint NOT NULL,
    gen_item_structure_group_sub_id bigint NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
 D   DROP TABLE public.style_master_category_structure_subgroup_mapping;
       public         heap    postgres    false            C           1259    30242 ?   style_master_category_structu_master_category_structure_sub_seq    SEQUENCE     N  ALTER TABLE public.style_master_category_structure_subgroup_mapping ALTER COLUMN master_category_structure_subgroup_mapping_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.style_master_category_structu_master_category_structure_sub_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    322            D           1259    30243    style_product_size_group    TABLE     D  CREATE TABLE public.style_product_size_group (
    style_product_size_group_id bigint NOT NULL,
    style_product_size_group_name text NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone,
    is_separate_price boolean
);
 ,   DROP TABLE public.style_product_size_group;
       public         heap    postgres    false            E           1259    30248     style_product_size_group_details    TABLE     V  CREATE TABLE public.style_product_size_group_details (
    style_product_size_group_detid bigint NOT NULL,
    style_product_size_group_id bigint NOT NULL,
    style_product_size text NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
 4   DROP TABLE public.style_product_size_group_details;
       public         heap    postgres    false            F           1259    30253 ?   style_product_size_group_deta_style_product_size_group_deti_seq    SEQUENCE     3  ALTER TABLE public.style_product_size_group_details ALTER COLUMN style_product_size_group_detid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_product_size_group_deta_style_product_size_group_deti_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    325            G           1259    30254 8   style_product_size_group_style_product_size_group_id_seq    SEQUENCE     !  ALTER TABLE public.style_product_size_group ALTER COLUMN style_product_size_group_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_product_size_group_style_product_size_group_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    324            H           1259    30255    style_product_type    TABLE     &  CREATE TABLE public.style_product_type (
    style_product_type_id bigint NOT NULL,
    style_product_type_name character varying NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp without time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp without time zone
);
 &   DROP TABLE public.style_product_type;
       public         heap    postgres    false            I           1259    30260 ,   style_product_type_style_product_type_id_seq    SEQUENCE     	  ALTER TABLE public.style_product_type ALTER COLUMN style_product_type_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_product_type_style_product_type_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    328            J           1259    30261    style_product_unit    TABLE     &  CREATE TABLE public.style_product_unit (
    style_product_unit_id bigint NOT NULL,
    style_product_unit_name character varying NOT NULL,
    added_by bigint NOT NULL,
    date_added timestamp without time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp without time zone
);
 &   DROP TABLE public.style_product_unit;
       public         heap    postgres    false            K           1259    30266 ,   style_product_unit_style_product_unit_id_seq    SEQUENCE     	  ALTER TABLE public.style_product_unit ALTER COLUMN style_product_unit_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.style_product_unit_style_product_unit_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    330            L           1259    30267     tbl_department_department_id_seq    SEQUENCE     �   ALTER TABLE public.gen_department ALTER COLUMN department_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tbl_department_department_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    273            M           1259    30268    tbl_district_districtid_seq    SEQUENCE     �   ALTER TABLE public.gen_district ALTER COLUMN district_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.tbl_district_districtid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    274            N           1259    30269 "   tbl_fiscal_year_fiscal_year_id_seq    SEQUENCE     �   ALTER TABLE public.gen_fiscal_year ALTER COLUMN fiscal_year_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tbl_fiscal_year_fiscal_year_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    276            O           1259    30270    tbl_menu_menu_id_seq    SEQUENCE     �   ALTER TABLE public.menu ALTER COLUMN menu_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tbl_menu_menu_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    292            P           1259    30271    tbl_month_monthid_seq    SEQUENCE     �   ALTER TABLE public.month ALTER COLUMN month_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.tbl_month_monthid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    293            Q           1259    30272    tbl_season_master_season_id_seq    SEQUENCE     �   ALTER TABLE public.gen_season ALTER COLUMN season_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tbl_season_master_season_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    284            R           1259    30273    tbl_season_month_config_id_seq    SEQUENCE     �   ALTER TABLE public.gen_season_event_config ALTER COLUMN event_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tbl_season_month_config_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    285            S           1259    30274 (   tbl_style_category_style_category_id_seq    SEQUENCE     �   ALTER TABLE public.style_category ALTER COLUMN style_category_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tbl_style_category_style_category_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    308            T           1259    30275 "   tbl_style_label_style_label_id_seq    SEQUENCE     �   ALTER TABLE public.style_label ALTER COLUMN style_label_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tbl_style_label_style_label_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    320            U           1259    30276 6   tbl_style_master_category_style_master_category_id_seq    SEQUENCE       ALTER TABLE public.style_master_category ALTER COLUMN style_master_category_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tbl_style_master_category_style_master_category_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    321            V           1259    30277    tran_bp_year    TABLE     �  CREATE TABLE public.tran_bp_year (
    tran_bp_year_id bigint NOT NULL,
    fiscal_year_id bigint,
    gross_sales numeric,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone,
    is_approved boolean,
    approved_by bigint,
    approve_date timestamp with time zone,
    approve_remarks text,
    remarks text,
    is_submitted boolean
);
     DROP TABLE public.tran_bp_year;
       public         heap    postgres    false            W           1259    30282 '   tbl_tran_business_plan_year_data_id_seq    SEQUENCE     �   ALTER TABLE public.tran_bp_year ALTER COLUMN tran_bp_year_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tbl_tran_business_plan_year_data_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    342            X           1259    30283    tran_bp_event    TABLE     n  CREATE TABLE public.tran_bp_event (
    tran_bp_event_id bigint NOT NULL,
    event_id bigint,
    event_gross_sales numeric,
    readygoods_qnty bigint,
    readygoods_value numeric,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone,
    tran_bp_year_id bigint
);
 !   DROP TABLE public.tran_bp_event;
       public         heap    postgres    false            Y           1259    30288    tran_bp_event_month    TABLE     C  CREATE TABLE public.tran_bp_event_month (
    tran_bp_event_month_id bigint NOT NULL,
    tran_bp_event_id bigint,
    month_id bigint,
    monthly_gross_sales numeric,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone
);
 '   DROP TABLE public.tran_bp_event_month;
       public         heap    postgres    false            Z           1259    30293    tran_bp_event_month_outlet    TABLE     W  CREATE TABLE public.tran_bp_event_month_outlet (
    tran_bp_event_month_outlet_id bigint NOT NULL,
    tran_bp_event_month_id bigint,
    outlet_id bigint,
    outlet_gross_sales numeric,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp without time zone NOT NULL,
    date_updated timestamp without time zone
);
 .   DROP TABLE public.tran_bp_event_month_outlet;
       public         heap    postgres    false            [           1259    30298 %   tran_bp_season_event_bp_season_id_seq    SEQUENCE     �   ALTER TABLE public.tran_bp_event ALTER COLUMN tran_bp_event_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_bp_season_event_bp_season_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    344            \           1259    30299 +   tran_bp_season_month_bp_season_month_id_seq    SEQUENCE     
  ALTER TABLE public.tran_bp_event_month ALTER COLUMN tran_bp_event_month_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_bp_season_month_bp_season_month_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    345            ]           1259    30300 9   tran_bp_season_month_outlet_bp_season_month_outlet_id_seq    SEQUENCE     &  ALTER TABLE public.tran_bp_event_month_outlet ALTER COLUMN tran_bp_event_month_outlet_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_bp_season_month_outlet_bp_season_month_outlet_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    346            ^           1259    30301    tran_range_plan    TABLE     �  CREATE TABLE public.tran_range_plan (
    range_plan_id bigint NOT NULL,
    tran_bp_year_id bigint,
    remarks text,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone,
    is_submitted boolean,
    is_approved boolean,
    approved_by bigint,
    approve_date timestamp with time zone,
    approve_remarks text
);
 #   DROP TABLE public.tran_range_plan;
       public         heap    postgres    false            _           1259    30306    tran_range_plan_details    TABLE     y  CREATE TABLE public.tran_range_plan_details (
    range_plan_detail_id bigint NOT NULL,
    range_plan_id bigint NOT NULL,
    tran_bp_event_id bigint NOT NULL,
    style_item_product_id bigint NOT NULL,
    range_plan_quantity bigint NOT NULL,
    mrp_per_pc_value numeric NOT NULL,
    mrp_value numeric NOT NULL,
    cpu_per_pc_value numeric NOT NULL,
    cpu_value numeric NOT NULL,
    priority_id bigint NOT NULL,
    prev_year_quantity bigint,
    prev_year_efficiency numeric,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone
);
 +   DROP TABLE public.tran_range_plan_details;
       public         heap    postgres    false            `           1259    30311 0   tran_range_plan_details_range_plan_detail_id_seq    SEQUENCE       ALTER TABLE public.tran_range_plan_details ALTER COLUMN range_plan_detail_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_range_plan_details_range_plan_detail_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    351            a           1259    30312    tran_range_plan_details_size    TABLE     �  CREATE TABLE public.tran_range_plan_details_size (
    range_plan_detail_size_id bigint NOT NULL,
    range_plan_detail_id bigint NOT NULL,
    style_product_size_group_detid bigint NOT NULL,
    size_quantity bigint NOT NULL,
    size_per_pc_mrp_value numeric,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone,
    size_per_pc_cpu_value numeric
);
 0   DROP TABLE public.tran_range_plan_details_size;
       public         heap    postgres    false            b           1259    30317 :   tran_range_plan_details_size_range_plan_detail_size_id_seq    SEQUENCE     %  ALTER TABLE public.tran_range_plan_details_size ALTER COLUMN range_plan_detail_size_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_range_plan_details_size_range_plan_detail_size_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    353            c           1259    30318    tran_range_plan_events    TABLE     �  CREATE TABLE public.tran_range_plan_events (
    tran_range_plan_event_id bigint NOT NULL,
    range_plan_id bigint,
    tran_bp_event_id bigint,
    total_mrp_value numeric,
    total_cpu_value numeric,
    total_range_plan_quantity bigint,
    is_finalized boolean,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone
);
 *   DROP TABLE public.tran_range_plan_events;
       public         heap    postgres    false            d           1259    30323 3   tran_range_plan_events_tran_range_plan_event_id_seq    SEQUENCE       ALTER TABLE public.tran_range_plan_events ALTER COLUMN tran_range_plan_event_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_range_plan_events_tran_range_plan_event_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    355            e           1259    30324 !   tran_range_plan_range_plan_id_seq    SEQUENCE     �   ALTER TABLE public.tran_range_plan ALTER COLUMN range_plan_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_range_plan_range_plan_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    350            f           1259    30325    tran_sample_order    TABLE     �  CREATE TABLE public.tran_sample_order (
    tran_sample_order_id bigint NOT NULL,
    tran_va_plan_detl_id bigint NOT NULL,
    tran_sample_order_number text,
    order_date timestamp with time zone NOT NULL,
    delivery_date timestamp with time zone NOT NULL,
    delivery_unit_id bigint NOT NULL,
    order_quantity bigint NOT NULL,
    designer_member_id bigint NOT NULL,
    remarks text,
    is_submit boolean,
    is_approved boolean,
    approved_by bigint,
    date_approved timestamp with time zone,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone,
    sample_photos json
);
 %   DROP TABLE public.tran_sample_order;
       public         heap    postgres    false            g           1259    30330    tran_sample_order_detl    TABLE     �  CREATE TABLE public.tran_sample_order_detl (
    tran_sample_order_detl_id bigint NOT NULL,
    tran_sample_order_id bigint,
    color_code text,
    style_product_size_group_detid bigint,
    order_quantity bigint,
    style_product_unit_id bigint,
    remarks text,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
 *   DROP TABLE public.tran_sample_order_detl;
       public         heap    postgres    false            h           1259    30335 4   tran_sample_order_detl_tran_sample_order_detl_id_seq    SEQUENCE       ALTER TABLE public.tran_sample_order_detl ALTER COLUMN tran_sample_order_detl_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_sample_order_detl_tran_sample_order_detl_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    359            i           1259    30336    tran_sample_order_embellishment    TABLE     d  CREATE TABLE public.tran_sample_order_embellishment (
    tran_sample_order_embellishment_id bigint NOT NULL,
    tran_sample_order_id bigint NOT NULL,
    embellishment_id bigint NOT NULL,
    remarks text,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
 3   DROP TABLE public.tran_sample_order_embellishment;
       public         heap    postgres    false            j           1259    30341 ?   tran_sample_order_embellishme_tran_sample_order_embellishme_seq    SEQUENCE     2  ALTER TABLE public.tran_sample_order_embellishment ALTER COLUMN tran_sample_order_embellishment_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.tran_sample_order_embellishme_tran_sample_order_embellishme_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    361            k           1259    30342    tran_sample_order_subgroup    TABLE     i  CREATE TABLE public.tran_sample_order_subgroup (
    tran_sample_order_subgroup_id bigint NOT NULL,
    tran_sample_order_id bigint NOT NULL,
    gen_item_structure_group_sub_id bigint NOT NULL,
    remarks text,
    added_by bigint NOT NULL,
    date_added timestamp with time zone NOT NULL,
    updated_by bigint,
    date_updated timestamp with time zone
);
 .   DROP TABLE public.tran_sample_order_subgroup;
       public         heap    postgres    false            l           1259    30347 <   tran_sample_order_subgroup_tran_sample_order_subgroup_id_seq    SEQUENCE     %  ALTER TABLE public.tran_sample_order_subgroup ALTER COLUMN tran_sample_order_subgroup_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.tran_sample_order_subgroup_tran_sample_order_subgroup_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    363            m           1259    30348 *   tran_sample_order_tran_sample_order_id_seq    SEQUENCE       ALTER TABLE public.tran_sample_order ALTER COLUMN tran_sample_order_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.tran_sample_order_tran_sample_order_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    358            n           1259    30349    tran_va_plan    TABLE     �  CREATE TABLE public.tran_va_plan (
    tran_va_plan_id bigint NOT NULL,
    tran_range_plan_id bigint,
    remarks text,
    is_submitted boolean,
    is_approved boolean,
    approved_by bigint,
    approve_date timestamp with time zone,
    approve_remarks text,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone
);
     DROP TABLE public.tran_va_plan;
       public         heap    postgres    false            o           1259    30354    tran_va_plan_detl    TABLE     �  CREATE TABLE public.tran_va_plan_detl (
    tran_va_plan_detl_id bigint NOT NULL,
    tran_va_plan_event_id bigint NOT NULL,
    range_plan_detail_id bigint NOT NULL,
    style_item_product_id bigint NOT NULL,
    no_of_style bigint NOT NULL,
    style_code_gen text NOT NULL,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone
);
 %   DROP TABLE public.tran_va_plan_detl;
       public         heap    postgres    false            p           1259    30359    tran_va_plan_detl_style    TABLE       CREATE TABLE public.tran_va_plan_detl_style (
    tran_va_plan_detl_style_id bigint NOT NULL,
    tran_va_plan_detl_id bigint NOT NULL,
    style_code text NOT NULL,
    style_quantity bigint NOT NULL,
    no_of_color bigint NOT NULL,
    color_code_gen text NOT NULL,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone,
    style_embellishment_ids json,
    designer_member_id bigint,
    style_item_product_sub_category_id bigint
);
 +   DROP TABLE public.tran_va_plan_detl_style;
       public         heap    postgres    false            q           1259    30364    tran_va_plan_detl_style_color    TABLE     v  CREATE TABLE public.tran_va_plan_detl_style_color (
    tran_va_plan_detl_style_color_id bigint NOT NULL,
    tran_va_plan_detl_style_id bigint NOT NULL,
    color_code text NOT NULL,
    style_color_quantity bigint NOT NULL,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone
);
 1   DROP TABLE public.tran_va_plan_detl_style_color;
       public         heap    postgres    false            r           1259    30369 "   tran_va_plan_detl_style_color_size    TABLE     �  CREATE TABLE public.tran_va_plan_detl_style_color_size (
    tran_va_plan_detl_style_color_size_id bigint NOT NULL,
    tran_va_plan_detl_style_color_id bigint NOT NULL,
    style_product_size_group_detid bigint NOT NULL,
    style_color_size_quantity bigint NOT NULL,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone
);
 6   DROP TABLE public.tran_va_plan_detl_style_color_size;
       public         heap    postgres    false            s           1259    30372 ?   tran_va_plan_detl_style_color_tran_va_plan_detl_style_colo_seq1    SEQUENCE     <  ALTER TABLE public.tran_va_plan_detl_style_color_size ALTER COLUMN tran_va_plan_detl_style_color_size_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_va_plan_detl_style_color_tran_va_plan_detl_style_colo_seq1
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    370            t           1259    30373 ?   tran_va_plan_detl_style_color_tran_va_plan_detl_style_color_seq    SEQUENCE     2  ALTER TABLE public.tran_va_plan_detl_style_color ALTER COLUMN tran_va_plan_detl_style_color_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_va_plan_detl_style_color_tran_va_plan_detl_style_color_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    369            u           1259    30374 6   tran_va_plan_detl_style_tran_va_plan_detl_style_id_seq    SEQUENCE       ALTER TABLE public.tran_va_plan_detl_style ALTER COLUMN tran_va_plan_detl_style_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_va_plan_detl_style_tran_va_plan_detl_style_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    368            v           1259    30375 *   tran_va_plan_detl_tran_va_plan_detl_id_seq    SEQUENCE       ALTER TABLE public.tran_va_plan_detl ALTER COLUMN tran_va_plan_detl_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_va_plan_detl_tran_va_plan_detl_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    367            �            1259    29060    tran_va_plan_events    TABLE     D  CREATE TABLE public.tran_va_plan_events (
    tran_va_plan_event_id bigint NOT NULL,
    tran_va_plan_id bigint,
    tran_range_plan_event_id bigint,
    is_finalised boolean,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone
);
 '   DROP TABLE public.tran_va_plan_events;
       public         heap    postgres    false            �            1259    29059 .   tran_va_plan_events_tran_va_plan_events_id_seq    SEQUENCE       ALTER TABLE public.tran_va_plan_events ALTER COLUMN tran_va_plan_event_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_va_plan_events_tran_va_plan_events_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    247            w           1259    30376     tran_va_plan_tran_va_plan_id_seq    SEQUENCE     �   ALTER TABLE public.tran_va_plan ALTER COLUMN tran_va_plan_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_va_plan_tran_va_plan_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    366            x           1259    30377 %   tran_va_product_embellishment_mapping    TABLE     R  CREATE TABLE public.tran_va_product_embellishment_mapping (
    tran_va_product_embellishment_mapping_id bigint NOT NULL,
    style_item_product_id bigint,
    style_embelishment_id bigint,
    added_by bigint NOT NULL,
    updated_by bigint,
    date_added timestamp with time zone NOT NULL,
    date_updated timestamp with time zone
);
 9   DROP TABLE public.tran_va_product_embellishment_mapping;
       public         heap    postgres    false            y           1259    30380 ?   tran_va_product_embellishment_tran_va_product_embellishment_seq    SEQUENCE     B  ALTER TABLE public.tran_va_product_embellishment_mapping ALTER COLUMN tran_va_product_embellishment_mapping_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.tran_va_product_embellishment_tran_va_product_embellishment_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    376            z           1259    30381    vw_product_value_add_plan    VIEW     �  CREATE VIEW public.vw_product_value_add_plan AS
 SELECT stp.style_item_product_name,
    sit.style_item_type_name,
    spt.style_product_type_name,
    sio.style_item_origin_name,
    sg.style_gender_name,
    smc.master_category_name,
    trpd.style_item_product_id,
    trpd.range_plan_detail_id,
    tvp.tran_range_plan_id,
    tvp.remarks,
    tvp.is_submitted,
    tvp.is_approved,
    tvp.approved_by,
    tvp.approve_date,
    tvp.approve_remarks,
    tvpd.tran_va_plan_detl_id,
    tvpd.tran_va_plan_event_id,
    tvpd.no_of_style,
    tvpd.style_code_gen
   FROM ((((((((public.tran_range_plan_details trpd
     LEFT JOIN public.tran_va_plan_detl tvpd ON ((tvpd.range_plan_detail_id = trpd.range_plan_detail_id)))
     LEFT JOIN public.tran_va_plan tvp ON ((tvp.tran_va_plan_id = tvpd.tran_va_plan_event_id)))
     LEFT JOIN public.style_item_product stp ON ((stp.style_item_product_id = trpd.style_item_product_id)))
     LEFT JOIN public.style_item_type sit ON ((sit.style_item_type_id = stp.style_item_type_id)))
     LEFT JOIN public.style_product_type spt ON ((spt.style_product_type_id = stp.style_product_type_id)))
     LEFT JOIN public.style_item_origin sio ON ((sio.style_item_origin_id = stp.style_item_origin_id)))
     LEFT JOIN public.style_gender sg ON ((sg.style_gender_id = stp.style_gender_id)))
     LEFT JOIN public.style_master_category smc ON ((smc.style_master_category_id = stp.style_master_category_id)));
 ,   DROP VIEW public.vw_product_value_add_plan;
       public          postgres    false    367    367    367    367    366    366    366    366    366    366    366    366    351    351    328    328    321    321    318    318    314    314    314    314    314    314    314    312    312    311    311    367            {           1259    30386    vw_style_item_product    VIEW     �  CREATE VIEW public.vw_style_item_product AS
 SELECT spsgd.style_product_size_group_name,
    stp.style_item_product_name,
    sit.style_item_type_name,
    spt.style_product_type_name,
    sio.style_item_origin_name,
    sg.style_gender_name,
    smc.master_category_name,
    stp.added_by,
    stp.date_added,
    stp.updated_by,
    stp.date_updated,
    stp.style_item_product_id,
    stp.style_item_type_id,
    stp.style_product_type_id,
    stp.style_item_origin_id,
    stp.style_gender_id,
    stp.style_master_category_id,
    stp.fiscal_year_id,
    stp.style_product_size_group_id
   FROM ((((((public.style_item_product stp
     JOIN public.style_item_type sit ON ((sit.style_item_type_id = stp.style_item_type_id)))
     JOIN public.style_product_type spt ON ((spt.style_product_type_id = stp.style_product_type_id)))
     JOIN public.style_item_origin sio ON ((sio.style_item_origin_id = stp.style_item_origin_id)))
     JOIN public.style_gender sg ON ((sg.style_gender_id = stp.style_gender_id)))
     JOIN public.style_master_category smc ON ((smc.style_master_category_id = stp.style_master_category_id)))
     JOIN public.style_product_size_group spsgd ON ((spsgd.style_product_size_group_id = stp.style_product_size_group_id)));
 (   DROP VIEW public.vw_style_item_product;
       public          postgres    false    314    324    328    328    324    321    321    311    311    312    312    318    318    314    314    314    314    314    314    314    314    314    314    314    314            |           1259    30391    vw_va_detl_style    VIEW     8  CREATE VIEW public.vw_va_detl_style AS
 SELECT spsgd.style_product_size,
    vpdscs.tran_va_plan_detl_style_color_size_id,
    vpdscs.tran_va_plan_detl_style_color_id,
    vpdscs.style_product_size_group_detid,
    vpdscs.style_color_size_quantity,
    vpdsc.style_color_quantity,
    vpdsc.color_code,
    vpdsc.tran_va_plan_detl_style_id,
    vpds.color_code_gen,
    vpds.no_of_color,
    vpds.style_quantity,
    vpds.style_code,
    vpds.tran_va_plan_detl_id,
    vpd.style_code_gen,
    vpd.no_of_style,
    vpd.style_item_product_id,
    vpd.range_plan_detail_id,
    vpd.tran_va_plan_event_id,
    sip.style_item_product_name,
    vpds.style_embellishment_ids,
    vpds.style_item_product_sub_category_id,
    sipsc.sub_category_name,
    vpds.designer_member_id
   FROM ((((((public.tran_va_plan_detl_style_color_size vpdscs
     JOIN public.tran_va_plan_detl_style_color vpdsc ON ((vpdsc.tran_va_plan_detl_style_color_id = vpdscs.tran_va_plan_detl_style_color_id)))
     JOIN public.tran_va_plan_detl_style vpds ON ((vpds.tran_va_plan_detl_style_id = vpdsc.tran_va_plan_detl_style_id)))
     JOIN public.tran_va_plan_detl vpd ON ((vpd.tran_va_plan_detl_id = vpds.tran_va_plan_detl_id)))
     JOIN public.style_item_product sip ON ((sip.style_item_product_id = vpd.style_item_product_id)))
     JOIN public.style_product_size_group_details spsgd ON ((spsgd.style_product_size_group_detid = vpdscs.style_product_size_group_detid)))
     LEFT JOIN public.style_item_product_sub_category sipsc ON ((vpds.style_item_product_sub_category_id = sipsc.style_item_product_sub_category_id)));
 #   DROP VIEW public.vw_va_detl_style;
       public          postgres    false    368    370    370    370    370    369    369    369    369    368    368    314    314    316    316    325    325    367    367    368    367    367    367    367    368    368    368    368    368            }           1259    30396    buckets    TABLE     k  CREATE TABLE storage.buckets (
    id text NOT NULL,
    name text NOT NULL,
    owner uuid,
    created_at timestamp with time zone DEFAULT now(),
    updated_at timestamp with time zone DEFAULT now(),
    public boolean DEFAULT false,
    avif_autodetection boolean DEFAULT false,
    file_size_limit bigint,
    allowed_mime_types text[],
    owner_id text
);
    DROP TABLE storage.buckets;
       storage         heap    postgres    false    15            �           0    0    COLUMN buckets.owner    COMMENT     X   COMMENT ON COLUMN storage.buckets.owner IS 'Field is deprecated, use owner_id instead';
          storage          postgres    false    381            ~           1259    30405 
   migrations    TABLE     �   CREATE TABLE storage.migrations (
    id integer NOT NULL,
    name character varying(100) NOT NULL,
    hash character varying(40) NOT NULL,
    executed_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);
    DROP TABLE storage.migrations;
       storage         heap    postgres    false    15                       1259    30409    objects    TABLE     �  CREATE TABLE storage.objects (
    id uuid DEFAULT extensions.uuid_generate_v4() NOT NULL,
    bucket_id text,
    name text,
    owner uuid,
    created_at timestamp with time zone DEFAULT now(),
    updated_at timestamp with time zone DEFAULT now(),
    last_accessed_at timestamp with time zone DEFAULT now(),
    metadata jsonb,
    path_tokens text[] GENERATED ALWAYS AS (string_to_array(name, '/'::text)) STORED,
    version text,
    owner_id text
);
    DROP TABLE storage.objects;
       storage         heap    postgres    false    8    14    15            �           0    0    COLUMN objects.owner    COMMENT     X   COMMENT ON COLUMN storage.objects.owner IS 'Field is deprecated, use owner_id instead';
          storage          postgres    false    383            �            1259    16970    decrypted_secrets    VIEW     �  CREATE VIEW vault.decrypted_secrets AS
 SELECT secrets.id,
    secrets.name,
    secrets.description,
    secrets.secret,
        CASE
            WHEN (secrets.secret IS NULL) THEN NULL::text
            ELSE
            CASE
                WHEN (secrets.key_id IS NULL) THEN NULL::text
                ELSE convert_from(pgsodium.crypto_aead_det_decrypt(decode(secrets.secret, 'base64'::text), convert_to(((((secrets.id)::text || secrets.description) || (secrets.created_at)::text) || (secrets.updated_at)::text), 'utf8'::name), secrets.key_id, secrets.nonce), 'utf8'::name)
            END
        END AS decrypted_secret,
    secrets.key_id,
    secrets.nonce,
    secrets.created_at,
    secrets.updated_at
   FROM vault.secrets;
 #   DROP VIEW vault.decrypted_secrets;
       vault          supabase_admin    false    3    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    18    2    18    20    3    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    18    2    18    20    3    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    18    2    18    20    2    18    3    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    18    2    18    20    3    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    18    2    18    20    3    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    18    2    18    20    3    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    18    2    18    20    3    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    2    18    2    18    18    2    18    2    18    2    18    2    18    18    2    18    2    18    2    18    20    20            \           2604    30419    refresh_tokens id    DEFAULT     r   ALTER TABLE ONLY auth.refresh_tokens ALTER COLUMN id SET DEFAULT nextval('auth.refresh_tokens_id_seq'::regclass);
 >   ALTER TABLE auth.refresh_tokens ALTER COLUMN id DROP DEFAULT;
       auth          postgres    false    259    258            3          0    29962    audit_log_entries 
   TABLE DATA           [   COPY auth.audit_log_entries (instance_id, id, payload, created_at, ip_address) FROM stdin;
    auth          postgres    false    251   �s      4          0    29968 
   flow_state 
   TABLE DATA           �   COPY auth.flow_state (id, user_id, auth_code, code_challenge_method, code_challenge, provider_type, provider_access_token, provider_refresh_token, created_at, updated_at, authentication_method) FROM stdin;
    auth          postgres    false    252   �s      5          0    29973 
   identities 
   TABLE DATA           ~   COPY auth.identities (provider_id, user_id, identity_data, provider, last_sign_in_at, created_at, updated_at, id) FROM stdin;
    auth          postgres    false    253   �s      6          0    29980 	   instances 
   TABLE DATA           T   COPY auth.instances (id, uuid, raw_base_config, created_at, updated_at) FROM stdin;
    auth          postgres    false    254   �s      7          0    29985    mfa_amr_claims 
   TABLE DATA           e   COPY auth.mfa_amr_claims (session_id, created_at, updated_at, authentication_method, id) FROM stdin;
    auth          postgres    false    255   t      8          0    29990    mfa_challenges 
   TABLE DATA           Z   COPY auth.mfa_challenges (id, factor_id, created_at, verified_at, ip_address) FROM stdin;
    auth          postgres    false    256   t      9          0    29995    mfa_factors 
   TABLE DATA           t   COPY auth.mfa_factors (id, user_id, friendly_name, factor_type, status, created_at, updated_at, secret) FROM stdin;
    auth          postgres    false    257   <t      :          0    30000    refresh_tokens 
   TABLE DATA           |   COPY auth.refresh_tokens (instance_id, id, token, user_id, revoked, created_at, updated_at, parent, session_id) FROM stdin;
    auth          postgres    false    258   Yt      <          0    30006    saml_providers 
   TABLE DATA           �   COPY auth.saml_providers (id, sso_provider_id, entity_id, metadata_xml, metadata_url, attribute_mapping, created_at, updated_at) FROM stdin;
    auth          postgres    false    260   vt      =          0    30014    saml_relay_states 
   TABLE DATA           �   COPY auth.saml_relay_states (id, sso_provider_id, request_id, for_email, redirect_to, from_ip_address, created_at, updated_at, flow_state_id) FROM stdin;
    auth          postgres    false    261   �t      >          0    30020    schema_migrations 
   TABLE DATA           2   COPY auth.schema_migrations (version) FROM stdin;
    auth          postgres    false    262   �t      ?          0    30023    sessions 
   TABLE DATA           �   COPY auth.sessions (id, user_id, created_at, updated_at, factor_id, aal, not_after, refreshed_at, user_agent, ip, tag) FROM stdin;
    auth          postgres    false    263   �u      @          0    30028    sso_domains 
   TABLE DATA           X   COPY auth.sso_domains (id, sso_provider_id, domain, created_at, updated_at) FROM stdin;
    auth          postgres    false    264   �u      A          0    30035    sso_providers 
   TABLE DATA           N   COPY auth.sso_providers (id, resource_id, created_at, updated_at) FROM stdin;
    auth          postgres    false    265   v      B          0    30041    users 
   TABLE DATA           A  COPY auth.users (instance_id, id, aud, role, email, encrypted_password, email_confirmed_at, invited_at, confirmation_token, confirmation_sent_at, recovery_token, recovery_sent_at, email_change_token_new, email_change, email_change_sent_at, last_sign_in_at, raw_app_meta_data, raw_user_meta_data, is_super_admin, created_at, updated_at, phone, phone_confirmed_at, phone_change, phone_change_token, phone_change_sent_at, email_change_token_current, email_change_confirm_status, banned_until, reauthentication_token, reauthentication_sent_at, is_sso_user, deleted_at) FROM stdin;
    auth          postgres    false    266   /v      J          0    16790    key 
   TABLE DATA           �   COPY pgsodium.key (id, status, created, expires, key_type, key_id, key_context, name, associated_data, raw_key, raw_key_nonce, parent_key, comment, user_data) FROM stdin;
    pgsodium          supabase_admin    false    238   Lv      I          0    30067    gen_department 
   TABLE DATA           �   COPY public.gen_department (department_id, department_code, department_name, department_description, is_default, department_logo_path, sequence_no, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    273   iv      J          0    30073    gen_district 
   TABLE DATA           E   COPY public.gen_district (district_id, name, divisionid) FROM stdin;
    public          postgres    false    274   �v      K          0    30078    gen_division 
   TABLE DATA           9   COPY public.gen_division (division_id, name) FROM stdin;
    public          postgres    false    275   �v      L          0    30083    gen_fiscal_year 
   TABLE DATA           �   COPY public.gen_fiscal_year (fiscal_year_id, year_no, year_name, start_date, end_date, lock, is_used, added_by, date_added, update_by, date_updated) FROM stdin;
    public          postgres    false    276    w      M          0    30088    gen_item_structure_group 
   TABLE DATA           �   COPY public.gen_item_structure_group (item_structure_group_id, structure_group_name, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    277   Mw      O          0    30094    gen_item_structure_group_sub 
   TABLE DATA           �   COPY public.gen_item_structure_group_sub (gen_item_structure_group_sub_id, item_structure_group_id, sub_group_name, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    279   �w      Q          0    30100 
   gen_outlet 
   TABLE DATA           �   COPY public.gen_outlet (outlet_id, outlet_code, outlet_name, outlet_description, district_id, division_id, outlet_address, is_active, outlet_logo_path, sequence_no, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    281   Gx      R          0    30105    gen_priority 
   TABLE DATA           r   COPY public.gen_priority (priority_id, priority_name, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    282   �y      T          0    30111 
   gen_season 
   TABLE DATA           �   COPY public.gen_season (season_id, season_name, short_name, is_active, added_by, date_added, updated_by, date_updated, sequence) FROM stdin;
    public          postgres    false    284   �y      U          0    30116    gen_season_event_config 
   TABLE DATA           �   COPY public.gen_season_event_config (event_id, season_id, fiscal_year_id, start_date, start_month_id, end_month_id, added_by, updated_by, date_added, date_updated, event_title, is_active, end_date) FROM stdin;
    public          postgres    false    285   1z      �          0    30955    gen_segment 
   TABLE DATA           �   COPY public.gen_segment (gen_segment_id, gen_segment_name, display_name, is_item_segment, is_active, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    391   p{      �          0    30929    gen_segment_detl 
   TABLE DATA           �   COPY public.gen_segment_detl (gen_segment_detl_id, gen_segment_id, segment_value, is_active, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    385   �{      V          0    30121    gen_team_group 
   TABLE DATA           �   COPY public.gen_team_group (gen_team_group_id, department_id, team_group_name, team_head_name, team_head_id_number, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    286   ��      X          0    30127    gen_team_members 
   TABLE DATA           #  COPY public.gen_team_members (gen_team_members_id, gen_team_group_id, team_member_name, team_member_marketing_name, team_member_marketing_code, team_member_designation, phone_number, email, office_extension_number, added_by, updated_by, date_added, date_updated, photo, user_id) FROM stdin;
    public          postgres    false    288   ��      Z          0    30133    gen_unit 
   TABLE DATA           s   COPY public.gen_unit (gen_unit_id, unit_name, remarks, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    290   _�      C          0    30055    group_user_security_rule 
   TABLE DATA           �   COPY public.group_user_security_rule (child_id, group_code, security_rule_code, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    267   ��      E          0    30059 #   login_user_attached_with_group_user 
   TABLE DATA           �   COPY public.login_user_attached_with_group_user (child_id, user_code, group_code, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    269    �      \          0    30139    menu 
   TABLE DATA           �   COPY public.menu (menu_id, parent_id, menu_caption, navigate_url, image_url, seq_no, is_visible, is_api, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    292   ?�      ]          0    30148    month 
   TABLE DATA           ;   COPY public.month (month_id, name, short_name) FROM stdin;
    public          postgres    false    293   ��      _          0    30154    owin_formaction 
   TABLE DATA           !  COPY public.owin_formaction (formactionid, appformid, actionname, displaynamear, displayname, methodtype, actiontype, isview, isapi, isshowonmenu, classicon, isitem, eventname, sequence, profiletypeformodule, transid, ipaddress, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    295   a�      a          0    30160    owin_forminfo 
   TABLE DATA           �   COPY public.owin_forminfo (formid, formname, parentid, levelid, menulevel, formnamear, hasdirectchild, icon, classicon, sequence, url, isview, isdynamic, issuperadmin, isvisibleinmenu, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    297   ~�      c          0    30166 	   owin_role 
   TABLE DATA           e   COPY public.owin_role (roleid, rolename, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    299   ��      d          0    30171    owin_role_permission 
   TABLE DATA           �   COPY public.owin_role_permission (role_permission_id, rolename, roleid, formactionid, appformid, status, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    300   �      g          0    30178 	   owin_user 
   TABLE DATA           �   COPY public.owin_user (userid, name, user_name, password, email, email_password, is_super_user, is_admin, is_active, is_loggedin, logon_time, employee_code, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    303    �      i          0    30184    owin_userrole 
   TABLE DATA           }   COPY public.owin_userrole (userroleid, userid, roleid, isenable, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    305   ��      G          0    30063    security_rule_menu 
   TABLE DATA           �   COPY public.security_rule_menu (master_id, menu_id, security_rule_code, can_select, can_insert, can_update, can_delete, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    271   ��      l          0    30189    style_category 
   TABLE DATA           �   COPY public.style_category (style_category_id, style_master_category_id, style_category_name, style_category_code, has_sleeve, has_hoody, has_length, has_fit, is_active, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    308   �      m          0    30194    style_embellishment 
   TABLE DATA           �   COPY public.style_embellishment (style_embellishment_id, style_embellishment_name, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    309   `�      �          0    30935    style_fabric 
   TABLE DATA           �   COPY public.style_fabric (style_fabric_id, style_fabric_name, is_active, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    387   ��      �          0    30941    style_fabric_detl 
   TABLE DATA           �   COPY public.style_fabric_detl (style_fabric_detl_id, style_fabric_id, gen_segment_id, segment_display_name, is_active, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    389   ��      o          0    30200    style_gender 
   TABLE DATA           �   COPY public.style_gender (style_gender_id, style_gender_name, style_gender_code, is_active, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    311   ��      p          0    30205    style_item_origin 
   TABLE DATA           �   COPY public.style_item_origin (style_item_origin_id, style_item_origin_name, is_active, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    312   o�      r          0    30211    style_item_product 
   TABLE DATA           %  COPY public.style_item_product (style_item_product_id, style_item_product_name, style_item_type_id, style_product_type_id, style_item_origin_id, style_gender_id, style_master_category_id, added_by, date_added, updated_by, date_updated, fiscal_year_id, style_product_size_group_id) FROM stdin;
    public          postgres    false    314   ��      t          0    30217    style_item_product_sub_category 
   TABLE DATA           �   COPY public.style_item_product_sub_category (style_item_product_sub_category_id, style_item_product_id, sub_category_name, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    316   �      v          0    30223    style_item_type 
   TABLE DATA           �   COPY public.style_item_type (style_item_type_id, style_item_type_name, style_item_type_code, is_active, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    318   �\      x          0    30229    style_label 
   TABLE DATA           �   COPY public.style_label (style_label_id, style_label_name, short_name, label_code, label_description, is_active, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    320   h]      y          0    30234    style_master_category 
   TABLE DATA           �   COPY public.style_master_category (style_master_category_id, master_category_name, is_active, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    321   �]      z          0    30239 0   style_master_category_structure_subgroup_mapping 
   TABLE DATA           �   COPY public.style_master_category_structure_subgroup_mapping (master_category_structure_subgroup_mapping_id, style_master_category_id, gen_item_structure_group_sub_id, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    322   �`      |          0    30243    style_product_size_group 
   TABLE DATA           �   COPY public.style_product_size_group (style_product_size_group_id, style_product_size_group_name, added_by, date_added, updated_by, date_updated, is_separate_price) FROM stdin;
    public          postgres    false    324   �f      }          0    30248     style_product_size_group_details 
   TABLE DATA           �   COPY public.style_product_size_group_details (style_product_size_group_detid, style_product_size_group_id, style_product_size, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    325   qg      �          0    30255    style_product_type 
   TABLE DATA           �   COPY public.style_product_type (style_product_type_id, style_product_type_name, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    328   bh      �          0    30261    style_product_unit 
   TABLE DATA           �   COPY public.style_product_unit (style_product_unit_id, style_product_unit_name, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    330   �h      �          0    30283    tran_bp_event 
   TABLE DATA           �   COPY public.tran_bp_event (tran_bp_event_id, event_id, event_gross_sales, readygoods_qnty, readygoods_value, added_by, updated_by, date_added, date_updated, tran_bp_year_id) FROM stdin;
    public          postgres    false    344   �h      �          0    30288    tran_bp_event_month 
   TABLE DATA           �   COPY public.tran_bp_event_month (tran_bp_event_month_id, tran_bp_event_id, month_id, monthly_gross_sales, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    345   di      �          0    30293    tran_bp_event_month_outlet 
   TABLE DATA           �   COPY public.tran_bp_event_month_outlet (tran_bp_event_month_outlet_id, tran_bp_event_month_id, outlet_id, outlet_gross_sales, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    346   
j      �          0    30277    tran_bp_year 
   TABLE DATA           �   COPY public.tran_bp_year (tran_bp_year_id, fiscal_year_id, gross_sales, added_by, updated_by, date_added, date_updated, is_approved, approved_by, approve_date, approve_remarks, remarks, is_submitted) FROM stdin;
    public          postgres    false    342   �p      �          0    30301    tran_range_plan 
   TABLE DATA           �   COPY public.tran_range_plan (range_plan_id, tran_bp_year_id, remarks, added_by, updated_by, date_added, date_updated, is_submitted, is_approved, approved_by, approve_date, approve_remarks) FROM stdin;
    public          postgres    false    350   q      �          0    30306    tran_range_plan_details 
   TABLE DATA           5  COPY public.tran_range_plan_details (range_plan_detail_id, range_plan_id, tran_bp_event_id, style_item_product_id, range_plan_quantity, mrp_per_pc_value, mrp_value, cpu_per_pc_value, cpu_value, priority_id, prev_year_quantity, prev_year_efficiency, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    351   Oq      �          0    30312    tran_range_plan_details_size 
   TABLE DATA           �   COPY public.tran_range_plan_details_size (range_plan_detail_size_id, range_plan_detail_id, style_product_size_group_detid, size_quantity, size_per_pc_mrp_value, added_by, updated_by, date_added, date_updated, size_per_pc_cpu_value) FROM stdin;
    public          postgres    false    353   r      �          0    30318    tran_range_plan_events 
   TABLE DATA           �   COPY public.tran_range_plan_events (tran_range_plan_event_id, range_plan_id, tran_bp_event_id, total_mrp_value, total_cpu_value, total_range_plan_quantity, is_finalized, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    355   +s      �          0    30325    tran_sample_order 
   TABLE DATA           >  COPY public.tran_sample_order (tran_sample_order_id, tran_va_plan_detl_id, tran_sample_order_number, order_date, delivery_date, delivery_unit_id, order_quantity, designer_member_id, remarks, is_submit, is_approved, approved_by, date_approved, added_by, date_added, updated_by, date_updated, sample_photos) FROM stdin;
    public          postgres    false    358   �s      �          0    30330    tran_sample_order_detl 
   TABLE DATA           �   COPY public.tran_sample_order_detl (tran_sample_order_detl_id, tran_sample_order_id, color_code, style_product_size_group_detid, order_quantity, style_product_unit_id, remarks, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    359   ^�      �          0    30336    tran_sample_order_embellishment 
   TABLE DATA           �   COPY public.tran_sample_order_embellishment (tran_sample_order_embellishment_id, tran_sample_order_id, embellishment_id, remarks, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    361   ��      �          0    30342    tran_sample_order_subgroup 
   TABLE DATA           �   COPY public.tran_sample_order_subgroup (tran_sample_order_subgroup_id, tran_sample_order_id, gen_item_structure_group_sub_id, remarks, added_by, date_added, updated_by, date_updated) FROM stdin;
    public          postgres    false    363   �      �          0    30349    tran_va_plan 
   TABLE DATA           �   COPY public.tran_va_plan (tran_va_plan_id, tran_range_plan_id, remarks, is_submitted, is_approved, approved_by, approve_date, approve_remarks, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    366   ��      �          0    30354    tran_va_plan_detl 
   TABLE DATA           �   COPY public.tran_va_plan_detl (tran_va_plan_detl_id, tran_va_plan_event_id, range_plan_detail_id, style_item_product_id, no_of_style, style_code_gen, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    367   ݶ      �          0    30359    tran_va_plan_detl_style 
   TABLE DATA             COPY public.tran_va_plan_detl_style (tran_va_plan_detl_style_id, tran_va_plan_detl_id, style_code, style_quantity, no_of_color, color_code_gen, added_by, updated_by, date_added, date_updated, style_embellishment_ids, designer_member_id, style_item_product_sub_category_id) FROM stdin;
    public          postgres    false    368   ��      �          0    30364    tran_va_plan_detl_style_color 
   TABLE DATA           �   COPY public.tran_va_plan_detl_style_color (tran_va_plan_detl_style_color_id, tran_va_plan_detl_style_id, color_code, style_color_quantity, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    369   �      �          0    30369 "   tran_va_plan_detl_style_color_size 
   TABLE DATA           �   COPY public.tran_va_plan_detl_style_color_size (tran_va_plan_detl_style_color_size_id, tran_va_plan_detl_style_color_id, style_product_size_group_detid, style_color_size_quantity, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    370   ��      2          0    29060    tran_va_plan_events 
   TABLE DATA           �   COPY public.tran_va_plan_events (tran_va_plan_event_id, tran_va_plan_id, tran_range_plan_event_id, is_finalised, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    247   ��      �          0    30377 %   tran_va_product_embellishment_mapping 
   TABLE DATA           �   COPY public.tran_va_product_embellishment_mapping (tran_va_product_embellishment_mapping_id, style_item_product_id, style_embelishment_id, added_by, updated_by, date_added, date_updated) FROM stdin;
    public          postgres    false    376   �      �          0    30396    buckets 
   TABLE DATA           �   COPY storage.buckets (id, name, owner, created_at, updated_at, public, avif_autodetection, file_size_limit, allowed_mime_types, owner_id) FROM stdin;
    storage          postgres    false    381   "�      �          0    30405 
   migrations 
   TABLE DATA           B   COPY storage.migrations (id, name, hash, executed_at) FROM stdin;
    storage          postgres    false    382   ��      �          0    30409    objects 
   TABLE DATA           �   COPY storage.objects (id, bucket_id, name, owner, created_at, updated_at, last_accessed_at, metadata, version, owner_id) FROM stdin;
    storage          postgres    false    383   ��      L          0    16951    secrets 
   TABLE DATA           f   COPY vault.secrets (id, name, description, secret, key_id, nonce, created_at, updated_at) FROM stdin;
    vault          supabase_admin    false    244   .�      �           0    0    refresh_tokens_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('auth.refresh_tokens_id_seq', 1, false);
          auth          postgres    false    259            �           0    0    key_key_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('pgsodium.key_key_id_seq', 1, false);
          pgsodium          supabase_admin    false    237            �           0    0 *   Security.GroupUserSecurityRule_ChildID_seq    SEQUENCE SET     [   SELECT pg_catalog.setval('public."Security.GroupUserSecurityRule_ChildID_seq"', 1, false);
          public          postgres    false    268            �           0    0 3   Security.LoginUserAttachedWithGroupUser_ChildID_seq    SEQUENCE SET     d   SELECT pg_catalog.setval('public."Security.LoginUserAttachedWithGroupUser_ChildID_seq"', 1, false);
          public          postgres    false    270            �           0    0 &   Security.SecurityRuleMenu_MasterID_seq    SEQUENCE SET     W   SELECT pg_catalog.setval('public."Security.SecurityRuleMenu_MasterID_seq"', 1, false);
          public          postgres    false    272            �           0    0 4   gen_item_structure_group_item_structure_group_id_seq    SEQUENCE SET     b   SELECT pg_catalog.setval('public.gen_item_structure_group_item_structure_group_id_seq', 3, true);
          public          postgres    false    278            �           0    0 ?   gen_item_structure_group_sub_gen_item_structure_group_sub_i_seq    SEQUENCE SET     m   SELECT pg_catalog.setval('public.gen_item_structure_group_sub_gen_item_structure_group_sub_i_seq', 8, true);
          public          postgres    false    280            �           0    0    gen_priority_priority_id_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public.gen_priority_priority_id_seq', 2, true);
          public          postgres    false    283            �           0    0 (   gen_segment_detl_gen_segment_detl_id_seq    SEQUENCE SET     Z   SELECT pg_catalog.setval('public.gen_segment_detl_gen_segment_detl_id_seq', 19757, true);
          public          postgres    false    384            �           0    0    gen_segment_gen_segment_id_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('public.gen_segment_gen_segment_id_seq', 16, true);
          public          postgres    false    390            �           0    0 $   gen_team_group_gen_team_group_id_seq    SEQUENCE SET     R   SELECT pg_catalog.setval('public.gen_team_group_gen_team_group_id_seq', 1, true);
          public          postgres    false    287            �           0    0 (   gen_team_members_gen_team_members_id_seq    SEQUENCE SET     V   SELECT pg_catalog.setval('public.gen_team_members_gen_team_members_id_seq', 6, true);
          public          postgres    false    289            �           0    0    gen_unit_gen_unit_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.gen_unit_gen_unit_id_seq', 3, true);
          public          postgres    false    291            �           0    0    outlet_outlet_id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.outlet_outlet_id_seq', 1, false);
          public          postgres    false    294            �           0    0     owin_formaction_formactionid_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public.owin_formaction_formactionid_seq', 1, false);
          public          postgres    false    296            �           0    0    owin_forminfo_formid_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.owin_forminfo_formid_seq', 1, true);
          public          postgres    false    298            �           0    0 +   owin_role_permission_role_permission_id_seq    SEQUENCE SET     Z   SELECT pg_catalog.setval('public.owin_role_permission_role_permission_id_seq', 1, false);
          public          postgres    false    301            �           0    0    owin_role_roleid_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.owin_role_roleid_seq', 1, true);
          public          postgres    false    302            �           0    0    owin_user_userid_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.owin_user_userid_seq', 1, false);
          public          postgres    false    304            �           0    0    owin_userrole_userroleid_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public.owin_userrole_userroleid_seq', 1, false);
          public          postgres    false    306            �           0    0    serial_number_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.serial_number_seq', 5, true);
          public          postgres    false    307            �           0    0 .   style_embellishment_style_embellishment_id_seq    SEQUENCE SET     ]   SELECT pg_catalog.setval('public.style_embellishment_style_embellishment_id_seq', 1, false);
          public          postgres    false    310            �           0    0 *   style_fabric_detl_style_fabric_detl_id_seq    SEQUENCE SET     Y   SELECT pg_catalog.setval('public.style_fabric_detl_style_fabric_detl_id_seq', 1, false);
          public          postgres    false    388                        0    0     style_fabric_style_fabric_id_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public.style_fabric_style_fabric_id_seq', 1, false);
          public          postgres    false    386                       0    0 *   style_item_origin_style_item_origin_id_seq    SEQUENCE SET     Y   SELECT pg_catalog.setval('public.style_item_origin_style_item_origin_id_seq', 1, false);
          public          postgres    false    313                       0    0 ,   style_item_product_style_item_product_id_seq    SEQUENCE SET     ]   SELECT pg_catalog.setval('public.style_item_product_style_item_product_id_seq', 2554, true);
          public          postgres    false    315                       0    0 ?   style_item_product_sub_catego_style_item_product_sub_catego_seq    SEQUENCE SET     p   SELECT pg_catalog.setval('public.style_item_product_sub_catego_style_item_product_sub_catego_seq', 1614, true);
          public          postgres    false    317                       0    0 &   style_item_type_style_item_type_id_seq    SEQUENCE SET     T   SELECT pg_catalog.setval('public.style_item_type_style_item_type_id_seq', 7, true);
          public          postgres    false    319                       0    0 ?   style_master_category_structu_master_category_structure_sub_seq    SEQUENCE SET     o   SELECT pg_catalog.setval('public.style_master_category_structu_master_category_structure_sub_seq', 376, true);
          public          postgres    false    323                       0    0 ?   style_product_size_group_deta_style_product_size_group_deti_seq    SEQUENCE SET     n   SELECT pg_catalog.setval('public.style_product_size_group_deta_style_product_size_group_deti_seq', 33, true);
          public          postgres    false    326                       0    0 8   style_product_size_group_style_product_size_group_id_seq    SEQUENCE SET     f   SELECT pg_catalog.setval('public.style_product_size_group_style_product_size_group_id_seq', 6, true);
          public          postgres    false    327                       0    0 ,   style_product_type_style_product_type_id_seq    SEQUENCE SET     [   SELECT pg_catalog.setval('public.style_product_type_style_product_type_id_seq', 1, false);
          public          postgres    false    329            	           0    0 ,   style_product_unit_style_product_unit_id_seq    SEQUENCE SET     Z   SELECT pg_catalog.setval('public.style_product_unit_style_product_unit_id_seq', 2, true);
          public          postgres    false    331            
           0    0     tbl_department_department_id_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public.tbl_department_department_id_seq', 1, false);
          public          postgres    false    332                       0    0    tbl_district_districtid_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public.tbl_district_districtid_seq', 1, true);
          public          postgres    false    333                       0    0 "   tbl_fiscal_year_fiscal_year_id_seq    SEQUENCE SET     Q   SELECT pg_catalog.setval('public.tbl_fiscal_year_fiscal_year_id_seq', 1, false);
          public          postgres    false    334                       0    0    tbl_menu_menu_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.tbl_menu_menu_id_seq', 1, true);
          public          postgres    false    335                       0    0    tbl_month_monthid_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public.tbl_month_monthid_seq', 12, true);
          public          postgres    false    336                       0    0    tbl_season_master_season_id_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('public.tbl_season_master_season_id_seq', 1, true);
          public          postgres    false    337                       0    0    tbl_season_month_config_id_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('public.tbl_season_month_config_id_seq', 19, true);
          public          postgres    false    338                       0    0 (   tbl_style_category_style_category_id_seq    SEQUENCE SET     W   SELECT pg_catalog.setval('public.tbl_style_category_style_category_id_seq', 1, false);
          public          postgres    false    339                       0    0 "   tbl_style_label_style_label_id_seq    SEQUENCE SET     Q   SELECT pg_catalog.setval('public.tbl_style_label_style_label_id_seq', 1, false);
          public          postgres    false    340                       0    0 6   tbl_style_master_category_style_master_category_id_seq    SEQUENCE SET     e   SELECT pg_catalog.setval('public.tbl_style_master_category_style_master_category_id_seq', 96, true);
          public          postgres    false    341                       0    0 '   tbl_tran_business_plan_year_data_id_seq    SEQUENCE SET     V   SELECT pg_catalog.setval('public.tbl_tran_business_plan_year_data_id_seq', 48, true);
          public          postgres    false    343                       0    0 %   tran_bp_season_event_bp_season_id_seq    SEQUENCE SET     U   SELECT pg_catalog.setval('public.tran_bp_season_event_bp_season_id_seq', 114, true);
          public          postgres    false    347                       0    0 +   tran_bp_season_month_bp_season_month_id_seq    SEQUENCE SET     [   SELECT pg_catalog.setval('public.tran_bp_season_month_bp_season_month_id_seq', 266, true);
          public          postgres    false    348                       0    0 9   tran_bp_season_month_outlet_bp_season_month_outlet_id_seq    SEQUENCE SET     j   SELECT pg_catalog.setval('public.tran_bp_season_month_outlet_bp_season_month_outlet_id_seq', 2740, true);
          public          postgres    false    349                       0    0 0   tran_range_plan_details_range_plan_detail_id_seq    SEQUENCE SET     `   SELECT pg_catalog.setval('public.tran_range_plan_details_range_plan_detail_id_seq', 114, true);
          public          postgres    false    352                       0    0 :   tran_range_plan_details_size_range_plan_detail_size_id_seq    SEQUENCE SET     j   SELECT pg_catalog.setval('public.tran_range_plan_details_size_range_plan_detail_size_id_seq', 543, true);
          public          postgres    false    354                       0    0 3   tran_range_plan_events_tran_range_plan_event_id_seq    SEQUENCE SET     b   SELECT pg_catalog.setval('public.tran_range_plan_events_tran_range_plan_event_id_seq', 20, true);
          public          postgres    false    356                       0    0 !   tran_range_plan_range_plan_id_seq    SEQUENCE SET     P   SELECT pg_catalog.setval('public.tran_range_plan_range_plan_id_seq', 25, true);
          public          postgres    false    357                       0    0 4   tran_sample_order_detl_tran_sample_order_detl_id_seq    SEQUENCE SET     b   SELECT pg_catalog.setval('public.tran_sample_order_detl_tran_sample_order_detl_id_seq', 3, true);
          public          postgres    false    360                       0    0 ?   tran_sample_order_embellishme_tran_sample_order_embellishme_seq    SEQUENCE SET     n   SELECT pg_catalog.setval('public.tran_sample_order_embellishme_tran_sample_order_embellishme_seq', 43, true);
          public          postgres    false    362                       0    0 <   tran_sample_order_subgroup_tran_sample_order_subgroup_id_seq    SEQUENCE SET     l   SELECT pg_catalog.setval('public.tran_sample_order_subgroup_tran_sample_order_subgroup_id_seq', 104, true);
          public          postgres    false    364                       0    0 *   tran_sample_order_tran_sample_order_id_seq    SEQUENCE SET     Y   SELECT pg_catalog.setval('public.tran_sample_order_tran_sample_order_id_seq', 25, true);
          public          postgres    false    365                        0    0 ?   tran_va_plan_detl_style_color_tran_va_plan_detl_style_colo_seq1    SEQUENCE SET     o   SELECT pg_catalog.setval('public.tran_va_plan_detl_style_color_tran_va_plan_detl_style_colo_seq1', 760, true);
          public          postgres    false    371            !           0    0 ?   tran_va_plan_detl_style_color_tran_va_plan_detl_style_color_seq    SEQUENCE SET     o   SELECT pg_catalog.setval('public.tran_va_plan_detl_style_color_tran_va_plan_detl_style_color_seq', 131, true);
          public          postgres    false    372            "           0    0 6   tran_va_plan_detl_style_tran_va_plan_detl_style_id_seq    SEQUENCE SET     e   SELECT pg_catalog.setval('public.tran_va_plan_detl_style_tran_va_plan_detl_style_id_seq', 82, true);
          public          postgres    false    373            #           0    0 *   tran_va_plan_detl_tran_va_plan_detl_id_seq    SEQUENCE SET     Y   SELECT pg_catalog.setval('public.tran_va_plan_detl_tran_va_plan_detl_id_seq', 38, true);
          public          postgres    false    374            $           0    0 .   tran_va_plan_events_tran_va_plan_events_id_seq    SEQUENCE SET     ]   SELECT pg_catalog.setval('public.tran_va_plan_events_tran_va_plan_events_id_seq', 18, true);
          public          postgres    false    246            %           0    0     tran_va_plan_tran_va_plan_id_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public.tran_va_plan_tran_va_plan_id_seq', 16, true);
          public          postgres    false    375            &           0    0 ?   tran_va_product_embellishment_tran_va_product_embellishment_seq    SEQUENCE SET     n   SELECT pg_catalog.setval('public.tran_va_product_embellishment_tran_va_product_embellishment_seq', 1, false);
          public          postgres    false    377            �           2606    30423    mfa_amr_claims amr_id_pk 
   CONSTRAINT     T   ALTER TABLE ONLY auth.mfa_amr_claims
    ADD CONSTRAINT amr_id_pk PRIMARY KEY (id);
 @   ALTER TABLE ONLY auth.mfa_amr_claims DROP CONSTRAINT amr_id_pk;
       auth            postgres    false    255            �           2606    30425 (   audit_log_entries audit_log_entries_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY auth.audit_log_entries
    ADD CONSTRAINT audit_log_entries_pkey PRIMARY KEY (id);
 P   ALTER TABLE ONLY auth.audit_log_entries DROP CONSTRAINT audit_log_entries_pkey;
       auth            postgres    false    251            �           2606    30427    flow_state flow_state_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY auth.flow_state
    ADD CONSTRAINT flow_state_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY auth.flow_state DROP CONSTRAINT flow_state_pkey;
       auth            postgres    false    252            �           2606    30429    identities identities_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY auth.identities
    ADD CONSTRAINT identities_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY auth.identities DROP CONSTRAINT identities_pkey;
       auth            postgres    false    253            �           2606    30431 1   identities identities_provider_id_provider_unique 
   CONSTRAINT     {   ALTER TABLE ONLY auth.identities
    ADD CONSTRAINT identities_provider_id_provider_unique UNIQUE (provider_id, provider);
 Y   ALTER TABLE ONLY auth.identities DROP CONSTRAINT identities_provider_id_provider_unique;
       auth            postgres    false    253    253            �           2606    30433    instances instances_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY auth.instances
    ADD CONSTRAINT instances_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY auth.instances DROP CONSTRAINT instances_pkey;
       auth            postgres    false    254            �           2606    30435 C   mfa_amr_claims mfa_amr_claims_session_id_authentication_method_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY auth.mfa_amr_claims
    ADD CONSTRAINT mfa_amr_claims_session_id_authentication_method_pkey UNIQUE (session_id, authentication_method);
 k   ALTER TABLE ONLY auth.mfa_amr_claims DROP CONSTRAINT mfa_amr_claims_session_id_authentication_method_pkey;
       auth            postgres    false    255    255            �           2606    30437 "   mfa_challenges mfa_challenges_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY auth.mfa_challenges
    ADD CONSTRAINT mfa_challenges_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY auth.mfa_challenges DROP CONSTRAINT mfa_challenges_pkey;
       auth            postgres    false    256            �           2606    30439    mfa_factors mfa_factors_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY auth.mfa_factors
    ADD CONSTRAINT mfa_factors_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY auth.mfa_factors DROP CONSTRAINT mfa_factors_pkey;
       auth            postgres    false    257            �           2606    30441 "   refresh_tokens refresh_tokens_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY auth.refresh_tokens
    ADD CONSTRAINT refresh_tokens_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY auth.refresh_tokens DROP CONSTRAINT refresh_tokens_pkey;
       auth            postgres    false    258            �           2606    30443 *   refresh_tokens refresh_tokens_token_unique 
   CONSTRAINT     d   ALTER TABLE ONLY auth.refresh_tokens
    ADD CONSTRAINT refresh_tokens_token_unique UNIQUE (token);
 R   ALTER TABLE ONLY auth.refresh_tokens DROP CONSTRAINT refresh_tokens_token_unique;
       auth            postgres    false    258            �           2606    30445 +   saml_providers saml_providers_entity_id_key 
   CONSTRAINT     i   ALTER TABLE ONLY auth.saml_providers
    ADD CONSTRAINT saml_providers_entity_id_key UNIQUE (entity_id);
 S   ALTER TABLE ONLY auth.saml_providers DROP CONSTRAINT saml_providers_entity_id_key;
       auth            postgres    false    260            �           2606    30447 "   saml_providers saml_providers_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY auth.saml_providers
    ADD CONSTRAINT saml_providers_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY auth.saml_providers DROP CONSTRAINT saml_providers_pkey;
       auth            postgres    false    260            �           2606    30449 (   saml_relay_states saml_relay_states_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY auth.saml_relay_states
    ADD CONSTRAINT saml_relay_states_pkey PRIMARY KEY (id);
 P   ALTER TABLE ONLY auth.saml_relay_states DROP CONSTRAINT saml_relay_states_pkey;
       auth            postgres    false    261            �           2606    30451 (   schema_migrations schema_migrations_pkey 
   CONSTRAINT     i   ALTER TABLE ONLY auth.schema_migrations
    ADD CONSTRAINT schema_migrations_pkey PRIMARY KEY (version);
 P   ALTER TABLE ONLY auth.schema_migrations DROP CONSTRAINT schema_migrations_pkey;
       auth            postgres    false    262            �           2606    30453    sessions sessions_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY auth.sessions
    ADD CONSTRAINT sessions_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY auth.sessions DROP CONSTRAINT sessions_pkey;
       auth            postgres    false    263            �           2606    30455    sso_domains sso_domains_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY auth.sso_domains
    ADD CONSTRAINT sso_domains_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY auth.sso_domains DROP CONSTRAINT sso_domains_pkey;
       auth            postgres    false    264            �           2606    30457     sso_providers sso_providers_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY auth.sso_providers
    ADD CONSTRAINT sso_providers_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY auth.sso_providers DROP CONSTRAINT sso_providers_pkey;
       auth            postgres    false    265            �           2606    30459    users users_phone_key 
   CONSTRAINT     O   ALTER TABLE ONLY auth.users
    ADD CONSTRAINT users_phone_key UNIQUE (phone);
 =   ALTER TABLE ONLY auth.users DROP CONSTRAINT users_phone_key;
       auth            postgres    false    266            �           2606    30461    users users_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY auth.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY auth.users DROP CONSTRAINT users_pkey;
       auth            postgres    false    266            �           2606    30463 <   group_user_security_rule Security.GroupUserSecurityRule_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.group_user_security_rule
    ADD CONSTRAINT "Security.GroupUserSecurityRule_pkey" PRIMARY KEY (child_id);
 h   ALTER TABLE ONLY public.group_user_security_rule DROP CONSTRAINT "Security.GroupUserSecurityRule_pkey";
       public            postgres    false    267            �           2606    30465 P   login_user_attached_with_group_user Security.LoginUserAttachedWithGroupUser_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.login_user_attached_with_group_user
    ADD CONSTRAINT "Security.LoginUserAttachedWithGroupUser_pkey" PRIMARY KEY (child_id);
 |   ALTER TABLE ONLY public.login_user_attached_with_group_user DROP CONSTRAINT "Security.LoginUserAttachedWithGroupUser_pkey";
       public            postgres    false    269            �           2606    30467    menu Security.Menu_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.menu
    ADD CONSTRAINT "Security.Menu_pkey" PRIMARY KEY (menu_id);
 C   ALTER TABLE ONLY public.menu DROP CONSTRAINT "Security.Menu_pkey";
       public            postgres    false    292            �           2606    30469 1   security_rule_menu Security.SecurityRuleMenu_pkey 
   CONSTRAINT     x   ALTER TABLE ONLY public.security_rule_menu
    ADD CONSTRAINT "Security.SecurityRuleMenu_pkey" PRIMARY KEY (master_id);
 ]   ALTER TABLE ONLY public.security_rule_menu DROP CONSTRAINT "Security.SecurityRuleMenu_pkey";
       public            postgres    false    271            �           2606    30471 '   gen_department Security.department_pkey 
   CONSTRAINT     r   ALTER TABLE ONLY public.gen_department
    ADD CONSTRAINT "Security.department_pkey" PRIMARY KEY (department_id);
 S   ALTER TABLE ONLY public.gen_department DROP CONSTRAINT "Security.department_pkey";
       public            postgres    false    273            �           2606    30473    gen_outlet Security.outlet_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.gen_outlet
    ADD CONSTRAINT "Security.outlet_pkey" PRIMARY KEY (outlet_id);
 K   ALTER TABLE ONLY public.gen_outlet DROP CONSTRAINT "Security.outlet_pkey";
       public            postgres    false    281            �           2606    30475    gen_district district_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public.gen_district
    ADD CONSTRAINT district_pkey PRIMARY KEY (district_id);
 D   ALTER TABLE ONLY public.gen_district DROP CONSTRAINT district_pkey;
       public            postgres    false    274            �           2606    30477    gen_division divisionid 
   CONSTRAINT     ^   ALTER TABLE ONLY public.gen_division
    ADD CONSTRAINT divisionid PRIMARY KEY (division_id);
 A   ALTER TABLE ONLY public.gen_division DROP CONSTRAINT divisionid;
       public            postgres    false    275            �           2606    30479 6   gen_item_structure_group gen_item_structure_group_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.gen_item_structure_group
    ADD CONSTRAINT gen_item_structure_group_pkey PRIMARY KEY (item_structure_group_id);
 `   ALTER TABLE ONLY public.gen_item_structure_group DROP CONSTRAINT gen_item_structure_group_pkey;
       public            postgres    false    277            �           2606    30481 >   gen_item_structure_group_sub gen_item_structure_group_sub_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.gen_item_structure_group_sub
    ADD CONSTRAINT gen_item_structure_group_sub_pkey PRIMARY KEY (gen_item_structure_group_sub_id);
 h   ALTER TABLE ONLY public.gen_item_structure_group_sub DROP CONSTRAINT gen_item_structure_group_sub_pkey;
       public            postgres    false    279            �           2606    30483    gen_priority gen_priority_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public.gen_priority
    ADD CONSTRAINT gen_priority_pkey PRIMARY KEY (priority_id);
 H   ALTER TABLE ONLY public.gen_priority DROP CONSTRAINT gen_priority_pkey;
       public            postgres    false    282            H           2606    30953 &   gen_segment_detl gen_segment_detl_pkey 
   CONSTRAINT     u   ALTER TABLE ONLY public.gen_segment_detl
    ADD CONSTRAINT gen_segment_detl_pkey PRIMARY KEY (gen_segment_detl_id);
 P   ALTER TABLE ONLY public.gen_segment_detl DROP CONSTRAINT gen_segment_detl_pkey;
       public            postgres    false    385            N           2606    30961    gen_segment gen_segment_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.gen_segment
    ADD CONSTRAINT gen_segment_pkey PRIMARY KEY (gen_segment_id);
 F   ALTER TABLE ONLY public.gen_segment DROP CONSTRAINT gen_segment_pkey;
       public            postgres    false    391            �           2606    30485 %   gen_team_group gen_team_group_id_pkey 
   CONSTRAINT     r   ALTER TABLE ONLY public.gen_team_group
    ADD CONSTRAINT gen_team_group_id_pkey PRIMARY KEY (gen_team_group_id);
 O   ALTER TABLE ONLY public.gen_team_group DROP CONSTRAINT gen_team_group_id_pkey;
       public            postgres    false    286            �           2606    30487 )   gen_team_members gen_team_members_id_pkey 
   CONSTRAINT     x   ALTER TABLE ONLY public.gen_team_members
    ADD CONSTRAINT gen_team_members_id_pkey PRIMARY KEY (gen_team_members_id);
 S   ALTER TABLE ONLY public.gen_team_members DROP CONSTRAINT gen_team_members_id_pkey;
       public            postgres    false    288            �           2606    30489    gen_unit gen_unit_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.gen_unit
    ADD CONSTRAINT gen_unit_pkey PRIMARY KEY (gen_unit_id);
 @   ALTER TABLE ONLY public.gen_unit DROP CONSTRAINT gen_unit_pkey;
       public            postgres    false    290            �           2606    30491    owin_user loginuser_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.owin_user
    ADD CONSTRAINT loginuser_pkey PRIMARY KEY (userid);
 B   ALTER TABLE ONLY public.owin_user DROP CONSTRAINT loginuser_pkey;
       public            postgres    false    303            �           2606    30493    owin_role owin_role_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.owin_role
    ADD CONSTRAINT owin_role_pkey PRIMARY KEY (roleid);
 B   ALTER TABLE ONLY public.owin_role DROP CONSTRAINT owin_role_pkey;
       public            postgres    false    299            �           2606    30495     owin_userrole owin_userrole_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public.owin_userrole
    ADD CONSTRAINT owin_userrole_pkey PRIMARY KEY (userid);
 J   ALTER TABLE ONLY public.owin_userrole DROP CONSTRAINT owin_userrole_pkey;
       public            postgres    false    305            P           2606    30985    gen_segment segname 
   CONSTRAINT     Z   ALTER TABLE ONLY public.gen_segment
    ADD CONSTRAINT segname UNIQUE (gen_segment_name);
 =   ALTER TABLE ONLY public.gen_segment DROP CONSTRAINT segname;
       public            postgres    false    391            L           2606    30970 (   style_fabric_detl style_fabric_detl_pkey 
   CONSTRAINT     x   ALTER TABLE ONLY public.style_fabric_detl
    ADD CONSTRAINT style_fabric_detl_pkey PRIMARY KEY (style_fabric_detl_id);
 R   ALTER TABLE ONLY public.style_fabric_detl DROP CONSTRAINT style_fabric_detl_pkey;
       public            postgres    false    389            J           2606    30968    style_fabric style_fabric_pkey 
   CONSTRAINT     i   ALTER TABLE ONLY public.style_fabric
    ADD CONSTRAINT style_fabric_pkey PRIMARY KEY (style_fabric_id);
 H   ALTER TABLE ONLY public.style_fabric DROP CONSTRAINT style_fabric_pkey;
       public            postgres    false    387                       2606    30497 D   style_item_product_sub_category style_item_product_sub_category_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.style_item_product_sub_category
    ADD CONSTRAINT style_item_product_sub_category_pkey PRIMARY KEY (style_item_product_sub_category_id);
 n   ALTER TABLE ONLY public.style_item_product_sub_category DROP CONSTRAINT style_item_product_sub_category_pkey;
       public            postgres    false    316                       2606    30499 f   style_master_category_structure_subgroup_mapping style_master_category_structure_subgroup_mapping_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.style_master_category_structure_subgroup_mapping
    ADD CONSTRAINT style_master_category_structure_subgroup_mapping_pkey PRIMARY KEY (master_category_structure_subgroup_mapping_id);
 �   ALTER TABLE ONLY public.style_master_category_structure_subgroup_mapping DROP CONSTRAINT style_master_category_structure_subgroup_mapping_pkey;
       public            postgres    false    322            �           2606    30501 $   gen_fiscal_year tbl_fiscal_year_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public.gen_fiscal_year
    ADD CONSTRAINT tbl_fiscal_year_pkey PRIMARY KEY (fiscal_year_id);
 N   ALTER TABLE ONLY public.gen_fiscal_year DROP CONSTRAINT tbl_fiscal_year_pkey;
       public            postgres    false    276            �           2606    30503    month tbl_month_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.month
    ADD CONSTRAINT tbl_month_pkey PRIMARY KEY (month_id);
 >   ALTER TABLE ONLY public.month DROP CONSTRAINT tbl_month_pkey;
       public            postgres    false    293            �           2606    30505 !   gen_season tbl_season_master_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.gen_season
    ADD CONSTRAINT tbl_season_master_pkey PRIMARY KEY (season_id);
 K   ALTER TABLE ONLY public.gen_season DROP CONSTRAINT tbl_season_master_pkey;
       public            postgres    false    284            �           2606    30507 4   gen_season_event_config tbl_season_month_config_pkey 
   CONSTRAINT     x   ALTER TABLE ONLY public.gen_season_event_config
    ADD CONSTRAINT tbl_season_month_config_pkey PRIMARY KEY (event_id);
 ^   ALTER TABLE ONLY public.gen_season_event_config DROP CONSTRAINT tbl_season_month_config_pkey;
       public            postgres    false    285            �           2606    30509 &   style_category tbl_style_category_pkey 
   CONSTRAINT     s   ALTER TABLE ONLY public.style_category
    ADD CONSTRAINT tbl_style_category_pkey PRIMARY KEY (style_category_id);
 P   ALTER TABLE ONLY public.style_category DROP CONSTRAINT tbl_style_category_pkey;
       public            postgres    false    308            �           2606    30511 0   style_embellishment tbl_style_embellishment_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.style_embellishment
    ADD CONSTRAINT tbl_style_embellishment_pkey PRIMARY KEY (style_embellishment_id);
 Z   ALTER TABLE ONLY public.style_embellishment DROP CONSTRAINT tbl_style_embellishment_pkey;
       public            postgres    false    309                       2606    30513 "   style_gender tbl_style_gender_pkey 
   CONSTRAINT     m   ALTER TABLE ONLY public.style_gender
    ADD CONSTRAINT tbl_style_gender_pkey PRIMARY KEY (style_gender_id);
 L   ALTER TABLE ONLY public.style_gender DROP CONSTRAINT tbl_style_gender_pkey;
       public            postgres    false    311                       2606    30515 ,   style_item_origin tbl_style_item_origin_pkey 
   CONSTRAINT     |   ALTER TABLE ONLY public.style_item_origin
    ADD CONSTRAINT tbl_style_item_origin_pkey PRIMARY KEY (style_item_origin_id);
 V   ALTER TABLE ONLY public.style_item_origin DROP CONSTRAINT tbl_style_item_origin_pkey;
       public            postgres    false    312                       2606    30517 .   style_item_product tbl_style_item_product_pkey 
   CONSTRAINT        ALTER TABLE ONLY public.style_item_product
    ADD CONSTRAINT tbl_style_item_product_pkey PRIMARY KEY (style_item_product_id);
 X   ALTER TABLE ONLY public.style_item_product DROP CONSTRAINT tbl_style_item_product_pkey;
       public            postgres    false    314            	           2606    30519 (   style_item_type tbl_style_item_type_pkey 
   CONSTRAINT     v   ALTER TABLE ONLY public.style_item_type
    ADD CONSTRAINT tbl_style_item_type_pkey PRIMARY KEY (style_item_type_id);
 R   ALTER TABLE ONLY public.style_item_type DROP CONSTRAINT tbl_style_item_type_pkey;
       public            postgres    false    318                       2606    30521     style_label tbl_style_label_pkey 
   CONSTRAINT     j   ALTER TABLE ONLY public.style_label
    ADD CONSTRAINT tbl_style_label_pkey PRIMARY KEY (style_label_id);
 J   ALTER TABLE ONLY public.style_label DROP CONSTRAINT tbl_style_label_pkey;
       public            postgres    false    320                       2606    30523 4   style_master_category tbl_style_master_category_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.style_master_category
    ADD CONSTRAINT tbl_style_master_category_pkey PRIMARY KEY (style_master_category_id);
 ^   ALTER TABLE ONLY public.style_master_category DROP CONSTRAINT tbl_style_master_category_pkey;
       public            postgres    false    321                       2606    30525 @   style_product_size_group_details tbl_style_product_det_size_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.style_product_size_group_details
    ADD CONSTRAINT tbl_style_product_det_size_pkey PRIMARY KEY (style_product_size_group_detid);
 j   ALTER TABLE ONLY public.style_product_size_group_details DROP CONSTRAINT tbl_style_product_det_size_pkey;
       public            postgres    false    325                       2606    30527 4   style_product_size_group tbl_style_product_size_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.style_product_size_group
    ADD CONSTRAINT tbl_style_product_size_pkey PRIMARY KEY (style_product_size_group_id);
 ^   ALTER TABLE ONLY public.style_product_size_group DROP CONSTRAINT tbl_style_product_size_pkey;
       public            postgres    false    324                       2606    30529 .   style_product_type tbl_style_product_type_pkey 
   CONSTRAINT        ALTER TABLE ONLY public.style_product_type
    ADD CONSTRAINT tbl_style_product_type_pkey PRIMARY KEY (style_product_type_id);
 X   ALTER TABLE ONLY public.style_product_type DROP CONSTRAINT tbl_style_product_type_pkey;
       public            postgres    false    328                       2606    30531 .   style_product_unit tbl_style_product_unit_pkey 
   CONSTRAINT        ALTER TABLE ONLY public.style_product_unit
    ADD CONSTRAINT tbl_style_product_unit_pkey PRIMARY KEY (style_product_unit_id);
 X   ALTER TABLE ONLY public.style_product_unit DROP CONSTRAINT tbl_style_product_unit_pkey;
       public            postgres    false    330                       2606    30533 /   tran_bp_event tbl_tran_businessplan_season_pkey 
   CONSTRAINT     {   ALTER TABLE ONLY public.tran_bp_event
    ADD CONSTRAINT tbl_tran_businessplan_season_pkey PRIMARY KEY (tran_bp_event_id);
 Y   ALTER TABLE ONLY public.tran_bp_event DROP CONSTRAINT tbl_tran_businessplan_season_pkey;
       public            postgres    false    344                       2606    30535 ;   tran_bp_event_month_outlet tran_bp_season_month_outlet_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_bp_event_month_outlet
    ADD CONSTRAINT tran_bp_season_month_outlet_pkey PRIMARY KEY (tran_bp_event_month_outlet_id);
 e   ALTER TABLE ONLY public.tran_bp_event_month_outlet DROP CONSTRAINT tran_bp_season_month_outlet_pkey;
       public            postgres    false    346                       2606    30537 -   tran_bp_event_month tran_bp_season_month_pkey 
   CONSTRAINT        ALTER TABLE ONLY public.tran_bp_event_month
    ADD CONSTRAINT tran_bp_season_month_pkey PRIMARY KEY (tran_bp_event_month_id);
 W   ALTER TABLE ONLY public.tran_bp_event_month DROP CONSTRAINT tran_bp_season_month_pkey;
       public            postgres    false    345                       2606    30539    tran_bp_year tran_bp_year_pkey 
   CONSTRAINT     i   ALTER TABLE ONLY public.tran_bp_year
    ADD CONSTRAINT tran_bp_year_pkey PRIMARY KEY (tran_bp_year_id);
 H   ALTER TABLE ONLY public.tran_bp_year DROP CONSTRAINT tran_bp_year_pkey;
       public            postgres    false    342            #           2606    30541 4   tran_range_plan_details tran_range_plan_details_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_range_plan_details
    ADD CONSTRAINT tran_range_plan_details_pkey PRIMARY KEY (range_plan_detail_id);
 ^   ALTER TABLE ONLY public.tran_range_plan_details DROP CONSTRAINT tran_range_plan_details_pkey;
       public            postgres    false    351            %           2606    30543 >   tran_range_plan_details_size tran_range_plan_details_size_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_range_plan_details_size
    ADD CONSTRAINT tran_range_plan_details_size_pkey PRIMARY KEY (range_plan_detail_size_id);
 h   ALTER TABLE ONLY public.tran_range_plan_details_size DROP CONSTRAINT tran_range_plan_details_size_pkey;
       public            postgres    false    353            '           2606    30545 2   tran_range_plan_events tran_range_plan_events_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_range_plan_events
    ADD CONSTRAINT tran_range_plan_events_pkey PRIMARY KEY (tran_range_plan_event_id);
 \   ALTER TABLE ONLY public.tran_range_plan_events DROP CONSTRAINT tran_range_plan_events_pkey;
       public            postgres    false    355            !           2606    30547 $   tran_range_plan tran_range_plan_pkey 
   CONSTRAINT     m   ALTER TABLE ONLY public.tran_range_plan
    ADD CONSTRAINT tran_range_plan_pkey PRIMARY KEY (range_plan_id);
 N   ALTER TABLE ONLY public.tran_range_plan DROP CONSTRAINT tran_range_plan_pkey;
       public            postgres    false    350            +           2606    30549 2   tran_sample_order_detl tran_sample_order_detl_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order_detl
    ADD CONSTRAINT tran_sample_order_detl_pkey PRIMARY KEY (tran_sample_order_detl_id);
 \   ALTER TABLE ONLY public.tran_sample_order_detl DROP CONSTRAINT tran_sample_order_detl_pkey;
       public            postgres    false    359            -           2606    30551 D   tran_sample_order_embellishment tran_sample_order_embellishment_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order_embellishment
    ADD CONSTRAINT tran_sample_order_embellishment_pkey PRIMARY KEY (tran_sample_order_embellishment_id);
 n   ALTER TABLE ONLY public.tran_sample_order_embellishment DROP CONSTRAINT tran_sample_order_embellishment_pkey;
       public            postgres    false    361            )           2606    30553 (   tran_sample_order tran_sample_order_pkey 
   CONSTRAINT     x   ALTER TABLE ONLY public.tran_sample_order
    ADD CONSTRAINT tran_sample_order_pkey PRIMARY KEY (tran_sample_order_id);
 R   ALTER TABLE ONLY public.tran_sample_order DROP CONSTRAINT tran_sample_order_pkey;
       public            postgres    false    358            /           2606    30555 :   tran_sample_order_subgroup tran_sample_order_subgroup_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order_subgroup
    ADD CONSTRAINT tran_sample_order_subgroup_pkey PRIMARY KEY (tran_sample_order_subgroup_id);
 d   ALTER TABLE ONLY public.tran_sample_order_subgroup DROP CONSTRAINT tran_sample_order_subgroup_pkey;
       public            postgres    false    363            3           2606    30557 (   tran_va_plan_detl tran_va_plan_detl_pkey 
   CONSTRAINT     x   ALTER TABLE ONLY public.tran_va_plan_detl
    ADD CONSTRAINT tran_va_plan_detl_pkey PRIMARY KEY (tran_va_plan_detl_id);
 R   ALTER TABLE ONLY public.tran_va_plan_detl DROP CONSTRAINT tran_va_plan_detl_pkey;
       public            postgres    false    367            7           2606    30559 @   tran_va_plan_detl_style_color tran_va_plan_detl_style_color_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl_style_color
    ADD CONSTRAINT tran_va_plan_detl_style_color_pkey PRIMARY KEY (tran_va_plan_detl_style_color_id);
 j   ALTER TABLE ONLY public.tran_va_plan_detl_style_color DROP CONSTRAINT tran_va_plan_detl_style_color_pkey;
       public            postgres    false    369            9           2606    30561 J   tran_va_plan_detl_style_color_size tran_va_plan_detl_style_color_size_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl_style_color_size
    ADD CONSTRAINT tran_va_plan_detl_style_color_size_pkey PRIMARY KEY (tran_va_plan_detl_style_color_size_id);
 t   ALTER TABLE ONLY public.tran_va_plan_detl_style_color_size DROP CONSTRAINT tran_va_plan_detl_style_color_size_pkey;
       public            postgres    false    370            5           2606    30563 4   tran_va_plan_detl_style tran_va_plan_detl_style_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl_style
    ADD CONSTRAINT tran_va_plan_detl_style_pkey PRIMARY KEY (tran_va_plan_detl_style_id);
 ^   ALTER TABLE ONLY public.tran_va_plan_detl_style DROP CONSTRAINT tran_va_plan_detl_style_pkey;
       public            postgres    false    368            �           2606    29064 ,   tran_va_plan_events tran_va_plan_events_pkey 
   CONSTRAINT     }   ALTER TABLE ONLY public.tran_va_plan_events
    ADD CONSTRAINT tran_va_plan_events_pkey PRIMARY KEY (tran_va_plan_event_id);
 V   ALTER TABLE ONLY public.tran_va_plan_events DROP CONSTRAINT tran_va_plan_events_pkey;
       public            postgres    false    247            1           2606    30565    tran_va_plan tran_va_plan_pkey 
   CONSTRAINT     i   ALTER TABLE ONLY public.tran_va_plan
    ADD CONSTRAINT tran_va_plan_pkey PRIMARY KEY (tran_va_plan_id);
 H   ALTER TABLE ONLY public.tran_va_plan DROP CONSTRAINT tran_va_plan_pkey;
       public            postgres    false    366            ;           2606    30567 P   tran_va_product_embellishment_mapping tran_va_product_embellishment_mapping_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_product_embellishment_mapping
    ADD CONSTRAINT tran_va_product_embellishment_mapping_pkey PRIMARY KEY (tran_va_product_embellishment_mapping_id);
 z   ALTER TABLE ONLY public.tran_va_product_embellishment_mapping DROP CONSTRAINT tran_va_product_embellishment_mapping_pkey;
       public            postgres    false    376            >           2606    30569    buckets buckets_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY storage.buckets
    ADD CONSTRAINT buckets_pkey PRIMARY KEY (id);
 ?   ALTER TABLE ONLY storage.buckets DROP CONSTRAINT buckets_pkey;
       storage            postgres    false    381            @           2606    30571    migrations migrations_name_key 
   CONSTRAINT     Z   ALTER TABLE ONLY storage.migrations
    ADD CONSTRAINT migrations_name_key UNIQUE (name);
 I   ALTER TABLE ONLY storage.migrations DROP CONSTRAINT migrations_name_key;
       storage            postgres    false    382            B           2606    30573    migrations migrations_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY storage.migrations
    ADD CONSTRAINT migrations_pkey PRIMARY KEY (id);
 E   ALTER TABLE ONLY storage.migrations DROP CONSTRAINT migrations_pkey;
       storage            postgres    false    382            F           2606    30575    objects objects_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY storage.objects
    ADD CONSTRAINT objects_pkey PRIMARY KEY (id);
 ?   ALTER TABLE ONLY storage.objects DROP CONSTRAINT objects_pkey;
       storage            postgres    false    383            �           1259    30576    audit_logs_instance_id_idx    INDEX     ]   CREATE INDEX audit_logs_instance_id_idx ON auth.audit_log_entries USING btree (instance_id);
 ,   DROP INDEX auth.audit_logs_instance_id_idx;
       auth            postgres    false    251            �           1259    30577    confirmation_token_idx    INDEX     �   CREATE UNIQUE INDEX confirmation_token_idx ON auth.users USING btree (confirmation_token) WHERE ((confirmation_token)::text !~ '^[0-9 ]*$'::text);
 (   DROP INDEX auth.confirmation_token_idx;
       auth            postgres    false    266    266            �           1259    30578    email_change_token_current_idx    INDEX     �   CREATE UNIQUE INDEX email_change_token_current_idx ON auth.users USING btree (email_change_token_current) WHERE ((email_change_token_current)::text !~ '^[0-9 ]*$'::text);
 0   DROP INDEX auth.email_change_token_current_idx;
       auth            postgres    false    266    266            �           1259    30579    email_change_token_new_idx    INDEX     �   CREATE UNIQUE INDEX email_change_token_new_idx ON auth.users USING btree (email_change_token_new) WHERE ((email_change_token_new)::text !~ '^[0-9 ]*$'::text);
 ,   DROP INDEX auth.email_change_token_new_idx;
       auth            postgres    false    266    266            �           1259    30580    factor_id_created_at_idx    INDEX     ]   CREATE INDEX factor_id_created_at_idx ON auth.mfa_factors USING btree (user_id, created_at);
 *   DROP INDEX auth.factor_id_created_at_idx;
       auth            postgres    false    257    257            �           1259    30581    flow_state_created_at_idx    INDEX     Y   CREATE INDEX flow_state_created_at_idx ON auth.flow_state USING btree (created_at DESC);
 +   DROP INDEX auth.flow_state_created_at_idx;
       auth            postgres    false    252            �           1259    30582    identities_email_idx    INDEX     [   CREATE INDEX identities_email_idx ON auth.identities USING btree (email text_pattern_ops);
 &   DROP INDEX auth.identities_email_idx;
       auth            postgres    false    253            '           0    0    INDEX identities_email_idx    COMMENT     c   COMMENT ON INDEX auth.identities_email_idx IS 'Auth: Ensures indexed queries on the email column';
          auth          postgres    false    3985            �           1259    30583    identities_user_id_idx    INDEX     N   CREATE INDEX identities_user_id_idx ON auth.identities USING btree (user_id);
 (   DROP INDEX auth.identities_user_id_idx;
       auth            postgres    false    253            �           1259    30584    idx_auth_code    INDEX     G   CREATE INDEX idx_auth_code ON auth.flow_state USING btree (auth_code);
    DROP INDEX auth.idx_auth_code;
       auth            postgres    false    252            �           1259    30585    idx_user_id_auth_method    INDEX     f   CREATE INDEX idx_user_id_auth_method ON auth.flow_state USING btree (user_id, authentication_method);
 )   DROP INDEX auth.idx_user_id_auth_method;
       auth            postgres    false    252    252            �           1259    30586    mfa_challenge_created_at_idx    INDEX     `   CREATE INDEX mfa_challenge_created_at_idx ON auth.mfa_challenges USING btree (created_at DESC);
 .   DROP INDEX auth.mfa_challenge_created_at_idx;
       auth            postgres    false    256            �           1259    30587 %   mfa_factors_user_friendly_name_unique    INDEX     �   CREATE UNIQUE INDEX mfa_factors_user_friendly_name_unique ON auth.mfa_factors USING btree (friendly_name, user_id) WHERE (TRIM(BOTH FROM friendly_name) <> ''::text);
 7   DROP INDEX auth.mfa_factors_user_friendly_name_unique;
       auth            postgres    false    257    257    257            �           1259    30588    mfa_factors_user_id_idx    INDEX     P   CREATE INDEX mfa_factors_user_id_idx ON auth.mfa_factors USING btree (user_id);
 )   DROP INDEX auth.mfa_factors_user_id_idx;
       auth            postgres    false    257            �           1259    30589    reauthentication_token_idx    INDEX     �   CREATE UNIQUE INDEX reauthentication_token_idx ON auth.users USING btree (reauthentication_token) WHERE ((reauthentication_token)::text !~ '^[0-9 ]*$'::text);
 ,   DROP INDEX auth.reauthentication_token_idx;
       auth            postgres    false    266    266            �           1259    30590    recovery_token_idx    INDEX     �   CREATE UNIQUE INDEX recovery_token_idx ON auth.users USING btree (recovery_token) WHERE ((recovery_token)::text !~ '^[0-9 ]*$'::text);
 $   DROP INDEX auth.recovery_token_idx;
       auth            postgres    false    266    266            �           1259    30591    refresh_tokens_instance_id_idx    INDEX     ^   CREATE INDEX refresh_tokens_instance_id_idx ON auth.refresh_tokens USING btree (instance_id);
 0   DROP INDEX auth.refresh_tokens_instance_id_idx;
       auth            postgres    false    258            �           1259    30592 &   refresh_tokens_instance_id_user_id_idx    INDEX     o   CREATE INDEX refresh_tokens_instance_id_user_id_idx ON auth.refresh_tokens USING btree (instance_id, user_id);
 8   DROP INDEX auth.refresh_tokens_instance_id_user_id_idx;
       auth            postgres    false    258    258            �           1259    30593    refresh_tokens_parent_idx    INDEX     T   CREATE INDEX refresh_tokens_parent_idx ON auth.refresh_tokens USING btree (parent);
 +   DROP INDEX auth.refresh_tokens_parent_idx;
       auth            postgres    false    258            �           1259    30598 %   refresh_tokens_session_id_revoked_idx    INDEX     m   CREATE INDEX refresh_tokens_session_id_revoked_idx ON auth.refresh_tokens USING btree (session_id, revoked);
 7   DROP INDEX auth.refresh_tokens_session_id_revoked_idx;
       auth            postgres    false    258    258            �           1259    30599    refresh_tokens_updated_at_idx    INDEX     a   CREATE INDEX refresh_tokens_updated_at_idx ON auth.refresh_tokens USING btree (updated_at DESC);
 /   DROP INDEX auth.refresh_tokens_updated_at_idx;
       auth            postgres    false    258            �           1259    30600 "   saml_providers_sso_provider_id_idx    INDEX     f   CREATE INDEX saml_providers_sso_provider_id_idx ON auth.saml_providers USING btree (sso_provider_id);
 4   DROP INDEX auth.saml_providers_sso_provider_id_idx;
       auth            postgres    false    260            �           1259    30601     saml_relay_states_created_at_idx    INDEX     g   CREATE INDEX saml_relay_states_created_at_idx ON auth.saml_relay_states USING btree (created_at DESC);
 2   DROP INDEX auth.saml_relay_states_created_at_idx;
       auth            postgres    false    261            �           1259    30602    saml_relay_states_for_email_idx    INDEX     `   CREATE INDEX saml_relay_states_for_email_idx ON auth.saml_relay_states USING btree (for_email);
 1   DROP INDEX auth.saml_relay_states_for_email_idx;
       auth            postgres    false    261            �           1259    30603 %   saml_relay_states_sso_provider_id_idx    INDEX     l   CREATE INDEX saml_relay_states_sso_provider_id_idx ON auth.saml_relay_states USING btree (sso_provider_id);
 7   DROP INDEX auth.saml_relay_states_sso_provider_id_idx;
       auth            postgres    false    261            �           1259    30604    sessions_not_after_idx    INDEX     S   CREATE INDEX sessions_not_after_idx ON auth.sessions USING btree (not_after DESC);
 (   DROP INDEX auth.sessions_not_after_idx;
       auth            postgres    false    263            �           1259    30605    sessions_user_id_idx    INDEX     J   CREATE INDEX sessions_user_id_idx ON auth.sessions USING btree (user_id);
 &   DROP INDEX auth.sessions_user_id_idx;
       auth            postgres    false    263            �           1259    30606    sso_domains_domain_idx    INDEX     \   CREATE UNIQUE INDEX sso_domains_domain_idx ON auth.sso_domains USING btree (lower(domain));
 (   DROP INDEX auth.sso_domains_domain_idx;
       auth            postgres    false    264    264            �           1259    30607    sso_domains_sso_provider_id_idx    INDEX     `   CREATE INDEX sso_domains_sso_provider_id_idx ON auth.sso_domains USING btree (sso_provider_id);
 1   DROP INDEX auth.sso_domains_sso_provider_id_idx;
       auth            postgres    false    264            �           1259    30608    sso_providers_resource_id_idx    INDEX     j   CREATE UNIQUE INDEX sso_providers_resource_id_idx ON auth.sso_providers USING btree (lower(resource_id));
 /   DROP INDEX auth.sso_providers_resource_id_idx;
       auth            postgres    false    265    265            �           1259    30609    user_id_created_at_idx    INDEX     X   CREATE INDEX user_id_created_at_idx ON auth.sessions USING btree (user_id, created_at);
 (   DROP INDEX auth.user_id_created_at_idx;
       auth            postgres    false    263    263            �           1259    30610    users_email_partial_key    INDEX     k   CREATE UNIQUE INDEX users_email_partial_key ON auth.users USING btree (email) WHERE (is_sso_user = false);
 )   DROP INDEX auth.users_email_partial_key;
       auth            postgres    false    266    266            (           0    0    INDEX users_email_partial_key    COMMENT     }   COMMENT ON INDEX auth.users_email_partial_key IS 'Auth: A partial unique index that applies only when is_sso_user is false';
          auth          postgres    false    4043            �           1259    30611    users_instance_id_email_idx    INDEX     h   CREATE INDEX users_instance_id_email_idx ON auth.users USING btree (instance_id, lower((email)::text));
 -   DROP INDEX auth.users_instance_id_email_idx;
       auth            postgres    false    266    266            �           1259    30612    users_instance_id_idx    INDEX     L   CREATE INDEX users_instance_id_idx ON auth.users USING btree (instance_id);
 '   DROP INDEX auth.users_instance_id_idx;
       auth            postgres    false    266            <           1259    30613    bname    INDEX     A   CREATE UNIQUE INDEX bname ON storage.buckets USING btree (name);
    DROP INDEX storage.bname;
       storage            postgres    false    381            C           1259    30614    bucketid_objname    INDEX     W   CREATE UNIQUE INDEX bucketid_objname ON storage.objects USING btree (bucket_id, name);
 %   DROP INDEX storage.bucketid_objname;
       storage            postgres    false    383    383            D           1259    30615    name_prefix_search    INDEX     X   CREATE INDEX name_prefix_search ON storage.objects USING btree (name text_pattern_ops);
 '   DROP INDEX storage.name_prefix_search;
       storage            postgres    false    383            �           2620    30616 #   tran_sample_order set_serial_number    TRIGGER     �   CREATE TRIGGER set_serial_number BEFORE INSERT ON public.tran_sample_order FOR EACH ROW EXECUTE FUNCTION public.generate_serial_number();
 <   DROP TRIGGER set_serial_number ON public.tran_sample_order;
       public          postgres    false    613    358            �           2620    30617 !   objects update_objects_updated_at    TRIGGER     �   CREATE TRIGGER update_objects_updated_at BEFORE UPDATE ON storage.objects FOR EACH ROW EXECUTE FUNCTION storage.update_updated_at_column();
 ;   DROP TRIGGER update_objects_updated_at ON storage.objects;
       storage          postgres    false    383    646            S           2606    30618 "   identities identities_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY auth.identities
    ADD CONSTRAINT identities_user_id_fkey FOREIGN KEY (user_id) REFERENCES auth.users(id) ON DELETE CASCADE;
 J   ALTER TABLE ONLY auth.identities DROP CONSTRAINT identities_user_id_fkey;
       auth          postgres    false    266    4049    253            T           2606    30623 -   mfa_amr_claims mfa_amr_claims_session_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY auth.mfa_amr_claims
    ADD CONSTRAINT mfa_amr_claims_session_id_fkey FOREIGN KEY (session_id) REFERENCES auth.sessions(id) ON DELETE CASCADE;
 U   ALTER TABLE ONLY auth.mfa_amr_claims DROP CONSTRAINT mfa_amr_claims_session_id_fkey;
       auth          postgres    false    255    263    4028            U           2606    30628 1   mfa_challenges mfa_challenges_auth_factor_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY auth.mfa_challenges
    ADD CONSTRAINT mfa_challenges_auth_factor_id_fkey FOREIGN KEY (factor_id) REFERENCES auth.mfa_factors(id) ON DELETE CASCADE;
 Y   ALTER TABLE ONLY auth.mfa_challenges DROP CONSTRAINT mfa_challenges_auth_factor_id_fkey;
       auth          postgres    false    4002    256    257            V           2606    30633 $   mfa_factors mfa_factors_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY auth.mfa_factors
    ADD CONSTRAINT mfa_factors_user_id_fkey FOREIGN KEY (user_id) REFERENCES auth.users(id) ON DELETE CASCADE;
 L   ALTER TABLE ONLY auth.mfa_factors DROP CONSTRAINT mfa_factors_user_id_fkey;
       auth          postgres    false    266    4049    257            W           2606    30638 -   refresh_tokens refresh_tokens_session_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY auth.refresh_tokens
    ADD CONSTRAINT refresh_tokens_session_id_fkey FOREIGN KEY (session_id) REFERENCES auth.sessions(id) ON DELETE CASCADE;
 U   ALTER TABLE ONLY auth.refresh_tokens DROP CONSTRAINT refresh_tokens_session_id_fkey;
       auth          postgres    false    4028    258    263            X           2606    30643 2   saml_providers saml_providers_sso_provider_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY auth.saml_providers
    ADD CONSTRAINT saml_providers_sso_provider_id_fkey FOREIGN KEY (sso_provider_id) REFERENCES auth.sso_providers(id) ON DELETE CASCADE;
 Z   ALTER TABLE ONLY auth.saml_providers DROP CONSTRAINT saml_providers_sso_provider_id_fkey;
       auth          postgres    false    4036    260    265            Y           2606    30648 6   saml_relay_states saml_relay_states_flow_state_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY auth.saml_relay_states
    ADD CONSTRAINT saml_relay_states_flow_state_id_fkey FOREIGN KEY (flow_state_id) REFERENCES auth.flow_state(id) ON DELETE CASCADE;
 ^   ALTER TABLE ONLY auth.saml_relay_states DROP CONSTRAINT saml_relay_states_flow_state_id_fkey;
       auth          postgres    false    3982    261    252            Z           2606    30653 8   saml_relay_states saml_relay_states_sso_provider_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY auth.saml_relay_states
    ADD CONSTRAINT saml_relay_states_sso_provider_id_fkey FOREIGN KEY (sso_provider_id) REFERENCES auth.sso_providers(id) ON DELETE CASCADE;
 `   ALTER TABLE ONLY auth.saml_relay_states DROP CONSTRAINT saml_relay_states_sso_provider_id_fkey;
       auth          postgres    false    4036    265    261            [           2606    30658    sessions sessions_user_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY auth.sessions
    ADD CONSTRAINT sessions_user_id_fkey FOREIGN KEY (user_id) REFERENCES auth.users(id) ON DELETE CASCADE;
 F   ALTER TABLE ONLY auth.sessions DROP CONSTRAINT sessions_user_id_fkey;
       auth          postgres    false    4049    263    266            \           2606    30663 ,   sso_domains sso_domains_sso_provider_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY auth.sso_domains
    ADD CONSTRAINT sso_domains_sso_provider_id_fkey FOREIGN KEY (sso_provider_id) REFERENCES auth.sso_providers(id) ON DELETE CASCADE;
 T   ALTER TABLE ONLY auth.sso_domains DROP CONSTRAINT sso_domains_sso_provider_id_fkey;
       auth          postgres    false    265    4036    264            ]           2606    30668    gen_district divisionid    FK CONSTRAINT     �   ALTER TABLE ONLY public.gen_district
    ADD CONSTRAINT divisionid FOREIGN KEY (divisionid) REFERENCES public.gen_division(division_id) NOT VALID;
 A   ALTER TABLE ONLY public.gen_district DROP CONSTRAINT divisionid;
       public          postgres    false    274    275    4061            �           2606    30673 *   tran_va_plan_detl_style_color_size fk_delt    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl_style_color_size
    ADD CONSTRAINT fk_delt FOREIGN KEY (tran_va_plan_detl_style_color_id) REFERENCES public.tran_va_plan_detl_style_color(tran_va_plan_detl_style_color_id) ON DELETE CASCADE NOT VALID;
 T   ALTER TABLE ONLY public.tran_va_plan_detl_style_color_size DROP CONSTRAINT fk_delt;
       public          postgres    false    370    369    4151            �           2606    30678 !   tran_sample_order_subgroup fk_det    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order_subgroup
    ADD CONSTRAINT fk_det FOREIGN KEY (tran_sample_order_id) REFERENCES public.tran_sample_order(tran_sample_order_id) ON DELETE CASCADE NOT VALID;
 K   ALTER TABLE ONLY public.tran_sample_order_subgroup DROP CONSTRAINT fk_det;
       public          postgres    false    4137    358    363                       2606    30683 &   tran_sample_order_embellishment fk_det    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order_embellishment
    ADD CONSTRAINT fk_det FOREIGN KEY (tran_sample_order_id) REFERENCES public.tran_sample_order(tran_sample_order_id) ON DELETE CASCADE NOT VALID;
 P   ALTER TABLE ONLY public.tran_sample_order_embellishment DROP CONSTRAINT fk_det;
       public          postgres    false    4137    361    358            ^           2606    30688 $   gen_item_structure_group_sub fk_detl    FK CONSTRAINT     �   ALTER TABLE ONLY public.gen_item_structure_group_sub
    ADD CONSTRAINT fk_detl FOREIGN KEY (item_structure_group_id) REFERENCES public.gen_item_structure_group(item_structure_group_id) NOT VALID;
 N   ALTER TABLE ONLY public.gen_item_structure_group_sub DROP CONSTRAINT fk_detl;
       public          postgres    false    279    4065    277            |           2606    30693    tran_sample_order_detl fk_detl    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order_detl
    ADD CONSTRAINT fk_detl FOREIGN KEY (tran_sample_order_id) REFERENCES public.tran_sample_order(tran_sample_order_id) ON DELETE CASCADE;
 H   ALTER TABLE ONLY public.tran_sample_order_detl DROP CONSTRAINT fk_detl;
       public          postgres    false    4137    358    359            �           2606    30962    gen_segment_detl fk_detl    FK CONSTRAINT     �   ALTER TABLE ONLY public.gen_segment_detl
    ADD CONSTRAINT fk_detl FOREIGN KEY (gen_segment_id) REFERENCES public.gen_segment(gen_segment_id) NOT VALID;
 B   ALTER TABLE ONLY public.gen_segment_detl DROP CONSTRAINT fk_detl;
       public          postgres    false    391    4174    385            �           2606    30971    style_fabric_detl fk_detl    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_fabric_detl
    ADD CONSTRAINT fk_detl FOREIGN KEY (style_fabric_id) REFERENCES public.style_fabric(style_fabric_id) ON DELETE CASCADE NOT VALID;
 C   ALTER TABLE ONLY public.style_fabric_detl DROP CONSTRAINT fk_detl;
       public          postgres    false    387    389    4170            w           2606    30698 )   tran_range_plan_details_size fk_detl_size    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_range_plan_details_size
    ADD CONSTRAINT fk_detl_size FOREIGN KEY (style_product_size_group_detid) REFERENCES public.style_product_size_group_details(style_product_size_group_detid) NOT VALID;
 S   ALTER TABLE ONLY public.tran_range_plan_details_size DROP CONSTRAINT fk_detl_size;
       public          postgres    false    4115    353    325            Q           2606    30703    tran_va_plan_events fk_detls    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_events
    ADD CONSTRAINT fk_detls FOREIGN KEY (tran_va_plan_id) REFERENCES public.tran_va_plan(tran_va_plan_id) NOT VALID;
 F   ALTER TABLE ONLY public.tran_va_plan_events DROP CONSTRAINT fk_detls;
       public          postgres    false    366    4145    247            y           2606    30708    tran_range_plan_events fk_event    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_range_plan_events
    ADD CONSTRAINT fk_event FOREIGN KEY (tran_bp_event_id) REFERENCES public.tran_bp_event(tran_bp_event_id);
 I   ALTER TABLE ONLY public.tran_range_plan_events DROP CONSTRAINT fk_event;
       public          postgres    false    4123    355    344            R           2606    30713    tran_va_plan_events fk_event    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_events
    ADD CONSTRAINT fk_event FOREIGN KEY (tran_range_plan_event_id) REFERENCES public.tran_range_plan_events(tran_range_plan_event_id) NOT VALID;
 F   ALTER TABLE ONLY public.tran_va_plan_events DROP CONSTRAINT fk_event;
       public          postgres    false    247    4135    355            �           2606    30718    tran_va_plan_detl fk_event_ids    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl
    ADD CONSTRAINT fk_event_ids FOREIGN KEY (tran_va_plan_event_id) REFERENCES public.tran_va_plan_events(tran_va_plan_event_id);
 H   ALTER TABLE ONLY public.tran_va_plan_detl DROP CONSTRAINT fk_event_ids;
       public          postgres    false    247    3976    367            n           2606    30723    tran_bp_event fk_eventid    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_bp_event
    ADD CONSTRAINT fk_eventid FOREIGN KEY (event_id) REFERENCES public.gen_season_event_config(event_id) NOT VALID;
 B   ALTER TABLE ONLY public.tran_bp_event DROP CONSTRAINT fk_eventid;
       public          postgres    false    344    285    4075            _           2606    30728 %   gen_season_event_config fk_fiscalyear    FK CONSTRAINT     �   ALTER TABLE ONLY public.gen_season_event_config
    ADD CONSTRAINT fk_fiscalyear FOREIGN KEY (fiscal_year_id) REFERENCES public.gen_fiscal_year(fiscal_year_id) NOT VALID;
 O   ALTER TABLE ONLY public.gen_season_event_config DROP CONSTRAINT fk_fiscalyear;
       public          postgres    false    276    4063    285            c           2606    30733    style_item_product fk_gender    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_item_product
    ADD CONSTRAINT fk_gender FOREIGN KEY (style_gender_id) REFERENCES public.style_gender(style_gender_id) NOT VALID;
 F   ALTER TABLE ONLY public.style_item_product DROP CONSTRAINT fk_gender;
       public          postgres    false    314    4097    311            d           2606    30738    style_item_product fk_itemtype    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_item_product
    ADD CONSTRAINT fk_itemtype FOREIGN KEY (style_item_type_id) REFERENCES public.style_item_type(style_item_type_id) NOT VALID;
 H   ALTER TABLE ONLY public.style_item_product DROP CONSTRAINT fk_itemtype;
       public          postgres    false    318    4105    314            l           2606    30743 .   style_product_size_group_details fk_master_det    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_product_size_group_details
    ADD CONSTRAINT fk_master_det FOREIGN KEY (style_product_size_group_id) REFERENCES public.style_product_size_group(style_product_size_group_id) NOT VALID;
 X   ALTER TABLE ONLY public.style_product_size_group_details DROP CONSTRAINT fk_master_det;
       public          postgres    false    325    4113    324            e           2606    30748 #   style_item_product fk_mastercatgory    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_item_product
    ADD CONSTRAINT fk_mastercatgory FOREIGN KEY (style_master_category_id) REFERENCES public.style_master_category(style_master_category_id) NOT VALID;
 M   ALTER TABLE ONLY public.style_item_product DROP CONSTRAINT fk_mastercatgory;
       public          postgres    false    314    321    4109            u           2606    30753 $   tran_range_plan_details fk_masterdet    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_range_plan_details
    ADD CONSTRAINT fk_masterdet FOREIGN KEY (range_plan_id) REFERENCES public.tran_range_plan(range_plan_id) ON DELETE CASCADE;
 N   ALTER TABLE ONLY public.tran_range_plan_details DROP CONSTRAINT fk_masterdet;
       public          postgres    false    350    4129    351            a           2606    30758    gen_team_members fk_masterdet    FK CONSTRAINT     �   ALTER TABLE ONLY public.gen_team_members
    ADD CONSTRAINT fk_masterdet FOREIGN KEY (gen_team_group_id) REFERENCES public.gen_team_group(gen_team_group_id) NOT VALID;
 G   ALTER TABLE ONLY public.gen_team_members DROP CONSTRAINT fk_masterdet;
       public          postgres    false    4077    288    286            k           2606    30763 ;   style_master_category_structure_subgroup_mapping fk_mstrcat    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_master_category_structure_subgroup_mapping
    ADD CONSTRAINT fk_mstrcat FOREIGN KEY (style_master_category_id) REFERENCES public.style_master_category(style_master_category_id) NOT VALID;
 e   ALTER TABLE ONLY public.style_master_category_structure_subgroup_mapping DROP CONSTRAINT fk_mstrcat;
       public          postgres    false    4109    321    322            f           2606    30768    style_item_product fk_origin    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_item_product
    ADD CONSTRAINT fk_origin FOREIGN KEY (style_item_origin_id) REFERENCES public.style_item_origin(style_item_origin_id) NOT VALID;
 F   ALTER TABLE ONLY public.style_item_product DROP CONSTRAINT fk_origin;
       public          postgres    false    314    312    4099            r           2606    30773 &   tran_bp_event_month_outlet fk_outletid    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_bp_event_month_outlet
    ADD CONSTRAINT fk_outletid FOREIGN KEY (outlet_id) REFERENCES public.gen_outlet(outlet_id) NOT VALID;
 P   ALTER TABLE ONLY public.tran_bp_event_month_outlet DROP CONSTRAINT fk_outletid;
       public          postgres    false    4069    281    346            x           2606    30778 (   tran_range_plan_details_size fk_plandetl    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_range_plan_details_size
    ADD CONSTRAINT fk_plandetl FOREIGN KEY (range_plan_detail_id) REFERENCES public.tran_range_plan_details(range_plan_detail_id) ON DELETE CASCADE NOT VALID;
 R   ALTER TABLE ONLY public.tran_range_plan_details_size DROP CONSTRAINT fk_plandetl;
       public          postgres    false    4131    353    351            �           2606    30783 #   tran_va_plan_detl_style fk_plandetl    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl_style
    ADD CONSTRAINT fk_plandetl FOREIGN KEY (tran_va_plan_detl_id) REFERENCES public.tran_va_plan_detl(tran_va_plan_detl_id) ON DELETE CASCADE NOT VALID;
 M   ALTER TABLE ONLY public.tran_va_plan_detl_style DROP CONSTRAINT fk_plandetl;
       public          postgres    false    4147    368    367            g           2606    30788    style_item_product fk_prodtype    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_item_product
    ADD CONSTRAINT fk_prodtype FOREIGN KEY (style_product_type_id) REFERENCES public.style_product_type(style_product_type_id) NOT VALID;
 H   ALTER TABLE ONLY public.style_item_product DROP CONSTRAINT fk_prodtype;
       public          postgres    false    328    4117    314            v           2606    30793 !   tran_range_plan_details fk_produc    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_range_plan_details
    ADD CONSTRAINT fk_produc FOREIGN KEY (style_item_product_id) REFERENCES public.style_item_product(style_item_product_id);
 K   ALTER TABLE ONLY public.tran_range_plan_details DROP CONSTRAINT fk_produc;
       public          postgres    false    314    4101    351            �           2606    30798    tran_va_plan_detl fk_product    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl
    ADD CONSTRAINT fk_product FOREIGN KEY (style_item_product_id) REFERENCES public.style_item_product(style_item_product_id) NOT VALID;
 F   ALTER TABLE ONLY public.tran_va_plan_detl DROP CONSTRAINT fk_product;
       public          postgres    false    314    4101    367            z           2606    30803 $   tran_range_plan_events fk_range_plan    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_range_plan_events
    ADD CONSTRAINT fk_range_plan FOREIGN KEY (range_plan_id) REFERENCES public.tran_range_plan(range_plan_id) ON DELETE CASCADE;
 N   ALTER TABLE ONLY public.tran_range_plan_events DROP CONSTRAINT fk_range_plan;
       public          postgres    false    4129    355    350            �           2606    30808    tran_va_plan fk_rangeplan    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan
    ADD CONSTRAINT fk_rangeplan FOREIGN KEY (tran_range_plan_id) REFERENCES public.tran_range_plan(range_plan_id) NOT VALID;
 C   ALTER TABLE ONLY public.tran_va_plan DROP CONSTRAINT fk_rangeplan;
       public          postgres    false    350    366    4129            �           2606    30813     tran_va_plan_detl fk_rangeplanid    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl
    ADD CONSTRAINT fk_rangeplanid FOREIGN KEY (range_plan_detail_id) REFERENCES public.tran_range_plan_details(range_plan_detail_id);
 J   ALTER TABLE ONLY public.tran_va_plan_detl DROP CONSTRAINT fk_rangeplanid;
       public          postgres    false    4131    351    367            `           2606    30818 !   gen_season_event_config fk_season    FK CONSTRAINT     �   ALTER TABLE ONLY public.gen_season_event_config
    ADD CONSTRAINT fk_season FOREIGN KEY (season_id) REFERENCES public.gen_season(season_id) NOT VALID;
 K   ALTER TABLE ONLY public.gen_season_event_config DROP CONSTRAINT fk_season;
       public          postgres    false    4073    284    285            �           2606    30976    style_fabric_detl fk_segment    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_fabric_detl
    ADD CONSTRAINT fk_segment FOREIGN KEY (gen_segment_id) REFERENCES public.gen_segment(gen_segment_id) NOT VALID;
 F   ALTER TABLE ONLY public.style_fabric_detl DROP CONSTRAINT fk_segment;
       public          postgres    false    391    4174    389            }           2606    30823    tran_sample_order_detl fk_size    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order_detl
    ADD CONSTRAINT fk_size FOREIGN KEY (style_product_size_group_detid) REFERENCES public.style_product_size_group_details(style_product_size_group_detid);
 H   ALTER TABLE ONLY public.tran_sample_order_detl DROP CONSTRAINT fk_size;
       public          postgres    false    359    4115    325            h           2606    30828    style_item_product fk_size_ggrp    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_item_product
    ADD CONSTRAINT fk_size_ggrp FOREIGN KEY (style_product_size_group_id) REFERENCES public.style_product_size_group(style_product_size_group_id) NOT VALID;
 I   ALTER TABLE ONLY public.style_item_product DROP CONSTRAINT fk_size_ggrp;
       public          postgres    false    4113    324    314            �           2606    30833 4   tran_va_plan_detl_style_color_size fk_sizegroup_detl    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl_style_color_size
    ADD CONSTRAINT fk_sizegroup_detl FOREIGN KEY (style_product_size_group_detid) REFERENCES public.style_product_size_group_details(style_product_size_group_detid) NOT VALID;
 ^   ALTER TABLE ONLY public.tran_va_plan_detl_style_color_size DROP CONSTRAINT fk_sizegroup_detl;
       public          postgres    false    4115    370    325            �           2606    30838 &   tran_va_plan_detl_style_color fk_style    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl_style_color
    ADD CONSTRAINT fk_style FOREIGN KEY (tran_va_plan_detl_style_id) REFERENCES public.tran_va_plan_detl_style(tran_va_plan_detl_style_id) ON DELETE CASCADE NOT VALID;
 P   ALTER TABLE ONLY public.tran_va_plan_detl_style_color DROP CONSTRAINT fk_style;
       public          postgres    false    4149    369    368            �           2606    30843 $   tran_sample_order_subgroup fk_subcat    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order_subgroup
    ADD CONSTRAINT fk_subcat FOREIGN KEY (gen_item_structure_group_sub_id) REFERENCES public.gen_item_structure_group_sub(gen_item_structure_group_sub_id) ON DELETE CASCADE NOT VALID;
 N   ALTER TABLE ONLY public.tran_sample_order_subgroup DROP CONSTRAINT fk_subcat;
       public          postgres    false    4067    363    279            �           2606    30848 )   tran_sample_order_embellishment fk_subcat    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order_embellishment
    ADD CONSTRAINT fk_subcat FOREIGN KEY (embellishment_id) REFERENCES public.style_embellishment(style_embellishment_id) ON DELETE CASCADE NOT VALID;
 S   ALTER TABLE ONLY public.tran_sample_order_embellishment DROP CONSTRAINT fk_subcat;
       public          postgres    false    309    361    4095            s           2606    30853 3   tran_bp_event_month_outlet fk_tran_bp_event_monthid    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_bp_event_month_outlet
    ADD CONSTRAINT fk_tran_bp_event_monthid FOREIGN KEY (tran_bp_event_month_id) REFERENCES public.tran_bp_event_month(tran_bp_event_month_id) ON DELETE CASCADE NOT VALID;
 ]   ALTER TABLE ONLY public.tran_bp_event_month_outlet DROP CONSTRAINT fk_tran_bp_event_monthid;
       public          postgres    false    345    346    4125            p           2606    30858 &   tran_bp_event_month fk_tran_bp_eventid    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_bp_event_month
    ADD CONSTRAINT fk_tran_bp_eventid FOREIGN KEY (tran_bp_event_id) REFERENCES public.tran_bp_event(tran_bp_event_id) ON DELETE CASCADE NOT VALID;
 P   ALTER TABLE ONLY public.tran_bp_event_month DROP CONSTRAINT fk_tran_bp_eventid;
       public          postgres    false    344    345    4123            t           2606    30863    tran_range_plan fk_tran_bp_year    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_range_plan
    ADD CONSTRAINT fk_tran_bp_year FOREIGN KEY (tran_bp_year_id) REFERENCES public.tran_bp_year(tran_bp_year_id) NOT VALID;
 I   ALTER TABLE ONLY public.tran_range_plan DROP CONSTRAINT fk_tran_bp_year;
       public          postgres    false    350    4121    342            o           2606    30868    tran_bp_event fk_tran_bp_yearid    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_bp_event
    ADD CONSTRAINT fk_tran_bp_yearid FOREIGN KEY (tran_bp_year_id) REFERENCES public.tran_bp_year(tran_bp_year_id) ON DELETE CASCADE NOT VALID;
 I   ALTER TABLE ONLY public.tran_bp_event DROP CONSTRAINT fk_tran_bp_yearid;
       public          postgres    false    342    344    4121            {           2606    30873    tran_sample_order fk_unit    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order
    ADD CONSTRAINT fk_unit FOREIGN KEY (delivery_unit_id) REFERENCES public.gen_unit(gen_unit_id);
 C   ALTER TABLE ONLY public.tran_sample_order DROP CONSTRAINT fk_unit;
       public          postgres    false    4081    358    290            ~           2606    30878    tran_sample_order_detl fk_unit    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_sample_order_detl
    ADD CONSTRAINT fk_unit FOREIGN KEY (style_product_unit_id) REFERENCES public.style_product_unit(style_product_unit_id);
 H   ALTER TABLE ONLY public.tran_sample_order_detl DROP CONSTRAINT fk_unit;
       public          postgres    false    330    4119    359            b           2606    30883    gen_team_members fk_user    FK CONSTRAINT     �   ALTER TABLE ONLY public.gen_team_members
    ADD CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES public.owin_user(userid) NOT VALID;
 B   ALTER TABLE ONLY public.gen_team_members DROP CONSTRAINT fk_user;
       public          postgres    false    4089    288    303            i           2606    30888    style_item_product fk_year    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_item_product
    ADD CONSTRAINT fk_year FOREIGN KEY (fiscal_year_id) REFERENCES public.gen_fiscal_year(fiscal_year_id) NOT VALID;
 D   ALTER TABLE ONLY public.style_item_product DROP CONSTRAINT fk_year;
       public          postgres    false    4063    276    314            j           2606    30893 0   style_item_product_sub_category product_category    FK CONSTRAINT     �   ALTER TABLE ONLY public.style_item_product_sub_category
    ADD CONSTRAINT product_category FOREIGN KEY (style_item_product_id) REFERENCES public.style_item_product(style_item_product_id) ON DELETE CASCADE NOT VALID;
 Z   ALTER TABLE ONLY public.style_item_product_sub_category DROP CONSTRAINT product_category;
       public          postgres    false    4101    314    316            �           2606    30898 &   tran_va_plan_detl_style sub_categoryid    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_va_plan_detl_style
    ADD CONSTRAINT sub_categoryid FOREIGN KEY (style_item_product_sub_category_id) REFERENCES public.style_item_product_sub_category(style_item_product_sub_category_id) NOT VALID;
 P   ALTER TABLE ONLY public.tran_va_plan_detl_style DROP CONSTRAINT sub_categoryid;
       public          postgres    false    368    4103    316            q           2606    30903 6   tran_bp_event_month tran_bp_season_month_month_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_bp_event_month
    ADD CONSTRAINT tran_bp_season_month_month_id_fkey FOREIGN KEY (month_id) REFERENCES public.month(month_id) NOT VALID;
 `   ALTER TABLE ONLY public.tran_bp_event_month DROP CONSTRAINT tran_bp_season_month_month_id_fkey;
       public          postgres    false    4085    293    345            m           2606    30908 -   tran_bp_year tran_bp_year_fiscal_year_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.tran_bp_year
    ADD CONSTRAINT tran_bp_year_fiscal_year_id_fkey FOREIGN KEY (fiscal_year_id) REFERENCES public.gen_fiscal_year(fiscal_year_id) NOT VALID;
 W   ALTER TABLE ONLY public.tran_bp_year DROP CONSTRAINT tran_bp_year_fiscal_year_id_fkey;
       public          postgres    false    4063    276    342            �           2606    30913    objects objects_bucketId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY storage.objects
    ADD CONSTRAINT "objects_bucketId_fkey" FOREIGN KEY (bucket_id) REFERENCES storage.buckets(id);
 J   ALTER TABLE ONLY storage.objects DROP CONSTRAINT "objects_bucketId_fkey";
       storage          postgres    false    383    4158    381            *           0    30396    buckets    ROW SECURITY     6   ALTER TABLE storage.buckets ENABLE ROW LEVEL SECURITY;          storage          postgres    false    381            /           3256    30918 #   objects file_uplaod_policy 33sdmd_0    POLICY     �   CREATE POLICY "file_uplaod_policy 33sdmd_0" ON storage.objects FOR INSERT TO anon WITH CHECK ((bucket_id = 'sailor_bucket'::text));
 >   DROP POLICY "file_uplaod_policy 33sdmd_0" ON storage.objects;
       storage          postgres    false    383    383            .           3256    30919 #   objects file_uplaod_policy 33sdmd_1    POLICY        CREATE POLICY "file_uplaod_policy 33sdmd_1" ON storage.objects FOR SELECT TO anon USING ((bucket_id = 'sailor_bucket'::text));
 >   DROP POLICY "file_uplaod_policy 33sdmd_1" ON storage.objects;
       storage          postgres    false    383    383            -           3256    30920 #   objects file_uplaod_policy 33sdmd_2    POLICY        CREATE POLICY "file_uplaod_policy 33sdmd_2" ON storage.objects FOR DELETE TO anon USING ((bucket_id = 'sailor_bucket'::text));
 >   DROP POLICY "file_uplaod_policy 33sdmd_2" ON storage.objects;
       storage          postgres    false    383    383            +           0    30405 
   migrations    ROW SECURITY     9   ALTER TABLE storage.migrations ENABLE ROW LEVEL SECURITY;          storage          postgres    false    382            ,           0    30409    objects    ROW SECURITY     6   ALTER TABLE storage.objects ENABLE ROW LEVEL SECURITY;          storage          postgres    false    383            0           6104    30921    supabase_realtime    PUBLICATION     Z   CREATE PUBLICATION supabase_realtime WITH (publish = 'insert, update, delete, truncate');
 $   DROP PUBLICATION supabase_realtime;
                postgres    false            C           3466    16615    issue_graphql_placeholder    EVENT TRIGGER     �   CREATE EVENT TRIGGER issue_graphql_placeholder ON sql_drop
         WHEN TAG IN ('DROP EXTENSION')
   EXECUTE FUNCTION extensions.set_graphql_placeholder();
 .   DROP EVENT TRIGGER issue_graphql_placeholder;
                supabase_admin    false    407            G           3466    16993    issue_pg_cron_access    EVENT TRIGGER     �   CREATE EVENT TRIGGER issue_pg_cron_access ON ddl_command_end
         WHEN TAG IN ('CREATE EXTENSION')
   EXECUTE FUNCTION extensions.grant_pg_cron_access();
 )   DROP EVENT TRIGGER issue_pg_cron_access;
                supabase_admin    false    614            B           3466    16613    issue_pg_graphql_access    EVENT TRIGGER     �   CREATE EVENT TRIGGER issue_pg_graphql_access ON ddl_command_end
         WHEN TAG IN ('CREATE FUNCTION')
   EXECUTE FUNCTION extensions.grant_pg_graphql_access();
 ,   DROP EVENT TRIGGER issue_pg_graphql_access;
                supabase_admin    false    408            D           3466    16616    pgrst_ddl_watch    EVENT TRIGGER     j   CREATE EVENT TRIGGER pgrst_ddl_watch ON ddl_command_end
   EXECUTE FUNCTION extensions.pgrst_ddl_watch();
 $   DROP EVENT TRIGGER pgrst_ddl_watch;
                supabase_admin    false    405            E           3466    16617    pgrst_drop_watch    EVENT TRIGGER     e   CREATE EVENT TRIGGER pgrst_drop_watch ON sql_drop
   EXECUTE FUNCTION extensions.pgrst_drop_watch();
 %   DROP EVENT TRIGGER pgrst_drop_watch;
                supabase_admin    false    406            3      x������ � �      4      x������ � �      5      x������ � �      6      x������ � �      7      x������ � �      8      x������ � �      9      x������ � �      :      x������ � �      <      x������ � �      =      x������ � �      >     x�U�Y��0�&E7��.s�sLcI����E :��@�q��aoz���`?:s뛘D��&S��L��|��7�]QN��i�vb';��&@"��P�y�)�gx�;��A`�"���E2Te`��t�ui��)|�^��7�z���$e��u[x�'��)���~������~�Y�����k&Ovl-"L�[�6k�����<�ǛsQBӨ/����,:���Xs�R�7�p�9���u#Q[P�C�3-�܅fm���n�ӈ:^��7�?�hJl��纮4A��      ?      x������ � �      @      x������ � �      A      x������ � �      B      x������ � �      J      x������ � �      I   2   x�3��"]�Ԃ�?N0a�@����X�����(����� \�G      J      x�3�t�H�N�4�����        K      x�3�t�H�NTp�,�,�������� L�+      L   =   x�3�4202�@��20�501Mt�t�8�8K8�r�@�```F�1~@����� ��      M   P   x�3�tKL*�L�4�4202�50"+0�60���".#�����t����������bBZ�9�2�2�32�H����� �y#�      O   �   x�3�4�tKL*�L2���u�H���
��8c������9?''���Jc��Ҵ4B�L8�8�S�3��B2�RSi0jp*-)��#���2*�� ��c�9�9=ANHL'����)�(9?%U!�$39���1z\\\ uKv      Q   9  x���MK�@�ϓ_���H����F��/��$d�&���E�w,i���,yއM�&��_�9D���.
;�8�� �h�ax�_4�bX�����F��� �^+�*�3i;�1�����Yq
�@kue��H������пu֙�B��C�x�����K�B�Y�)d��u����c7U�U!�;�h%�p�z=�Ц<$W�� d]�VG`;"��Za�~+iޝ��O�b�Ѫ2۔ªkJ�JU��=x�P!��vLa�բ�}��3�ZKg�v/������=��~>i�\C6�Q�n�\S~�y�7�QO�      R   +   x�3�ru�q��#.#�� g�Ww��H�=... `�M      T   V   x�3�tK�Ɂ%���FFƺ����
V`�K�˄3�477�(&� �.#��̼��"E�~#.c����̼t��s��qqq u7?      U   /  x���=o�0���
�֝}篭�����K���Ј�K}��&��;�L�mJ���1��@8�p�KpY>�92E��;�����T	+�B��S�YwD��u���2�x��t�|�t*�zZ�\�_Ҝ��l�wG���$�����W�.5�:.���D��)���%'H0|�<�a=�����M��rל�ze��עk�fl��"d&	��D�+68L���x��W>���Jr���� Sj3���HNk��|V[���]�v$1�@{��I��]�Ey��9�������3���+U�/2ե�      �   |   x�34�t��-�/�,���Ca����!�������!)X����D����'����4j �8�9݃}��8�&�n�IE��
��9�E��L0��L)ɀ���1�L,�S�,HEb�7F��� �8Wb      �      x���ݒ7�&x�}���>Fk)�؛5�HI�MJ�����\L��"�(YY�U%5���9��6"� �E��2;6#�^��p|��t_��W��a��x����ߝ6z�����������W�W�Q�OM;�ߦi�����4����׏���ƫd���oI�ۻ���N �eنG�&;����w�ǻݦm6��E{Ϣ�d�v��6��$��8����@�u�	>��}�Y�%,�d�Gw�>it)\��Э�|���������IO;�詇��j
g���5)����xL�ɨY����O�z,s �V�L7T�ط�YVi�$&E��EQ�??i�7��$��B�:���{������h�dܤ}n�mMVO�י��E�RH���}_9��1P�Χ�t�I�� ��@F+��d�gߟv�7/w��͇v�u_0.�R���-�i�vV�X� 	d%�9+)1i�[4R�X�;hs�Y[6d���;� �dr�ls3�̦��E��2���7vCS�G��N:kgM_��6��hA��`U��$�]u�A0�[�>k5�N�k�3�
s�ٮ:�v�^��	��`.����(h*}vm��ر�]v��2���uf-I2�Nm�AF%��qo}�k _e�TZl� zF-����֤�Ν�]�O#���qkj�#� :��`�	�����Ϋ���%�Ο�� �%��2'��U��w��ʘji%My|�=H�ٌT.���x�8*�+��+�5)u�Ϙp�'�6���t���&����y~���Atf%���H��|���Jo֒jͰg�88��4�7���K���4?�
���؍��9X�8���|S�!��g��n1�I��t�[�^�=u��ɯ2ȅ�s@WU��~�9p]��[Р�IM���%vU"���xA��i浝F˛v�v{}�|�_����0�=_T������/W���-�<U��m��Onl��8��u����_Z@��܈�Ohj�p�7 H����D�Y���u��Vg��@�-�:�]m6G:�,����Yy7x�-w�כX���ձ��u�vu�CJ�^���=�Z��Gam��0pf*v���u�U[""�"~ظ�6��Ż 4$D	�CՌ��h2�@����ֹ�=���F��s�/4֡]�ܨ,rգw�Nԩe[.�F.P�ei���x�am3a��'������n p|����і�4"�VT�]l
�2SB��1�](*HO���TY<���Io!LLή��J3��^i���}�h�5I���ʫQ|[�/:"5]Dj�����n�pTW�F��tg��?ǩ
vE4�do�j��%��'��z���t�7�Wq�Y��>��������p���ޣ��ϧ��Nڵx����t"b�+@D;����F��|�ls{��_w����È�óf���z6#)��b��}�)c�� �*�iS+]�O��Pڕ���N$�]�&�n��̐2�Σ�Yv8�G1��9#�Ab;��ۂ�%�;�o[[���FP`S3��N��<PŠ�.���I`�%�D�,����g~��0$���b_�-�o�~��6��\.G�^��iv˫W��I�s���
[�ƹp����h&3uŠ�5�[	�y�"7�T���Ud[�,�P���!3U$Bg�u	ԯBL�K��T�o[����B�kpuNK̦�K*s��	�����/�\aO�xoc�F9����RDsf`feK��Z�(x�)�9�-`��K�0�Q
 \�.sN���4��$S��r�pLU)@U�3�b,�������^@�������Q��R�.L�Nd6�zc����#q�%�\�Z��׊�,�H7��Ҍg�� �eE��P"�3��16�Bׂˊ(�8:��R�%�q��-85]p�*/b5�
�S�df��jU'23�}U��C���q?G4<O|���e�=�r�,��4���4�����	"����9l�Okx�:{��KnАR���D8«����n�]$��B��z�Eb3�./�\?߷�$(Y��7Y�'����9���g��]�L!*w(�ʕ�H܂ �̄4��̸z�E�f���|�mJ<~����D~������Թ�pfDy���ѓ����3�_�RjS�zs� ���R%���C�g#Q��
č�!m�'��!�h�<�h���9j�MW�pZiL���7�����7�_7�S,�h"���9<tA�"5���Nu�l��i�"^�Ժ���䪨v�U�e�5�Ĥ:S��H�/D1�D��:Si�8�@��_˵0{|���G�:���y�Fu�R��s��ל����%��Xp��VD���`r~�Ms�[���
�a�/x{v\��&�Uu�ʛqD����G�D���4�S2�J�H�2� �T�>�g��9���n� ���rt�e�{W��>�L��B��_�������0��o�B.�/'�x�lҨ���'Zצ�Q�� ���X�LX9����j�#�U�G؜�WP�
�b�T�d�}���~M�#gI7��;�&@�4q��#�i(Mp���[�9�~B	�z"�� ��ur�I763�Q�&x&n~��}0�S�z T�� ΀����Y�ף�͍�B?�H߬fU�Ƶ�<>0�L�\�k�"@���Oaw���e���U�cu��q_��Pt6M�����������Si �&>7�/��(�\����\��F�a� w�!T�Z]��H�:�QV
��k�*��@X�Y�&��>0z�b6���N4x�p4Àu��h�C���!��7�֜��/IS��^u�_���T`�5�o�)���xM}�r�z��/9,ļviI��<��2i Xv��]*Ԗ׺�֑�~j"�jr$E]�<�z67����lk��>�Aw���>NlA7ϭCD���c��ۉ��*�.-�R�n��
X��(�m�;4ǿ�^%���V=�u`W��u��2�� ��V� ~5*x�4v�s���,�PZ5׃��e��(=����2���^@��d�(#��k�vZn��)Kq�"	*��BPCR*YW�V�*!NtM��c��^H?5�s,��1���pn�v]�7���R���\c�N�"�/$/�ON[Da!���<�"�5��1�}Ali�\]��if�U�嵏⺽-�2'�4:�qM�Kj�kbP�g%�]N��z�b�������p���I�lz���C�}M<iw���j
"x7Q�Y�M��
��!��hk�q���u�vsuw1������>���X��8v}�\�#��p�aY�����"�g`q6Ҩ��E k�fD���"\$�?U��V�znzU,Cw���Yx�����ߧx��NO�=���wm*tϭ�JA�	�YZgI�*����0Mⵯ��Z�l��
���!F�S����f�3�M�`
��&!R�4��tA���Dږ��C�x`�[F\�����������<�K>�	%z	���32���W\G[�-s_�@�6�aI���*�d��4/��`�����s)��D�ul����L�}a�0]PèU��R��1~�'<�Cdi���3*/ޑ^���z�}EK����&�M	S=�4�Ȥ�� j��Ҹ`S����~}�a-2�Qt�=8��fG!����35��"��'��Jݾhr�����q�q��1�1��]27`�D����%�����b�Y2Y��x�*�)�<��27������bU j��-4�v�˼��@�F��X�>A��ƘU_��F�8�4&0�8��p�G��5��f� �/�H�Y��v�A�y͏��/�N��N��@�Č]58��� ¸�d�E��'cW����7��͚�[1l�C�`{f%�Y��v�w���%�k�m�ˍ�sT�,���(|��U\�Xd����!s+�ދf���{<��ڪ�ǌ��T�cN}���!kQ�>t�������TG2�'
a��S�H[��H�?��.�~��-oǥ���n�w�	��Ԃ.tH�l���e���>rW�"N]���I (�V���0雈И��o�����#��I>jȞ    ���e�M�Ϙ��46��x��.`��1��~}�Gȏ�0�l��؞i�h̪��J��m�y������E�� |��Wm�.`.M��g$�Sd�A���U����	I��`�4I8����M�h��nF��XҐ
+�Hhj��4W0=�/ ��i�<bYc��%8+����E���$%'m�z�Ƒ�r��!�5	���mJ�AjsW,�Y���ж��%�-B3�tz�EE���h_il �eD+�4mN3�\EHZ�1T�4�iGۮ�4\p��6/������r(��.���$�*Hb���ݶ����>2`�/b��,=0Kh�V9.86Vq����/�Ю-�I������a�-?�>������Cy��%ِ�{��A��5�ց�[��k�^�t��{�a��V���g��s�)�+�^�f� �Y��fCsӞ4���Ǳ!S�1&�*���g��9-�gC}�GJJ�Ϋ�
�2�����H�����TH5���iP�%�8�y�O�jFU];�Y�
i�K0؆��I˭j�ɬ
���͆�?��D
g��7���f+�ى4+�dC��ԔA��,o�n�ޛ@��3
��E����UA���'���}ˁۄx^��^���/`}����]����s)�R�"��� li��M�X^�i�{N]&�m����y�+��m��p�3���{Ý��*v�B��s1Z��g�d� �s�$Eh5V��z�"�6{���[�(N�%
 H�V�	�=�n���cڭ�<�p̗"Ƴe�?]���Cn~�iG�6\a��13A�]f�[kY��**��/��)�a8?�a��3#�(��B���2qR����2�~�{�|R���}9sD���(A��L{���˶*�U���}�E,��O�Dm}��'�$��`��t q���-���WE4_��b�d\s�3������cc1�_E《�s�A�DB2�[O(��7�T�g f1�uaȷ�g,�GD���;>���V�ӟ�Ђ���x��:��xY�.t
xH�8G�l��I
O��@�4�1����T����#7���W0���Z]���+�K�Lȧr����b���M�V��8X��E�9̎8�8�?&M�,?��Y����Ѯ�9_ k��u�l<�e��&��0YgRT��Y�;����U��X*��I[�0�ԈN����Q�m���<��P��Sy_�sq"a��n~$��5�>�#Ί!�8�B��(.w/�7�ʱ�GSn�b���J#�LH.ʯ��3�� �\�*��`
 �-�	k�,�"�m�zm�NK޶"az���e{�ϣHت����ѯid���lo"/���vĸ��V��"H�9�q�h��܀�>׭��s!k7��š���%���w&�f�2�����VQ$����Q}���vD�f��LT{�������q��|��#��^=�BJ��{5���MX��-#�QM�ru��#F6�!,�չ���u��U�)L�]f���/���OwSQ[�cD^$�ќ�1�����P�b�C4lڠ:K���M�>��G�
��.�c�4�V"i]�h���YR�9w0n�;��-v�VMbWELa]!&�Dh�����\�ݻ>[&��9�T�n�ŕ����_M��R��rc��y-]�51i&�����Ӣ8�Y~΄Z%���1��s��R�X4��ɸńօ�"yaӂ\�0"U���e�9��D�f��*�<4��*Ek�	|jD[��lV�^/�O�B{�&}�A�Q=wj��}��?	��|�뷩�wġF*=�c8�yAgdɈQ���u9�C$�;S�����Ȟ�������5S뜇��g�z��u�iM#�����]�jCDu��qq�k=����,+��wq=��,�v��U��c��'W�p��Y��([_~滆k�qLH;:bacU�?�1�n\y�������+�ԕ�ى$s�Mq	]$:!]�ee����9�lTm�MG��{�񰽭F��6S���@-&6���&��gi�3^��w�YON�A�uN���҄�⌑��.4�M�-���\��w�/u-;M�^V��Z����ΝZ�w�}	tj�6�DU�UхNH�Ł&�ʖ:k�]7�͞�ީ{����t������툿���ݔ���^)����R�9�b�����r�raK�Wvt^f\�4L�`�s�/�$���Iֹ�["i���U:�]ȝ��Ao����6����.�_^r`��v�o�yx���T����B:fTf��d�4���h'���1y�E�@�0���������j,P$��z��P������fV�	G�Y���:���,g��ǣG�rs�b�*M���RI:���������,Z��h졠��I��X�B��Q���2f���K����z��
	E�z�6oZ�܇�N����	u��l;L���(��Z� -��X[���Q�z	��j��"8~'6�',M��?~C	� ;vr�_ �g�����9��S�C����8�]�N�����Kį�L>]�!�u\�)��I{D���r���U�gfI;ﺼ�P^���aֹ�����[]��yt��������*�e��U��%���-�lqAkm�CT}	֩- ���c�GHWޯ�T;S|Axv�׷3:7a�<��%�����hx���$l]��n����:	
�Wu.+�w��ƒl]�ǎ��35��z�!:N�.ӑ*z|v�
� �b��ԝ���Q��.
p�"�@�}��$-��g��\"�-�f{c^����o��C�<��]������捋�k��fU�&�����&��m��p��Rl�H�:��~�#�h��g�� Q�>$���pt����ɨ��|ۦ��@2ODl�%�R����򨏘�ļF�ѻԲ#�l�WIK,-�����KG��%6��j��=�}c�jP_8��W����ЀȲWy����:&�r"{�������
��Z���b�I�i@kf�����R%�ef�	傻��GeZ��g�<����	�W�}�*��y�*�d���f���C��R=1�~�c�a�����>T����62�n֦ȁA�F�9�����L�cn���:
z����m��PEC�e�y��݄��6�Y���iT��)`�Ѣi�{�K��ѴY�r	d�6�a<��it��zM��p��Uԟ�!���nV �i:��
�V�7k�����(Ш��bqB�rwLwBV��Ɛ�R�K�o�O����:U�`q�!��V��%�><�K�k��V0)�r{�肪�@��|�8n=�<o� u�Uf�qMb����Wu[.|
�2�D��\4����F�E^��YR�)XES���4{���i�c}ȆM��R��x��v����4\�<E�5�w:�Rb��}�燜oݎ��ēF��6D�ybG�D�����}��h�2�1B!pU*6�"C!�u��d����ە}��2ޫ�y�)���J�/y��U�K}F<O�"���b��+�����,&�R6��&5��پ~aD��
Lh�r��K���AǮgL}�v�&@*}Aj�����Zu#���{N�.J1dٳm.��CMf���pz�1Y��!8 "N��4i�����������V��R7�ןT��;�-�|����-"t�֭�R?�(����/��]�Js�]�*7Z�7҇V_�Н��'_g�7��i1:D"��_��U��{bP�̮<�x����kW�vӷ��5����6g�t�Xy��<�[�n��}�[WvM�Bwr��o��G�����Rw�ܣ���z�9��i���+�;����@��3k	Ό�A�'haB�k�@� ��9۝�P�����Q
M����\3:�5_���U/Uĩ�ü�Y[O�:Gu������JgiQ�艮���<�dt��8�]��'�6��/h��k.R9�״HZ��W������
���u�WTm�MO��c�"�p��L O��0~y�]͆�L&�.Y*�W+gx�Wn�_MmW�������$.���4y��U�p�BQ�H���n�\@��������5���56}oD%��������t:��l    ���A2��������\������-c�s����{��0��l�{ �~���R���	Ma��_���K�a��G�~����uiIԜ�W�M�ߏ�%�A|�:�8�9��[�̸&‣8�n�hl�9�,:�K�Ɋpi=G�;�;��'���{��s]�nѱ)+%�z�_�����1��W�4�ɽv�[���L-E:E�j>��� �zm���T\(�Kk�HC�@(H��*��R�jm��>p�)�(���Ȧ�[н٨��6b>��ͨ�����U*H���w~�U�S.�4;_c󍜸�XLD�%fl!�)��zHZ^�u��p���m-܃�5bh�֖+��?`�s�S�s{��|z�v�ϰ~��>?���.\�/ڦz������pf���4�1� ���(��д�+�h�=�����[/h�݇<��J�s@��v�7��߉4�m�L^��/��o��:���� d��7�[�ސ<~)pM
��B���j���R���d^�o��y�������	�u�K/S��Oc���3%�Lj)�m��S�V�MH��g�O��q�O11G@%�4F)�2a�z��W� ��`s��8ۖ/���2�r�Saj/w����h�ON>R.�����i��q��$��BۨB����,�8��
�=��6��{��������1N�544���[G�3�R��A���#D�A��]�F7 ����i6�:kcYy���O��|��f�Ѿ��כ|puU�d������d��T��Q�\eɵ��ݞ_H��j.Η>)���A���+ℯZ�v�Ǯ��'�GǾM
�̓Ş��<��^`��/��7j�1�^^H�M-�&�W����e��f	���7���g�r�Sؿ4� M)��0ks������?T�f�J�fOhDJ�@Z}Y�P�ؕ<�q� ��/1.���f$p>���P�؞{���Nd������n�e�\�v��0�u��m���7�9�-�� �q�b;bv�m;=�IB* "�bTD ���.?+hY�@��y��f�HgxO�N܉21!�B�����``�c%c\P*x�W_����FI2�Ɏ�)D��f����� ���K�z���	 g�A����ϢsO�}��f+�6�d卭�^/��=���� �s�^�?_v)���3�����hy`� ���Q����s�����z��<C�Y���A�C�����)=�
Y�������W�#�@[
�X�YF^'�^��}��T�+���uЩAEo�U��w�2Q}��S�(S����6�]�����.�� BD�{�lϖ�\R�Lު�sR%	�I���E��ܯ�g�ԌMl�7͙���(XR?[ܵ |6��R���1���N�p2L��_�[�ť��������Ջ����c�W.0ge�k�ү*��6$�}���� ��Ҳ�R2�菩����tr�;E�%�-MK��O3�js��v)��&�����W3:Ad����^9�2i\�-�"+�Z�r@�9` �R���V��Z�Q$��Ky�ށ����H҉�r&��<�h���W�p ����U���N�Yִ�D�����o`|F�nj{A�� ���`2��"�p��f�n� ��}$�h��)vq��[�o��M��E�J-Ѿ��˲%�7�uH��AOɼ���g>�c�Q�[m��A��?`��#(^܇*��+) �o�1�ȗ���c8R���� �PHٸ�CU<T�{$�o�ηA9��;�C�1Hr�'��=S�
��F�&Gą�Pk9R�6������-OC���K�_�hN[v��a��5s9�}ᄌ�af?�>�b=y��m2:������7�g9-,�����֚\�/(�9��:��^�By�XVpp��"{�*r� ?���b>r�q��%;H����F]@}��겫MW$p'���O�Gxω�M��m�jCN��QnTV�ň܍AՋ@-�wM�/�!d��/^2>��Τjʤ�3-�d�KwA���5�)�%<��v}Gf1Ȉ�uź���Z���Z��[��?[�C&o�f�S=\�{|	1a����Ǫ�A�����<lV�ɞ#}�M�.�~�L����D�����;NTg��\�G�4�-�eD�眗��W��'T��!�����us��2Č	Iڲ5@�k�m�i/�d��B�gU�.��^��~��߯�����I�ɐ{*GYzNl4l��΄�s��]�;�M�����޾��G�I^?���O?��ϗϟm^?���Ջ��2�� N�����RD���k�����i�%�j���b�@�>��� �Y�|�9��4;��uh�^�I�!S�+(I�=MU���s׈8��v5V�x��פ#����[�r�Ue�j���`��js�;��R&A�ڪr��$=��3j��( եV�W�W�4��("Hؤ�c��⹙DĐ{�H��SL��6��`A
�}TQL���x���Pd�"�7%��������Wۻ��v'�����z _oD��E�k��ɯ�K��1�T�?AS�1*U�W��:V�������� ������N��DÙ��0
6���s6�.��z����Rd��s�w,�*�`��A�|V�Dh��l���I���W!�y|6r�U}������s�I��ӺJ�	[:	A��ߣXd���ͺ-T�"*�r�`�Y�ɣ��j��AV�Tͼ��E�)��³���� ��y���)q�q��8m�me|�},�/��	�R����H���F�n\�Z4��)�q�Uَ�rd��:B�E�:�xi�X�b�T���_�A�E�0�(����x䬝Fp�aY+�m�STO3���n����hù,d��~�m9�;E���O+l�C�iU<��ڏu��GU�rK������a{����������j�p��:f���s���J���
�4P5&&:nrAS�A;dَM�M��t|�7®��Lf;�f� �&��V����b�����;���V�G)cB�DF��:�H�y�mT~U����~v냉�Q���"�xlf}v�TS��U>�Dʺ1���"�ٜK
���Q>/���Q���u�#d9Ju�K!�-��g<�\�"��O�Oȉ'��\̎��GQ�=��qE�e���s^eHGbDA����2���Ƭ�ƪ�d��2�ׯ����@��Q_�DE�\��(�ы��d^��u3�N��ڙ���ABn���+R�4��n�8ֻXzՊ8+�T��:�c������6�ꛣ��;۲/�v��Rr�Y���$�w�F�W�q��TFMtt������f�8�eP �&�9�x6̾I�zB�+<ݮJ3���ݮ�&N	9��z]~]̖��6X�ɕ�&gg,:2���=;�O��n{�Kݧ������O2�Av�^�7j��㪻}�)���&��4oZ9�#*�Y��z:�S�8�M��9'�^ :'�/�����i��K�O�(CNAl�D��Z�`)>��M�c��q��Q�1�Du�!��{DՉ�M�n)K�¶Zs�G��'R<�{x�]GO�`�'ɩ{B�(�1�z.��!�9БWo��!�9�/��<��q��"����h���P賽[��� �����3�b�q`؃6g��bB��kd2��<%�	
�q��,z�xda#��ky�J�E
�<��2��p�v�#��1�1&����9�&����]t��K�8�>�Ӏ�mK��k���3���a���/	�_ա{�+ݯXEm��O�KO�2�~o7!
��O��\�Z�����p�j�B�U�wu�����*�p��'�r��fߊ�3DH�9w]�UD���Ȩ���*��Ů���:1Z�y�����]���aPZ� ��DRg���!':���s���U%ꎏA\δ�)�g�0:Ji�@lz&��ӧ!,��3�gel�&*:�>7=�����(�Ę�*��U��כ��^���K�9|�Q��9�3Ⰰ	�_ ��g'�/� ���]5��=�<J)��w�O���^(�    �.Y1$}��R]F��0�Z�좻�!����E똟'�8�ǨM;ԡ��K�u�B�jO�����5F�^��*�!�i�'���t���ܧ,��PjW9���5f�_����/�^P��t���^n��g����(�W6������aCqJЋ�@�pRft������6��C=|񽣏��|�3M^�΂��.'SM��P6��^|5�fn8��c�����}�<��r��r�:7�z�� ����H�Ӳf���숕i�ꛐu��~E$�{4}~Dp˘����߃L�ϖ�LW��n)���F�����#�v^j�t�U�0��.~���S��q Cd�[h�{��,��*��\y/x[]j�sKK&�2���7z(NS+D3�n��	쭖F����Kfs���X������ϓ{7�H |����n}�s2��F��}��m���!���'.��0������K�L�tm�d('��7_�y�\Ծ֠��N�����v6ƅP�~�� Dh��`3D��,bNy`�13hR��E���G�k0aR�fV;.����8XJ������8b�.e$`R�\�%��EZ�g��]"�e?U�����Hc9���JDM��v�Zy6�!
x
$��A�������f�#��9�>��2�a��4T�m�q��V�q�NyX��m���V���i>������Jo�q���N�'�=�`�2���.N�sM�cR<�2�Ӹ�o�K��D��Wj�[�$�沐o]*�QD�f�|j�,�!Q̇l4����#��P��R F&*�WT�f:�:CW����}c7�Qab�h�38gh�Υ�]�7��t���(����������A�����a@?�����?�v%A%jv��gc��cU�ʳ�ͥ�6cL�-]�e�]��㵬K�rcg���t�L @+br��$Ɋ��*�P�e��#˞7L��_i�l�\VU��f�V��j�ՍW�\v���9�`#
���l@_ �����k�d����ffe��̈́%���;�ַ)���E�!|�U��?�;�$��8I9LHW�U	�ۆ3�P�(Z��^7�+��_S�S��,Q��3�Z�8Z�~˄E�$m�`[�[7{��L�h.1o����ܢ:�r���@D�Y҂������M�-G��_��3Ǫ3�P	�Λ�VH�u��t�獬<�j�ĺ�s|���������U1BK���+gl3Hx=�قS\e��jQ݌�f_{�Y�!gw��������[Řf�*���[�r�&�P��9*����ᴻ�(�����������Gᴚ3T�wT�O���R�!lV�B�p��.�maJP��՜���Z��y#L�z���;�R��^�=�N�\����E��W?�,��*k�1�
鴿ieN�]�r��h �5y��u�A1l��0*Қ����e��]��W�����~Bko��J3l*S��-Xx]���ZF�������i��8������a�W�f�����3�
�Z[�NYe&-�A\ [�S	�;����L�D�E�l�pm����9����kK�x��E|ekK��ϊ-ȿ=g/�嵮P�*V�6�#B7��z�֕��O�6�z��D�|����.O�Z=�LH4���|��Y/�����;��R���gJ��]<]��G�qK��mH�&�+����_�o�_ž�U���;�Db�!V2qn>X��*.͗T�Xnb�b:�L��֮H|S4�8��v9-��>ĮwS�86� �a�A�`�u�=�U�뺤���h�l�ᒺpҫb�m�Ҙ<�z�%�;�]�^���?lci"�SW$��(�&����5�2�[�/t���|hW+��BY'�0bo2�ŰY�o��Փ23��ƑU����n���l��1Q0�z�~�D1gHPڮHec���\�g�g�����5���()�5�H��t\��a��tw�(j'*�Y��e��d�/�ק���nw��<.Z&��V��HX��Vgo,T
g�.�G�sL�5�+4��K�n�E��v�!ƣ��X�����ny��]\�a�ѧVw�wD4w�3JȀ#���Ř(ğ�ގxfo\Z8y����1{|���T���e���)��z@wQ�^�\��"�yjŴF��#��=S����y�TȵمYH��	ʩ�9�&>'�&<��U��:v�d�\�����9Wܼc�j� 9DN3]Yg�c-S~�,����b�ѹq��٤_�>9b�m�aN�i7�c�oq3����۟��C66W�i(X���y��~���+�WEO�K	�i�ϋ�.��vƭ���<�4�	���������c������Q�8j�Bn��0����K�1���L��Gŕ�\DN�B��Z�>��T<zL��E��8�6~q��gE�t�
��s��Q]z$��1��{YThԘ�:�mn�GE坎���XnR�Ĉ�#RڦJ���}�R��(�_&��B1�])b��n7o�d"(�r�vD��1��Ț��ILj�ѹ�9zg����!�8��71�^��ɂH-�n�i �ũ��A��)�s������\t�T���R�Eq�q^q�@7�c�O�����JE�]�03�D!�p��ѥ�slS���'>گ^���W����g~;bkL-�
5�����>|{�� ����~�X������^I������Y@5�~E.$z�:�|Zd.�n3����չR�S���㎏��d_���ج�C#������tj�@�g�C���K�'2�9W@eG���a�\����@S���İ����8�:��2i��K�
;*��`��9;?eR�NP��w�Lv���f��]���
�$#�¢��ϴ_B�:�q����b��7���:-tvkzx]�5q]����'������
A�w�p>_��2^�k򄲥ث����.I��N7(���Ƕk��nĜT�κv���JD�k����݇�������!�;u-#;%�uYS]`��N�ط���]<˥��M�z2�B/�4^��up�m<�z�&zا�Z�p���kK�(\5C�Y�e��zy!+9z�"�FG�p�m*A��S�jEA�uWm�޳E#Q}U!T.�/5W]_�XiWX�ʅ"�&�t[�����g^$�CA��o;͸N�g��?�4זxl���kr�}��a��Y��riN�\R���d2�����k�b_�ն��N�Wܮ��!��T6^���Z*k�Yu�+�g�B2Y+M�����7��t5M�-�}�	����Zi
W�^2w�?���A��uƥ'R�����M�e�sj�X0�>���j։">�F�Z����S�-��U��D�A�#�vy�G	<pS�.�nb/_n	-;4����������Wr�U�������Y1��m,�:s.Pߒx�('k�k�{�.@EQ@��NT�������&e&7��)�]�pm��]�Z�.f��K<(�bq2٤M$Io�נ[Ł����݌�4�gw�\hȌ}d�&�C,Z��T>^�u!���I���ԽNM����ԇ��:�u\Bb��.��z�(�6��~��|�~�����7�����K���ꅴ3ND_�c�B4�gM�"M��<��,�������?������5&�̛���û C�*��4r	5��K#�B�Ԃ���Z��`B��.(ԵM}M�5�a�>���C��$t�T���@=���f5�K�@�>�">}+�G�M���ZPG�j6\��w=W0��$Pc�e(�Zpj:4:�Ta{Z��^������_c����|�����o|.�Еk*nB���7��?���o�~z�|���߾��L.s����ʒ=>t��bZ����s�x�=��/��|�y���;s��PXxE\AϪ  z��e&��$�0�>��r1:C��L�������[1�A,����])�%���\�:�(��d�t�I�t|��pھ߾���]Yx���-7���,��o�������[vŹ`#�@��q��ɋn��#ÖR�s����՟M�kp�U��xUS*㠽2���x$@����ȿ�0�ᗤ�U�X�,��    q�W�|�ȉʁ g/��뮿�%	�"@��|,d��Y�
���4�'5~��� �A�&�5�>lP:Ǉ�͋0]_ԇ��Y�j`����x���`�uq&RWGm��؋蒊n���'�5Đ�4��|�Pљ��`޼a�)v'B���������0H~�yq<�V�2�p���Q�U��'duN�)5ti�/Åvf�t��Ɨ�)HO�?�`Z�qg���j��@H4��,���6�qASa�y�����7ru����y����r�X6�j��=���$��C�[VLtWvYt�n(&�B2��p/���k+��<��5�L��8��s\dR��/�[Q9�����*Iz��ѣn����(h�l�8۩\���	嚻89��D��a �����m��m(���L
%D�L9I�\��=��X1�@tm6zt��EB���i�q�A�O����h��F��U��B�����%%�\��h�ĩ��Q׋:ݯ�U�8��\O��� �J"[�h��ڧޯ��/���[2�M�S��׾�0.u�:d�����BE˯�,�}��P���6��ץ�xR_�Wn^�E�bR��Vk����I�Us듴�z��s�eK������u�rvq ?:���QP��-w���m�s��(f�[F�)Q��χ��>N(�� �^�{�0��r��'�>��H����U)��l��1�{~��l���K[2�7�4&}o���}���V�@�lz�9m������X�9p����p�[���i]L�}��� N�F)��ɫv�w��/)�򘍊���7��<�d�"/d���__b�A�!�ydc�F�L�r��hu�k��-'�m�C�QM�w�Wc��ߦd�����pw�!=8�0RC\v�d��/Md��VP�h��ѷ���*�oWUo�=�����]5jj2�V\��oٕ��&�lj��]_d�.s��u���d�^qljdCXtm�lO�x����06M3����+��n]�A0�WIݔ�z�Ç{�7Ot�O���,��Nf`�.!��qq�0.���a�S�u�-2��U���s
F/q\�o��\�l^�ۛ�
�Fs�Ģׅ�Kͥ�|�$�J�A���'�;~��U�lS4�`��y��ȃ�hp�Nġ����r��C:r�����=��^}�����`,~o
�hi�>������=�Q�|R�w�L$Q%n%=�k�����}!�ح�@�p6��Ԗ���#��G�J䞜�h��ߌ$��[�:+�Ƞ���@��׎_�Q�oa�[��y�!9�ŷBoii�u�����T���Ki��%���B���~s��_��Ra<�,�kyu�2��>��I�ܸ�b�H\)��A4�ZY+L�U�����M��n��ƥ�tl4u'�<-�� �K"$��;�ރ�V�m3�	K���I\Q��H��H��gh������J�y�P��^�]��.[P��.���aQ�1�VYB����@/]D<G�O+��{�T5S���o��S�rH�����mT��k�io�7+�-vG뉣���ќ��LDa��a�1u���`����G$�cKzϥYBw��D=;>�fKnt���6��\ɽ�����(��a��6�5��_ee�ˬ�.}�zA�-�X��-]�ˑ�-t��؍7ͯ% ��ߕ!QCST�p��{�A �Jq.L�v>a�dR�¸Pך\��e��,-:�k�2;��ִ����L�[����Du���K#o����>��)�^ΰ6K���v�UC[O|tV�(����k�h�H�ṕ��״QDM>�n�0.B?C�W��� �.;d@�	nF�� y�� ����p\��0H+uI��	\17�8/Ǉ����ϳR��X�����V�F��2H�Sq]@��=/
�`b{�(ή�������8��H��ƴU=�ѯ������ }��3j�4��
�yp�O��Q��!"w���3г�k�M�+����mYi���0���ֽ����q9�]z=�.U���ЀOb�D�Z�/==�i��}�aנ5�y���v�%d����ğa0Q�����\*�lM��#�	�$qz=�KY�A�b{�]$"w��>����L�����Va����GC�	l�L)���� �dc��8P�eG(��!}4��w�5\W}��%��E������x��aW�	�����?������Nw��2�����M/NOFs���*A�n�������[�W�*&�ɇ�\>��QO���(<$����yǵJc����A�׭��=j�¥ṷR�Gq�� �A������A����yMs�q
7[:�I��)GE�?y��0hL��QՑ�s"�M�S���ۣ� ����T��x��u��33q/O��븬�<����_w�S��d��=��*����qY��ťD�ӎ��B�������Ǟ#����2���V�Dr3�BL͛�V|�u���?�(����j�������iw��P/�	u��v�G�X�j|.Tlj) $I�6/W5a���u�7���u�P�>�c�f�hA=�@f�i7�w7���Zi�LԤ�:�>2Q�ZT!~k<�YW�o�����+d�D���j`S2@����`j���p�C�2�ز:�$G�J��ʃ襝S���fӣ��py}�0��A""8&�
���{>0�MC������Ld�p�u���x�6�?��1N}Y1��e��c�F��tir�g*0Inr��l�3�92��t^�5m�,�\���܆*�����/���[XR�=I��D�ۖ{��g��7XKs��RϚ�D2�U�W��m��N�㎀����,���O��ӡ~6�7�w3;�G�i�̎e^�4��c��2f7��U��3
a8*z^�O�DP�õ�Y̻���[�5"|��r��9#��|A� XϸMTU�;��l������z$p�%Z;�8�B
 |!)�[��#���D-q����*��X
}G�[媶DwOE	�͈1������ɩ�BHOB=�����ŀ� s��T%�A϶pmW����7֘���Ev��n� �B �.�;<Hc폌�k5�ˌ�n1D�Jp�rt�/&�Zb�](�?LF�.L�5���F�Hy�֨����Y���3����5(U��F^���3E���mPW�|A��%.;ʐ:E�"؟X�E7K�lsƉo�@r�s��0��6��\�(�O�w1����y��r~.^��`�1�i�Ʒw��Aϗ.�q)�?~{{{:��k��kMn��h�$��2-���1�َ�.V�(�d.hb�\`�B�o��s(��_	��Ρ��q*Z��Ȓ�6�e�����Ǻ;\��]����{�ȼ\eֆ��ˣP�q�,�DY���d�qZ�u�ֈ��p�\c���c���\r�>��:�����\Y����'/���܂R�3����`��`�n���^�#,i��Ns�R�6�!:-�J_����������5)�����D<���4%��@YmNT�c[��{��^�k�]&C^���/or�Qt8ѣIy쯗	T�˷�{\�Oq�LZ��6�SiXrN.VP�x���G�'�#<W����~41�p����t_�=GSE_9�A�Ӟ�,s�����xt�S"�5�9���g$}i;��ʵ={p��P�MKty�ڭ�5kMk`�Mx���"j܆�s��a��g.G����Ҕ�,��i�H�a<��
�Ө0d�U�o!,_5v-"��	qM
�[E*#3�7zd����g���4(��{EI@<g��R)b��{�ה
Iөs&�V-�p�Qdy($�W��i+rFR>O<�� �2Sv��A�-E�t\T\Qr�*��2�l�CH>�a\ ��l�&m��7A��#��8ѓ���q�#�Z)����i�U.��,!l�9W�d�]-ނ�xj���|D����^b���&'ig��R� AS�7�A=mL�ۜ"/���r6.1x�A�["����ԒR�<��F]���i�z�\ Y.����]^�i�W��:+��t�bG!�AҶ �����RRi��,(sru[ZH��    �Qگ�]�����1�Y6b���h�P@P�6���ba�F��=�tԗm��b؈!7�y��w��`��1^]$tM��M��mJ�A;���]��j���ėA����9������b�f	�7�e�gi�G�ݪT��e91(��&�sl�skX{UM��Ж^r�s��3Q���>�a�rᗺPmE�^�?*�z<|�%U�����eAA�@Lu|���~q ����j���2Ѫ@�.�]�[��9����tRt�.J�v���ۆP���wB��;�KU��w# 	Q�S�<�B��j���%�*akA��q9�DnP�Ӈ����"Y�R׆��j��_��{��ݼ�?�ʤ���]G`0F����Ħ�3*o�]Gz��%�~T��Ȁ���K��O)�Z��YsSr��ƣ�G!���ɺNw_�v���E�v�E�|�^���?�6��_�-Q��XV��R�0W�g����Ȋk�2�<���ZZ���̯�jr�϶qM�Fo���(�dE�1�3"� &za>�ʆ�.z.uq���>@Š�cu�^�&�M�z=�X��Hs����$�*@+,*b��
W�'p��YE�8�#J�ž+O�>a�H���VU}��u��u*�L��!�4�n)��qf~pq���C�n�	��[������/v7���|���3H��gJsVV��+��L�;*v:I?~���y�$#�6hY�:�?�xA�:�������"V{�]w\Fm�&��3g�rSɤ彦���/i�L*ww`�x+�%)�l0��F��g�яK�B����G��	�[CCi���dp5M��x�4������E5��!�:$_' R�Ģ�5��g+\7�;�2�@{�kj��T���8BZ3U��,��d&c��N�������(�����8�D�J�Q)��w�&N��_~�q\1�Y&�ѧD ��ĉ/E��"��8��N�>Z4��q�D=;HV��o�PL<�W.h�>�#�Bq�o��Y��
��xr�����>T-����⊠�L���8X������)w��*GG#���,����L����H4�n6vy�S[��k��E���~bp���kg�\�#~�F@�e�ώC���HQ�y�i��2A�0Ո��%nfE\]��dS�+�\��Q"��Р��j���x����-a(m#���]׏k(�����i�;&����� �j�utE�u��t������|Qb��F��Du��A�Kc������x9������*�q0�n��fv�j:��h�����pK�v���r�v\4���K ��;J��q�0PHw��gS�(��IO��t{�7���,��H��wHd+U��gu�Qi�b�*GC�{�:����T���R�2�r��Q�����ҡ������2&u���D�<	x�z>��g?�8 ���B��	�G�h��|K�>&�/nIiZ$�Y���~-g��9>�m�of~ő�Œ�#74Q�Y���O1�j�3�j�n�*��E��X=$ Aw~>Z1�ĤGo��>��^����c�������{."�'�
�&��0	�Gƻ��_���qpp�' M�i
Xr��>TMS�%R̖ W�4ܿ,�)JAa2i̶�)�]�E43V�Z����	<}�V�y��1ӔX��Sn��7��|��_�4\���xm_N`�������1�m�u�1�\mVL[���L;$��,����#�C��.��e⧃7���x|�V�`E�	=�K1�t�++����������ٞ6��u{8����7/�^pT(L .�t D-0Ex��Doٛ�[܀��#�sј�6G.��2B0�ϯ~�ӫ�^�c�_����=�/�I ��c(@ e5KW�q6k�e3�����o>C�?�0,�����w�~��af���%aFv_}��a������۫�0���G(�5�x���pNz����p��e�8�������u:�6dd�ǥ��o�n��)����������N����~���I=x����@̤ߝ�G�G`-ͤ��'������yM*�݃쌚I^��>�~>i«ô���I����0�n��${23)ë��a/�`̤?����F�fR��������Z`'-x�qw����N�Oy��C�a�a{'��3vR����h����A�6����[k٤ 4iD�c�q|9\6o/��3vҊ���7�c3��Q�V�}�$`�n҉��7�=��=4�%����o��6���.�h�O��6�?w��qt���dޑ�N
�lw��p�茛t� 7�����/-1i��X=���AO{0��Ȱ>���7�M:3�G��E+�):s��/>����o�ï;���MZa�����봛���i��w��S���Ovᫎ«�����OW�J�x%�����~p/�l+0�C~�<�0����`��/\d����?�'-��e�#&�x;xG���.2hr5��n��m8 �z%�}?)����vӓ�9���{�=�+:�r��urx��Lp����n������~�O�_s�Ɗu�r�I���OQ��I���ۦM]�.U�/jZP+]P�:�bu�ͤ�_js�����򁎜�e�I?2bҏﶿ�6/w��2Ca��f&6|�������c/Ļ�m|���7�b[|��^�ͦm'�h�5���K�r3�>�t~�^�"sgl�袒�`۱/���� �oO�'��!��������"'��GLZ�6ȇ�u�y?�S'� �����0��k�?
YIB2{�>�9�}�3Ah�规c��)���7��klG8s�`��~=�XT����>�F��N�V�_���Z��n����O���_�Ӛ�=b��l#�u;ɖ�z�K-���~m�_O{��Ze�����1�^j�찗V�k�K�V�i/�PO촗Z�'��Rx�촗���Υ�����섲��4�ݱE�g2���r�$���������wǑ�UP[�L(�Ihc��p��K�i/�����4G{�e��p.�������Υp�;�K/�M6VhO�i/��jv�����l����r,�(����ZhO<�e#��i/�Z姽4ҙh���\
�ۓ�#\AO{)���rx֊~=���R��K�~���[��DQ��Wd}Zٯ��΄�r0��_ӹZ��P�{�}�z�ӹހ=�K����ce{�i/e�vʹ�2{���N�7��vR朸f�H+��]�h��_O餲����y����l����ג+�(��])�M�U��p�!L&[�V
e+G������R��i'e7�S���;�i:��Ѵ�2��%��:`�cT�k�y���F��6�0'��aNx�t���y�N�nlG��{$�5aó�˿n�)��K(�u?�Ze��A�dM�_�ӯ%~��k5�D�����k���6�E�r����+8>�������_�����^*�����_��u�ZA�����xv㯧�*���Rt-�����*���Rd �_�VJ=m����ï;:��즭���馭l�����Rd��_ӱ�n��V��ݴ�"c?�z�K������.ɴ��P������'+�J?m���ө����R���Vz��{:�B�駭����R*z�J��ɾJM;)�T=X�ϧ�����|㯧����l�3��Rx��i/;�i����)�c��l7���U���L�â<�e��l{�����΄\�fN�� �L�)r��_����):Ê�/Ï�i/�L�(����p��@��w�������a��O�����a����{	�=�E�5�__�^�'u���}���)Hc�L����>�B�o����),c������B�Tt��{����Rن�"�5ͣ�VE��t��^�����𳧸�Ae�����"pb�u�v�+�����)�/�)�b8�����nj�n�i7�t*���+FO�)���_ӽ+n�X�/3���M�S�v�*����K'N�V�cG�#�L2�",z����"�n�5=o��	@�D;#Hҟ�~�������������$��zR6��B;D(R+�:�O���5�H�_�Я�k��R-0�t�H���?w���ߓo    ,�=y�Rm$8��1���	P2RM��,>`����^::�B������J�7+���ӡ�T�����p��ޤ�,�X���Y��#AKF�1>���E'pI5�����_�(<臇���~�h��ׇǯtRd����%\J�8��0F�*2���tJHr�>�z� �%|J�;��ްB]#|�IU� *Y��sr��?���"�J�8��8���`�e�5a[�K��r�k���X%�)]�G��-��_��#����dթ��Or�.�y[·�#�"�����no������|�Ň�?';.$y!'�����n������Y�@Y�Q!�I5�0(Y��sz�
�]nD��WW�O�DZ�=�؞ދG�qP�h3�yHI�q�s���d��C�q�������</i?�qP?�n�~��'6�Unw7��71+7��v��MJ@6��^�Q�j��e�F��Q]�(��k�GM�1�ِ2��<����v/<VfR�q)�IjN��@h���LU����C#��4����|b��*n|u�B}* 8�I��U۶�H�X��D6�N
D�]�E2�qf�,�]�K�Q��t��-��ǫ�I�����B�:��\�YV��#��um<P��N����t<Y7iUf���.㐅q�0`Y&���z7��������/����M�jm �ٵ�@�<;�/g���v	G��B+3�����q��u�2X��6�_F!K�'e�z�=�%��2X��h׽^��|]���L^�w9)��������᰽� �b;�,Nb`��}<X�Hg0ϭOL64R��֫e���He�Y�xI��]H�_t����n�H�Q>>�������k�e}��kG��*�Ip�N�U�zfjҡ����wC�fjDgT8�K�N�M84^�gN84S(�V���9�P��M�
��6�ƀ�iٜ��#��������7�u�]=� ��n9��D�2th��]FVE&	XV�.��k�<�O���|�~�A����E�����	U6a����ܯ�MU1DTiMU��tKB�p�� k���BB� Tz5�{�%��Ҋ=ڎPj��ni�u�(S5�V�rU���Q�	�S=kо�(��,�B±�7�M)���
Dx�p�,�"��C&�w;���W����h�_`�B�{=���O��t�`.������ǂ׌{Q=U�zss<�2\�cb�`�n���0�7�_y�y{�}�����N"BQ��/n�r��A�jV�ݞ>���nE����JΉ���\�DZ�U˲^�8�;**�(~U{�}f�(�B�T"��i�p�&J;�0-��x��3ѯ��V�KV�hʄj�+I�������T���A��rq_n�AGmkD�۞mO�l~���y3�q�vGGbp���O��,L��(��x:/w�����0(���Fe:?|~��H7�6T���/�oލ����m?>����1SZ�kY����I��3��w���Yt� O@f<��8`�N���6��6�ԇ�οG\�a���d�F'u�o���Ջ�'��;0〟N�ԦM�p��FG8xD����i�����6�5a���7����ǃ��8b҂oEvq������-5i�����ͫ����Ib�`���nҌ��N[Y��0hR���?���A�^a4�Ƿ��w[�!GM:�r�~�[7iɏ�_����x�ͳ���̸������n\��}3���&��oZ�l^N���x;%/��m��z�M$�����<��~��.�0�?6/�R�u���k��w�޼��޴OO�ߤvҜg������x��?�O�'�[2��G��0�������t��sl �Q���qx�F�8ḭ��Xs6i���p�|4���Tx^:��dX�Oø#`G}?/����:��T�Й�Ӊx����mٯ�C?i���������r�|�l���5+ �wP�g;K��&���z݊�&�,�e����Ȓ7�8�Tk{{ڃ���">�J��u8<Ȱ�aĤToOS�����2&�_$�F7���èI���>퇝_2m3��w�����c#���w{�U�6�n|�����O�Xxq��w#&.D���x}��m�}�w�5��������!��6������ז�^T�d�2�y����ۖ/�[�g�������0��k�#ܞn�}�M��Š�e�����i!����t�@d�����p��w����x�^Pm;��77����7�!�&Ey=���^�&5yu��A��TX��a����EM����I�bk�	uW	6��~{�˲h�A�p���L�8��#Cw؝�׿@c�ex��}6i�0=i����+�>>HM�n��>�o�|�aa�V�pp������Өr ���I�B�k`�&=����v^:�q��~��~As��v�N�pФ;o��_�����Io�>���}� s����8�t�8�]ޘȽk&u��nۍJ�c��I�O�ns�Q�zj!w�k]F5�-a�ߟ���&j_?�q�������qO` �%T��0�$u��}�����\C	�������^�����9��F'��p�
5��ߑ0؍�n��O^n~��d^?������|.�f����3t����O���=����]2��d��d�L#|ތ@�l��oN?ogm��NҪnג�?ޢ{r}���햀�	�?�[�{��f�z�@�6g�b����G��Ͻ�(YJ�0�膏ý{�~]g��̓�"������+���0�	^�|;�$���ɨ�ם�K�0�#��Ƞ��Bꫥ>IhI�a��)��FV��m�Xg6�a.�L�����/]�Iu~8��>��	m$��O����=p���z
�"�-���m������<�o���ᝎ�-A����n>on�'�#���;���!�y\���>V�����S������L��6�.G��.<�ߟ�Ì�?O��I������R�� �Wc��!=�Z��"�9 ���#��4�K	-o��VK��0�����c&���)IF~PDnE#�s /�!+a8�#u�?!W�P�'�5JXr����j���6h����ϲ�_��b�Ʒ72�_�|���;�a�щ��j��L4�ͦ���"L����z�l�"X9
�[��'�8~	^���w���)�� ���m:Yu�V��z����p�P��&c��O���OH�â����j>���vSTÓ����n{��(|+����8*P����_�׻j�K�m�xڅK�����?a��"�����~Đ�֧fOh.�ͯ����$Q�4�t���@��o��{�S�n:~��}#���Ӏo��T<��eR*<ҏ��l�<|؞��	���?�o��`����!�y8��ߛt���f{�Z����n6�cRP��ou6onő���)��8��q��`���x�ԗ"\���������Z�#u�����W���a5�X����$�ܟƗ�8�RD��؈����Q��(����o� �I�����i=⍻�ý�S	p~s<����@b@��;���'#x1܋��vy����pLL��C�t�d_��2]6o�I���/�T���wãN��|!���p+��2��c��n@p�D5�c}�����҆�������s�F� ��}�O�K��K�rŸ�r����M2��w'��x��䆁�e�.0�����'+�UP'�����{�ƸEc�_�0%@�������Ur�k�;��a��9?R8���cZ��J��]@�����~z�'e��O���D��#�6�)O{����tP��U�~��ۓ� �L��<�E����iw�,��!�Y9�󇟄E^��v�p��j��g���������߈�I r��6O6�7F�񋱞�G�D���ڞ���c��|s��/�ɪm�C�d-RZE`���vP�����i�s��XN@���!'��#�	>~�7��a��/W��ݿ���@�)BU�~���͙�ɟov�b����ƚ���~��"ĞA~u<�{�y+}^��j��ﮏR=#    ��x��"��Ç�]� "����E ��O�#\D(B�G[x�	��ꦭ(�2�t$�[+MHr�p�������naK��e�,n�p��8>O�	-����i/|�i���?�����}^�C�)�۠U�ۏ4�x��I��-�c�ܞkB���7�1w+�OB�_�E�@����x�M��;�>��o����ru�W����:�pc���A}�m��#�x�Ϟ 'W��~;l���4�K��v���*Mp���o�pE���@0M �O��Û�����q�ӟz�zؚ��Z�e�&�xaʾ���kDD��p+�}�S^�#S܎:
U�����I
i'���y,ó}��Z�&A(k�"��!F�����=٤(��g�;_}^{���Nѧ�����i���Z��������_�B�K�9�|�����Oa��a)ה�p����,p �M�	^��������==��*�h�)r�o��KHQ�H#?�q|WÌ5!��'ҬM���q���q'��4��O��Ð�i��a���M��] K!	|�;�,�)����7o�Kow�dT��D��4=��@8��*@�p�]��*
]=��h��h�MA��9�_ݼ��w� �)�y ���g�UOP�H���)Dy:�C�ys=<٥����+s���^����=H�Tǫ<��~��nL��3�����<a	�����&z$O��x���e<�&������~"ą�x1q�&4z����Nh�����C�!���AMuܟ��S�t��~�=}ڊ
#r��	��}���m���Bw�����:�����o.1�	����ys<��ZoN�p���An�bq^��pTC_��^�ð������s�?�Č;�YTbZM���p���<�;96�	q�b ��&�x�ێ�O�*�gfGP����	���;���Y��~��	}��*'GM�f�촛fI$�z8m�������]$�æ�S&��6�2�1�;_�'Y��a��4?�7�vc@��q����6M�<��n�]� ��!@:��@W��<V�:L��;�c����7϶7��/n�<�7۽���0,B��۝n�{��=I@�I��J��Y���+6v6_φ�鷿�i������g��=�o�;��1C��ۧW������0�;�O0�O�3˛��>��>���1�Ah���o��'�{o�U�L@��'�ΪP�4��?|�t ��>�;�����S��p����t�����k������W[i��!�jĻ�U�ƴ+�N8�̋�w0\~��g�yat�p��8��I��ȯ�w��C�h��L1ot~;���F��U��_��B��>���p�K�����d1�mN~�ps�ys�>b���W�&���D�M�Y��,˓)��������,zC@��|��Ζ�;�o����	O�����,�+�H"1�kcF����=]�cF���ͱ�߅����Z��d#��hfp�f�		�2�G�4�?�����7a쮱�jTW5�W��|�'����d��=r`�sQ&&oo�����u����v*�'�ԇ�����^���/�)��6OF92�����=0��|5%Q�-A�?l7_�a��)��l�/?c��T�4W6݉K|�i�B	��@�� ��Gȍ
�����i���� ��ɭ�>Gt�w����ލpǴ����p/��M����"����e9r[��[p�V�d*� 	C]����"2��P.Jb�ݩ��G�btޡ߰�����"��WU�U^?I'@`�ú���a,I��[῝��Ͳކ�emO����翩��B6Ҭ;>�M����lÂ ɼ�[��������Þ��*.B�����;�w�ЭV݂}t����L�n�\=6C�L��Y�D��'�.J����O��@�A�k���'��RV�t��E��5 ���  �)�ʊ�Tyqu�y�Ѧ���Cw�f�:���E�@?�YQ��LP�#8��v�ک���P�*�'OMJ�ѠF��VIV� ���
���n�.<T`֟ɝJ`}�<���6�!T�wc�kI
w�7z�W���H�U�$�|:-��S�g,���zh��%U1:..�G,�t�\�+`苐������S4RUM���B�U ��[ԇ�Y��EǏ)P�O=U.�������NT�\K�0$���6�Π��Q!�c)O���u�s�f�L��JU��\�<�R��\x3��$_�t	��]��M��.��7zsZ@m��wZG��.�*����7���M؝V�C�8���@]C8a��|
3.$KqR�GxBق
$�th��&E�x�n��r�z^�]�Hq�RN)�$=�r��� K�"QI֌y�>��u�����%�>e�+����q��&��PĝՈ��l�Y��\V�̿4���[�9A+@���YLN� u8���+vX�K �^+�^j�ı�Y\7�K��{���J�n��,$��B:ER�ț��[�Odu� 3���~�z?N���6��WcƋE{�7�3R5�	�g^G%Hw^Y��`Ƽ+`��se��ƾ!e���țMR��k����������)����o�P�O91��/����1����N���X�4�,v�f�|r�U��̟��0�k]��_6��$x9�����
Y�ɳ�ʎ!4��zͅ�W@�c����C�j�N�k�kr!�w�1��T!�<���Hv�}�r�`'�����M�P�J�����8HR��]�Z�+��͐�_=π܄�J�|6���9+�Zdya3+��X��}�B-��D������NLqT�о�v�b�ʞu��+7�/�&��*�=�>�s`� �-}mB�'HM.AO-��j�^�6s��76���"��@�Gܩ�p�*��� �Ṅ�k�i����n�ep��-��z k�_GfK~q&ߎ�L�oSj��ʏ}ؚ�k@[i�j#y�Ct?�~R��u�2@�m��
J�ZRi0dÌ��.����s��"�_A[q@�$��5�.�G�u��>\��� �~ѷWKR�T��V�Oy)�{�~�^���k�sv�c�^���қk�(݀�>�����I<���t����%�֌̛x>�t`�$!����@a�໡Y�R�X�:�N�@g�dk�w���c@cS��T�۬�O�xlX��d� �},%�{���w6ߑ�}�tC��*��۲�,��@`�9:a���?�)���<#���hw��b3o�Ϥ�n�]�Д�	���ۥ��'cMi�v�pk�I6-ޫр��/�K�c@]�N2M�����.i�x�� ���d �,�/�#n�_�o��j;��m��,������n��?Ѐ�{��=u�H=�6ft��ʺ�����M��1R�l�b�1��6��\���z�,���f�h�U�xX�i�Y�_f��/�5��T@ֳ�j���0�X�6�1�6����v[R� �@~�e=�Vap��d���M�hb�[���.��[=�].y�����g}v�"���$�!҈\���71|�&eɱ[�����Wz��1��&	/��m7������C"u;�>[rn���q%VzE���b�~&a� �=~��|�N8�6u��G~q���>�7I!F\h��hb�_�ْd8!~lga�NxR�(|�>�=Rz�Z�\{8��f�%�(pX�䄲�I6Xl�W�H�9�8�q�@�Xf��Pl��� �4lَ��Ϳ��!]̦�6��9aZ`ثԃ3��]�7����ƾi���zX�(¿���=7MZ��a7�b�@!ٚ��.B��~ɺ��|���es�cFi��-dW�we	ξ��B���yَ�����rH�'�� �r<��0�U�����+��z��~��"�>6d$�/�"�N8�b��x1���ۄ}�-$
�[&ft�?Z5!�	i{����f�p;oIZkAk��ٺ���)!�W	��V|dێ�kAd��4g���񿲂���j��q�5	��MÝ�X�=��]�mí`�ѳ����Cj����5������uXvX�F+��X�nȅ�J�u�H��    V����g�tܯ�b�DÄ�JOR��i[J-�%[��
u]��G�!S_Yqx}x��hol��a�'Лm�R
��Y�%�vN����C�%�ȃ�����פh̟����8lد��Җ3ole|ȋ-���A�?��dr�z�~cO�����Âm�Ӛ�q�N[�ʮo����n5#	�jM��=4�7�E'�̎���-�*5Sdjg4'������o���`��Y#�WhT5�]���$k.`h�T����Ó��[���|�1�ߛ��E��/.��$���ݤ�[�KZ8��	�N|裛UEcYj���	־�8|��U�2I������4�̂��k I�sH��4�������R�m3k�O�kvb{=�����R]��aW˜s?O� ���W�pצZY)�><`��ݭB���� �x���ʹ����N(GhA`q4ī�_?\w?Ľ����*��r� ��y�W���d�1t�~����y6�+_>���(ބ}�~�m�}�9�7˻y�ޕf�IF �$��0/JR�䎕b���Z���c����yǼ:��o9`m�:�&�?.�R�\�BJ�P{ �gO���Y�$E�DQK��e~��G갬�7	Kn�	�"�$�X�7˸d�?Q��jyנ���s]���r�l%��"&XI��D%�z~I��f���9O}�/2d��˱!>�۳�� K��T��J9��ť:W�@��-Y�&R�Bo����¿3��I`�dr?��y'/1I朞�V�)���.�F��<����Mۘ1*�R5q܈$|��m?��-5Hq��6M˖L��q�0�s�ʸD �+]����>�]�qkP����trx�i�n:�oߤ��Z����p~�����?6�]��W��,�]����n9�es�s�C�;��K�r$?8oP�� '�1+ovp�Ǿ�������j���r�?{f�2��W�];̻�ӳ�<=�<H���c�o�~X7�5[)���0;��.w����g�ף;�R�렔2.�U#�+闘�ڄ���?�:�*��E�%ꗿ�Խ�c�^��b�Q=?�K� ��?�~��I�F?�C���5���xO�N��I���q��O���@�E�9q�`銾z�mU��c/��m;W�{4ԥ嚣K���RC�F�r�l5�x���}.����lm�Q4#�<vh����o��o�=�<6lCM���ڣ[��%���=p�	}Ƶ/�=�[�?�ڱ����\���3k�~��˾=9�+kW��?Zm����O�8`�Z��85�����F�+���a�µ�ho��	��_�^��˪AyuE�+�O�O@\]��S5�m8��ڻ\[ҍ��HL�Wh_s߻�v%�>�o�r����g��7:�r��L�����Zc�$�=�W��+��<ੑs��^�����}ﮐ�%�_rS�r����������=`dLVε��_��G��E��1������_gN�}���I�@KK�80�2��\��{�ؿUN�{h��:�>�o���<�t9���N��خt�z�@�ʊn�����
���H{Ձ��;?����"�>��|{r�+e7ʎ�.ߞ|��[�=�<%ړ�z@�bw��J�ʵ�hώ`)U�����׋j�_r�iR���+�_v}�F��^_��̖���T8l�ڛ��6�}���[�����e�1�Iۿ���!�WxRNf�r JU�~/ JU��W@JU���JU��o�JUE���RU��	�R���[�B&m��B{v�^�B`����cۣC��=�7���[iϾ�o���;�d�/��K�vX����v������D�O�/)2��d�������|����ȗ,9?_Ҭ��d�7F���G�/�|�	_r��	�T��'�������-�=:���������Pсk_��F���s���h���%���G�>f�,�'�>$\_��׷��l��ȷ��L1�}N\O��-�����=�7�jr�ѿ���Ύ㇛�}{�o��W�=�_2ńo�����hǇ��~|��;O�흴'���T���W�����?�(��~U����S��P��k�Qr�k�Oѿ%go�?���篅�����t�~3r
�ޔ����/�|~eJv��2%;>���3}{��Z����F����8t'���=�guͶ���bׯRN����ʄ�\{��������/�~��Jr����/�_UQ��o/�/g?����㷾��I��*;?�_��;���1?k��ѷ�����}�R�C����������Ilo�׉�a_)�o���R�?�o�J���U�8���þҬ}"�Js签}%���<�m����<b?s�S|M��_��ϖ_�ߗ��'µ��;_	�"�s|{��cǿ����e��4�����_���M�������UE�����?���o�������,;?ײ�e���r<ķG�֬�\K���;�f�+��|[U~U�������x�o/���ǝ�/kϻ�o����5l�:qYe��+�s翾=���c������S�_����Y�_���J�s� �2�\�
�+�,7����+��\��9��ۣ9>�\�G���*�������+���`~��/�x(���~QH���� �2,�*���&���h�{,
�~9~�
�+cI�� �2�;/V�����W��K�J����� �2��o%��$O.��t�9���8?*����H��/�G�xVr>Ȏg�*��J��X�����͵��{��,�W�y�o�a��c!�Wi��W6'yE~e��Nړ�~e5w~�
�+K��WV���S�_ْ���,�_(��l���W�d��+�I�Y�_Y�w�W֐����r�VT��T!�Jq���|���#�J9r�T����$�������r~��Ӫ��d�7�2'�s#��5���_�ϋ�F�c9�U�_����o/�_v~�J���h��W�rUN��r�y\a�?�ܿ�W�&������K�'�I~U���U�#���6\<�*��꒝?��j���~U[������ ��sv��_դ��*��j�G�W5�������l}{�4ko�_�p�µ�G��ur>��cN���~er��Wƒ<� �2�}?�W���_���*���ܚ�?
�"�O}{�#����L�O~�9>���I>���tI��K|.yޡr�$ϣU.�_�޷�������4;�T>�����d|�o�ױ���r�z���=��o��W2���k�={}+���W�_9�P�|��[���+H�~�����\Y���ʑ�n�})�[_���I>����^+%�K_�_������:ѿ%��ۣYޥ��/;����!�C~��[���%�O��O���I�S�_9M�/��cϗU9��d��2?�� ���e��R�_� �r���R�g.�ӷ��ױ�#�U���������_��F�v�������H�_U�<�W��?��T�_U9;?Tc��<&��Ml_'�G�Z�_)�H~����!�'��_�� �=�W}#�W����V�e�ߋ������+î����>�|_��5�_��#�Wd|�o/�d������q�d�s�Rt�1>�|����������J����5�_��k%�W5y������߉�U�ګ���_�W��/P�W�"����#3������x%�������r>t�?b�k�W��g�ۋ}E��+�+G����ط��Y�4��~&������x%���_�¯j����W���T��9���W.��p�o��~��2�E�_92Y�o/�_�u.�Gt{�_����~D�b��5��%���8�g�Q5���I�1]��/�?��:Ҿ��W֑��.ľ"�#~e�ϣ���#����:.�^i5�op�V�A�4��K��������o��V��������ʿO���_ٚ�_�?��|,��|�$o��W� �G~e+�>�Z��}��W�L~�ۣk�yп�</����cj�2�x�+�<]��������'Z������}%�Ͼ��a������Z���|Z���x"~�Y~���I�+2��o/�/������f��g�N��%]��_�    ��
?W�H��ϕ&R���ۦ������4h��av*B��?6\�r�Dq�y���������f�ٗ��f�q�$O�Zu3�����$E�Nc�WN���E^��N*&�.��Dy�~~�#w\yd��>,o�&���g�8JP��S����勆yU�}I�86P1��NKɥM�
�߶Eܲ���o��b�z�(���s����_C�_��k��eRK�z9ʰ��MvڵT
/�_>{�˓Z!�3�'Ͷ����T?m���0�����
l�#G����M�d�U�a��%�<|�liޑ������L��4��`K�}�~��e�:D�E@ .���ɸ�=e������%�R�g���+;3��?���������=�����u7k��*_�m���c�=��/k�_��7\�[��?��3թ��"U�B�����f�/WM8��φZ�D�~6�&?2;����,�V����:t۟(��Ow�O'V�$��B���?��,��k1�l�a���8�KҎ+Z�7bJ��X����5�B�_��k���*A]KvWV����̳T���?e��\ǐ���� ��{�B�ȍ_)�����1��<��if��7T�=/BI�n��>��-)����fo;o}x�3�&?y����}.���ȱL�+2,�Ӽ��|;\�FU�Ӟ�C�< S�?o��L�ű�e��n�O����[n�+�*�t�O�M��kP���{�}JU:��#d[���a�l1�.Z��*���8��K)ѷ|{�~A���W��n�Pu�/R$)��oQ��!"%�^\T��n$w=��r�rWW�[�����a(_>��7���g�]:�kP��Y���}�Б�8���S.*��s��\K)Ew��ŧ��0���V��B)�����[�=��S~'�ow���!;�Άvŗ�U���q�~Y�S3���&��&��
QV�Dإ���Ȥb��PNho�׉�%�ވ�3��$�RJ���ۏ.�l{Ia������"Ɇl�f1b��-���^r5x�0߸3�R|&����j���ulN�R
z�9�}{�l��^iǚq�	^��mo�oͺ%Q��~X%ЬP����4�_������i�|�hUֻ3��_�X�@��r�/۬(�)Tvu�)G�3sX��t�����z���yT?+�~G�k|m�!��hw���g���ޯ�����J`[�EU��;(��·q�2.��y�֒��r�6.����������~%/��y����C��Z=�_���RQ�Ш��I�������1*? o�5�	7t��A�˛�����)��b������>+ً��8o}g޵�������'SͰ�w�?W��y��g��Q��!e�scr��~����r]��+�͏��ïrqa!��*���_�)o��XhM�T��r��c��o��n,R��h6�R�yհo7v�������_�br"6t�
l��ܪ�b̼ʶ�bՐ��i�L�����8q�6��ټh��\��`�7X�u�t�.�/����l.��دY꯲��*pM]��)+�\�q�
(ӷg�G�q������~��78#G�?M��[�r*p������	�u^�C��|��!s�=��9U\�oI�C�Z��2/�C�� 'ϛ����4	�
���6;�}��Un�`p 3o�e̈���*l���"�"�`�ެ�F�;VZ\JR�X*���E�@�_ ̓/���Y����o>��8r>|�={��͛O��#�5�����/�g��_��T�ar����
�m��e Wd`���G3��`�[w�M�ۆ�S�Y��;r�J"BMB�
�R�P�J��T���"!U#�<#���R��mb�:�=
�����Gv�6HF��@A��8����(et4��K�+���.���Rd�E$i�=��͎+�<���,K2��tk�[qcg��0A�}�-�z=��i�2=�K��Ǌt{������
�^"���1#���Ǯ�;��y���*F(&!������fM.^�X{�y3�$�`����|�+�����g0��Go������|�hI��7=��ě�\W1ʙ0o�-�4K�W�c��K�zl�y��8��g�MrH��rٵ�wD�C�f#�[@�1>�47�Ϻ�O�+��u��$�L�[%��d%V�ͶG2	G�I��L �K��d ���  �l?\v�rɹ���(x����<��� 1���cv�>��I���lnح���~�Ȁe��~��FvA!C���6��G%��k�|F�2���c�e��&� b8�"��^��y��f�s.���f֯�䳩1�m��ge��{\n��?��f9#G;��a���g�O�~�F��tƺ5�Om���9|��gU~X��y������O�ա&�`ȟoN�d��sQj�!�7րP�ȉbUjZ<H�˨ušb_����j26� X���E~���I#���	�LH����E���^�Z��ւS�����䰐�[ޢ4�E��@l&�)�>��m����/�up2��rj���O3R[�Pݝ����@�/��kr�e�0�6���O7Rd���llS�/{Ͽ(~OaJ1N�����4_l��M��[=�Cv,�ڀ�$��K��5�oJ2=��r+����r+|{�ؾNl�r+lz!#�*5�n�HX~7�d�mv���%=��g������v} 15������������Y��a�� �������Q���0�^r\��%��Č%xF�5Y"g�xζ�g^A�W8�ۓ�Tr\�t{A��� �KM�^|��n��E��QkI�#5Zj2g����n$�%��Ȁ�6���%�V�>?j<�ئ�jt{�_2��c ��	3 �;�n C;��t{9�:a�K��9���5���%}�߄y{r9Јw�5��_��$� n|����� �d�.��;��-�е���u7o�{�\�m���,�E3O8���4����jE��X��̎l$��N��=|�-)�-hA'���g!�ȼ�̛�p�vpι�X����fq���>y��-�(N9th�&SϠ,�U���IR#�:��GF�C��Oh~�c�0'��� ��hA5�_b�_T��(��e1��x��I.i|Ƭ8�dd��)"�<� �Hf���^��F���z��t����<��}�K�-��ۀf.��J`xO��hO�fϲR[��;,X�E߭�e�ca�ͻG�S�c8��ƭ�V�-;���Ŕ�Wz�����c������[.��I�o��y始���[r1�/g�I���=��@�^��ega��p�I2���RSY	/Wd�N�����=q���]����>�Rz&�|����$g��:ȞEt �n˝a�ʳ�Eu*��ް��xXw�y{���re���y���ǡ[�yk��V�_�n�-8�-����rCX �Q�� C ���/��Z��09�m�N߽���݇�������,���H�����ӧ7_�����z.���c-��Og�}�N����çw�-U���z�[qxy���g@.�c ��X�U�ɸ�jXbV����ub{���~��un��� �����`s�N�8u��d%Ƚ��W�Y+��J�
.5�OC�%�JIk������^|I+�����v��%ki�N��+�l��N��u�\wl��0lV�y�D5%NfQuN�[@�R����ls��� G��ϻ�e��-ɳk%'֓7�A\Y[�s����,�� ��u����.O�$��f_T�Kv��#O�, ,|�ƛڊ�#ɞ�7͐���� 2S����d�����5�$[�/��k���8v?~��[��Z�����q�蜬[e�pc��M��PE.��QzѮ�~�imY�ĥ�_��U�V	ψ��a����7��`������ϥ�ٱ�����ܱ�n���J�����yΒ�`
���S�FQ�L��>k0��Y�-�Y����'�`@�ȭ���F�)yJU��H̻�l
�z�֓�����>��}��.�u.��1t=�u�8�ԕU&�±�o�|���*�c���    �YË5D�iM�v1�y�6�0��%\k ��n�PfҹF9rkS���Ua���u�����ˇ�*C�f@�����bHs�ƌfTMs���aN����r
��7��q��ݻ��)'m��8D��1��NL|���YMY�?�؛��C3�o0�e�9�\��H�*'�Kka�so\e*��d��yXN���K7��8��⤻~�O�\ij�e��l�n�%��\������!�up��ӬS��&�0����b:�7CX�j��d�w�)v ����6˛�ݑ���Y^w`�~Pd�`=��_?��d��Z|h�yR9�"W��"�	������D$�-KM���;�)�O����_U�r�����v�l�ثj������z�A��q_�F�ڰ^)g����b5;gZ'�r]V�;��٠4�[�$����NnŮ���g���s�{�,���T���]��M*������%���1�o/�b��ո3{�7�C�� cz��쭴{+뽕NN���[?��n�ٹ�c+@��@N@R!A�zf���O6emv�.��:��f�ZV'�*��7߷�,��rǤ�ni�,�`�;<\��W�Y*ߒ�x��<c���V�io�`P�U�k+�̢�W��8��5&���h�]۩�V�SB�o�˲S3�1�����]h��?�����~�5��+E4+�6=�м�b�͘ ���뢉�u�Z����P8�\����.��I]=NH��L5Y��{~�h�y3�_q��&0��e+����(vo�|�?>z{IW�v>ެ{�VC��;������a�Mj���.�\U��z�>���#Ċ��g��I�Y�n7T^�ޝ5����6��~
�YA��u����,�z����&�c9���h�u�nL$jduΖ����/�~Q�b�fv�mn×����͗���0�	)~�1��gd#�*Y���wG;�6��p�|.�mǃkWBl��c2�qk�X��K��0��9�dz2
�a���?��>,���u��Aw��I;�������'�����M�?K�6�d���Ŧ�n��Xr��b��,G�@��7��}�b�������I*�
���R:����J�gJ}N�b�V��y�ْ{l�l��e\ٰe�X��"�dy\����H[����8���m�w��|I{�dp��k���Cw��c���������N���N��I&p�ȼ�N(<������6��e��<�L�=S&��-��[�5Bǰ :�������A���%Gls�����B�S�ql���))��i��!��O�
~��nI����`HU�T�<%J�B�v(n������u�����'��r�y�vgv��_6�����|�8tE�p�r�k��5	�����'��;����2�!��������s�l�>B�Y?�l<���˛߳��/�uuKU��h�ӿ��߼�뜘�>8;��kv������Wo�.IN�w���ER���d5����&���#X[�g�H�D�ڔ3�ɚ3�� ��!Q�3S{�����78��d]\'�g�ȕƑ�.�f=q���o�Q�!��;��/���1���YŌ���$��oz��M���V�۳�ظ��#��9rg
,\�;Y``oQ���t�Ɂu�q`��f��`������摓 �72.���nq����!͜�ɉH��Hwk'.��us����H9H\���0 ��xY�~*`����{���fHـԒ����q�pz�5C�Zu.ו�=����2+�,\�5Wæ��ϻǞ�����]�$� :0�;F
`��F0���aG��,t\��=�n&TI� �=�q %o�u^�q���cw��u����H���y����I���jش|E5���rt�g�z:����L`]ƹ�� w*�fJ/�/�����K!eTz�{)�,&�����ϋ��;O<�\��OY��,L�*��i��W ��L�*���ȉz�����vy�m��5$yM�?𫉗�/���W��\8_Ы��-y%��W:H�(�fX�\�B�*��=ָ?�R�^t�ۡ��#���_���(�Pˋw�U��i���!�A(�=�`�< ��_ ex!bn�J:�z�������c�*�r����UM��Z|d��M�	_��kq��ϱ!NZ3%-��OKR����񁃈��jpt"��G�}��l�oFO���/�Χ���&���$��|��9s�/^7��iJ����ZV�1̔��uC�(�H�ct���=t��Ɖ����?��x��[o��Ŀ2VU�{���/�u�
E����.�F�O������}��i���n�}$1�<�n ��2M*$��,� �M�}D�>�-��D����$�;m��-7)GZU�ݷ���~�3����JO^>�+C�ev�y�N�6bL,�Y�o�}ٝ��-����~���@m/7K�>�;�Q#�^_�Lٰ����� ��+��1���:�?!Ib�B�d��}�a�[Q^���j�p�ZɫwG��+�>:�E
pd�� ���;z���i3t)���x�Λ�e��6�S��!����[Q����y�/ ���b�ҧ��z�*�Z1 ��ˇuf�l�^�<Ͳ���ᾶ��2x������ݬ��Zq�^]������ȹ�b/w"?^7�f���?Tx��DN���8�.��\�#�8s�ͣ���SƝ�yuLe�R�nހ�_����^Ať!�*3Uѿ�n?�o�wr�s�x�p�FN��fy�7dC ������P�ϑ��y
��z�ј��_�� �Ǜ�f���f]sg�^�:$�f�Ư� K0�
 ��f��o=`�_�y*d/��˒D�h�%#�}�8�\ɹ)�ް��t{IJζ��g�s.�n�ۍ��a <{�88~���2Wj���D,�� �|�G�qa�u�����kj��lnoG��~=:C�xH��1�^���r�&���R�)?+tˬ(��/^ǻ��%f���i3\�xӺ �O�&��Q�9��+����"=�����]w5J�]�0+X~R _�l,��dB/�p���M0�I�ݪe�@�^�@��w��I:p�I�8��{�-�t����ε
����}�OAjɮBj0��7��� :~��|�??~k]h�2���EW��$/�$�ۆs�rrv���A�O�c8q�Tm7�i:54�G������@��~�/7��ι��������^5֍'�u _ͷG��f�����G�[����df��L5����{ߑf%&t�R,=�}��LG�ۛ��6�}��^�r��0N�4��?�*$����P��p���?s���R�ﾹaծ����7����_%g-��_j�>b=��b�~�Gls�\�Ǥ�j������#�/Y�ɷ�92}��B��ݬ��׾�	�}?F�~l{+��¯J	@�*�k��]�*5��t��az[-�R
 ւL��#�O�E������<))�FZpq��K�K.@ӷ���TZ�jv���d3�Y2�u�P
[��Bn�%��j|( "���t>�JH��ͼA{z�=�*L�˝�����y�f���U��f�qq�Ɣ�O�G�&�W�aq|�]����n�բ#OW'~!��t�H��ǡK��U.tᦛ�����Y�t:��if���jC�ؐNS
 s���;J�D����l��P⏛����پ�$�G�]�HA���"';�#�d�tK��SE>�R+Eym1j��L�p�a��$��C��f������M(7E�Ҫ�^�̙�H��0u�[e��%Pt{��7	o\�2b���[w\E�Ԥ {`��j�Rv�-��]*�{N�A�NJ�$����
`2��o�fq͆sze5�ێ[b@d89�]���S��y��O���rx�qɥ_�C6��|��Nl�= �cl�%�WfͷW;�ٻ �8+�y��Bx��ʢę5��u!�9)sM��m�bNN(k����-BJ�"$^�ا4�v�;�>��r�z�$��~�2\���]^�1���9f��5k�X.���8@M2�^z;#$p'C��V�mV+I?�o��p���qh��J���!���t�a�ʴ��4�^=��+`�����T��y�	�    s��'����TW@��1�?���j�rz֯V�"�?v UB��ױ�`�K���c/F���&>nY�B%yBf�ER�4/��=O3������y�쫯P��
q�����l�\�m?����M�H�;���N�l���C�2tM���E�ueSf<�E���+���v;:
p��nL����TMӰ����M��y��X�s�=۬�6�se
�PZ;���w�%yԮ�6|�h�vH�����$Ā_]i4�kӋ�A-�ƚK$�E�(l�F/�P%������᠘\� s�Z
�Xv��zl����0
�l��	�V�U9)�D��2�y*~��EF9S*ҟD��l��؄E� $(o�z;Ԅ���|>h��k�n�c29��&'�8h�7��դ�����L��s�0�}�F�7@Y���k;<�i��nO+��v�lSH;�O��Ԥ�f�ln�]�]A�C�R��Bp�,I�T�v���@��j��ʯ-;��%Y��%Iƕ�%��=���.� ʗ��&,�\��B�G�6^)��HkA���j�.
gȟ��?5�M�}u�HY��|K�$��4x�E��!�~���G]���l�Z�&�m��dA�l!��l>Q~��9���4s^���C�h�0����\Ҝ��' c~_�s@�Z���p+�E����g
a��3 �t1� t�$-F]L���

r�A�w����0*�~�x9�;1tb�e�Sbg�I�dD�-�rIk����rk<���C����JS�S�V�� 2���S�Q��x�%jp���!E$�n�A�j�����h�����ټ{\�����`X�~��n���~��b���/_��x�u�'킡�{����aޯޢ��QdϽD��[�}�z�}��:)W�T���I�Ȱ�O�Uՠ���$��$�?�]�� [���޽;�#e8uEYyG��Hj�e7Kx{�XIͪ[�
XG��{�l2:×֥�ӯ�[�f_�^��|�R�T�F3,�9�!#Y���ڡ҆���rgE)]��K���ro^�	�t�K6�.��Yd�8
H���4*9���\�^e�RٽT�^*��*���c��M�5����s��j���BV���]�(�
H\�<j������?lc�xa%���י�q�`$R���L�z�-�D��%?�7�H:N
p|�ݤ��hpc�V>/�]����gʄ��ҋ����H~���j�/�U�Iy���,�cx��S��-��7�v��$��$Ї�]�g�?o�e�K��&� ��ɵ�+p�I���
lYW`G����%2�ľ�w���A ��Ǆ���}�R`^Č����h	v�HU��2�T�^�9�^�ƣ��i�w�å���qI��-io�'�q+��D�p��g��|���1�_�ǔ6�3�`�g�\�����41ʑ����*:����a�n�X��nE	w�4O�\�f+H��Zr�� Ҝ��7'�,���4 ����W1�&��AZ%P�6�)�IU�?��1�%%���yH�K�P��%,����Q�p79�~�&EVoڊ�H���N�DI�	�%��
	�{�8,ҕ�Az���X�*'3Z���=�I?ܴ͆��/w��";�aV�sq»�_���]�Ց��}�B��x�V�����[>% �RK����(���qh]5��6{�Y�u��K��'�Gɍ�ݟLS+�����}�ͦ�A���������Ћ���y�E½ƕ*]ZJ.�n�����B������- &ȇ�c�ż���W����ZxF�}���h�U�j���n��C��u��;�,ځL�PV�Y���Q����>JP����������Ix�a_l��|D���ϟR��Z:_[	L���&��ۏ���7A�c���u(�6�Ξ6Iqr%����:2�Ai��r%n�7C�%{5���xKc6?f4c���zv�6��,�Ǎ��C�d���%E�Dҧ��:���X�Rw
;�����>9,m��*|/���{|�-���n��&i�l�>�(����Z�[1)t�oy��5F�����M��@b5zM��<��7Cbύin�6z�t�`$6(�0R	\+=}�ƪ m�,W�*ey ����;��N̨i6��-�S���#�1�����l_%�7��mb{X�];�I�L�mkV���r{��Qp�a��-R4�/2�$��|�*o�@x]9Ց	+j�:?���_�ƾk2�s�G���T�K�����ߓv�&�@U���]
٭��N�W	���g���3ts3';Vl;��rЫ�����TU�v��#��
��x�rGa��n`c���n�lT�x�X�F�_#g���W�y�;o"��B�\7�>����Wp��<#�U@�;U�����gN�z�h���㠡w����ej*���$"�a��Ub{��~r�G*�~�|��u��Mp��E�R%ŝvE&H������p;M�JK��q��T� �/�p��q#;�iM*���zo��W	h:M�M����EH6���Y��媿�#�lH���m�ѐ͟},P�X�(q+WU�d��	Fb��}�VH)74�	K��K���>�qD|l6˛�)q{j
�J*08R&G�� �R<+0S�X�T�1Q�Q �H�"����k�/w)AQ�-���H)�d�7���.�`@�CW��MC�dY$��Ѳ����MRYqX�����D�"��LP"�:�����e�V�D	w�τG���4dzʪ��qt�Ȫ6�
�����K��% ����Xi��*0�q�w�P�6��=�~kE�TN�^L�2�~i~$.W�z!%e��Y&,��M�Y�7�	��~���)q�p�&)�%���aC�+5�d�I:�7�8$N�a֓I\�ip�ǂ��TC������8 ӏ�g��M>�D8	���vX��I��z�^��[��jO��SW뱼sO&95�y��H��J�fKQϛ�MȸF�����cGV22��W��jMrE��nެ�X[#�l���Z[!�)V_|m@HS&U`S�m�w��	�
.�1��8�D���I���0[����l�~�5�|!�_"���"����]��2!��l�I�67��?�������B��������l�Y�W�� ���<� �ջm_�Xbf���BI����t>}��S�"?K��83��.�oI�b��ӫ�Vi���S�^'�e#Ե}d�%pk�@�
t���d���N8����9��`$u�(Jx�c����NoΛ�] ]�\��5]�ݘq��o����A8[7�E��� �^Κa��o b%�4�*�RU{��^*�S�����p�2���A���E�2ZV���_������������&�c�`'[M5	?��L?�7��L	k���[��U�����D1,��]���Ubz:o�i�ޢ�� �����s�QC����y3���h�i���,��I��#�� �&���Tj/�x�~�o��e I/��f�5	k�[EaZ���H%���D�Ķ�GE�p�84�g���ͥؼ��G�)�#:�2&,]v�J}����1��0MW�{+���湒Tmsl>��ҭk�RJ�4m�u.8U��$yݐ���B`���U�x翄c�J�V:c�-��%E#��0w!EFvL~A��{�������n������e��m������똚�T�窄��*ԏt$A9��ˣf��-�2�(y�S%<��zoj�x>s�'x�Z����|?Y��LF��8��j�S$�;��f�0��^d��x�����o�������N
k9�H�@^��5[���Y1}��Jl�ۗ�������vđ����V�h�[N�G������MY	F������/2˞�5��[��Dvl�jo��[i�Vֲ�Z�`�ӡ%3?Y ��?6Y�����"?AA�1Z�����o��Y#^@	ֶ$O��'x�[P�(:��O���HO����ӌ��иt����`_O���qT�*}�\�*ɷM�綶��(�����!)� h��!9Q����H=s�����S����3/�u��P�+��
ay6�&e;����.A\LQ����.7â]�������k�����u��4f˦�ۑ����vC�v����K �=��z�|s[��M    ���졁�7_����-����r������cC4u>I����6{�:^�X%/������b;�i>��Χl���[�N���W���庭�P�e�~2��u���5`j�	"�S��لTMK]��`��|d���5 ��{3OUơ��M��&ET�Յ ��%��8u_q��z�8�����/S/j��7�0���W�\1y��u���j���m��Y��n=K|E�3!)�]nk`�(��5u�Ғ��S�Y� ��r?Y��ľIf�
��S�d�����$<���ܤd:0~@��p?�Rǿ�׏ۣ͆�e�{�K�6�^b�q�1�L��K�-U�B�߱������7��j� ~h7�yEZ��O�˻�I��Kh���\[%��ʃ\�7�C'V�IU���H�=�������W��BY�I]7W��<��h�$���q"��ۿ���l��<�XL�{���U� 2�ē�r��=�YRn����_	?n�V -�rm՞:����S7���8�'�� ��W�CB���w�JxR��/��[������� �����R�^��a�.S�~�ݫ~X67I������J6'	���*�6~nH�p��}��5b�1<v���%�m��C�NNv�˔R/��h��}˃�Z��{6L-���l�H�)��\�ɿԤ_X�$L���}�����k���(+q�
H�����t��{)���Y���<�n��6��\��%�Qܛ�d��Hd����p���"5�d��n`�]�����.ߕ���+$O���Λ�']�\Q�/U�K���1O�M���F
��������]����b�'|�&�q /�)	�x��8�6��ہlJ��t�r�Q��%#��*�nB�*��Il?u����6`�4���>�9 ���P�f~N��ZNW;��Q�8ԅ�>��jg�=�)w�P��>�r�_l�n����v�d���n�ŀW��
���a݄������L�aH��wo�$��]�ס_�hS?:����m3O���S����@�.o��ŕ2��~������9-i��=��M�MV{��^�q�� 7:n�z�#�4; ��C�!����xtv�#5�+] ���M$��ɒ3��q`����&��6c <|H��GT�#���g���15M~�,�H=gꟄlԗ���$�.6���f��I�`���mB:ig�h���#r��ǧ�IM)X&��������ma�
����N,��$'qW�S���6���G<���j�wڄ{�����7v�X7��Q��A�#�7�1Y����(�C��H�R�.O����M�"�������c'	��x��\,�s�$��  :��6��/2��I�+�-87�?O����m��p�!�|xY�ng����D�H�#����}D�3;$�p�����myf�c(�Ƕ�����R���tS�)��%g��8���^"������
�T�h�5qD|���=���&��Ml�I�!������[��_��\��BR��{�p�����ȿ���i ��z��)����:rv���-�`ȷ7�s'~��2)�L�b��N�D�f���
��%c}x:J�٘��[6����$*���c��`�
6����t�^�-AD6[z�RU��Ƣ3�פ�K�fG�b/(R*U�S�,v�#��u�K�Q���r�����<
�ҎJ��6������o/r/�C�1D�Kb�8�ѕ���x!�����4��K����7�#����
�}�f_�S}��:z��oHBj���m^߰�[�]����i߅$X%�;{��o��yHSD�෼Y���HR��o���~���s^�c�<�ˡ�%��gCJq8	��Kԯ��{�%�>O���Y��2��ێ|� ���ݼ#q i�͐���15�>�g���P���YI�zB�'�x(�;�������(�+�	�ʊ�dj?Y6��2��"����=#z�ef;��dV>�$�mdF�g��� �۬��!��S�.���Q�Հ��u�[�t�8zt����Aڗ�����F�Wd�86*G��e_w�<c�TG��v��q�x�2�Ŏ���y�҅�0)\��q�8<��7c`�eI�/q�c�;
Q���&��Ml_'���F��6�b�m��|�6���[��2�]Y�<?%��o��Lc�U�d1sfY���X��F�y;g3
{)66�+o��R�q�8F�o��M���a�.����&�fY�p�m�&�Jy��C��}D�>"xf�2$u�)6���48h%�r��ն��c?���U����fu�Q1J^)Ś�Uv�?��Y�� ���L��A?p������f��������!�:��{j�+��+��ߙ-��q�|X���y���bW�Ma�>����d؄2�t�W/��O�+�T��Z�u�k�	s'�>GZ�oB'�����>��ڿHm�
�y�־B;N-����^&k�u3'���z�r�\ ����pp����� ��7r�f��YA��`�����AY���{��<ub{���4���yH �~�>@ZR�e����C���{�-$M�c�6�~� FV�X�!���Ђe�쫏C�(E}�Ϫ~���v<tieZ��ֳ��G�-^}��+�W_����v�6����� �O�U Q�M|yh��v+y��l^6�������n3��:̍o~u���׀sP3o������k�,��୧O��.#%8�韼-����\z�+��U{�̞��f��P��$s|O�%�vP$��-'��}v�ɣ�g��G-���Ջa�ZuM��z�'$j�"����a�_)~~�6����
�6\{�ؾNl�ګ\�y�}�7E��󎳵U>�-	�퐏�_���`�z����Y?�&�@Tߟ��X� �D����-���������A�`ph���<l$��A�_�c�$k��`s�,���`�@^5�����P��k��+��O�Y�U�W��*�/P���7{��:&�XM����y�71\P����Z��`r���'�(qM�"?*�j�`�) Wa�d-�RϿCR��R�{���Tq���i~4
���[���={�Zn%��S
}y�H䇐\O�Yh�L�z�+6��X���o�$�����_jp��!��c���6�
,��ޱ��s��a�p�� _Ϟ�M��(uN獷�f	�%������]��]'����p���X.N%���a�z��������E���vfe�5y`���z� ?���y���N�u����~����u����#��ZMs�$]k��Ʃ��笧�x�Dp�;gN��{����W(^}��
R�CS@�Q>MƗ����]3��88%������ ���O	�S@���������7]�`���D�oc9Rd�d�X�ww���)�f&�[�'��?p��F��F5��tfK��y~��n��.���7��Q�1'��MĻ�)��R��[�s��UI-2�������#C�� �H<w�}�@n��eȾ5|�_/�����dv?�̴��M
��<�~�jsGn{k���,�Ó�=�$���d�:'w�`ٗ͚���UZ`�0�V���oOV���a��~�B�U�šbI�d����*<,L�9)@���.��G��* �d��G���*^.���jR�� V�_�v�٭#�v�+j���y�q�*���������^�H�@�۟P-��������>�9���������sS��4�R���ګ��:�= fM:kqf�5�v^��f	�]�t{䫎'����Υ
�����L-����6�]k��[a�w�@>#��͢&I�����<x�E����&Ya�t�	�~����ӧ.v'��sqL}�o����>9>ɂ�9'�����pI�C"]�:f�}|	RV�6�[G�K8OE��оNl���/�t-Ӏɖ��� ơ�!�>���\��pX��MZK�i�j�ۏ���z�����qM�P�����໧�j��O�RKEU9w�����(^{����~��P�Rrч�^WѺ8�rC��j@�0S��WN"�����[���7C�Δ���    ��|.P��_�$��Li6DF�m�7�<�Բ��$U�*nç�m�P*�k�ۗ��������&�����I�9����%C¿�����@��7�d�_��s�Ғ�������$/����J�?H�(3��!�t��d��/�R��D*�:���df?��OV�'�aσ����\Y�R�]��Q�:�;������ �I|t<�R6g���M��<�j�$�$���[���Ų²n��{�8F��J�M��XQ�5�$_B������ɀǻk������=�Ѡ��=9�u���JS� 3fh Mߞ�Հ2}{Ҥ�����ҷ'g"�˪b/ eB{�ؾLl_��G�lkv<��mM?O-��� N�59J�Fߞ%P�oύ���(��K�Fߞ}�ؿ6'���1��Ilo��Ҟ?%(�oύ�l�j��*����Q������ۓ㧐�-��	�(��ĵ.B�%N������&e]U�l�-8�G�VƱO\�B���
n�O+���j�ܬ3�BN)�C�Q���:�Tz"�j���Bp3�h�������.HnUK��2�V��K~� ��C�,��	�9�ʚE�z�O�H���2�񍙹3�s�e�TR:1���M�RuA�`�pS�1���.S���~�r'+m0�8���0���:��sjD�� ���s��l?����V��%+�g����ե�X\	��C���eU��)���f�ǝ*��3'���\Oi��\KI��w�Y�,ɼK����z�-�^�Â%@��jO��S��г�!��P����<y�1C�)E	�)��7�cvwJ[�;�]|ؒ�z���E��GL��h��3ʞ�Ge���4�����W$�)D�6�^���$��N��dg `���(<��T�dE��p�
���o�� _PhԜ��U%8��?BWK@�_XO�������Yr]�m��0E�����e0(�{�Q���O�p����4R)|~\�Z2ui�ɕ�C���~�b?��O��p�G��O%Ч�:>;G	���.k�Ƀ>S*�A�:�?�i-8o ���I!E��q����(DPr��DU�(��#��&-�Z;j��:�}�ؾJl���fX��ϫ�Kr�=o��&8�9�U����u�Z57�0��X�l�[0Dt���EJ�L�~����K(�l��*%�*I�����l���]���p��_�C�){>��ߡ3�
���z_!V��e��T��!g)Q���y��Jpc�@a/��|�p�@,HXB�J��vN�-V���xl�����o�AW	�e7Õd����[2˷�Mh_$�G�G�R��*$��sO R��J�0%Uz�-_v�*$�x������B�+����߄�-���]Śh	�����Y�ˌܾTJ5�m�5ҟ ���["*�ل�:�}��Gq��4�Π,M��X'y�H��ȑI+*0��;?�����=�TK��`qm��
�WZ^I6W���LMW�*��Ilo��nV��nA+C�U)�K��
��r�����+����_G!��}�Y�Go�ˆ�o@8�{�$���@9���G!�2�f{�b��.�G����bw?a;���z��[Z��K'�L�6��S�]�x�
�3���rv���o8Q|^<���pRU�z&4�m�τ
�g�F<U���6��<kKn�*!���|�%�]������BX��j�?z5������s�@VǼ01��bX��~�y�ി�mCn�mF��_�7hI-H�~�~��k�,�k��p1R�PhkC����Y�5C�J�֤CreǪ�$c֐�����(G%U���Ub{��>�������f#��Z�E�8U��6�ۋ!yHS�k(�bȒ��H���?xʶ������.�=����1T��Y���"^��dz�?]��_���{�������~�:���w˻���
�ӆ�@\{Tw�FB��~���:��l䴉N�a@8���׈���m�0�����-��~�
wHz@Ϝu�5��:� ϒu�6@��;㐪�49��i�<�g�<�'�;1 �ǳ�O�g@=5��j cٷ6�҉�j���y~{Is�)$�47����w����dd�$�0 ��zG{�Lߞ��~?�H�y�Yt�7@�)�*�Sr�}2��2��ސ����7뾟=�U4�Ev;e 5�ai�&� o�wޮH��n�x$ڔ����1Em~R�?VX(ib��ґ&��ۃ�&�/��a�a���������c��M2宼HZ�[#���:I�SQ�$5R�<�1���y�G��"���ʛp	0P����> :=�K�׈�k��k���o��,G�S��ٗ� �'B��=����灘p��w{�t�I��P��f)�Ә�sS¬ι��쭴{+��c6E��f:L.�c$�h�4s%�hp��G)�� x~���{ƀrFA���<����O�N<=��lc�����VU�S�j�_Xv�,g~�+��P�%\$;�b����*'KW�R�r�a)�ޥ�a)�>�U�>�������q`C;��iLD
`OR��:8���� ��v	(HY��B�#aHjY�'w� �_�e7�7�2��JM���dz?Y�7��Nh@�WM̄}���ttO����-���ݻ��o>~�N>��M�Ĳ�]�Wȋ ������/ٻ/ǿ�K��N�D1��������.�3dp,d/`s����%�l���ޚ��]��ޚ#��=X�Փ~�'���>yAd�����W�ԛW�%W�as��n�e~n-��8�LO
Q~�_4d�NE&�N-x-|�UY���6�r�%��1�X���[$- mQ�ׇ+ZE�������.�=������/�;r��]�;V��߬o�BotX���>V��o�,�ƽZ0ڸW����h�n�-Z�^�j��	z����I�T��t=o��N*��p[N|���x�Z�J��Ǧ�΋�LFg�r/�����Y��j�d޶s�Bq�x{����2{;��u�r2���nS�+�Y!��K��/��'��kV�@Qo�a�G�!򹐽���V��);W�d',��Ƌv=��Y�KfA�Fc�DKV����[Y�V��g8�>/rC�D�`�,7ס�EȄ@
�?RE�ྐྵ:!�d@޲<!V%������`��Ǒ�yZ,Hn����<L��� ;)�U�98�g���[i�V�{+'�,O��Y�Ev��=�V�/,aAnӞь�&���[3���*���c1o򺱚�����p^8��9i�k2���=&��[#��ȏ۸��6Ol_$��n�L ���� bز����0���Vg��-��Y;k�!{��Ȳ`���uB"��?=YJڂ�n�YH�d6�E�P���jO��SW���h{��ƑeMw*xj�>y�����2u[�2sW��$$b	X !��fg ���3��p	U�1�#��׶����!���_�gc�*��U���5[ͯ�k/�Mb��6��h�ez��t���[M�]Y:)tWw��%e�U�b���;�C"����P��v�(���ZS��:QR_���iّ�Йl|��(�b�}���ĬVM�=��*�P�@�>+R�&ޔ]a%uA0:��А��;T��u�@�b�+��3�뇠U�zc�*J*�*�9�mJ-�\�*�T�E8�4��o���D�U������be��im��?��ڙ�Or��GE(����}���@&�V!����w�G.t䤌�ШYM�L�륑|=K�)|����X;��cLV���:>�Q|2������l}u����O��c����UjYT%蒩phˉEV���7��y�1�@���`仒�U4R^�ny_�?s�1���g�9k]�L`ѸJ����V�7L���Q��u�Mރ5d�_��Ϟu䕊�_0��cd#t�uS�
�('ٳU���;/;x��
"<�dSp�d�S�l���U"|�A�/u!����B��e,��t��ƫN��,wZ\�[^X^�SO��[��GŚ淺|-�.�l��5�Ʒ'�i꒚k��9n��!�b]���Ӎ�0B�m����iӀ[�tܔgz<k��T��t�?��[��9Q��]�e�(���!S��    M��.��VG�75ۼf4o�0�'��76�u+����Ǣ���:��5]4����pv�QC^>��D�z�Ob��w�O�0݄ �#H��gQ�G>m@#�S��~d�����0��R�ZO���*VN�i���ii���� @���fŰ���XQ5^O�n�.Z��6��]`u^U��(�O����F7�����Z�+\�1>���}<{01�G��B�b���Xyu��������L�t0W��&{�ެ�Y�EFNƲ�E[>=��D�M�~�&�Mǰ����ʼ��ۧ[����X��Z��~�"5�>�������ャb^���S�d�_t<��j�7?9�7�&�/=t��STԑ>��@��8+W���\�1c-ؙ?DG���:�e�@NN
L_%ra�tK2֊/���<��|u%.-:�f�8�K���M������,
�6c��K���m��D|�t��$�l-pcU/��j��X+���1^/M�?8�6c�ؚJ��ԉ�N��ȑ���`Պ�%d�Z�������XIb��h&���ª�u�װ:�����?�h/�_�=��	Z �ힼ9?̒`N��<�]��^M���ۯ ��'˜���&�I�-�,���V�����r�p�n&��P� �m��t�4Hq���%x�1�S�r.�C��>��Xl��m �-�g,6���E5����Gr1Ȇr��sƂ�M��m�w�XB>/�vQ��CD����X�E]����.`�
���!��t{�w5Wp�1�^X�O05c���ϧR����d"����cZ^���f�׵�WU�w�%M��B��ai(_ϛ�۴��7xBeu��ś���2)�PΛV�es(}|���C$B:(�U��g�ɷ����Fo�.Q{��Cd,�V:U��o6pS*�y�nm6��o�U�������Ff,u��7����Ū\zQ�`�,�r�݊�{V\��� ���z�����z���?ѓ���<�y�h�ڶ�94�X72�׺Y�4��b�c!���~�P�/JM�:���sX��.�=L�c��H&e�)�w�O�,ɋ�m��4?��-�G�ͪD�1V�/�2��P�A��>]4�.u�B��4�O��8������F��	 UB�5 �,�� �-�CI�#,�)ˢ�b��, ��4�\�8�n�ë�u��`s�,ۿll
�'�����3�x2ٿnL�#b���F�D��DdM�����c�v�ؿslQ$��}C�;�w���9�U/���w�Uވ'���cݜ�ؿs,����;�Ϝ�x�a8u�O���O��ԙT�$Wu�8�X5W���U,u'���0���� �Ǚ	�I� �U��A��+�`����#���$��	�>|�n\�e+��0�S-�8F���M��<�̍9ٞ�9>���'2��"Hb>���bq�_�oz����'�h��]� �o��q�/5&���0��C����"��f����%�I0�'E��<T�	�Ё�>r��5��"�9��\1��U��\��Ϯo��%�]�]���'�(�{,��O��i��˚,��J�&\}RRR{�X58�ǣD�YM��h�M>��`��ܽ�"��gĎ��#�s����/�@�P��]cq����hD�B1ϵ؉�����%�e,�~/^K�0��Pں���&I�>��E���>����|�U^��bPka9�L���GG`��C#��8k�sTi�q����+r��{	��x1��Z�@���hh]u�]zOM_=���A��88v����-S�
��Y�=ϗ��ka�+�Y����N0�(y�-�O���zY�E�v�>��>���O�n���ͪ��K�sW� ���z>�n����sD������z��]��x�d��}|�L��ŬɊ)�����b=d�T� O�v��C0��7�����#�!����]/u�G��QLC_6*�{�h{�����d?�䵞gm`���Q�S>�9�lW�K�E�gu�ϓ:�ذ_z+�%�g��![�K�YCQ>�z��O-�W��gv�Kܷ�2ӃI ��5�ٻC�h�o�X�1&>؛#��,���͋V�r�b!�0!X�?�YȽZ��8����=n
,���t�A�g����t�K\&��mӮz��Q,�ZS�8Q��,VXI~"�&j���"o�%��%�W�x�3a�*�*p��K�)�ھDo��u򶜣mD	b�o�z�_/����\B���:��	3�p۬s�Xv�"[ ��@e��>���M 	>�Y��<��nW/���,�ޔ?s�wXX�njt�eU��X�r}R��ҪB]r�eUS@�~��/:Q����r`���{�<c�W�\gK�>?�����.�e�o�70��87-*AءyԴ��zUO��z��A�gE��tS^��\	uZ��t����=�� �27��T�w�XQ͗�M�C�B�tR�K�Fƴ�#���^0���Dq&PSYT�$Jd���ZpY'�-�6M��Ef��Q>�x",�^�[nfS�$���4��]����ʧ=�a��.�>����-��]a����~)��k��?x?.��@�Kݞ�n�NL�~?���P2O��\�3^���?lS?�ii�`��p��mk9�6̟��sp�u�{���c����S��Y��b�wZ>��	�헵�Gz�(�󶾠����
�^b�i>��I_���a��d��X�uIHI�;���)=�KC��coZ/�sx]a�Ӂ˜���N�}鰣���nI�1^f�H����7�K���}��nh 7�`^��)x��,�$9�"^��8c.���o�k��n��>[*}+���MҕE%�8��1��U_�E�]>>���!:�~�6�t���m���i_8g��݂v�����[�Z���ԡ
"��0ȝ]����n����۬�D
�tnۣ�Y 'v���7x�:���3mvAؘ������Q'*r�b'*q�8I�,���e��![?�}�S5�o�x=���1��r^v�����#/���f�������α�gg�^�G����]M��0Ȏ�Ď6&%E�����>�$�)���G{q�v3��Mx!��V�TE�[j�_�łp��s��U�ԉRNT�B�r���`)-��XY�Ai+`	w�h���[.:���B�X��Ua<����z
�u�ɞ˸7���nl\;�r/'a����	勗�˙	XǍ�S,8&�'�������K�����=���Ut��6�4
G�Tz=�7y��\��7�sêȻ<cX��j�����ܻ��(+���s���Y��ݚ����#�@Xz�����3��e��Ꚇf2���uQ��k��`�����qxӵ/bP+�)`�$�,.���P�+�����:u�
�p%�f�����������0��*���Ȭ��rh�b�leज@�V��$��q��,*��(�/%�%��_2
��E�������!�nH���nMZ6*X*6mk����}���[�Hl��?[�WTK�(��x���c������Z� ,#�����é�8�)cS�B�&�����S��;�#֘�P�<����_7��³��z��:">
O����g��� U��5��4�j���}����4"��]�Y(���<��a9}�m�����;��-Y�� ���ߊy��<�P�E��vK=G��Py�����ѡz�]���*C�����M�l(e��U�x 6��*j�7��h��2I���9{��b�u��㡂����_~{�wU�5��{�G��X�b����񷓻��;by���tv?�$ސelK&q`R���������Tn� ����~��Q"Z�=��]O�P���lzr��_��K׏mQ��O�z���?��Ť�h}���`����g�&���0w0d�Za�k�r�?�� xbJį%�9@�H�BRҥ�;���		89�}�>O}�L+�	C7Q�&0��o�Z�/$�Im^�eaJ��(�}��+a;�_����e��b�6V�S�R����+(�c�uG�X�c��t�	�����d�pZ(�⊮hM��O�m��u�GP��    ���%z[.��=�?a�M(��'G>&c��S��۟O���SQx�śb�z4�g��O�F2V����f���h8m����n��e�q�D�NT�D�#�V���2�$�����u�׳��k�pwl��j�2�px$VȂ�(�O�E�n^m�7CQߗR�Kل��I<z<R.�n�Di}�˥dA0u�+�9��T�k����j�Q�L~;i������R*��9�xϫ�ձl}��u�X���Cc]���RC��g�ۋ���7�v;;{Ic9w�g���
�*�8��1��������Nt�èCV��4���-��-�,��`0�b���|Zn������Y��XB-�����-��ժ���	8J������O�ۻq�!����/����w�!�ǜ��l�y�g���!n6�@9�Q�aJ�Љ���؉�����W���hy���o	Y�5��'�f��"� ���j�׍��q�X�nu�y��6Sa��Ĵ��U^?;�����ょ�(��p�F�"�5������5R܎"V|�p�N����ZM0b��^ǽX%�E��=��p������#�O#Q��N��h?�~��~������J�O�8	$��su�����ݲ\�� gW��Z̟�̶�ԓ��|7�����Г'�D�䈶�*�Y팢`�]υ�o�/U�s)�l���5��;���(��m[ZxE�=_��<��;
$�u۬���5b%�;��V��z{eǏz��Pܰ���0c@Q:�4�(�(��E�t�SG�:O�)z?��Ic1�Q���=�e�Y?Kω��"V����~#�L�כ��O��e����S��j�m{���P���yAa�#V�G��M�S=ڨ0^�zcĚ�q�>��r��.8���o��������̛}������@"֔�p:}���]_}�V��q������)��HA�� �g���e�9Ƃ�#֖�t
~ܬ*OB��A�>���e��%x�?��E"�M��9-ß�g��o	.�,'O��bg��dAy:�h>]Y9.,&O��e�Vh6�!B�UL�L��X6&��X-�.ȝ��x"�d&�/�q�X(��7,t��.����tDX�>��N�^6��$�ӖV�rY�e$�=��RS�	�F�`�^�"�@���Z1a�vIi���lL�_� ����k2�;0��
�g^/[xC���z���A��Z��y2� O�ʛΛ��O, Ouh�Ҧ�<���쟰 �Hj�2�Rϗ)�8!�*��C�l"	�����@(u����C�Ⱦ�F���'�x"�ѯ˺�,#f�w��[Z���ǌM]��U_B�1�1�L��q̲.^I,f�� �o��:`p�I,�*��v���1k�����L8flN�c_�a���Z�"îuQlʍ}��(�\�h�.g�2�@��c):Q/��K)b����g���~.���D��GYcѱh������!j�Z1�;0C�����8H�H�1��A����p2�ß8�V���L:�l�Yl��?�I�;)�:GSob�J9�p�b�J�ڢ4�e-�|���Ĭ�:��3���lL^�}�b�V�XU�]�����/���b���|���wq��٪h���3��1��m��w�{v,�N�����Ake�@�4$H�=��؏Yv�cX[�d|&����]�X(�Զ��c�Fu78�߉�TH�{�Ŭ���~a��N�E�e���QAj�Ճ�R"�ǗL�2'҃�yG��͎��S0t#WP���p-�XTҖ�J��/O�RB���,��M�^� �[��<��D?�VUQ>�,|XQP5h!uŢ�v��ȟ{���XT�nSX}E��n׺�\֩�EH��'*u��N�ͺ��z{���mn�Ƌ~ڷ����Ģ�2�*Ģ���~]s�B��qMv�EIe����w\���EQe��1=0Q����=\C4MՖ�\(Vm)��׆HеE�X�EQ��W`c�X��}�H����������λֵ����j�����u�r ���o�q �Ie�K�������|=o�ל��jE�DhMp�u^��z��N"��{�I�I2�t8�h��a�G�D�-*��=�d#�^�_�-�����b-�%R!okx1J|1��n���;0�#V�kz`c09��h7�D
�r�T�KXLՔM@d�j*a�K �EOXM%�_4������wS�%�s�@mm��Ǻym��آ���z\X$`&A��(&�gB1�ʳ:�OB���4�XR,�J�EU=U��'a8Pe�ż$Rs���BO��qb��_b�L�]���M�-�(���z\>{�%\�>a����w���al!e��mR�VN8ߑ�p��-XR2����5ؕ0��e�EW�H�cm�����HlC:Xfg%R|װ�[*���GM��-�D5ԉ)l r��;oڢ�2^�WAVM��ȉ���dOI�զ]�`U�D�h8G8�����0�--��b1�%1�sz����=�Gη��4��A2xG��FZ����O�řt2tY��� u��F���$I�D�N�r�����F�����4��9��S��U�%����F��nJ��<���8�$��`hhh����y8�(I�1����`"�Z@?I3JM�(߉:�z����qX�5L��>�ZtO���%�`�R�㺩෬�qS�9�}'J�0t�W۸Ձ�����>o�?A�`��&G���
��ȉ:��c���7%x�d{��kg{��`Tp����	��d{��7��N�v�m�2z:�ϖN�F��U���$�H�T$��Eo�g;0��:0l�z��,"XS�L�zN驈�F(�G�RrdJ�5���ܻm^1�7ɴ��~-+��%ݴ���OE8m͖ D�	#�mLE3m�%��F��T�S2s%EDu!�J`!#��6��y��i(TL�����T�@��4��7n�����h)n�\|�`����HELe���Y��l����J^۶��0�"��s�#:rC�P�s�b���~i8�x}Ӈ��<�N�������̒��j8<�4�Uc�T)�UwV܂^w*�j��c���ku���Ŋ~|�������ik��-����;��/m&
\���"�}l,}�\F�ð}�Gܥ���m]vZ���OQ[�$ަ(��t��ԍ�@,:`x��TDW�=�]�K�Ȯ&�_��������r�27NdVÁt�(����Hq�>���$�b��Rin�/�`��Tژ�K�*i��\�.�3�Х�A�p��%Փ����޻���4���U��ܨ���|7,p��#O��uK�F�ݦk,�#����zNn�o��Ѭ�Y58ۥ��X$��Q7�0�g�&�SM�X�z��=��5Ub'u��l��*t#W0v�L�e��b�9v�ҁ�
�H�碧Jl������K��9a�WMإ�d�`;��b���S�:^�08�?��ƕ�AP,F7�;����K��OO� �8r�#����������|ݬ�Y�j"�Җs0�NM������D���
-֡&b$m3����d����z'=x��]��MO;�9ԯ&b�N�E��$s����;Q�@5-^8G�b��m���Q�9�b�U��H�S��ǖ��]nS�@�>{R,��U�{��YP�:`?���S�/A�^�bs���X�6f��Y.t�"G.�n��� yᾨZ}UMWZ%@���׺ �ʪ��W��IQ�v_ !6���B���nmm��P���n�2�0�f�`��b��J\��R��h�߃X&X�h�r�-hL,��9|�g�U�4��0T٭����-����1��*1�7��y&�D��جъ*S)iWr_�a�*R�\���GN��\`����u�6�U��_��+m��U,Sn�=(�X,�&��4`F�WT<�
(
�X���r*#��k>��Go�
|y��L�d�LF%�-���D����(3+Qf��cӂ�fJ��U��t��b���Q����*JT�R�9i��P$Y]/h�z����R"�
���Jt�)zl���FJ�X[*v�'*��l��(�a+�%��}Q��j����JdXͽ    �۔ȰUA+2����&RlU��y�؉J�k
?[�@��wtD�5,M+Q`���w�o��U� � *W0sEp��5Qaݭ�qT��6�ܻi+ЦE���<�YcE�5�X�Z�KX�om�SDXg�L�U���N΢�r�X8K������j����� ��`�
����0��Lt�f��vE�D�m�}l�^&*�#���D�mnҚw`�&j���:kf"�6�&&1�j&r,s'͝��Om���͗�p��$�BF�d�L&�"7!m�p:4�f����Z(�H�X"�.�5gy[|���B��e�`*uWV(3I�ƜdC��3= ,�&1�Wݼ�Yg"��s�#�91��U	z�ȱ-�r�9�(�zͶU�D�5� f��
����$��Z�>IV�04��<��*k�%m�����z_�'���-�3KT��D���D���|( 0�,Q�����K&�l۔��@H�C")��4�L�ض7A��p�V&j��Ώȱ�f�>P���٪��k3Qd�ÿnQduOl���l���]v��~��g���h�==cp뙉꺳Z'Eq%�@?DQZu���Lt�ݢ���2�
�i�Xu����X\=͟��2ܸ</��w�VM�XO��'*u��״Y�q�'Km�NhbA�2�R�+�hrw�r������4�S�L�4�c}eq��ўDu�,���E��,M��<��R��t�ʩr�2HM\ ��ݜ<���Ue�%%6������'맧Eޛ�P��(��F�:��Z�XL%�0=cP<c-�����F=�9zZ���j��b�]�K����_�'�
�-�@�� ���Y��66QWY��a���,;��M�����J,���`%:��х��/0�L&#�-�hj�&YB��̈́l��f<ˁȃ��Щ`0,��+���+(������W�#Nl�*�2Rb���CӒ�?كe�A����l��f>/�+��h��6�0�� ��N�ChV:a�C�}	�G�DX2�s0U��ԑS�\��b+�6�.+y���#����,SOKR��܈��٬���)6�����N(�F�ol#@ �D����le[,�Mi�J(�C�?Wy�cuʒ	�����nG���t=F�Vo���4��a����C�c��Q*��
ЀY?=mu��e��A*;Ph��d�
*S3�(�@=�{�X�K˨�=.@ׇ��G��ǖ׳��Nt}:\oa6��jhg�J�8��N&��2f3Y�>�Wy�4��`�i��Ã������@��}�L��ԙT�dv �d�L�Uq8B����ĉ�l+O'ӂ�H��:�?h�Q��z�h{��Q�pΖ�s9{~��z��
<"��YUlt΄7{�N�A�U]@�|*m�j�����@P$ZZ��������|2H�}� �!l �1��XT=�MR.>O��zV/��5�3��N>(�f��	l���l��?tIHY���VЮ�Z�D�t[;��E�3���	�F����d�-�>�M϶�:��cd��H-�_�GRąc�-�-�5��z�g�{B�,�,����+���j��c�����TUc�͉���y^8��➣Ϣ��p��Y0�}��0���h��@?�g�T#;�G�`�T� w>��y����e��Q;$�F��=�۴x?ql�R/��$�z^�5��h�
�"��g2=l��z^��{eT_oc�,�����W�(p��Y=/�j�}Rw�Cd�(1�g�SK;S�s�|�A���ݰH0�\,=+�E��G,� 63j�,;>롚�e��Xx|�D�m��P���@׳MT�1$lUߴ�M�2"#g2v&g�m��mv[>ˡ���g�.���A	z�y�ɉ�*�yS��p��)�����d+���%��^��!K�V�@L��w\6����ppBq�#��̍�'�j��@�XK�\,�+T���`�m�n��[�q�b1�Zo����8����b/]��0!���s�����Xɟ�~l m�\V��k.�&B��b�}���
&>+�N��L�d(d�;�V`�8ql)m�[d{ƖB�h�=�Lj �d��P�H^���l$c?�F�Y��OS1��gQ�O�(8�86�~�҅�P��g���ܰ�K�a�L�hj�8�`j��\�\>��}���z���^�U�ɟ��+����9��CH��^+��
^Y<8�[�F�c��y��YL�(hY��7%0qSWP����V�$�YU�X�͔��^���d��W���E�#)~B9�Ą�<��"?mVR�{h�şuT�fu��d����c+��E���Y>%�U�_370`	��]A6�������̀�ԋ&��t��E�Ez|
XF�h�]��oD�}4�O !*�S��M�S�6,`A�)�id��zaN)m����(�o&`q�]���G �T��d���#>�
|5�t3#�F�c��ퟟ��s�%�5����
���`l,xHG�me�ms��#�8r�#76�ȉ ���ƤC�������h-�w$��E�X�vU.�Te��ga1�����2�cd��.�D�+}zq/`�<
�K�:7��X愱$k���k	����2u[,�X�(q�{hv9�����V�+���NT���-]&��l:�H��\7���D^��I�a#���DNT�DE{
�ET����;NP�q��h֑L�t�=��M��lO=��<$�@[pt0��.a{�M�$bd�1:q'��@��� ��8H�:��4u���up0P�NF�L�R�@]��!�#v�ґm�n:2�Q=a#���c@�OGW�#,uÔ�9ajd$��Yɬ�6�\���L����[xuW#c�4��G�d$PS92��gh�?�s��{�m�����fp�D�ʙ��=�hLAT��Ֆ���
��8�Do��˶��x0�����yE�1�K��&.��(�)����8�Xs��d#v�B�[	{ա���:p��z��C�,�^�X�Z+~ydwylw��Ϣ�������K�5B��U�^i6��:D���e����	YW�,�V�Y�
BV/��l������ź�b	����,�^��+r�;�=��+�������#X�2	YK5���+�eUn����T����b!@G��!!���ME ~j��ʘ��R��V�M��zـ�H����V�*s�i���=�
��;r���z8�!�D�mQw��_��XV�"0@<�#���h�N�{h��,�^���W�Y8���`勐%��^7Ʋ��X3����V6�G�©���;ʖ�[�Ps�.YA�V� d��j^v�����(��vz��/�t���/A��sx�`�T3�Ȫ��n
�h1�0U^/��f�?0�M]��L]A5-��)�Q.З���X��!��W�}^���WU����[ZqA.8�`� X<�Zor0.6d�TےV1_���+��]�r�䴣��&R/�QD�#�5�ʩ�VA�q
�BVM5��xǴ������U�Tְ�̺�U�R�?��x!�-����^��k�W��.�V���)����@���H(Y�I�죱:j.P��tff�HcY�zm��,�06��#dT�/Y���S�0���ÇB�A5ԭ�_b�K��d�����*�u���Y
����	>v�Am��Hl���7�`cȒ�B�ƴx^l	�v@;b���&?b��K_4�c�p�*'1ϕ�}G,wW�
K�_��{��"n���W��u��D��~��X������r�2'�UЯy�Z��#V@�����M���5��/������6��	3��|+���ʗ�;f�k�%�D�|~m�����Y��گ���8�����EGD��t:"�9m �l���}]�w�7b�6ڭ�C"2������o��:Q�F�و�����^s�<�*�=������,8
��8���Ga��E�\��%�98�2
�w$|���)���x�w[0�;�&�P�6��w��^E#��A� bi�Mn�XϤ�mb�#�2    ������{D=���L.u�#ǖR���p+�$b��1�
b��Z�^����-q��ၼ/�`�(��w��
Pq��� �7ؿ��Z�mY�d�G��˦-�����Ni�{���K�����ϫ�gZ����EO��Ё��O�l�p�H�h}Ī��L=0k"J�\�/�`a�(��1�0�2J'�dU>=�r� 8��-��- &�c^�u-M�̏U�:����:���(U� �/+sX�Hk����JO�ന��Cc�#�i� ��u��YFJl5k[^�X^�Z^/6����Չ��O�wa��y�̍�v\v�k +�׍I=��t<�0m�;����D,zVM��z^7�R�U@�-,}^7۾��/a�0g�������9�1k�N$�I�A��cCi;�"?4_���1k��;re��Yb����Ĭ���|���x���P���
��7��[��|��Ŭ����y5�I^-�&f��\7bVA�������	1��1��ľ�C	:l�/��oZ8ћ; oeW�0��}�;+���q�$ǁB��i�islb�3h��D[>�'q90��80�����8P{¢�zd#�B���C��P�A��C=�8C��*�^]��C����`w��5t+��Ѐ�8��K��.�r�2({�_\�#D�1\q(�F@q8]H�y��q@8�=��:vOX�\"�0,�"�:���|Y��K\v�*�*f�Ӛ���b�(��V8M���'qe�jSq=f�S��1Ӆ1,��-������X̥h_�c�8K�ӛw�?=�X6�,3��8�L�X�E�cV9o�emD�YRŬp���+��l(:?Go��ᘅN"_,:��,s2e���Z�uӁ���MY/l��,pf� !N�1��q*���ĩXH]Z5�S1���u�X↥n�r��8L�Hؤ�d�tQ]+�[�#�")V� VŶMK�d��@��<b(��%f=��Y�r��Ø�Q!�m��Bz���V�f����5�*�=�al%M���n��a���m^���bI	���7�Lz�7]�3�%{�M��t���F���1��n���ٞ��d2y�[2�?I&��@���_s"iӂu�QG����!1��t@�-7y�1�ಔ�2J��Dm�سD�Ѧ����uT�I$"��c����H�}U�*�9�_��\j�%�\�ȩ�O��Ȧ}[��ORA4��o�`�e"�i߭Z4�%Ŵ�^�EJ^���}Gk
hV"�j�]�����U�D�SC����;}�
�r%"��1�=#��c��6_��P���~�)W	맷y��u�,�(��%�[Si�VvOXF��{��m�b�m�u����5h$,�X>�IT	���켓��m'�+�-�}�[*&,����j����z[��y�[,h��:p�#:rl,ƭ�{?�UB��V��=��W��V��w��m	f3',���-��I,��3ȉ����)��<�W4{:aa���g��YS�Cb{$�G���OO�xա^z�9��敼�¢G���m�8˦��VM��X�Y3����|�1�0-���Nz���Ju���B���[�'���c�˜0VI����o�"������!^K�1T����r�u���(�FzW�8��j�B�KX5�9yNy�.�̝�9�M',�2e�4�26V������.P��������BE�6�v]���EQ�j���NXo��(*�E&F²��=ꝱ,jq�"ʄE���%Q�p��5Q{,r�b7,ape�$KG�B�dj��%s�LL�,�-���1��l"�-h�)�w��t�B�Ӥ��޽�)��؂��SVE5�+�����DY�)ˢ\��)G�X�}�(�ʅEIߔE����<�b���fiQc,e��>/��\���ݠf�r�}N���s{RVJtL��-�,�2w9OY&5\6'e��@_W��ğ�Bj��AW�����`��� �����0�#&�����`3:�͖O��h�%5si��Li ��ހ�Vi v��w�od��OԣNC��ͮ�L��D����6&t`"&�./�`i(v@|���b
4�X���z�G�i(�Ѷ�M�G�e�]]n@d��m�jI��4�;Í�::SP�D��Hl��j]����E���l�V�X=5z�`Nee#��o����tO��Y륱Ѷ4����bGd{���8)q�go�U�HY��MYU�̥����� �-VX�G��5��P,�ZS��:���)�L��\aYhe̢C\��l�B�N�������(82�������4	}q`
a����_=���K�v�,^lf��wW0t��4�:������u�G_��i��Y'���`)����������l�K���@5���ӱD $f���Yۼ��L���r���IU�D�NT�D�j��d��h�hn5��e#Δ�@�L�|U����y��	�R��Qwi�A��Si�����,z٤����J�@���ޯ�*/�f��XKY�u��#�9q��X��m����+�����_Q˲�傶6�&��;ź,q/&l��*�e���C�8{On/h��e�{�I���)�d���O�+���7��f�AX�(Ko�;m��
L�U���s�#8r�3HP�C�,�A�@�UVS��$��dڦ+?ݳκ����&s+*8�,�]��A`k[�M��w��O1ݰ��ݰ��p%�,y����bRi�l�֘���e��E%���v��J�����0ߢ5��H�UQ���ЬD�����*�Ê�ȵ�&�(�k5���OO���D���RGN9rٞ�S╈�hL���)k���P0@���)6����&.]+Qj��|���:%
�n���R"�hV�[F��<����D�m���*�b��`�߁��� �U���訅�_P"���f���Dym�-��S�ׁ���$I[�[-��� �_i�Y��"��jWm��D%�����_�����-%2�=:r�#���z���V��K��J�ؾ-���%�U�k�?��-����%/�܂#���»3�c�� �H�#��1V�-�����ݦ��oJTY'4qGSwtl>щ*[��b���}Q�9�*5� ;�G�M���n�F�P�Wrc#���T�����Sjl@ߋ�]�X��W�&�*�mm��`���@�*�g�E;���M�����r�ҕ����별��+���cr��u=ȥ�`�X�}��s�R�.����[�y'ce��|o�~`+Vƪ�-�@�	ԁ�B�2�m�M�_Z��
Զ3d��	�X�}�߼Y��k�R�m=���uX��yӪ �EX�){�6	�k��9�W��d��>��$3]-�؁I�t`,��2Y�zk����F�Y0�!��-}���6�LY �P<y?r��11�gZtl�@L��Y��ɿ�K�f��FW���2�WV�<��e���!�ɬVS��
O��XG}(��2�3RJ�;e�2�C��On�7c���uy�t.3�QtƁ���n�f,��	z �t>�U���Z�.~�=���+�M��z���1������`�
��4k�i ���� Gb3]a�Eɞ���(�S�9�S� ��xY��!�P�,��1<�,����\eq0`s8^4�C(r�bhos�(�Q�GoY�7��M���I�.J"���TPt�O�v6�k�
�6��r��ϛE�� �X6%h�Ge���o|��f����r3���f:d�����s�M@�r��n�Zӏ���Z�,���1���|���Ud,�θ�(��>��y���ؠ	�+]gv��X�5~���kcYTh���X��X��|7,03;�()��Tb+k�x��@���K�T�M�1����J���&2��!��(Sj����F66Ɵ����&Bp�(�21�׼����e�#:r�#'6�����g�ȟMmQ%8cmt��i�����Q[X.a}T�5��W���ق�|��!�mdA[j�i�R�ޔ�O�I�.�    �h@�'ڶT` cW0qSWP HL�gй'��P��Ǿݢ6�Z)Q�ŦFb�X�`�Yb�xjr��k�p����@6&42��YY`o�@1�uS�M��QLf�FL�.9\��0�
0X��p��W�{Щ���1�� �ak����p�����灝?��a�����p���NX7%�F%�w�7,t�"���b^�r���t�G%V�(6DP:@-��'L0|z
�B@�&�D�=��C �0��"1��#��>�8h~��ֿ��ȉ���ĉ�h��$�,ȍ��6�3���*���J����GH�?�𙑥�=O��
w]�`~����-�5S��k�I�	��j�7X'.u�#��q�XK����\�$U)�6acy)=]���.R�ҹr�$�Rg/;�$��� �,�#ba�~Ĥ´�D��F�A!��hZ|׬�ZS�8Qᘂ'QO���FQ���A���X�Δ��*Nu�>�(�.���գ��ؒ��M[��>�ȦB�U/����E7��B�'\''(t�"(v��=D��1�X�H#��jI �HӶE�]6-j���6�vE�\�!��n��Q�D3%�ؒS>�L7y]v+�{Ya�o	l_L$ �E��h@��IB�J6�ϛ��E2��R'J9Ql"�U�\c�5R_�R�E�'H�m�&�w����[z��*E\8�t�;�E���?%Ṇ�uq|��E2%pI�G��qb#�o�O��̕�ԅ��I1��ECՀ����X�q��у��/*��XPQɈ:!G+DI`:�s,�0����Y��"֢�=����QՀˣۦ�"���Tb�\�L��DL<0��O�e�EV%�[�+�/��(�Z�D��2�i8Ƕ���-�QTUC�%����r�鋤��]�T�}�Tfq�bm��)	W��B&�d�L����{#�fAa7�����--����:|\����&��Mh0��(�ܺ�Q)����/{��D"�������?�7���\�"��뵖�О��)G.s�Du�����Ȯ���"8�c�ALT׾��i�Go2��%�m�EoF���g�h�}}tږ�>6�+�F��K=�A���� 8Ë�j*�9��nX���nX4�l�d��nƼ3�	޾� 1����/����)��@_t׾}�,v���K�e��(�o=��x$Q��:m�|~������ �a��j��L���a�������a��U��j���[oVژ,ʛ�>뮏����ʖ�:5���H�KӴ%^W��@�gZ�@$�#��#��	E������3����|S6L+�Yy�C�=�Y#��D��Ϩב�9Мf�����t�f�b�G$��������\�]9�3��Dp%`U���H!H�!�Y�E�Ǣ^Ҫ�����XXu�|G.p��g!��h [0Z,��i�Bk�v�YT����*/��,nA�Ȫ춦Fh�몏���M�Ȫ���OVS�y����l�R��%�d)�uѱ�J�NT�D�Nۇ>n��]������������h*l�
XM}�� A�g��l�u�����e*�� ،������+���M��>������G��l��x(�pjE�ք�&2!���)� 9��m)�ch69�L��~����!F�~[�X&!����d�-�r���A߮M�V�bSh�e�V�LRlm��bw���B��D��d d��!$�^�t�G�gI��m�&;!� ��2)Ef�,��Q����@6�fU��Հ�Ǟ�􁰡h�ⅱJԋV0N,�+�B9?f	7$*p�B'*r��J�n��a�ˠ�o��A\���A�\WY�$���+8�!`�[�-�V!ˠ�3����2��1gC�:Z��W�D"�#��80������k�� �-�aVA������,,�eP.���Т��,�~/ڥU�zC��w�V�_��R �/eQ���w\��R�E���,�~/�.7��#R9��+���vC�X埀��VF�^��.�.* ^7��x��'+��=�֔���=��K[.Q7O�����=5��`�â!,1�80�1Ӯ��-X{-��=��b��
>�X85���r�#��9�񛈌	]\�8�s�����]�*����?ph�(��=�s�1s'b�ȥ�$r�b'*q��6�\G��S{T�Y2�5���u�E��[�%~������M~��JVh@{Ȋ�U�m���BM>�Y68�_P�Y9�ġ��V���-�إ��ԉRNT�B�x��]������?PxW8���a�(��a�[�*:�[��J��қvhy��UUC��Z!��Q��C	O���_x�����Ydu}W0pC�9��Yo�L�q�]���@�H���&�t��)Ma�FZ���l��^��MF��[�mc��aKN$F"�0��E�+���+��kZ��Z�I����l�b��,��!+����V�z�f�9�Ť�r�[��ت�_6Xk�Ѣ�'��?ڦ��UF������i!T�,�:p�#��l�]��>9d�U�/y��M��V!�	���Cs�BVZۢ��B�[oz����+�����h�����b��Y�!Yt�b�b�5Φ)x}fw=������3H�, �7������.�O��(]��޼z_oñ��kO�c�]o�w�."�z�=���m����GQ��v��1/��6���FK[�����W��b�OO���?i^�_�/x�Jw��ǓK��zz��b{�����ws���`�j��W����������+o�z�����ۋ���w��N7����n����߽�?��@ߧ��h�t�M�����Lg��n�~xw�W���)�'�����4�|�����avr���Q��y<V|�f�g�=�X��%m曢.��=�_��C������%X��U������3�B��uc���Xn�W�*B�o�j�	�!ķ�;�8|���s�ۭ
�.���h�}f��KbE�"�V��e���|�.��nĪ���������̣�=N�P������ggg��_��	/���&0/������O�W��ޏ˫�3p6��og�~;s����n��V/w���N���YM�'�cF���8�G��d��?q`���4UU<6��kϧ��f��j$�:�����̇�w��'y�kj\������ds����rgQ$b���l[�P`�숕����T9����|��/����O+�Xz6�6QQ�;r�#:r�PΞ�����+�)�EoB��⳶F��u���盼�sz4�c�K���>{������k7y�>���㼙q��ye[v��0�Q�na4��#A�=Wi�d;)�p}������aB������I0��񿎆��6��d���6�?3l�fXI��������~ܚ�D��/�y��J��y��~���Y[��dQ]��6��� Ƥ/�~k���Ƥo�.��?�#�|�O���/�Hu��_L@�>F,�y�h��7�z�v�ȑSn8,����������ָ^�fl���#���}t���N���gg��N�	��X�?l���~�zA�� ��|���j��(�-��f�e�]z���(g_ޝ|���������H�bC�~;�����-h�&�E���p��?��g-i:��)u9���צ��؂�ǷZ,����\�4F-�������é�ez�oS�^�-�J>����h���c�������ڛ=>\ݟy�g�M~2�H6�+�&�?Lo�^_���ͻ��tlo�Ƙ���_G:�6:�18����o7�������ϓ�����!�1�X�Y�ɪ�u/E�ݖ�m+�u�k8�4ⳉs������.��v�$(�W��|p����}x���#$�>�GH�zoTfs��UU�mL0��0�߇�Ƒ��ѽl�j�a����;a�tZoK��Oo�8�H�!^��f 폨v�օ�M��?�[]���,��#>�p"SgR9������ �qe��A�g���m1�;�ӈ�E�Ƞ��#>�0����m���YY5Il�H|)�
�f%�EL�f��    P��t��� �B(�>y����X�`�{�g�b���*~�P��*�O���p�z�������z�^Mo�nO�A<>���I��\N���|����ǳp c�w�gW��xv6�]�݂C��q2�9�����p������w?��O.��l|���[s����?�<��	�0ѯ&��m�����_4=ⳣ�ov#&�0"���X�1������a(c~���J�}��%9��V���v:HVz3{��8�fw������߱����p����ף��)}�'��������p�I�
�i�	��l�>����p�n�͸�'�̂C�"���Ǉ�=Nޒ^y����b>�/uAb��|2A��T1����yE�*�F�6��ܗ�=���}�,��ؑp�G7�������1˜4�\������ۼ��=�;����'o�@�����v�>0
;Wyו�K�v��b�g-Nd�J������A�M"Ox|â�b>U�����☱3rg�,������P4o���o{����yۗ[�ye!`wǌ��䢱c>-ѥ�l<�X��_Kn���v�p�L�5ű�A>� ~��}�	��ˮ������<g�):KѦY�d�!<��3t���h��;�#d1��Hҕ^#uOa2�@<�Ktc|&������&N�&��G0�x�,�����Q�~��?y>C9�k���鑗d�[�c�����"�w��]�I��+��,�/���b>#y��ΪbA��Wcb>qd����C��'Ge�ւ�F�MNS��>YL_���wE�5��'����'�%{�����Ţ��3�)}����>[9#|N2}ͫFJ^��Kn�Z���q��N�h�Z8�S��eL�l�<������+�\�����?�z4�L|�8�>p}�t��������w�z������A�G�Ć�	��d�gy��=r�QTyi�����eQVM���	�م�<��ؖ���b�.�d��t,9_m�5>�0':'���;^8��38�� F{:]�c��5�><�b>��h�u��0����-bdc>ϸ���O�>t�dT�&s����ѱTiQ�$��ĵY4��.%|f���YDk۴۬��&��ǃ�>��� ��lux��^�*�?,�*�
W8��([�тy	�It�s`�'�y���p����������j5�����Yf�@� ��A�Y�gW�¤�ËG"	���,e�M&�ЈGw��}Hu4���#r�5:᳅Q$�'�h��:�wK�|��������<uJ��J] ����/V�~{[,��SmT�:�y'�m�A�#����a�G$R�GR��N}��]gey,՟�s���+�WH�Go�u�n���P���Z�,� �f_�Q$,�/����둰
o���n��n�t�ƃ��ŝ�ϏR��[�`�t�t�$?�Ū&X�����MXk7�����������NL߇��ͺ��kIXX�ϫ���c��I4( ;�,��H�I�����i;iK�NT�D���'s6��`ˠ�m㚰V~Q�FVs�t��Q�B�W���"d*a��v�;��gxy�$���Յ�K�#P������Ď�XT�j��|3�,\�.a}Z/��fFcQ)#�/,�M	+�nh⎦���>��(G3y��jP�H'˗]���I�}�m�ϟl�xV�Cu�eM$ɐ��uG�q��.�����R�V/	��[h\&NX��;���I"!�m_-'`�ބum0sY�v�^���m&^V��/���t�k��y��h#�8��r����6H��gBA@����@'�i�Aa3?���� cNJ�Q�V�o�e����R�k��3�0��<,�8�D�d�LF#g���^�#�>��g��"��M_yg�ho�D���]{gߧ���WGa6I�<�D�����ʆ�c��B�h2Q��1�S�^y�n�M��j{�J�eS�+wc:Y��T�X���~��}FQE�ι �ȑ��đ�L�|�Y����)�H��gQ`m��Ϲ#�c����tN�MY��3Sx�N'��X\N*A�Eeǔ�Y�\��f�L�,6����M��u�����{��^`!��d_z�>��^Y^��]ϒ�C����)K���+U�,����pqg�����Q?(���<_&e�����Ԣ�M�
����N������J<���[����:�C�F���;�����Ct8��8������h�o���<�7�>ۘ?kɳ����ya�c=e%��oiw���Ƽ���0|g!���i�@?�Pt]��>j�g�+�OA.�u�[��h�{�R�59Q���1~�EK%6�6n����"��.)��ڡ(�r�@s��M?��KK��M�x���E���<����z��<��?�1��3��bC�<۷Œ3w��T������5�H,�f�A>� �>�g�-�i�x���i�9��ԼF���R�ʷeݯ���)k��)������ZH��	,5_��T�E�v^�4���1rK%��fN��+�K����^�-�H��fg���I��yqst9������.�����RV�o�.n��9N��?�����q�y���F)�κ���\�&(^�b҇�2^�����8VQ�=�o���I�:���w�r�i�Nu�8Ċ7��X_�6u��KS�4�9�F;n�%�{A����CY���sH2�Y�~,j�&g����?�廿=<��%�Q%
���W����_5��,���~Q�� \�9eU�l��J��"EKֿ/��U�x�,zӞl����-<��auk>�JBg��.�zQ�7Vj~����:��������������~t �O�6/�#/C�n����t*y���;��It�����.݁�J���x�;��c=�j�%�m~1B<����?
3}v���26��|jZ���u��mtV������E�Y�{K�Ƀ�E)��!���%��೏�Z�j�O�CE0%Aݺ�>�*��i��O�����˼ե�,$}�
�cY�O�¤K��lû���zd���f����I�7��U�G�])V⃉gݪ��粹��(��9qn��h������D�A�؟/Kl�.��l��A��X�v?�-�N�������̻y��EΦ7RcH��e��W߯N�㳋�۫��`�0X��/ӛ���]��o�*���Sn�c5��'�q�Ϧ��.�k5{}_�ď���ۯ��ݷ};{�P�4R�����ՀA4rGcw4qG9�jg�g�6�I�^N�^�4'?��{�o774��- �=��u^�1�S�߯'�I�����ھm�md���|E=M8B��y�HH��[����1/�K8�m����4�0_1�12_2U��m�9�
����сU ��d��\y��F�[r S�A(ﻸ.2N�'Ap@�� ���b8.�l~1���G�(�rv�g�u��X�]��U����t�]H�g!� d��hN,�:�����q��/bGۀ�J���/�VI�K۫Ĥ������k��t����8]O�)9�.Ah@��o��J�"�@S����ÿ�� $|��Q��"o6q����p܀SZO���3�l��$t:��N(�갦�f�����OOYR��F�V���=�{�J�2��!��=��1���M�u�D����[����_4*�?,��uu*�պ~�h��D����fk���Ļ�=�
�Y?���k'��Q�c	>b��#����m]mĥ�S�vEOX]�3f��%�\6�@�����\��ʓn���4��&�~��4��Hԁ�E��$6^��WU+l����|U��fz�xl�����������z&�^8¨Ш�=����(ׯ�Uے�7���O���o=����H��G��$�U�(��z$-"P��[�.�Od�zn�L|*�/J�Iei̚}k�����/_�J�L�l�p�Y�D�g�vUm644���j�~Դ*u:��N(T4T���E�[����t��*U��]k�BW ��+ϳB,��jS��T�,z~6�mS����rA��(���7�V�%+�P�Jع��ߓq�t�J�k��K?S�ٌ�ة�7[��oUk�0��1���C��PtwOza��]pS���'�    ���nn�-�Sp�˻�0=}�j��00�n�\��V���r�գJ@�s��}�:��8Sj��w@vA�������Wi-�B�Nv�'��q�'��q�|[|��{�A����!�`�D�OmdAR������0M���ve�%��ގ�(q�9�x���(�x����8�*���gaإ`�=�	��:�_X��b��7��x��̶�%n�[�)mkT����n���5R�0��k�\f-c��yj1��w�tT�ض<��X:5�8$�~/��K}�I�&s'i�Ҿ<��� ��l��O~h����-���Q�����܎8�#��C��Ӳ����2Y=-�$��[6�#U�RӲ5����Yq���|��v�QQ)\�OѲ��!ۮ����U���"�M����C� ��E�.<�&�>{vzA*�׳~v��q%p� I�����|�%@�F*eq(fyq����&&'���M>`���%��#�ʇ�R0����q��88Z�B/	wo��Xε1O��k��i��Lfe����x/����I#�7�����c���� ��M��5��]^�R,��k%T�p��-�v��!Q�Gj�x��r��	דW;��7tV�z�|^��|��lr6��m�;-�I�oǾ�:H�v��QӼ�+�GU�����*4-�5Pݰ�HTʧR��s.G&�=y{y��-KAz��V�)Xo]"k�
�?5wiv�}�8ǀ�c��1���%.�Ż��m�R��r~c�q�����t�^�[N/"�-'���r@o��9�-y���U��<���.����X3`����RyizQ�&\�l
b�t���`ў�r<	��)���&�'.ҙZ�ݮ׻�$$F6~;N�k�%�v����d�Ξ���<�I�)\o��	�sHZ<6�%�l(U�tO�k&Q���q�_"�CI`�w&]�?m���7ڝOw��Pk	.5K�b�Ʀ㇄�y.�/U[�~[q��D"3��_��J���'��Kӈ�^O��l�!��3$��v �z=1���.�����ǹ�S��A[\�Z^�?��Q��}��ғ�ף]?~*�}���C'%����k��'�?0X����xx�B�����)���i�@��/�H�L������	:`B��� 0}bqh��VN�[`Q+�k��c����;�ף<�h�M�^ڋ��9 fN%-�Mi�A���`о��U�/6'x��dM�&�()�NLU�3%LpA�~���f���c�u��qz�nDn�w��(�(]��&�)�����ho�؄P����_����7'�#��{�(�(9֯%bv�e��O���5	W��u�F%�ݓ����i;�Z���b�	����.r:�P�e���?X���B���	6��5�\���Ll�|�P.�^7\3f	a%	��<�*�����F���
-��B���-���\`\gW��s���l�_\�uE7@�PO��7�j+=��k�-q?b�#�?b� V��}�9U�Ŋ�ӖH[(m/{36�R4��Ղ�i8N� l�Z���u�0�ހ $���v���iIY�� jݴ8|�F���A̂� ;���*eK�?/�
���. �p�?D��|(9�Tw�.��/M�zi�iMbH�}�"dm_���D�8=G�Tb8�u���ϻz.�R*	3��oT"��>دN��K���4#�Ѭ����m��_n�m���ݠIwh�ꂱՒW�i�X����~U���/vнl֔ 4 ��Yk��xG�dݱ@bCj2��\~���$ǆ�}�^������/˖+⑈�ק���D����UK�����v�yq�5J؉��\P�WU��.�V����]�4_�7��%�xXZk�"U����ҩJ��D��yV�M�lRx<qA��K����O %�e�����<�-<4�3�ْ�}�#�6R�a#�6�J��>�%�G(^�Z	���89{�4�	�<��ؤ}:���������O#�I+���<��wOD6���/�`���S��vc��BQ��/�`��EqqG\�g�iۊ��\�������=�F��DL"L�j�{Yt��i��	1�����]p�򬩖bQ�~�+��qA?�/�W��$�|~e��$ܳ��v�#�I"�#�tW�{XO���FU^r�j$�0q}�6z�8u�B�����Š��ci�_O ���ZnL�*��]pУ�l[�G�\ �U��I�\��=*[v��y�zx>���"���y�T�Tn~�mK��C!k�68��m^���y%��[ZИ.�[�mI�\w>!q0�v���}�-�M�ظ�4��ͩ���"+�48�}n9VӍ�i����@� ���]�O��$������G$)���ec����;ࢎ��#�Ƚ�Ҷۚ��M��s�r/͖D��A[�C��X�vc��+���t�`K�wH���Y��w��0���a�&���m�n~�p=��A{"_.UջQ������ ��ĩ8�ΐC`#N��q.���wv�r"9pr8=R�+��<9sH�Y�$�*�=��Q�%r H�W�j/��iv9QL�_������t��k������z �bᙴ{�����|NXb㟰��|2��MwD�xAD�lK�j�YվlɃ�x��7��=�� ��Q�9��r�����q���=n<�����0pR�̸�H�i�9�y	ӳ�&�ݩb��'��b}��t\D�����B�|şr0�7W�j��]�n`ק�3���d��r�ߗw� �n]k�/�)�q[��Z9%d���n��,��IY\�����>��������$���C�@�_�.T��@�X�V���9Q	�a8��Y�2pvع�&w��L���Hb�},E�#Z���a��ϰ����Z�~O���l�]�ok���:t�>֫�}4=��ӽ�7|�8#�����1.�7U��b�W���"���Sq��Tb�ٌέ�@d�ݞ��Ɠ���*��~RV��ZT��h$�X�O��������ʬ-\jϨE?[�x;�����X)�H(���sE��zF)�j�G��T/0��Ki���`*���6��T�w�5�/4<�V�U����wt�E�+	C"���5*�
;��~A�*fզmط����p�j�Q�.����������u�>�]p��Ҹ�-�=��1�ܓ�����`�'�3T�0�-ӗ;����$��-������G�J�.�V.�+O+aQ7X��t��7c��=��.���VY��I�*h��-�l�y�V�v}��U!�L*b֟��:��07��Ǘz�p��u%
������1B�U�%7F�ߕ��/m���<�Q�7H͍��Ʀx�4�C�����J� �w}n��'I>�ev'βQ.�E6���0J��=��2�"����ø3��mkSꁁ?+�����	�&b����I"9E_C��#�2jxAZ퉑�ʥs<�&��bJ�x�ֳ�Y^@�״�`h�)-���B�k���<���\�`��P�L�"�/�-o���|�`ԧ���zL�I���w��`FTO��W�(W��0=����|:�2I��<�:��[�Ib�4ɥ���Sk�o�q-ا�����z2�S�~�>��E>��)��	���:S�&��|z�Ûi�Mx|�֛�ru_t�b6�Gb����e._^��/�ѕX��YL~&�� �?z��_1h���i�R��_�� �V�1m�%D����o���8��"��H�N2HY�i�I��w�3�|꠪H�aI�j$�?x���nH]|Rh����&qi7��8wgY�m�����@*[���A���t����g%�}�^i��>i�����ǚ��d���Ik�H]���Gh� ۳�,�Sʮ����P!qnG�����G����Ҭ%�U����a~N����䜈�a+��<+��ƨ�d��q^�� 1��U���?�)�Da^H�3_R�6_��OV\]��|�@Zt$&�	;`��������6
~|���[�#aF���b���P[s��2\I!L�"`L�|!�Q��ސ�T>��b$    �*3��>��vU�j���~)[���\&�^�s��km.��Q��(tzz�5q�~F����yG��lf��|�T0�vL[�����9�B�+�|���|�%�#,t`��!D#%z������tz"2Iُ���QW�`�v��#W�8���>�m��ױ�S�V9����F~��C�yPr��z-H^We�r��k6���8`V=6��7�|�~��������n0��R:���/�}�XE!]aM�zҧJ��T��m�Z7/��H���<M雬��f�b������@�F\,KR�ˏ�F(���>8p��&���)	�� ɜ��rjZ�-�b�n�Ƒ�d��c�VViB텨mqAG\�wpl�SVH',�F���}�f�~jt]�-��-i��~Ϥ�Ip��ck���|�t,�O��I} }�m�����H�KJ�O�yK�ʶFŝPI'қj�#'�euR "��Dg�2�����`>V;x 6���f\Q�F�3U��pNA��M7�E�1)����7[�/
��zJ�t8����"��9dϗ%��n6m��q�j�H�!���Z�"Xek�d{������W�@0O_q��;Q ��\	��h������ٽ}h�M"M2:6V/fγ���+0�Ѱ���M���~�H˓;M�˗��xSb���6T�"��D<#]*������+���kDsW�88|p�9�^��$�'�N��P���v�Θ�1b��\�r`h�a7r�E��lAF���r�� ��
����Z��Ӈg�d=�뉘��`��ؠ�q��+�l�W ����v�m���Sa�������ѡZ50��npx*:���C��?D|�z.^U���EA{O�o�l2��K��I�* ~Y��T���sE�H��o��yǇ�N���˝sā#n����sA�c%��-��{K�8��B �|v�a�.EKb�v��q9��w|E�> HUV�)�M�w$0Fz� ۏ�]X���
�.�q�����h�U�qr�����X톋�[�z��'�]��R�e�K�������L������ ���ܼH���{�`�o��F5��W��S#�7���3�;
���p%�Ջ�@�H#��Yf4�_��s�l�.M#��+��1���U���&�����)���S�0�-~�ϟ�1h�����c'�$z������F1�f�0��|6�߉�רV��c��1��p�;��H�E6҃`��V*TΗإ����/�7p\��Q6��L�'dR]��.[�k%�K�`�o�n�Ϸ���MD�H���c#}E��(����uQd��# _��='�(
���Òn����������|߱���/�j�ʟ���u߽쉕�]� r��JO�Kpם8酮��?LU�ԲvC��?���o
���	y,rX�\�+���'ӠO_�n�XH~� ӻ�����qp�8	��O�|��T*V�`��uhl
K�io���y���3�����u�ݫj�|� �m �����`���דx��S�H#���)�W��>��>��>2$Ȓ�$��W_�v������\m�ٯ���H����l�Iو�3,��nn!(�l#'���7�뤽��R����Rz��J�����/_�wY#bkDb�0�㪲�l�����Ͷ7��H����ɿ�\��Hy�O+��>s��+TL��쏔~�¾<��:1tN�)�������y��z!_�?���ސ�=�g��H[X$.�3���8�#��Vd�Ŵ�jB�x-Q�aq�MXX���o���*4������`zKB!�<̯����B�ȋZ�^Z�Cp�+~�Zm����<�Vז�E��,L��y���\�,�}we�f�~��}<�:#��Ƞ3a�"+��Xd\H!4���C���kM6�
C#t����t������|i�J,�4倠���5d�lp�|�.Ɏ>!(�Y��3C��r]�m��&����J�.�ܞ�$Rw F]�qW��"�h >iC`T?<���C�j��ŧ~�V�V	�M_Ѳ]�Y�!�ߙ4�ޠ��5�H��>K�B�|c��0��:9��A�ζ��Z���c��l3a�$�vZL��r8_�8�4�w��D1�_����<g�&C�m~��^�X#R[Dr���|,14��~h��AF��(��dX�Aͩj��hzC~��0�5&Ӆ�佡���KR�0LLk��u'��ш���|Gv��3ُn>���\9��⭕�E@3���u��q{I�o���b��{S���7�Z��d>�\~�N�&N�3I\��sZ�N�.��R޷�/��$c�WE����h��)�-t8],`��Ţ�IJ�0�
��a�\�%����E1�q{a�U��Z�F�\�\��[����PD��a����(�,n����y�̜H*�v�x'Hu��8R!12�:��B��o�^h��~��i�ܸ32�L�"A�Λ��YkK�Z|�j�ժ|m���y����(NdT�^�⿋lX��i��ya���qe���fNR�/29̴��U .��|e2A9�ޫ�f����������`^7���]lվ�a�h�Yv->�~1�䜃���',/�C�l�����,���|<�(ݦ9��E�tw���6��X�$��(x�d/Yi�|�M�^WK�*��ꡮVS&h�����\���?�QO�$���������MQU�x��Ht�����Z����dw:�\�j��0艈�C��
���gU��J��p����p�޼��nO�N�v)��g�SU�ļ*������E���"{}]VZ޷H܉@�q�lK�^�! ��*^�e�$�rɃ2����R"$*ꄊ;��N(�_�Jn/0�(�(�G�Z�v�b����?!y��G�������v�~f�v"�ȳ�+͊L�p1#/�!w��"������\������]����"Y�oqF�Hp���+A�@�N�.����]�� ��G�E�g�I�19{=Z�����3�K��-2r����/��.�.1O�kF a�5L^�?�dHZ�`N��>w9���L�IG�9���&\�������ˑC?d/�.��.��.G3?R!�y���H:3ϸi��d�:�9�ɦA1��{9h�� c#\p�ɱ� ��>���3n��&��<���#9L�l�"1�>;Ѹs{_~�L��Mk��}���3rڀD�θ3vM�|J^�r�M�#�)�um���"�����@�����l��G�n��U�����|mr��"�+�<�D�{�Ф��q218��e����L/d���q���0\hl�wnV+Ѽ�A�����u����C�`�W��9	֓��Zo4�N�1H�.��+0�
�������F	&�x��p*.�+�э�w��b��ն𒨱Q0�򥶩���X�j�*A1^V��N�_r���b�&�$䠙����qD��'�f��Hl�% d�9�� ���lҿ��$q"&�͐<�A�e�a1����8ˋ��r�~g�| ����m2�%@o��<�q������ѯ�1��`߽8�L��m@5X*��S��XA{���u��e�C�5�P����m6��t{�T���$Q�U�|>�|�k:+T��n�
��M,`P�ܣ��"���mM/��ȇŘ�.���3Q�&���<���tc'd�vFF{+N�VP���*�伭�7q=��x�F�:�Y)�<r���	�`-]��:���Ւ��{b���0���Եy���7��\���׫R<;�"|x;N�c�� 9v =�"?�O,�� WuJ}��_�����۵�Z5��>���bP�h�{bc3E޽Sq��~ 9$)䳞�Oc�x�?���'�o�&q�n7�M�
[m�,n!w6�l��W��CB�2��	�vF��QU�q�!�c�*�����+aM��P��k��B[�[�])ۑ���hV��j���*ޯm�4NЫW����ݓ��F�M��YI�n��J�$����_��&,$�ʺ�������Oj���:#���R�mj���n+>U q�R��j�XCC.-���5Y��)W\c�z�    �T�5_R�x�x���$Ot���A��V}�ĭj��'�'`�UO�u]Z%:'�)Fw?��.��oV����_��#�u��#.ꈋ;��ƍz�rK�D$ �ͦT�)��CMN&���l2xI��ӊ3ݮmV
H�E���LxL@Fg��l!��k�Ŵ%�1�.fd�^&z��#����$�Nv;g�����9Q�oȋ�&F���v�!k���L@7��M=1���eW�ͬ�|6J�4����y.T�^�0�(�6���ȺO��'��w�pѦ}�Ţ%}Y�x��D�.��x��R��D!s���g\�w�:hz��>]��dPϹ��O�Q�؁��<#'�����. =��P���z-���]�K$�Y[���i��t_�ŏ�j����]��H@R_�K.c?/��<Փ����0����T�2��Ls��IX��|U�u^��ߛ]�ॕu:����Ģ�8?�_�=ڔ%Q�v�E�`�&z�]�$�(b<IӞ7棽B�i�z6	�}!'4Hh�XY�à��(�Y��$6i��Ϋ�<+�~��a��	3��Tr3��>b�?)n-�$;�k\��vÁu�m[����Ն�9'��<U�$��_}R/�	^
*���e��ۚE� ������<H��]i ��j���$�4�.W��[G���YS�4�����:ⰍlW�7��T�M��Vmx9�\�*����$&z?.�sK��ݴ��l���|Y�>���	X�����V��ӳ~��>,u���z#�-��}�w
��7c<���s�)cKL�v��ɣh���(�*U.�s]��9Kw�Ջ��ٖ���K-��4���դ�i�Ԭř��$� ����	�aP,�"#��R��3��%KR�ñ"{��M������+��:݃�y�J�� ��Qq'T�	:�j�U%֯�-eL�C�7��I�v�"ا)���ܛ���D(�Y|r<�z�γ}�胡>9.;��EǑS��f�Q�F� ���~hV������]|�A�u�
�����-�RK �^�kx�E+�&�����}�e �G^$���j�"��ݾ4WK�=����"�9#��綖�e}���AGFf䋿?��Rd�n)�ݝ���ԏ䃚�bX������RP���~gd�v��T�k��M
v�n*�y>y�ӕ��C�{'������L��\����h��y�/�E&�,s��`O�'�´�S=!��#w)q�eD#����(�</b~u�{<��v�`����'z=ۮ��D,�,��O�����;�����Q��o��1���ɗ��w8�W1e��c����I����Ҵw=�~�rM�j`��C�� �U�l�ÉZ�n�S����"[���\>^��IM��<�1v�8��/�ѕ�a���R�Y��F��&Ʊ��SՀr��ʧ�݀,����t�G��p��r��Q%�ٖ��"����T��~6qS�;�vʍ�C�n��+� ��)˥4�,ʽR�ٙt��YO���Z)MZE;�~[�\n\
F�t�?�wbKE�R-���Q^'��	�"��z4[d�\}+I�$1A���s����� ŭ'��a�yR#!��"�f���6��bؿ�_\fcj�{�	�=�n�w;�O[�����=X����V����z���Q���l�sQ�4>h���Ɖ>hlZ�r2Hs�f8����I{>��J_��/_ؽL��(�kTő.u@��ٶ}��ٛ�$�= ��Vw�'��a
ꬕH����ԍ:]�����|�g���'a��LM��;`����{v=Ό�V-����v,9y\y���0obܓ�9@����l!�.�����rr~)�l2�_�Q�P���@��I���B��d��^6M��;	�M��ޮ���9�1��w��YVC�My~K�y��w����
hv��j��o�:���v�x0��r8%&8�P'��9&y*�l괖H��"?0/(!u��w(�^�ߠ.u~Ih������@NN?O-���������S�tVw׳������ӟל�N���\���`��P̧�1�����t�]��0S[����[3��l4��h3&_��"ʾ�x3�7d��\f �Ϯ�����jw���!?0y��|$\�<N3���br�0����<���|8�dr΋ �^�	ȗn~7�u��|���b����?n]���/w1�8�j7ȧ`_dt� �$�����jc<屜	���K;��F�#yy$-r����j������lە���ý{x@dZP�r��c��;bڿ,r�腬}2gr�O��<� T��L^���KUН�R�"J�_�ɝt�;�Sd,	�qv+m7"r	5bK�{k��	�*�B.+-�!<�!�E���Y1��2���z:}���˪3W�%���Ǐe�+��,"�<X���W�u��>+�� �'wq�8�՞�';�L\f�&��{���.�(2NX�Ϩ�@*���i�9�N/�NK������ �;!��Ƞ3�贘�5�6V��=^+���'"��V>>��r���*��O��UJٍ��E!����l�n{�MA�_��7���H��
pqX=���#��ܸ� ����Vځ�8��]���z����);s���g��s\�9F�cD0F�c ��Ͳ�L�\R���&�S>��Jg�.�d���I��b8��c��:�Y<��O��/�X8��mh��;L�����j�ѧ�L�B����,d�R:�/?�u�� ����� ��S#�5- ��gs�a��d!���p����{��(tz����ލӳ��Wǆ�,���p~%͑�Ϝpm\� ���lU�h�W�f����_�^
�~�6m��h����6��kE"]��ƜW��^9�����8NB�1���2�����$����-U����K�4#��LH����^@�O�^E��;
���7y�s�E2���|�5���%Ԥ7��K�k$WFBN2#|R�V�}٪	�G6u{J뵧
w8����\�I��{{?��hX���@T~�}[oJ��I�N�Ә�~��|R�N~Z7q����/7��b4�������2쌌:#Q�-�!w���Ɩ��b:�UŶ\ ����R�hP��"ݩ��X�=�����2SW�M�*W}.������ZSCݙ$�\0��|���\�B�3��F���M��E����\�����.��>B]�������/���&a��Ð�~|���C�G�KE6��N/�2]%���o���e���K��?9��>��p ���|/�}Sl���~�J�D1鍻��A��ҡ.����x9������[!�Wy�ݒ@�#4�"	{�ۋ�e&��tN�������
�-����t���@d<K�k�	J����kz ���� ��)?.U��r�~�R���1Lm8��&���6��k����n�ICT\��z��pq㞓ظ ��IҺe1����[����\|	E^�h��3yӈ���t�]�ю�6iKʕ%- ��˽7d�X����|4ύ����MS���ڹ ' ��]P��Bn��
�x��jki��U����Y��F��U�6j��(TL�y1�+zd�NX���4|��s�r:��Qta�[t�I���B�)��V�.�K:�p����*��i����K]�ݢU��Lͫ�Dx�! V~���d��6��/�/�]w���x��p�d����z�����J�h����j;�K� ���2��T/��7���0���6bZn^����<s�t�;xB��cO�1{dV�k4`:�Lw�{$���m+-����OMѽR�U;6}OX�=L{%���@`=��!�H
,��=��\7�v|��+A����Y�(�r��ү���j���NFi\P�?%�������r���>����d��4PG)W�!�ޑx�H|p$^O���Rxy{���e{����!�0��x*�?�ݏ�+n8�Z�c@�N:mӼ��!F�h�)WO81H�k�6e+<�L����C�X���O�z � 	  'C��
�Z�]�C�Eh�G%��J�~b�U3y}����*ꄊ;�vʚ�}�j#?IIz�Q��/���y?��Tɂ1��?���Is\k�^~�I��m��KN;�꟥=�K`�xl�����gE>�O��C��~�9 ��-q���
�� t�E��C���p'a��ɩ�O���l�������_��dZ�
���H�����p���#�� ޯ�lr��/d����}dE#�k*�wr���r=�M�n��$'��$dʄ��fp��v��\�&^z$W�E<�T���+��MV\����!�n��'_k������S��;�ޓ�Jl�F����A�����A�{Vm[>4�M)n˶��V|�˗�����iUo	��S�PD��Me����D�)W҃�]���K�g���+�*[�{�Q'Y��z���0r=1x[�/Һ���S/<��ե��	\��*Tn6���6
�g���*D�4���X��S����*섊:�@�5J+�Y�ZnB�b93�?����d��p�/�#�}���J��W+k��^ϕ0�e��ܐ;JV۬����~�Zt��@=K�F!��聐W
Z�-&r}&3$����b�T)8��m�H<����q�Ѹ�f\������9��2��q�h��RL_��%NG�qO�AWn�F���^/����NLGÛ\��ͮ.��Y��,��!~�k�(�\�	4)r�Ziߑ�!Iqz������U�b��#��1�l89�J�����$7
۸A�!��/�G;�K�B����L��\���$ƈ�o�7J6L"@���a�<4��'!��U[?����a��^��¬$��S��h��V3M��=Z�$�����7�C�t�E�Pa'T�x����ؾ��q�n8A�@�GZ*�������R|���j��CH�+����!�tYZ�t,��^?P��`�F.��=��5����>���'�S���͌�N}�0���|�0��ӟN����i�V~�Ҵ�'%��YN��J�����{W�S��Zg�����y�ۇ�u]��H82�K�T�M{��ے֕��w�ͽ�����!�f�z0��N�m��� �� ݮ��IEP�����WauG��v���r&����ix�F9L�Z��?X�=�	J���e�^�����W�o�io�(��$�}���W8��A���&��,��-�,����C�j� �c�ɉ�^ϋ(&]W,z���?�3.��?�ގ�'۩��p�w�x�WF�������gJ��}��٫t>���"K`p�Խ�lI���B�G�ae7*j�3��D���R~ ���_�vë�� �U�=h~�	M0fU*	^A��8E�}��-����N/�� %S��^5�0H��E�$��д�u�n"��|4%q����jw����]P�����1�����Z.O�)"�W�Cѧ�w�@�G�~�@F��r���"�����bE��S��r�mu�+����y�7��<i`��W1SɭJ��9��7��Kv��?/��I.�&C-�?�g1����\��74�l����͚��w��:�ĉn��!�4D�Q#xG��=N�U�}//k��z=�_3aA��S�N��aG%�G?�>>U��aY�l찶���\���O�
�"B�0����-v��t�������\}���������_�_U}�n�.�ߋɴ.d�j�<��<�w/��dAj���;!��Ȩ3�m%�2�>�\X�/�/�Ҙ��	��}
l��$�i��'��d��������br=&��v�ܝ�^.N.>�;�v-'�k��
��,+��p�����fr��[Nw*i� z�EA�l�m�*W�}�zʹ����-aq7X��v����P��@N���/�#� �#�Z�J��@�F{D�X�j�$�ط*�Y� ����W��:�� �`[�	~�H,sV�4彽��Ol/�t@�w���������ù|� ��o�/����?ϋ��=S�����9�{$���Ο���J�D�K����Ð�\��4~��#�ˡ�Gz��/��K���T��Jr."���?z���£G���f�'���SU���F��̿.T�4-o饤�J�� ���R��� ������������Ȉ5��Vi+��g�h!jJ�5p���A.T����]AI��� f,�
��S,l�C�J� ���\o��~ ��
[#k��E$߸*���NB�ܓ���E���_O�ڬ�A�CҀ�J�.���x�^��<�vS��ui	�0D�]5ef����F��n�J��2��-հ���o��o�iL�9      V   7   x�3�4�tI-�L�SIM���!Cad`d�k`D
V`�m` ������ �Y      X   u   x�3�4�t,���KD�b��D�����i�q���%f��$cT\��ZR�F�l�)PYHb^YfE�I� 7e��&���c�eT�����B�f�1W� Ufk      Z   P   x�3�t�/*�/J,I���4�4202�50"+0�60 ���qqzd�g�'V�֘3813'�H�-1�$��-1z\\\ Ɔ �      C   1   x�34�4�4�4�4�4202�50"CS+##+Cs=Ks�?�=... �t;      E   /   x�3�4�4B#N##c]# R04�2��2��317������� ��      \   r  x��S�n�0<���bC^Mo�CU�>�"���j�!�~}��`�*�b��xg�cfdJn�*z��N%u)*����9؝�T�
��g��H�fwd)R������TdG8~�'o��UQ��^���
���T �&-����!tXY�1��Lim�y�HK�:�����h�w�u�1��Fy{7G4��V�N��<����M�񹗝����v�"�)���>�O�4����a�
�0=��4ͷR�JEZۨ9��\kB�>tA.��?��� 0�����KVFh�;o�U��Ě2��KT�����`�H����L��u�v����H%}.cY6�]��z��RF21h$��h3��n2�Yl|�� �      ]   �   x�%��
�@ E��~L4�^N>�!G��6*C�2i��7��s�
��
;BIM�)��p���V���%dg�5dz�����+�PCc�r͍���ɏ�B���C��7��XdaJ1GR���L!��ϔur���j� �y����~-+      _      x������ � �      a   :   x�3�t*-��K-.V�I���!C(��JL��������!)X�P�+F��� ��      c   +   x�3�tL����4���4202�50"+0
s��qqq ��      d      x������ � �      g   W   x�3�LL��̃��F�&�f�C�^r~.T"K�8��A�������!)X�P�˘�(1+3	JR�@#����D(Ic���� U�=�      i      x������ � �      G   N   x��̹�@C�X���v-[�$�;���g�r$�o���Ͳy�������SI��E�@�"AT�SE��A2      l   N   x�3�4��sw���%�i`X�3202�50�5�T00�#�?l�\F@��A��~P�Lc��ʝ|B]!����qqq B;+;      m   E   x�3�(��+�4�4202�50"+0��".cN�Ģ�҂L�ʌ8]s���3SR�*�*����� x��      �      x������ � �      �      x������ � �      o   p   x�3�t��	��%��F�ƺ�F���
V`��M�˜�?��5(F��݌��5��?�O��?2�C�Q�p����H4̄3����/F��ݘ�����`ϓ1z\\\ I�]�      p   >   x�3���K�L�,�4�4202�50"+0��".cN������8�32������ ��b      r      x��]ْ�8־v?WT�D׏V��0�m*10,�vż�s��Aʞ8(���Έ/�tt��a���V͘<�_�M���%S׏	�N�OH�C�RL�B�/T$)�A��}'8��W���ϋ���1|z�vL�G9LIU�s�$���w�SfA�E������;C���O��^��ZJ��M��� �1��1��ǺJ���Y�k��ah������W(zB)��X�W�N�0��Ԋ��Ė���U+&;!
�,<̏����/�T��`�Tc���
�wM���l��x�����櫀��n� ^��)�|���^�]2�<�Ac���½l�%��p��ͺ�ɷ����!�{`�~pyn�d�k�����$���ޖ,ݖ��!��:�u2�ݔ����CɈ��ƪ�F+bXN���eF�D�T�	#*��x�$���f�0�ɵ%XX�Z���9w���V�7G�p��
ȕ%�z��=���a|����ӧ& M@^�X��򲮳�'��I������b��J��.][?J�c�Փ������7�"`F�q؜��tq�s��pȑ��{\��<E9�������Ha��j���7�!~(	ͭ�*���� ���0գ��E۾*o���,�NOSr��ϑ�穧��/�����BT�H��i�3���ϙ��XZ����=����۟�������yn�*!ӕ���
����wT`��q��s+.��v���"S�$?���s�`ZW:��EH�ܻ�0�L�����	�rW�V�^���Q�ײ�<Ec�`���!�{+�/��ڶ�r�1G�WE�x��\���?��?;��Pa���A� �,�l?��7�
�HJ��͓��2�
�G]C�.��Gw����gX�d �&����EۈW�Q:_��n��4H��iHiQ��"i�*�l�߿;s��-,>K�D�ʠ���X4���m �<���8_ � ��8L����n71��ϧ0B���j��}y�Y>J�8 ��+�
�ڤ�kC-,�].��E}�{yʤ�w��"c�CC���.�с2z��t>���*/�3֘��Վ�盇��G�抦�cp��c~6H��$����P��KA0`
�0B�o4��hF=�E���F.���L�1?�������:��S���	/À)��=ln��Nzb��d0��H������w:�}��}|�������c�/s{+!���%U	�o�>;x����;)Q#r/�����K��3���C��K]Z3?Q�a�(��XF���W5p}(�aN���J�C��\��ȖJ�܀�+�n��/��T��-�K�h8���H�͑�+���+��Ʋn�!�p���tß+bv�@�G�Z�{��ߐ����k{�2��e�{MݹnDr.o�딝0LR�5Q�r�;6�к����|��R��j��$��{v�J4�Z��|�P�g������|�R�W2H�q0�+����GS]���)j��z[O�[_`�Gq6�B7����u��-�`0!�lq�	��S�-7��8I5�\�r��ԁ��B$y!�y�R5�&���U��F��1^�ۣx�_�`rG�e��&Ms[W�*�Y�V����ۑ1�ɥ�����8�`ű��߭�)eF��4��M9�6��(y~:!���H@|�T蓬�s���
L���粑��+�2�2��1Ǽ�w��� ��PZ�`������X^%��i4` g��K_�8.�qq(���ZJ���E�4ml���QHڪU�<U�(,mӦnQHڔݥ�G!i�u��>���V�!.�Z�`�j��5�#�ZO�nxUVw�z�ʃ�a>�@?4[=M)�KJ�0���|����P.�M�ύ�<�씃<M&���!��R:uU�84����sC����T���[��m��n���Q$Yb:9�-md�ν��B�y�)i���R�k�[���oݳ5�3��3yh��iѮ͒.8�E�M]���&��)�T��O!���ZhR��a��!�&�QuC9տD�עo���B��:%��i4�q�X�AUb(2~̷��bhkꋼ�׫Qxx�k�<پnd�e�&�Lgn>�2ЯwaB!�}̴�U��QN�ܕ��0�V½h��Y���X,u��t�Ծ�|����4���W��m$�P�nF�em�ƀV�~�(���=)[���v�箛���k��&jM�]�y�u��x�_�~��%1��Ւy���{)�{�v���[�K��2���z�o�5���I\���&����<[� f������V��dbЋz4,I��X��B����{(T��:ږB�-!��k�p�z�q��젾9�Zq���VR`�1�$�b�ơ���/K�)p��$�%y�I��g�3γ�j���y��ks���uGR(�!8�k��[%�vm{y^����������y�h�����˱�4*9[��]<����tך�/��G�_����c^:b&RR�� J��n�A��F�|K�/��+,�Kݖ���:�V|�t�n'���9TCX!Ng�?�G�8=u��F�2N� U��~�zX1"'�Q�RI�p��))
IQ�5�Gy���f�3 ]gv�H\�8�m2a�M`� �hf�ǽ�.����AWujx���mU/H驩��-Y'�t9D#5��v��8�fI�61���[Ah��=&���\z�\TX1����n��˛{5n�%UG����N��AUU�8�u}`���WA��ң�U�{=��K�0u9�Z-1�ɬ�i�vݞx�h�4f;(�%�r��f��UWZ���`y�'��:�u��\�q	��z�(�|k��HQ3vp���2��)�`Ag��.�n���r��G��[�!s��Y��@���}[jYg���"V�A��OI~�Z���a�V�+_Y!�Lj��.sL�8�(�S��\.���/��j{���>�5�z����%@�5s/��ʷ
�V���$_w<2�	^��G`ZTb�=�Q\1�9�
��:*w�q��)օ7ǡ�>f�.�F� �
���v]���{0�x��p���tt �)�0���6"��V��6"��ij���USEe�1EN~9*�u�Dx�0�l�;�,�θ���Y&P��iw�lq�U} v���/�d�m��C��X��1��m��JB?T��,�y�Y(ȥ���>�^��Z�?2��@L�m��J�j�^�尘��i�s"��Z$�n�Dy:������sL��r���?��,��w{x��B����#��M��̆�"���fp(4V���g�:��`bA�\xt�1���Pp�<���Eh(��^ڝ�m�\�c���Šjh��P{�x(mk��@K$;Ye�"�:B�Sq	��<����#����� �c n�NU�&�Na��4����CB��t�Q`Kw��R��,��C���K2��F$�z�a��N9HYj�����!��\a$�f�ɗf��E��V�����h�9PP
���T��jA7C���5�f��V���6�5��2���p�B�կ(<jݯ�|^\w�1s��m�;�Yjq`6�S`�Bd�O�Xeg�9�|�Ӏ��,�Ö)m�����	!��q��B�E���X`b"��K��Ҋ-����l)0b��3(C����օ�ݳ�b��I��r�z�1�םacĺY�E��<`X۰k�=�N�U�G�5�&�:�p��T�Z{i�#j3&=��ZWu٬+=i�\�n��R��GR�h�GK۲eHY�?���!�:|� �Y���R��RB���7�	C�6��[h��;#[��݁�:��d�.��sb�P�a!<�#w��nA����	]�� �A�[Qn����4WAhvZ��O �%)�$�H�bM-��T͓[�B�&Hfgw�����AΕc���N�� �y �!���	�S;0\�f��'e6Q��sA�i�v3,3�,�d��������V5B�K�YW'Ǚ��M#�l���9�{���ܿ\z������Y�)����C�3����6Re����!��_���Z����q��#��`�/m53�V������ �  S
����1.1��v)[+����(wfțaz��gi>K�Y���p�3 ���F�˯Z~�|�u�mIN-��签�]�E��-�.��~_L%��8�Y}D�髻-�U8n�.��׏��3�C,�ū,��z���qQ�b�ַ�^�ȻVK��B��������i���F�#��D�БșG��o���y������!�_�[jً���N�H��"
�$����J�g�H��a#�#��C�9l�p��n8QVw�cMڱ*`�Y�
~�� �=����mԌ��<�J��ݹ��3d��6�)f>Xa^y�ָ!aEj�zc%�T�!~�����|���?�j��n�wsu?
�U�m(��z�#VGu�Q�����w���U=��p0����~�LN��`���*/Ô��S����V�ܬ���F޷�P���8�П��4���q@�`l=�gl:�)�{㯀�<8�y���Vx�5�ں��Ü�g"�=�|#WMC�(��a-�p��{�u/��r©��W���*������X�h^edcfD�f���dS`Lj'FwX��RG����C�H!\2BΉ hG0��u�<#�<#b��~�� ��5Z�H/H�]��\� (}�bp�/��-K������B<�Nr��U�z3�@_�W�j��.���cW�~Uu�i~�d~��4��R(���<��8�qj9̃\�qOMb�����m�{Lπ��N󗌘G<Ӌ��A�2c'�^���ō��^�O����9��\,K������m�x�d@�>�5�Z�75���f��ed;"5y)z�j�&4+[�мuP/>K$��jػ���4�u���^:�qn�3|V"#�Ȱ�V��p�6q���81��B��rXݫ���H,./V����z*�a�1׳�T!]s8���)ĝi;�
:�{�Џ�`����ʏ�PF��u�.s_N�y�����;y�l��Yt�v����4��u�(�� �}e��-�s�7D�ݫd�ܫ���i�yM�+"��k��Z�m"'W�=�n����9�%��9\B������cx�cΠ���mx��[T�2]�0o��@j#b� ʼ*�3f����sz�3��(�;S���%rozF�F.����Kc���NM�m�����H^|
{�������(�I�z%�`�ϴG����G�G�m�6ĳ��a�^�7��x�q�?��1��P��C�s�l��-��Z��:Q�$ڸ��0\��\�NBFF��Ɂ�TxY>�_ρ�z�=�S��fLU���L��i)��C��vw6t�*����gɁ��tP�D��Ʒ��I��|z�u�G��1�8��?�|�(]��\1�\�U�\z�Smd� �
�2�� �� �4�]V�������@�z��G̐]=e}�O��~�U������J�0-��2�d�r�N;��uR��y�C$U[��D���x:0��'���zGD��!�S��в\�CS�=V�+h�k�������s��(#�����E����c�'����B`���0Ի҇'$�"4)ȟ?ā������	AI������1z��:��nËD�H��p�̢d�bP�K�l,�:S�v<��*����M�z?��������1B]*���㽳��<�R�6�FEܣ��I�>i�MƊ8�CNp`("4�.�ϵ�Abu�8v�-��3��g��o]'�r�^vI,n# ��c�L��8D�a~k�J�!��	+$���l�@L��E20���t��B�����7dW�I�Oy��
���a?�_:�!g�oY�o<�(�����j��ly��.�I1��w��U�K�|��H���g%M|�RP�~D-}�%�`D���s9�����m�B۩�2��c(��%��HKed�=��Rc>4쵑�+j�w���o� �az����Y�S��@���] 
�r�1�ʣ�K���-֌l#��6̓4���(�Np��B����������� S      t      x���ے�8�-����x�m�:��%1B̔D�.e������XN$ �4�U66���Z� ���j��U���z��}���۱?�����M�6j�3m��ԿT��?s�S�?�Ί���Y��?g�����7��|{�����m�����v��HV����~���U���	� � ��e~k��\�ۍ��'pv��v�vo�����v+,��'�������~^�?��{��G	�jsn?����6�H��~�{%L�:��}��o�q^k�p�,�l������*��Ad��٦�mn��ug��{=1�N�x���v��ܿݯ���^��p������6�`��bvv��z��n�k-
�(�P�oQ�O�=vo�G��/����پ���X��2[�]�	P0�R%0��a��N̖��4l�4�g����Z �je��0��jm�'�r���Z�m<%}�p��ŀ�VE��~|t���ت��1�o�������ޟ�"�z�j#��&[�s�m��b ���]����0�Y�3����ݩa�� S����q�k��wG���!�Y�{A1~ϰ~�CE6��O�x˟ms�8c��#,��g�ڎn�����M�s���?�w��k�`�y�~\�������f?�)�,�a���s ��'0U���Y��b��~��n����޵�CT?����ye���.,�_g�)��Uk�g�m��^u�w�����Ӻ�d�K9��u��P�����(%��u�uz���a�zr+�GvWϡYAF;���f�:����T8-��lY����g/BoYXi�fP������p6�����X��#��f}T뒰S�p&ל�Z���d&r����^�c�bX`�{e>��ڼ�k��k[�_G8��|l�x��L�RBg�&�"�p)���bwvi��?�����D�]�˂rk\V8/L��*g�X��������\�T�Y��,�8����?>�����zUl��x�u�UEj/���Z�_�hU0������=C{�ZF�V�t��?�ǵq{\0�0\��ʘ�{���.�y:~���Ł��X녷�u��<�h�G^�S�dm�#w���b�^Jԙ�\	��f	�s�JX��~b����`��zzGkp0[���!K]�BB�m�%+X�M�Cf�Z���ʓ�ff�q;^.P2"�
�\����g�z�0�"=×BZ�6\,�
L�HO��q�h�-�QT��"=�n���a�Ez�7�֢��E�>0~�-RÕ�j5��ZJ�]��jo�pXwB뭒#絛a�Qip���ym.���ns�kW�r;���zg�y�$C�Ӝ��������G�O��O��N<�:����� SlnMw�o·��c�ױm������i�����5��uY�sg�����S|6.q�������o�c��m>Vc�����I⢅Յx��ߏ%���A�٬�1�j|۫K���[ ������]ڳ�}�c�*+�����\������m��}�v�ct�;�����!O�C���ZO9,�-�ɕ�!5y�p�����gYR��h��
M�|ʏnw��ޏ͝�c���gwv��!�t��=ra�S�4<��G4����˧��֞:ѣ�t��I�PoJ�$��ǹۭ�f���k�����ZQh{�<�������zS鱚��3�7{��J�s�_�ߓņbɥm/.�l%�A�ц�;<ο�D@0VJ&�������וۤ�Ǆ�uZ]+��{A�k���)=���;\�,��� 8W���n��v��p��)�]�ۍ"0���V^�����q�4�Z�K�Ņ�s�h|�����, �f<�����4��"8���E��a�Ep���: �X�}j�8 ���b���\�cXa����ݡ�"��1���x����;`���8#������������ٝN��?\NtlW*�����G���t���ڄQ�a��Kqh����q=�cł)Sz�m��w�~0�|Y.��Y�T��)o�?�GZ����)�~����ݾ�2��RMէ\4����W�+gn|�O�<��!�c)�S�f�)Ak����ӥ%!7��~����~1��tP�7�}��ͽ�Ӿ]�v��������������sK����~�^���vN����#��;$��m�A�J;�Aέ�.���"����ڐ;������V�Vi���ٽ�~��{�7��j���嬻��1
4��ll/����|��<����\v��v7^YC��L7ʑ&�m���=�C�qxk���k>�O0۾_}C�'�g��z�VT��%�w�5��[]f�,��C��_��R>Ό�ѕʧ�!a�
Ο��^��m|!�Z�gjxX�Z�O9�=��`�zr�}w��{w��w���p�5�Y&����a�xu��Y��QJȁ�-�z	���^Un�����=,�L	�/ �N8T��)
�e�A�3���?WC��M�����y+�el��aX�u�љ�M1K��.=�;+��������?���`���Yo
���Xiv�93b~�V�MQ�����^J�7E�}Ms�!�qX~�YTF��MaR��{����5Ͱ��p�T���rS�̺����s���1�����x�����t��G�3��S��޸���bʏ�ڟk��\�bk�@��6���W�0�c%���L�5.�����9�����Y�G �����0,¡�!��:z�_�ӅC5��Ji���1�y��n�\cb o���l�9�.�o�~�H���PO��G�r��2�.�ñ�.4ޤ��|LE6����(�]f/����n���GQ�t,�km��?�'��m~H��FY�<b������&tD���oV�}��t���Q��f�yX0�*���'��VIg
�#�]��k5�,�I6��u2:8G���Tk���̥c.Q�<��@�D̡,�q��K�Px���F��Uq�؜)�;�.�r��O��r�\�e�xd�N®o�c�cy֗w�]������Ǎ�Gwg��˻T�yD���c�j�j����U�D�*Ǉ��; ߔU:A���΋�s���>]�U�wA��m�宍�j[%rV�N�lZ�q�����`e^����sf	�	�S<dsNɶ!����L�֖���v�3pnNl<�. ����	n�f8wu��&�� ��cš9� ��Y^��� ��g��CD�*����41��*����N\����	��e���[�\
>�I�2\js�K��l��Xj!���G��,��2�p��"��FY�OS���Ye=4����5:��Js�ԘӦ�QP'��<<��������b؆���fҩP�z��79�A�=sJPS��SO���Ҭ��
��vW��h�����&�`Hn�m�sz���\u��~|]n_��<���8��誧��c�9N0��Ņ�v1�����tv�f���!w^����8�4�^�4����㿋B~�.�d�!��g�J|���$�l��f(.���TX76z[H[���#E4���N�-
PkzA��=Y�xh�����AL+m��xZ�PUKE,��&����il��X��ˏ��b���Ȭ�L�]�z�I����ɻ���DC�fY�jd�Y��l6݁��*�=u;���F&����޿�]�پ���_�����4���Gsec`�i��=3�Y���!�����0!0?�U3b���T٬�zF�WpGj5�T���¢A��JEO�$��V��4�%����`�cA��7����-E�^�w� ��*;ă�����Uф��¨K�"4���;)
�
�1�ȟ�~���c�՘����zc9Vj.��+���
T%RW����+��47�v5
<j%�~�&�>U�F#�C	��ɧ�w�9'�,��K?�����in��M,��G�"u8�0%��������]Xֽw��e��3�Ɓ_�ӄ¡��x���'���5Y|���ڸj �vR��
o�UY���b�j�3���j<D�����o@�2�b/�0 Z�C�!�I� �verb� �I�pY��!�v8u��p!��}b�U�4�W�|̦�	��    @1O��a�J�܌l�P+�%�P͒K. i��c^؜�ǝW�qkc�Iv�"��u�B�=�!�f�r��K���f���~��q��JxwxW��8ʊ��ˉMg�<��<۔e��c1����$���OK��H+���!]ǣ{�W.8��A����E;O�o�=��qi�j<��� �.�*�����Z�S��JE\O!;4����<�RQM*q�/�R�������|����?,)��O�(H!0���|jD�͕�HB��<j!��K��4��5V��x[�א5]��� Fet[I	�9���%����4j���A��*�C]I�<��#!���>\�%b�"g�1���y�xZu��u���E3�NtQId�.�y���e5 �]f�!��IJ0{�cp�d		fI����4�%XTE
�ة!a�"��2e��
�ɜ��"q�2钍�\�!,CR���<5��"��+�6X��W��N�,��Wc��ˤS&I'Hm0��陁�]f�0~��.:X�sR�r��zR��}�3O��e��({��<��ga(�1����G{gI6@����d��m�jY�ʹ3���ypB���I�[���b�ʺ�/y�vמZ8o����J�1�zRH��6��ݽc=G����6�/{\/���]w�����Ŭf�0#��c��Z��}(�9�]���k��Jo��&i���׮}_����B5E�_���Q(T�0�L�5*�'�� P��pC��n���H|��_����͂<҅b� �_�#��]Kg�W���m�;�F����7ur���l��s�y
��B"�\p���8�ք�k�f`ܝ��+�d���F�eg)�@3?����s�-a�����0�� �4��O{[��z�f�l����
���M���|Q,z�t�%r�P�2��4�0+�U��lC�׮U4V#�@
��ݱ��o��[�4B�;tG��o�������^��`��meQ)���"�EiW��fT �� ��y�CV	�u(Dj2�Ehש���Uπp�*'g��W%�y"M"��03[���^��f�`��f�z���|�2WN��V}'��z.�_񘈮�pHt�oۛ�WXH�ʰ)�{f���蠧9���rR�'����G���E~����`Y�"�y�R��$�曊�ZtNz���FF_�� i���?�X�х�f�7��۩�>/��;�&s2զ�Gr�ݽ!�/��0����PSgTX ��TQF��@�X5��QxQQ��5L�D-��T�UqD�\��}�՛�0 Kn=��9~�N�r�f�,��s��9������CA!V�^ ��a�(�F
�TڽnE�*_&���J��Y�����1�3w�Q� %�ꕸ�4Ԭ�[Gq�i��͵2x���jU��m��dz������v� }A��\��jUV�(�W�^���jڶ�.�`.�ɩ=V���&��"��
:	��+��k9�+�Y�G�[=H�����ѳɩA�Y���B�1Z }_�C��ޟ�Ԍ�O�DS<���vi�v�a}mdg���0��&bP��n_2�������p�!GW�V߯g=\r������ r���X^M1]ŝC��cN�/���7��Wϔ��������j"~-���Ц��h�d-�a挑#5qW�X]�e9�D0�\�#�`��~ie1���|a j5l6?�v�k+c��V~�Qԙ����p��1ĉ2ꫢ�R���z�/΍�@���J�X�l��&q����>�!u�]�2[E0UOt)���~�x������sw���m{}��v0�`�������=�V=�9�r���P��vM�4&«�� ��zI6o5:u�0����zA��� H��
���;�8ݟܚ�{��,�KjS�5ިU��L�}"Ȓ��K�r.A�Im�d^�PX;z�3��H�	�Im��H�#LT�a�a��	��C*=�ۣc�6�ٹ��pj����ϵuK[���^��0�K�o_=��/��=8�ښ<�pY�mmVn�C]�{�8�Ǡ4��j�f���~B<J�S�=���M�!��D�%Z�|!u!M��0���B�l{�w�~3��\�"v�fa~5 �qFJ��(aS��1���ұ+li�����4&̶'���\��Xگ����bv�rw��M�p�)���PB��rğ׏����������o��xZ*��f���ivg�K�@�q����]�3PȝV�;�82��l�M;�Q]X��$���}05<��c�F��b�$���n��4����i�(��[�U�-������;/�j3.��.=�ߏ뿯�`��f��&�����V�M,���:{~n*���[V���OU�0�Vz�sT�l,�1�Ŕ��_�����K��T��?s�S�.+�b�!C�[��������A�PY��ۿ/ԋ��B���%�=	q��ϓ�L��X�Q4e�Q� m�̸,L8������5�bi4R&�"Il^��8	�/��P�+�wnxx���O<ۜI���!(���^j��l+l2�L0�"���L0ߍ���L��?�?�J&$5�JX���D�f\�:��ȥ�����\���7w��p��V%4��$�[�~��ѩ*O�j/����W1k:�tL(��$�)(0�`�U�\�亘�8U�;B����=�	D�1-R�	i�C���MNcA)�bCF��H�Q��ۼ���D�Z'&`�G��u<p4�T�);N[��F�^R���?GwO��Qǂ	��㘘j-7��7ᰱ?m�Ø"��9<~��ր���S�?��GV鲈0I�y8���$Q����J���2f�D��R@��w�ʟy�C���}��=a�vE�@3���U~`AMA/��0q�58/�ˏ5Z�X��_��A_��D�?��w����=�v|���p��3�A�֡�ϰ�dR�P�.ȋ9%���K��<b���!��ɤ��c3�а����M΄B�&�	y�0�Գ�^�~w`B{?��RL4"�Ǳ�<���_ƭ$�á^d�����XL$�f�wE>bA�I+�ȀОLƠ%o�����#�0�`�jy��J��deg@�g��$)�s����0���髩}���N��+��x&���\����r:~l��Ar��⚳�32�`�!�|�M)�l�za$Q(���SC�(���L4ؽ]�o�˅"a��IG��a��t�q�@��Tk�op0~��Q�F!O�mXƎ��O��?��@ �0m����n&N�r՚�$�Y��??R���a(ң5m1`��$�GK ��P��j.���)(҃5��0�H�(=P#���2�H̨H2/6iu���p	�vg���ﻆ�$�Y-M}In+���j�(j!eϨ@��z��6���D�e�uB��L$�{�iٚ�D�saɭ��p�p��WK3����fF���^?�ᣭZ�����m&�B��Dsa� ����m7�b��`�z^�تC���_/����ߏ��fBEw����O��㏾m��X�&�WC�ֽ�}�i1���K!��a��:V��YϐֽS�-x=$�9K7��G+:��փ��"f[Ҙh�#,��~+��w�"���qO���@�����D�|��S��c��T���\B{��0զ��&°,�	C����%�9R�ص�&�����0Pd�T�IP��'!��Ŋ�V����x��8��z��C]kK���H+�lR��.�}Z_ȿ�im�S^	,&\�-�V?N�&-X������ۙ�a�QpɎ(�r��{�C�8���L �z1�|y���'���������p���� �B��b�^G�I&,�W��A��|�q�S���'&�xҾ$f���"��1��5�C+�C5�4��)f��������x��v�}v�"��D[�E\�̋���3�F�����o��!��ô�
<�m��w�~���r˥iI5�Vg���1qP>)���d�4���z.�L5Ns/�ÿ8�M�O���%��K�d����^�K�����~��.�oI��7�R�pi�D-�t���0$�ב��Lԁ]{mN.'<=�    �fg���g�L(L�Aĭ;�̄����5	����k�
�e��{��n��h�{&��ڄ�5�=Q����6$��ʍK�%�-Q��L{�v7I��D/��#=�Q-�	��$˿��c�!4���	�I�NF3]\909K��2�721�LS���G-U<$�H�Fh>��ͷ���@tA!�h�"hr��Ha�:���ٲu>�]OWL?�~w��{�RP�0�����e|	
``��J�P�I�P�qt�,Eę0Y\���T��}"��N(�",�cD#�������1�������E��&��S��c�� �US��K���`�PH=�`�"��	�/z���!IA�]�(�,�B�8-�yCz����e�s����p�]�*�,�A^�)��+L�%��T�4'�g�����z���Y��WO�p�����}�޹�TlT>�թ|Hlh
���,{^W�W�씥P�����Td�+p#2h�gD4� 桡щ ��?�%2q�g1'Q�� J4:u�~��RL($�*�e�"!KU����� �� �ӕ"!CU^JT<�R���UZ#?5]�	.s�k̇�%�%����q�2@ZZ����d��/� Y�d��pp���"&�HD]4�JZ�X%o���X0�*��4\$�0�*��h���૤㉚�����t�O��DV�:�ܱ��
]����YƞK�$
��yO�jY��/0>��a�հ��s���M(�l�`Y�Ğ���1b"�[-O�K\Ơ2�-«�?���X�
�&b�q��ة~�����K"����C��MY�#w��/xQ$6k��lBkY�t9�u�헻��L;'*j��qr\cB�T�����]�%1�猷AM���0&�Sݶimpg�K�Lu6w)7�4�dΠ��=f���%���k����N�_ݙi�M�.�`A�]�(n9�Ñ�Y����oG��2z�W�%�P�*�U��N"k%&j�2IҐ�����6B!�3IL)0zj��r�tJ�ԦJ��t����ڪ&f E5ɺ(Qy����-)���l�Z~�.F�4��\E%P�C��Ü�k/��LUt���]�b������
��|"�F�����@�'Z�Ku���I݊��^u�D��?sa�Q�n?��� ����mw��AE��i�����E]�#d�Ǿ�j=�n>�KL(��ų�-��H�|�	#H�*�S�I9Wbjj�n�6)튔K�)���$�2�P��ҍ���Uh�j[��Y�-U���_.��W$�[G����B�Խ��0���Ϸ��*�G��ޤW8������C��5&��j��a�2��.�
�����8�:�~ʤ�*"���/�>*����cπɻ�9 XO��<(%3Q�ɛei�[��I�7��g.��"]�����.&�y�0���y�6����g:��ͦ��?��I�΄��2��fab��?3�Ӣ��M��0ÛU3& �ڹ�H�Ъ��?�� ����\EZ�JEO$��U��B�>&��h�J0ﱵ"�ryP>;(��"`���eAS�\�b�%�+x؂*u��qT�T���&mXY�_&z8����O�D���1�m ��ʍMd{��VdN	ȫ�Ê�dNH������i���Ă'Aݚ�{ ����"l�(t��B�s|o�)��G��c������ƴl4G���q�H��;�h��#n\��Ņ��{����|L4�s=M��{�<q�
��d�|lN̸	�Ph��K�#W��*h�E�[<_���;��L�7&*"��ZY$�R� jL��U�T�j�O�tQ�PV�jL�xy�(Ux!�-ȓ��_(�(���Lz���U��ћ� �sm�� ��4��2?��z�������;UI"]�N�x*����e��@.�O[�g{��hv�!����Y��롡7Qڸ�G��;�:���NoU��r�}��A�*�Z�ˏȞ��zS���!�9�ퟖ�2�ر�@9����dh��ϳ�C�JoP�|�~��ۮa^"�٠�����*L����7Vv��r6��y��9��g�ºĨb�S��D�]���W�qk>�y��@���H����8{��W��Z�ms���3H>�X���f&Z4�(1!�g:~�W�u:�����}���w��6A�����F_M���_���IV=^* .-���T0P�^^�������E"�\g��/����N�&vi��"�/Y�h��~�b�t��H�&����%�u�Ip7K"��vM�����]�yM�EB�)r֤�[&Sl�K�&�2��ƻ0y��ƛ���V'�x�&=��,|v��&1�2����PԤ�[&}XqrU�o:�#�䬕�򳉲�6�<2AH�n<�|�Z��zR��}��D��Vզ�d�/W�ӡ���zSO����.ܩ]PVO��#���.�gÝ3ݹ�C��7�4�sk�{A�Xk������]{j;̙X���t�>���{w��d7��������z�o���ۭgfյ�ޔ��7�Ȼ��w9�2���߮�	���8�����?m�]�μh4�����8�fb�W��tLs���	�ЦJ&5�hn�$Pү3j�Pu��:l������hB����n
?s��G���@͒/��O�@�d��KV�����3�͉{�		R�d�V�g J8\2&���>�x���.�	�7
{��lU0�[LS;+T�����L��B�C���7Z?.���3�4�io\�r�vՌU!�s`۪ܤf��w���b!��i��f6�"}�4S�x����N���BO/t@w��~ho��Y5Ȧ�4�;tGw-��w&�3e�w��%k�bc>�>��10`�)d�o^�WP6�m	����F\�]
��0D/QF�-T&'��ף����D�ڂP,�F;5��E� ��6��-�4�I+(E֤�k�w�[�g����� � ��w���I�K��D�[�!|"KUI|ד0�5��z�%
"T�K��G|��[V��~�}�4��S/}+��DM�����[ƛB�Dk¥��%p�Η��w��o�J�|�1v��F��o������1gCu��~���RED���/%b�d2mĒDx"�0m�N�@TtWq,�\���"8�Tp�1Ǽ9~r�[���3ͧ_��'�)�;���v�;D��}�(�yZ���e$�(�W�2�w,>c�C����
�)����4����&�*#��w�j�b�v��$ꃡ���T!����T�������A
����Q�]Ue����;kh��2�	��j�V��1�ĚڮU���~Z�P��]���ZF���TF-�b���)Y�}y�����d��$ʓ�H�mn-����2U�ނ����!��#�l���F�F��&�7�u8D$����l���C�a�Ӄ5�I�Cb�^���� ���Ev��3��WU����}WUۅ����7D�ԮDs���x�0��;s����F���N:����+�O�tp��g��p�� ���%_g��P�uc��S���1���2�YU�/�r�`�a8y�/�0��������*"[F����k��/�Z�� ���2��Q4KEm�M*�+PpP��T�צ�8܇�vA���Hl�zbz:�����;Dt��.�?�m�����2���~m��c�d�ڔ��a���%`��UK�S�Ɖ���K�\$4��|aȚ��T���'{,��~�p��5�����o鶖=��悦����K�c6��[���	��B��N��Lr�=kR=��"���AN�T28T����O$CB1(զ�,tP�M��.
8��+���5rX��#��L8,\ A<�rl��u
��xF�%`7^��-0t3�m�H���ʈ�j�d��;��p�Ʋp��� �1>?}%����/䀤��Es�:�I�V�m�0�j�/K84�f�Y��!)�tΞ� gPS��1����r�K��FT&�@����k�P�A��ֺ@ث�j���g4k�4���{!��/�r` {硋u�!��t��L1ꙻI�aP]gZ���B�ivwzh��i֒��C{�v���#�^�    �]�FѬ�y�!�\�Ku1@
�`�Ӛ�?�+�?�����z����k�el��]����m���q� �{^;r_�e����(|z���Q��(�¡���F&�c6�
M�ۨĽ��������˟��at�t�������B�r�����-+~���*���"D���m|su��$�5��s/���J !���R
����\�4�X�}��G���헲�x(�ؔ�G0 Q�,R]lJ�}�� CpS$�ދg��c�amO�@ �D᧏��@8ż�����6���O4�"�r7�h8�}��P0Pp��� ����I]ݧ�a�g�Qc7m	�5vS�����eR���[�ۯ6�`�v|�*OK
���j4����bA��K���!��<+�0�p�r�!3h@rq�x��}���W��:���Db��C�§29J�$�L�Q���`�4�7�z��{j@1@1堓�8�S�H}Xǳrs���))R!��þ`s$B�w��敮����y��� �g*�n���4������c�,�#,�y�l���PU���.�,��(�����4��T�S�y�W�DP�$,l�}����g?Ta����	���7�����<?�P�����%�@�Ν�2^8�h��޽��
�J�ɥ�C�}b�0�0�c����g�l�jj���ԫq���n��}�sP��.���I���7�!��a�d5EX����%X��m�,&�Js�Sk{����� ]�8�{RWg���H�ɳv�rpe�r����f���\������6!�D�W��hB$�y��~�������4�� ���]r"<�wU|��6~2qT|~�r��"��0��@�!PS�y�J��@�8
�'=O�9@8AD,�f��.��~��cr�H �?��o˒Hf��#UYQ�����iicZa���	��Ӱ�ےt��D�䈂�kl:;)�0ql�A�7<t��[��	�( ʾ��7��Ʀɻ�����y*�?�	�Қ��
� �'i�"-!F��0�"=e�z�D�x�$h8Ez4��8Ez��U,�]K�֨��m�h$�V$i� �H�+�3�(WK���k�*))WK�u�;����p-iHT9*5��>����7�i/Ȟ0�He�w8i0�Ho���2t����>����a��-F9Կ�9�%��q�G�"��B�$�J�t�/����m7#���D�8�DϱZ�����\a`�}|G��Q����<o�v��~�8ڒ���Ńc8ѺwN�0:'yɴw0$���ÞV�K_4) �,�"s�+�M���v���NlM�5�#".��QLz�K����EU.�|�N��䰽����D��l�֞�W`�X_ �V~Rh�v�1p�v�Σ�k�߀5-�GP���`�7S���G��0X��Ӥ�ٶ����?���NL������<��u�-i�ȏ��ղ6�
ːH��>m�_Κ��>%�A��7o�t���Go�6�$e� ���3Y��v� �nh�u�bRV=��\�v=��y1_1}����'������p���� �h��E�r"\
l���AC�tn�S��fBm��d�����-�Ǎ���Z! l��<�23pHcj��R�����U�*���tB�����c�=�h҂�6�����L�1�������{\����aB_zĶܻ?dF�����:�iyl�����qg��p@�0��@�ݾ�
����z.tt(h��^{Jp�P+6�v�Pj�7uRB-^8y���R���s�_��K����E�r�Ĺ�t���H 9�#)�@�c��@B�C��ksrI���~_4�(�n?��baa�A���e1\ �ۭIx�_\ض��6vo���"�u�ł�[�P<�Ƅ�k�������>.���4ڮy6�j8�kw�4��Xnn>��+c2��d�wҘ\4.�]���B!v�ҙm��������2@�L���:S-KdY���4|8��L�7"��!t�tU�		$���Χ��ΐ��m��t����ww��7)F���X�Ih8��/G	���ҿ�e�d�]/!�!q:L�08�i�̇�ۚ���<%���133N��D��!{<cc����H2لq�0-q$��l�Y}wy�ݝ�=�������C {�vS��-)~�����/ī..��?�l��6E�}j�Y.5.G㕊�ˍ�:F�Y�M�;L�P�|J����P�y�TZ�M=��ݟ�K���{g?T�Q�D��"$��)J�.��9_i7�N	E�6J�P�7%�Voz�C$>4��p���@4Se !��b�P���]h4SQ�?X������VEub(�*��#S
-9�ī�P�i�Wܕ�h�WQ_�������Z�����X2���kh�Xu5#{i�˙hN�I8��SohraJs�*���64�6ZV�@�@Q�l����F�g���n"�o̽J&'$̾J�AZ�|0�*骢.H��%�p�&-�4X5��Nf�-~:��]���b%J���.�\����`.�\^���IHQ����9�^�4@�q0:}"�#X3����*0�uP�L�1�v�7�r��݅Vҋ*l"�-Rn0Ĳ�;U� I7��֯V���ǅ&e������"�d���3s�u)�Q�w�~�{mϵx��f�\���6*�c�/�#�( ���9[o�ZˀA���izۆE����h��=�RW7�kP���)i��6^Dȟ=���p����\�4�|��9H7��O I�04Μ,,rD�-��y�si�L[VѤ�gw��-1jSeQ�qi�d�����$씘?�hSAQ�Eh��J�`�T���Z�&b�" �5��8Q��P��S�%�34h����5z����s�Re�;c�/�$�_U���/�ti�̿k~ՠA�O�ب!$zG44^��v����aC[�*qjo��f��%OC�Z�����A�>��l��]0�`�E[�b�����E�#$�N����L���V�q��[�R���"��%�(sB�� �z)1���5&BDZ,Bx�l�bY��v��������E�^��EZ��(b��`�!)�:RÖ��b~m��/��--|��֐+5���1���#�C�^��!���5�Fy�!��A���r��?`�몣*�P��UG�q�B<W���flC�z�s'�]ML����!gY�P��|FΦ��͂2���=u����E�rМ�9����Sw��\Cb��4���Gs�BA	y��M��" ?����OK���%C�dqVͨ���gG7 �ڬ�:��W����[W�V�ϐ�R�3ɸz�V��41�%��]>��ǎ����3��n)N[����T�G��_f��\T�В*S}�H���7�(}�Oe�G�]?�b�0��7&�"D���D�HT��=�S��r���)YY܆�@�o�if�����V���u
�7��6l%�z�SȁL�M�pNB�Є�:\6����9�?^"�׮�]�~E�uށ�s��hp�:�7.$�޻]��'�����i����{<��\��&���cs��Uh�Z.�6��ڰ8}'�8�V��&��v���|X�.��R#mL�bY����|���8�B��H�����'�ȫGI��/��	B/!����W�Y�q|m��L����5
>s�biW�Qm�~J�~=ʣ�����!TI5*<C*��[��#���B�q���7����t#!�lJ���uff�z���TȽ�ӣ/�o(C����qB��BCYf�<$���B�c��i��#	/!&P�r�7<
�s0��`��º���������X�`�¶[�{Ҙ�)"Y��t����T��D�Cs:�����Ψ�X�7�(��ѹ��>nͧ���������
��)Фk��ms���9��Z�/�@���5�RL��/;~������8����m������>A�d�0� �F"84R�/.�yF�<^� �S�A��X�4��~ω[���}#��Ή[��]�?L,�:� �  aFuYP ����u���A�3��g�JHA���=M8���8q��=B�䉋�%�/��$U\$2�ҜT��d��ەĊˤѻ�,��� ]/D�T
&�i�f��[�9	�I�P��#'��2i�����pq:c$�,ͭ���c�6�CX��@!;1*�s��UO����cw�A����/W�ա����AM� �ǣ�KT� 	UO��g�IA��m�w�u�.P�m�i>�����@�P&��S����b|��n7�P�$L�qm�߻{'y*���-�tI�KsGn��n=;w���D���j���dJPq��ڞ¶b��qtꁳB{���^���{�D��O`������ЧJ�Fg˟�x|�dpT��,�]��B�i��z�qqе2���<uwb�q������Y�-�Y�=��i�\@�!��������)v/�!�*7���"�jC��ϳ�|�M^�r��N2B���[[�8���c��*��H95lu�P.��
���=�"c�P�1�B՟���t�f��_+W�&�dKL�Yy���xY��9C�l�Qe��PVE��"]��~Ԇb�/[L#=�c?�7ɢ2�@;������+�o������4�T���/�<BZ�,B�����������y�dQѩ��Q�K��(0��'Ԅ�`�*'�|���9��D�v�Tl3���W��B�d6�w�q^�WRд�Sl�w�c���1�+�	2		��ؓ�
�m��C�E��Z*�LR�x�%�b�!� B��}�{��В>qY���y��.ɹ����}Kŕ�fj��CS��U��%�И$4Swt��N-�����{�`�*�T��Nݱ�7��):n.��ÐIh��e��� �1B~t�[ҙ�v3`��*D�a�+j�r�����Pi��4�;3|��6��1s�9~r3bK�X�=���rK���B����P�׾�D^��s��<-��r:"H��|��<V�1D"�Fs��,�h�%�l:@"+e#���?4������Ә�+@�zl��ӪLu@^�I�8�V������$Fx���}Z��B�߶C@c%т�	]�*��&�Di��[��=�7���	�_޿�Co��"B��Ѻ�y"?'�IvZj�����f���'�n�[�8���EsW����?��/	�q��2ag�˵KC�À��<r侅��J|7���e����Z���&E�AG�����&��q=��6E�AC?K��������UFqG\4m�o�O�0����;w��۶.���\,"�VK�b3բ��EI)9R�w�Z������^�5q<sV^o�`�b�s��O1��ߪ<_�g����t�h_�1���ѯlXpo��w>�-����b��+$[l�xƵ��bGF�"���jR��_���p���^."�뉑���/v"@�[�EɻK�o������[?o۞����")�8K(g��&(����#VK�Y�f�-AG����.	}�:_�����U/��	�_�9��aM&�;iK��nkٓ'q.�J-p��;���K2%B�-dS'�k&����5���+���ȦN�뤴%�Cuh�HM2$ԆR�-�GKl�i��E�S{E<�|2�K���f�^�t��B���˱�, �1�x��%`7n��"�bh~���!07D��ظ�H��SPD\��ҡm.Z�C~�J|&�2�BH��\4�?E�I��}+z�e� ���+C��x*BD�����":<|�Tܩ����Np9lܥMb#*�l=^��EG���q�����y�tt�4���{!���*��Ee����z�0�54
3�j����u�}b.� ^��f�p���2�A�@��6)�=��n�_�|0r�U���n�@/���t.ꥺ h0�iW���X�4�s��l=��`���n�	��Z7����X,C���?_;��_����~,
���G�A8�G[h�����F&��v�
�?�w���Γ.޲�.��ѵ�����_��_��ԧ"      v   ~   x���]
� ����@�k���M�6B�.�����iz��y���!�b�\R�h_�ucO�ۭ�i�c�au�B�Ѳq�ܢ�ň�f9R~uI�v���^?5��3��T�8�r���K�W�1?2(K9      x   8   x�3�tru��B�pr��Z*X�g�6a.#� WL�1z\\\ D�      y   �  x��W˒�0<��B�ܒ	�M��2B��X�|F��2�V�M�>��ꞑ�������D�$YH��,�(��J~�/�e�H��w�h�>bؚ�����H�``M�ݥ��۷g��;Rbv\$/�K��@��}0iъ��i��qC��hJqX@4w2n��9�4ih3)?Ř��)��h�Rǧ�NC��\�P)Y��LY�n�P$��S�	����L&�ˊ�`͂\���0���b5��{�@L]���G��Z�C�n?�
��U%m��x�pɴy�s�-��WZ�*��J���n��O�P6a�����T{��`P�][P�7���W�R�0(�+IGm:�A�Q��lP��XȪ�!��KE.D�P<�4=�u�0�U�e���<����g�:X��ۼ�G@]��{I��6�#]+z�8Η�7p�8wuM���\�^�+�7h�Y~B���4��r��ų��@y��0�b�p��nGЮ�o��n1�s�qD����eܞ�ؐ��0̞�w0�3õjX�̉&��G3��ۗ����h�d�.ȅ=zй.ߑ��[�@K�+���>R���a�=ruM����|���D!��������p�;,����
���qTǶd�e4�V~�q�k�.�r����6�ނMn�{���wuz>�Aͷ�/��u�9��O��m�Z�Y$��F]A+*�]I�f8�����'��M�lL��N�X78/��S�U�m��jZ�i�p&��l����v���ڜ      z   �  x���͍[D�u���;��F`��o'� ��`�
=嗗&�y_}���^տ^o~�x��|~�|�������~�k<��|�z��~Z�y"��<E��y*I%Ƙ{O��|���%��(��D	�D	�J UB��
D����J(UB��
D�P ��Q%�j�@T�Z(�B��6
��B��
D�P ��Q-���Q�P �
D�B��[(u�O����Q�P �
D�B��!�B��!�B��!�B��a>��5�QC(5�QS(5�QS(5�QS(5�QS(5�ߎ�5�QS(��QK(��QK(��QK(��QK(���R׵�Q[(��Q[(��Q[(��Q[(��Q[(�M[��
D�@�
D�@�
D�@�
D����g1ŧ���CAL���ԛ�m3����I1ש�b�N�s#�R��4J1�R)��N�s'��y.��p�@�Wƃ��`ye<X^�Wƃ��H�6)��#M�x�J��ƃ��`ym<X^��ƃ��H�6���#��x�T�,�xdy�#�3Y����G�g<�<���,�xdyƃ�����`y�x��a<X�0,o��l㑚m<ҳ�G���`y�x��i<X�4,o�7�˛�#}�x�p�4n��m<X�2,o����[ƃ�-����H�6���#��x�|�����ƃ�m����`y�x��m<���GJ��H7��ƃ�����`y�x��c<X�1,���q��������N���a���0c�?c<3���)�bn������*�N���\񻷠(z�[XT����s�Q)患R̅GQ�Kxż�GQ�Kxż��Rƃc)�bn<R̍G���H17K�����ƃci������X�x���s�bn<R̍G��0�V�x�[�o5�G��0�V�x���s�bn<R̍�2��2��2��2��2��2�G���H17)��#��xp,�xp,�xp,�xp,�xp,�xp,�x���s�bn<R̍G��x�X�G��x�X�G��x�X�G��x�X�G��x�X�ǲ�ǲ�ǲ�ǲ�ǲ�ǲ�G���H17)��#��xp,�xp,�xp,�xp,�xp,�xp,�x����??��0�-���0c|x����i�1$�X���e��)�b�RT�\����;EU̍U17ST��JQs;EU̝�ǹ�w%<�b^£)�%<�b^£)�%<�b^£S̅G���N1�bn<xm<xm<xm<xm<xm<xm<R̍G���H17)��#?n`<���#?n`<���#?n`<���#��x���s�bn<x�x�8���q���ƃ�1��c�s�bn<R̍G����qL���ƃ�1��c�4<�i<R̍G���H17)��#���|�c<�-��ȷ8�#���|�c<R̍G���H17)��#��x�q�<��a<�8�G����0y�#��x�q�1<�c<x�x�8���q���8�#�\x|������!]�      |   �   x�}�;�0���Wd��{������E\�t(�BE��R��ڇC���q>���x�����նm4 
�ӈڂN!�
��=蘭��\�����:(	!��1/�s���j��*��a�u,��� Ӵ�7�Ax���V�]���ȑ��}�NVFJ�� Ff      }   �   x���;�1��:9��ȯd�a�f�E�����f������WXV�'I|J����G��`{Kh����<�����9x8w��=�-�|9a�M��"�Z����@��U�����y��ŝ�ĝŝ���GnNw��+Ns�GՂ*����<��������e�;Lu�N�9�Њ߰B�Z�#���	]ȝˏ�l���ݯj^���|�Oj�z��'5{=_�v})9�+e�^      �   A   x�3�ttvv��t�4�4202�50"+0��".#Ng�O?w�W_�jc���� w�      �   .   x�3�p�4�4202�50"+0��".#�`��*b���� ��1      �   c   x�����0k<E�	��g��s���A�N_<4$��uK�*T�&�Cs���IG�+n�JM�������CzP��&ބ��Մr)]��?k���_&      �   �   x����D!�3V�<#�`-�Kx���'�����#�U�.P�����h\؝�#��O�n=��h�(&Xfc�:^���ᬙV����z��/���wˬ3lz�(f���b�er4�3Y&+�ݡ/f�L���d�ͳ7l�������+      �   l  x���A�7ϻ���h6YY�E���*�_���6L�t�{|�1����s}������s�?����o��s�5����]���}��{�w��xg���;�wމ��xG�x'{Gxg�N��;����^x�=M��HO#=0��L�4��8M��8Nc=0��\�`����e�oL�*�o��u��z���X��;�����K������oc���Oc���@�e1�� ���UF��:�4Wuj��0�\�a����\sU��檖��V�=��u��Z�w0�Z���V���K����w�����4tz��9�@�4&:M4Wui��4�\�i����TsU���Ns�U���z�l��]d�Z��?8v�X�·fo}�����ϯ����W�����/fg���w����O�`��e81�����p��c�������>����j��c�������>������W���v\w�P�㚽C��k��:�����K�CG\�;�mĵ{�>��N�`��e1��=L4Vu#�U�LcU�0�X�1L5Vuc�U�\cU�0�\շ����(��n����(��n����(��n����(��n����(��n�zA��=M4W�4�\��LsUOC�U=M5W�4�\��\sUO��U�L6Wu����[�u��_�D7�|�K��|)��/E��cr}wL�������1���?���r��0�\�a����ls}����S��ƚ�;�5�wl�o�l�������'������'��������?n����������_Pu>@c��DsU���N3�U����:M5Wuk��4�\�i��������x��;=��Ύ��������x��;5��N����S����x��;5�T}�1��DsU#�U}�4W�1�\��TsUc�U}�5W�1�XպL6V�*5�ªV�����wf�P�U��C�V����ɫR�g�>Uj��з�J���4T�񳃁��Xb��4&z�h�j#�U�a���55V���ƪ�0�X���Z�`sU�&���R�Wu���2����XWj?kT�F�FUj?kT�F�FUj?kT�F�FUj,1��=M4W�4�\��LsUOC�U=M5W�4�\��\sUO��U�L6Wu�F�FUf?kT%F�FU^?kT�E�FUV?kTeE�FUV?kTeE�FUV�g��hLt�h��0�\�a����PsU���c�U暫:6W�L6Wu�^��Q��?kTm��5����Y�*+��5����Y�*+��5����Y�*+��5���^8kT>@c��DsU���N3�U����:M5Wuk��4�\�i�����檮���U]Y��wf�`�++jqUWV�⪮�(~��ʊ����(~��ʊ�����.u�1��DsU#�U}�4W�1�\��TsUc�U}�5W�1�X�y�l����/��"�`�Ί��1;+����/��"�`�Ί��1;+����/����9�1��DcU�0�X�9�4VuC�U��TcU�0�X�9�5Vu��U}�l��Ί��1;+����/��"�`�Ί��1;+����/��"�`�Ί��1;+����jM·���%TN��q6?��i�?��c����|a̤sO���=�:��4���Ӵs/�N�������_R�UF      �   m   x�u�A@0E��St/��i��	l	!����I���� �|���mITr�Q"S�Έ�#)��G�&X�g����J�s�n۾^C��-SJC��u���ΐTc�R7#6"�      �   <   x�32�4���4B##c]CC]#sK+c3+K=c#s#Cm�i@�A\1z\\\ �.k      �   �   x����� D�a��WA>�a�L���iRU����$p	�8+Yb����-9�}�	&�X���u�nϐ�׷1�-��F�]J:p�2u����c�����I�7C�A����+�;�=���V�;�T��U�	��ht4q�qx�=��X�-�[�)��&�O�      �     x���Kr� ��5�"��P���C�s�s��S8���W�>�) r�� B�p�
�7����yE���h�h�+e��ϥ�K�S	��꥾Q&/ө2�J��(s-�N�t�RD�]Y��O�LI�SO'�=,���pK�r2�G�ۨ��������:�����:�
�,i�V?Y5Ҷ��2�����C��fe$���e�2�{��yc�t�~�����I��l� ��fdb$M��}�򦚫|N��*��]���_5��z���f:c����D7�B��e�0
"�      �   g   x�u���0ky��e+�g��?ށ�o��x�p�"�>��^澀|EWZU�<�4����x��h���!3��5b�exQ�l���:n��J)?��#�      �      x��ǒ�\���λ(�մ���U�
��ZΘ�A�֢o���%�f�`M##H$�����'�7���kB�~C�!��y��������@�nF���f�o�����m�?w�?�������?`�_Q�B	��G�m����}-��\�Y5]9�}����S�	�Lf��
�'MQ�?!,�q�
��u������O����tY��Q�_�u{��y�/����˾�=�ٷ�Ӻ�msym�6���ٞ�kI`�4����H�������o���&���	�b=2��p�/����|�7���o��p����̲b9a�{0������K	;���t���"�TE�v��d��m&�;R�גAjC���������r1�������l����a4�)$��*WCB�v��3]\��� ���H�t����2� � kP�V����ǁ($u(�CńS�%Ů�`'��*Ǯ�"��a),~���o?�@}F�ZW�ޠ�u�߯=�&O���+�ql}.��'��<*��������{ ��s�U��/:Ô<($n�]��P�o��8��=��?��ڌ�N�ybݠ�&ǜ��5�����M4*v�Є*�;�x�p�=�����N�
�w\2@u=�Dh��>�����K+����U�a�v�E���=����o)���1�7�8�����~W�!���&���Q���Qs"�o	~+��f�3�7����,�l>��a6�����?�G,��`+��@u�fT��M��~/8��L'���!�\G��n������BI�X�&�׫��	���Y��J��m��a\��!�1\0]�E�F�z[����� atf�-S1O�L������_���m>S�I��MU���$5�?���5g���)@"XUn�klHE�t�I� ���=�$�9�	] 3sT#��!^q���_s����W��K�2��d��NÆ�T�`���F�c����=�|��f�6x^�I���1�ڌ��T�e�^���l�܋	��=x�λ֞�@�����>0R�z�Q'�Qz�Q��hV�Sl��X�鋧�Bz`����S�����k��l�b���@0��?��el��#r�\Jc��c�$e�U9�c�f����Hw�j3qB搬Ki����
��3�Bl\R�V�ׅ(�����0���g�U�mV祁�tn%�|���?�wW���ɃN*�*|/(Q6�nlS��)�!�����3o�'U-L�B,w�-jz�"�Oaq��N��ф��S���Į^X�nC���`��ĤD�=���$���h�	�v�x�� fmdU�\e� ��`#;�Tgbb~��&Al��$KZE��­�R�5q���{Qf��t�=2M^|j� �&V4�#_%|�K��d��.���ܙ�.QN{r�>��c�ϖ0�ُ9$+D��H���p�Fm?�=��)�U
UkytyW3�y��^�a�R5�?Lr 2ܡf��δ�h
�U[A?��!�Ӥ��c�k���[ʮ���0�ew�j��ݤ*u��[���{1Maij�#h���I�dI��S�����˚�åё-<o�Ŕ�p41<���1'b���k���Y�=A�rd2��r��H��.F.���Ψ�D$�E�.0�%+f��	]P�~C��|�r��'R��Iz���/�d��Py$��e����n���!��$6z�L ��]j��3|vah��D���Ne�8VzsS2�|��N��$˹��7����������[�)Z�x�I��l,�@̆v.056��%L=����k����	8�F�	ȭZh������ �U����^�P�`��-����C�2�0� �� ([���<�(��ʋc�ٴ��sí������'C#�x@��JRrh6�1se 9��ChKS���5��c_q�`f�����c�����*3�Lo1�f�y`K�F_2j�R'öC�r��8���^mPiP��v���T8���HZ�����︉2ky��5l/��Ng��6��c�W`�I���ɇˤ�����b�<q/����0� $3��0�uVx6B�����XluG��hq6�Z���k���U�Z�l3
"d�?���o�<�}<�ͥT��B��7?��D��Ӆ�'ݵ�Ȝ9K�t�G�M9u�T�,�%Ť��:&3g�g2�Fn�����<w�u��u,�f�����P,�)���1.�6���شWga�5�4�h���$&����*p��d_H~�@���(�n�m����+�&��rv�v����E:~�J�+#���ǌI��p=J�B�LO=���R��x�t-6�gni��gVylx�AS�����b�y� �;�!��6X�..��>3�֮�P�y��E%Z�N$�9����U�ͷ���~�z,��^����gn�_7Q�ud������ٷg�9z��\?�9��eE_���D~��I�M#~˄	҅��
�f7 ����Fp���`3Φr0B���ր��� ��I�'��Q{��!v3�-��@�_[�h�����<|�ꅕ���}�-1��5��n�m��&���!�m�dfg�65�`L�fd���r��`�@�~I�6$ȱ�kŖ���]{�e���p�O����.��5��52���>�N����Q'����L9dB]��!�n._�㳅k�>���ކ��&7l4Yt��ˋ6PÜ���^)�����h�}���w��a_H�o5�07��w��?���e޳���D����%�I��7:H�5Qe;��_۲>c���{zWQO���$�/��Ŕ�k�d�?��ժ�����}���PhȔ����~j�����L�:�q�y>|��JU�p�у����A�<�)LK��r0UY/QX��rČ�z�2��P�a�3v��;� ?�PRa�)`~�Sj~,��g��`P��w�
�0�7�cJWatY��K��� �{���D_j��/T�1���F�#/q8���R����Gq[���vr�^h������w@`R�29�|�0�@+�2	1�0�0�"����u��ѯ��[�9�D�v�G��X~~T��8D�|5��}��kwL��P\�xx>:�9�9>:7��
������\rc�'��v3bQ��@�B[9pY-��gg�!�·��l��k�Х�7?��6�,z�!w�,�I$�T�2[?��@�%hbj�9oG�^��U�˲r�}s�����4(��Q�c/w�!�Ā�%]��Yƍ+W�A���ʧnve���!s�T0I#����vqi��ɔ��5Ȫ}!�k˸�5�{��˘6����ٽ�M�\��z�� fb)9>�mE;����c?nc[*P�7�$Sa`��?�
�c�� Ԛ��:>��)�}$hC�H�e��	~�`�- '��H�� �C	����� ��ϫYf���}G!O-�jXR�WA��@lx��R��L�LB��cc�e0��;�>�,W��g��(�_�}Y;qM��-J�4��x�M��3wK`����]0k{J�T��r_p�6E�|��Ȧ7��g�U��������}$���"*in�t:`G]ߚ|H�c�ƬM&��ՙ����W�%�$�f�(�@��+���y�LT���'&�i!Vg�������n1�޷�!2D]	�
��wH��S���"VUe��=�Ry�~�0�&�8G�G�������(y��ݭ��ŷ�\^�ob>�$�ou�(��(�[����*��b�7��Ԥ�p{&���\�s����rD@���(iV�v�8�*AG��C��g�MПl����K{H��p��f�v�'���Zù4�i�;��L�nC��O�)x�Q�c����<�A׺�*��(S18FK���Y�RH�d*�u��������{l�W��aρY\蚊�z[zqG���3�n�xO��oX�K����	Z��Q_����aN$��ZJv��d9����"��:}[d(9޹HS$Ҍ�p:%Q��V�&k2��W�kk� d>�i�l���[�T�#�c��:�s����r�0�o�σ�ήOM0    ~(�݇�U+����/����5�����l%u#�ߗW��h�g r�=X#uK��������~�kN ��a��9�YU��#*��^�Mxm@�Ak��@�kcaق3m,���A�K��
.���=i��:��
�W�O�ms��y��^�ep#֖��4��`�"�۔f
R�DQ��/q��q��9�/�3\�0��8�I1�ũW���WޖP���/����2�i�R|�#`�#༑��N��C��<2�e¸�ܠ�+������;��fE����};�'N^o�fj���q�.��U%�xq�����Z��񊦟�(R+�ܟ��}��v&�	�D�}���.��
�K2�=��G)���:@42��,�Ӈ��X߻}3��z�� !w���PF! !.�$R_=�]�}��B�6��'ۍ~n��`xn����>n-�b�0LPL�ặ��+��*��T�zb�Gb+�������P1�'�syxN��ʋ��U�bO�W�g����`�_�}�%�{?���|e2:Յ��[���1<}��8�Ul{��X�z0'�;�#���v���jL�^#�I�M�`Ư���^[җ�K;�XED������V�Y���Fs��tq��e% ř壠6&�_�?�3�l��6��p�Ɨ����O�v��|�	Ȓ�w�`�@�sڕ%�EI*�[��3S��P]����� z�~#(�(P.�i��1��Hւ$G� yQ���m��L��|��~�$E��+!� ��s��.�)�Z�/����ٖ,����k�sb���@5��l���	Ѯ�\��<Q�����l掽<w>��L�s1��Ow��v/˘�V7�W���c,�:�S��C�ܮBX�i�_X�<r�0������t��7A��tFNRHW-H�'H/r��b���̓m��K�s&^�0�Y��@:�6��>�GY&���|��o�$��S�Y_T���U��ȗ�:��)�IwP(_���M̺�˟��~�؁�S�=}\q��*�&P*��W�O�>���toUȖ�n�����������$
����iL�خ��/�I�T�l�S3<0����K]z)��4Q�����C��k]u�?�>`й�+��t�Ѻ���w0��#WM�
R�v�lLkn-���Ĕ�믠�4�f�[��	�Z&�x?L$j�k�^�J�V(lZE&Y���O�ʿ��0~��&V���yo4-\�BT*0&5��Ɍ�[w��M��:�ke����3w�ndi��=$S8��������h�v��w/�Ep��ͬy��7dǏ��̽�q�H̽X��W�v�w&��>�M�?�sk�����o�����&U����o����fo�>����@���)���7�z(��������}	��=����!����Ϳ}�b�q���Ve�]U�"�F�]�����#,�]�Z�QkO�y�C8��Qm��hEE"�P*��A���S�_g�3=�8�cId|i���17"�m�]!�5Y7���>��xq�����b\;>�	��!�xߺl�R1,���uw&�׏�W�{���d�3^�=5%��������tQ�un����,*�=��V�j��ӈ���h�S�&��>R�x���@0�����K�k�����o�Г~��2Ŋ׷��k��{��#�\
�ɦ��������n*�A��/Z���1"C��>�5:�G�D~��=�A�(���Pe�[�zkـ��5�!���7������oqN�s����Z��q+�~ʥp�&Tu��J����<=/��#��.���k�o��M�S}$p�&���w���1�c4���ڇ�=�i�(��c�uq��A'����޵��k�n%� �������Sx���v����3������Y�}m `V���S�i
�s�6�n�w���<��Z��!���4�b�Y�T�1�����8ӑ����f��թ�����+_�N�@ʠ;�l1���ҹ�l_$�Ci"�#
:�V�׬22�
�eKWE���`$ ���^��Jl@�}.5�ye�yX����"��̭��Cw[қ7�dq�9Rp�Dl?"�1kK��ȃ�joA��"���ﾣ���s꟟&m�Q�Ka�s���$~f�ua��(���9=�%3"_�(׸݃�)���5Do�����I2��i���8���ߦ��OJO��?Mqk_ �#�>�3��ޔ�0�޴$�dg�I?����i���A�#t�k �1e6�8�({�kp�T��"�3EAc�-�#�v ���
5�o�js��P��9J���狰~�A$�c�[���ʸϩ��-ɏ�7h��"뮦L��=w^���g�\:���>[��Q�q3�i�'����_ۂ���I�m���R��yH�-P0�V�U�&��-�k�6$k3��:3�tG&���8�T�1���/�ֆѻن���b���	���%g�tO<ch�>P�©\Ե��T�;����-��Mާ*�s(�Kz��}�9E�@�`���K����X��,�Ӧ��r2G�#�dY���9��bo��|i��1��m]�'!�|�(�·����£��<��s:�x�.&�3fWz�״�o@c�P˕��*?����\������������
����ؚ��%T� P��J����v������D �?�9���1��尃~���L|�EK�n�:`�FzJ�,77tl��i�@A�Qz�j%�5"s��P����{1U�����HV7��2bNpX��,�>�#�~���������(�R@N�V9����ck�g�$��QxQ����n�Y�[d�a1,�RL��KJ���6&b��.&� ����t�TJ��%\	.������x��:���Mc��
�f3_x�o��/��8;=7����ޞf,��+O;cEm�Ew4�2����������2X\�F�P�G,⟡C7��0����=���`� �"|���W�OuV2'L�MmY��~C����!aD��s�J�`0Q#��l��@5?��Hխ�T�5��MT�W�!S>��L/�@��+Tt���m��h��LN�D9���LV�'�ܖ�[pHx��*���t>����{i�v^���)=��[�X�~�y�.Hz����dY��Jܶ�
ҎE�9�ȩx��:�^ш��N�y�cG����!�βϺ�3�I��MDT�I��Tb=�O<�SR����3�9F�2�6��3��/�l�a�F>�.��}3��Ȇb���i|��»~EzL�P�Gb?Ma�#���x��<5>�fv��h���z 2J��q�}G�%�c�� �!i|8RZ���Jxk8A�7��=���3;�b�+����T���0���:hP��3
Pa����7��V~��&���
b2�%�
��?0�����p����mܠ�a�����x2�����T�<���se�lWP&^���	!hiچ��uKa�̕ײHrKu�����i��{A�MNE���(�~�%T/?S, q1)#_�]��������ʁ�E����=6�9�P��-`_�L��>m���F���b�~^V^�4�	l���D���aӽ�낥�����a��+�|F�M��%��?e5sJ���-CX�n	YZ����q8�����dT�3Kׁ�:�m��b\�qA�f�H~��c8��V�%w�� ��r+\�z�l��,�.S�-&��)���W�^��҉He���fc��	߳��Pּm�������J'��E1�����'iy
�xB!�`�"X�@%�+���Jy�<��,��ˌG�ӸwN��n��y���Z���J���x��6{��D b�GEZ�q|�r�qt�Ȋ�L�jw�ԯƚPc�!5��h�B�1�ɲ�mIG�	��{ܶ �3��O�ϸod���X\�Y	a��Uf�-�M�Ze��?����_ߌ���O�� �r��ː�s5�E�K��\��� B�z���g3�oo��An7�C"�|�]y���֡_�B~��qOዚx{�sz��C��*��;Q�    S,aZ~ǫ1��1�aA&�$,��V��Q��<aQ,Rs�=<����n�^b'���G;S@�N�2��������Yӹ�̪ohK�|�8O� 2,�7,�������\n�nƳD��Ө�I���ر�s���h<���p���f+������=�/�J�]�H�+��C�qX���HV�.C���������:��m�H)�����Æ	�%�m�2Z�q�6��?�����L�u��aԪ���=�maΌ�`��'���U�xo�u>sz.8�tD���K��S<�a��9�O�v��x��c��S�L��]������a�e�HXP��Z ��/�t��1c��TI˸ѫ�~��qj/\�jUú����L��+j�w�#��$�T�z��M�RV~48"zq�!�@K��DM|FQ� ߳�t폣�A%ن(��Ǜ=!�7�:W��xZ ���1�M���>�ve�~����ԇ��� �L�q��9A�{���>+�_��d��D�b�&�z� �a��9[m��Z�?�K����3e3���}b;
��ɐ�Y���%�;ۂ�����>QT���c��-�Bk)㮯�����<h>�[��?���[�oR\��oh��	c��i^��Z�X����hȮ��
��3�g���;��\��gV�lI���cj�K�:5!D�.n۹��S�°�9�^2��i\8r���#��a�r�ֈrۊ���[��"����0~.\K��̲����a�D�>'*���lDq�Oť�E�5L1&A1@Ar�;t�ܮ6%�h�-��=wNY ��)�\�A�(�y���D 4p�&3�@+#XN��L�vBꁮ
��̚��k��p�Wӭ'������.��u@�`�@�舩Q�� ?��<:W���p��]��I��=�J������,!��I7��TP,$ �'�����4	1�*H��h���?!���u���H��6l~�g[�����?�~���� Z�&�+~|������k�V'���;"~��oy�S�BTq�C��%`�{ts���ε�=vh
Μ�Y�'��$��a�Qo,��J�l�A�^��!y�=HLf �ň{Qs�Pnρc���ߵ9�D[��\��@�O���WJ�x�l9��ܕ+rI\��J}�w
�4�q��t�H9��tlt�M�Iʱ�"��vZ�~��d(k���i$�6p�QP=P�pUq����v�`��9^��o �X�g:ؖ|�JZ��ʉ����u�V��Q��?�*9�@%��tɪ[H��o���ǌ��jC��C_M ŗ(R�ӹ+�pl~�����Z��Ŭ���|gَկ�A!<��|$½P��f�r~�'-	��+��(av������('耓' �z���y�\�	�q��i�m��f��+�3��fUp�d"��{���cр}'h����^b���u���^0m�����\�c�'��Ϛל�"c��㬉�wF_��J�[3���ʢ-%#��>�I3�(�����z�w���J��;�&R�Z�܉�V,�ӂ����=�H5�8�vE�3�o�7�<[DfU1�����f�#� .&�Z
��="��\p�%T��|oe�Cy}�z�ֺ
2�qM>^��v��M�H�5~x��@�^��b��@�h������I�'��l��G�P���+�U���z��1��i��Z��������Gl,�4�t�F�>��J���i�2�j#��TKؗ�`�%ۉ������廏�dG�%�A��(���Sj�:�4��6=��ϊ��i���#��m�a�I�D���g��M��}�4͟o/���޶���2�oX�bc���?�K������#�N�2�>m����x*�s����^�۟3������|[��ome�qJѱ�o9���:�3�.�6�o�"��J��[�W�!� �3p��"�..*2ڮ�^���m���%樾/����\�$e,3�k�qN�D���udW�,��
����L��*4�	NL����G�0�/E�2i�)�}]ʈ>��AK�*X7Y��!J3���o� ���Ly0�R�����!���ȾS�/�r��!�8:�H��p�K��^�<��<N�<�����{��؟oH~�,��~;?�|�V����ax����K��]i�7�[��\q^���gICΛ#�-IcX&S��̅��nj���Bܝ�R�����>�0��ֆQ^Q�����|\�s���A�J$����{�t\׉u9~#ʦ���|}5s��\�c���t�w}5�g�S};���;2)�C��Slܟ��ُM�$�t�Ag	�"�
�oS����Xm$�I"�G����Q$S=}�����Wz�'`E�["��xN8@-֬U�Q� �����^�vY�d�j_#$1�/��E���.7T����p>n�>��N� ��ʘ[�~gmR���<��s3@�|�U`����@��p���I�0x�34k@����y�N�)u�nz
i�Q����sy%�\����K��߅ۊ�9�67M{��'(�"mR��uy�)�.1���E�n�Ga'�8`���b��M��H��"�K�� �[!���#�4$���6��ʫ��d<��� �w����OOC����ߤ����aA�4�{���({	2�IB.�e�W	[�
��f��I?$c�@ɥ��4x&�����7!��z:�iX��x��&�j�U�Y�J�F-:�6~BN��A����:j�X��O�f�3�G�|���Έ��'��.�u�`���J��`��luգ^[ŴX�@x:�$W���{��gu���dIS��fxeE̚��7'�}���֨Hh�'��ɯ�
w5����E:x�E�z��1g7�%Ir�7��5�,��*������{$�ά;Nv;v�P�I��P�A=!�b�u�R/s��xi�{v�F����Y���l���z�nq5l��u�gƢ�<jA<��.�I�C}���q0�%��Tbe��c�V	�[��3�ԛ�
�Fد����e#*�>�AϚ��u�3�����1�C��oQO��&>@�hT���q��YM*��c�k��=��	���%oG ǅ��ȧ�AXF#��(2��N�
{E�Oܓ�� 	���E���ɷ�y��0�1�]r�+�ӏ��8�����7d�4ɈA�dn�j��%���J�U@�����oj-Ϧ�0��=EG�$��b�����i�d ��X�0�]�Qb���
�����'yH��?�ٙ=�8K� �Ց
諍��0z8oFh�!V1��T�7�H@n���`�;-9���|*��;��D�']7c�m��7��g��M���7�s��T�cN�ft�7�<��s��~�9�'��޷��J����=�Ӌ����^�u��~ojվ��=���h�Mn�g)o��&P�*��~m0��� )�����E������k�+&V�Q;��5���M�3�f[�/�k���O�_;��'*��i���
�v�tgf�&���G��!<m�1�wXyj�B`q��o��ř���d3��5�,���G�5b�����4��ۣ�:�����Y ��;��I��E�g0|�;�^H��;���7�~*!]�2DV�9H5˸hg���,��T��]�ET�U[i��i]6=]�PA߀���2-��Fq�I찪f���p®r���BQ���i^� .T��k+B�n
9�`=Tt>�m.���P�h���ln�.i����Ym�y�`i��^�&^-2�#6��U9�������Q.�+HL|�:�>E-�j�#�n%p������&�z�L�����tlA�d�]P��~{��:�}��ל�3�$__���O���@��q;5��5��_���1����n93�jh*�7�|�6�N\l|I��s?\���)Nr�`5c_���*Ԇ�r
�si�h#Y����.��%��[!]�����O!>ֶe
6ĸ�ؽtjo��QL����[�˒7�n�Aધ��V�@A�3}qɞz6���$�W��X��z���l������<�T	1\�Q�����Ѽ����V�*����1 ��諡��V��p��]~?fe�ZVYux    ~v��T{݅���YUڏT?9:�R?���b��E� ����T� �D�^G8o�^n�ϴ�x^�?>�&۵I�\ �T� 4Q1XwV`ފ�\����d|{r��7��!&�:����c��l��zڛQY�-�Ԃ��<_*���v�(do������:�Mp�C=�B�
��x����~��R��C�^?n�>!c}�l����iu3_8�ZRyJԧd���A��1�y}��=˜s�����a�}�7�h���Qx5��-i[=��Թa�O�ŉ���8J��)��ㅣH��D=%�>!��b�8'k��_#�gMGt]�Tٻԇ�}�>�z����s�ϽKa�O�{�>��,-a���j��K��aN��)�xQ!��J�&�$��m��b�ƫl~��u�����`��kA�ʷ�){��;p׸T �!A6�K6`���9cY~^d��EUW�Hƨ����|(��$��b��V*�;�vQ".������qZ�����Iuq��[�PG�U>�7�����y���*��#)XS��7ܴ�K�<�*C.���[�w*��C�l{ұ�MUy-�ލ2��F�w�N��� z��I�^�lYJ�����N����q\���YS��:Ũ�=�b���Hۇۓo�}��������ڀ�QQ&\�g�_4��v�ZE?�9A���Ȉ���?�k���=�q�R����[�����h�<�3ov�vs�`�b����k++�ӡRewQ��G,T6��9�ZVH��'�G��$��r���\���Y�Y��f�_� �� M�Wwd�o�GlO��gss��Kҙ�{�~3�Ћ����p��Er���oŐȀ��p�f�=XI�e��~�>B� z�W��P��w!Z���:��puB���]�S�D�2訹,��P�y-*��Z閼��RF27:Z�曍���Q��	���p"�	�l�pK����W��!p#���]I1�ƒ��w3P۟�:Q�kڌ��zF���{QR�̬��r'��WV+�Vl���$⢘�)��.�xAI�aU� �v}��@&����^�M���v�]� v���/�ry+[�"�dw�p�ۧ�v�����s�]�Ϩ1]}ٴ������HQ͈ā�#(�B��t���}��S�mk�-��AVx�c�3��GX���{{�����Ș�WߟB촹�I��41;��4�B\��>�3�f�-+�}��(�w>�
��'�X�0��W?�G���`��1ZCg\���H̹�-��E�H:
�ʑs\�zx�!EB���W,�9�`�)@�^1��l��7�,�Y����������L�0��4��C�¶׽��	���S���&�d`���܆��Ү@��IP��Ւ陪�%g(FȆ˛��O.U������O�&�R_+J+fM�<7){&��Bý���O�Е��Ɂ�Պ�y����X3�G'���+q�_Axk��1�m*���CB��m�Y����~���B�ud�j��Q�WI�'��M�7��|��v/D��ü1E[B������)8?��t��lMڪ������O����D��5��'��`����]�R�� �nG�Ʋ�K���rr�u3��<�'���Y
5�e�	�_�	@�PU��%�N��J9P,�G����R�������UA�ز؞%��� ��Y8?Ը�f��k�=����c7�	2PP�q˕(\3����3&�r@�M��� CCo|�����~I`�	��D�<X�����x�M��5Ј@�4$_{C�Pr�[�O��ǼhwKF�k��z�����ȗ������.�=٦'̭!�t55w�r�.����NH_��6�7����$�9]梪��ܸ��ʹ��hN�`>2��1P���'�(f�h���_�mL��+ߍ(r�wI��*�n�n����Ϳ!���ݏ����^ߞ$��E��N�|OZm�|Q�IS0ٷ����9�i"��+�<k�}L��g�S��퓹ޏሸ�p�d������c"aߺJʼ��b6U����		��������9�{�z�~"G�m3Z�y��w��|>]�|^�����@�<\�d�� ��St�|v����C����1ड़H�n�^[��_�M�/c������ %��,U9/ȰHм�����>�� ����h;�.�[�jc����@�%����n����뿧�l�
�Dؕ����R�ځHp�N������D�z�r^t��D�P17���߬_oG��w����I�e�nW_���l��7��g����zh�wE"/�I�п'��&2���Ci�#C��2�B�3�>�b"����N�D���C�tQ��h�Mk���=��,��`��)��7z޶)+]���z��j۶`b(�1#�v�o��m�f�mu��ɔy�EsS���������=��h.R��+�-D����'��H��I҆�,��˰��^mс:P�"��̞V�Et��� J�$tΆ"�p���¹�������п[1L���Mg����J��/�H`�i��(E��u/�yҳ�ӮW��N���9����S��s��dY��L��¾��<���[>2��p�)����/]����wk�Ѵ�׺JG��U��L�9��DA,���`���O��!�:P/���w_�X���#�S�!1Io��x0�����" �b"@����:��J�PC�ߜ���H�8U�w�^f��,y~{�KG�zo��t�[����W?A��0Du�|���� }��7�)<��9����[d+P·z��c{L�o�xa��#7^:A�m�_�D���fN���S!���6N� �>?z�,�)��m�3����Օiѝ� �A���1�&q<�Q��>�������L���G�ZD�g ���I�y����/��Y��2t[9�i������5�B�RQ�7�0mo�`	)|����� ��Es߷�T�O�Y�tߠd�ܢ�P���)9'}�T�}C�c�k킑7�O�Qv��=���7�����Է�'�

l�w�b@\N�"�G`X퐋8Tp��[>	�>�] �� �!}s*�J��&:(�߾�i����e����M���@y0�BN ��4Z����l���^߻���%j�ӱ���e�z�E5{�EZ������������a��t�#�͇(�j�v�ӝ�p-��3ʭD�*�Z7�>=M{<b���\��H�_Ҋ�k�:��"8A��v}�����52|g>���٫;��J����q�)Y�5��6y	�$�C��]�gN �bD��N��uf�@��~}�e���4r.�:����@!���`�����6�W&ȓ��[{v]�0�i݉��/ia���H�X����7��W׿����b�� &:�7I%�|`��<�ɼ���iˉ�
[��Z�lU���'��}�w�F�[�LQM�	4��b_�7��0U�HRjU��U[�L��(����\�{�b���W�g���>L]��?���?�GOt���Qy�L��4� e &��pPoG�@�%�)�!������l[���}<o�[guh�x�]TG�7Q�ro��y~�$�n�hmRɷv�"��g�@�Ǭb�9�L�6�@�P��c�-�s��x��1���m�=�#;mG+7`ۋI�]�N����rw�-)	0��%G%�9[qS�W�oј�'`k�X9��!�o��4���M����x@a=��=�9���Q�>/ %;1�}�N� ��|�b|�.�m���8;�b_q&��Yq�Deko3����K���C{Aq�٩Xgm�=��s9f�َ�`�ŸKi�����>NF����N��2{�?��4	0_]V�0D��ԍ�p��F�莲��~u�}���4'3.5���y4�bEl3���-J�K��vC�'o����$_m�6��[�ܴ�|�¥/�%�K�0��`×")1�pxv�� ޲�(��kdH*ۡ��j5���P�r�,B����a�?��A!�ǭY���Qw���(������^���ံ��hPw�20�    ��c/�GF���^n��-��C68\{e��Kp�&�Q^�8��;@fBc,R�8^P��J�����m����#H:����7w�C��ϱ�5�y���n��Y��oEY���R�r:�3��)�����6KfU6NA���$-��b�I���	��$���~,�+��4�a>�x_֗B�v<�v�\u�,C���I x��a�3~	z�x;�Իt��_Uj��C?��	�khF��KK"��U�e����Z��aLw�Z�5z�;��^w=W��_;��\�����O������u�8��u��3{pʝH�0?�����҉Zʨ�v���g���N�~�jtTcϚ�/���2�T!����#����Y��_,Ga\^�\�s}+w��{�yl��`=��JCBtMr`7�%���h(�5,��"�h��љ���l @#﷔v3 !����BB��[���k�'�:|/Q}�R,i��N��z�A�(�#7e4}6D����{���%���������|
��4�8�cǤ�a�p�85����)QOw��3�6�Lx���~%���A��&{D��{f94(,�ؒ�@��H���e���H'��I\!GP\%�B�븚�\�/kK$���n�U���P��pg3����Ǚ�_��[��	���p���劝t�Э$�e�AzsQ���j�_��VRnDk�I���ۓˤ��7R	��{8쭜o����+�M��yp	���ø�l��m�ި�[�fsq]}��:�W]4���Rbk�k.�ƾZW�]��隽o9�u���t+h��h"��/&��Y� �q�;�f���a��
p�=1�W�3��o��v�T��t�԰6ਜp���n�FO�}�����y�e��;� � C_!Mm��W8�!��,���g���\y�f�z�ҷ���W�h�c�A�]`�
L
�nG"�Y���\}��V ݝ�-t#(οd�_��֤�\�L����h��H_(r��;��~_��}֔4�u���0H�F�jn������u*ЕQ��n����+i%����"���#V��vqè�l�`\Q���*��:"�Wtz
����e!�U��\�<;��kೝ6���e�UN�݀eχP�OH_�b=u_�<b�����K9	�Ȋ�F�F��#k�2�����a:\��˾'��B/�� �/'j�q����f�:ƌ���i$5x����D�� �{�����>`�#�*�����*�S�8ZX@�,Y
��ڶͅ?�2�,�z��5���UqH���f���'�ۥ%�[N#�GO�=F����\dɁ{�� ��si��N0i(��Q���+���{�A���,Τ�(��'����}�b}G�nB�@���O����A��WK)�R%uл}�/{E~:uS]��"��q�R
B�IH�R'�7��;
��[��hװ��7�2�㜎������;f��?�n��&�{�eK��k�Q���"p;�]�#��[D<qȶ����&�z ����e�J_)t�5��=E>_�m{�����=��D�G�9u�OF��~m�ةfI��w�VW����\7��6:3�7��e�W�W�'�d�[��¾a�"G.���ո �2���[��F��������L`�
��O$"�*�A�Co�n�KX�$\:�{��U��J��7��0~޿X̀p�xШ���F��������=$��,Wk��y���.��\t}(�EԲ9�y�H�������l�o�u^܇���]�2&w/��S�r~6��v�8��ׄ��1�'c=�X���|4@�y�F�l�tI�Һ��/�M���7;�p*���!ڇ�/����!*���2%b�`drp�U�|WΑ�-t�����=8���m�	~+��̆�F��3߽�h
����WC���Y���b��35������}I�~,�%��? &��nA�Fh�B������5S��Q���4��Lܻ"����0�T�F(#��s�.1��jLm��p�Q��H�=\���Mc���Ї|��:���Ɠ�~��I�<5?jг�Ƿ���;yF޶�P�8t���@Q�'�j�°�+��,�٩
��S��Q��I9j{A�%���o}MN'���9��*4�5�m(�d�a)�Y���v'&L. ��s��/������A��ޙnb�j�{����J#񐽙B��:*�KtҠB��7z�ރ�����xe�I�˯�BE�[�F<W9�`��̋tS{�R&�n*'C����ap4��T |`���˸���njx����D�w���/������O�rֳ���F�
�A|�j��?�IVg��t<D�#�_c
��������۠���C�����Wij���lrbX��T�h�dY~�1esE{,Px��|���h�e���1�><\ߧwA~pL�c=Rυ6�_�{���ڱ,3��7���j��Ok&u���]���W�e;(��_�|	ɘ̦��X׵x9��Ϋ���̄BҦ�gcVu#q��|Uۻ�S�9-)i`�@�u��}n,���2�`�w�(����bH�k��\}(z�Ⱀ��p�)�N����7����3�<�PZOdyEk%���#�%u<E	('��ድ<��x<D�\�Ϥ���?U1��V�ҳ��ܡ��p�B��]��{�ej(]=��mM�'ܖ���(=ɢ�n�QT$���m��Mt�m6<+���8�X���}��%d���_�lp	^�m�p@�����c��x�ݞ5l��6}پrNZ�Q�9��@���F�%I��A�ݜ�`_>i���2#�1G��Ϋ��,T!��6�d�\�P�me?`�2KY���nLo�-}�%^ ���
[S���ւ��SK��������٩��i�{�������xۊ�}�n�I1�&�t$��m:}�ؼ�ḋl�/ш����5���*y����W%5x��&R��Yu��9:8�������8���/����3�ʩS��q��v�A�VpO��7Q�sdK�Z&i+_���z�V�9Ti���}O[���c�=li�+�5���Y��>v�ǹ�o ��!]L8ʚz��I��+%�� �����,��sc(k\ 1��<�In�#��Ϣ~�c(W*e�k꘩p����MRb�|�b��8�)O��ͪ ��%�ss\�PpU�'����kV� �`�~�UK�F��Df NB,H˨�7f $5��9)�q�@i�=�$,�of6&���p�."C�������a�±�d�tW���J8^GwE������}�)g�k��g�o���O��=k�k��p�S�h
A@��ˬ�ba�ˊ<���T�ҁM��Wo��$*��C� C��}��_�\}�+����ؒ�d5�Ž獣���%�"qJǩ���y���ʴ��'EYmIY�O���b�Nh5�f�7��Aaz�'ٸ+_����pD������J>h�CƗ�6�Ù�D�AGWGh:r��1�����3�8�<�ĄAȱ��[;�P��"y/X�A�YX�mUԦz�#�^8����B��V��lEaڌ�k(F�'�H���*$J�]��C�$s�^�*j�]��޹��G���c�t�����ON,4:�d���e{PhNp)�ɋF���V���H #���k�:s��=[��:i��%8y�i�G-CG�h�k"�o�=��������e������e!)�=E$Ԅ+�YV��A@Se�nF���tH;��Ԛ�qnA��R�n�D��Et�=�A�"��#= �ᅅE�����!��Z\��Y\xe��U�BJ*��Qn����N��p�E�[{����x�OA�n�5���P8�Xh��B�>C�s��`�M����O�C�v̙,��rL��qcXto�8�5"�A1�䑗��(ɛ�yt��F,�x��t�H�.w"B	�I��f;\~I��\&Ir揈Kr��V=��m�I6]��T?��L~�ߩI4��Ͷ�"#8`n�ٖ3�=��H�z ���G�ț��>    ->;�(�O���=my�H��( #�Tz��E��6_�m�.�C�KY5�}z�h&��)���5Ą��,-E�����_�ݧ��;�@,O}��Ҡ��r�s���YA_�=(�7�������&�}<���Y5{_|�<{�	<Rp*�����Gq*�$7)J@pX�-���h��ׂ����Y���S��м'�JI|�'=�,�,��{���w�KsL��U�EG��sO	�lv�@�gP�F��t��o��7�:��x��W���,�X�#>k�5\'�gT���	/t=��r:͊i-Ir�8�6�*;W�)�@�Yk���N��$/L�A�,�`x�,ll�~��ً�϶�7�{ Q�O����Ķ+��*N
M�����9ݗ��u3<��V����~�2�jQ���`��d�蘆�Oy�F�t���!9/��,��0����pa�)��|�Q��S�k�YyE��5��0�ږ�ڮR�]��["T��n�xk8�5U�.�w��Y&�I5��AVub��b+Jꅾ�lڕ�����-�/XV�S[d�y��7?1--����n.F�Y@�bщ���H�5$f+HXz�dqL��s�$b!AO�A��^+h�	��.��s���=����.@ ��W�.�& �  "�l���L�VF���S�?s[e�w�E��<���n]�gUwٙH¯1�Qnh�����V����X)�w��62Uj�r�� ɴb�wYJ�����G '�R�]1�}�b@J��Q[x����G��@�h{``�U4g��5�oIJ׌>cG�3Ø��#���Dj���I����I�OWL捕���3�ư�;n�a�%·�ho��:�b����f�Z���2���j��f�oѰ��T������{�H�!��M��Y�/F�{E����J/�UL畱 ~����r��M���^m���il���+��ԗe�XP�1ڧ�q�w���j,��]�-�'��1�Sa�
_�,Y�GWq��2�8�'o�csց>�`],n�ܴ��D[�����0�qNɩ |s�W�/-[ƈ��.�y ��r�+�߂䯘�eTަ�<A��p�ey�fT�y��D$��7�ے5� ��P>�q�n~K�.���j�nc�u�SD\�@��д��3�����\k��Yb7�[�Tا��G��%7���ϰ�* 6ψQ�1���u>\8�W�R'�g��6���.�+����~8t�G�z�
z��(�^rW�;�4��.g��b�eV,VV��ũԓ�������L�M
����,)���ǐ�[����};�E�[�E�*�-R���q�S*T$i2���Fĝw�%}�oW�4mz��ִ����gEJj�E�yA"]y�iP��@�����3�G�_?������K4�2�d���>�h�8�s�m����B��w�nJ�O/�i75W^]��5PۀE�{~|����8bt+�_JL�^�Z<D��$;�;%�q��[�DH�\>#����1^����u6N��
*�I�;�%cj<P�yv"Fr�����_��ՑGy���k35O_����^n�v�D�� �;�%*�7�#aU��	�`����WD��X��O�O�ld螀�o�(�㮿P�U�|7��\i��Uej� ���^�1c[��Su���C}x�[Xq4�y�$�_-�%t%�/�т~��\^�����m�n���&J�f��׻�)���
�u�H�ޚ�x��	���T{�Ѧ1�x~��&e�y'6��yo\Ͱ�1���
ͪ;�Wb��3�_;1a��r�/7v��iK���Ċ�2�S�G�6�؏w#n�g?��F�5����'t/m+�I?�����S��\�Δ%�]�=��~_�y�Z�'�<YU��)�Ҋ�w�L�#X��qx<��dɄUF�����X��:�B����[+}������b?��ׁ�K�$�Sb �@�v/`��G�A���������L���]9H����y�ȸ|�B�iP�q�q�o|�X0�t"ƕ�51zJ+�ۼҎ	uZ\������]'e���G��+��[eyZ'�}�F]$��M/諶o��Up�i���d�v�N�j<`��PSq40"};�ǟ��f��XZs�Xwd�j�K����`�e]�Y�t]�;|��L�/�Ժ����i+-��;��jTׁ���j�1ce���0kwu��}TqT�̢v�FNQ4�(D�$�M=�(<��L�(H�(��y9M����j@�����D����'"Zd�w�g�Juz�05l�I�u�ӊ�ku?��r0����
N��f��޾�{���Ӿ��S�ˁ���*�BQo�D?��h_;AH+���r�	A��O�����h�|�+�\� �S�xZ�w�4��[ٶX��8`�Ȍ�9xyr���ޖv �M�9 }�W�1��;��*��kv�]y�q���>hS����j��B����ܓ@
4L���#��Ҧ�ߦ�"��"����e��k/ձ���Y����h\�e�gУ��>f��s��A	1L���:U�'��~���]��X��	�UȮ�j�������x.n�y/�aY��WO(�L�IQlt�0��٤̑݀:B"�_�I�<�G�Xv�qZ i�&[R
R��./���b��ք�$Z�DK�m��"�|��{��:�jb�����:����}� �$!��bKt�
�?��E�g>���h�Ux��u�F��~y��S�@r,��I�䙊�D�.t�e@��B�tR~�*C�wH[��U���E�{wU��0��)Uw�Hɀ�,Wy�d?��T��~W������_~��< $(�0|"�lP!��mѮ!���6m���]�$�G���G�����Od,3#˂�}.i�"�<��l���f�C�8d�lٍ�d�Dﮥ��FdL�x??�un�7��R��:+��`����6���	Xy�\�}�cC��i_�w�.��Z_Ut�ṙV��?ȴ;�4;����ihT�9�kU��3��ÔK�]��R�	�|&qη%!m�����@�P9[2�lē�Mb~�C./d���X��WE�� Wn�&J����d$�c��P��$y��f���snOŷӷ�Oam�Ϳ��E96����^��.,p%����۟JBl�1"f� �w���ڗ���C�n�&�f㹅`�����N������Y~0�_�qC�����d�YP[z��Q���H]?j�����$&b8c���g�����5|��n��F�t�M�XjĪ7����ך���V����*��oT�t|���<�����,j|����9L.��}b	�#d������/�UfhQb��P���iMfX��E_���\��E����.�I��y�,Ч�9�f�N~��8�������n�GC~���A�׾���F^�*�r�h���^�fP��x���g[6���_�K�u�IHҜE�����}���ݢ�U�h�ߞ
��i|ȭ�wJ]��*n~g��.qV�/�,P3�]�I�߃L��5a��͇S�xͻ�����WƐ�r%�e�E�O�C�3��M�G�yx�z���2e�� �B]��PJp�+����WT�q4}�
~d�,�|���}Yte���;�R��7�dlơ!A�1x���PQ������.G̯g�=����.��J��M>9�vh��qOQ�R\�9!/�)��Fq2��Pg΂'��U���d���<z�.Z���z-�~��V--�-�5��r�f�����
�T%�S�$�J!|fU�e~�.�]F�7?�;TW���q)s��O���a'R���N��*�9�x�O�Ҋp�`t����%B�\�s��@1�0���mW�-�Q�`B��v����V%��|����C�[�u �xp�i��MH��@̄㼝��zt�6��'X)�� �3Adá���K��-0.�����:`#ٟ��`r7�tE�|���t|���Ό���k
?oH�;Um7��=V��Y*��$��B\J7�3�vF{Cm{����JFOH�`��t�N�ǈ���fc��ȗ@)<'�E_J.��yV�S�7�W��賱ȹ-r��o�r��    ��z3C���i�c�O�դ3$��+3fR؟��,�6k����C.g���ǩ���ۘx�&��~ϋ�H�joY
mQ@����tus`Z�sx_��ב����k,s�Lu �)H��I��Y���8��I&�Y(�oj��Y`l�<�>v���/��m!���m�o�鴅�����(�.��f�{`�0@�Ha�E�zM��_�1�&0����z&��bs�\���2Zqɳ�ύ�ylp�b�3� ��SP�ԪZ1�̰ʗ<t2��%9A�����>���������.�%�K�{�q<�$pn�l����Z�j`*�Р�Ғ�ʃ~�rg;�����E#	�"�e_�[���Kw�W�4O, �$`lm�DIw�7u�qZ���}�
�5�k��0&Q���c�m�+��j?��)�o�. ,��,�
������8� ���p u��!�pCD�|�3�W�L���L����~��C�`�
���tV�P�j�{� 9s����/��c���~W���4fe F�'���ѵ�"d�s� 7�N�oBYn�\�K��n`�'����Ɓ����0��z�L��OR�bs������&�98֯�ZHX��;�1J��t�5�IhIL��e��@�q�OU5H�8�LB>��~�#�]Y���h�ey��÷�~��䀰�o��󂎛FY��{E�y/n��BQv$�%�'5���B'�_1��K∏.�Um?�N_�o�J?����坝����cD�����~����P�h��6����9��Zl��!��x�p��4�r{�^a_�_�㹮��m�G�sS���[k~��gÍ�l��ݼ�v�&5�^��f���uzm!�~�j|��m����ljN��m?^U��I)ߘ��Wœ�PG�a%��16�Z�G���Ӻ|t�(�l��Em�W,^eخ����dM�"�ʣ�̈��]U�hٸ���~�1j�>���l�AY:~Ԃ�LKB���!9il�_L�`��;��S��r�c�tSh}��q��� �a��a_}�E#XΎ}�*��d	����W�]%:2CI��̹R�a�2�6�P���a��H�ǹ�Q���JX���=T�\>ڿ�V�O>�*Q�0�N|�cZ-�79N�V���&N����Hc�5Cچ%}��+�l-�08ڲ,͏Q���������F�W=H[�h�[�p�#p@��K ���h<�k}62�9G�O�{��' ��W`Sv1�7b��o'����m��t:��f6��(P��G�g�m����2B�a�3�m���Jk����D�N�@�����0O��#T[�ׅ���amRJ����y�+�u����s0�/~��I��ڝ�6ˇw�+�^�|\�;�,�ǖ�HD?���g��F���C�3�ZPU�����(��4�J��x�r��Z��+C[�mH�@̹�-M�=��Iϟ�)������[U��/�F:�X�K�oJ[ np=ӗ��KW�eM	mi�Z�5�>x�9g�� �ks��&��5P�9��/6;7ܭ�d�:�A8��qӂ�چ���o�9p!wс���S����a\	܄MA;|��Np��j�oy���T�S��L����~L8��D�v�z3׹޶%��op��Mc�Q�m��}X�dyU����C++�ö[F$��/�y��M�;��"�~a(c�[S�(}oЗ��q\�F[1V_<��]'�
���C fA�ID��}��\\lrΠE��&Yy��UF�o�
�N���R)w0�BϢ�����InK�v,�� n�^���J&)&;����0��n�n��Q1T�6����s'O�B]�TdN���H<�R�) �^~�m=�E�[���,y�y[�F�6�����V�*��(2��68�GZ�`��+��ʰhx�"-X�=x���m\/��a�ОZ��$jbDHE��ŉ��S��7��Py�^Ԕ�]��~�ӭ{Kb��t�f'�-���]��M��%���w�%�<��
�������A��
K~���'�n�� �E�У9�]�lƾn{�)�{�.:���Ƣ-�H�l.T��vW J�Pۉ}�YDva]�v)1;H��5��x�D#�r�
~/W�A��*>� �7��˶ڇ���Y,ag���)�	_��F�[XA��ҧD�=ˆ\��0�j-�@8Ô|����7�*�D����}�k�ֱ%w4HS��U��]�d���ߒ���P�+� t^P����4��T��V�^�,9(��e@$@4�q-�c`�/&�/е;��v���U�u��&R��.>޵�c�v߿�x"�$=��;^_��!H�]zov ���E��v�Փ�/l�e������:��J���qV'7�PM�*��P��7��?]��d�G�a��7��.;ƔML�%��S���Ԓu�wv-�t02������u ���娹���[|�(+i�{��28���&^>

t��L?���A�܇B���[�W3���2�lB�7HR3R�gX���p���U��W]����:'^������vob=$���u*9?U§��2'v���m�T�R�C̜��f�ު꼸�\ôڑ#;�h4�ez�L�e��^��7̽oMg�GbF�5���
��M=h�e�	��N��I8��f��D�j�[w�6���f�a�K��˟�L'FFY�)]���'�8y��.ٶb��ɂs�?�Иz�%+ߴK�l��H�!�z��-M49�	
�Y�����B�o&��9�iwƔ�s�%�,�j� �� A&Њ����͐����_������1+���p1z��Z�o*`�g��}��X�4�n���sf��튫�3���mxf�l6m�S���@gH -Vė��D9�d��R��Y�Q�G��ۜ�Az���L�2�0}iO1]n*���ט��u�H�����n�=-s�0�$`��-y�p�EY��#����ӳ{ʰԈ����7ے�h_��`�w�z3����Ė~�����{=��H�h�^U�������o�&��	����ӝM���
��xys^��:M�&� ��P?������S4�9�~˅*�p�0�Smk2?,�R�Rm���X`�_�!�o�R��Zd?�!�+�!JFo4!�/�kc�4�7��QĶ��ߘ3l)E`�EQ�Ym��]�mn��!������WO/<�d�(�y��n�R�eZ>�s�����#�d_x�����[�/|��kn���D���2�z1�R����H�Z���w�9�����D��3$eb:�x�ծa�\��A�yW�����V�/|ѱ̖8� ��1ݰS�ιiM�G�/{���^�p�1l&[�Z�Y�X:pJ�!}r��U�[A����rkr�VGdw��6'o.�+kq%�z�v/������%S��$e(K^���_�G{7��9��U�X�2���Cs��?�b�s�$��m����LBA	�;B,��g�kCn��"��b�׏�?��\ԠvR���u��ID�b�ď������5\��\�:�o4^I��h(��0Х���U���D+��W!�����}�8�)�J`"����Ub�-�x��C�[+�	��l����E��\�0y����>E>X3r7��u��~�	GXV0�$8ؙ�ʠ�.���ζKQ�8���k�Q�"ç��"����K�}������A�m���B����b�I�����'�
�<pF��#���a�VO�8�gǫ�go�֬���C��Z����oc���$�xې9^Ͻ��,���oG��*ފQ~Y��^�Ӽ�u�T!�VA�}������]EW�p�[#��� ˞��)��#�ȿK�t� �{���J}�h�m}�9���'T݀�j���<L��������I@jA�������=��%G��^����/O:,A3��S��Z�!!.���{9}����洧yYL �l�C-h�_�y�V���V���dA����4�c��0,׌N�Oɼ���l�fS"ؤ��o�����_d/�:�n|[a2�&�7jH(fxTߧ��E�3#B�    
,�x�����L�H��Pu`LE�����.�~	s��F�|;���=��SpHJ���C��-��CSt�nд�1�&$AC�bH�5=S����f�&_����A�L����P�]+v�:�?�<ĵ\r�X�4r�� �c�X<��g��g$�T6�`K)�	j�ܸ�,4,��me���[�GM�������#wN?�Rh�y�Bv�+�m��d`��-��.S;�y���DpI7��6
�|��I��_�X���8����1�բܭ�:v�$[:��Ue��Ȟ7H�𱟀��Y�3m�^3T�j�����)(��t����W��|���?����s]��sRS��Zt��_�-H��	����t3�ԛ�-�I|H��W�#ו�j��d����>��.�4䴪�!��׋�V�����}_k�����n��Z�������T�9�=��������/W��6�%HY@�	SL�V4�߃�w��U\�3TZ$��^|�^��c�- T����R��E�_+a��+��Z�lv0�Ђ�m���Dx�&*��4���H�9�Ț��	/#J�<�p�EK�	Y�j6J����Qx��/Y��Mz�~4³�h��(*��y7Yn|�m�o��%��
������/V��]T��Բ�E��� �O�G���>�!Ԛi�"��/U��̎�x�S�*�vjҒ*�L@����t"�l��%$]�<��_��A҂^�=w�Q��}����meH8�v�'�5A��������z~Q�h��h�Gz�Y�6H9\b�3.װ�OȠNz LdG���<�,cЎn�d�Nkps�ZԬ�����o6�x�hk��u@Co�cs��I�I^�'d:KC�ZX�B;����L��� �iFT�`mU�߄���q��tT��d��xHq�᠚փ����.>T��8��	��S߼�\��g��������"��mXy�q=��^�I����{���c`�s҂�Ͳ1{PjLt<�;R)i�{��-�}s�7�tU���qi�o�U��e	�!�I����B���\"�M�c#}�q�<8AV0:c�dщ��	�hܞ	��X�ح��P��q!q)�g�\�C�Ӂr�y�ҹ@~ɚ�=�J��N����ƭ�!A���L?�^_M�p�Q��&<�W�l�<+t���f�e���\mR����sw��N檾q8���o31�J/;x�pjk)s�:�/�������	��y��a��,@�6�/���/��q_J52tOmB��u���kv��������(a�	P%;��Y��+f�k���F�t�������F=� ��v6�A�SuC0A�7�C�S[ W�X/ȁ�m
�6������d��o�H;�/��v�
�S�!Ol`\�G�>�.LNS(tw�����SA5¬����G*g��pu~)=��yA���". �d���z�0�����qR L���Ђ���B��X�lQb[��BT�P�p@��K��z��}����9�*+j���Be�!fЧ~��N�EZY	i�W�ȼt
u����_�H��*YK���7��:ǩ���X��KS8���M��L%��������q|V��^�uň���_ �����ں��.�s�������׏�y��V��n�zU8���"�1��^-؇��>Ge,{�KT�
A���z=p����}L���SJ�=T�Tm��ˆ�!H��ۛ�Ɋ��g�������1t�lY�@��B�O�������Yf��������&�t�_�"r|p��n6���B�:_��zT}c�x?��}k��-��u�d�����7up�mw�k�t��5*��[�rg.� �5Ԧ��.;����b�o��J�E�(�'i��*��%G�N~��N3�4*��]�f_�бP~F�0a;��/�rҬi"/���@Kn�EHI�&x\����U�ꁧa���D��>ƙ#߉ʒ@I�j�]h*�؊t���e$`#;�����6�ڙݻ��6�nw�|��͈?�;�P`{���y����	��fC>�j�ךp�j���9_\46�"��~�Q���k��Q���"�$�W�n��k{��(d����(�U�1=�JZ�MpN�;xL���;r8����0n���(��p�%��|�����9�s{�C���M6F��T�
�|�<5�s�K�኎�ޗ��w	<���}Y�L`r��������"E;/�[�2rp��C����'��A�+[4=�3�*�BP߂��1�4c��j���(9����x ����b5�*�7�9߇ �9w%�Z��÷TR@�2��|���ށ_��ٷR>>S�Y�������Bm��\�$�������rɇʖ�*7):[�x�46��x�h���:N{��gP���p��o��}g�s�E-%�B�NG �ih�s<�����#���1W��6{��:�a�!۷���y�+�L���˂{�fYI@��瓼�8�\��E�79���PH�����C*/,�o܇��22.�����,�Pq�G�\X����Y:I��=t�]�H�v�o�#gE���(�܃^�&�B�S+C,�6��g�x���(Y$�`����C&�$�=Ǹ*���:y"$�đ���À�X^6��z� D���Nå��Pe�G�} �7���n��%��o����צ5� ��l��:��M�t[:C��,�Y���3 �-�-ʓ�Ƚ�$�(��)�p��j��>����E�"%B�u��-�"����G��{]{�R��������];������W�0�2���o�o"����ߏ�G��~[՞~w���0�1�`>�<� ľ�5+�A���X\��p�)�� M�mK�AS��Cm��障��
 `j�l�c:�b��z�������T�d�U;��N� 8������}LFe�&��ŧ*����W}<��BB	����0d)����(\{̱ �0�+�Ӱq��	TK��is�{�	��gQ��U�2�f1O<ūvz��y���W�HK�녺��A��Oo��~W�J��tƲ�bU����Dl�Ъ΃9(�����?�3�A����2�2���n����	8^͑��;xl��o�ޫg-FEK�׉?g�ބF��p��g�WS��������"�誮���C����G��)��g��R��g7���KE��r��sl�w�yq��#�试S�����2�o��I$�hO��@4���r�	������N��{{��|�l��r���떸@D3{��E(I���hmd-k/B�Q��o/8X:A��o��7��V'N�L*�n���a!߷I��ۍqG��~5]uW�H���,'[��FvJͪ�G��b��/�=�ok�|:|�p*-4�Ī�[N�Zw<S0���� �\U��YӨ��{�����!�	-��M���{?Ua����I�U�6����i$/��%�;m�h0��>`6�Y�A3N-[���O�Lط��w�� N�2�?�� R�����`0�ʙh�0Q�b���g�� |vV�ὡ�Hm���	��}D��x<��e�f�ȌY�Z�WP��^�j҂X��П,-=��I�/Q�e�Ɯ��u�(�&|1l��8n�ő�s�P�����j��#*ʵE�$0��bl����-���d������^��Wb��h�
��{Ȫ2�k�o�н��iɢ#C����:q�ix��1*�<!�F����z��a���Ib�J	��u����8:���9��{mϲZ���j��yl{��Q�g(�޻.6�C�#|�:����Id�3��u-f2���\q��_�]�g��M03�]7� �M=�Ħ��Q���Ē�o�k'��	XX��ﾉ��>�m��H�4=0�oͥ5�T�3h�)x�$���=�,sI4�w��ް�\ژ'������0�����'M�O��㓙Q\��!	��aN\��益[ɰHl�L�F����xv�p}T���E���L���ۉ%�}�L���$����¥��e, �09D� D:`!��5ʵM���KF�Z�Qcsk�����!Ü)��c{X    �H�W:mJ�I�W��$##aK����7C9Y���n�;��wޛ��7��kW4�#��^P����C6��3u�6�if~yZ��Z�dr���[�"b�ώ0�;8~��?��\��ߤ.�6_�<M)���=�.]S2%���G��j��uH.�� ��<7w0�%$r���m��xR��:���?���X�@Σ��U��S�J�zbK�-�{`. ~W7d�9f����W�J���m�G���#�Jh��0L<�<`&���`����gO���\�����sp��..[�=�����#Op�E`o@��J?N6c�`6%�/6
��9�z�ʒf|�чŀDh��i~����(�*��Tq�����r�Y������|�����g!�����*4ӊ�]����x�2{ڵ��AuD"F�p�(Ӓ��*�h�B D����fկV���tPÑ�G�� �c���r�S�.����c�Tڼ��MLk�׮�ymLζ>J��t�a.��-t������ƵBV�t�(�� ��R�<y�b�|�P!�IC�������D�s��Y���@@2����:WI�LCpo�*�N������3���G����pUF-u��f�xП^y�R��<�,0�>���w��(,#N�?���{��4���>Ϊd[y@�oS�ă˽�/(|�ii����OBZ���)YF~�_����U������N��t�&�P4~U���r�|��"i~�]��?��ww�b��B��>?����nRm��������, c����p��\f����(���ᥳ�;�w���$|h^��,Ԯ�����k	~ہz��}��@�a���j���/7U5�;y_lK��b��?�~�m:��<�p����N���>n�z\����SL��)ok��������pI>H`�ϔVH	��Ӻ)�}��)T�\�5�e3�Q���9��'וK����������<ցf"��|�w`�5Y�5��|�?������O�S��r[e^�@��f܍&°%Tq����_x2�~CM1�e@.Y�SŐ*xZ&4
����A�N�{�g~���Εuô��=2��ts������}��~��=���kU*Sb7)u��w�/}%z��Q�KZ�gk�	���U<�bW<��_P��d^ȯ�Î�᧞`������}��.іN�j�c>���S1^�h��9c��U���=�:�{�sw߱&����o���e(���|Iga#��D��1�3+���VQ�� �\�8�
|����}�qRX>����,��t$���:ӿ�f�#��gEkmX�J�H�@J���Cf�߳]�hE���or��y�B*���H���s^���~��`L��^���3����ӷV� �D��i�>�%B=�@-�Qt3����^ד �-zқW�~�#�V�%�"#��-1\Md��#��
�����C�l��zԹ��h��7:W�4�}Y�c{XfH�[�3	q��G@�ͮ��.�E'��vƱp{6��%8DV2W�z ��)$l�b�+WL��l��˞���m_g宅��o���S��,�m�9����	Y����������ҜAE�-&m �����Ѡ�dWg^{|��b���3�\9m��K��"��*�a�s��t�}C�����B��lm�N����v
�c/.j���:�:xQJɞ��~~Ɵ�Vx��u���;}I���]����*�!����K|���^�u8�pF���J�A�|4���-�I�UR�Bn�+��"R�m�ژҒǱ]��@hR���Xy�-yWCz��[�GɂM�k�"Cx���!�Զ��3?�
��"��|����v��y��Tʾ]C��&�䂨��|2۹<D�|q)���h�c�fW�.�:�-���8_��nJ�o5�������{f��u82s���=���b|+�S��#���l8@�}Tټ�	 `�����X����]R� Кܻ] �p�M����,Ҿ�ƈ��䅬���B�aӠs6X����BmV�����t���x�����Z�*T�ϛ�I�X?5���}��,SgDoΜ6
Nz��]I��#w�~Y�Q�bo���u���N�[��KEw�tR�\��~�l���Ւ;�k\�T�!�fH����4F6%'ثV7d٫�|!�fE����$��$Y����af�is������Y�Lm��a9��������y��p���ʭLӼ��S���46	&b�)W9f\�8���ۨP��u'8"��i *��L�U�?�4/�Mb�#�{�3+���e�F1r���T�Q��
�Q:<��,��=��X��ޝ{{�_jN��UBq>?1K����>i�iN~�k�R��o���x4��+j��`�꫞ >k��933r�m'��/�UN�{�g~�H��^ߪ��t�:W���I.�q�,��z�*�Y0���6��F\�*�U*�|s�Ҏ�r Z̬K��f�Mq�;�uCH�{�wD ��%�D9����p_KT�a��׃�wZ���lp�/��SQ9d��ۃ�|���XZ��v�`��Y��oP�+�J��UQ����2��U�q7�Pb�8����{�˽s�ϷԬ�cS0�o��i`��s��P�2�l5�'�u���GD���S�*h�N~Κ�NRM0h�؏�Fg�;��X@�X�����A��t�X(U���&��N,�����;5ri��퐥	�-����/��)��Sy)*ķ��=��`�m�V�̨?<(8�r~�������1B�ݯ�Ҝ�H�!���Ƙ�>��U����/KJ�Y�&��M������τ��  ?⎀�U�==�íҦ�s�|��ߌU?Ix�d�H�hg���y!5[<�����}˦�A��-���yc1;JQ�傞�#�q"?p��".��0Y����E$�\�[�CT)Y=�ԑ��#���U���4��qA�	���zGM3���D/�w�ɶ[`f��,J�������nÅ�Î�U�N�I�G�z[�vQ;4��}���T[�B���s�z�3�h���t���fB7ף�������(u�&5�P��l�a�r���������C�C����owe]�v}���'�5PY�a^x��z�h)���sT�K�<z6}�"\�/�����Q󸅕E#Yֱ����L5	ʈ�qM쭖�f�\R&0i�6W�V���`�DW`��%C�8YN���+D(�0)q9��X�/Nux�^&�L{��..V�"?��mW{���	ٛ
6>�9�U�à�GS2*_���Sf�����Ēp��UK	��n��T$l�(��GN��x�XW>��SY�h�	B�H�"���?���eq Mn�'y�ۦ��MOMt	�B�7�,r���
��x�/�+��Q�O��X����L�y���l.����@#��M�����A�T��y�+��'┹B���2����T�y��iA����kvǤ���p:��6b��/%�� �!���B�W2�>w�I�/�W����\�O[��{uBX�,���$}����̯�Mpbr٢�x��;�7Z_7rYXZ�n������I�%ΰh�T�|�v��{;��%�� 泴�Z��Rs�K�͸�$X�w���S�M�7�C��jJz�_�؅�U���>B��� ��l�΢!����0K��R{@D0�U�D*{�e�g��6$
ih��<�����qP��v�v��.����Y{F��Û�Kk �G��dj�ǎ�0�B 5�����\^q:Y����T���S�^ø�Ч+Y��~4���4���ƪ�a1���m���e�r�g�_�#���I����;4�W��.���hA���,o#��=A��L_Q�h\
�T5�$�r$0wa�����7d �d5H�z�>��]��r!��ZL,�_M��u��s��^�p�L0�^�L�O��y��$瞧u�0^��P�"A3�B��AZ�YW��!C�$dw�52��ch2�`��3�68���=�gu9Ra�X��w�G�ԭ����X>��mM�    ���8ی�4����V��$�����
�U��2Ô�J��w���*nэg��a��s���G�rz!������uk��{���x\�P����&�s�ƽ<!<�W��?Hך���#Yu�P��Z1���V�ʳ
*x�SfU��u ���u߮�;������/���ΐ",=�7���x�SO5�V *�i\9�r,NS�+����O�n��vV(��N��}�8��AjK���3�M�A����*q>VM~U?���� s��k;�kof�)9�8�a�&Ί���u���D~׻�tIi׷��5��g((HuS �s�� �j�o�k�5��@����|��!o+�v*1謽�~lY�:3�@�8�M݁l8z�X����Hh#/Aa��*��T���wv�r,�݃?�@	�Z����H��7�qY�!cj�V���Y0uv3(�9�F
�k�H�6�TN�H��� �Ϩ��i�@��r�2:�7��ʕ���������]�m�Ǔ�c�jK��iE�)X%@�G_>!��x�ݯ`�_��:��VJ�� ��؛��m%��ׁ����DC�.�h��u��F����8����2��$ɛ�x���x/woWܽ_ys-G�6�/��@?� x��1�߬`\^k�幂*=�ό��œžu��r��X�˰� �ӫ��;�j����c�"¨x@v�M���W�������3퇄������hC�P>�D������h�=&,��q�y5�٦���O���Zn�m<4�tڏ9��/.@�;I��?t�J���p�#x~�}�M��0.�;'�����q�= �9γr�Za�*1�dܬ��*���ڙQ{����S;蟯U���S�����G�f	��둯f�J��V�Sj�"�IK石5u�ĈW�A�K���@��L�@��&Tq��}����~���3���UT `�� ��y01s��
�����l7��EK��f���+��2�|P@M/2���Ҍ����e���z3�G¯0��h۫�L�<��G�.Ό.0�NOe$�_�����G2�k�D]fm�`}���dq�d}^3Q~fea�x�Q+fǳ7���:�C<��,��E��i�+��S�ͤ�I�O�x��m��{\k��	��2���8Dۅ�ҹj﷮h�0�W��:*�_{-jI��s~/K��οmu�Z��ہ���[��^��*���lo_�3=������#�7G}ko��d�>��v�c�:Mݩ��q�K١�7P3q�c[PN�y��A�j`�3�[��r	������៉z��[7�ar3��_�w����f�f���ٕa�~p�N����;�� �M|?̠Ң�П�ϫ��9n[��;YР��s�<}�^x厃���!����@B&���J8�&|C����_���]����%� J{^s�kuK�-M�@]Mq�(~0�c����!�("�O�~�舂����1�ߎ��g)Վ����cAjS��
���R`@�`"=���u�(���G��F�SO�J�f+#zO�^��H1e�ȋ�^������{�O��Ǥ��S�l��p3C�a1��{��_���[�|^���\�e�q7��w@r}��%�g����t��"��\�U_�R��z�y��ae$Q'{�+����X�7F��-L�L=�:K�LH\�z�a&0�W������,��pq����
˱+�˦�D�pT�D�������*z�O��=Mt@��N�'�GJf�Kɔl{ �����v���uM_n�/����J�y�ӌ���P�qX�]k����q��z~��	.�J��q��[jV���)-�
x�
A����Y`R���#���_c�#�>�Yt+k���ꭉ��߰����@Q����C�.rW�P��o���e��l4α��e�.��_._J-�r����06�;{n̊3s�_�7.y~ʊ-�fh�OҔwI�B���C�&9t��Ҁ8��G�Y,��D�X��]��p���뇞��NBՕs���v+�+Du�<ۦ� ��S.v��}�]��D���4�tP p����w@�}�ɡ�A��}��F
�Z��SB���iDzZ�>��w8ak�����?��M.R-GI�biZ�)�D���>D}	�[^J=E������N�C�m�,%�J����G��
 +wZnfp
�ь���y2���'W�=M��+@�:��]�>
X�E�Q;�������")h>n�jS�k�ޣ�3�dp\j��H���^l98��N������Z�[��	�X��ݨ(6c�T�S���T�Z.][\۟�w>��#kTh���J�)�K�iom��ݩ����U��"�`�Q�z.2^���\�s��_�/�8��9�t��;�w#v���Usvlo� [�	�w��&��یC�[�x@(g*�؝�<~�^�?��ʣP��!Ž�ի;�4���Ri�{�W��~�c���Vsd����z����U9��Ȏ�&�{�&�l?Gޛ��~�xu�Ru��э�^��H��8�F��sՅxoE��c߆2֯���Q�}_V��� [��w��r]v���#P$9�Ur4�� ��!�73��H�Br�Q�;I{FA��ޜal�Y=:@e�P�\x!4d��Kz������Ư�}��2��3y�O�����g4�������%���ȏ�Y	�!c�^yf��BVS����N�ia���,�u��3KIEq��������?�0ó�3�����$��n|����%a�����:Ҩ�w_c�"�a�kq�lj�T�u2E����Dϱ)T㷄z>lF|H
a�"84��rxs��JQ�6��O+��T2=��X&�+�_�j�td5y`T�,�դɯSR'�g=�ؼ�>����
�%�6UP]�P�FL�|B�_�ب��7���>�>APIߋ�����S9��Q��G�mF�(���F|��Ku�\�=��T���w�W�J���׻��ܪ~]�YVf{�D#�:��4d'��|�V������fG�mY#�˅�׿�����F9���q�����U�j��k{t�ϴ�0�	�eJz���{�x�X�烓���9�,g9��k���2ܑmV�-iWܖ?s��S�Cي(zbsL$�q����6�Z �3�0�3wq����tQ�*y���wq?V���ug�r�k�k/	ÕT)���D�w˄��
G˔�������I��X��ʤ!@�W�()�C�NR�w�կ�2���#)p-���.��蹹�.��Bν�k�֭�	?�ZCi���Zm=��ca�!��QHȽd~S�����!�@��+�]_ː:�o��c?ztX�����w,� 2��T/���|:��06 ;�{�ɓ_����%(6<���
�Ax�� ���a�N�4����=�w�AH���������۞�V�ƥq�Ѡ����}OU6�;)0,�nj��jk`f퇋�L5f�z��-~B.Xڢ����z�c��0����A8-��.k`���}o;M��h)9���-{*C�e�T�̉�Zjq�!��P��bmV؃�_��fƌ��O�Tȓ`vt���}f�;�Đ���E�~�S��YC_����O����c~b�YQ�
��+ �Ԛu�4�i~�7rA��Sț�k�l�b
�ۃ�'�!i�J�w`w��@:�	:{����NU��g�_��h�	�����Y���_Y�U��tF� ��'}7%|U�~S?-���\���ȇS[!��f��?�)�3���0�lx��uܱ��y��#d��Qf"����+�-'���~��p���,[��U���K�FMg�n�NdL�
 ���w������T9�p���{I!�T�+�Fk)��08���Þ	�s�Q��fٱJ����H�-�J�������
��;���G��ND����#�C���8�^�4�*�:�;b��D�!q�� �S&�$�*�[1��X����#A�n��>81q(�-�ʧ��&�!o?�]O��W&E���v�<0�ϣ�3�<��ӫ��=���fDQ*�n��ت��>�pe��	�uT����Y����΢xq"grmL�1��o    R/�� c�@>#Q�~�b6_�q���u�k�C��˛�.6�콬��P��Ue%�� �.������]6���,�	�2Ԯ�rN`� �t�c�\����Z����2�e���� dG��FF�~���5f�������b�i/����ޠ��Q�֕�/���T�U֊�}�ܽ�ՇIs0=��pJG'3#�q��p�&ػ����hPP�	���a/��J�U��-�;�4A���w�aU�U_�)n�5���7z���ޙMw��iѤ0�@M�h��s�:��]#��O���D���u��s�Y��_� �we��dc.������h$���r=�9��.q��At�-`�[O�yfR�=tx��J���mgf��;'����#o�=9���KZށ���lK�^�8�1b�a�;m��>^MY��T,v���0�||���<��	L7�u���(���<��E�]��t�(s��	����o0`>q.���ާ\z�3@��,�G�RX�T L=mf�T�o`�ZaT�	��'A��n�a��o\��~	�.���J�y��-zH�>8����C_e�0��j����N�K.�Ցq�������c�Ή����l�7���h�g!���Bv��+�Rtbg��q�8*x[W� QW?��f�:k&xu1���c�+z�� �����V���F�(���:k,mH�cgXeG��n�bݧ���4��nh��1�R�l����2?�W�~��`,��9�'�+��
��9������X�ց����"�. �1O��(�L򫍵	V׺�zӷ�ⴴڗAl�|��V���Z���r"	�9N����]��8�%^������D|�(�َ<McF3ʹ���&[�n�V ��] �"Z{�X1�x)�&��
���G�эK��������ZN��^�]�y*/�Q�~��X������`;F�����ܫ2:�0��[;G���v_����"���ͤ�]��\��&��pR�x$8288�I�\x&�N$p�`:Z|�����r����>�����F/�D����/�cB~�FV�p1I�r�9k�x��6yIR�㪾�A�,�:�JN���5s�˼4�*�Ѫ|}~�?-�}�1�<��8_D�C9�b�=��y��W��C�-r��y�L4�ps>*Wqp]�5qO�� $��Z�~'���,J��0��z[�"7\*�F��:s���߄ꇼ@�'��iٓ����G������_c�Я���sy���?�j^�t�zKA��(Kp�s1����ceŮ�?�sT���P�b�̌ثVZ҇��i�����;�o}O�.�ϴ�o(��
E �V�8 d"Z��m�<9�M�_���uC�6L����n� *'�[�3ڞ[7�M$Q��U��?rQ	 ��z��zö��G�o��t6-�<>��쯥S����[�t~�3>���z�"$�y����&'�ޛ)�\{G�T��V�a���[,���̍��ז
�2%~���&�Z=����	�iU� `-��Z��q�cdf��f���F�ts�K��v/��-���6\�Wץ�(�mh��U(����èX�[�fV�Ӊ{E^5c:Ź�]ݻ���CD�|��R%t\�S���ӿ�9��K��;��a�sQ��Q�V"E5ە�̅��Xt�;d�zb ����NwU�{cئg$��8�a�E,��c�\��q����q���L��e*% ;�������%�V�'��隧��C�^�P��o�X�k�1�0c�u�����{)q+���<���C�����~��E��������w����#d���[�OEsu�a9x��+1���P6�>36��0��3�#L��c?�j4��u�1��$���D;���W(�� �N {�)"8����{O�mVLU@�͗�'z+�Ū�E�w��G�v���O��Jxz��3���{!�$��Q񿇽��g"�G�=�*�65s���n[iء �QF���,�M�7�o���WϤf ѹ�\��*��#|~��$��g�o$nx�s[t�<�e�n�3��㢿3�݅(3�9ft��pH�˗�Ժ�-/��رu�S����]��pŢ�9d,�p�`��i#�����jé���`b��^ �l�c|�uw�͸�s�R
�5��r7P1R}�^�7�j�������/�����!��R<M4��56@����ж�s.�_�g�@���jrۊ�?��xr�{(����7'\�Gзne�̼c���Ƭ�Ä�G��[)��E�_�SYO�a�,"����9�9:��!`����) 5����{��Fa�.Cb� ,�a&� �=@20?D0��I!��k$��P=��cN+=�/�ߞg]�WH���g�����+���i��F���B�O�چ!��q��6-n�ͽlH��J���t�h�D9<�h�A��))�b2�ۻ��-�X;ˮ��R�w�Wf���Y��ߗ7�������m����8uu�twP	��w�~��@ȷ�eN��?I$�o�Dsg�F>̛�mfM���2^F'�p�9����g�8^�)�@-���j�L�^ls�Uױ~)���Cc[S��WS� �����YR���#/�
��X�t!x���s\z��Ӝ����� L\���|Y7�$mS�A����A;����9�.r�D-<��Og��s��3�%�I��}E�u-Q0�'=�@�'��H]M��S j�K� D�:A�􂉟|��#��x��l��5a����50.q��\U:��]߃|�7�eF5�n�$|^����t�<��EY���1 璘����h�����S�8pv�f���})A�x/���r������HP��7��x�r�)ԧH;�pG4�����Fِ��s�Ϫ��Jh;A�t�]9�P����+��9P�\����X��cy<�v�1~^\9ӦX�ufI����h�����A0I��фe��ա��b�HT�Kg��D�3��T�ֻ�^LX�U
.��&7I����k^��Ti�̄Ĝ��#Շ�����&���N:a�{fu4'm!��~�;\�x; /��b~3�v�z��к�]�@0j�7$��Wiٹ6��1��^ͷ%������zw+��,��޻�筑I�Wo)���A7[H"c������}�¢�CR�lR~�U'<k�
�A�J�n�CCy��WHX�)��q�T5qS͒��6��w��C	p�r�ǵ� ��#�A�!,O�ͤ�{�{�
s�_�Q�5��j�+Cӡ�雚r��o���/�y,�Z��'^��ii���vNA��أ���X�n[�O�`v�/.�[�Ev�����e'��mi�������a�lv��7��c�4��r��`�,F�Rq����3��@�&�O�Y��^=*�߮�(��q_Z+p8�����
#n��~�� �$_��8	N+�z'� �v0z��4�w���so?Pr����%Q�<8����(�qƆ�>�(����HY�|�4�)���e%����4#}h_�f�À�`JW��f|�	��C\,���w�,dd��$#w��Ԛ0�$����p��E�����g��=�o��_��߾0�<��"�`�� نm�}.I�u�^�Iz��� g(��G��H_�O^"}����;u�5O4�m����_gy�U���]�IO!�H��@�}٬6�`׽abG�$��X-�V#tٽ!J�o�,e}D�+`ҝ"�D�2�#�V�7bO`��quy���]ل��֠���m-�$[����W��Y(��t��s��]^�P&����&֏EAz	#v;
�ǔ"Bm8��0���(Ĝ��q3g�X7����~7����1AA1[�)��.*r�%5�����hŜ�D��h��'�T!��*#�E�V[Bt
���w�\��Q�2C-�Z�~�Q.�=3Fpr����3�|gi��Q�Qs)�y��/y�dN}�~j�B������\؎� ����n�w��3�[��5��)ʟQ�����uKSXMS.�!�҅4�X?@&$��ϕUO��~8��3��J��W�-��'��    jƹ+���])T�1-vU䱮Z\�;1?+'��F���\L��v��*RO.�u`j �m���-��^�6Ũ���G�t�0~�%����x6��oyT�r�穂T��c��7���Aaٴ��~��져�T����4�W�� :�t�A��^���Lur��3�#p�v$TG=�F�M'��@����o�>I_��AR�`�3���٬ZJ}�}K�Fx�� X�Gͬ�CH�CI@(R� �Ja)E�[�ܹ&^&� (�`s7Oq�yՒo�(�(�pXN	2đF��1�.�g��+@�:Q�_�$��G������} )T"V1�W'�CX�gD*�c�Ώ�VH`Ï`dL�GW4�7���͘_����kDl[�X�Ρb��,;{��̢�J!��gl}o����[vUo5]%HR�<� �?��
�^&�����[�S�~�zL�:�Z2���%Z�s1�K����[ɖ�����3,�t�uS_}wB���I 1774N[���*e�Q���Q�nAV�d����.Z�l$���ߨV\*�r1T(Mƅ;Z���qd��5����Ec�*B�
(6�ҳ�;NPKY��� M�1��ou&ve?.P���uy�Bo���Zf[�t�e�l�HǼ`$���q"�� ��:N�.T+X�#+�p�{6P��W4��}@�l����(������<�-ybb���m��t����q�~Q �;�u)���3���꫻vWM�\�a����X�Ĳ�Z"O��{��>��K�E�61�@5
�^!]���n�!yX۲�֊=s*�7¯��w�Un��^1����^�<�2��,����BR)��F���|�����t�[���	1/����.��!Z��L�A�A1?�m�H,P^FH�`�X4��[�O?N��Q	�]F��!/�jF���zTƺ!���A�������-���B����6�$e&lS�c������A���B��c2����C�PvS�/��0����H\a[������t��%�n��`a郑�VΑ0�����QI��Wk�g|�3|M�U��z��*�`���P����t'��?���:�O'7�?S��oEc�� jA�i��2jɡx��Zz��+0��/�
Ap��M��w���+ы}����8�o����g�3��US������;��θ�@��a��~�t������U�mqs,G����9c�y��Ѱ��������`�tHX��WvP^���� ejH|@]w�o��@���F�h���x�v�.Z�{���?�&�*~[Z����d���)n?��n9����N,��ˏ��~��	V�<�(�ʷ��Vm��J���+��f��?�Ѓ.��Xx�:^�A�oru� ����D�#-̬�ѬY���|{+�����V���ƭ.�^�s~(�>3b_����3�D�Ux�&��!B�/��ѥ P،�$v���B{s��X/ؾo󉇗�7Q0c|���y���ד^�dj/~\�5���'��7N���H^�z���	����(�/T�E�1-ն��x%m1s�~w� �"���3N�9���{!�%^���-tF.����"4[t����$���횅]t_cr��S�n���E���F���E<�}���m �֥�WX7If�销�TN!��b��;Ħn���)�����S������9c�߂ �����e��qM�]|��������`�"���z�9Z�~'�я���I�+�J�o����y��W\�-`�� jwjh��>��[ɳ�����
��&�[��HUr��Z��-$�/4�Q��P8���!NS�V 7����'E>��]��N���[B�bk�"Tp^c��XM��4s��U�ng���@�L��.w�w5��/�nu����-9ս�$<���/��$o������}rS��]?ht| xir
��C�=����Ffu��!S2�AŊ��1 c�[;�'��2%�'�X4�k~�tQ�Us��ݞ��s3����e�\�8�Cz�� y�a80�1�6�k�%�_Jފ�H��g���5ΑL�Z����f�(Hь�?�P�]*���� ���f��m:�HƩϥچү�&��s�ˊ��S"���nP.�4�����2�=��mm���C%�]���S�!A�Y|#���,���xKe�i����-��g��X�U�뵖V���ct���DV%���7\��8Z~p�c��,�GA8���� �B�D���G�3���n���3:p1������y�h���+,��	P�P!�iA{
߾<߉=����&�A������U+o��*b��[/G���׮}v�������}ui��:}3�&�֠}<��LBmq��~��4O�3,A�+s��H��__=P�e�.�<уd����+˫�C���\��ؘ�ֶ����~�핢�D�
�VE�����\�i���y��N�ԃ�tr�Qd��U�{�Eq��ޞ���"!���&��Iu2q��]�H�=P�ch�l�KK9!�ը�k�)U
��2�t�w&������:�3Ӷ���b����p4��>-R��m��C��s���]\��&Z��b��ژ2d�h�������6һq�Ro/D�r!��2��(��i��#T����v�oE� 8�Qk��_r�Fj��YV����b�1�WPf�A`wec�v>)g,�~�磀����|���a.tv�O{_�+��q�`����r4�%=p��_�M�BP�<����-N��\Sj��X����laXʋ:/�?��_+r��K>�<JSEN�����L�a�Lۧ��$��̟�y��}���(/�'�a��:6�B	��M8�B�pchU���o�[��c1�� R�"j��2�'����We�l"Nݨc:��H���!b	hc��d��֏�,<�Vd���V�N��]󨙮�-�����(g�3�(�,�#M�Fi ��42�`�ȪB2���Y�	�zM�3b���\���iC����;m�D7�[U�U��`���7*����<�ؼK����݊�+�i9&�6�,�ps�9�4�?�C!kz���c��N���?�g�����ާf�wwU��~��h@G\����:~�H|���7<��k>��F�h��LM[��9pu�����N(�]�nWj��s��U����y%�b_�c����k��o
_e�y�ݽ��c�]��p�6������1_������dB����ÿ7 ͕��It� ��5U��@Q�V6��t��.t2�L;���o}����"+OM����) �#ۢ���q|��o,��a��ʄ}�
4�g��g�9��T��׋�vf�J�g���㋑�>�a�%���/�U����A�J�\�D2��������e�y�r�m"�l|B�����6�N-;� 7�����Z�˒?�7��D���h,8N�'�`���N�o(�Na$�~�㸈Ϻa�b�RK�����|����[@,b�t�8M����^c3���qh>�3D�izӍ*��i<jX�cK��}FOp�[�D&)0�' n8�U�l��}�����D���v�}0b;V�$�;�W
>NDv�"o�}���(�[��+v�1�E��H6����'�z�]ɐ�6 iYB�1�w'�E�q��ާ��I����&�.e��FTħ��ɏ�|W����B�̦1�[z��[��\'����}O��th��L,{���&���GuA�6�3������n��?����{�K��ݢ~l��o�AadTͤ�hJU�z<l�0�N���[�]���1*�]��^��2X�M�-*RġZ�-���z���#�S�Q�u-�-�������PQ9�_�2�~��)BU��}�1����	�'g����[��(�q�Q(���Y�eaێb���͋��1��:�O�O���~
�8ƒ�eI8w����R�g��L��sY��'l�	����*��Fa�{y��R�E/���O���^���K/۪#
����}    �3���+���H��,�q���te�p�M��k:{`n���i��ss�x��gD�����B��t�f]��xsh�W]qv!j��C�ylaae����Re� ����m �|�2n������癁�gB�*X^��A|��$=��]�+�3ů��Q�[�hL�D����o�T&�ܘ*�[(�����RRF����{����I/&��3�P��AZ<�����/���hA��N�p��	kx)����z/>,�t�K0�H�p�T�d��UX�	�2�$�QX�`�W"e�����žި��;3���} �3��5���̅�i؃e\Z�>�%�5�_&o�+<����ǞD�*]��P��ũe��wP�XO��Jt�p�/Ԋ���>屮��b���[�lI����h��z|$n�=��4����B}�`�������F�!Y�GxC�k����	`�y��b�X���,�ˀ�c
��\҆k���[c�}Ui�l�x�@1.t�{�HYּ��b�aUW���(*Ƕ�u��ő�p�Ppq��4��j�j�y]�2��T��،00��U:�+���C~���T;3ϑ�P6B�t=����x'+�En!H���'�X��5J�x&�I2Q���$%��i���Me��I�zg A��i��A�.��ިW��)빬���h�^�C���s��f���������][�;�83N�M��#/{�<+�Ο%nR��+#ǖ�~m�h����E�t'?��yy��
�����%�"�y��ў����g���~��,?��s
�xUv�&^�/�������f_N����Nv��4�s���+��Ėjp#���6L����K�����^�)r��u����`�fN�|Q���4�t�=`�d���Ŭ#���83
x�
O�x$�`�y�x'yD���Ey��O :�0����e�A�2�1A1V�Y�G
AL�GM� �19.@f�tq��#6��֪��B��D�%u�f����]�n���kV���76�`��e�3iM�aH�Nk�2��q���42���� �%�z��>`!.X���̷��p�M\���S��a��x��L(���k 7_6�=�t�mN���]Q���H��/ݚ30�׺��{~P.���x��ڡ�{@�񦇭o%�X�W�K^"�B�Zr%�9pԑ{ܡ�uW�t�pp@�L
�2�I܆:��H���0$��-��*�]J��o�rJ��xX�}GʶCv La?����G)�X
64���3W��7mk������)�����Cq�.����r�떢s�4_�2qV)������V!/�[!��ꝱx�z�H=�p��.S���pF�fC,mC�{�ޜ���2���k���0f ��t��UđP�����@�)TB�����)Kt̏I��[�,E���-��Yl�N�ǣ��Ն��x�]����
�֟8���hx��s�����c��y�y+	z�Gz-����h�R����Z�,�/�er3�MFټD�C�D�M����[��������j.�R[qt��{�/�v�w���+�x����16u�)��\r�#z�y�J�,rF%i:�c),��Myb��㔝��ͅ�kbꠒ$���p��s�\-�B��d������}hB"玑Dx-hC|_�~�� ϻ�^�i�h�p���k��~�?������F.�w4B��b�Y���j|��ӵ���$ݯ΄�~:��,q�0��w���U�Զ��DץwEx��q���+`��G�2j��7��Q��h�ѿ�D�G&dU��O7�P����X�	����2֤)it��]�n���4��TM�Τ7N̘�ӆ��\��׏ȗ �$�5ù��R���g�u/�.�j=�V,��r,-���2A'Җ��W�e�H}��7����*��6�"�)�&(P��$!�r��g���HM��4�Ǩ��C٘^97�f������8��AH�o�$���o;�Ay+(<����g����T�!Ĵ�;�c�q�^B$���3����=>��mW�q=���
:���߽q�`���X�gw�#�w�L���P���`��Xڬ��ƕ�;�Oί�ywj�i��:��w�;�ͫ�p�}d'��0�`۝'�����;c.�9�x	i��az�K1wsV������I`�'�ϏUO���f%{�P����q/D34����=�&,2a9Z�fx�k�k�%-֕��瘱 A�Dݱ+繯���F���8�:�����m[ײYnLZV�0�G�%�(�A3�=��c��<�YPXoˤ"ӽ,�\��U�k�!��_"��v۷�=R�<3�G�;ͯ}Y��=�7_����A�s�x<�f� ��[:��Ȗ�˷R˴���'��q�#c#�����e����/���W�q|�Sm���ȗ֑��vGƢ��䯸��9gO�;��*\H�Amǭ#'P���*��|���a	AMøAg7��2���6�_�^���.I,�Y�;�-_~�f߳�x@�V׉��|���:�����_�yYR�'�6r�b��#8���+\N_�L��f}A�T��a��u����D��m�Q��a�,a�ۋ��Ig�%��b��)�f�T���?��A���5��<'"6�	�ky�	˗INV�����U߉�CRP��cN� ��R��N��Q7}ǱW���[Wޜd�?�5����s�#��:�BJC;�
�W��c�͜`E��K�8hS!Z��@�ֶРl�HH�1�����a�OP�9�8�*������I3�fqЍ��f�l����"ڂ _̷.\�]{`~�͑��?O� j�~9k~��"Ү
�q�3/<���5d�
�R�*��%�NF�O�pٹ1⭬y/,@EkcjȽo��o�X����lS�	�����7� D=!q�}j���\y��D�����1vY}�T�<9������c�$Z�1����`��ꐕ�N$4¦�{�l���z��U7:쓬.n�2jZ��H�Y�% �/�%F�3�r%��wҥ�ǡq�3��RK�"�׽�)���.�uˊxY�˸�կ�������A�J��`?�q�����=���U� P��QL��.��N�V�L�X_��ާ_-"�a����vá9t����5�2����`��"��l���F�`�I�v�_���k�]���e�]X��'�K�!����4�y�[٧��ٟf5��?,�k�az@ю��(�۴�����Sؼ��f#�B8�ƛ�rgqLE�O�~�M[�g�]��p)�P�s��ڳ�#�)?Ag`z�7*�",������r'W�%���>�팎>](3��B�|tr���Eu!�-���� �E�[�=b|��w�I(K3j����rN�:m��to���#H޳�j�� o�,C	5��cB�����䐇���m���_�R�r�HΫ�cưQ�<츄��[�j~�E��۰�Ȅ�Y��'����#!��h�-Є?u�b�G��&�(a���:�kTa�5�[�)3��L ��W�r죁dJ�NY��r�7��z߲͉M�Ra�^2��p����T@�EA�W&���h�-��Kfæ�ٍ�>@�'h\P��o����à�{����� ��9���Y�h��~��B��j���#㸂y�|���:�9�ET��--K�lGI��\ӧ�eՍ��[�Ґ�-l��hwˆ��� �|'O�*͇C-��C��_���"ve�d�������\��m��3/��{C���Y[�
VG��_�q���Y\��G۠�]Q�g���L�mjѐ+v�"��^^��s�����ql��jM&�lI�K�SEJ".��f}d��,�2b �
�mfkѷ������&�� �i��;����N�O���n��c:Z��$y�O2�3i�}���H�H��p��K';��[��d	b#|���Ҷ��� �tނ���>dM��˲NTȕ�1(�"�lb��&7B'{���/�e�o
ģ���M� ����?��� ��ts�9v��4�?!@����	WF�Yq̤    F!��=���O2��!��"�.��oh,U��J���4�-S;�::a.n%��3K�l�,9�WcY�ʦ�_�l[!��Fد�Ό
�Y��\C���rl��ZI-�D����2�1>�����}.܏��eMHB��������*;ɲ�WfbW�p$bp�^d��F_m�$���3F3�/�NG[��jL�*X��O�4N�h��rM�Wo�e��(�f�r�hb7�_�����>T[p��U$��'��Py/~?fܦJÒ=%���b���\F����m���׺ܛr�{�ǲ�v	�^�](E�p6����F:�#��|��J�����c-�"����k�ɽ#��	MƎ��$�A1�@Ԑ��Y�����ro�E
�V�����%7��0�0�)`oޠ�������������c�K�{B8����pgi	I�7v
���8'4�O�$�W�����q��Ů��.[�l�G z =^��Y
"|��<T�ʬoQϺ�z3�N���~�+�/�����o�|�D��@��`���ȹ��m���UDL�Cgd����$�_�ɳ���9��x
*bӤt�9 �Q[��~�Im�-3���ߥ��=����쐩<f�P���""�{�yɲy�2�(/��vW�ݎ�� [��kX�=��$�jG���X����f���s��k)����#j�W�O3I�J��ޠo���-�sr�2�����
OZw҂Br��CZÇ*;03�>��g��ݽ��r�����Ѫ�6���Ll���ݸjWhp���J�WL����������8<���]6�D���l�!8�V�
����Z������|��@A	��n��=$m�f��jB��'�C�r��q4�GF5���vW�����Q��#rR��-�(B�5��r�e���=cON�-=�mY��,��G��v��j��Up��}�Gu��ߌ��N�~�Q��D�z���N���1�2����%䲥��1���q��{v�tE��gai�eg">���"�jŪ�P����������tH4�>�����K��b~�����-�l��9p����Q���x��A��Y6&W�a�\\G�G�2��Aހ��;�b�X�"�o�mޤ�-�c_�?.�ʟ��m[6��f��[�,�+����ԭ�:�q3��^6���]i��`w��]q PO�$"O
{YP�І�dtV]�7}~���s�ƍ��ƶ�g:��J[���e���f�n~�B��F���ò�]��?�xd��΃�p���8}�n@@%s�^�TI��(h�l����n��ֿ�{�@��	d�AG�\�PbT���6�Z{��F�VY}�O1��0����~d��Ȗ��OLD�������G��
�>-��}	�p~�
E�ˎT������	6��/s�M�坓���TK8��x8$S�ͽXA�o�D|��%�4��]J�"F"D��F�q
x]�W��^�ZX�y��p��Q�/���@��㒂ٺ�/>�GY�D����*_��L��|��	�e*��繢�~{��b�(C����s_E<G���e���^��g�k��".��͑
�RA��OO�Rs�`K��
�+�`���;J�Q�:P�X�(�39^��j�q���' ���Ū����1_Ru��.�.V4��]�}�iܑN�~RE���Z_��Ą��3$���<2u<3�^�It��/U_l��4����Y`qBI��2�S��+f����*� Oe0�����t��Z\�u��=݂Į�w�*��M������ypD��)�lJ�P�I~*Ndk��$�9�ẏ����BD����[�7�q��a�Yp����)����e�C,O�� ��ղ	�wˀ�D���ף�+����3��4����D43$��'9[�r�� :e-��h:������˭�bս�$C��H�7�^�_�u~�U���SoǾ��Aųx>�Ѐ�,����XHݩ���(�a�>S�9�hO��ˮ���ʽ���*�M��y;(̞����ZZ�1��{��P��Tߍ�F?^z$��g�귏��R�͟q 3I��a�HO\���j�����0De���dOE�՗��){��$�w����pPb��-�'�ZB�L\J�|p���沌JG��Ft���/t���^����?D�b�w}��n~B��t�X�By���@L m�[�d���+��jzL����4��Y����6ۗ�OB	r�z��n�3�)>���;|�Fe�"$U�lHu��+�7��4Ǝ#��Ph����=���ۡSQ8�?s?e� �l���	�&�WA�'�:�]��(r�S�>�P�����m
vc��!��\HF�6G�|jSi�u�U���	�)CO�$y.L��Qq+\jG5�@����������րq��u������dp��Pk������Tr�ⱡ�#;@:ו�K|*t��;�0{4�G���gC����W��;d��>C�ŗ���D�u~[P�mg�ޔ�H,.
�����S#�%�V����ô��ޏ�B�~U;s��1�vf��q��ow�$�N��
�Jd4w��
5��4>�)���a`.�)��b�Ǫ�1�k���ߦΆ� q���lf"�J;  H�v_���Z����5�ف��AT�j��Y���$�Ȭ"R��jv� 3d�%�Ȗ��B�%����E-����2X#����
�y�:[�����������<	���7��Z�j��^��Rd�?"����{GoT>˵;5,i(�"1%ҽ�w��aA�y��z��`�$ ����k����P�5�/)MI=�� �_B�>, �,^s۽et%R�#FT|�ʧ��A�������?%�z4�G{��+l�Ő�4��]h�iw@�����s����KxZRI6Q���c�5��K�LC�2�6V~<������˄g�)%�Z��(q�M|��	��m�g�5����<u�l�G`�X�Հԟ\qj��[��D,��MI#���6$%��On��I�v���H=��5��Y�W�k �/�S�#���Տ�&�G��aX^#�^����������O��|l�$�&��|�gs>*�:�~P�>� 	�N��B�G[��D���ޢ��B��k����PUgb�Eol�<��	��u��S����J�گ��5c5��17� v׹ߌ��fK4y����7�~�<tN�N����-ٓf�6�� �)eY}~A���:��L�&�C�W�C0�P�b��#;�3��(ψgm�xH�a��ޠ2��G<�*��߾�U�Fe��<��.O`��+��k	�/�FA���{��_��ИL��&j������'�zU����Q�G�?	'�ᧅ&@�U8n���ck��ur&�B'u�
6W���;|�̀ӴM�+� �r��` E`���b-Rl�
q$9%���NQ~P���)�A7ZA�����Ϊ��@%]�9��u
��<�#x�R���N����'�$�-�H�7K�[��K�-;95�Ġ��q�x^�.�>�0TS��A��'�-En��w5�e�/��q���9����n*��<�h�MjZO��RW �f���;^�J�{�uIS�f�YM�HHW{�ZW��u9B��:����솳���^��Cь���u�. E���O���?*f�ä����4�bʫA��;̉��ԫN~���3�h2��T��<�!h�2��_�w+p��!Z�M<�6Qb�J!�)�
Ajp��{feax���n��xg�^ }<g7�d�U� �	۫��эgĉ_��9ټ�g����D�,a����$m����e�X�#�.�Ԣ�E���*Gj�6�kz��|���+I=!)*e�.�_6C]��|.bf=:M�Ve������Z'�6��Xu�`L 5���"�$���R���A�2ѷZ�c�|3�HM����6��A��  �@bqG��(��`o�J� �����v]�&�NP��)��}A_��0P��h�5��§g�Fzu��J�@Z\;��k*�1J3�lw9`B���N�V�Hn.3Ŗv��4    n\CB墍���37&9����k[�u�i�M6s��˖�|��V��B���^��B!��@��雗�IbJ���gn�����ՁYq|�L�<����0IfI?����P�)����QP'���퍳v�z�Ծ�wΚbӗ��$��������i=py�C�Ү,K��|N�+A�n?v��U��/j0W	�&�	v�a�֝�L�G�"�R�a�3�]�]N�����L�.�{�
&�@J�|���oM ��>�~��|w����7�̳�h��2����������1��r�ESn�TE?\�g���E�ñ+a�	'�-1�Ձ�d���g����4�����f�14�X��!g�ߊ�Ǯ�����0���:��L�v��~ꄙ���3�"p,GUX7��JvzaI獅���'�y���L&�������Dӟ���p��Ҕ荌/�=e��ʤ�)ڐ5�)5�!��Af���������FA��N�Jҡ�\�F^`�냭����j@�<O��%�$�-H+��}6x��črʔ��p��e�#�5r��g�f6<�����[�0
\j�5q<v�ɗ�[&w���؞��2�Y��H�ִ�v��QU�_����,pzP3�ؾ]�0L�|~;��i��S�TI|<>��X.�®7ׄ·1.-ū�I�į��#S����(Ӻ�B6U8zT��Q���|�l��;����g�Hђ|ԑ�ǛA24��t�E��KM��C��|3����@`�'�����~���s�b�:$	� JHfpc1��%�עr�ȆN�����o�O���Zӕ	�8#z;d7�$�1+���v�}|��T������( H��r:p�2���!���G����P��L��VP9Gp�x%茶)n��ܤ��=<^ب�HW��9�!{6'�-&�ayX��o)��`�Нڨ����/��
�$2��p<o[����2�\(�!^����rV�2��a�@�JM��Iu��r��ش~���,,d ���dM���<{�Z����F���&!�3��2�Tp���O�.���u���0�=�V�%��"���U�W{�󙋢	����켰�x�3�SŸ�5x�����:�%���[ViO�U�!�E�EK��)AWc�Rh��_	��j�ADR�Ď<��Ζ�޾z�A���g�&i���)�U`��	�P'�����0�D�Ih�!j�
�.t���81IJ�<�u�rj��n�L)�~�#Ip������2Fc��0�s\6�OJ�������w�K緹�;���W|�L��5�[�#+J�N���]���5^�I�t��~�6��%Y��x�z���7-����s�R���j���7������b�R�J:�'�}�'h�~�����8�2����x�:*~��
�2��j��$X�W��\���1ʴB4l&Øv�5�M����}���m�_m�5`��6i�����G�H�^�?�'�,��?6�첍&%�	I.���CW�i�7a1Q�sv�b`�yE�	�dU��U5���]U���`0�Q*�>okp��Ȋ[�"��98F5�!M�X'E~� ���j_N�jhX�{9���/�_ă�n��(�����+�Z3����5��h"��Ê�z�[�Y��;C�\�"s��8�����D���b�[�@;'>�a:cp ��w��`��Rb���8�ƀ��QEL��	��=pidA	XC��p�aG��(����E���|/5�s��W�0M�%*o��|�Zâ��s�^]�f�)n^Cp�~�����⮡��j5�S�W�'U���`b
�SP2�t~�7/R�`&JMܪ����B��毯�D�:-FaU��T�#|cj�gv�����"������K���J�& b_лQd�{������:y����lc60b��] ����\0��#*�:�5����B����&%��%k��A���w��2ɀ�ا��8h�$��)#���ݩ� �Q�[�?���J!�l8[�2��J��~E��G;�3��&����΃M9a�%����J:���~E_�m�݊�,?�"�X؎9�䳟տ����Ɵ ����d�*�÷��"N�s; 9��{ ��T4�Ȳ�"�+׋
>��h; 8'�LMcQ�)���,��Qt�j(�U�;�!N��$�b�>q-m��}����t�q|��(^��PF?4�Ҝ�Vu]�*'�ƾ�D5/v�ڣ|L��ob�E���y-Thcj�7sQ�^�C.`��xo	��i�L��O�ԃ!���#hZ���6���A��˂�>�ƀX\�sG�?��sP�J���m�I�Z���N<X��j�0�QȽ�sZ��vZ�j�dG�m���-���I/�%��:f������`�SYa��|��W�w8X�ml\iO���ଯ��\�#0G<�$����D�3툛tE�勾�q蓙j��:���>����h��C�P�l)��31�d�"�����|'1M�k<������0������%۝���l��r�����~�Q��z��T�5:ϔH��I�D�P^�(;�,���6����N�u7^{�1KJe�@%�P\�dq���}�R/'�����}����_3��2?܉ފ`����'5gc5��!
1OؘҒL���}�a��)�(��{BJ"sH�T�.�����W�2��������7��e����7@��� |oO��8��M��Bϣd2��O�4�@1��C�[��xV�I��䪔b�V<M塰���[�p�(3U��u�%2��#+�槮L��WB���7y��C=��'..W�����,�V<#��Q���X5�z
;
K�CB�x:N7�ֶ⮽ޝ�d�AƵ��c���6��w�R�!,J�a��6��/\�,|��â�c�>t��%Н'O�Az,4���C �K���!�Zkq�^0�5��	S;�i��c��R���� %_G_.��C��DH;�����u�:A�B��͕H�ۃ�n���C�08��?�G	Ȗ�|���˷��O]S�����[�%������S$���o�
�b�vJ��B�8�'���~?��Fl�S}j�G1�.���/��G��[�y�Qį����x���O����������^��׵�KP!�hYV�*}*�/t�vhG��~�t�G�D��2_��7�HXj�SDHߥwIo~G� ��S�{��ʶeD�/�1ֆ�(�LLdW�,TFD��6j_���5g=��X���E��V���M<o�L/=��,AZr�%�ڗѵ��C� ��dZ�쪔���~?¨a�^:j�
�[�S �R�l0�VQD�VH�)����Ȝ>	�3�P)$g#R�Sj+4*�ώ�UR��{j+V�ܫ�������rՖ��@3�{Z�Xs>��ԥ�TL���\��f笿g{��`ǆ�U/���_
 ��M!�_e�;o�P�_�o�-`"��E�*%���w�����X���""A^(d:;�@��T"���� F�6ɓ��)5�qؘ�Wx���M�h>$�CpU\������G�凸��9��t��y��6�n4Rр�������9(W_ ��aX�: eU�^��c�v�Lh�v}�]M?���K�F�#H*/��������3��Lj۞-�0oh�&��Rv%t�v��B�l���ی�Ϲ�T�>��m_���|�[5-a�ł�8�	R�E�����~�w��#u��Y���8O��n{���k��0��ɕ\����j��m�1�mj��.��r�`w�Z�J��dh���y��A�.�V�^�K�H��u#~q�3~Y��8z��[���L��(WH���I�Y�t]	?^��(<�I~k*G��hQCay4'V������V�Ѥ�j���dQ!}�6DJ@�*�v/��q�Я�8V�{���2�{v�←G鐄3ί�VXF�lV锯�v�\>p!���i�~(&�a��3³�lN�̬��C^��^Mz��4"C�b����������	�i$��x���V��u���|��3������~��/o�f�E�Ѷt��&��    b�`O��Q���$3��;kFd��;!�������fr]�����I�!�¾�b_n��c�zW^G�orAk�ȲnL����[Y���Ĭ���Aٯ!�>C�<+=VL�^�ZZ%��Ĺ�{���A����K�g P��(F�Ε�33���h;~�f�g�a��u����+
���ٛ��5�<��W�.hA:���9,�lD�O�Wf\��Ԕ���s.��$���r���g$�R5^�S�K2�Z�����(b^/H��G^�&-�4Tw'[l\���8��RW�䱬�����=zz��}RBo;|�����Gy���~E"Ԥ�@9x��-)�����>��)�%,M�"���u�U����1f�Ė����j�����[tY${�^�!�_��L5G��i9�*�(2(K A�\�eŰ�EV�.K>����D��'��k�:��eg�7er�֘��� �6��Ȗ z;��Aʟ�c�ì�a���6(�I/�|uڵ����R��*E2���;ӥ��Ba��&0e�.}>�~�)�7�h���v�-+�t�'���f����&Tup�q}��}�� ���q8UZyD ������N�]r�F��/�4G�S8i�0�L�{�Gf,/�Ň�4��d�`B^kF�Y�}��bą2����L��M �W��?T���e?�<�C<��H#n4D�x��=t`�3k#�ׅx9�b[�ű�۹�i��iy �ڻ�9��4v����9hR&���n
�U�B��(T֫�ym��0��h��IMl�0�D||NAr�T%$���k�U,��󱠒��4����Lϔ̚�q�h��e|b�?a
�d�r�#���~�8�d[<Ou#��Y%�5�/��vl�r�W�����w�C̰ssUޒ:Ge��:���t:��>�k��u��b3=�iX��~M��i)�Y���A���Y����a�}��l�ɬ��s`�ʝ4��|sfP��[)�]>f��Xڧ��5��EH�o#p��2���_d��EA$�{٭�����7VN��[��!,��<@���Z?�z��3[?rr̯����sy��3�徚��D3ԡ�'�����Z�������T�)>#�5Y虽��j���4)Kk)��F���$O����A��@?��z}�S�{�/h�oyRk���àGM8}�
Ԣ�d�����&3`�8;�مO�P_���hP賲�E� ��̬��d@Y��?�*!f�oop����o��5������ɑ��K����q7mݍ��=��G[M1$w>:	����g��<�ͤ����>	1;�:	��6�,S9���$8,��}Ee�B�ƾ���+��U"�G��mcQ�����E��ݦ���q��*d�@*��,?��in�=� �>���PX≜;����R`U��	^Ρ���ل�*=�~�2�K�2���=:RD|��i��V�ԏ*�k�v����0�9��F��U��8�p�n��ozNWH���w�,>�n��9��*����^���<�	!���"����A��Űl�s#�xHy!���i�n��+����E�X.���H�x���T�*H6���d\�QN����f)����&����.zEy�zS���J��D�K<2�"]h�tA߫E4%���2p&,���A�⸞�}��K�E�X������S¦͘����.�UuG�F� zGp(j[�����V*�)Hj+Yp4x�3I�개{�KZ�UM�[Q����ӱ�ܻ���o�X�j3�ivQ�I	t~�����
�D_BQC?�1'�,#��'�~6.f�L�u�L�?��8��;���7Gf4D�)7����	��@�W7v.�M�݋���8��Y/c}���D����������26�����O���Բ�6;�}H�iaB}� � �K�T�|x4�n�o���e�k�C�, �Ķ�\��uTA�+����џc���WN	��E��W���tT�q��E��x�0؂y�\�˾�ěG	m������R�f�]7tw;=��9)R�Ĉ��%���aؽs|�8_��D��"^P��������=�)��z�B ��@ފ� 3��U Ҕ-��X�w\׈�?,�H��'���7�y�y6�i%ś�M.�կ0��;�}$�4�P 	G�-%��틝��(ڢ�'���4�Z�[-�5���&j_��D��]�1���Y�������x'�X�rðX�m70��v:&ŗWWdx���������nAQ�v
}���cP���N�l����P�B5�G��S�܈�[�V78��<2��Lr������eD���fJ+`d&�8ğ|�h��R�Qp�S���"��hTc� Ѱf�y���c!ɛ��h^�����*�3?�en~2{b	�n�y0��ٓdg~#*5+&��$U�<����ޙh~�Sc�8��/ݵ)�oȉ�ψ����E�rx��ܴ_��H��5�j]����S"W:}p��[�~)�/#�7ij�%ӟ�hneξt���֕hP�P\�����j�;���Z,�������T�Fχ7��̩!'ޜ삭�K�
2���+H�̎Sj�i���u�� -760��ɟk4a��Ic�g;�X�t���A��ާo�8�c]��dAp�ܰ``9�_r5偁x�i�o8H^��:�3����u[Fr�.�}
������\�c�mR ;<�8�nƦѵ33E���\?�|�id�X�sR�y��YR���%���7�^����C�N�Ӣ��WG1<
��Qr��U�}��;?=�ʭ�j����G��q�k%�)P4�y�)io���=��������a�5^ȣy�%��d��G&���2O��l���I�sԍ���l}8�E����%��5xYN���7VͰU`[=�Ԭ�-�JZ{ψBk�*k�^�+����!*�[�E�n��f���Y�h��_Æ����6��N9�̿9?=aH��)�9Sxn�U� $f�ⓖ��E��C��K�Œv/*���kҡ�8T]�2|�0
�~��e�w��ޙ>!A}�B�Yl(�q�/��6�F��L���N�%4V(;���1ӭ��Z�~�<�K�W��7�)�[�����޾��&,CF�s7E6<�m�E�d����u��]v̟s�:�_q�t�9��m�
ҔmNQ\�d@2��Zڻ��^�H�w��%�b��]h֪��&�V�E��ي��=L���\��Bˬ��0_!���ʉ�H:��X� �~�[b3xov��{����S�Bh�2OJtu�۽0D$͉����)o��w�`\��sy�W<��u�19cbS�b�T���A���x�w,b����/%'oA?�!�1�1vs�EY�'.��1�_���vl�q�8L���[1�yc�e�l+u��9Q��_����x�e#���Ļ��S�K��z�7D��RE�g�>鑤M&���( 0ʵ�#�Qo�H{,�)0�A}��u���ǳ��@PH(�bp���Ԉ]t�� ����@)�9&����:@7���+���B��A";�CS�Dn�E	�s����%��w|K��KQ��0�;�b��6wMl�/��Q�aB�P�6/���/�謫�V�DvW�[�K,�U�A?Wn P0'���G������;��S�p ���b��)���}��|_�@�+�<��Nbb�u֡�GX��?�UZ�- �&#�\�����E.z^���rg�N��'6�����W��[V}��3�	7%�",�$�4�`�r��j���s���i����;d�ۥ/J��X0��fz����L8�kFğ�B�|�~�Q�K'U���;.�C�=��<�L��h�d���ȿw�:�_������H�Agw��4�\,XJ�-Rj=����&K"ᫍ�������
���.� ���Ixh�I94��'I�_`�(	ٶjO\?7xo@���i%����}�WK!� �t����I���9F�P��^�d�G�q��3�_�IV}r���:Wm���6� ��+�ZdL�'���ݍ���}#�Pxx���k�2��y$��5^��A���    ��m����Κ!D���I}Z7�L��B��6�D�#_���%�Tɴ?����ͼ;�W~ FLR�>b�]!�Z�
PW�UPZ}ۗRi�vP\�Q�+ r.������*�i|�hv�[�a�bⒽ���
�����W&\������k^y�o2|e�:H�2�61N- ,`eB�1*�v��=D�ûC��x`����Ǭ���{\[��b��w��H��K�;��<��ŗ;n�e7/Q,d��|�u�ø��8�O톾#J��mb.j���|[�dh����ɘ���ܧhE��~��
�t5��(���/�_�I���1X��B�Ƃ�}�u�m��64���gU�%����c����)��D PC֦WJ��F��\����JLI������m�,���:��A�x��3�*{8����k�����'��.) ��@�
���7Eb1g����gKxN5����-���Taz�ū�5��s��F�� �q3����s�Ih�����4^�۾sdƻ�^f��	��(�nXhx`�ͧd`y�Xc<�G����6���9�:�I@kJ�! !*W���A�;������Ą5�� �t���Rl�~�ǋՕ�/G���y9�G�1�Jzq��nw�)�H8%��jU��Gm������TT��G�r�}Q��� �i�K�`p������Hߖ@k��zw�!ۧ<����0�p]��7	ge�a�f}�"�f'����P�d��yJ���9��+/<n��y�~��=_�I��O����@3�G���Gv�,D0�eH���^�R��H"�H����/�����}vdZ�X�׳�\�i��j9��)Y���<5������ล�E�$nP�-E���<x75��J$9��h����X��4�5o_4���<�+��Z!C�N��Qa��݅�ڷ�u��z;�Q�31D�7祑)�r(W]P���Θl�?#�w�޶���~��c��#ۉ$�eo�m��-�������u'}�䱦������ؿp���P`疇���bʼ�"�U�7�M���Y�v�ܭ0�M�g����a�W�p����P�O�P~� co.�>�v�(0r�`0$_��*��%���3�Ǝz�c�Y\���6�o[e��˚�N��;&��DPޓH���m+��?r6o���&�7}P���s�Α�h.�"=���PBΘX��av�V�sO4��A�퟊�J�M�l�B1*\ES�a���F}���N�%�l0H�IHT޾� �5iR�g�����A�^p���ϕB"��m��BG��0����U�f��hB��oO`Ui{�(G�\�È�a��,�3��K�^י�����~c��Fz��}�kW� �s�/s�rߐ$Ф �W�w�u�R�Y��;z��v�(>��z�����Br�5�)��D]1c�O�H����0F'�Op�/3���<���d����#�7�]�Z�݉�MP�TǨd�c�#C��N���f�"���gØ�� ���&��*�X�o�?M�@�ٗj�)�(�&r8��W����
�����uq��c0
�������gt�(��㉥�f�·�dEH+��+� �1��~{A�gp #��ӣ�;�e�C�������0�BH����w}Yȿ��`ñ�hwk9����w���������.b�W�D��tj�||�V�97' ��[|�k�nxG��E9���s��=٫��KF���J���y�Է:�/�S ���M�q�ȼ��\�eű1.B�BT�jUu�J�����o�9(�B��I��y�N�e���IOW|YX���$��'��\ꬮ�oK�	��iX|p4*�-�솘u#��
�V��Y_և{���(WB=�-�(��=¯��4�H�?×���Z�P�i�vv�C{}_J:�%(�(w�$��w�Wk<���q�z���
�ЩШ�N&�#���	o~g���5��f=<����Ld�b�+�ش���������p,P�J��|g'~��R�f�1UUm��O��A�o~��h�A�$GL���U֞v�5��B���j�LM����N�$C�,�	���c�<%Qi>�O����4c���kqc��[j1i��媧1q��ca��]���d�^E�#�v�1���<(���ʠ������!�̢K�*"2����z	c��R���Ⱦ�h��x�p���]�,'V��f�mC&Ը���"�n(I����(JiHIņ �d\���$��w�n�N�!n�;PƄ��E��6���M��:~�~q�O�X�c�6�è�ߥH�k�#��nz{9�5��>��QX��u)f�P�s�LAVh�?�GC�Ԩ�*0ns�u1�p)�F3�1P��U���S���|�Z�/����|�1G�d~����~4�B�o�Ep
�G/T�F!]����@����E�>�ג��,�Dmi#e_�|E�4��l�W8��$%ò�{�������G����A��r���Z��d���������%߿�Uu�6� �h�;�
�:0:DZ�KlbA7U7O�\���k��Ip2��"�+���(�!|h*����M�*D�7�l�\]co�<�(��"��W�Ѥ4֓���o��N46��Y��bg�1� �� ������>'�zL�Bs/�����L���}}�	;����ѱ�����J�>@���L��F�M��%a`�v9��O�!Z����oޛ� ���.;r�}w{L�@��0c���~a��-��g�	�T_N�9�^k'�Aخ�/�C��=��˰��B�-������n����o�n8)lV�� ����7o���Hd�k���%�^�ۦ�Q`��9ň��w���7�k]���[�.��Nj��2�U-0���I���5k�j0u�R���ϙ4_�+k��\:��-��� ,C@'�-�j�:A@J(|��9^^����Ur3t�I�4P��IM�2���\�w)�,�25|��HR�BY��B=5��)�|<ϚG��4a-p\>��gd,���c�e3�j��E��#w���� �~�t(�}�:�Հ}�c����	����j܇߿r�,Tga�fA����>z^�ڜJY����7'`�#����+���sN���d��E�5�*`ȏK�Ѷ��.Ud����|�9<i`?�qZ;I�[�k]�r�V���tW�a�編�4t��V�ɞ��R��Q���uj튪����~St�E��L,}��ئ�y����?�:,����hݤ��� W0��9�7����׷y���,a�gj�5L5j���)�0ҹ�����VH�/ڪ�g������vcb�!
��͔�����/��v�o��|_A��Rf�x֦%��Ч�q2�\��H����{�
��� �7�.9�eIy�GDxAI�CQH�^Pl��?bo����
��+ӧm0qw]$���a7}�"������H0������o�Zq)PR�3���3��"i~��4#�$�70��nq7�&�`����|<�@�}� �"�|B+�=Ց�t"��mg�D)�z"���'38����s��M+l!e�&GB�"<�NS~'�]�G��E'���]h&s�ĒZ�E��2���l{g��ଘ�����ʽ�w:o՘�cR07,������O���U���Xο�	����$�Z�Yht�h�_e���u��8�>�����%o/x}P���U�;r��+��;����-߬�����>�A��/�Yc��B�f��/�C�n�VW��J�x璪d�"��(�oyI�F{DQ%\�~��_Q�@4^?Ώ�ls,�S+���с�Q3���d��;b��H2{s./Q|�����?5�����cc��qF�yK�o�>�)[��&.\��v���+���\f}@�x	�Vx���{�,痰�od:tb�\�P���0��6�8GhtQF��gI.lx�w��8$����%,ɲ��cd�3о�c�K�W�;�wv�.a�p�O#�^l�z�i��Q��
��4�V�9�/D|�
�����;L�%�'zN ��E�8
    ��~�V�!}`9!�;�$T8ދ8��2^4�` 1ƨ'��0��Z��TR��9$�˚nڲ�P�ߐt�4�&!Gౙ$0<�#�<O ����Œ:H�\ g�a����oO���M��=�3����H�dIg؂��������-�<N��;���oQ���h_X�5�#z�1+D��2��*�!@/	�7��q�gf����A�	p]��A�n���c�xQ���?�UZؓSG��bC��B6r�o��}<c;A���d<�v(��(�NE�D�F{X��F�����Z�Ԗ(�3����/��v�ւ�۬K�<E�����X��zs|&?�����Z4o-��X��rj�I�/	t5�ϭ�k����Q���nA��]wIѣ�����"g�U��������f��_t�?��I��/�}nr�1����/�G� ������ic��g@��t�\=�HmO�YW5������f��i%7il��>�I(��8f\����p�Aݎ~��x?8uwޗ�<�:�����~.yu7�#ۢw���w�N�B�t`x��] Q)~G)��8�w$P�����9(����Q6C�g��+���e"C22
��	Ca����ٳ{�弼|�(i��@S������/�×
c�L AJ �@TK�p��8 �SgN�ͩfp� �� ��[��-�@C��e	:A%��L��ŵ���=�p��u������x�g;�ֿ����ֵ+Tfe�|��PK7��b27u #�c�s�[��̏{_�%�q:4��K�G�M���(�7�|����T!�����9J�Yj���X?(V����1j��n���f�!�	�����;M;*�:�T���^�d��K��a�z��!J�(y�?�~+��J�.?M$��G�l� �B�-��zK����n2�ƥ�ps5��� ˠ��3�����#�&1�ғ�,�����I�t�r���ȗ	�HdI7{=p�Y�3��0��qf��t.��a���d����@��a�L~��[�UE�~���L�3M�R0����iԊ������.��p�k�|����m�a��]�4�j':��I���D$�Tn���v|���	l��.��{�]JuE��:��OǱ�c����� N��b�hC��L��)������/j8K9���(����&ZԤv>�c����8��--�؃�X:[��3`5!��}�i�����B(���n�{Wo��E7�]J��)xsyU����o�a����7Jں-$`~�^��#�����~qI�:E��s�{+�Q��������)C����~���%�QP��MlUhA;�{�e;��0�Oo|��)�7m�"�Q��p��փ�X��U�;�^f8x�q	�箞/q�Q��J�Mǁ'�O���,Q'`�JUJ���G�Q�Yl��ю+6s�gM�ˌ��ꓩ�6.���5;+�uG����0v?Ϛ���'�ث�<���J��L��T�i���G-<���H��I�_/+�+����qԣ�kXj�p�Xm�H����D�oo<lm�v�s�Y=U�9A8��W����u���uKqS��ʻq@�Y����/ۋF5�?�(��!r�k�g��N@�e���6h�&�{
��	��J�"��4T��E��T�͗��(Qۅ]�P3�z��1��p���
h�=�Y#�䕪]�b�ڢ��(n��Tʱ1o06�}������-s����
��G���n	�p���g�Mҁ�]zP���%�W������Ga��~��#y�Y����0���ܤp�K�M����To�B�b���]�o��f|��.��\�����I��?���@?�BB�`)�W
y�zG���;XN�F(��L�����{Ԧ��@��b}��#�>^�b�.66��5Q�`]�7+}��k�D�C�b�́\X!'� ^�"�}%�wwb�HŤ����Z�Y���M�0�׆E�l@���PP�"b6?׷�.��,��T��m��sR������@w0����e����@W��5�ͥ	dΦi�:�5��"8��(��>P���Q��ن���1����h"��aʃ� )�s˷yh�#|(V(�K���0
nF����a~	Z�F�' rGYdF�e$���;�2�y���_u-L���_H�i��$n��H��5i_~	Q��J.jW��
��}�z���2��*�Z�Z͇�<LlC�H����E;æ�_�A��u|ytqu��Fn�}�qs�P�lGo��Dh�e���o	Ǌ�'�c1qF8F�E5\"wN ��径@r�0���n�2�鯌�85�OH�#'���d(	��q'y	)L-�%$�V!)-6�T?�V:n��U�21_����ܯ&A��w�EX�W��Pw#=50�sP/ׁ*�b.�֯<$p<��kxs������Yȭ�s�]��S�����
��?o�B�%D�^Z�9W��U�`�����#[�7~ؗY�ju�뽬�5�[���9��1x�����alAW�b�����j�z/U�ޘ&2��t�P�&D���6���@�^@r`X{�_B#wb�T%�!�%��ͳ�{i ����:��w��aK��Dv&tU��,?Zv9���F7����o1������E�ZF |]OR�4�[U\`�;h��(J����u�fYX>p��gc���Oǃ  ��--�O�_�դ�$��=���\UF�{��	����M}���|^���j6S���|�\���'ǣ_P]I�U���M%G%�}�Ժ��_�R�% ������$�S�UY���#dKX�Ա�nN������V�A�mf�{��9�+�b��u�H�Z�"����hC�?�o�$�6��)�gq�J]���c�V���8����0���ʥp�ܦl�b����1s;~῿�3�):��&`�gyQ� Bv<#�q�X��9z�b{��^���v�)|K�\���k�O~C:�Rw]��sW4\t�Y��P�������-s��D��/ª��[4k�E������;��:�u�:k(��Gu^(��\ny���4��g�����q�C��u4��~k�%�8~9e|X��N���^�{L2�*4�c���#�j�z�h���6�y�Db�{Am��Cd�!���a���J�Sݿ@�A����a�g���"���þ���{h/Er<.��ec0�{#ځ�-��e+������<r��|`@����2,+[��6@A���l��|�	
�6Y\��%JH�s�<\��WM�f.H���`�K=�����z]��1�D��!��>�{L2��Xe���UW	��z0�WV	`8��	�-,HЉ6+���k���E�O�=��-Pj4�rm�����O�` �H҄���r��I�	6��S��^�%X��;��u ��w��ߟ�z�������Y8��^bM.K�7����Y���/P��a�C�lQeq�r�~� ]�&DJ$!���:�k�)�l��b�p��GO�޻b����|<>�>D��/[ᥐA~���
��#����J3�� ��N�C�h��^��qWl�z�n�	�gjCA�?�t"����+��r�~�9��	���_��O�z��c�K�va	Yϸ�#7��2X�GAN�ٿ�H0����W�ҮMM�E�,n����c�ZQh�H_Y֬�u�]��V	���]ꊢ������<�Κ�(_�Z
�f,3�e����"B�nw��~�̗�W�����D��s��x�{��o\��-��W�QS�/�zW�u��E��o&�p4�g2$�
x���۪�2.|��p�R�iA�M��;t�JO��o�=|��y�Ol����R>{zk�.i��m"��$yZB"~�u��a_B?I�����; �T��ߐ/�M�в���B���aIM�&#��.��hg<WiZ�rqXph���%E���
H� ���C�4�܀ז)R&#�1�9���;`�|כb�߻vP�N�c�1�^�[�`e��]�q�8R|:�н�wf��@t"3����Dy�o�B�FF�Ȋ.vT�p��    [S���2j�;x�x�����|���&�	jY6���������*�#Dӕ�����7�ߪ���̔Ea�*qX
Y�YK�Փ���?|(5v���E����� �^G�pB�4���5�l�5�W�Zr�����9f��֋T��
m�"�7�I�w`�)��	��L㉏�	�����A�z\L$n���Iݼ��ֻ�Ϋ���#Z��eNi���<J�v���Oǚ���n��_Z�/�m��+������.3�)da��}�M��]/�ʱݯڎq�}��Ło��)\qq*����/F��Uܮ,�Riz9S���J��t���{����5/�<|������͑�{��2#��Ej��%Z��>���@�4�^�����E���(rƢ�H�(o��R  x�¿(&1�N��l���+���Lo�2�^LPiKۏ̆Ӣ�(�QXu@������W�נQ�d�}�Ӽ�4?�Q�S�"N�~�y�m�56̆�i�����en|>���@���� O�K� 	"�|K|-vƠ���O���2���h9ʸW���� =E��Ian���y����b���BK�q�~Ȣ��Y��B*=��«=|R4��S��ݮJ��L�]i>㔪�Z*��/�}��/N:��8$m"�9;#\%Pt��}�7pO_N9�:I��ъo:%&���6�[�ƨ�q����\x�e�!��v�>���!��I� a�n{,�;P�3})��l�U\�Py�`�z'�%e���l�s �M��ۮ��n)��{	A*�eFfد�����&_��V����<��y9�?d�� v�8�6�{�|n��;��38���l��8�k)��'f��B]�_P��B����SBw�W����h���7�L���/M�oh�r/A_�1h��\p��\gJ�,������	Df���e���"6����F�����
{[�9�.>A�4�z@�h]�<V�����F|��u�Q���)I`��+9[��D�p�j�y���i�9;:���B=;���2��n�g����<,Z.̀R�#��T��t�ɤU��ۜ�������D��P�t����i$T�@�����>ʛ/$m;~�>mi��Hy�1䛁����B��"~R4�[G�9�l�%�!- ~���צt��)��(@��M
{އ���1>��f�{/���7AJ���+G�dx�'7}�凾�_�X묚$�BϬ�>Gp[��LͲ�=�+R8�$�ȡ�KI�7�G�u�Q�:�i����Ԁ���g�!��+����'J5Gp� ����jM_���T��v�ڂ���#q�i���f�>E�vֆ�e�����;�J<4�bί�����+d����Uy�]�@�7�}3tEd��ᚮaw�g�c��F_��!I��&G4�i��х����rP׮)W�J�����	����&�6!ɧ�Vr�-=�&�d-�pǢ��ւ�|�.��4[���K��B�M9L\�N���9]ٌ�@�Fѳ��>j�@�W5�*f9���l폑�hQ�Ȭ~�3�D�z�)Ii�����o��~��X�p��>�^(�3o?����Ḷ&���7��ՕAh|-���A�$��V����-2WS~�RZvn����u��+(vUnZ.BbQ� �.�_����ԫu�[w{�Q��#o���K?�kA�Μ�{��L�7����+�.sq��Ky�1�Z��Y�rN4��(�ۉT"�6:X�G���%.[G����a�	��`��L8���eJPjk��#�5i7��ZPw]���>ę�~��ynJ�y+�ͺ} }�� �K���V�G�1`���)��	�Z�6��N�_+����<���a:֌�շ|+�k��'�k�6{���v�mW�|�=�0N�~�]�"[5&{��@DX�1�����:��-����r%`��D��9�u|��9FJ�Y��:�}��B0؁���b_}�M�K\'a��'�8v<�(��F0�"� l29�
�T>��y b`Ш�V+[�n%.�a�@���Vad$�n�)rhr�	MѴ(�ǆ�ȣ�G��|�0�"<��cW:�ګڤ��.2�i"�,�:�9 �W%a���튥Sv^�Gbn��
�s�顛���3:���ˋ$cr���S�3��<�a8���N�B���kp�k��(>y��9ӌ<
�B,���ǘ��o�?E�HY%�2bv
�h����F�
��V�����v%<6��ߢ1��k��F���H�4�Ep�֎�"���������x��:1��������F�c�{U�!g�3�m"���쫟��.3�|�c�}g�*�^��٢����3�<��
���@Q+�brng����_�it��*C�|tm�v|�>4�8.�?�X7v�ZP�p<���×�w�pG��4�W�G�����]�{Ng:�}���Z��B�D�U�\�	��݂�Գ���Nz
��:�j�$�:9��@�t�s�����rh���yՠGy��W?�Z�^�|���[XP]���PB�O>����	7���|�n�a�̧ u5�n���6La�2����֛�
8b��9��|��T�¤�'c|̤y�sj���0�D}�|m`< ��t߇h
�,3S���&�L�M��qw���x�ݦM2�����R���f����12�EZ�ؐ�a��KE��Òu��
~i�OB�
��R�|r�~Î@��̚�������oWUK]J�Eq���{�H~nI���-��1V5�b�k�q�l���J}��zG������|f�
kT������r�z%��G����<��d����\�Rd�_j(ҧ@�	ym�}�Hv?�`��Q7��L��s���9t��>Q���k�bEl�İ@Ou]]<cR붗�7�A��!�s���1�O�7�U1[R Y�!d���>�!�P���ew_U�H?\� L����Ѱ�*x�l�ҁ�� �	ݦ۷�z�$X=����q�čo�ŷt;T ���:���L��E�F�G��y�����?p�������E24<���z߱�~�~b��[bٟ�0��®�QY9I�Z~>��Ո��T]"�Mh���_?�4b㗡[�� đ0H#�
�KX�"+�II?G?ܰ�8X�c��F�5lw]���.Ҡd��E����ɴNL��)��լ2�ԡ\��߈ȓz��)V$Z�������NB���2�A�e\ $EϨ�SH&X�N<��?�G��8ޛ�֏�d�8q�V\��q ��P?��&0����Y'*a^ɟ��Ռ@ƫk�v��b�,�͚c����NԠ�O(bx�St�����|~� ��7	��C�*��[[?G�����;��*l�[V6-*j=Zi)�G�f�Y������S�)l��2�v���o�Ê|(��Ё��C�*:���ze+�ATvu��QZj>�Z*%�"a> :gB��bi�O0�ƴO�o�����.���� $Z�7g��(&t�gtq(}܅�,
�㤻���􁛯S5���&/��ly���]m.�U�����8.}�n}�S{"MU4ۏ����@?��c��zҜ,��,+����*�8�Y�i����T#�%�6满&�l���`s�h�7%�CI>Dbh0
�b(J$u��!x��=hڕ3''��oJ�x��$��AY8�4�*�h�9Ǉ7���z-7������_��Mbx����q��0>	�1Þ
9ȧ�Dt���;:�S5f�d��&^�;O5[��{�eN��<��b���_����1P��2�z�g�ޅ�@D�b����	���wp�R`���~�W���_����V5�+ �7�gm�8�?��[�Ae��DAN%9���E���/~�6�p�e!f���W�(��)���gX�d����q�����P����OԎOՎ{T���?�e����	V/��V��U��s�+X�0�AU�v]4��HL���YG݇�,�������g����XNPfS���7t �)�C���������;l���lAM��fA�e�J1	�q�32�ՙ2联�\��5���a    �×������6 q�r���2�,6To�x��[�
���E�m,EV�:�-��.�ͱ��b��*�,�<rb��ٔ.F��Vot�����c���6D4����5(�<. ���i��vy	,����}5`F�8{E�o�y����
B	������lD���6�A�C%�R�
�Q�ckҼ��s�'�{;I�=1~K�)2 �+�/$��6����zui����y�vH V�ᮁv�ic.���hFWl��t���u�9�f�J��s����z�_� ��j��N;)��>X�S��4�V�z����6��|��i�\p%��r�g�O��/D��U|���eC���=����P�`�"A8�zF�R/��^s':/_��d*�����pXҳ�����.���C2�=,\'��,�xi1ؐ;0�;����%Z���2!�`}P���iIP\��Þ�A���	@�c3\�|v�m��B?�����h�}MѾ�O�a���G�K0�s��(��	a<J��1|�,�y��M��>�0���;y�<�T����	c��,˰Q�X�U�ň�?���ۛo](�>o�D� �+�/Dd&f����V�Ai�@�� �%2j���m#��8 (�x��*F�`-8�[�{OTU_��2��/f[Mڋ�^����B�^�\���DJ��֮��|Ѳ������>��ce��r�8���Lތ츛E�SX�P���35�~D�m�n�z��캠��.^2��1�e`D�$l�xa�x�\1��7�<ܡ?�_��Z+V�P�k�#&�0i���䙆�o�j��%;�N�>�{���ԃ�����=;�����V}:�cFO��}�8�	�ύl��厥�������9ר�7#@ ��=q��0+9�sw�������~>��-(/�R~N1�NH�p�t���(����a�զ�T�[Kb��3㒴��^� �����r��[
g�f`�zUL����p�����{��~Tg����ᓨ�gߓ���;$}Q;�B/AV��q|�l��ڠ(u��$0���*P_vy�v1(�Enq�� �u�&5�kl�6����.;�?j�"����5
l2l[�	Qh���o�C6�!V�����"CW��.dWi��[�eRV���P������`���m#�`�S������
�]Z�%A�o�\}/�:�,�>""p��y�x^��}J/���2�ږj� �����Z�x�ZH:�\�/�W������i1�,I�I�16��L�l�ې�ȍދ�0�p�J�כn��%��S���[ �WH�G��BA_�16.��K�~Oܑ?���t��;�.ו�гʆ��ƹ����WC��G_�`��m�S�t���Æ��lU�v�mRF�?s�w�%�V�+��Mx>� ��s�G�8F�ļs�ۃx�o�R�A,��r$��݀�i���I-/,bRPx����/_SeǨ[OO�\����w���Ղ	CdX[:")��%HʫƄ��k���{8��r���QY�>:Ǌ/�ҍ��A����7i	��?Y��\2�<�Iǩ��e/3�X+�[�Jn����Uo��|SF�ZE_���J���J�1t������ܧ��x�V /�Bg�ؑA���s�Iv�8�;�L����MM�HZ{�m��n��ia�!��_����l�?��Ƨ���)W�iB��إ��O�*>V����FB�=?�\��>��0�LA"�g����8 Z��O%��F�YՅB߸���4sV�E�gЛY��j���/��k?F��ͱ����7�b��7G������ �s��Z�*F��͹��X2�I?��=q�_��xWh��ܛZ�_�q�d��^�5L����HU�wd�J��_�n3m�E�wƶ��T��x~�RqOllm=��4�M��2�v��yEQ��������k����QAy��Vd�R���^j2W������ĄE��i��ϵ�Dk��������@t��3&<��J��r�M���9}V	m�	~��\V�`�qo�{Pǔy��gY�R;)O��m�uTT^��n���ᅀ��;��ݓ0�Q�1�[U�(#��Ҳ�e ݜ�'�H�����G6ՕXMd���C3Dz���4��j�Q�\�f)�d�*bש�-�˃�L�d�y|~H;G7���.w3_�b+'߀�o�����oh�%09U1�.��݈=p�<N��;���h����9��ċ"|/LX��}n���!��B�x-�2�T� ����r�|$��s���P��~�z#[��CQ��������,��"��Z���+fӠJ��wk�LS���'&{B��x;=��6���T��kds.\L/7L}q�(k����.�Y���4=�Q_����P�"��.T�NH����Qg��9>O���M�� .��7n?z��.��o�H��ǗJ��Ѓ��hE��t�_��<&~<7L,�P=���[X|�p�{����	�^F.�Ҳ����/���EQ��:���׊���w�,�lX�P���k����2���cK�pI�o�'�h�v�����.Iޯ膿DO��20�;ߝ|r !D����b�<|۱ʩ�Y_��!Q�>߷e9��#x�y+�5Ǭ�,v��D�)�^%W�tH0 zǿ[��pt�A*+�` H*�D@�mCk�<���P?Z�����6����+��6����}�4I�H3^�l��<'b����<|�E� /ƀη��/!��(-3V�j�?A/���v9�����1��^�1�ŋ�)����L �ᬰ�&u�9��+�ǣ�u��z&�l2�d�s5^�Dq�S�v�@x�{��l�qQ{�@�l��ع�T�w��l��0��Y�hp;�	UF}�ǽ��z�����M�u�F͏�z>���^���,�J�&~�(p*��%��{f�OD��t���Wa5�BGl����k+zϴ�k�{���]e�^�<�e�$1f���W1j�L�k2l�I�Z2�Q����-���瞧lLz���9���0��@]��/kcW��"��e�1��9�A�}��l�[�}�榗���μa�"�����5�×-�Z�r�=�U�&9���%�����	��o*A�7XG��TT��͟�}oi�c�aī�'�2�ux���p(Z����V�E!�@v6��4��|��� ƞ��'e�u�-����J`���.|�TG�-:͓�eG}��:^$}kd�Y��9-B�T�rtXe��t�^�%��m�}@m%�l���+�a'�c����Q�=���d��>�Ӕ����z�P�Զ�S����[��~�{�hwdXd���ֲr(��ϯc�6HM�9���#fۇS��E��F֚��V�H�@Ř+I���$T��O�Q?��h������;S��cnw��9���	����"C��YX����#E�;ur?��Q��|t���
9�� }8�	��C�ต�5kbO�t,��6]�ͷN� \ǧ}Q�*g���-�a����r�f	}= zt�>,p��=�c���)���H�O�	tT $�&g�^쑦j����ֽ�92'(ꤏ�O8e8(Y��4y��a�k�x��{�2�q��3N���(���N�i����DG��'ȳ*�!�KiV���KN�Á8v 0ܲ�0 �p	?*���!���pn�psgL@&\#���T3ai(]��tX��f�]�N�|Y��?�%�Q�˗{v��&ߝW�S�O���v�������-�vY�D�#��!�}̌���q�[��#�z�)+VK�B�v�\���"� I�o�΍�8U�v�%D7���r��c�e���y����Ƽj�z0+��T�}&����1h��c�c�'w�^�Ƿ��(�c@��RD@�_|7�'�D�r���B78ۆ`�MQ�6��^�T����x�9P�4flڊ��4ҽy�(c��%�4��Vx����~ �6��<�0i��}�E���;�MƣA0�"\��4L�};^��2<���^���3�:�s���*{�y��B��w2L�?j�2x0�(�4�@�s����}���m��ڧ-�[�:]P��zQ^�Md����2�Dc�    ���-��O�./mo��Bc� ���i8g�^�3��]-�����:b�r��i�n�#���5H�P)�UϪ�2��kN�Q��/����u��e���dd�������Zg�a^w�#�6/�[��|,��,S\��;e�ߗ���1K�~�J�$\��6��v��|G��J�vf�{$�ۍ�s�����#m��I`�Z�����%��鉳��}5��q���ϑܐ��JyҰd���7^؟�~?ij��9�-�X�����t�S�� U��f�4:�ʼ�G�~��'��]D"��aS��=A�f-��6��	���P��.:��@�D+���@`����֫�1��r�kuT3]u%\?i��$��y�q޳pv�?�t�z)xfP,a�O�KD�v'	�¦���gw�UCG$!.��bX�VX.�x���f�WXat��da��~��D��d��6 !�F�V��Z��m�� ��q���t���F�f��c��-����'ɷƆ������~�Dk)���=�29G􂰓q[����>��>p�$�j��L���$��B`�!S]�̆��ky����r�mN�(c6%��;!Ӌ�f��@B3�3�;U؁��9@���iY�2t �PVC�-��Sί�]�Q����K��g�j���"�z���M_��r�z�cj~+"�x����*٣��d�a3��J?f�Ղ�5}��C˺�Ong`���ZK�u��ɰ�Ј��\��j~��-�"%;��(�r'�=+�1�2!t�����%�<��N'A��$��%�I�q)�4��|�GE~P�z�Iq�A��W>+�}���첪a�������V_�)؋Z�>Ō�z;���'咍���zS|�����}�/V��b�(��PX�0����}���q�$+�g&u��fi!�)6e̍6̧i6��2�0ν�LI<�7����a��#Ѳu�ҽU�`�}"vC��G�y��Z��S��B��!�d��L݉"N�����ev��AT@v�\��r��m`�["����`H+������]G��x�d�*��������t8:� ��%�~ 5̈́D���|��t����l.�{�[/�~j�[u�\6荨߁��$ ��c�Mt�	u��O��"�diB�b?i���X��%��Q�4��=��-�`�ic�>UoK_��ԗ���&'q<��<�^�ah��}m|E�[�5�s�5p�M�3?�tש��T:E8^�l�ʥ��c�x�W�cjo��kU�Ie\)��Ķ�Tq�u�lSE'c"�g=�������*��A��K�V�j��^�n�ѫ3��~qծ>�����������&��B���Fx>ϪL��������m����Z���P���$ӯT���A��V@*^A������朕��3���B^�C���В��muA�ӟL�eV-ѝ�a�݆��!Y
1�v�L�Iկ���u �6�Bښ;T?�����=��\��Aj�'����kH ��tp}��1��{G;�7d6�ݝ� `�������k<��=�GnU���� _�4��R(��fT� `��a�r���S?��o+�	!�3��,m�ω��&<�F�ϋHx��3�-�Y��7.��#�}�^�9���A��xh�H!�����P*�'TT��	��7��7T��|}MJ$��
���=bHz1�L��F�)����4)C�i7KUD����
�绋��]Q����ؕ�T�/U���]BA�������q�K�^�ab�
A��oNeo����:��{�l�{8�<����_���w���9��!�3[j"�˾���d:[��_�����������r���$��=�>䒮(�*�%7�E]�ͼ��sDE�bz!��A��R�ͻ���8�.(qG�a���>><�l �R���K���:_�;0?��Ea��L��Ʋ2��$��lIgb�ۉ{aΓ�)�#L!�I��.|I���'��M��[>��� n3Hk! 3�l0A]H 6�F���{��,b�Aw-'
?�yr�K�b� ��������x�"�R�s�v�?Nt��� aJU"������"���3��
s�,�B��I�%l�GJ[�hG8(��
o�{}�l��d�2���?|פ�?�4�|,�2U>Oĝӄ��6~[g[+��N��s?�i�j_�8�n�hGC����S�ϣ�|����EG����ǜM�$�k\H�p���;}
_��E�ȥ����geO��4X`��J� 鄘�w�~�$\>C���΢�0q�����`Z�k�A�(��c���Ҹw��
���(�XB�S�e8y�Wɯa�\A�u������,>������q5^�d���5�o�قf+�����Ͼ)�o�f����u��g���`�Q��@(�Ѝ>��i�e�b�I�,�=>�ڸ?��v��@�S����MqJ���J�H�QV�"�^ÓU�2�T�g��R���y��X6�&
A��J$�A�)Pd���s�{t�r�G��~I	|eCʛC�<D{ 4 QB�/:E�s�1�p�߽�ɤY����x���en���ݯQr=�y�ָ F�I�J�����!e1@ހ�	�_P*�Yd��Ua�2K����K�}�}dZ�)�%��Q=�g`f*�?_�P/�F�OC���=��W�[�^����X�h{H�D5����D���:�(!A
�C̯����p�w��;m2-�Om����]�_�3��)`�\C�	��<;��i������ku+�f����h��5�r�P��c	L�)	5x�hF�^3#)�Z1�E��tU��d[�f{	��!.;��V`�����֞]0��jv�d����l�ڜ�v�FL�v�����1�&�R�49�|壯ُة����������\&�h��[�ڻ$��W!� w��"�o����d��Z)�g���[�����ٝ���{C"�����x�9�U�sn�y�Qx4
��� ����ߥi �0�vp��!���\ � F|>�?>^Z�ޗ�j��)n��/4�w�ktF�8��wjPA8_`%�T��N�9]f�+�vD�6*U\���4�F'�ܾږ��=��3�x?��9�e�®ۆO���($�`(��'� H&��J|��\;Y9�q	��P�� q�31�i�G=%�X:e�7��H�����?{���DQ䖶)�ye��%�^�R!��)&>xu�/�+.��kG�L�7��Pʸ��A�Aah��!���NfL|bG�|6��n;�\�e�Z��g�����Uʬ���،Hd��M�-��/�������`\#�q�?F���Σ*��Z��h�*Ów��9'ˉNd�k�42,�m�.0CC�wnA�'#0d����I��L�sx��NN�� �e}1�Y���@}��ݒ-��͡����Yݓ*F��'�8�B��Y<J�;D�ař��	���׽����%��Դ��i�uKq�0��Lb��6�k����2xą*L�ѯ�T�Q? �����6����~�K:��G�C�=�0��Z� �α�RR������� Y���|�{�蛳Y����ӣOӸ��<��Ӌ�A�y�{�]���azl�n�4TmQ-��<Z��xA�,a6ْ[������h7�%�m��+�*�G�w�|*4�,�Ґr�8�� �����M���ص7�CgX��&j;%D6%r��~(���3T4#����������I�gHݑw<�SjP:�ה��y��"%s�;=-�^#�mפ�UV�P��i�T�H���U�G�d�D�5�邴����� m�z�_���Tl��8�^���g�]�x;��B8Sᯇ�y�|#>��8N�?����6�X���d��y6�(12j�>`މ��U���X|�9�x'>?n/ࠛ�^R�wQ���7V�"0cF́W	�}�^��qYC�9�G?��@f$�ȸ�>f�K�+�����B��k�-ӀAGD��8�	��0E��[���rZ     �Q�]V��R�����Ƕ�5��L�kl��H.�]�.���b�a�%>�p9�5��_��UQ��Q
U����I���X����>������ǳJK��2:Y�C+�L����3��y2g J�e�$�p/�О߇�E�
�?��)�Zo���hDJ��Q���H�� �c<�z���G�0v��/Wx۸9��!%�Y���Ǟ��V��Y���3<'k�ɝ��ک��[�4�H ��~�U��==�ig�iLAWo��ˤiˎ󜀺^�q��a�����C��:�sr}L�{U��	=U��x�e�:8̛Q���+��G��2�[���,��kD�93I��=��cs�~o��*h'��t��I� �33)�D D�x
/_�b��`�!uo\���p?��m�T{��M�t���<t���~���w����n��WQO�6����ʹ�Ki|0a�<�$�Qos���g��"λf��E^�	3Y{�Az�b��wq ����+~'Y�w��@R�v ��œ�V�h�{�{�D�!�H`��E�;��37C�ϵ���>�BS9A��"��MJ�»��Н �����ͧ�a2�c���R���%vX���<� 4��2~� %�u�<bO��Ѕ��) ��"�*R*\�����d�A���A*7Qգ��\80�9S���Y�n�n.�-�gj��B[�� )8����z�[A׈}��J��Bh���I��=g��\���Ϥ����?�d�P��ϳ�	[���[���*��8rmv��*�&�!
�O�d��p��K�����ʸ�a+��y��nbO���kH~�帙��H7վ��]�C�K�E5d����j��_��_��T6��懋�_,q�R�L�
��3ڇ�z��.F�L�n9�r���|��t-}9?נ��l�6[4�`N{Gӯ+h�RgG�M��T�����川�YY�d��G�޲�z�ǣ���I�Z{t�I$:9��zx��/�Fu�ȗ�{�K>L�����u�dn�<�ˉ3�8��"BbTI�d���iK�I+���:k `I�!� ��|f����k�E�����_](�)A:�䖈mף%��{sq�U_�Z�Hg�x�m�{xO\�C�@�"�̊����:܈���Z�s�ra�3F��|L�'�ѓM7����8ig/뉮WJ�Nc�vU�mX�t~c?#o1]��S0�P�܏	�z#�U'&�5�ή��-W�m������x���t���fbf�]cҬ����Pt�H�����"a��I����`&�ȯ"��)w�U�����rj��z̦��ҍ���4%�>A֬�d��AN�G����׏�t�̅/
F������4,h�2���2�{�7�`kd�G���k,�����u��7M�Ea/��>�a��{4��4jC���x��T�^al*ͥF��x8t������Җsޘ��^�^��}�2ʵR�)nK� �%$
[�ˊ}�;Ʋ8)V����b�J���	�eY/f{�@�n���r�3ʟc�y� -��Z������?���")	EAS���kT���+���d�@C��+1qnѴ�C�j��u~,�{ �%�ۇ�ce�����s0B���f���i�$�(�&�m�^�ox]��0oC�C鱜C�y��y�{�}d�)U̎|����/T�
����M��,��0��[��"��)�غ�[��q�C��R�Ԑ�iH�������䂯Is��{6mDU�	�	�MJ�����o�(:,�l&˫m�_���`�c�.�~#-NN���(�|�hdAr�om�/��
b��u�T�.��B�����"
����uH:ԃ~��R+���Ɠ=�����ZK{(�TԖ����X4u��,vD���Q�j������#����A�C_edR��Ĕݯ\u�#op`��S��5���o��I���~p��I���`�s���R����D��<B���[MN�wJ��L�Ot�Y�k�3��$	ߗ 5BɈ8�
�gz�m����*EF��2]� ҊHe��A@{����1#
TȆ?H��Q�g2n�e{v*3l`��X"t��,`2r� �a�����A ����@-��
�f��%,z�ԫcך�x$	u�Ƞ�D����}��&��闯���i�CMy'�D���n����d��}�d�$Q��v�� ��ʢd����fʒ_�sK[�nqN��B��`(t����Gbn��C�A6|�&�����t	C�6o��"�G��I�=�G�Z�?G�$�_h@\�Э�^�H�sH	
�a�4�0l����;�������V`D���p�������`�{��W�=c,�ѼTZ��a�k��PХ
@?��A~��z��3̹ڭ/suJچ�)H"7u�s;�^���z��B?ĺ�	�Z����*�~Μ��T�r)�K����	ܶZ�=�V���И.�a������tQ��4$���h�M�,b�e^����gE�a×��_.��U;#$PC(4sG�v��H9���s�)*��w�߼�>���ٺ�ȡ�uC������s�����g�R|�'�Ȅ���Bx��oZ�a�#�y��B�#-eN�U��I��`ϫ�@��:5���O�`���e��u�ǫy�5��Wgw�{�  ����%T-�RO�o>y3�/����� +K3���SG$��r��/�Fq���&�	5��X �W���Zف[1��O�ĻT�T�����Dk��I#Xp{ FyC_G.>G�w����|d��<'x_�K�C����3��v���`�JI�o�mP��۬�xx׉�8�3F
1?N�G�YfU��c{ܯŪ��z$9W	#����I�,�?bE�]��R�-�^��0�Z��� 33U��}�+�6KɅ���~N�
�K�Q�2��5K�F�g|�l6`MM$i~�qi������̿bљ㎧Tتm5?�4�x�����hD6ëm����x��p�っ
#���y�&ڵo�jޗ����O�|��+Պ�~C~v�5�GPA�����N��>�7+��:�[�$�8�ȋ�N���A��n!T�rJ��&�a#테b��� ���q�𫉆Ax�S*~?����(B[x)�A��?��C�a�.���=�������6-Ǔ�Qࢂa߿�������w�y~�P��=)Z�y9`X�LK��
�wʅ@W�(6#�a�
TO.F#�����&ң^OSG�����E09�27� ��oO����9�}k%������f���]��g>-kw]R�2m��L�b�4�_DB��|Mq��&QN�ř��zT.��S�^E�}�W����QhY���C0�9�r�>��#�~dK1��"��V��=����WL��)���#d܃���@R���,��6pmC$.�oSMO^�Q�`mk�Hߚ�"�x�@��"�����PǷ���]�??���	z�6,杻[��ؠ2��<O�O�|��S�{���cf0�\{��6 �T_b��y$��0�:u�H�f���g�Щ*�;*��GI9�N�:�y:H#�o�(�Q4: ��kA'i�BXc#c����k���#S��$�F-�G�	5Ldއ���O��X*�@��^��\��������g��OQ`�P���ǨE�9� �Zj4Fi��I��l�� :C�࢏���JB�ܵ�8
��5՟eԐaL����L[�oћ7nǣ����2x;8#T�+�3}AZ�2PFk�V8����y
�d��0-����3��3��v����̬a+�@g�1"����0�>I~Xw�z}���`AW"�Ϗ�ms�6�ZD�&�������6ܡ �U���ú�狃��)R���Ɂ,k]��{^V*џ��vE.�2q*a��L|G3p��P��~�$qO1�iR������9�CG�l���7Ga�����о���
4?�n}.�ĩ[�Sc!4)z�H��
f�cm��1�j�݀0?�/n9͠6�cc�8���i d�Kȳ���D=�-'�j�3��DbWF���oW@��;�}��|P�X�����g�i��6�����1��    �p�+Y<��5��L'e���bOF4Jr�
����nj�kKk���&��hz-�z*t�o���>�)cSvgbp��-#�|��H��H-!/d����[PU�B+�v�2Dh�,���o�%����[�X֦��7�u��W� �8�SB.�sV���}��̇�q���Kd!���`�D��u(�
��qq�"�~њjA��8��`��~u%L/��͇z���h��G����/m����hӍ��,I��k���&���m����S�ZQ�Jl}v�̊�o27+-sh��~��`��`����P��C}�QӖ�
S�t鯔d���X�U�rT��`�x����k�'G~ueS>���HE�wn\;�!�j�Wr��\���Q"8Q���
�]��0j;i��jW�T�u�mU�<˘p�Eaǋ�}����j�
��>�Z
��Ӳ��9��|)���b�U��� �!�f�� _��[���U�N���׊��MT5?��ԿU�
A1���;�X��S��r'����T>��5���'�iI|,���WFK�@_�d��J��:��[~=�ڽO��X�l��Pf{^�8_đt�q|r}Fc�A�a0�����p�֯2�<�_��)���ۛW��\�".����YG@�	���v�|�0�Y܌R��$4�>9�rY���8���,�:�3�N�!�U��� ����u٘���'
9S`O~~փ�m%z��s$Z���~,�Rw��BpU��1;��p�^�����+[�D�� *ti���,�љW�ؿU�b+��_54�]��{?��7��D�q��:I�Z7	�޻�a�P�Ҝ���2�rG4���6O�w��]L_|�-��I����`5Fz����Hq8Ga8L��T�՞�Ƥ�}6&3_�IY�������EvS�S�[�6=��EnbR�m��c������!x��J�7�=?�l�}5m:8�=Hb��4!�>�p������.�|b,�ݿ)J �[��!c(7��ς��k>�F��� �֖�	�6Y'YI���?���J�v�=mm�V�%��3��K�_M��A2zr��ր���܏pdK|��ǂ�HQ{�5�e\��X�؜S������|	�����梍�߇�7��c�#�f�����ZԐTkM>I��=�����a'�����`�x�MdD{��^���"BGuo)���$��ˎJ���}mWm��@�=e����Җ��-_]s`=`�$����.]��.�r,巈������e8^����	�	�oCS��pa t���-��t� I�θ����B�P� �h(mg<D�!���	Å` R�%i͇F�/$s��.Ȑ\m~s�` �ǚ$<��%E����![=�� r���6[D����Rh�{��W���Y�É�n+�	n	�x���)����9 *�۪0�̈́^&���f���c��Pz��hQt�h�)�~���9��C��
�R]8P�!+��=���dS� �(�j�Ӭ�֭R��]������TG�]N�˄p��>�F��P��Y�oQ{Aߏ��Q���w���n��O����m\��H�t8\l�S�"hN�P�ET'�%�S�8q%ѐ$��
��C��,��39��,������� &t�3��F=}$75��|�P�[P�7rs��|������;SR���4���D������I����]x=3������?1۶'�P�$�Ä��8@�h�>�k �c�?�N���|�
pL�+�ztC{�αZ`o�j��W�$*~��"n0RA�$�|<	�������z+o��H��hF1��n�yn����#�v��֪����ތ�����J�.����|?y���
�V��PXl���*�V��啇�A�0��N�z�ɹ�_��8�?B��m��.�-
�!���s4�φ���4�D �w��PX��U�g\���U��G�Nc��$_�$�#�m���i��;������Ƒ�%b3�
˲Dz��YD4�$��Z�Oˇ�h�˅LH#�\X�V��ð^s��&��ڄ��FO��P�}}�+�(�O�p@�>r����ڣ��z��-S��Ҳ'�@ _4h2$��D����[êo1��'�4*�����8�<��Z��w��	&ޯ�vz��8N^L^ު��(������"�
����
�dp}ޖ�x=�~޶��%I�qS��(��$B������x��f��Ǐy#�Jꂔ�����X�n���[��t�u�T�a����z��k�	����J��A���/=�RW Ҥ���T�Ϋy�z�\24v�]5#K���V�EV�ӄ�q�:Wj�����'�M�,OIA��7x�!� 4�A�}� �&��2�o�	X�Ӿ��Gqſ��ۜ�����_a����	 �)B/��$�h��o<�����vm��&�f@�GϏ�}�+�s���L��C_���ߵk�M�`�+~E(3�K_fޫ&h���W^mG����)�������xӄdj�+�n��{��n�0l�u�j�s�{���6��c��� ��h�!�O`f�g�}ĵ�qp���,��A��;�zV�z�(����E3%���s��$l��~�ͤY�F������&�-Q�rd�b�c�"^�g�Cզk�Х@f�Z�
�5Z��8��e@���]a���i�	n��|:YjYg~w�		���=�?�`����laqq�{�������קӯ��D�� �H/C�$2� ��\<��UlN�{a���|_�ZP|�Q�m���q�HJ�Z�芵��_�c]p��;fj�1�E�)���|Ċ���^�3���e�铈�L��w�3���P���J�`pQ����\�]�$�Q��!�벌-�l۷x7�4 ���� ���kPTV������|���񠤸��~���o�$���Q6z0�=-$_�W�&�}f�j���,8��6��㘸���N#AM��(3�:�d��ֆ��3��jVbT���X0�d�#�6H"wW��	f��t1�g�h�{��m�|���Z�J���B�Ti5���Җ�b�5��������\by=�8�����D�3���e?Y;q� ��f\z|���M���v�qq� G��*����ӎ��f��4c�#���s`��ʯ�N��(�-��Rr��2˯��� �(2?����3=,·�.�*ZZrI0��w���A��W;<�᪡���ÞO	��E�C_�VD|�=�Ɣ�nOfO��MA����`B.�N�RԘI,r$ܜ<��>�N}�������~��@�Qt��R��g-h~�#�⻛	��#�Wbg��|H�UK=9�F�A�`�e09D����C�j��0���6{RQD����V�����#S� �Rm����!�u��|J���$�?�������!�DX�����qTX����C�+�>�~��9Bk���Ҧݔ���^V�|�Sd�u�&g+�O�=�"���&���K�+���m"���p���ҡ���Z�ZO؞��2�t��W��cA��(��Km��$;'&��#����q�i�Uf���f�w�L�it��G����V�<}l�|�|��ˀM�s��KjN�&�_�B,�Q�&l�9��VE�Im/S���O��)p�;j����c��'����0%�5���V2J��L�L����?�Y�ッ�#@�YJ5B'��TQv(`�#,�5��u��_�a;�깊��9К��P����n�䙞>CN>���:r���`�q��*]zg�ho��������tb��I)f@r��E�M����#Ht�o��ᗵ����
��z���A�-.4�2@�;QY渻���n� �p��o\���g����|�=.����>ZY�Ub�9��q�=L{AϘ���/@���~���*/	�^m<��t��~,c�w��Ma�P�Qa�#1YY�� V�:�=�/�'m�������u��b��$_x��&��;i�M�y
��	�
� �qIV+C0R    �Bl5���I_�����7$�/�Y�G�n��^/\��v���x�e��h��P��[.F�����K�3.ؚ̓�4���J�~d��K��&qj��#�Z�D� �g�ۉ�����w����q`'�o��������;�س�@ː���q�����U��B�	���͔4l��3��I{J��h��I�l��Z.�n���73���zx f��Y:xILw�%#�'O28k������عN�Δ3I{}�2c_Դd*!��%��V~����9];)��_scÞ���NS����%��Hr�\�"(�yb�O�q�c�+]�ƙq��#��E6�n�.�~q
�=BW0�K����O���PrN�C���h����OX}��I-��
]��"D0B�cZ|4�����{�+R�U���a ��8�m��-}t<���ߝ!p���$0��E��Y�3L��0hL�"%�
��f�S��	��OQ�*D�!��ݒ)�؆�\�RIƌ�u�0j''T��sB�
e�|7]����XS���+�'�|�,��n�dT>	c�f���⪝�6�D���Ŧeb-���G�g�,��O��c
-� �L�n��eM��h�ƍ�v���T�5����W��k��|aW,P��B�>,C�P�����~D��_����.h�e�%cZ[��v�/f�
����:�D!�5l�F�Ѱ8���B�m�\�l��k�MZ!�"@_'c�3��ӷ�E�C��+T�=)�%���G����`^��M�mќLA!ác�.,dh�h$ʤ߄  ¼�n/LqvGVg_�[.�B״Va�{���]�v���)��� (��Fc���e����ro��g}&a��1<r�X���g��*@�r�HaU�%�Sb�o���O�p}GB�WD#��bʭ'ֶ�9̥,�u���x��@4Ũ���'��_��� 8�ΰTC�]/L����ƴ��dU�D�i����h�PzZ(�5��/ջ����	M$���7���U9ם��ԩ�Z�3��vH'A80�Ṋ�N���Ňʱ�q�/�h�7F�T����U"�v��s�0=q1���pr쥋k�m�r��"ߌg~c\v���kƈ��)�V{Gl�!��!�	�ԯ.�����jL��;�.*��v���g����A�����-ecس��8���m�֢b({�>p�3���浾ϵ�꽤�.�:��Y��ݾP����y�|sB�SƳR��E|^Rs���3#�.�����.^�J�g1x���]~۪��F&"�kC�d��j3L�d��wWÌ��%��wu�P�±!�N�lFX���ٹj��Z_�&<���j�0�4�~J*8<��h�`����'ӫ4�ܪ�뻵c�S�H� 	"���|�8��-�L����+�"X��u�
?U~/�	P>G�u���p�߃� wZ:~�U���+��U�7w/5�C��$Pv?��~Z�]���r���ĀAyB��F�xĢ+��Q6h�ֈ�W�>�僲ǥ:s*�f���	�Y���1���{Uc���q3��X�g�oc�u�L�1�e��L�A�PN }�"�,�vP��О��ja`t���xo��Ұ����bƃ��c�>��.��H��?̀a�,�F���q���^v	���+pwn(�����dIMR���X�7y}�9@���f�P~ǡ3+�A~%�x! '�e�K`��dvo�)V��eZ��^���#����[��Uq��}r83�V�e��0ˏ�o�a:�0�!i��(k;�С�>�&i�`э<q������/��>r��ẑ���`����EQ�j>���T�s="�;X�B3ih�8��/�"����ڍ�/Y����v�x��;r}r�5UTЫ:A�r*_��F����9�5Γ��\	�R8\,�\o:l ��抆��(�|�I,��2�k ��j��`��h�Fu�ؔ���i������1��=ɠ�]��:�Ѵ�ڬ�c�ʍ��@�~sVE�m�V�Wu$j��ƐDSt���������6�'AN�'��7�X�EƯ7��+��}7t�UϖSM��Ы8����pgLwC��m��-M|��u��d��#1qZ9����ec����y��Gն (�o;M�r	��e��^�k2K���WY�z<j��	,*l���Y?��-W8{�*4x	��2��.~������0o�׻h���h�[,��,Z�YP�S߱f'`�~�4�T�_�����I�q�����Q��8�"�)����XC��A�z�7{>��z�T�r��M>�������=����]P̚��uA�w�������v����&��ò�:��̡G�u�r摾Y�Ȱ�Ňrɴ4YR�i�k]I����R�x��V��6��s�IF�j�*WF7�=��
0�ô���*��+>T��5�>Q}WVy��`HdyH��F����;a���w��q�59
Q�{��"�Jᤶަ�F\��K8FL�Z$�&�Ff���`I����+���vcꌜ�J�����:G`�>�MW�>~j_s3��t\�*�(�� f��romac����v��SZe����co��Mh���u���.�N ��\���웟��]��Q��t���YF�d�n�K�o�7,��Io�(R���=Qhl_���F��ՙ�ڎ ��G���̐C�9)��	�e^?���'���`)�L�6����1�1�\�%�����v%�w���u�Qe���:֪���o��� ��y��z��vCd�<����(�#v6��\�� ,]3���{_yIɵ����XC�8f��A�~�$�/�������a/Yu��C���7�ٯ}�����U�܈3�����5��+�u8����Mb��ѻsGp/�\W�U%Z.�?���}�����<�Nf�L���/_�pY�p-�1Z�}AL}�8!�?{B�Zq���j�"d�W�.H�D��	����ʧ4Q"R��ɕ]y��i40R�h	Ϊ�V�]���ว��.��,Ҏ�M>�	�~���^.�u�������9�f6�7�M �6��B(6�������0����Dy�'��?²T������X�*����
9�$�U�#(H��*q`"o3g��?���w1�W]�k�f��Ǘ?�n�K?j��o��D�>���e��ʞ�[G�a�hYU�MT�%��80���a޴ if�O�x�ގg�s��Ŵ�e9ᄭ}�T�?����R�-_��a�E���`��%���B�O�j��vٞa��/[1w�;5�Ό�j�'��5��z��g�;Tv��tiv�z�H����U�9͟)d ����)�������|�v-��Ԓ��f�W=Yh�&�*��:�洬���v��B�Yd��3>s����r»��	�1�H�e�0�,�����y���3�c����GQ���W�C�5��������KH��l �)X�fY �U�R��[&�/{�%M[��=;lB���;'1��ELtj�2W��zlԠ�P�m٬G��
���!7H��S�[:&@z�P���vP_Dt���8sn���p8��q?-� �G__;�����@�{��N�8	+)��F;�|M{U$��"�'"c.�E�6�~��iZ8B�
���6��4���Qte�P��*�����}4ײ���t����`�����+��"�����R��ծW4��3�ۮw��j=2'ɢ	�[��OI>#<�sS�ȟI����2� G���3E<ޒ8��~��j�>�d��w�U�D�Go�/ۯ/�m)R��#~t�B�⨔�#�4Wn��'ޓ��i���D���[��������m/�E��	�W���5;�fiWe�W֩�>~;l�F	m ������`b/�8�pnP6lE^(;�*|�aO��+t��X�r��E ��X�l ���8&���_Q��ެ��o�eZ���~�rr�p�h���q�1�y��R����ǥJ>\nH�^��`�i@��3ԥcNtz��|�Z�s�~.>B�#����*���
F��zW    ���~Q���vB%tu�tuW4s�y��:�Gs���l�E2h"	��.�6�����k��O��ԝ���:���C��@
�N�i�E/�K���?�z^>�����wߖ�@���i�H��P�[�/�QM��T|+���q���έ`�>���hY���FFn;������/���EH��->'^z&I�õ��410��Mߨ��?:�&̇��		��c`�u�� *�R��Ri�b�=%�dw�K����ݡ�$��UXF6NR]�Ʒ�(�`�W��B�(|��vUP��1�5W��@�%plX���N���d�'LK���^��k#��ş=�!B��G.��z�']z�UC��T�bU�o���<�,)���Zd����m���]� ���U����|�z���ߣ]�K0�C9������ܳ>�l.�����wlv�5�I��٥�(�*�E�y�o�P����GFX��7�Ŋx*l��m�_r'���"pc-�wK!�&�H�Iל����}ue՟�##~ՙPy�B��L�s�O�m{���7s�5�W�w�����X0�C�OW���/��~�衎Co�oEx_���J����a��=�j��*e�\šV�b{������S��ݗO���;r������,	Ǩf�%�����N�|cU��n�Li�$(T�e��w���~�!�&��(�#QZ&VE����39��cm����f���r��?��bBibݝ���\��~ߡ���m~h djn�X1��	v�
_%�-�k��x����+J�dGې��L��3<ZǙ��&�/�/���y8��9J�*���:@�_�_���/+⋙y�4�)��*��C��(N�P���i!���¾��i����!��Зw�e��)�c]t��2�u��?���%�[�l�����{p=%֠���űat;�`���I�Z�IM������!�gxY؂�8fJ�rA׶(��8�:�U�U"�[h7�W����ǟ%�M�q��;�ж��<���K�rh	�W"�MOvJ��j��8�lW�0�̰�������N�!�Cgr��>��:�z�E�+���U*&�+T�����w��E^�o���qR{`笚����Z|�{Lٛ)C+����E�5���k/>��g������~%ܹ�7�x�xP�-; hK��S����D�n�)�@��U4>�������*K�'K�x֌�0@F+��]��&��4��Q��l4�Yv#j�̆��z��o��s*��7S���L���5�TƂs9��quk�@]H�YCvv#�����@��f�Ŭ4��'�q����;ZD�/0���J*�0����G�>&�������V��p��}���"nf���I/ 5�`��;>T��m�������^ϣ�<��3(.���'����x��:Z����{�78�Z���OJ�K�����]$^�?$��c���Al��Ǌl2����u�ރ˓o+N���۸͇��`&���D����I�H\i���*[2�� �o�p��Q:1�c�Y����}��I�O��mo�~Ӌ�J��Q����]�IiH�m�Q��V�]��*��tE��ΏϞ�ԇ�m¡�T^�v.e.a�f�Wt�O�l��xk|��E`�4$��x�n��C�������f����&��
�M�1Bs�h��ʰ4��n󶅁�%�C_#ꄐ;���|��X������R�Ʉ���g�2��:1�T�7�F�sr?j	,��ox�e��ò&.�:@,�o+#�#풞E�; C��%κi�=����(B�"v��J����fM����ﳸ���<�y��N�lr�U�X�c"MD�5��H�M�)�P�]/W�,K�_��I�&�Cꅿ���B���TN���Fh��%�~�Vz��Zr��e30��>l��i�w�g&��`tw�C!��취VS*�M�*f�-O2F�
�pAk$M�H�;�P�u+�Ӝ��j�3/���q&�{��k����>�*���4�C��cZ�&����Jϰ��w[��&J��_�F���Spk�.�S8xVYz��焆 �;]CE���ÎGDD�}��~�o�J-���^Z�LpZ�q?27�A{�*Jpv2�F�5mwM�F���z
@�Z=d��R)]�~W�̽�����F"-(p!�H�Ry�뾀�Y"5'�!�K��>�u	/F�n���Fᬅ�p�����>t�����ߕ5�ݢ:tT�"x�X����ؿ�G.Y����>胫��.F����a�ߧ��fB��~3��ؗO��
ߒ�(�x��U��"^�aX,J.
�DK��8n;�Z��ڮ�wO/�D)GUկ-�=���z�ݢ�&�Hv&�%'~�����f�h*%</����p>�FX�C����ɉ�6�_Y���OU�Wy��B<�ʞ@�/H�X'|�K�&⣥��7�ß~p �:m�E�o��f]=����Y�D�uU��n�L�4�Ē�i�M��a3Ӝn(�c�qr=Ԯ	�&�Q4�Mo6��{��uN	cͫ��V���ye;G��A���:�%�<M,� ���=�*�4�7ĉ��:�v��{C��$�}�QCm'�wy�pjrZc���!p�?��X�:�o&eHl�_�8��m1�,qlw�ŷÅ,K�	�<!��U���l�n���������!u^q�IAv�{KO�*!u�~�����g�ơAهX%�=�f�.:Z��KoRY<�{��bV:!%�,xj(Ek��~.��S��2j��F��K]�u[��~� ��2]�ت��3�M�e��O��r��r>�^�L����8����|��gv����k�� ������p�d� ;��b��ߘ���a2��hsWw*��>ML�\�>=�]�<Z�
I��6s#��ꡧ�1*�]@Y�]7�]��G�ъwO�h]����u28t���R�V����e���Fj��b� ���6:��c��RBo�I���I	- �k������8��b��,�ߩZ"�?2��w{ZY�&�����?,���LU/��t23~�[.X�S�O<�G�婂���&�y�-V$�F���p@'=���������A�����9�i�*�hT���_��y�5��&��>]6��f��F��}���dѫz��hTM�r�1�l8$$�����R�V����γ�Jg��$&�8 2=U8?J�����nl�>�%uD�]U�I�ߌ{_l�!R�����]���%�R�xۋ�J�C��
:��>��ѧO�]����m�	����J����z=
�Ч�8���㪏�Wڂ��@�>d����B�$ft���)]�N�(+ù��e���S�B&dm�7��PL	d(R�����͈n��B#�5�tъy��]Iu3�:�(zT�����*͜�*�8�a�q���]<��I��i�o�-��1�<�������#�4Nyۥ��іB�( �������@�Ē��#/���KW����hQ#Z��}���/�ܼ��`B���1uk�ZV�����$t	���eMҽe�s;<w��Oq�I�m|��s/@����b�%D��jfQ�4��x=�@�i�2�����Px7t�����1�q/>A��{���0�P\������@N1�\m˧E�/2~�X0�Jݑ���d�{n>���H�m`�� �"�Pe�}(q�E�����Z��0H
��g��3
��h=�|�&ؤ��A};�h&�b�;��3Ɉ_4���2P�|SJ��w� �Wۙ(�6���P�s��`��<��#�����w��	^���X��U `�0�r`)4>�
���Ĳ����^Ej�f�@^���>�l��
��ä�E�x�� s]�#��Mh³bꢯ�X�����{:�1��t�k����e�n�6��PsR��9tr,�mʂ��y��S�k��x��c�?�>�x5���k� ��QE���.�򳮫K���]]R~;�i�"��$�ne��l`��d�
����-o��ACQ�Yxv:�GppP�Y���VB@s���J�G��g$�����{1�3��.[<^�1�)��    n�'Xq��oS+؅=�'��9^8joWx�3GJ���M\�1�lP�f�#����L%���`%NAo���U�)������c��l	35�A��P*��8sy���$�@��=_4U`G[�P��ՠ>}�^~����y���y�#�N����9մ�t�"D��"~��������M����?a.��=����:U Z���A��@CE�m���˞��%1�m�}��p�)L����of����'��g�o���.�_b�M��ep�Zn���n	X��TԾv�c�D2?e�Ԣ�\	��nKJr���8`�`_a^֒Յ�$>ֆ `�躅� ���fI�UO������Lhb��ۄT��8��M7�H�|]�َ�g��I�X]��l|e��I�o��`�r(�Щ�=}� �D�LƏ���h�{����j�I�I�U���e���!�>���zn���Az�o��Ά�Sc�]d���.�(�����I�fMm��h�=�h��z�xs`��%"U����~G�	�j0�o�~u#�t����/�=���V|F�e�rl��\����x�_����T��7��	b�i[���F$Sn��(��Z�Щ��'�ۀ4���'a7��5��-���6�j�(I��Kn2��(�A�#���W��U�|x>PC��^��p�����u��#�B���~K��e�@0j3���q� ���GQrR	�2�oߙ��X4C'�j Q�LnƔx�pɾ=�Xk��rx���K�XӧVqyɭl��b�F^`�zS���
~tk#��A~hd6C_Q�]���؊��Z_U	�+T�Y��h���g}N�@g�A�vN�$_y�ם�cW+L��o|��U���c/��"�щV.�V����yL9����G<8S�;�(��;��o��"�>���q��ki�+)j�ǋ����N�G��4����R�:�q}�J�Iz���n�!I��!�]ϸ��xT��*_=ƾ�XE�ٗ��`p ǢL�&�Mr��x�/�',��i��ݮ1`��c7`Ӷ��DŴ�;�xy�iB.�ʇ�� ����?}��!�������8c�%����N|o���|x\���Z���mD)~R�r��׀ �GȔ�u�r�̪�ȸ��tۍ"� �$� 2��9g�~��,���}lU�޽a���=m]�|��k0��e������_b�˷�?�T-j#��O�R�ۍp���z�XwZ�w�.�w���`����5�_��u��AɌ�X��gE�A���`��L�Q�X�Zl���u,��Ny�EEc��}��W�\�l�YhExs�,�A�YN�ſ�3�y�x>���;"$Y����V��tqe�#rjz6��c�9������VV2ɽ�l��:�Bu2!�Dq`	ȑ5Z���z@\XnR~B�5)����ٖ����}N���0.��k�%�Fa�.%�p^~*�y�v�
`Q��?�b�j�����;9��&Ĺ��e�2u���4KgL���z-��o {�X=��*�H��:	����~(��ԫie>NC>B�݆a\`ɟ��sr���C\��ް����~�̄�P�R1�o ��/Juߖc�!s�,	� 9Ȋj��d�+H���H��n]R��W
���]�U"��H�f9�W�F��z��Q��`��"<��9��bDv	���g��(B(�c��w%Q]�5�Sq�bCR�/U�u�w�s���G��Fؿ}Y�~���/�m�uK�R�&e)h�yp]���$O:�6�����"�
e�ȕІ�t��!<�q�a�'�Z�u�r����v�(Z���!�Qv�,;G������k���g�`��RD�/g�>�0�����þ|��&��D���MDT��5l�]�zp��� Ȇ�a��q�*���Z���3��c��noQh$W&�����k"�9֛��
��	);ۚQs#I�I�c��`��R�)������"�9sA��5�k^2B��o5����#��ۖ����a�ђa�����4C��mH��Z���G�;�
��z�}ndnk7v)3��L_������Q<�O�����s̺t�ܴ����0|�v��"����@f�$G�M�v�Z�
��Bl-F��ST���|Ā�+8縔 cJ%�^*-��v��%����y�u�yR��mK
jb�0�o*�JP1���F�-=ű���* ZzFaxNbs�`�q�j�{Es�Â"M[�&�ߵ!x����Cʐ'P����rC��ub��s/��ȸJ�L�N��X�١(�����n�W���Z7�т�V�He;�A���5�λ�uv1��.�P�R�c�g�R#��\����9�PA&�	 �K�;���d&��,���2� !�BP�;��#��L�@���4����<�혳��j0F|^�9��<�cr�D�ձ��DX�����])p�߬ǿ<l*�#Fܣ��^�J|`2Y����P#�C�wH�dUi~"��Cݿ�_*��x����*��Au�]�S[X�rW_�=���[g���{UoDd���7Iv���l�1:�ѬFb[�/���d~~,)�p�*��h�c���k������ϧ���E�6��lߏHoev��f�=����	`L�0K���wx?~q�ٯ����f٣�ˇ�Nc�"�^Y��<P�c��8X_�wj)�\��E7�o膽w�cOU������'�Q���+���0)AT;��E/׵�/v�Z�TD��ΩB��d���hVt�U��Us�0��(< ���J����|v�it�-oX�k�$���O�@�v�_�q�2�
�`��[�\�I-��ܨ#��a��4c�mK�?��RH�~�����Խ ���y�5Y��z�ֺ�h�8'LߗWE!�[��b�p��-�hV�%���"�׆:��a�m��!�q0��Ҧn��?�g��m����`��=��$|z#��C�U��L>�d���Rs��43Ӂ� P;Y^X�
5nj�� �R^�����L��A����N ���� ��^���AA ŝ � o�͑ź��_��M�@�FP���=��:b6u[vݴO�rz���z�Ϯ�Nɦ+S���������*-�=�y$����g�[��1"��_^|~���8|�ܠ���c���J
ؗ���w5m�,�d�����TrY�c��dݥj>54�;��0����뵜a�:�Z� �"�"����J ���DlVN%Y�Q�u�,����1�%\"6t��3�zZ��5
�ucU�W��۟;����-D�v�6�mN�^I���?���jʁ�y��i���9�����#���@z�/�S_,b�Jl4��z�p����c�����Y�i�;��ֶ� "�B�g����x�*6>,�K�[-�=�0>��s� x���AW`��������p�naDz�&��[�-�y������ss!M*��箷��%Q�(�k+I����]_���C���9�xv�W��n������M%�~R�U@�&I���LZ@ϐ�9�|��R�v8�˔S��rZ�g�j�W��6Q@/*6BQ���_ۛwc_6���E/N_fR2��6!\0�LoH�b
�'J�M�!z8��H��+ ӄb��p���(w�zс�E����M��Cyk~��8�`�Wvo���g=���A ���
:R"Fh�E�nߓ�*:��(Wp_���MQSpx����c�װ�?���*s B�6��ƄO+u��PǒJ�7���e!wYlaB�j�|._�����CތF���D�kѓ!�R���Ԟ����ׁ�B���y�޲߫H��J��~髶 ߢ%�W���vh&vh����am��n�	 �'��M��K��C�-�����r��H���>i�nu'G�@��,���_�&����-�j�4Gy�gF�Y�mR�r�0���V�X%�>�ءN\V�Vùq�$�vzPt� �Ԩ���.K�BmJf+_��h��z�9�������>��f(֒4�b�/���˿�g��+?��Q�v0�>-�vg��|9z���'���&5*D7�0    Im��uZ�[R}�^��Y��������k��
oaXzO�;K�n���y�,��yi�s�Ve-#L��^�c�1�Z~8�P̒�,��i�VU�(ԟ��J`��s�K��~PZ�;���5q"9
��+;����y�#�䛻n��Ĺ�3��V
��ί��5�H#�����6�;�h��c�-d�"��?iiތW쥓�Ȓ�.�`�<�.�<Rn��W�Ń�E[���+�����������H*����4�,�tD���6m�`'8���v�����j}�>M�	X$-�A*�(�VH������9��Q�P$�U��9Cd��e9�h��*oS�)�Ҍt���������B8�4�l F3�\qCި �/�A���ﶊ�H�	�b������������d���`]ڙ?[�L��,�6� N����ڧ��p�p!�/P;��g=�V��~ߜ��� 5����*M�W�ݍ�&Ex����0_nC�x�'�%l���૎��8#�S��wfR��������f)�,����Ճ=x8y<�;�^�����s
y���'���&rؤ5\h}X��t��F%XR�1��#�x��t�[kA��ٴ��ße�M(���+�Q�������x���}�<���J��z���0w ���V2I�<���ኈJ��uﴃ�p�2aH?��%iWL��������'�����Rm��m�̶hg_�u��WE����V�A�C��?S8�m����g�w�t&�&���cs��cm�2�d��~D�q�#c���6�ӹwV���]ì _2�g�����y)@z��O�F�yAص��\��e��@�]�
q��ti���4�����Jm��Ű�9F�	ɞI{�rΔdK�Rr�������`���͇��%0�EN{r��)k��bo����c:
�E#笰�	�Cᖈ�u�6]�6\��w�[hm3+x%���, �'QRGeN)uP.�g@s�+{p0�";��D��\��5���
����M��i�L��z6eu�i����]3�r�.��mߟ�a.{� KDo>~��s�so�J#T����['�^��ʚ�rԶJf�!r��4�����s�&>�R�P�b�p��f�V�8����m'�M��!���yh�޽+�0S2B'���t��O�J� �7
gٗ x���J��Rh�p��������h�'���t�pp:Z ��%%v�f�q�_H��c�r*��X�͋�����0�vI@��΅�w���� ò5�JIB�F��5M_�3|�[\X�,,�eB�0ް�q�<������׮�
�ը������2bQ��@�x��fQ���E[�,ԣ��⩖��vxq�=]\ �� :�����?�_ޗb�=���`�<�o���6�X�Y8,�N��-�b�Ԑ�r�Cm���x��$�l��TZg���}�|�.�Xd���=oϊ��^�g��$愶�����������%�FE����0�6���@�G��g3�1�������g����Ӈ2q���z�F�X�.	�{+�A)!��N��1��L)�[����f�ʚ�����Ƅ���Tm����D����y0d��:Q��g�W�v�>v�1wv����K3��?ZobP�����}1x�&��Ô�ǜ_�!��X$��#�f�H�����'�TyCԷQͼ����6W<{'���G׸��S
R⍺�� O���l���γ �TA[�ɩ|P��w����t[�&�W�K�y��٫�� �M�[ ��I!�\�Kׄ�P:�t�Q@��ͬ%�y����'}����~�Û��U;���o~ߘ���#��3B>�/��b���+���!V��;�1S��J�1y�>�[��1��
��'��C@4�n��O�?��^(]�]����tǑ��ѠW��p��5{i�������~Y���qG��ę�Ͳ8Mt}jڪ�Q�d{���ㆺ��E�Tp��n��I�ֽ�
����Hc�����~���3N�+^냩WQ�;W��6���h��b�	�c8�E=W�`��v��e��Em�>�I*u,��п'�x�Ɯ�����cHa�|�Rg���nk�KC>C}�#���B�P�2���ȟH�av�;�Aґ�����m�d�C����L�@p�}mȏ�����ۭ�uSz��N�P&F?�c�� nI��!b���Rx�ua�{մw3O����5���}�i�Ʊ�����%�5dT�%�+�zf������m���6�1ɭM��y�8�T9l]�	A�VF�(0�`�����e�r�o?���fZ�qq��jL+��Z�6'�	�����~*����c�\�%O�V�*>>z'�q=~�|�^�rKG�\~(�b6��jGo|S'�9O����o�y������t����̔ZL�74;Z�%�]��+,�]N5Q^Mơ�\�GT�kx��-2M��]��7�e�.#�m���9�2H�>,�.ۏ��.˚t�4T��S��P]"QY���D8���5	܋�^vаp����YEC�#�G��e�f;�1c���zFO�66��Q�P�}l4�$��}���E�@$<!�uX;k�8v�^ ��P���N͓��oJ���#l~�L�P�nfw?dj��
�O��kV,�{$;W�oVic������Wz!���&-#Pl��iۀ��[�����V�^�<��d@�.��w��+�	TĎ}#m�'�A6�$è/�~�/ay�?ʣ��'�$�FF�ɇ���Χ3�0n°�Z��$�q��]�;oj�vO
|�d�=\Ŭd���v�~����*��e4�S��ђ�����p/�|d���m�tm�t���A�Z�v���d��n�4/� �NiVD��嬭C���[�QǷ�9�o���Z�D�=��%��>��I��rp|�6�EET���t����F��2f�ޞJ�ߝ3}TS��s�mf*�aF˭������|�� B�Ö�&:�Lfq��D ��:y�)�ሲ@�������!������:�ԇV� ����l�t��@>8����d��\��J��`u�/ӻ-����Z��.DRkôd��,��"Y��hxk�����E��n"�'�� +�r�!���"ـub��h��͏t��.�D�m�6�X�o� X�j���_�S钛��iC�FA���u�.��s>i���Ɛ��1����_h�L��R8��p��q �@��yC�cD�[r�ź���7KE�G�ּ&���35y����������j��`�ڤ�֐�I�A�D�5�>���8~[H2�o�S!���r���y"k���qMw��嚻�ԂH�E���n�k�w<�>�!}Fvx{�/����_�w�����	(h<ݢ�|�-2�A���%Eb�1)�%?��J����dU����sy1r�*#�/߮2�o���d�����2/�p�����:��U`�1f�6N�	sל��:�B!�U0wx
�&w=<�_b�0����<Z���g��1�HZ��h�b9�f�IU܏�O_b<(��C|$&�.���D���T�O"BCb�+F��ύ����v�omS^v���ɿ�S���Z��Kr�U�rهݦ}�Z����7�����5
޼�>/M>����P����5O)���f��Cca�w�肞z����|.k�O��h	\�!�|t�q���h�^��y8s�O;m���y��(���V��&���ӗ'��1��y1�`�����������\����I��!4�T-�)/���b�8~�d�:m���'��܄��8�D��f��W�4���o|&K�����}L"���"O_QD��Q�"�{�pU���3(�_�3q�$`�߸���>2u�Ep�Wc*�o)6�xs�T���z���i�|�i�iB������Ŀ���̓�Jcԏ5�="�ݼ�P�ZFP"趦a��ͧ�'�<��x8w��ݯ >zvi���<+vZZ]�xj�As8�i-�����8�ݍf�G��Y���IQ=^.wC5�	]ϡ��dԬ�Wں���{����M�Pɯ    �D�ɨ�H���t��^.��7����+�h��W�7{�p~���)�<o������W���?e���X�)î-��q����U9U��%y�N��?-�:%�v!Ǽ��֛R|��R@����G���$���k�K}if-`��O&�f���L\66g��ޭ��0{"ۙ4���_p.���ءۅ�|��\�T*�,8�$���9E���"�����R��;�,_/���|UnI�^�ɻ����
�{�G�Mz� �*Y-}O�J\X��]�׃of�Ȣ!L��&�Ƹa��d��J���k�}�������D��ў��F׷MO�s_�����iW�*����Z�|:�A�6Ҝ\Yĳ�}*	y�B@��o/�6���U6�N$�W�:�o�j1�#!o��h�;uZ�
R{���ALغ�8O��]5�]R�;���N�bG^��gJ�4ȸ���˘�CL���&ث�)>\&�O�a����G�W�ˁ�'$��e݂���M�����b�[���ēѢM��G8>?�I8��y��8:��#�g����A�|����.�nll�#ي1���,�!8	��f�Yq�����~��魉^���F�!����nY?�rz��r"��5;����H�+����r����Zb0��Alٯ&����J�n�F����ew�+~�3$�}�SѿK�Q��{����9���@�QO�7�{i��7�ߧ�f�����~ F�M�W�q��шJT|,35<H�W��U�1j%��Y�F8=~�Y���zz��G����	���[��xs�陛x�U�d��#q� ��:,�b��]������I�jҩ��m��nT?�����ׁN�'�|�?���-�����b�k�z�#Mp�s���
�z��Si�R/�����{Fق{���ˢ��eji���KROV�9� ��k����|ڥ�}k��[z?#){h�9s�H�m6��IZ��݃���5�5��/�L���,Z	O^l'�k3�3��|���N���r��I�7��֕��5t#p*�M���iOX��ޠ�7�R[����?��>��Ba.R�ż�%`y�6�Ds_��	����E*��T/X���vz��ө�ѥ�N���X]q�>� �~}Z���j��V�Xd6��͌@6��.�0dg�}?�L2��0h	� ��)5�;Pz�tq�+!~F�l���Wd��?[t%��j��r�;׆��e��.��@Z��B��?�ꢛBm�$eԖ�׻�S�T�~P��aK'#S�$��x|��a]�Y^s������L㺵�3I���O�����x}���H���y�(�������ƫ�F"�)D�'$��X�z�gR����|Z��&��:j��g	7n�K��l⟰彋�a�P-n�juӖN7�.�Ú�ݰv7��5`Ef�o������C buX-��"
z�A��u�������Γ�10�-��C�,���E;d|2�f@_U�Eiպ��h�8�T���o�j�L?X;9|؏?�ON�H��~����<�<GGt3
 `�����[;r ��(�	��������;��z�6p��#f�id�UUZ��1�}
'�jA�h������&C�(]_��+�EU�{ןue;pr �C���e�R�=��urvf;�]@��[��;����l�P���2X~*�?�勇[�+���b[���X��.���XW���dPF��dM�o�`���I�����ca����*Ycm�C8�hԮx��kSo�p7�ECUV�Vn����W�(�N���i�z*3k�0�ﹱ��]Lw����˭�������pB
�<���l�VK<H?hx�?5j����� �F�EOg[,e��_ ��@Y�q�P��p{��<�F[�1Cl|�
o�����؂2þB���4�(�Ū���B .��T�$:�kg����K߮m^&i��Y>nWm.������p�BP�kz+[g��Ӡ�����jE MP��o%�]� Y$���H��;z��]/�	�2ܚ�&�g>1nv�	ʜ>c?=H��n`�m<�TScю�U	�?�ל�ݥоb��_!W�Ej��t�o+�^�\1��f�C4��s(XTD`Q�4c�հ!��#�G�$����Kζ�RaNm����"x�4H�W����~Q/�l�د�}W	9 �wV���U��X��
�!$�v����m�t�����e�I�&��7�D���#X�
���cRt<�iM�+Zg'F|(\j#?���y c�C��D����uJ��7�h��H[�<�զ?l��W��kG��:딯�P2dU�dM�}�< �:�FwB�Cg�Fb�4Æp����mSL��5X�X�/�e��6(IZ��E!�L��5���ɾr�,�۞�ܺ�(C�O}ZR�`c�n�ˋ=l�p|A1_x��e�pA�j�]L�f��&JI�py��V����ړ���v�͛�j1�VG�2��O.�X��f�}���/���oqTb-lی��K��X���}�:��_j����C�p�J������T|��Q��j��X�7�F�:2A?4���:{�!M�=�!)�d�@*����2�s�j��V�p�J�W�,����e��"�h�yǖ'�o�e�8��x+��G��Ђ7b��G�z�Op7��_�jz����.R�{��%N��w���b���!~���@��H�f0θU<����%W{�}�0�AP���S2[�Vx�Fɺ�/Z=��v?�)T�ck��
��L��`O"���Ot�M�=���	ԯ���L;N#�C<�g�&�q��%8[��;}:�Ex���DE-ƽ�V�ۆTo�OW�l��%�+�-�kt09�?f���8q�1R�ө��}eSg>g�ٍ�F�Ĳ%*p,��0g:R��>�%�g�_R[�Dg��a���{S�ň������$�,��թX����WCa�I�f$
�l�.�߅+Z��c;8͹2�jI#���5�!ՉzcU��-�O�F�S� 9�b��/`#ޅ�J(��%��#�M� �LơM��:��>VH�ޜ1�POZK���,J'1R}�p�tA"�?X:Ą��Gt�lSlw3�Ǿy�.�Q<�H䗙OE�Ff�<�E[��X+��<|���݄p�݆��?�(���۩|]ǘg����0J݇+��'�~�(CL�����e����i!q���&V9��iϔ}��'��YdU}��Fr�=���v!�/����-�pM����ޙ���HV�7��� ���'
o�e�ˀ�*-,���%ax��؏¨5�ܦ�-��n��:�*t����_�c[�5V�L���W������"e�G$vZ��'o��h%��{�~v�.�m�=ϯ�O���N��!�S�d�C����I<:Q�y���7,��.$�N�񠄀�k�����S/��S`Y��e�ٗ����%���J%k/����(�
v�uTn�]e�lG_��όIYs'u��{K׈@�W��T��[�$-����i�KA���Rh9��m�v�ѐFy���z�L[�j���1���x����H �窼�Z�M=��[�=�����$ܢ�1�5��{�4��]�gaP�N���5�I§C���W�����:�6%�j֗�[o��7b���a ��ҏ�fHheO��6Y���l��e����!o���4����p����/�n�|�p#a�mr��)��b�t6���I�gJ�z���+Mʅp�\>
?��|\����6�^��Q��d�<�l�-�gf���dI~�H\� �Qȃ"��
����:?�[&��놹��j3��w4S�h�>��[���ampQ��X�\�g/L�@���Î�$ebV�C/Ժrq�5�7���xǙ_^��J䶆�ɝ�b���	��s�*Z���t>�@(��r{�Ϩ�nv]�c��y�a�v2�kf	x��A��= A��1P��&�"�g��Y�����u��2�B�2r��~D�)T�0����rTB�W�ۼ��9NWKd����XQ�`kE������HC���T�=R�    &�J�x���>瓭��L�*7���d`�3��[��Q���T�{"����p���r���ș�"��*�4�.�)���L��skH�m��=���*�AM^��^UM��_[QT	x�g[+��f��F���A�B���Bp��Ζjy��i_9���ٽ�K�=B}�d��t&=�(wm�! E�f�+Ҹ��M������`��{���!h=���m��W�O.�&l�?�y4v+�N�J�Q=�Q�H+��	6�"�#��QQ���rr33�.X��g���K��j��_��"���%�k� ���5����@�PE0E�����tlr��[#��J8��T��+g���$��Dߞ8�:"8��v�����1�%>�ɸ�I1��VR|0Q��T�v�)�Oà��D��j$r^��h0��Q�<~����%���4���g5��`����U�kS��1?����y,9;>3�x���#K�O]��KS��K�G��v��(�!�J�%��p\��+��y��kIn��'���}&�;9,m����茽�쩬H�爖�(efN��t���Oy��о8�?�c6nv�	���P��jr� �S捁|�l�l�м^��Vx���|߂\�T��
rbrI�V�h��t�\nȱpG ��(H��QZ,hf�dA��=�.����8�M��یc���{�I2$o�Q^�ރ2��������Kę�W؀!}ک����I)��y�	<��7pǃ�@�ޖ��h�AR1+���w���2��{�l&�O� �y��c�x�r�7#�;Cs�4I��\0V���'ԗ��C��(�H����{��"x��5T}d�\�.Ҿչ���4�s%�*��q}'�������&�?=�	��|�_�|��{i#j�T�]�"�TH��FIY���_������@~s{y�ݖ��=uJ:����\�MW`��]?�k�"	���'��9�S�3X������P��GLI��UV`J��<??|�!B	VBtİ�ހky�@�MuT/�s�l�OԿ��:�{�G��#u|v�@�bld�`k	��]�F}[�@q���-<��'�G�:E�A�2;.Ұ�my#��r��#\���>}u���ƅ�G
Hُ�ث���xN��~���ȬA�l�N&���|�3���	8ʩ\�X9G?Al��)��M��0�خ�z�Q�tBU���L^V�K���^�ΧT�,��v�o�a��q�I�4H����/%�'������8]xr�
�<�ன�>�a�n;X�>>>���a��1��'F<����!ט$񳙹 �S� Br?�Ar̈��]�K�>��E!�`��U��O���?Y������K{����D:�b�Úfp���y�������S|�»��2鶵�}f��:��Xy�fQ��M�5�?�b�"7ٵP_�1����/Mя�<YφJ���G��8��1S~n|�>Kn��� �# ��y)!t��J}ʄ��4�hc��E׾~�[�Po>�<q��p��~��e��W����L��{5�:�����@P��|Wzq����i�rZWLؙb���f��t�5�]�Z�d_�3+�����$	���j�a���� 4��%��c ���ڹTO��ްD��TIu��Y��3�h*�� 	��}]��R�]B��!�h$U:?� �ι0�q4��/A�U�;fY���ڍ�u�U;髑�9��I����,I/��j�.�U/�Vu~Nƍࠡp����
m�؜��^}
r9��Ld6�b�H:�-g�(
?܆H���e��;O���{���:g����XV1n�A�~��a.�ma��w.3�r}�dѤ;�ƨ�����G�8�U#M�)\�k|UH��Xft�m���:����"R�  t�N�Q�����Ap]Sk�������xl_y��9S����+l܆x6�by�i�fW�����m��ɝO� �>�g���E������V��Ē/>���6(e���P)-��{@:KW�޷�u�>��I�������-��Y���g��ǉ�^�S`؟Et���+T��AZ�fDoã3lw�� �&�=~7ɯ�?�(�[s��0_�F>�F�Η�U/}=L�c�A��3vdo�T&(����\"��]��� �Y��|E��ƣ�����gn�K��OCn����8�����3Q�~K����k��q���-��u����ᇹ�of�:*����
/��I/T�$pk�J#��q�uh��Gߖ;�Hm�qqQtv�F�2؉hZG�tOY/�`G�4��R���Z�|�n���+�%�h7u��Ś���/�x���Ji% ���̈́��@�/�ٯ�Dx����q?�hD_[�P�8C�EK��'�,ӚKЎpJ��q�g���>,Ï�ӯ��'���!�e�K������r{W�kN����t�hE��B�ׄ.R�q�d���=
��vа�<눳�g̕_/�?���n�7v���|lO��.�r�<ѯ_`��g��E�h �'��˅V��T�Z`�`$$7�Z�E��O^�|f�U��{��%l��AK���IO5�:�olv�b����<�A�)*��y�d�`��3���߀Dm҅�"�˯�d��fq^�ź�t�,�>2B�^�E�vK �t�[t/C�èg,��4$�do|iTS��B��BI^6T���+}㕠�9���Dz������޵ض��b��g$O�wmD�'лP�ȇ��P.`�ȗ'ֽ^�	��ˬְt�ʫ�LK�ҭ�j7y�zmg7H'2��<8��r�N?�q�/���*~��b��G�!ێ�uX|#ｩ�@xF�V�FM�o�P�����l��j�f�^���} ÈWVE(��)\�̪��7N5�b�'�P�@Q�}��z�p��]�lі�@:�E�����O1P��]�^|"���零i���7	��:V&���쪏�no�X��/��O[����cd:(���V����2�}�@'s+�S=K*9��Y�_�m�f ���P1#~OT�Ga��VZ�a�zC5�ekv������1��zZ�߉8{U=�:���S(%T��0T��r^�*��+�i��u��r�����@ݐ �a���M�ž�OrU��M��g�q��|�O��(�asp}y
�7J��9^hp:�����o�J5u���7qIH���3��i�F�b���}��I81\��Q�p�1���Y�d�f=��}N�M�
�ni⭙�5���Ҝ�|��k��ei}+�����gn�B�m��U����J��N�>qç,A>JX{��+���Ls�@~� �ĂImA@~Ɗ@.*zvuE��[YE3CǄ)^a�Y��̪��,t{�As~��,��~�m�M���l�m0]~+>�Pb��~j�B�L>Eq3�O�R�֫8�q�D��*m�4�V�6Y�8ԃ#ki�ʛyp{I�����/���n�x���I�H��`��C�rS�#�/b|}N�e$[١ɗԜ-���.��63��:��CN�U�h2@�q�~�ΐ��o�$0�RZ �}�#c�p8)�)��	)Z�?�{W܊,1_�е��i����X&���}/W��ᙶ/~��+1?eu������G�1		��g\�88��w^H>��L���#.s���jxgbZb`�LŹ=�+�<����	}R�<��M;�� \c�r��]� �Z%��Ug���7�rR�H��#��	���1`�o )��,�uM����f���0�&F��~�,Q���Jg��/l��E��\w�Q�� xAV���4\㸌*˵���V�:	ɤ��4�,GE�j@Ұ{��˞C��;*M��V��I�����]�M^����gXM���]؛^ܚ.�H��oom��<SK.�G(���KRd��(¤o��[�*l��[b�3;'l�U1j���x7|��عQ�E�:��Eb|=���s6�h:��')�_���c?-%}&���u���DoA����n!SO�a%u3�~�6rk��r�:�#���x�!]�n�fz����N���.'b�������{]D�����m�+!!�$<����v��g�8:    s�	��G4H�Aʷ��=��N�����m`-a�vQJ����)�ɷ�b;%��Bls�V6��W��_1�M�'��2���VU�l���x0�҄OK�g��5�h���l����_de_qbæ��{��-H>)� ���,�1�a���A(�8w�c�9`��0/gi�R\�	�ִѓ��/>~��;�(+�0�;�o�-o��0���V�z����u	{5�5!/����h�Z���M�P��qoz��	X���O� V3��a�}�)5�����|�y����w)� ��Z2�z��PB�F�M����C�1g���0�͆�E@B/B-��	
�ĿT����S����_���;�d�/�2I� o�6�_P��NJN��j5��V�org� �8�+�J���􋈲͎i����˪)�B�DNm!��[O��I�ɪR�J����P.'*AQ
;��M)��o�����D�ŀ�/D�f�h�W�O�T��cŬXp͹�5����LK��Z+5SȊ��?lzV8%L�`��I�R�'�o��=8C@�h���0B)0?�!��:$n���4�,�R�+��3�Z�kS�G>;w�P�����&XQg�q�Y�iz���e���)hk�,��y��V��It��>i����-�e����W-�ς�T7RT��OT���ކ��ȕכ4���rL��(��ωۿ�}x���<�]Ku�u��h3N������ڨ�ǿRv㟑�	|��V%�$���Ӆlu]IA�*���}�fs�~�TTvc]�Q��NQ#d=R�u����`���Vd�Ԍ�����a&�z�����!�d������h�E�)##�ֵ��(���	�G��ZC ��y�3q��A<[��3i�"�naZ �7��y��+��z-�j}�['g�������.&����� w���ɏ8����>��9e5�#�~�����!D�]��Ji���U���6R����\%a�A��9���*�d�E6k��.�ö�(���˕e>�p�릛��'|���`����^]�e�쉬��.Y�k	�k�F�^�it˫<f]��Q���v���e���IƗuw����hsDx�XK����zGc?�sL���$�E�5���:Bm�OD���'wr\��h�(Pt�p~�`���5��a�Wۃ^%هr��\d�XWk���]g���;�Q���/gDq3_�8�@D��8B�Y�Pq#���$5����n����1hJ��,�TdUd�YV8u(����7?�]A�J��
F�q�I�A�
׎o� �&�E�%�k�}vj{𜚶K���$gJ��B*��ĥy�W;bBZ�;��N�"���O~G2{���U�M����U���M2�uv�D�~ޢwe^�a�W�2Z�!�}n�ٱ�T�d��lI��mF�-��ᾅ�3�>�R��lo;�<�k}C�fɈ�=F��q[�*3���o�cM� �+8��C�����KJs#�H�Rj�&߿��h�
U{�q�2 ���U�މ���Gwb�}y�Xo�\��(�Vf�;���-z��3�������"�Yc�,�b[#zq=�j�!t�F�\e���E��/���x=�3MDpB�ran����.��j��HfsS���0�~�5��GZ>��R�/oI�8������I���	�Wt�-Zޅ�A���-��B��ڽ�'Ŏ��Xs�nN�vk�ŘU�~�f��D4%����j�X�a�3E{Z�Q��H��퇩�	�~|�t�($����v����#^�"䶄-ٺp�P?��~����$�i�ݧ�������'A�Wãs��a[圓HVW���PtN���d�\S!�x���ڦ3������ꆙ��v��`b &ֶ/յ�G�OCY�(v7�s��ⱇM��Ͼ��::P�qi	�tx^o��t\�Ps��kP*��~$�SX}�r@�&��q���e�ٶB�|Jl�s���ڨJ��i�pr�GV��;�R�Zt3��IWl���A3?����B��'"����@�^guL����l͓�p�j��)�_�g���ِ�o�8)|��p�a�_|E�K��E�ԌvL�
�"��U��W$?S��ήFo��x��i%����I0ʗ~.] �J��m���oP����"�/ke�c��֡ˁQC�)CY����A��_l�2QN#��b��\����)�yu��Q��ĉ@���	�S��L�6�8��\Zʃ{�2���G��Ɛ��?����Nv���%��YJ�����W�8~�Fzz
��uq
Z��K:h䏛��}YY�O1�.ן�Z�k��2}��Y��G�;�I³V�/5�U��.R�БT-�n��
��lQ%?�C���,�����ލY�xK�o#v���� �+K���3�&��S��z4��	�v)ғV�]��$��"3v���3��w�XL��eK
���m��8Ё5����F���tHX�Y��~������P��Mן����W[�W��y��,׏��Vw�G.�T�D�֣'�!n%"�E�ov �)�N{Ο��T�o�J(J�ň�'�x|�wGR����'�� ���+�K��+z�8�cK!���h$������?�:"�������t���<�p��L�!R��^�M�^$��^��V�3se�O�ƃ=���Ґ��g�Z-�u<��^���yѴN�A���vO�\L&�I\�;p������7���"���l8�����F[���`��T)�i�juU�,�'D
��Uo��Dɏ#��� ���=�sį�!��ֿ��g jI�"y�Ё�l��$����(���靹��?ɘo�UD�U�d�/�L�MW;H�[n���8������(N������gu,ɰ�����B����M`P��~��o��`Q�93�r `���@�J
�%D|���ŊU6u9�l�A#�ǯ�ԋi3ǾWiy��DF/�k�8�/�1P�qQ�ڰ�a����>H]��as��]���H���,"'�,��k���(�f_��w�	����+5�^�{^;�G�۫�_BJl����FG��\�hD�8�|���W�{F��ks��B�c���ᮀ�J!];�!ǣjET�k_s+�^k5f5$�����;��&n,��/M�2���S�9�d�䥑9�X�F�fĀ�|����f�fl"��{���J4o��*�_{Ǧ$����:�{�.M�+�'��/V���t�ym�4��B�O)V\�o��[=]F�a&��3�|/Vf/�e�z$�8�[�ɚ0�j���%����;��:�>�K��A�^�X�rV��Mȋ!;���- Q��<�
f^�4�� ��+V#��v��ٷ�O����=�W>V�w|�������ޔ�����V����������&��7L����LW"��Í�[�Bq��&�F�|��=<���'�<q.8��S�<�1�r�<ˏ)R�Ÿ~^�(�Y�'�e�D���|��~tMkz�G��sͬ���ƫ!��.T�pyaO>r�s�M�����EFw��c3�C�0�§"�Y�gȪ���^��/�-�otA�Ϛ}�T���CNGqG|MU��rr�qu������U��{�ֳ��kM8Z%�YtdG�j�n8J�)�L�R�~�Šv�E*�Vh's�d�!p��o1���C-���>����Z]���w��ԽԭV � ��l��/U��l��Fv�ĝ�( g}�Zɋ0d ��c�*TF[#����"t���4�×Vr2�jk��߹X�M���*�ٴ��z"��PbGR�,�r����H��Q=�@�vѾyE��M'A���^T�5s��>Hy�Fr����l���P%kV7#�=[��ʁ �'9}��ud77�,F�w�B���b�~$B8Pe�m�gr�vM)��`m⣁d i��d����B��K��ɦs]�x��2]4ɂ;ڟ�M�Ul��!����^N����֗q7'��Qa���=��^�[���2��	DТjhF+��47T�k���[����z2��Y7��[t:T�G߿k��PJ��<Zډt��E�ѐ`�4�V��;x���    ��m�FԆ�����vip��a���z�G�-R/��9����`30�>=�Y�w�ܒ �jJgI:�
~r8�����@q�$c�Fk�x@� �t�����3)G�|GM�Z��ڭ��q'��� S�
���wbN���_�릩|H�hO|ݐv=ٰ_@�٥��Z��s1U�*?����z㷵ظ+>�5`�Ԓ�����ߒ
ta�������5ݟ�Z�������~S�q?%vW�O����3wo��B4B:�sL��B�m
��O/GJi�Q	�qvRI@'��h���1p�x%�1pfC7+*�n����2ʖ࿷�0�F_�
=�쟈G�zU�Q��� ���$_������j��~y���橇P.G�/�j�i!3��'�h��b8U�/e�G)_"�N�w�Y�,cg����+N�?-��3�	H83����T>f,�S}	�n8N��Cc����dyi6\�Ruc:�Ź��	�'���.���:1�A�.7��9WS��D�*A��3;�`Dt(Ʌ>����<�����" x�'���a�M�vB�|	:<���6��q ;���vQ~$��s7�4���2rcZrl՘ЄL)ڥ�������E6�<��_�Z�G)��O�ڏ�O��Oܨ����b4�g�p�&��t��Ai����M���-��%TD��S�;�1�ua�*g	���)����v'�h�3���
����ı��Ke��C���=�_O����2��/�Br�v�1��I�%�ӌ��O�|E��"w���n���o��,�A6G�(s1�?,�?���M�.��2�El0�ł7����y��u��w���SY�yA�LW;�}f2�y�31���Ԭ��.��v�3u�m.ڦ�ޝCu�@����G1o�.���:��fK�&�2:6����ѧfP���Jr�7�QV��Y�w�qr��I�	���@)�8�e�#V��������`
Z���BoʑhVkŐd��gBe�"�i0SNfe�SuCv�.��И�� ӿ����2�ƕGD�~,߇�ڰ;�!����{vRA�e�@�u��o"��dn�y�j�H<��@[i_^9c_�L~�ȳ���'z��(��=�kS�?B�En)��4�uTV�;�EV0�EO����^Dg���>�F�[��(�K@�:k��xo�&
����b�S�O��?�P.<�����cxI�E�M%^�Q/�����H#����je�3�J��/ȭ�^Jz��*���F���T����f�sߌ�+|��#|4��GO�������Ϗc��d�/���Gp��@�W�c-?N��@- U�5�;B��3�_�I6�c�0��|:���;��Ϧ-�uS��i?m�h+(���2�^*3u�o�� ֫���$`��\���M<B=���(����Z�A@�G����/=������}�۟���q	�_Ϗ����=!�����+�Մ��uw�3=��ԩ��K��iI���O]�0�O�!����'��}�+���������d�"���{!+խ|������i����R��D��1)����s�:��l�Z�L�,�lY�^�M�������0�pPA�:ˑP���D>ۧG� ���
�!`��Lc�;7�V1ײ�
�LJ^I�z���?��@>h�P���jD��#f���\��k���*�B~�1�p�͝�_�}�8�Y�G�Տ�F�j���S�!8�A����>t�E�l�����d�����Pr�I�'!��Q�ǁ��QHE���oA0�5�.;�v\�.���l����F�7�[0��l��,(?6L(�-@���]Z�Jy��U�$��0K���Ԣ�]��L�G$��xOK���xFP��p�����s3^*��D氟��cxt��wYE�^s��LěW䉫�	��k�����/C�*Ӽ�\pJEK.�,؎�2A����һ+��x��Ů��-R�W�6GP>D�w(����^�$�F����Bf4�ī���"+���MC���Еޘ��O��Uw:Cz 7������DF}�ۅɓ5�����Udc��Cza��*X������~�[�V�W�ŋ�_>�WC�2
��U'�r��%���@��3�U�	Uq��RE�?�}O�3͚���׈/����&�ɞ~�%/�S����=�m����IJ�XL�\�����?u4U���v�-<�1�Ä��\	k�h����1�,�^���N�2?v�w&�kI��B����漙�{�?I�_�C��ʧ%����U߾����9�؎�É�a��d��ߢw]��}zk�"��o�{�M�Y����:��"�B� 5��M~I���3m���<�gZ�����LNUG���������d>	�J�#�9M�Di���ڸ,a�gl�V��-\�m{��>��g_EĀ8n��xn�lN���*Xm|b<��H@����zvv�Q��&���E����6��B �2����&s���A%^"���(�TS/��6\���<I8�x�T̆��UF�-��o)�H�1��I:臀�2�@����( |)[c�f5��O���4�7Q���#��_v&�OXo��͎�L�zz��w��F��o��6����i�������v�+�p��f�&c��3�&ڀ=<x��j|C��|zg�:5��ZS<q�i��A�˯��ükr��`ȥ��Jz��Ζ5?�?
f� �8,E�:s~�\׻��.��M,�d�y�gj�ʏ���6m�3�xY����z�ϣ$�ܔ®�̑�O���C�����I��
}������B*�,ON�R&�C`��CQ�-g�{q�����RNBZ��E4Uё�8��5h��`�}�]��tik�<�gv)'ߒ����r���>��h�ڭ�D�þ�k�����ȳݔ�� ���l���Wd�@Mv6{4$��7fE���qwvz����QF�Yt�Z1��N���
�_҂�$)Ob!1q��9�/Q�BE�o��}�B:�)���J��	�5����l;�L?@k[��>�����筄'���`����"°"
��l~=F�-��f�_��U���!���y0�~5�����57	'��ٿ�_~X��TQ<��wP"��4\fk�ΐU͟ �埆W���P���b	:s�͂�Y�>iq9���'����� �L�(4�ѳ4:^0
���7jr���D��}�|�%jmߋ�Q:K}ĢZ���\z�d�^�.��#���\� ��Ĕ���IX�mr�tv��>O�E�B�t��j<~��	s�Fv0���ԉ)���´!0Mo�Ѥ&A��˓]!�t�w��o>���FN3�2Z�|� ��+O2�92Q�7�z�B� �)m]uW�$��}ĝ�_������m��Z��L���	��Ʃa%3�oA�x�v� >'�L2��s/�� x�����Oei<k�w�a2��&���4Zb{=�yVh6�Wv���� �,�E2L�W ?{��V�B���'��xP�uWF�1s�Jhz�*�s*�� ��|U�(0�@
QH�=4T�z8 oR�8��� �b	�R�N�D0��Mh�p^GU���w)
�aDv���{ٮ9��ˎ�t�u,�*�I|23m�N�d��vyJt��'Ȳ�&��靈��"��'�i���l�P�ڶx�Kӷ�}XӖ�F�AcvE�.lRS?q�.^���?�ڌG��`blb�J�0���Hh�y��}^�����f���E��zX�Mo~	.�hDA1�c��'���:��!G^���jX�j"!���S*���,q�y)�d
���	K�u�ᖷ�_6rm��	$d�mD�����;o�G*��ɳ�r����%w�zm�Wg��C�����8��I��!+�MY�
�pk�M��^_cn@OH.�z�{~�t=p�"���0�0��_�˙]���@�y	:�w#�����L�I�:z��NM',���~�R���5�kԯ�(�O#��M$N�Q�8m�T�����3��z�['��[�1�� '<]��z���;    ���{�7����dq�ɦ�k�x܌p�~Qs�ely�7��q�p�o��Q.�F���fl6P,f"T�Cv��t��lc*n��F�����$	^�rQ��9\H����{r��d�m�9IY(��he\e8����(�f�z!�M^L��t�7��8������yE�՜��g^�3/Cz��cr>�� c�������3N�oq,:�X0R���_�?(Es"h݅*�N�fBwW^+�"䧂���r$Z�ZH��E�{q;P?�ْSU�A�-E=�0��KH�'���l|}ѕ�/���:�[�$(^��N�:�s���a�x���q����<�N��|[�LY�{�䐮�D���=���x*��2#�"%�ɀj���@ˮ����'&x>h�Õ��sD��ߙ��nR�h^�����=ear}�6�h�$@�k��8��7!�&�S���$����|�S��M�tت��B��,���p++�����'�hK�2���FŹU��#B��
4�}�/��C��Fj�~q�������c�y������OT@�(�$*�As.]G=u�Ou2����_�6rGl�%�Ĝ��M��4��l��H��,,�a�I�J\�U� 2�%b^,D
%�]E�8�d��]3]/�A��7h7���1�:׷;ñ�ǉ��͠����͘������� �F@**��;V�k�/:�C��-��|�5q}ECg����m*qsA0�LS�L�p�Å<���/Dd�,~Rs�ok1�V�[��ሉ+q�Et#ן��? ׈�O�hE���@\d����]���s���:�������dވ��mZ~�W?�Dk�0)k�g�\�l��6�ց����윩i0[�g���e`���tO~��#�s`BC$R�B��) bR<�Q�\2$M��.�#,$0�X)�QU�1�mT)4��g;d71�{����]G���N�����@��s�U����)` ��yI顆y��`1.�c&`G�Ӊ�o5?��4���B����3���>�����>��w��j:c�@R>�����ї����8�@�\�|[��B�۔���(';�c���!�R/�����EBk��+��u%N��5��P-��:�X�h�(��6���t�j��k5�΅�`�	J���EA�2L�(D�x"�h��<��� �ے��z���"�q��*���@y��N�.;Y��jǻ����~n�F?6HOU�>�Q����	I�������[nNAm1���L�s �
~Tp�-�үh�� �qr����
P�ߞ|����� �$�l״1꽏������ٕU��4WKaZ��Rx؂-3ĮX�.�esu3,��G�G���%A]��I���e�3i��k�+�!�#H�+��?#��#TN�4P�>tq4Yu�ޅ�̤X^�O�;�=p�4�J��6<��·�~>��!�5
�z}&$�RX#��r�zV��v�C�Bؙ�����m@���˱����e��c[Φ�Q�ѯ�/�o�^-��"�;ōg��!��8f8�g꥗	k �׳6y�O��%ل��~B���+I�3�{�|KP��n����k����8��,��i�E���[�ǜ�Ǌ'���_K%��z5�<��8CtD%����(�ʂH���H�a���BjlgPI�� =���ͨ}��%d<֙ TTI���a�X)m�Q&�ay[�4�Pb$Q{Ve���%�fs��V�H��	�!��S�TY��J��^�G�����2 3n����nD"��g�b�����9M)G��-�9Cg���r'N���ߪ=y�f�������G>E���gduO��r��i�I#�Qc$��U��)�J�\��0������B��d���
=����{��6�&Nc'f��tq|y��~S4_�8�A�m7�X���2�^R������Y��1�]6n� ݕ�%� #��7�-����<a"��Sz�#���E2M�qE�w1�e�!v0/�ꨬ�D�r�;n���L����!�+	,D�����uf�r��m+�x��x���|�Ȱ`���"X�0����Bs~ssL����l�^�,휟 >lxۓ�;_*~�S��en�L(���
Ǎ�o�ǰ��u����h���A�f�?�Wԫ�)�J�ק?�(��%burЭ$�m3�j����in������|Sb���Fkf��R��I���{>��od~�Z������k7f���3�B����}I�->���4j̄�=o���e��%q��Ѥ�jk�����oĜ���৬��0#s��|��AyI��x�$�Q{%�P� 2���{\-Qs���p��m�x)dW����E;���Q�u��o$�y}U���������T��0!)W ���0p��#�,��U�0�@pw��!��_���^�d��:��;Pl^�px��~)b�/�j��_[��TW�Rp��;�Y\v[�͗|?ӳ� �x���3��@��:�4@tR
�ф�
\��Q��8Ep�÷U3%qv��Ϙ]�4���Nf��"�����w���Gg�Eb�z��PU啽@���1�Ȇ�?��0�G��D�`P���'�m�vZ.V� 5�Bqv��**\_��
_�%��Ǿ$�m�y?\�9�l]�q�h��PK����ˍ�=�a�0_���1	v��{�Y�Xs��5�U54����/���W�`k�I ���hX1_!o��X�u`������S�i��
��|����~쇣+ʍc�+MU�W�!ՙym����O�r��S;�o���pI���fjT;%�Ɗ�=�J���1!��W�K/J��n{w�ʢ�D^"������|}��g��K�H��B�K�q/�9�3���p V8�Im�� �C볪��_q"d`v���Z0�$ vv]W\���րtJz�\�t5=��?�e}�V�_��a�����"����^��k��a�	��){����ٮ2��9Q��q��3d1���F|�]�#�m1�H�m�r�x�;�<&97�<��&���z��cN��>u����;��C���-/��J"Ϫ?�������1�;�E�I�6a9��j�-fo��=�4���$��xj����D��8���vt�P�;8
h��\�E��2.��S������;����G-��v�y�U;ު7�������u�R���,����9��@�d㫒�{��,]~����:7E��	�5� �iR�e>dBP,_��xJϕQi	����T\J$Nr�\T��.mMGzz�´���@�nZe���RI��/?��/U^-^^�ؑ�}�;��e'{~�lX-��`���r?��9�U�u Z4}� �2*����S
��=
q����W��Zr(�mO���5�H��g�B*]�Y�N\w?;�P&(��*�D��P�;�{�~WD,�����$p��D*
�A6���X�Rγ�^U!���w��N��q�ׯ�᝭�����u �tq�P�~i�|aa�_�^�^�~��p%rN�+��Q������c�e���A����!�]��LKq�$>##��^?����\�T\1�O�E��V��.�qؗ�*Y��ֱS�K�c;�w.��S
MtZ�H��լ����3ڜ)��R�[�eD=�h�Ǫ��?MB�z���Q��3���
p�0Hb��C�!ކ���ng�7�*�H�KLo�w��Γ���"�]	����8���#�^A�߹��+���}�z%N�KA��u�c�>�E<�/���7�<B���|ù��M���	
ŵ���Zn��g4�ڲ�V1q:��8�2�<������n�qJ���$�<���~�.n�I��:7��-1�.:~޳�26��ٽ�,7�bs��L7.n�ΫP4�1�������E&E��3j�Z&��\�6.Q�5����ƾ��y�6Xx��v��5,�"�d�G����t�ʅ�XԌ̓]_=��Ԃp��&����a�����m���m��|�#����q,���lA�%����5y굏-�P�R@ha����|�0��    ��M9��g�E3T��6��Ӳ$�qEy�O�-������-(�Jw���}*!��w$os�=�n�d����_R�}۳U:��VaR�����C�E�RV�;UC��٢�2����gzA=\��?�q��P��9�ɮ�Q-~�^q�x-��Rq�}f8�fg#������C����N�ot�+��y�0����v(|%�S�ِ%?��`��쓴(�r/=��p?�S�d]�H�N3qj�4$�����]��D�?�E/�os��u�߇�ڄ8�kk�����kI,��1�p�����p�$��7���c�}��~�F��J�;�h�l��(�X���jʹ`g�,�d���9�d,x�eԉ���?�t?qE�2:�x��e�I�o�x�~I��	��LĎ �*`~�����g���կK��c�}�o�N�
I뉵7�<jwL{�{?���"h̬��NAU/<�l��x�?�_L̿�ݯ��ٕ���5��	7�R_��~*�+fz�3��5�����@�Ʒ�݊� #-�H�	A�/_�P�DC�����4�|�}+8p41��K�����>�ZR�JR���<��{��q�����Eq ��?���_$?^�d(�?*��%`��ᶹ�Iy麂;�T�Ԉ�ӏy�1X��%�p���U�s� �����(u��m������I��Y_�K��؞�;$u��z�D�ӧ��L��۶肭�q$F�mT7y��¨.�qT�gp�Hq�	����I�p���"v��o�[�{���T�4�ΏHؑ�H�;�L�#�z��}��#5'e3����Er�>�!������ǞE�F�7�P^�%ݾ�ªb$��|����: �:z��+���=7�� �%�����@�l�%�J
L;��$���I���+��r�qRrAw��B��Cc��g=�W��)��v�U�������"�{���.[Ğ��:�z��fp剥�R��u�`�E��0�PyxGC��O�=L���E]�2��x��	v�@�O飰���-����#⨺�����E[�L칞���w�Æ��f�[��'*2�M��>)
Oh��ON��w�yr�h�X�w�&�1��S�wU&a���L#�ڔ�E�H6����4{@�E���,ƿ�����/�m�K&�eԷ�
"�.�
j�ޱ)�z��mn��$��;�m5]ᩘ:�4[#Y��$���+^j=�JM��e`������{E�R���p	�a_bm�8����c74rm^;��|7�]���|C_�Ć:6h�.w�K��G&�p��K Y�#M������ۡ�N|��D�nӱ��	�'X���j��|U��'1F���}����m?���I��X�x�����9���M ;�9���n��H*/>�U��h�8�`�o����|>Շ�y;p�����֜x�7��m��T���>A4��&�)^����!�"��?�0}-� _m��@���5v�y�6$1P���'`t�LUuϘ~6�>ܒ�'�� J�(- Le����Y���,B=��zᲉ�4���'Ϩ��i�j�M��G$�f�J/4�;É������U�$<`�1������(�hM��@0�*e^�$4�w1�TH�	��625&%ʟ���痓C��{UAFsZR�K��(
�5���L��˙v�gE`����L��/�_���HW�V��?{�6I����D��_���-���}�$^�z����|=4:�XX�� f'���$ǥo�X�_=IZp�^
*�g�ߘ�ꍔ�soU|�Y��I�r^m���6���5;۲�(�TĆ(��As%K�N�=���F���w��{�&ƴr��g�M#���B/� �q51�E<�Q{�c{��ӠY���f&������V���5��0��lИ��5a�O*Xr�h?��n�f�D#�)J�W��D���M�2�|#�91�j�@nL=�0�fu�D\6"��h���a&�y�V�o��T�o�U�0��ޔT���������_�iA��W��Z_�t��8Yf?�{�.}������V��?��gΓ;D���s�{	MlE~��'F�QX�%�>�|��)EG�3���g%¯��NA�[00�ġ�0~P��.0��;�U(�{���fڢ��B8�F�۵��ż��k�q�B�E��t�}PY��밣&:y��$�P��B�"-��@�&���R��P�YA����v�휇x$S���(�5�*�ra���<��D��z���>�dcvvs�?����d�N��`YO����]�W-�������xJ��F
`���:�M�
��H����J�5.��b<��{|��괹<na��զ6Ȍ��A��h�}m��3u��{�N���I�W��cx�c�S�oZ�;4�∹�PQ���B�'��������o��Q��l�>G��-EP�ZuW9P�X��Ϯ�������H��r*4g��@z.��jh���uJ�q�J�5�f*��'&b\�����cf�8������r�!�Te��~:�^�3�n��§'?�6Av`8����-h�R�(��Rġy]�'��M���r
}v^6�Od�]9��v��g�K���T�㟥*��y�f������!4��8�i)8��ອJ��+n�Z{���|�LΧ�'1*]��~��N1P�D��Փ����%5�E���~� jTL��:(�h��\e�f>�#>3y��Ԥ�>A����1K��Ԇ�WJ�.���j|"M`������d��q.��1�}�ئ��:38�H����aX���g
��'���j�q7Ǿ��s���uN���z��H�Gkd���ו\�J)����kW���9�%z�x��#�;�4Q1��#GA�"�甼���+Қ��Dn��\_�֑Y�H?b�l����A��5_eo��#���>Z@���#CGt��t��^wˉ��<��4���CHk�II�o���@e�)ch���H���fpV�i��p{`�%��Q]=�NS��h0I�\��|-�>�h�M�tQ�l�u�3��@����@"U���V	�Oz苂t�d�RR��v�u�V���tP|��\�	��t}eмb�^{{�o�Ø�X�4����?�M ˬ����⫃�˹f��Z�3��k~Z.��j��������'�!��U-���$�������qv���{����|�6����J@)lo�wJW��_�`4��������3 �p�Ql����1���F�{s�w?9K�Tr+��4;@UU̗�����5���CW�����ӟ�*�v����G�c�.��	�Axa��[ƀ�^y'I��/�:�<p��kⱋ��B$�@��C��~�(D��C�|�~�R<���MH��)�*��۸Vv\�Ш~`�8��q�y)(P6^ڱ��yd�>�:ם�I�U�n0�8x��=�q�l��A��}�;���;GX��dn+�2��m�ع׊1�˒w�	��s`f`�<�9����h�24�{�/���4X�~�~H�3��3SТ�3ˉ��Ӓ_�q��i}k��N/��X`�u<��u*w�4�h��	�j��~h@:>X��=;�ɘO�T���A��5OY��FȜ��0�w�L�x���o�QP9o��~�qԗ�,Y��.m���5��6�߲���n���0�W����L���q]���Skƙ9!���_�r?>�C�:��P�/>Q�3��p��9x�v���x�o*	���h�p�<�������y�e��P5$�0|p���A�i%�f�G��|}@b}P����"���J��w�9y�:eɣ�pTTy�N�������2cb �2=�)��SM��r}��ޚdd�d��3�2� ���Ȅ�R��݄��Uo�'�M��NcQ�W�~-��Gv�Gl<@~���̓Q8ˎx��QNx���*|֭�M}:�93Q�6a͠��������F�uCVy���2*0w��eD �,�͉�p�D�C��j1�Ѯa��##9�y�O�&�x���p
3��I}|�`�uq    ̿M}4eς	�KBv����aw�ԍ��݋.߷M�Q�
�B�곹�`�S��N	@fL,������f@?��'��ϕ�i�`���@lFH���j����۪/،2�3����CeG��L�͹�:�!�?Y��h�r3d�	P�pjO'm|��HZׄ'CB�����Eh���{u����$]Nzo�ss �qg�c��"��2ɐ/�W{(�T\��W���ۣ���K1��v���VK<L�g�J�ՉL|o
~�������ݯU�������Ǌo[���k��A�$�frb�:��%ʭ��cVfa><��� i�����\ro�MT������_��2�]��5�j|;v-Z|�Tm���\�xJ#���~��M����D�b�p}<���w�[S���X��2*���y�$�sd
��H�as����zu����@n��ҪCe�7?|�EEd���CZ�4&_�.���X�(�0�Y*Q��K�>�3����ۅ�	2Wƈ���U��sص��N-�,�����(pc�����%�4iӬ���"4VϞY�ϑ�l���b%f�Ϊ��(L7ֳ���!r7y�R�ȇ[�ï�IO�#2ОL�a> T�����������Cv��#tI����n\��$Q���&j�fA�]�mw��ꮖ�����R�2[<jD��$�S睜���z�*�Ok�f�d1r&��:��^ꏗ��0'm�����>�3_�$��9Y�Ha^�j#�+����~��!�Via�%��S��P�.;h��a���T+N��ɉ��,�}�Z(�
��Ad	z��Y$�����[&X&�w�rvrG�~1Q�2����Z�J�	F�d�5��Lv���6�l��1�|d����5V�[ -�~7�o��>ѩ�|�8IZ�w0�,�gw����$���O�`y��yjh�������ALj��������/mѲr^
5�L�aшW���邠K�W��| �<t9}�p�s�95{m� �WU�A���v��e�N�e�[1g$Yel��L�ĕӥik��_㲨k��C�R"݃�N�{x�R�O�ev�=1>�Y�t���720�/��߳��ȴP#n_��wm˅��=!s+q1,L]-S:w	�[Y'tm�[����/ƀ����ג���r�-:qN3��T���{8ت8�>i�-�$�:���E�~�6�SZg$��Z_%�C�&?���/W��׋�z~�
��~OWG<�V�clV��6�S���R�LTx�(7�����f���̏!S.���*�l��HF��檮�݋�`܁-� 07Y��F���L@�� ��X=��������6�]�ztH�����S�k�1<�q��G������_�Ǝ�5t�<�j&r�<c�Y�2oO#�^�=K��i0���/zNp�~b@���$Z�f��03)Y�fXש�~�Q��������'	�c�OՇ�k=�����-��֓uN
l��e����w�P�H��)��\�
{ Lκ<;N|��b	��!*���@M7W���5��������T��'&搰�/�"a0�}�x�R� �H+
܁���.�5퇟��B���(ݛ��<��1Ȍ~d�� 4���Z�Uo��������g^���G��F�Ҷ����a�.�ū��N/�b.��"�b�C����K��~b���~&���ɇ��P��s>��g�y�Fn�c%�°��0>�6�%34�� A��ZB:�s6��g����c38�>�#tm~59J(��8��<�l�zꛊ��I>�ܷg-J��B�S?9�VpTl?�+Td����)3Ρ���+\Mwy�V���G�#���zy�<���p�Κ$����8`����@����Ct�t���OU�%�W]���b��ҽ��N26��,������^MV�����Ъ��Dǚ�~�3]�|@�vx����~�Rx`N�F,ӯ��X��T�܏��9���+�Wk"�Li�nn�<e��T*WW����wE>��L�����=�F����r$��V��+c����E�9K
��'u!v~��+(|>]�Yi�������|�LC����β�$˲�}��������ɭ��6�~M��� Ҏ�)��I��M�-��5�����,v�!y�{�L��{���g>�$ev��B�4��U�`��Z*�{��fܞ�SZT������G���;!��`��Y����ʶ�yB������9���Wâ�ܯ��aŽ���������ӡ����y;
�0�zbk�c���`)�bkނhǟN.E&'�2L�I�����B0�GoEfC-�':���0��bܿ��X앩P1߰>�-�쎵�)�U���I\8AMjd�+_�����߽{�
%�ޖ����#=�����8.�aK�M����C&�6[��}#Z�̜�X}����ūʜf!"%偺uJ���BZ���:���������~�Q���M9k��/������M7\��-��mC����`)�TY36̪u��Ɏ��r.�2���6��
׮ӇHn�I��Vr�X�uCT��-Ŏ�<m�i0�	��$1�M���Rf|mZ�64��端�W�tu���,:G��!�� w@���ꃶ�3�����j��6ҡ�FU��FZ��Q /�~8o͍�TE��S�\t�"U*�J�%b�e�S��z��ߺB4=N� Tb�v�]����i0�(���I�iBh���f�ro�K��Ļ><U#�:d�� �����1�z����V�y�N%����,^!��v�|��Pf�kň���S�:��n*�]Y�\VF�3=��Z ��&����U�R`cm^7����QMPp��%c�y�N�`c����;��	ӭ\��;�6��l0���DL�����7��fP�F���Gx�qBQ�@�>�%���A�-�ψ&H�:|^�f��RZ�:~����2���#�u�UV�I�2�U�5
g��FS��ܕ'���~lhrhŕ&�v�#q" ��+Aۭf5�B��/��i���!̭��"-L����d^z0���C�
+�PT�w�-��mmtq����6�.���0��!��uHXܣ�5_��+�v<�d�ja���9�O?��p2{FF�I>�yj�
g�m�Ȱ<P@���w �6禲Y7�j���yw)>O���	��r��v�M.#q^�_�3
7�l(�"����/�rn��)��7' ��?��=`�{u�n���٫��|DqZ�3a�2^%���<r����Ĭ�g_y٦e�E8bo/�/�[Y�H3��
j�=*b>����eۣ�!u �޾�o������:,ވ�k��>h6š�b�>�^�Ԣʤ�z/��`�1�%8(���u�k�h��BRt�w?9�6h�w�}��	1�
�����ρ�0D���C�4v�%�a.^n|�+ u�˘�J�1��,o���F.�p۳'�(��I;��n��P^s�A��)�'��/�-;6��M����	q,r�ż�4�]�F FB�3�G\��H}�?Pg"��ZS�֪��_~�^Wjkr�����w�%�6���S�� ����_�K��y��j��$
�a�BL{s����8�ƨ�P	�1��DC��}m0�ˍ�(�2�N@��xk=��x�Vy��,��mNσ����Yw��A�ɛV��yf��RM��N�|F���[ؑ��"I�
��ţ!2��́Be�X��~h+����a���J�������-���7?�}Sd}὎!�$/�ӚQ��D��ՉTVq�GB���O�y�EQ3WLJ���\G�E�O˄��^��;铅ֿ,�G��#�0,�|c��h%~��S���\k^��#���[�o�����4O����)Jxi`�U,_��p2A��h8��p�vCʡ�M�
�8u�:^�<�OM�<�D��ӘB�/��!�v}'.�7"�L;Tz���ӍS�_�唃���}9 D �Vc=K����������ZB�v���q8��z��M�0��w2�ER�|�o�[yw�'�]Ge� Q�Y�V�yg�ʁ%���{���(́�樀�����U�b�*���2�    ��jc���'�GW����^d4~�A	O2�8מ�J�&�4���},�tH>��2Hi�1�(L-�����-��~&��r1K�ƽ��x~�6���4������<,����}Ɔ�Q߱u�� ?Nz�����J���� ��;�Q
d� x�m��s���?Ӆ���9�d26�g[կ$%�;��އ����G�Vwm��(��b���"��%7�0�%���n����4i�.�R�l���i�|	���
�lz���(v���r��0��� Mf�2X�j�I&3���H`���O*^>}��࿔�0���w��~W��RK�!�<`7�y]<÷P�]��`|����n8y� �$��r�%�\uxz��+�ޘ�������d�wĨ�&Y�Ѕy~� ������qT�7�3����?�n��6��@��Eϡ�4A��������퇪()d�S�`^�渾w�|��u��dꉮ��qa�/���f>"��VƎ�yH��KR�Wr�l wI3��^���x��W��B>p�K�Rfx����F��%�?~���=Ov�G���oH�?O��1�&���u!�0�f�U&)�)�'��9�&����f< u�3S�>�5M�-����u�~edyD�:TE��-Z�2p�N�|;�@��-���!�%�	� �h��c'�.��*H�hj'3R����t�'��}XE�w�8��}Zo��N|�mG��M_���#,������x��]�}A����D�c�r�*���������Z�j8r�K�^ۆg��=�ɓQ	Tq����VxkG������6VJ�vaA4�g�.����6S:v=�0*l��Dh�b;y~��Ly�$C#f�(���τ�rϧ�Ի��r�ߓ������;���lp������1)�N=A�s~��h���q�J�r�~4Hظ���R�$����
l��m���s�k(��)r��@���}��9��7R%^8��,��	'6x��6�AǚGElr���3���+C>�f.�U�0m�L��$K<�f��#�󈹪��m��i��u<(�����f�)������&	o���IM�+��R�^o�7�%TȎ�y2��o� `M��=�2}���J�k���{�zD����g�o�nu\.�q4��ڙ��n	��nl@4e�D�ܚ/&�M�gz
��U>�L�+�i,����� q�<xz���j90"0؍�W����l��]�Q��:�C�Zsͤ�h��ug0:����߾g93�d�Hl>"�
�nQ��c`1x�\UZ����^a,���|F����p+ �=DD־�!�Ҡ�Y�C�S�utA%�N�ۻ�%�À�5F��6BZ��7y"~�^;���@��rI�鶦�f�K%�����Y�nd-�yD'U��6�����u���uP�Rp*�����t�� �7�=��a�l4�C+�w��w�mX*�����V�Ep��������;L ��YM�������������ʯ��w���Κaf�y���j����O�Xgg��9�)j�ӬV�IOm�lw�#{�Y�Z٫p;��%�%��.}���>�`��΃Y���ķ�}{���o)��C�d���K�f�.b�dl�j�Mt��n�~��}h-~e�Z�$	�?����E�"K)6'��e��6������'��0�%�>s�"�L���Pd�mKԚ���(��hi��Z; d��OO�^���I7�z�a�N/�6��Ї�`��b�xs)b������g���#� �y<e"%�����o���;��BDB���N���*��g��7�y�A�!�/�pM�,suyҁu8}����3���L�ms�hX��O�U9�����p��>�	"�O�3�����(�w�u�0\w��C�Q�h���Ja"ؓJ�@�0�T{��J��d��&�V#�k��%�D�\@�#6;�5H�;|�3�P�k�IT� �C?Zn�,h2��G���K3o�כ��.�P����U}�/9W*���Nl�iGzqI�w..(�{7о�(d$��߳u@�ߊ`r�o��\���֚g�~������6��y�0,��1��Q���	�Č�$SC@�����JZY���M9� ��@�Y��N�d�&;���袯��I����62D ��$��> ���)>�^�2�,�m��k1>��B�ms��l���RYy��dd���Ȉ��+�U։�r�+��,K���E���,0珍q�����.�$x�f?��yl�������X�g��ͲE�<h���
a)?-���I�����"
�a0﯃��}=o޷�/��x�6s[V��J�N�v�� ��UR�.�Ho2���o��A���-���̌׶ݑ$=�p�Q��7R���'0Κ�1k�{��؜P�a��r�-�s
�!�&.�1�] -�S�P�i����8�j���N�ķ��iܠa`H��u��l*Cvʈ���V���k8����6�߂�;���,h(k2S�Yr��	��N.'nC�RM�ZU�4�k��~6���/R�Ч�I�e��ȂEf+���$�V�ָ��nKا�ΗV���R1W��-����t��|���ԣe\DN<0@��4���&�Ȃ�˲[p����9h`O�d���u��`e��t��fU2'�R���	~V��N�g{�%�Ioӫ�G���.�'�$)��:s84�#ڎ�PQH	-;T1]e��:r?�ع]�2��㓱聑 D����#�͋벅h����_iYt����(��%2Ǔf���5c���������*V��yj��jR �7�ӑA���20
b,y�/�@>_��c����I5f����Ű+�����Xvs��iC��qF>�4��K��-��5n~��i�u� 4���uBረmJC�ᐸ�A�	�}��xu�x�~�F9����Q}��d��Њ��~��oe�j��Ɋ.�1�*�E��i5��5��`��*]d�<綘���)���֯���&��W��*���X�㈥����|��6��_ݼ��ᠳw�^|Ȫ���B�����������;'O�$��v�JD?��̐�sf�E���>��lY��ϩڅ�ݍ����r^�ΉR�����o�������{�_e|F0	p2��Z	�&8��WMi�l��n[3�rS�����|�U�R�Xu���L�f}���1�X�B怨9*��ol�P�ǔ���8߭aƯ���&w�b��@s��k�����=���&��>��?	b�� #i�k��9��1k� m]�N_�jJ�+�"��6ˡw�s������h�/n�j9��2�����Z�#E�J�i!������a�#�=�R&�/��ˊ�k�<��C���(- F�!�'V�&O��Ԝ�2�uno���8���'v �1B$H9Xf��6�>R��)�����:����F�*����R^���0X�4|�0�T�[*L�X�e�ce��:��r?<�n�kp��i����G6���'��vW;�=o<�g��:AS���шg��˭~H���%*ì�n��6�].���
��sz����pFo��1#��<I�J��~��v|�2�?)�i�G�����x�9�$ࢲ�J��Q�%�Uwd�T��f(��o�@�`�DS������z�"�L�g=��t4춼��k6��0:CM�Zdڹ8C�֓�Z��d/p`�Y)+#kg���&�n�:h��& K
=��P��c�� ��*v�Y��.#�G�y_��/��@$�j���&�羻�q�<,�`j�g��v����%� �s8���u��}���	$�\F�b֧*�+�$��Ǟ����žwCF{��mW׮DԠ9��ۮX{C���<��K�L`8�	��-	���6z�i�kk-�XK��R�.i2��|�_���؉�;��$�.]��a�C?;Қ-��g��l�	�,|�;��Y 3Ҏd�pDg����Hl���bF�ҾO��p��R �v1��>��!��H��{��[���MƪTn�\T��[H*>u!pK�{    �}��� �w�ڗ[O�/��>+Q\�L�R�l�Ӿg�eN<�^S>p��'�q���-�Im,��<A,����<D����5R�c���0�t�>��fuߓ=��2�7��"���O�c�s���)6���
��p�Jy��C@�J<�G���g�n�Et���GXQ�<���t�:����Tͩ���}�"
�7|�~(�����8Y��%��p�q��w��T�We	�䩐���2�����l���(#0�v}sy_ڎ|rpҥ��ƙ:S}
cJ�p�g��3y̔ZT�]�@5hHd��x�I�O���iu��Ӆ���Y0���#GXGc{>~��s��� {���_竮��m�n�� c>�77G�:�y��*!(��E"߁aY��G|��p	�&�cu�$�k0��:Z���
t}`�d�����ݢ�7�'�V憾�G\�� �����9��|��d��K�5�Y/I���H���#	!����ӈ,;��P懿꯱ww����f뽂K���!ڬV�<����]֓�ώ�5,<\[ݹ���-�]�W:բ0�\+>���e����l��8cZu��aв�|C��U��X���F�WV�R���L�7.=���v`m�� ⴃ���������_��C8�Iz�?���Ĵ�	b�X�9���y�,�IЬل�M����R�I[E:��-
���J	i���><$�{gD�nxoj��c�xKԬt�xy���7���H�QJU�a�0��ET�����D���5�ݛ���i�)�Z�N�V|6��g�
p6��O9�M���d�va�/��'��zW�]K�,����gV���r'���4e���7��壂4s0&��bwL��S�%x2L_�����p7�v,�Q~��@OLw��1�7v�Y����"a
l2p��P�bD�K^�D&MYE3�?��j��0e\�Y��vd�ؐuL4z��tuʹx�NǙ���l|I�jn�R8"M����2Ժk翽��X�s}H�(�x������Y�G��Ղ�@m������e�9�l��#���I��b�M�3�\��Sg��ݢ�����p���Di��+)ם��ʍ7;VS��M�HK�y1���c�sȡ/����������Xr�76��G`Pg�:,X�A���a0բS�`[lI��e|G�%pΕ�2x�$;~���W)�w)섷\��*��HoRm%=�����,�+O`�Z�y�&I�-G�* �`��D��k�/�8?
�;�,�_�������g�#L�"�����E�/���0΄~�`��s��Z7����vT7�y�w�$�'�'��U��$��k+��c��
��O��6�]�1`��p�TF�/��~�����>��� �07�\��e*Zݜ~�����mb
���&R� �EAw���2�#�?~���'���|k�����d �{�{"���Д*nf�"��v�"x�_x���L�8�գ�i��_�c&f@Xj7�h��gb��o)?c��yP���twe>N����WbZD�&�	|��Tb���	P�n�n$�冕�{	{���3��e�/�؊S�z�"�!�r���	E�[!� 0�M�V1
D��/ά��������&HV$is f�����4<#��"k[�`b��n��R�N�6�����M�����U����ݷ_�WzJ�u�eB<�'�B��ߤ5��!��*-����B�%
�#��1�l�mb����:k��}�JE��` A��z�N�7׾��E�-�>R�O����I�#���m~iyT� �V:���L�Z�T��~I�`����Y �!����S��}aC��h��]2,�6_��>��Խ���/�ia�x�y�����E��[
1ĭ~�q}��@3��8X.v�w�/R�C�@�+f�[���x^��w�K�5��6�{%���RW�� �<þ|n&�sd�S?�"���w����T�'�͂�������j�l��C91e�f[@U�3Waב��-�O-ť�N�.D5#h�����m��K3�f�����u�cW��g��]h~��gm�����\���=�����$)��.J�%(����յ*�_��$N�'�H�� ��7�U̒��)�d�=�x�8,ƞ��\��C�?*� ���`��^x�$�S��Y�r�F>�/��_\m˪T� �_`P*��p�C�q>}�I���ͷu��x�T����߱���Z�/�4g	|�����m���'����"�U"��c;,�2ߤ�3^�� ÿ��h����q�~b��? �f�^x3���5h`s|k$k�7��ƀ��6��s���^���"�&��61���}3�h^M7�������>1�*�I+_V'�_N$�p~\9ƌ?7����!�FH���2~~�[��S���TA\��3�͕�BL�%�j��֫E��3����1TLq�@�>E�~���h*mY�/F�8����1�-��W|4Z��YW�D^:��{��04�P�ȣ�G�q-vf�: �ǉ6B��i�q���Uq2͢|##��C1~�4"����M���Q�H�?֋y_�h�SO�-��H���nf@��Ѐh}���q��SY������hk�uSÛ+�
�`
p�F�Y����*���m[��[�Y�sC�h��"K���-`�l��t��{Ӆ�$N̓�ᚻ��Y�:Q�t�	ؙ�i#��:�_ț�w�C
|�s���C�Xqf�co|�<J� F�����C)�n�(�� (�`�cC�jub��@R��Q���/�����K~��Je;��_��G#���}���BUS]��9�06g��	+'���o�� 1lN��k�CGuԼX���Yi�0�gȃ^!�C�ˏg�*0bb�����&~je�6&��Չi��ݛ`��ew���i�O�I�Ъ�~�Tv�ۙ�&�i��L��ٴ^���pq��\L �՟(v|� ���Ň�V�1;�FY��'b���q�y�7�r�Q��6���\�v�����y�L�%'�����#�~be�^���m#�_�
�R/7�Y�i8���X�iT�ke���[e�t��{�@څu?p�ڶ��Y�(�w ����ӤO�k�U�����o����J�Ek�IJ�L��h��ܙq�Z�!�e8���F��7��rR�(�eX�Ay�K|.@�k>���/���Ms:tC�|}�^!��>-�{�@������?�e��H���\H�Ȫ��P�����Kѣ����:���3�p�t�w��S3'5��u���wL�W�hw��W�YPz���of�ݓ�M�I�{]�l���r^'�� w{'�'���#�t}	l��b���Nd���A�^����ڼt��o�>�7%��D��c1:��e �W�mx�Q���Zg��,0``�Kҗ����:g���3'œ���zq�G&��k)4HQ�O�G�i�AQ�hp`������B��d6�i�ݫI�MqV3�*Zε.c?f�_�!@3_^� a܋�.͂ �(�'&�G��35<���ď�w
�[��[�ɩa|Lm�*�#4Q����+{��?�)h�C�\�
���}��ph��>0�P�ƭ�ݟQ�LEI��r(�7'�lT����g�ɱ|��Wx�����KF��R�������"��"�_DA>�
*<�h5֨zf<��7n+��ޏ�=�m3Tk0MK\:gD!n���$0�SA`}�|����+郙�s�˻^���8�Λ�4ԃ2q׿aZM%��Y�� U��d�Fu�Kn'��S�a,��xρ�/�1�LS���a�Mg�:f�pG��&� z�K
�`��Pu߶$@�Y7H�r��
�~ۍ\�&�u����p_�-���/Ё��GFB�d��������p��z}��Ҋ�E�v�>��?��f~o�1�3��\d�=�DGh,x���E�p&���X#ꬍ�(i�8F��̃�0�VO���;.T�dC@�>C_���{��@7$>-iu|��ee��Rf+��U�r/��(w%���ˢ�v��1�ٿ,���0Zc���";Q�q���Q8���ޢ9P�M    `�Z�����e���{�(pd_/p��j�$ҕh7�Qg��%s>�X'?�}�#�w�P �k���I�ܚ��]U��Nl�Y#&r����V�Jc��8���U������
UƾȮ6x�n��5^+��w ���~������&�1<؝��.}lM��9��
,>V�=g�� ��b�*|��P����8�����dR��VI��-%r�ָ�	'��Sڨv��B�Jm(����7'�6'��p��^(�V0�\"�5�>{���?;wW�q{�Q���)/ FevW��qo������g����}XvM[(������˯�_�`q�R�^�l��� Z fCNY	��O����!��Qe��h�kԽ�&�PP�[��:�?o��tU�zu�T��mn��|��P` >�.zL�����,U=|pZ~�`-��5~�z/L>ᗅ7����bS!Z%p�g��;���Ye#��a^2�ա���W���A��rk�� I��E��	oji}JT�s	�I̙5� t�\pn�u���uˬ.���Oi���=�.��}A<�:�cL��@BC�|���Ѷ-��ѽ[TuN�ίUXOm�����x���ɓm�9M����+x��KX�f�W�Y�wB'w���"���s��������E���w�	�3���4~c��ge7p�D*ٲyA�I�6�A��S��'M�!pK��.����f�����*�f�<�)ґ��xeL�}2��.Em�H���_�t����+ᮧ�b�Ho��\/b�az,2�8�W�fȦ�|]$~p������SoL�`��,�F��-���lgvS�H4�,��yc�f�oX��;ڑ����dw��O���Y���S2������%G1�;��\H��e��mj��M:&K�'�ӼNȹ8���*���\���8z`8�ʘ=�[ZV�B�D���iY�Ia��!#M�rt��8!�}�6���W9X�zq���<�_f���^y!X��Hbk�Ql�1��w���,ӯ�Q���~j��p[BQH�ΰh����H�3@�T.���1ܸ�K�+��tսu�����ώ1U�s�L�L�a���/\���#[�����b�[�(��fw0x�M�<t9�:#��-+3R�XXcO~��5����ax���]+a1��$0�L>z$�Ї��)6����L�Z�nC�J3��G�S������jK�QkB�(Ϙ?��Bs�g6��&�q�5��%����Mݬ,�\�/�;;���k
y��lB�T8�>�$�(��fTa�&wr`�l���s�j��v$��uK�����үa<ކ�G��5Kļ6E]���]��S��R���Z>��Q����r���=�LX��#���,i�K�Ŧ(���DoΝ�)��	\�����O_��n%�L},��H�W��<u~U�}�i��ƪw*l�)Ӑ���&��ķ���@K�daDh�GRTV�Z����x��ٜT��䩥��;�8�,H�xF�x�7�(�c�<$E�5����_u��t�����-ϩ[����&�b�oxс�1�٧b�]/ĝ�S-�v䓦e�Q^�Y\e����?H�V�M�k�"��$t��(#�|4�Bh��6�}�EY((�h%�Eia�Z�{j�H���f��Ĥ�#"#��@P��b/7�+��N�y��(���L�,ꛨ���4y��b��)��x��dH^޸1g���_�k����%ן��}(��
�Z�i�$F.���%w�T�#���;��c�M�If�Cc�e���	|�׍�~���쏲�9������%-&�_���`T��a p���l�疱4�);�$���(��^T��5�ڞ2ƶW��q����N�s�W�ϕ������[��~ז��ܽ����[����N�Di|؂C�=�Ao��I�'h�r���*��+��RP�vf/��z����+��"��T�j�R��[-$om��qy��[U��s�)^t���e���������Y4���^��
 ��؊��m_�h��pq�k4X3+}+�`{�nա��cB!Ջ�i�/:��2�g��
��-l�����7gq;ɶV��%jT�x������<���r���!��<��>Ѥe���Eg� Ǒ�P y�F� �Mj��Y"�d�4Tq9��!ߵ��� e����+%�@Wp�'"�Nï�I3�TU�۷�ӖH��+UΟ����r�U�8l��3��0x�����~k��9p�CV8�k
y'w�2!������ƕP%ޒP*�u� k����¯��_��7���z��R�Dk�{@ԯ�#�����h��s����ÜNx|��<z!b?�_�.Ș�^o�$i�ҜصШZ�
Ō3�k�N���)�:��� A�%�o��Ö���uV����k�-�}S?̪���u�.c)AUe�-py�#hmD��?&)�B6�o���}�c��W=���m�XP׿+|檦�j��r�́x����B�skYb���I�� P� �����F-:�'��`�9�-��ϿU�˛q�Tyo�K!n���Tq���O3|��s��<ӌ<Y��Гg%s@��𲪷�+���q76���w&Z���T��{ҫ��~��7�~X&�<����[U�	����Ц9v��[�q��7�Gf����ժ�Ӗ;x���	�R���X^HNl�T��V�<�#�C�3��؊h�¨`4�8��X�o�Y�$B
��YQS�g(*���n��9�1�W��vwI�ی$U��{8�"n��\�~��.�Uz�paon,��m�156���BkȮy�s�ԕ���i�2�	Nڜ�5����t}�s5߳�@��D(;ݏ�_v.�J��ceC+�&���q�lZq�R,9j�[P+�،�`�e�}-����3��U�Z�)�T'���� ~ �x%ߧ��L'D��W?�A��y|�}z��(?����������$�+���-�j�!)��&�"����.9TL��>e�qq��I#�4e��Rӥ�ބ��+�\m�M��1���Qؽ�2Yw4@հ��(t�[���Ĥ1lE��d���'d�6^sJ,פ���G{VM;!C�o��;	*��8�0����BE9�:�:Vso9@F��oX��y�*r��5��s����(���`�����Ԣ��q���jW���۾6dDz�ʍ��H�]�LWx��AR{2"g���,��"�c�]�C1ӓ�$*s���7^7�,dW�hx��~�فi�?Q|�\oۃTW��]�����#�@�g;��y�I�N'��z������%G��B����2�˕��G�Rf-��0���vCӛ�f`^� K\��'��|�÷\��s���ttW:�vHe�hUo5�d�}�X�$����6�f�EW.��"Eq�?W�}�h��R�Oe��2��c��ߧ��NP�G�ge���I�V~&wZ�|�����94<80�a��s�2��f�LJ_�3�U��1����oo���pS���ޔ�}��2�n�#���`�͹%�"�g�V�-O�6;�r�,N�[����������&	{5�Ơ�ut��F@��Y"9���p��J��h{�s"S��+��&���Z�v���O�����D�%8�,PK9�v��E������mt/G`�qV��˔'���;
9[%�'kp=%�$�%���c�$����o�����ņI�8`\�Z�nRcHd��\��Cͬ�r~���}�'qE۾1�j� �n_o.Ycٿ�=���#�YǫVh�
$�jc��ʰj��|9�ćE���R���@S/{u~�y����k��{�S��p��c��tx��L���,Z�r~�f�)8�Q�#��3�9���s�h�������w�u�	FƾdAL����Λh����Tا�?� ���J2Rw'�z���p�n�z��	��h�%���oE������o�{itV\�ɂ@Zy���ۋ��>�U��M.�x��
�!�t�p�����Wc��k������BD.3^��e*rk��U�I��]yɏg� ����3]4X�v��Wm+1�ha�"    ���v���U��`r�6�D�8�ê��w�52~�v���W�z��ҭ������rs1�osy��>r�[E�s.�T�]�R�!�U �Zj��*���"1#*�G��jZ��!�
����`�5�5�����`�� y�_� �.�	n�*&�bF�>��!F�X�?��k�-{�5��KU�gނY�@RÆ:�{v���d�\Li��z�Wj�$�b�I]��c�� \��l�;��j� �f���"�#)|�C/��-�l,-UP�'��������� �DD�E��݀�ڦM�	���/��e��Kpǈ��ЕjG�j�Fw���m�U2��*n��/�)V
����%�<~I>8���.�oĜ&�'�����g�B?z�o���+v]v�Y�T=c@E�B%�9��9�`笛H��t�!��"c���PGlJ��ɵ�A�`�B5�!�Q1>|���=�]��
�a4Z��3N���&kf������_�b��ċ'�׃͊%@G	���d�kIj�G,�֒4�'�R��Xa����7ٿr�6|h�<�?0�>��s�j܂�5�P*�c,��'�#U^�����l��e��9���O���$Jc��ޚ�zGu+����7�n+?i�`��4��^���L׋Ѷ]Uӏ0�u��?��L*�D>�V�����-�/Y%�h1��	ҷ���I�I��]�x���:�����{�`U����|���¿�w����i���nn��3;�_[uL�O�����F�]��>(���^z���❃[�����7�\M��w6�s�`8�j��Mߞ|�D�h�T�7���;�K"n���I�c��R�&�e	��h����ޅʉ���EA���;_��~�DI��ZP����^򩍳�ǢXN���i�F�g=Y�v\͉3M����R1���dR�om�;K�)����������P�d���=U�A�Pi�	F�֙�IG����:~��f.�(���d��6��	��lJ]?��{t��ì{_�v�y�E�\m���p�j;���t'.$fv�u���8QD�'V)sѰ�I[(�o�r	$����^l���]�J/���L�L)�� �r�l��[�3�0��Mz��o,�vҒ"v��R����pX>�:�-oH��Z�� `��3�A3BJ{��Gk&�.���YX��z�*�-�-��<I�&�h&]��g&�yw�Q�጖q�g~-� @�e
t g���D
��}q٨�ZR%�H^ԛ�Z4���eӻ}*;���7��'�;�cxI�<��P�:�d�c[��t���ȴ}��_zMzh����X|
�T����|nyw�<uؒ���J�W4~�Ws���0�`�یf���p��|r�ϭ���A҂�d�r؛M��h�.?��4�f�_�,m�+�8P���>��8;�$��͖yZ�����u���'V�" ��Qs��ۊg��L1궊��l~H�;4�_���Hn���*�|7޾�'�N�Kd��8E�ɺ2�[ئ*�e����8��`ۣj=�� ��LkL�`�����N=��I��������� ���vп�+/��J L�M��,�_?1?8����,>|i��)����<�(ej2� �#�El�l��7	���;y��(��A g=7�w�1|�!�"I�N�&�6�B�t�v��σ�_q����({qŷ�Qе3��F@�Ԍ,�R����n��E�N,��R<x(w��(Fkė� 9��y�yIe��Ocp��x[�����7�B�꼾{I��9+�.w�q���5�����EN����C4��E�`���e�W}E�VWko��VXR���Xk2�� ���mx8X\2SM�5�`�I�K2_,����Đ���5W�z8�S����V�O��k�~����OK�̳��H�.bv��ѡ�k�O���j�c��q<G1����c��<��jRh`va��-��\Y��G��F7-�Ud��Z�f���C����G|��+������o�m����mn�7ĸ�CM�U���w!�_�q)dkl�Ap����;N�@��/�Ƀ5ex����|��M�ډT�8,p$�V�?�
�/P��G{�Z��>���2�.�F �,�2e��Ov�Q�8wZrI��m��^�~��2���ڰ^ǜ!�P���������L�k��m�ܠ?$Qt��A6�{@�U-\h�7E��W��������I4Ί\����ڳeE������6V�u+�T���3�n�� ;�M��8�_x�^6���d|�i@B̤�mr�x)� h���>B�-�ʶ���|���}WR�i^���9��%=�5���YH̯5�6gN욤����ivb}EK���~h9��_F�wq�}!�47B��iݤQ�����\��biĩEt���^�3�?��L�b�z~A�Q^6G��H�r$�]�O��-^l}����M��1%�û'�pYx�m��>��O����X)Y,)��f!ۂp�TK�oK8�Bx-�2j��.Ŵ�5�A\�l�u�SW&����즆a��	�&�5PDZ�r�� �ř�iߑݚ�ʋf-��*���f�9�X!�?P ��5����z���-h���j���������O�nٱ��h~Tg��U�MA@�A���g9�z$� ��S�H��c��Nȇ�2�H������FQZ��vtB\�|
��g��M��7e_C�j��Lٳ<N�P����c�祙����J
�'<���M�4~�4.�k�&��G�GȉA;���}Of9����?�"b���\"�$�G3	��U�^���������T^T����c$Hc {� ���&��'����"K�c����#��0�K�;r&�5�hJ	�P�l/��f��ԬF~�<ߖnV�A��Pw
U�;G��%$�Ӥ"��?N���Cu�0�;y�xCxf���j<%(�qb�����<�}N��c��>��|�04�OP蘫6`����Q�j1���w��N�'���׊��_��=�M������UQ�FeH�jhhB���LD��6���,��&ܧ毘"��dRSy8mS��Nվ��h��2����쳳&�#�𘆻�t���S��,o�������ʄ�?�>���/,��D:��YU5ջ�m���./5��`�iJ��1� '��(��5S����k�L>����Y�up&Ak����ċ-�,�a��"S�6�q�#b>%Z�w�J X��J����޷��Q��V�O�C��FHu�Iў��iFkW[Y�s�,��0mp
���Ϭ©�%.?�l?�2)�a�jފK�)%TI#P`1I��ⱎ'���S=|������͹�fp̓/$AڹJ[6�6���ȉ�� �?W�|�b�N��ژ������2����K/�4��]$_��tn�?H�Q�)̇���{8�A4@��_���^����w���Nu0  I+C��Ԅt�w�ٖEh�WKeM_B�*Gb�I�~'�XLO�Q�=/}*��3�}��\�Z�Un��5z�~y � W��~Fs^�LLz�yP��GRuR5���t�q�u����QP@[���4��=-"4:�i�6��c�/i��P��`U���~6\��.�нy腑��q_���[�W���=Ў����hd�#=�}�ӫ )�1I�%��`�[Mr�4�	N~Y^�l�m���7����8�)~f��K��8{n�ؙ���O�X0=��6s�EZ�i�k�j���� �Ax������|��l�$��z%�A�|.���qA��}ZL��<�t���58G8��H������W>n�p+�"��BO�GN�s��S&I+�+LW[$C��$Ő���9����L�:G��v�k�or��]�(/k�'���9�m,�8�9^!�~2w5KJ'K���f����_/��;��Og�Xޚ�h/��I)��r�LL�{ |2ty+� Ҿ7�^�����/hx�+;WaB/�ب�w͐@�
��x$B���yLGmQzs�գ}�ḵ�ڗj�pr����2�Ep��g'�) D0Z�d�|_G�:#0.1�cȿ���%�oX-JҐU5Z����KJ#    w$���/ʗ���B�x��Yin�u��\�07Э��w���j�	�*x'}�x�I*�3-b/�g ��A.��
O4E�:ִ��dE[.��>ј$��~��e�S��jKH1�t'K�j����x!O�R������)�����H;l4�0���A�n�<G��UgN�s����ɱډ����	�h�4�]��9p�V5�?�I��w��������%�����6�}Ѷ������)��k�>,��7=�E�����õ�wh�Zc/��q$�^��Z.��^��!�e�҉��{)��a���~4ʲQ�&I��q́
jQ�n� 7w�{��A�%�X�Y ���mL��!/�8چ�Z_upK��!��f'����V:Ou����8SIb�իq���7��<����)���_d��ڥM1�߬����� rP��b���/�A�{� ~E=|��u>���g�R$�P��$aN�V���W1�{�UZ��K�锾+ �9�h.�Z�G`��/.O7>���Nd�W~���e�`��Z�.�&{p.L���\�c2�r�?!�	!��C�H��|:3�#>�ݻby=��06�3��I����Gv�<$�L_�
|Upz�%s2��.?<pb����	։D�)�R��`�-q��]�UuN�����ux�N��k" �䖎���sCz�˘]G>y���/ǫ��H�
_ټ1�gK`��祵��3���hB�7�傟��u�<~�e��1µ�-�"U�[����B�>-?����$ˤWփf�$��,l|6t����Wd����6�2K5�=6"����;���З#�O���ٚh�!N��h`�����%���o�C�o��7��[������¡��$JeKZ�r�C����bШ��Đ��d�}�s�c��';ᕀ�U�`UkT6�������Kê�Μ��p�2�����JEgbT��ec�O�+_�JڧjV/cK
��:	��0ו�}����@�tN|?3�����W1J~����8yM  #�r�EJ-�Q�YJ2]�@M+)@��+�i����/���Q���Ks����ކ}ݒ/��h��Y�܅5u���ߵ~�=7�R�X#ϼw�#7j��W5v$Q��� T{(,��~�Z�YȒ},Jj2�����tm�d���D�ܵn��Y���m{~��ɷmW^^l�l�'?�0zj�c,�ui�z��w��ke�Ţ�>˱���ɰ|��D(��t�lpm�$�H��B�5>UZ�8}�m��4��Hd3E�k� T�r��X���k�pnM��]��0�t�d�`qD���8;��{$Ƚ'�U� ��*��u�f%g��&�!�j�����Ç���*�	H4���8��\�>_��{^��Cw��<"���v����:܀h���'čFߢ�ݯ�1*���ݓ(� �A8�oPL��O���4¬�p�At��	y��[5�]ü"ʲt�V+ztw��aڅ������O��(������VʛƜ�Ci��p�n�E��o�o.4��=8+���"�ޠ�q�cE����D�ӍТY5���D]��3�%{☸�[���������آ���JA�U~�8LdH|�Uܓ8?�> fS�)�@�u`-R������
Z�U�QGHWw-.�h��]M �9�O�=l�N
�ݟ��I���-�\�Y�������,)�F��O'�
��e�s�´~���,l���7���W�+�ъ� &���G:�|�c �ج���G��(P��W�WO�ğ�os��A��'b�c��7a���ᮧ�j<��:Ќo�׿g���v��'�7C�:�=�����d+<j?�$�ȃ{�������+ڑ��U I���0�[�魙9�о[� �_J�mRѷ>C�S�WR�E?���J���I��h78����K{�C�/��"k춾�U���8�l;�Lkh��h'����8Z�r+��9�wk9-�_�M���	�ҥ�ʧ�ǝ�`���PJ��ʉ;*�핟��<ծ�κ =���g�u��pTfv��xO٩�Y��V��$=%u{��T!�oѳA�Xdq_.-�rn\ވ�O;��M7yb���q��2����+���*�)P�~ya9�3-�S��Z�����3	PS�;�<����*C�ɣj��Uy�Mi�i��4�؄�c�O�嶲�V�Ȍ(L�c
�0תh7��_�,</�D���;�B���h�ӣs�Ӝ��9�M�c��!/�?5��<Ci��f�����>|E8��F$A獫� �A;<��
(�u�G���2i64�%���	�Fd�V�v�)���*ג�#�
\���d��)KKa�a���>0����PV��9z*��PpzBC������>�V��F��L2~�~���x��ܤ�������;o�X��;���5D�z���nۍ�?7^�� O˾urA5#M;�=�~>]�CջM�,�(�>�'�+[�x�$�\5SWEшCJMP�8ulZW#�!rlh�#����l�9�\ }'�ö7s�Gs��8�2����v�>��S"~��z����Lx�x&�&�ͺA��,IX7N�a�	aM(������o�\;��vǜ%E[aeB�>'�Ca���2?U[�+v����߰9K�~Ω ��p���W�e��Y��}쵭ˌLɽ;2��hlg^b�*���q�_��Ж�����X�F�F�q���\_p��v|g�}Y��ϗd�W.47s�	�k ��G�5y�=j���ֺ:�o��Z�h�P� �����P'������~��a�ײG�%�9�W�Z���BՉ]�C�A�b�.�2#��7��Mu���_���%�af���5����l���a߃�~�N,gtF�7����E8��%�~�++Y��@<۳����P~O��X��ce$�S���\��m-�ol����+S��K����I�<��ձb�侀��|��������<p�LӔ ��$JU�z y1�&��mR軣)��*�9�`g_�|��ZԴ-2-#�x��8jO=u��?u\�<]?YsLȧ�yW��
y]bj�@�%66�y�5	���P�ǯ}�7(��n���E�a%�nѓ)�~	I�W��ΰ�c�����ڸ0v�[h(te����Y��ic4�@�i�{�2�o(~�4����?������L���Z:n>�(�ֈ�~��Q�iU�.C'��c	U3��?��|_a}^?0%�������%P��!bS�e�P���X��`��ܕ�ܴg��+$�xvgX�Iɝo"p(��Dg}S"�B���)��F$���4?{�
����?��+_0IDXA/���:Yܐh��Eȱ��b�ͣB��*��o��7Vm�IU<����2���ª���Ɠ*�����7]�n~-l�a���.{��c-<S��<���(O�tVS*��x:�`�6U4�֟Yn��r+��m<|�-��g�&S�0�?ڪ(+˙f�7Y�ᯄx7�����x��g����jj����2�/W�DP�*�K��E��b �%A� S�N�A��`'�)�B���f���d�RH9�G�I��3���.R4Z{�a��G��
a�]�f?j%Q���}�))|)�D�6��s��w�D��-0�Ζ��i^wU��a�Mu�7bu��l�j �23�\sǻyfCM�Eum�j�ָ�o��Ĺ�.D�c���/=Tj�4�3>1�]�+ʏ$(%f�O������|}]�a�g����僁����Njrz�%��a3�?�myY��r�"c�ߏ ���"�t�"~���� I@ ˫[z�~*@S�>�_f�⋈�pr���I��v��	� �A��t������0�"��A��r���W�?&k+�l�'1��\�*�]���8���eg-�RQ�2�γ�����1�nv�O����`������ǥd���	�~n��T˺Å·o'�
�0��/K�F�z���C#SB�D����Ö�铟,	�I�5�Cョ���Y�ز&���l��lE�J#�li���MWZ�0'���${�B^��F��(+�K�8��>��%Ny赬� \D:$g��bV  ��(u�������}���k�$y��a�Q�0�UK�n��    �\`�5�}o��D��E�YO��Py�5�ͯ��r�H[����7ް%/�4n�s�E����z*7����)�KOŖ<
$��pK�LY�/��?@�W����yڵ�y�~�X����gNc�?���Ӣ��4�e�Tn���P�l���0���]�y���׏;�Rah�뼷�V��-b�y|��߬�*�O<�[�o����6���>���`�I�_$xE&kA�І����oU��j}�7���@���P�H�"�����c.�y�t9]�5R'R��џ����6ٝ.��~���n=��\�h���>Sa���ZN�B���b��r�d>ś���B<�u�E��;�߾�J�z�ݬ�
��t�94�?g�4�S�j�5�m����M����**���\�XC(Tq�5#j�H���ՏաH��W���D��9�İ��;�%��e�-=�����]	��'RJ�/}�X�4�7��~����p8��Зwo�@��9��Ȕ�Y�&���hI���ҵ���z�����\S?���Q&�����;HF�|���?�eH�w�Y�R��;И6� ��'�����H�{�֌�i�P�G;j�����F�;�q�Մ�U��O )-_�"@�aO�AZ�t?H�uF�������Rj͚�P"��+�Y��W�Qƣ�K��q��SS�gtatJ�du��,Kcg���b��P
5DF�Nߋ��{���������v#��zI��H�1Z#|eu�?����@ߚ�-m��D�K-��P�� k�:9�h��q�|��.0hG���l����e A���O�\S����0J��k�2��	8����	b ����K|�gm�*+�jZ������e\��F,��^We|d���3�M�jL��2ͺ���w��jH��ܬ�	9�j��rB�Z��Q�N�ǝ)�$̅����������-���'�ӀD����(B_��If�gag��ho��ɾ�/;�;"�%K�n�=-�������.�����w�J�bU����T�)V� ��h�0�s��.q��hj�ޜ`���{ȉ��I��������sHV�4����F$�{����;��-DBx��f�j�j7)\���kC�u,?G3"�H������LFY՘=�9a�m���\ӼA���ףu뇦��`�̿�G@�S��)6t9^-������-��g�-�K��[�Z������8u�ö�1s��1�C��	���"_(�y�i5���#�I?�)	 )�ĩ�|���̹W�"��(�GQ>9���}�OUk�NA��~�I�".�ʆ?:/���õQ�ko`j�����%
^��r>eL�g���d��V���+e2aa�̅&v��fbUp�T�4�|6�	�@#�χ���	bkD?�MP�[%ʧ�aZ��o 0?��Pd�ª�,�W�vH㿒C@C�Qo�������R�~�,�G�8wha�Q�5(��<\x��gW��$ɺ��\ٸx�b���N���o�E��y�l���(���f���E��ӷ���ҤO��N-����ڬ�<XG\1��ߞ��v��u{�lLTwe��3����Q$Xt�������M�qw��L�%�cb�y~�d��DC�ޚ�ry��)�=-�]��;��5ժ[�1X��"�_%��f���!���V|j��/Cy󳇝���Dp���08��(�>������}}�Q?�긷6�B{���nOln;Ԏ`�J?\)��Ҥ���������n�]¸[�se	g�Y�	:����^y&8�WkbW��׀�&�����s��\\��D�ohx����D$�o8a���Y�C5*�T��<r������P�h�XF.��۱a-�SʯZ=^u|pykd�,A븘o���wB2�6����i\��+����;�$�Q��Mq;0(*ڛ4p!b��zp�ad�\�w�2�����{$$�z�&C��~H�\2i�g;.2]�������i~�4�آJ�z+8��}>$��!~�kub/����xX\'g��]7�_�(BrE|�ߋ�,�V=��e��5:���=m3lQS/���MY+�0�KX��(�_M�`�\YL+�8V��}t�h{��� ���T�`��M�u������&�	��ׄ��k̭�p	�P#��oM&=�4��{q+��yC�Q@
X2�	�L�F�+l����U��u�;���2����甆�P��,W�U�ߧ7}�I.3��%F����J���@�C&V��B����NWd�Z;ړ�F����
oZʱ`�{�۠ڍ�W�B�>���4ɵ��a�3��;�[�ۓ$&�g�k�;L�u�\eM�g�{�%̞���A��*���C$eb@�dH��.2BmI�{��
�2zd����p6�+�d��OM�H�a6�*3�J��7ۣ��2��õ=� T �ٍƜ�m_#O�O1�������K����n� U��q���}�W��FR�mD���� h��F�K�QB�T�+�3E�,��&0{<�Y��ۨ)[�fj-,F���y��T��:{��2	��U� �"S(�D�(ن:(�8MsU��(�^sO������1m��t��T�#I�9���LH]��C���G��sb܈#��|���wie'T�������>!���Z��t"+qS_K4R*����N��S�9��va�����ް��!;�i�e�T�VH6<�s�������M �}[�a��(D��̗I�H`_<P��2����s���'�2y��5�N.�հ[��K�%VU��J|�4�.��īs��f�oS�!���wV?@�\~��5y�ϋ_�d��^������ �&�F���X�:qGr�w�P4��\�E��çgJ|��("�'� <5�i��f췰���,FH����0���y
�\�a�T�`Ϟ������B������2�G�(Lf�\� >�Z�oG֦�f���_R�O�U������!<��ձ������A>3�}�z�1������=���$��q�57�_��2yҜR7�]��V@�[��l�Lۥ[42r�cR�$��׻E�T]������8�T�b�Ʈ���+�N��ԕ��7oQ��&+¨Zi	�{rTVMu�[A�?�|}�t�.�N�g��*���}�I�����c=�A���@��a�l���2�{�%�w��If��"��x�yL=�ބD �y��_��>_<x�ك��%�~ku"j/8iy`9�/�����uf�����<մɇ�����}Fb��p��v���:�Pλ���)�+�&�~J�1=�>��3�v`�0w��+��\)���}|L��n�ǲ#�(G��lP�:T��-�6;�A4��U�V�F��8+}2̿Q�,���mz�'���]��4i7�9]��<!D�YmM2Ҵ]�����~��(\���#(I�b��%� �c��+[;�1��}� ���;����;�꺅�����IFEN3�@�Jd"M����9M����,��/K��~��~D/�P�5����Tj)t�Z���'w�(R.z<��;8��m�f�=s���j�U�觃�+����l����Ǐ{�+S�<�#W�tuO=���a?iW/uRI���\O/�R�d����3OK������A�G�ͮ�����*�&|L�*RlE9#�C���wé�v���Q��6.�
k�laaq7�d4-��Ak���e1�x�<�qq_�M�J��-�1���d�@���7%����fd̼C�*��ـ09ˉ�W2猑D�2�uø���TW[ӯ�]��@i���x!F�*��x��%y+��Ɂ��a�P��U�5����e����5�D�0�T��ǆk"�����$���$��jZS::=�Zo�f�1��� 1���ǥ�1�h��&���+����%�'%M�K�7�#�m��|��?|�vx^��*F�91��:�F������0���xfq�>)_S&����(�7%��=g	݅��9�b�I�ZI�~S�*3�s_��
�]��R\��|���#]>�D�m�9HJ�USo���g뜨(�
 _�g��[�P_���`��=� =�    >���"M&��dq�דC�X�4C�k��9�{�ީ�-P���Ϫ������8�z:-�3bX�֔�	��r�w�8DE�ςu��-,����\_(�m�{ ����Wӛ�4D%���C,ӱ��H����r�I�71'AK�a��8V[Óqx�D6��jB|v��a}�������G�h�ET��F���u���υ��¯�4����moIsY��y���oM�},�����T�rO-R�����a�c���b�*����������U��H+�~�[\n	[ŏ$E���6�R����U{+�k�^�<�ڎd�S�e�vG��g�pg���#p:p���&�u;�c���
��__�þg��<���Mxo��3���;[kA�x�ၕ2RVnk�U�2{��Z��.&h�(c�֠�������è�ij�GK��[���ȽV?�Z��	?e��w��P��'�x�ڵE@�9'�a�3����Z-?U7�<��IG���]� ���m}�ݦ�]D/���ن8�-Q+�ӡUh�XS*M���p�M]���k��^��ک���umdti�89#��V�n�@L�0D�9cm�$B��)��U�1갭b�P��\���� 揷�$ᰜ��Cu�K���mH�ف�UH�\�&�fU�rO��S������5J˚`ܩ�ŏԊQ�J�u�����,jT������,�E����=&�[
D�����ը���^�+��-����U"z|t�s���8p��y{y�>�'.��h 6o�eltU�G�׋m`�V�ys7��(.y �4;n,,�"�|���BaW�㻾���)�t�/��v ߄D˕��څ���P��J�`P+#!�	hwNB�9�a�f4�Y]��\�K�=6��q
(�"�8\���دjp�n"������M ���d�5�f�x���p�"�#�2�4�~>gs�ua͏��Ih���fW'�;�i&�n�&l(ev�q3Ѐ�D��YV���56��+wh�q#ɺ
��Q|[�ۊ�bNs�>��5��cT�ёPM;YoV��(�V��B��H�e���<�%�2�|RW<'XwD4v�!�Vˑ��%#�>�C���ḁZ��e� ]���ŷ�̈́�# ��0�!�V��M�֣I��R��~�j�c�-q3"���f�r���T(�A䆞bzrI@�����خ�f��x��,�,c��WQ�H�}�����C/��[c����}��[��4���SU�rG5?�+Z�-���}�g��ZD�)���,~ ��E+X3v�p2��/���ͬ�p�H��1��dP�鸞o�c�wE�$��<E���򶣗a�t�m_:���}	��׷C�)�a� S�V��|:[�8�)��2hi�j�_z5R���&�:2�Tvx��YEܩ˔��D3 �S��9=��5j�|���vA�Ǧ�vQ5�d��ȬyL�=��UV!bK@Uw4tE"h�D`�u��5���q�]�g�,W.�:g�7��*|�+z8��ݷ���g^�����U��D@�h��-�No�Q�{�vb�3���F2��`���g�G>_ۤ�3R�B?-�s=K�{+u��X��1�lE ��V�u�;N�{]�^�T~�7�'�Պ�!��2|��J�·�e���&_ ^ఒDp��>�k]-�z���ٵ�t�ߴ?�M�j�$�#ǁ�V�(snJd�U��!�R^�I5�s�/G�P�B���h���{�":����J�xP��2߯�iO��vx��>.(Qi�U��`��A~Xa���x.����	}�r�{M?u�n�y���j2z���.�y-{,He���sóK��A��M��I)�s�.|��s�(z&V V-��`�"��`�C��hoX�������}p��Q�K���1���QtoРZ�v-�e�]Ki�F;�����23��D�����=��1�c��M��nY��λ�f�:-�}�?�:( ����եz7z�wĵؔ��	�ۊ�ng���� '*r��<���-��>�@D�
�[��gb�Vy�� �,T�k*���Ad�V	�8�����1�Q���޹�%4yJ�d���?�x��?�ӷ��tZ���D�����ڍ�+L���1�;s�k��q���)|����4�	�s ��hº9wJ����4��4�>�s�\�m��7�>���D��X�嚅�Vms���cXa����N'\�.c&"o�����cEc���w��(U1I<�fGyR-\���z��P���	#H�ⲗ'�=6�G8���T�+,������<��Y����;��A�N��J:�K�G!?A�B���o$�>V>I>f����!~Q��x8u�S�A�8lŗ9��QE��fل�c�i��Z����wv&7%���@����G{�����F��9�I	�{��vS/H�ݰ7��^Pk˓�I�B}Ă�Yp
�O���C�>��I��]�-󗾺'�&��w3�Z+��]߯�!Q����R�C8d�g\'�������EmSCǹ����ͳ�H�)�O�a�VOd�99
u�b~x�,"��e堨q1fo=4��!?HΘw~:�94�P��#H9�C��v�V�(�C��v ��z�	qL]�͛h�A�Q"���P����R󿤟j�6�{(�*C��9���]�K��-���W��C?Lsy?U�b4>���ԅx՟3�����(�����\֏ë3����X����c����~��W-�n�3�ptBDG�%oC �v�����W?<1��s�I%�qX�	:T�ŗ
	�Н_�����%kLY4��l&�r�5��V�m�l�¥��O�7u�5z'jh��`ͺv����; Եk^�iE3@m�>h%	�թ�}��bl,��Ü1ʙd�`G�EK��G�d�:s���3F�s#�����*�C�ָ��( �qZ�������z�� �a%H�,�QZs�Fd,S0Ƞ]e���~g��tY��>��Q��ݏZ��G<�����$�GN3��+@zy�a�ԛ�|�\��/�����k}�
߁�Ô�?m2QM.��J #Qa�7%��C��+�l�Xۿbc�YԸKMs��e�p�B>�*�����gH���P�s(�����]k�-B�UjaQ��Ӕ�u;���������3��/uy!�b�{a�k$(*��]���!��oF�PK��v��&a0A2y�{��f���V��eѱwh'W��0� ������y�^��{a]�Ūm�ط�7ǖ�'��4�͐��7VhH��
��Rw�N��;*8ꖕ����7;q>
�yv���x�D*\$q�Wg��7rZ����[}X����.��OT��{��j5�Y��>��jfOE-�x�H�������&���0H���9�Y�wշ����%�[T�(�A���RC��w�B-�*�q�S^�`�Ѳ��b����M������Do������,�(��O���q.�Q>�1�����*n��w��%Z��0Ҩ�:�ͦ4�ҽ�=�G8L�����U�rH��Z�
7']�Y���b��E"�߳�[��~������kU���5J�{�%Q�O����!ݷ��s�L ���@�]�WV��6�g�y|}�=m+���O��(r�$�qsڐ"<�"���/Al�S�r���ɐ˞����w�?H��I��\�*g���@�M
��O�<�>�\�i@š��F����2�VOE���vkq��H�J���8�r�����7�,��6=��?���FAf�F��d��_7а|rd����M�0�?A��Z��`/^�����c)#�04�^�%j�$#���i-c)�9�ե�����NTC��閰����r�����y�2Z֏_~�/x�7ET��4�4�`9����@(p��)��6{�㜪�����|��2,��i�����$�y�t�� ���!
8	W���s:vp�1!�?�k�S�a�j�d#-sbKD,�{#�	�,��~��\S��::�rz�\�.[����k��
:�K��������mӪ��h9H6�
%�ꃛ��L$�&�H�O�b�1�� �뷦O�    `C�v�fX������l�E�h�@;'|���}@~(g�`3�f�Q���l�Zh�\�7__#���8�cy<�����:�V��84җ$b���:dՉ`��V��]����EO���e]����O�ɚ�o��~g�:��Bi8Є�&T%u�r[�1D������2�E�p`hs{�z,��_���]�_G���|�Й˔����t)y�S� !0���i�c�b�y��#HG�˗ޑ�R�J��p��J
�vc��*�{�?���Q ��"�a2����/��#$���WI���5Y,$��z�Y��lK���!l��DF�����1S����u}1�'����<���eX���b��~%� �0y�~جN�k"^ʩ�Z �z�o�_��긷�9xW��j(�Km��:��9�r�x�����Щ�}�US��4��2e@��U���^>d�^����'���ή:�xp#AS��y���=e�BQ�Z�õ�ڄf�ԃ+��+j�&6Q���,\�0w��i���*��4�O٫��Ɇ�R�_G���͕�j����*�+�-�V�{�~;���L�]e˟d�?i��?���	�-~���#:�S��Eh�����@�����*ok
�&'�w�M)m�fN6%?K�p���k��눘|5�&����4U�� �ӧ7�/��Ӌ?�<���Z[i��6s��|���U&�(<.oj �A��
�N:#��l.���㋻|�����4U䈗Ԏ~�����Ax�7~��e)�[�:!If�L��ld�{��d&��� ;�C+��:V���{Yf`Q$���9��q���p�܈��T��uG sϚ���F?B����c��EP�.�d���(�ɞ�"����.]LU�7���I\��,;��ۨ��+� �.��FY�:27?��Vj(��A�����	�A@��u�`���*�}�Sak�ć��?�j�0�l~7�4�sbD�տ\��Yd�U�{:\��V��_&ؾ�|5���^_+�V�|����hQ00����uT�շ����=���N-/Im�W��E��b�Z�����f2%?e��!^{2W�NU�J>��oe���l���#�� �&����_�wޅ�4f��f�\�o�E���o��B�QWf�^2$�>@Q�x\P�n7s�w��d�W�m�>H=��+7m��:�\�ؒΆ��68�s�n��}ZNt��f:�#�A�ɳ���gS35�fUCz�����76�	x4�5�xS!�-���㚎ْ�E��(?�i1D�I���Xnpd�Z��v�\YKh(@��Q�<{�qȋ���*F$GC��v�"a����L(�0�B�ru��9�o�to3�d�k��6U�����M��J�Q�=�E8�;W��7eC<�'Ju������RMlÍ���O�[�~�[�=�ۇ"� }e(aG�/x>�a��H�-���乲��!t��7�<7ih������o3+`]yܿ�7�+�����;�axZW}~)w�DEuu�0�$DP�Λ�X��������[ud��Y%[	�`2'rw�}SU-]�2��y^���)�
�E�w�(K\��h�ME<̌*v��7O�os��a��g��T5�Hf�U�u��C�0�/�}2:��Y� _�FK��b�~}i�Z{�-��dCߐk��Y����A���T�~�,a�X~4�4Dx\>S)�#���PdLZ�N~�l�AZ�<������[�եΡk��A��Z������v��X�����{�p��%7���P"Ta)L�rV<�9i���n�x�����Ծ��R8����ɠ��[�ג��P+���h�{��;�%��j��ꇺ��$�;'�MZ���X!�2�>e(J����QXX5� aM�$����l-Zh�$�`$�y�4�L�=t�[\�go�8�R豭�0��� 9�Y�O�M�/��������ђ�\e��Q��/���:�S���\�p��=V���>`Q�K6�ۆ>�ˮ��6��.?]Ya�:�O�5����I��r�U<.?j>��({��ɨ�;����~�- �� :��_Y%gjͿQ�����խF�����]-���1|���q<1�鐞e5dRQ߻}�w#w�K�[HL+V��K�H�m��JK[ϭT�(�����^r&��^�lZ�,�A�|Ă�/C�X���n�Oo�h�+I�n�6WM�����͑���Z���S��6�\](�$��`�h�=����	N=jC'A/�>��M��Hv����jI���Ӻw�7̪�����3�r���V�����r���O�����_�Կ�u���%ک�J`v���p�P�(��<�""f�Ʒ_�V��ַ��Lٽ��z6�8r�jP��LL�N��/�?�09�p��r��,��e
^�=ٜ&@t%�IR]nHhF�Lzi�Lq��-�1S��(�H�9��O�r=��;J�-� �|?�7�xX{�f3���nɗvױC�D9]��~�p?������T�ƾ�Cm��Z
ڃ��~�N��H���S�)��!��Jͦ[ Q3~��fR׆�y������w�,F���>�I��F�SPLN�}��Y1� �c��ݣeU��͍K�m��F��ia��x��!�`;�'�K������2'+�}�h�0ЌU���j����W��HýT���pPm���h���ĮA:ڲ$GR�-��-�X�PD�Y�t4ma�^�gZ�`��6��(G�4�'W��z���!ΗWY��^�����`c��|�o��R���M���퐈W�G="*�!فHk ȟӻ�M`�Ɖl��s�+��o|)*�x�Q4l>�ɲ����S���P�T���^�O��-_oF�?��Z*&��E=1�A�!ͼ�\v��|�#Nʒ���訅�d����~[�����'y��M�Rf�jG�K3��A�;$R	{�.�fc�5@�,��`R4���^��/�#�����6&�Q���{�B��o$���V��J�	�o�Ձ��V�(�8�>="��u��q��  <��ik�#G�N�9Q�R�)4�j"����
��LE�G��>*�V�z��Nx"�1�ԣ��//Al���P��4vB�J;q��H����Գ�N�.�yCs)�\�$���۾��/3�m�o�`�{!�؟�C�����Wo �X�Է�Ȏ�ܼ]����� �Fq�(��4�K�v��^5�f[2��!z֮��Nx"��j|���W�|��))�iU����d��j��:A�F�+t����r�2�[����F�r�X��x��J^��$2�}[ĩi�/��݃���NI�}%ݍ��������\UB�R3$�f~��~��ʯuG+1YH����_�z�"�勪I����������R֬k
Z"�7.�u�2��.q��T�h�}�O9�R�BK���E+�۽(��JN�����")�=��7�Ҵ�d���s���TM�)c��
(S�Yv������<��"��1wwPS��v���p���ӗh�ۊ<]B)uܝ�d��I���Y�6�O���1�.9s.	dh�u��6�6d�r��y������o��<��EQ���}�m;}��G��-WVtlW�DQӦ��*7:Ժm7���]E�����,� �\��JJ̛)�G�6AW�/�X*�si�5�0�kCJ�)�oO4y�s"GGH��!�q��3%���E�@ю���Ra�f=]���$��U=�M��[�3�oS���S7�F��h�$4��㻝���y��Hh�P:�R���W���ǆM8�!tR�.ǩ��j�J����x�H�k	H �p����I�^���LĢ�j� ���E^�~��`V�Vl"I���Ouz�r�3�׃w��WQ�V���8���Q��.�#�PFs�k� ~k��I��#��A����f�y�\Y���� w'FͰ|�W�
ĵ�3G�Pe5/ÖX�j��O�uI��c�+��v��2�*���!�V+��pCa���:��=R����=P��8�n��c�[�a�!����t��"�ZP�l���8��[iB    ځ@W�v���-�z��z�v���~OR>����������zjCg}L(����7��K�^� K5��v��4�T@e�ا�q���!*�s�Y����y*��>�CH���aHC�Y2�/�~�+{�b܃@��vC�Y�J����j�^A}��QrN]����,��#oL�D����c�o�!}(r*[c~�I5j�m5�D�+J}�?��Q�b}uQX����r����x�B��������CM;(�Vz!��7�2+j5�)uO᪐ۜ@��z��g:=�m�S�\+�E=�D&F������/�8 伯�P[+�{�-o������.�"����|�(����H#�?���<����x�1�?�81;�I�y&��Қ0�N���M���3q���h�D5n���⚾"aQ��|���������rHQ���D�B]X.ye��,3�J�{%�Z�? C��M�"�ќ�$t�i6��ޣ*[u&�q�{SI���(M�/�1nӰ�
.�Ҡ).m��'�"�=N<ps��`�x����a��UZ�f����LB[�D&�oUx�ه���az����?6I��˘hM���M��]����WV��Ot���!!6���-�DM��*��'����ڙ��lݯ����8�`�7���y���hts��&�EN��1N����鏱N�Z	��T�D"R�L�-�)y:ɔ+h̳К��#||rX���_��Š���Aʸ���'K݈"�>��A^?]U{��oo����T��rZ�]���n��i�#����^}�V�}�(��6���������)+�w�C&y����Ru���e-9�tr�s�CX���8���`�@���bx�>,�3摨v�oΟ|��y3���<�Iq�:W ����%������9l⯖�eѥ��#�-1�Yw#k���z�����Z��%,���*=��rl�4�4.@��{�� (hg��*X�@���?O~�<�沉�gbiG� �L�fN�g�����v&p����@��:RD�mKqD�M�ُ�t��%SBLXue��y�$˃FL�`�pV�ѐ@5[���-�Vu֖�p��ᆎ��a~��+��ٷNg^����>��7{����e\*�0��6svE����JrMeϚ���Yξ�j�.-J~�Vh��E�����P�m�BUE�y�x��z�+�4���rYϣI6[7�>�C#U󶻓��}���������k��#��G~�{�ǰa+Đ7Y�i/���gF=u4�V_�)���V>��u�V��rNu�m=�9!�H��[)\�Xɂ?�?R枚NW)��Z��w�@w��F�-[�>P�Yi޹
����A�z�bk��T�'��=��ĢP���̭�t�l  |�80�4IL3��_� q�Ɋcz*�c�4i�z��>e>�%%��	�e��.�U~Q	�֐��_"p����~�8;�Y��,=gFfd����. ��nd��,Fk/ /��o7k��q�b�Y��V�S�tƢ������R���}�����d�M3_��;��`O�G��	��s"�ө@l���tE�ѿi��`��_���z�=��E�Q��а�k�9��1�7Ik(�݀Z��z����u!E� �2<r!���T4� ���2��:����N�t�B��Y)�k�Ɏ��FޮΖ���b~l^�9�*a���^̜�l���r�Hz�Q��ϼ��Є�V��cz~�qw����T�/�>&���V/lc��Y���c���'ghwjGeo����*�b#�lz�ӥ/,���62�&����R��Y�):�{���
O��rz|ZA��ӧG
��wf�>��AeQ�DѲ��ޭ�t�k���YO��LKX��r�o���%��Թ����ьڥ'6�y��ͯ���q.?��m�5�����+<;�Qm���Ð�U��~���ޗ�\F��ik�~�i�I%,���ԭ��s�3A�I*4�����4��f����h_p�a�����`6\�{���=B<g�T2xٿ�ɞ��	�p���m�d3[�H%]�#�g�r`�0��^��q��!�?Y|��Lǽ�.i���BMCi�xF�@Nؚ5r�i�)�f�����s����BV)�5֬�̉����F&}����8R#�¢J��!}�����)�T[��$7��.m�iř:6���xk5C:Cj�sK���U���h;90��e�O5g����3"5)�~̀u�4��m-��-��^���ї~�|���`R; T��9%�:� p�����ֳpu{�+�=0�	���^Ae��/�l�W/���o#ƃ�/�<:����G<�x�u���Ԃ4������0��uFQ�^�N0�\�D��6 q����5�^x��.67TQ*sDޡn�/�l�L
{��k�~cR���#&�8��*j�a<�t�.���L�DY񷉑�*.
�s�y��j������ך�9]1Zxx��[k#Du���SD�BG�x@�
��IH�7_ZV����ߌCî� �N����̀���;w���js�����L�?B ����b���܀*��p0����n��d>�t�y����NK(X�/�Sw.��#�T�w�А�t�L�ݦ >�UWddV|�r�9�ɆpP@�lfy?�e��J���	�:u�2�,��>G��:~�'���)�ʭ�_H�y���I�7A��c�Se1]Ļ��YO%T���0��&�h\*��([MΏ��K�q�����Hrn��AE�E��9[�!�P�/�3$2�臼���-��M�b]�RD<��K�H"6��;�Y�	8TƂܵ��T��i���ޡ����mU:c7��p��y*B���~sn�+�5:8��zm��m4o�<?p�����#����)|��`��\��⁅1��+�j�{Fa�~$���u�,�Z�N��9���ߤvǂ)͝@���C�4Q�&�U!J�W�G���m�t\0��sZ��0����ԗZ��
#,��g�"i,O�ҫ�@�5��@gD�\����o�&�	Y�<���T���:�+b��K�F��O��#]�ޒ�\��'��'�Db�=u��,A�}OF�W��N�J���vbR�'���"j�b����D�`���3~�*���3��6�r��~�a�3_E�YI$ں����L\Ai1O��z�6��]^Ky��hA�Q�,���,����������d尧�h���<�	`/�6��3��-C�y�[�Б��9�H\/�q��hU��m���?ΫZ��?�e�~��g�Ú;<W��j�|Ĉ�~W�{66:w����b�w~��[�	'8F�.�n8�S��&�7
�E	c�R�i�o���ǝ�4c�
2�ʛ�B/8�o�"`_���Ηz��U�������;�D_����ʓh2)e������C�pZ�$�D�K,�V��N�ѝ��!�o�-�E�t%s­<{��$��]�z[Z=Z���v�s�wM.�犬)��!}����cr0�5��};2����j����ô�?JA�R����/����� bpa������V��~�~*h��j�.���1_�-\�D�5�C<��hT׆�>+;���"� &S�<�����}�*�PE�D՝���'[#Fp^���~��b83�a����6X�ʖ��DAjy��s[{5��@�Kc���K��vh��eK�ʢ0��e�RYw5�K��3B���J����e#1X��w��/�������_��-v-�}�eA!FNɻ�Z˜���{����X������d�����\-�ds9nz-���gjp�@����A�.{�� N�y���1 ��u+�Mm�ʷ��o���Z/�e��R;�^���X�c-٘��{2Bj�����S�*.R{;�%l(Hþ�~y��V�ɍ`�A���1���p�R1��,|_*$=o��b�`�-z������,%5⡔d�u$[n�d��;m=�:mw`�����IsJ�?�%m.M!� 7�[���k
�����J��}`���׉�ڥo3z$_��+~<<��n�D�"    �L �瓂�ɪ͍_�x�2��G�a���S�7_9ln	���Xg����bn�<|l��"�%�8ݞ2�L]�,��pN�p�F�c���cn��Ï�@�*�з��?(i�!MsNNq��r;�^vx���ř�I ��{&������8��2I�o�yޗ���Dl��^21[�ۥ+�&ߥz�I�ڮ�i⧹�� )�ܾUH���oƦ�;9,S��32
J��]��L��$��3�J��7�55���ĭ�~f�Y� ���(s��Z_6�u7;{�ԧ��A>Z�;4�={b��~��g&�H��N�l�I@�E~�t8������Sθ��	.Y�M���ψ�>C���� �� }�]�$`����NL�L_'��|�ZOnX�%q�qϗ��"�F�׽F�pKM������g�^y�����߀B ���V���3���nȑT��b�����w���?�y�!��3��]!�\-��4�>�3�\_K�F�������J#z���L��l;�ˬ>����s��lo*W���,�k4';OD�ʥ�{���|k��\Z�F�p΄��3�P������Q�L�Q�{�ۋ�3u��y{~s�R���h[���v\,'X~�Y�:x{xU������C�a���ť'��fpi��&t�IE������b:��;�)��X��5��QJ��*Rv�)��⦿Ĩ��⦠ؓQj��Q?&�N�w�L�Fz�$G򎁷0�X�m�����Z��KE��;���,���e��Y��s�q�pU$���G �o�1R�j`zx�,k�F�
�ثr�����~}��)ǖ���K��^Ċ����~�`����* ����HV mw�X���W	k�{���r�L��I�>ٷ�2��H���sp��A���X��Rǯ''��+��i(��I�O}��F>ٟ ��~U�}D@7�� Tq��ߦ�[�{���&d�"-K���V��K��fW�r�(Hdz"��CB��q�XUU��3�l��z(�ہgpM[2*����wb������5q2/j�X��a�U�X����[��/xd� w�mǵ��ֻ=�C�W���/e���Y-Ng���>qt|o
�o��ف�]�s*]T�uӜ����o/Q�e{#�ݩ��PWz�R�߱U[�ͥma:/LS�@�ߒ�0��U�ܾ��~U�e�����.N"�o�����@���4`:�J~~� Ʌ����x��&Y�� ����G2��Tp��2���O�	���e�/�ϙ�(���0�E[̤�k��e�SG�ǭ8 ���bg��B{�}>�EN�+��K���sф�p׃����
�4��8 ���=�
!�ؼ�?���-�C�F�e�?|���H�J6�m�����8o\Pӟ�d��N��aE�r +Ϧ�3?T�m�h!,�i�7��K�ߖ���h.P.dym��\�Q��q�ɭa��G�3_ρ�r��l���*j.���n�D����ORg�h�����P����Dd��ӽ<�^+�鏕&s�ϙNP@1�&����i��d~a�@[oK��(�{�yؾ���=$n�n1K,1�k�6@'=�°��`�6�hm�M��KO'R�vQl�a���=e�-o�3l_Hp|pw��~~�E��].Ѷut���z����u�P"��Ɲ��]�"��ί��Př�ĆZ�:��%n��NI�e��_%̢�}:���3����e��2Sg��z�\�J�%a�*[c�DX5Q�2���o3��4o�n��?\��	h���c�7zC�m�#��F^�7XP���o,LQ��?�N<�C�>!��8����O��V�}�W|͝�2�5YrdgqFa&"K֨�=q��I�8�Wg+�\=�o��`R�~Y�s�tF���9�(i}��b���^$x�*i����h��b�ooؚ�3�s�L&�x'��VΘ�@�����}>#���[����\���٠�Ƕ�!u�x]D���C�-�$����g�L�k��d��T]�>"��K���?��`�Y����3��9��@�3G���W���ށ�o�a�CV��C���闉��dy���Y��ꛟ�i�ۋJ�jd:��o�+���5c刕2SK6���҉�/���?��<a)Y/��Wh�Q��L�����nc�e�J�~l����N@e�kg�G(�.'�Fg��/��V�y.��O�.��'�r$-�!��Y+r@�Ź��j��oՎi6��<B�C�Y��)�
 u�_�	KE���渙�y���7pq\7.Li�:�G˗L������Jj��{~ �_�X��=GZ�B>Cc(���ć�x"w�J��ó<� �m�^��l�t�5b�i�۠�U�O'�B���Z<K{��6�������̋��~+v�3	�n}�����믶 ���H$��m�5�f��H�E �kv��;R�<>4�r�ޞ_Tc��${��^�4vw[����SC�J�؟�Y��Sv�KD�4m?����}f���ʫJy��>QsA[���o-�đ��	�v�<s�H��/��7h~���g���?�S{8�B����`v���㱬x�y���<+dޘ�M~8���~k\_�=�ҜC�w�AVP��1}V��]� ��U����������7��	��jE~�|��*���҃y��<V�r�B�}���L�o M8����|��sӽ��t��d�?�����vg�ݦσ�B���_��J��o��e��p�L�9����t��/����7�9S5��H��^}r8��but�D��+~h�P�e"���=�:�;�.^�PoV���_��(�s��n�����@��ֆ�m5�xl�|����`�L�r����f��֟�����*twΗ�穩[vل!�>#��~:���g鏔9��O�*C�w���>@Y��:D�}�&�)i�!\AY��/%�Џ3��#:����vNBЇj	Rq�۩���&zK�Ⴟ�Bz�ٖ�h"��}S�V��,������E~���_NT�ձ�́?E.�@A�|��Xa����9iS�����v�k۳��ֽ�U32i�˲�nк�]��;"}Y��;y�y)Ię�n�~�(������E�m�#�o��u��,�W�d����e7v���Mz��79�a��1T�:���?�V�_Y�a�Bۋlq}�~�O5�]
OY�0I�S��,c)���>�F��য়?�����l�̶�s��|��>a���I��/��Yd��:Hڒ(�$T���D��N^�+����� ��\0d�JKG+`ȇ��d��@�����= �*Snѯa����c�A��9��ȅ2���b8fv�[�5�#�� ���8��n��2�����s��������՞����`��=��W�5�]�M��r32f��k��㸩��i5�t*��d��Hh`��S�PoUd��e���(�W�	vZ��pouz�j��0H%D�M9���rKe�Ҵ%<��^�
׀����I�Y�0].�ޓ �h!F//������ �ѽG�:�%���e��@��ދ%�˴�F�4"o�._xs�O��<�x�@����ϻ�c��{��7�'ݿv���!m[3P�F䷤�Ź���9�X��u^�W?(
׆>�+�F��'��(�p���&E�۸�;[:qި򂲑M�վ�9 /�P�	��g��}a&Z3oiA��� �S謣dTx�O&N4W�� $����+�e2��.3�$8z����Ǽ�v�,/�3U�ܿm��z�_K��Q�>�7"m�򖩞��$'��,Q���*e㖦y_T䶴u��x�!q���>$��A���
�iHg�.=G� }��H1b��B�W��jSKՋ�2\���}�&��
�%4�����c�[ ���1�aN���E�]%/�B����X�8����-�9�؂��7xס�5YFN�XS�X-�^���[�x''��+��ƫ�R�K��v�K�W�Uj�R��S����r�p){�w<�Hןh{F��4��=�;�J
���ۿ_��c-��+.���ad��trAw�����_6�W֜:�NH�M���#$��`�-写t�u    ~0����W�;ʖ�'/��z.BM4���'{���wЅ�a"����W7~iR�p4�w��A��oj(߸8J�r.�s�PX�-�gH��o;���g�(<��3��Β����abM�~'T����	f$�X9����I�n&F�ɑ���+zl�i!T�m��i-�K ,��2R�4�j���rT�=&���kebϠ2����V�e�.��n�� ���Z6�c1����t�z]��c�JH��7�c����rBUe�xj��Y��f1�� �hﾺE�$h���aWdg|�P�eV�F곹>�P���g���L>"�����V�嚉���/��	�2H;eҽpL����q�����&�Ά*��jO�RJ�+S��?$�(���=�M��G���]��0@�I�z���"kK��.�
�����9̲9̗Vyh�&kw��
M���1S-�\Ԁ��GӞ��d�-�x5����v���I-���x-��+�q�pZn���V~zeæ�������;K�.VX�,�]�"�����ג�Q�ɸ�)�6�F �P��6���u:��T{j�kz�}��G�����T���S�ܭV�c�p�ȩW��{P��zL�DQx��q��8���@w,5�O1.8�(XɗQ���T7�o/,?�'�tm��T���(;M�h�J����/�hʹ_��R�K�c��=��x�!�S?�j���Gʻ���JQ����L�ƴ�R����k�����Ŀu0���*�-T �*�&"p%������ *��]�BVN�����cFBY�a;�#�?�N0�a��O���^0\} ����-,Mn!{�uKK%��(:�%�(�~�aI�9�#I����MՔ����;G��ɨ��Y!#�0��ŏ��uH`uKt�\�d�vE��П�)��h�kU��P�ﻭ��9�p��J��8!�?�@�Y5}�Ӫ��0mu�y�C|?��R��?z͓�mgZ����5
�
�F�p8�#9�&M��enoS���T_�����P ��^���eܫ7˥���޿��'D}�.��߳�C��g%	~���,�>)�%���W����`3��3B�+�� ��M8~�Q���]z�!��qF��ŗ�o�s=�Ӓ�I<r���R��X\z�`<�e�s�������o�~�Άi�7jRb�lx�:��tf��=�^���SE�0DI1ec�ޔ�G!IW>��H���|���,�z+�����|9�_v���U�mN��N�@G ��^����7�M����:ʮ�o��{�:	�R��`e��e�F+5���_s����-g�a5k��oc����v��Vϖli���)����ĢNts��j�LH��(�$-9(τp�QR��yTs�R����8�����ɻ�P*�SR���^فv,��i�vjS�O+41)���75������N"JlH)�Q,��#\�U����=t~�eh�+�J#�u���8utb.9���$X�ڋp�L�U����e�X=v��i3a���mo� ��y]��'_�c7��4乡2�� ���S���o��/�}�e����d���1�iK
�Z���?
��l�Z؁�Q�Ij�3M���a��d��^��%q=-�n��Y����,��~�����
��УLE`8��$��wh�7�W��v3AW��HR�˭��E��*��6?���;�e��p�!�%�(�	�Lh~<��zCDMj�^��ZיugX� ��]�\,��sc���֞��@��� Aȡz��W0�Z�s��5#�q�b,�A�3�|�I��.�����uà��vO��y{4aA>|'i���(F5K��Ȇ#LZ4�#���g�g��y��N9��p��o�2;w��]�́)y�c�c�t�W�`��#�ơ�+`���B�e�j/5��Vk�����W��F�ꂝ������4]�|�ہm�q�+�5]��;�E��ke$�,X	W&�P�
�r`�M�_���I>�=�x�A�C-����aub.���/�Z��^�H嚥A��_��w�Ss-����,��)ٝ���4?m�%�(��l�aM�ߋ>a\j�����x��p��n��ܲM�R��p#K�8|I��a8q������,i?�/*���������"S��9o�.��+;�бNԿ]T��A���4*�֥��)�S��Y����o��P� v�r�#��4�$Í½/��5��
F����������^�^�b�#�a���#L	-R>p�Vf�V�7	�D�]�*�-#X�J05ޫm_-am{>����c[�ڧ�&��`'�Y!~!�%�Q�	��X�6����o=���Fcw�e�D�"EL�^3��A�Lb.ǟ���f����!��;�F�j#{c��tx�T�A�F��k�B_ȣ'w�t����O����c'؂_���݌���]�gG��Q�����z)a2S��zM[w|0��4o|C�k��ylJ�y�]R����Nv��2�0Z��`��c���������P�ю�d���~-�	�Į_>�C�C�/�������)�z��h������=��JM���4꥾2k�6��[~ށ+�
P�����ݷe����Z(�7J��j1�=���r��N�I��
�xݚ$tUi00R4�[`����\!�+}X���%{AU����2����h�'�k�m�b��[��6�b'Hi6ai%�_T٠6�yr��#*�4Qa�V��aR�S�pn��뤪g$q���TM�V�9��g򣲯�/�"�H��fn�M�L{+-H{� ]S,��._��H�(�v��Zsx�E���r��h�T�#���1�1�Z�5�^j���d�<|4�O�Q�&��/g��4�*_,K!\�������J��R��d� �j�����ĳp���o���*
5�q�Q�K�Æ&��w7�m<u��daߢ1�e�*]�lf�t͝�1��kK��&�&�)�LA[�2ȔmH��$~�J�k|[�����gcwO͋b��PIVߵ	7�_����{m�@d��x���%�n�Ӎ�1��������'.*[�m8�\�2[�"�h�~�L���}�����[, ��q3���#�S��+
����\���g�pNY˪�/�Ev�=��J��<��<߮�f���ib��I����G��Wb�U������\����X�K��I�&��{Ja���(�L��0��H�d�n��+�ee,ı�~�E�0�g֚B����ƌ����(�K�^R���5�X�^��-0��[�U+�kZy�����|�@oC^f�=x!W����d۾�4�(A�ڰxRɉ	h������e�\u��2-����o:���{��(0��/�g�D�/�^7�障�'�����to(��$����G��	�=\�R�/_!⢮�j}�&
~I0��P�Lۡ*�i�hPT_+ ��PU��%7!f�崖��
�`UZ`�P�/���H��B��N�'X�q�>��Yl��T��a|%6�M?�ɼS����y��/�[���a�&�%t|9��ks̍�[�.�6yk�7~�o����`��v�Ay��	�X�{.Lu�NI3��TN��#�0;�w�Y��^����@`Q�ReH��V�I�s§�_�?��l�O�i@lJ�w<�5
P����c'� �Y.�1�a�TS��Ξ�e�:�j���+����M ���9
�"��p��RDV�!���&��Q�G�s�o�u��:��2ן��+%�;��o�|2}�N������WA��3Θ7Be1ˉ�IK���s�
t&'5���b��;�v�:&�;�v o�iCϹrh��8�u���f�x��B���������#��o^����WN�"��#�}��������d(L�|��J�Z�\��,�Z�k A� v
TA��f*]}Џ&�'I6�ER!_��V|���t�����3M��Z�W�F��j,zl��ʄ��i����	���)w�*���I{����1ð�F��.J�S]�8�+gi��:+�-Fr�P.4	��P�5�:���x��'ͻ�{�-�|���    ̫��~x�� >���u�9;L��� ]��Jy9;�bt2ȳ���j�Ϳ�o(w�k&J��F�O|v>r�尛D��<Q�7��K�o?��w�$*AN ��
��~����~w҅%�� EPQ\~�wu�zD��)�%42K�G ���&F,�A��.�9�Y��S�������6�d�(�u����~*mF�j?��I���z/�i	\b[��F�5
�l�j���z�x�k�c_�/D������v�&X5��1ק୆�y�O*}�+��ō��,���v�& s�B<��>���K�W�B�HNyk�R����3�K���v-C�`��Yf���g��(����7��g��i��|��Z�.㯂��S�Z��Չ������5�.��§)ؚ�5oTO*L1s���Iڪ\"�X$�i�f��9�55Q�� �W}�7e"i��Mc(�F�����I��29�k��G�7�cU �qѾ�(��!�~U�Ƈ�� �t��0�(|YlׅV�N ��i�������@Ċ��8��#����5�g>����4��
���!,�A*�,��1�Y�WM(z��f�{���j,JRP+�z3Ay]~���v�C#���CG�bp]OL �<Ձ`���E�-�שՖ󷾾��P�k,%��g�a���M����S�S_���b?ah�J=9��dK��$��w�o�#Q�Z����A�ܚ�l��*B�ؾ}G!���"�A����i:	 �t_���#u���ҳQ���G�4MͭaٵyX��Ц��`�u�n��Bh�Ø�����\k�����F�	��Y��d����R2�L��mn�����Ǒ��lH���mTa���L��ˣI�3{��񎨘�/S�����>�$9@Q��M���o���S�_e�zs#�C��xt|f�1�W�P�ޙ��'�:�]?W��L(�flʝK$=q���+]��^>^��=�e>��g�%�{�c��(��L�$lB��,�L
vU,�6�4��a����3g#��a�̸�ALr:YЎ�1��C]Q��9]R�c����� @*�4_�+�q�Q� �~�p/z��=�"lc.�������7�/��6Z�55-�iK��A&Gj�FW.&��gx���3;�f��m�o�d�S�UR<�KA�x�}~��S���ոcE@�︨�����8��&Ȭk���bJ���~o_$��B����4�=ˌ(����6��O��4��4�RN���$��ԯ��e�e��IvL���deKw%;g�&�SX ��1�<�h���d|G7ay�0,�������1�}p/��V��{�/ڹ�D��n X�w+�E�>��F^,����a����Z������I�/�}d�-7�,�_���i�AV���\�Fz�i8���zH�mW���l����`�_nT�=���{fN�NŹ=u���}+�.�]��:�s�3@aRC��.0�94�s�HOb~������
Yܽf=��E�S�m�^Iǚ�T2G(�� ��O�&�\Oy묩��\!��L@�m�㍎���pB�|D������7�1ԔO�%�"���	J�f�����]HۇC��l���W]����l�{��k:?����i���x7S[��~�5Ww"`)nJ��>�]�a�ܬ)��r�ڨ���w�na�����z�t�\���|��e�[0��&�9��6;�48VK
���`5y���%�wm#�#N��NUn��ˬX���FWZ7��	!+�baܬO��Q}�ʧO�ZF�fMI{W��3�;�KT�x�iY��_&K�v�P�i���&+g(�7�>��U߻wM����-��L�ǲp8�Ke�k���ڇ�y�Ԍgx���>^���t/���}����˚&;�R/Y���Tc�
�����,ݖ��"���nyM�L��㖁
��^��HM�O�^[ $g���,rpX}� �~��8��5N�{X΀�8��t\̈́ �_Xh�k'{b� �ΗlcA�U�&xHN�Xȫǵ��,�����c���`&+�S��YU�#�c.m�a�i#��Q���!���΍��5�*��sZ�6�~�S�+�~�ܩ�r.��q0(�#�����ߡ+2-��va��?6��|93Ub���۴gP�%�/.S��x,K3�Kc�IX�O�h� �k��0��3[G)b���"U��I�,?�B�~����R�Fn������p?�����pe/C�(�T9~`ݾ�U�|]ҿ�>�v�4�k��s}J�Lޘ}�pA�Iu�.��Y�R�o��	��+�BG�&�A���
?\ C������Ո��@hw�E���/�~-N_̮{�#�[�����^�W�+�L~���C���a�>&�-n� x�7\<��g������IQ��{�el8�9�"��V��� ^WD�oZgF����Y,�IR�/q�R��xG5����Ԫ���|����ȑw��7ρ�<�#���[��2�MsZ99�<�a�t�����w4���إ�!������b�e%�ǚ����8P0ӕ釚��S�X�,�Ϭ�R7�� KD��o
W���r�.���~�u�Z�7���P�ا�7&�?���~���aAܓs�����r�[�2����m4v3���'po��M��b>ʛ��n�����=X�#��Aǉ��r���mZ�>���+!��)��)?`�-�;�*TA(�������J��%͓O�/���m:Λ1sɱ0 Ԙ�0���goYH����>"y��������J������3�ꔴV~ɰ����[/�b�O�{D0���*�ڊ\|��ًW���J��b�|C���b��f־��C8F]B;G�d�9�V['�!o���ȯ|���޶����#/<�
�2�ڈ����em�Z�)��IRj�>��
ͫVS?�G��'	M�2�#W��/�n�!I���Tk��dL�S2nņ��o �BGS�ڍ�2�5[�#-�oXr�}ɜI*�ڠ��&�.�0*��?����c�h/"xʝʧ��T����_CMڍ����2q\Z�x��BS��{ӊ��-hBW4����F���.0�ݾ�@Q�?���	������ZZ��n�*�lLF��; ?}\m\��)��<�F�Ҟ7vMŅ/ż`~��X���;�=vV����<2I��^A4���s)�ȯf�3� ��z���7��m����IԠۈ�c�:R04���r�ՒQ��_�!�w]����@;�6��MX��p�8um;�"�VQyH��Vy����=�!�x!�[�V�Z�E ��bX���2&Q����{6�0�}��]u�E'�������rg�Ӱ3����刅5�fǧ�=�'��)7�Q���������N��ݭ9t����B|�9�A�IT�K9�����9
P���f�Ae����iR�5�5	yl��[��<%�4�b�%�4W]�nW��u*�l*J�L�uT�	���IGëm�{��51<O#"N��;�w͔r^D�O�-a�B���0��K�Uz���]�)�wެ��(/V�W�5�f���^�cm��S��"`��[s6�z�#�r0�uƦ�n��(m� E�p�M�h���y��z���E��^҂��X������/��:�' ��5?�(e�5Ůd
� ����M�4����6�s�r�ˮ��6��WT��\|G��C��IhE��f��y�8-�8}rfH��g�ECquޝ/��'Hm���MP�!Tm�|��VA@f?u �Á����}���o=�尓@���y�����C�W�H2C�6��'� �c��߮Ke�;�� A��l;�iz�L��'�~�͕�qП��>�V�Z1
��~%I��$�N9<�:o����R�rm$�7ɟ�V' Ê�P]m�5  ��Y���a�^0�˗��`q$�ϹP��4�)�%�r"����:�[N��y;����8 ����(<�rث��H�ͻVI��e����H��!��pә'HfqF���
3a����Њ)�+<��7a	p0йV���HrZC�}�,�@����1��f>P_�7�    �F�6�o�ȶ���j���!�9@p��pgF������N�H��p_80��4����� �+Ya\f1Z��(�)�o�Ju� ���ۃ/�)�('��"��&���N��G\��'���R�DmI��ܓFk�B�>��S��K�@��N���1H_��@�����I�L"����Y�I����DaG��_������w.w��[sNд� A�Y��ǀVh�D)�;;�Πo@/��2�1_A�h�!���#��7��\��x9���T;�����[��n=Cם)�a���cY��� ��׌.vn��zKD=DKۉʂ����!�}z��/!�"އ�H}�{\GZ&/	���k���8����I�4 ��+{+`�\,���>"t��IV$�(�B7�����Ba!��c�vl�I���?���������"s�����x&l��
̣���G�y����4J�|�v�쏐~���`R6�^c�[9n��gr����փ.7���W�D���Ҝg�����F��197E�Ȫ�&$=��{p�ɋ!Ɏsɽ^:��ݨ9�kI|RT[����mgR�I��+M��8���Ǵ�P�"���ԇ'1?{���}��lbe~9��� �!�gW\����D�x�Yf���ӷ�_"�x�B�*���MX�X?�P������K�ϋ�(�R�\�5*.Y�.р���v����{�g��#@&��GSs�P�ϻT�K;v�5# ��Jl� ��=�p��u�l�"V:)B\l��y�[�N�5�>��Մ~�i&�;!�c�vXbU�Q�&�T���a:õ������RJI��HS�b��s�2||�	��LJѷ^g��a9$c�E���".6������ǻǤ|.g�&35�TX�Uq��R�!��Y4���v��]��<�4&�ȏi3�����%� ��͚�+���΁�;o.�>��)�ۿ���ƚ�i-Jy*8Rq|`��G�"���?;�;f�=#O^����9P�1�.]��b-�I�s�o]�͛�u��:5-�MyM�������^�+�#�<^Mk����υG���n+hqg9Q�t�H��yٰoI;��t4m�7T�,���қ�င& je�D�z����J��zV^$�o��dL��,�-���s���������2��#�O���Vy?��<y�u����l[��2��79o�4-ǉAE5���|VZ���*d�qB�<f��)�(�@�F��ǲ�cX+%瑼@��}r���^��`i�q<��?�aTU��ߪ�����o���
�x|��ƪc����Ur�g֕<w݀�񘐯:��ท�{�Q��p��T>�bW�~�f�>81��M�^��T�l�9",��?�e'%��U�+���D�ȇ.�K���S��u=i����)P����v��R�)���,)�dqO1�(je�lA^�:uc�w�S�h�_qX�@��'t�q����P��1�sP%@l�^KO� Gۓ=�2[���;k7%�;��L0�;�M���H;�.Y��y˓z��Ԝ�w�i��I~�s(|5��b��u�R��rqb���ŗ�"�s��.{�%�UO�gD�!}ﳈ�W�Ҝ�(\��ؠ���2�V��/[2ݔg����|K���B�+PP<RA�:�g���Iz�TC})5:������r��eߩ��v�AW������/3@M�)��[J�9m�b*E�=��A5����/��4�\n-`2�c�C����R����rG���^�*�g@"�:Q_M�ъ��3�rx���T8����{:�>��41X~v]�F��L�pE�OM�bq�Lh,	���ݹT��`���?�M�J)D��D-�>f��T���3c���Y�}�/R���ꤙ��yZ�������b3����n%��V���G�@1C�)�d��~j��<���͟�Lk-M��w% �y��wH���>�fz��I�lv�� ���J{P���W }B����J~Փ"w�{`!X��ذ��CE8�Z|�_ڭN!njvnD���F ����k��(����i��]�a�=�p�vH��$�`��U(W���Ę��&�4�
&* ���L��H��oC͊���c?�#���7/y`�ؿ�cv̼U�� o��N����#q
��n�6?J���)�)�"i�)�I����,>���?��v<9�!�Vң�I�����{�Kj��~(�:����2`�2��\�S��-7�i�nȃ�'���%t�i퐪�j�ϥ��u����TK�KM���o�Z�ڥ2V�㍆1���#�x� �y!)@��Sq��M!]kz����`&�㹓7������jZE�4ld���5��sR�ٍ��D�N���Q�����m��x��>7y������"�9z�0&��E.��j$z������h��?��dh��`I;!vd�i�z�}j�b�{:�5��Q�IZk=$uZ?�lj�Q`���튋���٢���x��l>��{# ~r�|<�%$����|Q�!��,��. ٸ�j>�b�'�b��$%����������g�;a�ķ���t��V��\2A��fdѬO�t�|?�L*e��� �IŌ�g[�7�ip�?��Q����`ܹн����a�U�E)�Yh�H�ų��;ب��H����)�e��/M�0�/:�����H��`u������7���{�70-�D��PA31�
ѯمx�mQ���@G���k��¾�pr�{���S�v�2�|JU'A3�݉����>v�'���
Õ!Ք�(4������s�6!rJ����ǘ?�i�7]�eLyE�x�z�T��Y,"?��x��@4��[&؄����B3�a�Z+iZn*��G�H�sƇ�#l�N��r�biA�8��n����X	++��+�)���G���{v]{y��z�>�Aa������ؘ���C)r�6k� 5��#'l|�Z���A>��m�i�|Mn��˹!���>S/${�P�:�D&{:m~(��+�3�c~��x:!��
��=��f^�)v�(SF?	��MH,0r�Yzv?�
���n;��M|S,s�~�����ə7�1�78g'%$sf�k����꧴��$CQ�pY�E]�L�D�Г�5jG���@�y����7^����a��������{�L|=��>�%xr���B*��nE�A���C5�?p	��OR)����g�jG�>���FMM`�Em7��Tn�z+)�T��X����/M�~���l�x��#��gGa���1mTzi����Wn����SF��m҇��uvX��6��5��"z�
@($�I�30�3��>�l�Q�rX�	T �z���i1�%+�M�?^�ݏ$o��z:ĳT/z�>�˃6�6,mb��1)*))-���gc�G c�A�t*�_5P��@�����G��3B�;���ZH��$����3�a�?g��E2��B��K�,�lS~�ֹS�<w|���bF�z��s��g��B�RF�\���`AF،��\)aZ=�>�c��-��7^QIB�X�n�14�g(p�.m,��^���e>��L�/��φ��y{}3���&8t"|�Q#�7[�P\����GF�8e���s���od�(��%p
?ye�w�����˥� ���{��+���-������09���zg�D-�EG�$������r؜=y��
 "�9�����s��;i�;IGy"�!?ו�.��Hc�$�\���<��Z?�oMNTr��|6�-�J�����ļ*�t� �E1/�>P(�����n��NMP��-�q��V�����tׁ n�/����>E�w��NSj�ۼ m���Qcm�M;����%�	]~�嫣��?k�WЅ�\]�}�gif62������s>V�Y=4��#	��1f�����S�>d���^B�w���Ce C��؝��hd�f%1ц��Ț�Q4{���m @W�&�&�.�}�f����\�+_��&��f���a%*/"��P���》{������A%."o������3ƚvbm+y�#�lW����@���/���x�ؘy�    o<:49����TOX1��p��y*���Ų�V�-0�5�D<�Ҥ .b����1��X��5�zv?�{mCu��yIDM�&x'o�zZ$���v�|�����O����o�>�����o��naL�Ǳ��ME ��~�f�nd�z��g��Rc,�]�)h�� T��K�{��~^sBN��6���e�6n)�'?�l��q�Ռ�C�p룻�FC|���{H�v�b-�Јscm[�K��t0n�I�<���щ]l��jǷ��Rg�/ĶQ���}�!�(&��^F�4\x��X��3T��49e��٦m� �9��7�^"2M"�oMR�����{�2'[9�xN%F>�L/�'+3��:o��~	��Bn�`��F���_��yЁ�t��ȉ��Ѡ�E����ؕ�P�ZILP[�(�����U��x
3]�=�^$�@�j�p����@_l$^?�ω];Z����xH���?���� � ��X?�M}#���8����u�Ǹ:��CVAQ�]+����u �&���P�y�=%�|'f5u��CD�Y
M�L����\��|��yD�9b;XH��SLZ����v����(�j��
כ��I� ��n����m��I��`���oj�n�^�|�^0�pZ �"#b~�vh'(f^UZ�n�w|��ĵ�1�C@��F�H�}Mi�ݺ�p�Fr��3�3�<>ė�m
�O�7�u�\��sW����!i'u����Y�����)� ��+��y�ۤo��q��e�˱#��So�r�S#+�]����V�N�8!�Ǳ%�jD=�<�nS/����!a�	|�z@Խbܺ��S׽��H����hٛ8d?� RLK�3����ށ��^�j"�f!�}Ƃ��1��Q+l��gԢ��(v�=����zxN�pu�f���<���@CDǘ�d�+�' �z���!�r6_��e�gXBV�]���"ټ��@�t������q�緜v�0�`�g�Wt�o!Ϋ[���}�%�ǩZ�a�y��=q��9p�+xW�C��	6%v��#���
ż�Z3@+XE;(}�D3�0��y(�[E��H��bo85dل��S�N���ug����x�K��qp뼋�8f��<�̪�A��	h�Av�3Jz@�i�$#'��h;�s�\6�l}z�Zp r�,��}��;XG�<�K��dT0�\b����]��&�Usa}�@��&ДW/����\5�4�d�Mw��R`�<�^M�"�T	=���!s^��X��vr7��=��dj���J;��g�;�!g�s�Ǫ6RAM�Sy"�zm=N�@�p�V:J�U��^��^U�$F�
8�Cwi9{�3l.o���X����G���2��F�K_Qc|�%R�d)q1����E(�X��|<��F��a��?��"�a���"fX���;3S�>}ݙn�i�X��oZK��x�,S�����O��u)2��a��@�I����*�gQ�p5���6���[�]��?��+Q�i�r�-��&vR�\k�_�� �o$������ QE�/�풺������)���z�>���z�����@�[����|��?��ٵ'�#�Rgf�� B�qG�0T���194b�[�$�4�X�A�wqO�S�P� �!?NJ�fr�v'�V0ݨq�9�c�Ķ)��i5���^�4
٦g�A��|H��J��|�j݇D7��?ՖcA�['/��k����
��� �����m��������2�u�X���]�rJ�Xܧ�M�Yf�2�Z���/7�t���Y��)߂(΍6�Q�����Ʉ٥NgU�G�Us�.Ǥ%w8g�@�~Q�p��< 3qS�����9ա��q���V�-�}�|d��|T$ٽ�	]@�V��V~g+��k�(��\bE�CT6�ܼb,U�^;ݚ�M�axCy:�X�Y�_U�'�x����9��F��#��53�u�j���Ӽĳ�!��SKo2e֧��A����l�f�}�k�X�ڶ(6�H�_�P�L2���i7��RW8]﵌���n���]�]�d?�Y�?��ݾ���SI{����د�e��7A��,	�;��=��d�)M%�r�~#����t���J���}-�4Ƥ��dͪ޲�WɃ�T �^F��:������*3�-��n$�)
Q�"��~�a�a��0���C�*ϛ�1�r����~�>�^q�͹�zry��	j�F���T��'\~����yO���
*H��
��t�K���E\�s��̜���*	�R��xݸ�MH/�����G��m<U��-xD����~��>XL$>���R�*&�*p#���K! 	rS~�i�|B`�cc8�8�6� �¾�o��7��C]�4#ƞ�A��r���}����Q�����#糮I}U�TD8������=�9�ȳr���T<�
*Nr�ې�=��ou���ؼ(8��o�и�9e��6�'���.k���apgP/��I+h��~ ;Y�]�*�H޷y�k�~�ߓ��0IcJ�O��^e��?ݧt��x�ƾ�O�v�n�f}>J|�U��F�3�ႊ���ל{�q�I��m{db��j�������
�>#������P)��D�SrRԻs ����uv�Y1���Ep�V�5{����OӭT������o���Ξ9]��9C��#D�ǀ��K��D��^QM0�0��c9qQC�X���u�X���e1֤8"WM�o	��E�9�v��Q�"��p�p��+���l�7���us>3���(r8��G�}GQ?P��2��/�3�Ѩ�̃�i;�]��2ot�\�W����a�|��x�ޅ���W+\�O����:��w�Bmd�w�)���P��۩ �.����[)�~$0"`��t:;��Js�ea�^�A�'���؏V/��	��ΊN���� ����;՚D�P�@��[���xrG�P2|�!%�%��i�F�?�����.�� .�c)�>`�wp�tB0�L�r�B���.~�~��k�w�ޅ W�8dI"}��]������*GfTeS����I�v�i]!����_bu\��EJo����*�I�諎F�l)�w����)��8�U��-�a��f�l���Aʥ�[��f�IK��ۺ�u&��/�AR�`�^-�4uQL�s^�"�B!>�_�>�Ԃ�d9�fs6��'6��N1[	����>c�끁S�����{Ţ�r�oe���I���Fi:x��#��q;$��sO�ɒ
6��7֝��{#���U������#����Ż�cG(�T�K���{軙�����y��5��d߂@ƣD�o!b�����Q��2�q�:���h�d���$�}~!�Ň�im$,��!!8�ym���u�M-�13���q�,��M�=4�*��,zө�{��x�]~��m��W�g"�d��α*A�G�7�!1�?��7��	q�g	Af��c��̴L��2�������.����H�`g�Da��!�0U��NH��j�_��$#�����8�<�����}�6�Y�������o\ً��	�K�����~�6&iʰnp��[�"�	Lw�A!�)�e.�/E����7��f|Ia+
��;�G/]�ؙ ��Bnv�Z�б�6t#�E>o�����<:~G�..�nUD[���+�!F5d�����t4�(|�M���4�uSr�Q����o���p�t��~��-���I`ov�`o�������{њ ���͉Z���o_�WhR �e�آ.p� �����sѠ�6�'����Wc0�m0��cH�% }3�Az�FC\q��j3_x�Y��*���(�����炩s��.�V[����D{M?�-����Sh�%B�׸�h�EęƮx���L.|.��بި'fx@Ӹ!_�K��)��F�Ï�������e=m��fܷ�::k��]�������;e���	���kFy�*9y�ǏmI��~7G��ۗ�    8��*Ź=��!�����gn����7�]���4��)7��T�v��>a}'j��2�������n�L4Y���.�Q={;��B7pae�/��"ֽ�^#�ڈ��8d�_$�4w;�(z-�Uற��~�o�X�o)�Prݲӿq��}��#L�W�P���ͭW"��8�IՈ���1�=K/^NF�=���3M�������Y�� �F�:�n�f(i⁾˛o
��2-h�W�DOI]���!���H�������P�e0�%�q���rH���.�͘�/m��T�Ѡ�u0�n��<���[[����`��䲏����&!U&���$_�C�:��%�ǿڈz9� �_��4]�B:��&%æIL6Rl��x"zDkq��I��"���u��鰡�C�<3X�Փ���\�.�K�"��f|��2��'~-`Uj��dD��jj5.�s�~w����5�8-�y���_k���R��:��ǀ�[0����~Д�1�8�N�/@��0d�����gN���_��r��4�
�k|�����/��c�<J#5)�(�؁�Ro����M����-�)�PοW-�o�v�tS�O����c1/��`�LI`���@��񫦾C'=!�z@acr��$�8�;Ι��V�ԩL$��Ӭ�*q�E�<ҩOQ�탤�T�Z2�Yk�(P�O�*UǪ}�e���E4�����Aj��L�$Ty~Jq5`�ڿx>�%��C��Z�.W��q�����YI[iUi�� ��-a�l1�3���%'8�����ʦ�$ �LmH��B��wO�IĀ��U�>lvqD�i��z��9��?Q�
1DOĆ��bqY�ɠ1$E$qv<�/�k_3fxC�?���L��1p��i��k�i1�Cw��gS����'���8�F:���G}��[�듒5��^�u�akkX"�x!8�d��.���{ �s���QyvKMN�0Wڔ����{=�	��iA0%R��]Ieg-����>�3�j��nU���O�ke,�P/��H�~q�׊J&���̳�<�F�S�K
ƈa��$�v������P�H�=�rZh�0d�&�\.��Ɛ"�L�,�.�L�=���iS��f9Ĵ��F���S���"pn���^@���[fǍ�n�t��oGG�!a`U�X�Zȋ�#��y}�?��y_�f;�Վ��T���h����X�0�C~� ��$��p����6�lbE��u�c#�+�������~�M�ӝ>�%8�� q$ˆ5�]�~���u!�F.�hH��̪���\ѱ���nٸ�M��R������V,7b۪�A+>U��},�֥����Y}{PwZ�Ĕj�������׬	Mc�ڵ~C�~�;7]�I�R"��[gGu�=��8�<=�_�/��9/��@lhth'�c���{i	�'���v��<�y�#��u�$�e���JIRԁ���Y���,����x�S�j���Ia��W	��#[���k1%�`m��t)���`9zڂ����'�þ��
����aU��ƫ�fN.q�}���3�π-��T�АG ��/G dJX�N����v�\��|3T�ҕ�!�^�$M���UIoIyδ�V0=7�]��f�� ���G��D�����S���#�b��|v�E[����E�����^J6�FP�5��2܍<�����|��������l>���_��B�J��r�jڤ�%8B#��
�ϼw*��ĉK���<:��<�
��&;���]�c1m�6��'. a3�ϩ��o�V9kr�V�v[Psm�h���q�N�f�}Zà�+J��|�(�a�l�n^�@��|�/��4�> %�& Aֿ�}dLK����+�{ͨk�B�4�8����q���|�j:ƅ�>��_(���)�b��;�In���G������2���]�'�kj�C�jW
w�E��W'�h��h
-/��'������p5�������?~��Y���*	��F���ѡa+^W���KV�0� q�öϙu��ӑ�[�)� ��'R��<�5wqib�L����α�ӭ=m�Ђ�0�x��jo��i5UÌ�8Ҏ��'�����I��1.Z��p��%>:��~�/-����v}.���:�
r7�>\/A���*j�|#a���\O��n)R��H�\R��-r��&#�~���}��|~��Wo0}�rY�D��![��+��.8աTZ2�!�wmm�g��vJf�=Z�u���'d�DB�"
>6�%��>N=t�%^>̴֝f�JŨ���P.%+��ρ�"�,���Pnr���ٙ�P.�R�x��*8����L�M�)�Z�:~9��s��^�:D��J�d�&&̀�E����Է���6�� [�@P�-������fS�nS|l��n%*&�x�n 6$�5��0ثtOT��QS�m���4E�7c�5$yMe;�+�~�u�r�g)!f��N�U�vo���IHE��?BP֨ƸN)/�l��i�
6�I�q�uw���BZN�^WQ��,XHr�,	9k&ЫU��{��mބkj���M[dy�cE�6���I�S?n�:I۩Q�� M���7O������3���g\7Ŏ	6įp���Q$P����8�P�8<7��꺚�Ql�qݬ�cM��+��#�?h�:�;�q숲��3ܞQ-D�1{�:����Ut#��l�jc/�{°^}������I��7L�@�}��6 `L��3�:Y��P6�����".,�ᔦ���q�2�s��qpÖ�s�p0�vk�_��J��5Vi&����.����esc��z+m�n7\-"����>Y:�Sg��ӟ��G
�D�M���;�"������ )���o��f���A.�9@s�K �)���~�;�,��f�5������f�����kQs,��wf�ȍ���)��DvW
��|N7 QÖ[�h(��x�|������*d?���r0b��Z��D`�����bx '��߶��Q��Q�1Ƌ�(Qֱ7ƥB=S�s}C��^��޳��j�%�pf�e�����������ϝ��[�y�M83ܙ&��+�9�f�kr�0����<�Us���>��}���"sLj����7�xV�'����Ы�˫����P{$�f�L���ȉS&ų�lǔ�^mT�&�S>s�U�7�Vǘ���\�H.����}}��JE�ѩ�c�ޤ��>��[Qa_��UD�x]�!;_u
��QX[���+�Q�QA����Zٛ��:��t���yF�dhM.Yw)�8���}ၑ���&LBhl�݉x�Ȃ"D�?��tGl��dD'X �k#�jX���|;�l�|���`==�NZD��˥�W$敛�>)M4�G2�iP>��)7�ɩ�@@#>��#�K]�Ra7U�W�H��OS
��qK�8E3�)������A�'
s�O�@���M����:r��zk ���D`�<l^S�5��UQ�-�􌖥�N���(�C����
���=.�m��ګ^!�`�������F%����g�)��&�˔��C���h`"z�b��\�֮�i;�md�=��s'�u0�8Q1'��)�#n�%� �S�����5g9.�ƍT���ڍ���"���fql�#K�PI�^�"��"jw�"c]���LĠ
��&$��$��1�����Õ��,�w4O�,�	�vq�f'����ա���w�v4�:�6����Y�	E���k��;A�u��	m�˰���"OAHu�,gEi%|��J���S�w�	�� �\'s�Kx[�u5�%�l����;�e���v��|��P��7p|��л`$���Ӡ���#f:�ⰴ���-{X��EE�.w�AP�{��{&���f&���.���z���r���n����Sq���L�!V����5���@aK�i�|�"�3�eYW(��(ԇ��ܱ����*K��k)����4��;(N)|�]?�~�f�ď��6����v]�w�͉wx蟥k�m� "�4K<uq�I"CL:�od��m�    n'��g��L헭���D����Saݖ!���bOH�+j�:��� ��t�R���8ӿ6'ʴx����B���d�o=*�|�ؤ�-�浭�ݾ�K9�����5��4��MohM��i��FC����Z���I���i:�e�D3Z`�L�O<���sA��������L8�P�&�"�Blo�$ �J�Pyy��4c��0Mb���h���QX�NNs 2�D������m�Kxɡ�{��9��1��(����a�h_U Wa�d��$V���pvо�,-+�؈j��E|���&x @W.� ��l�©���#a�\L!�6}�������_\���|�R�l~���mly��} �b��u��%%(���Zq����v���‴JW�%�A~**Y��//@
�ٱ�O/gm�ӭG�gV�:|B;�Y�����rt@�������6*��[�"��F\�A�&S�� hc	I�WY�j�cv�x������!���5:����6�כ�Jt
6%06��b8'��>�܄������5fZ��E�qw����بbh/��ꚜ��[�]��ƎW�����?��ڪw��X������р��n�����9����o�cG�O͛L�et~4J1l�\�y��'�g��;cY[�s��^��O�.���ɔh��ҤG(���wq'��t㭮=��=���H�Rx v3�!�K�C����+j@E ���B�(|�AX<��=T%Z���Ŭ獟�d�"���\$�k�5٘q���]6�g���<:��ZA�i�F�*e+��=���o~�&������Z:��{�t���-�Y��M9���06&����D�[\x�i��	Kp��r�TR-4�?�A��m;�������[��a'}�<Iu�G8+մ��������h	��>.��(�ٷ��ja����Z�F��|=_�8��f�-�4���K�T��Ƭ��=΍�Qm�Vq���z�؝�&d�"�vk#����[84��>@�%D��z�1��yF]��ZSR8�c�bA��|}�P�F��� X�ʐ�{sBsߥ8����|t��@���l��|
D�+TgM}(#��2����/�<y�^�c���r��i\����"�=Cܗ��b�N{�JWD^8{���$���磩(=~�(l��*!��$Գw����ש���l�Pe��ɠ�0�� 
߬�SGb�z�h�LJ�z��Q�I��?�W��Z������xUbE��+�?{l���� �ǕAG�t���P0�E^cޖ��#�cS�Lv>���z���>�Iaٮ�)
� ��nP&�k<�]}����$��Y������hq�B�4��?] �0%:5��vX�����mD�\�~g�K����o�9K'�P��C� �W�B��B˥���{꼵�/]��� ���+{�H�6��M�o���J�s�n�S?b�\����Aq�W�'�D��� %�"��l��'z�H����]����5?�"�/�^�7�z4}��$dm�&2�ڵ�\�տ�q6qC9Q�ڬI�����)�e3����a�4�*�z�:I���9��S`K9��:��o��Wr�r��!��G���z-!l&6�"�jE�r\=��7*�s1�A^%E��J�^�6bv5��/e��7���6v�R��3S�x���C*�n��ǲ��.;G*���f�Y^[�v���,�$�͌��+4�pɎ�5��Y$1�v;ut����am��jD�f$�Fe飂�E�t�,��]~>�j���M����h����r��9�n �A��m�*����Q�2���H�`>���]pbT���>q�Ĝ�ӄ�M�k�NP�.y��Z_:O�\Ф�c	ӿ�C��R�U8?�3�LO�2���YO������+5��� ����?�&��,�ú�&?8�������h'򯩠���vf�鏫�������EUG��\?W��
5d���[�{�]�y�yt���y�l��X�	j��{/��a��Kl\ސ�[��-,KOΏ�&��5���3�$�S�u+)��>8[rJI;)���bp��Y��{�I�
Y�2ro�Wm��=�7�:˃%���3u �L����B��z5ە��o�g�T��3�G`�`�B#��D�b��w֍�ݸ(�A�

o	�]MF�^��@g�/	J��s���Ǉ����q��G��&&�m�w���޾GZ��8�Z����q�:��s�������r������Pe��B�F�3������m�О���Ns���nj�s�vD���u�Q��y�F�2K���G���A�~ŀ��G	��C��5.�"���')�0�~D�D�5����{�sӁ};Зf	��c�@��=?�V���
�g�����X$��2)/����>�b�X�
�O��w'�1�˛	~s>����.?��MDM����	�F�j�@]NPԠRUG���nq�W�W͑��$�OM6��[V�k�Rjp��׀�w��l�(z�W={��qJl���Ow�C����#jaa�~�ɘ���f�4c6�`��k����Ƿ�
2.�,^���6۞�uFcGyd��
�A�����e5jm�����Jd��w�+Mh��wY���g���1�6�x��D���63��[�{鸎E0mP4�-�D�N���+V�� ��|���HA��P���M��#p�:k>��}��Տ~H,��֜M즄P[,�;��Z��G�;�Vlj8�u���T�����a�NS���9�q�+P��#�(��0"l�ҭ��a)���O�I�������#�kJ����#��ޚ������sNuK��O�b�����T�M���*���d^<"�����V��l��O-�tv��5�v?E���H�D�j����̂�>�U��e�$f�Җ�H5������}�;��n�:S[����!o�͈q�]`>�DZ[w� � �B��RH��K�j�R��FvTg�{������%Eh�w?&�D_�w�ȑYF��S�����:mi#��on� �f���[4���0�)}�k�>����gk����e�V}82�İJ�[dp��R�,����U�i%a�N)Y��kds{�����u���m��~$�ld��W�3���M�=�<���񤹐AMQ�,|U�6����-)���|Kk}[��̥���#qѩ�	K�!f��Z���A�-|7�*�2�����.�"A`T���aDA��å�H��E�W�$m+�Uݹ�ɡV����� _��;5C�x��a>bT�\lX��Neų?�˗�=L�-p��	<�fl��K`��ò�	~����.I к����
�������|^@Wk�iN`E���'V�:�n�ENJ�M?�'S����A�QT��I	��ŵ��a���S@�#�J���W�K�Sϼ	�Q������	���@��x��ⅺa<�B�\�L�X�,��t�g�_Q�"/{9f:Pp劚�>���~�P����>υ��A��K����*Ds㰧�� թ�r�~�$��m�}��9�]���Gt�^J@�-E��N�F�?>ߡ$F�Zsz㗀��"h�S2=����
�8g_�4ch���n�����sk��!�wސ|�}1l߻�]Ͳ��[�-JT|v����\F��]�s���񽩁�㆘O�p�)t�=X6:�_j @��]P���\��k�8�T^D��Qfg�	��.��%�٧�bb��_Z<�p��^������I�a�#�BH���j)�f'"5�g^ɨ�
�Y�q��Cޏ�ȟ�۷�MVI�v'���c׺* ��9�ka��Ӥ��.P�<�_E	�!#9���h���S��A�=�f�J/�HIr�s~�ƽ2���=\k����0��`?���(���Py�f_,7�s�j�e��7�X�?@e9�6T�@�E�<O"��ܘr��7T1�F��a�g�_��D��M�[�œ!nu׬FiW�� ����&�Q��Zb'�.pdX<�<�+�`�qJ����R������!DUbD���X�Րf�%�y%��{��n�_�#d���E�:��̅�G������'ڪ�ق����V2G��
-y}gʬ�    �lwMshMH|̖ٱ@И������ ��Mã0����"��A���6�eb�\�E�����G�ܢF�N���b<1ں��4K�Q��X��G9��mL����Y�{:��w����n�^��m>O{*�Ŝjk��?�\�h�ɐO�w�e]�䬞���<e�f���v����|k_(�_6=���M�fY���uY�
BK��P�D�JF�z<`t�;��@�D�l4p�Q@8&���:�p!��o����+�=?V6�$t
���c����-�p���i�|ۿ3��gQ^0� ���Ϋ���𜂙���F��X�L�#��'�{��4O9���M{8��jm���ӓy޷뾾������H{���o�K`w3VT
c�Ԕw�jӐh`�j�rM3�@�ԏ����	�*Je`)%&�qQ�#�%�ĕ`>c�^Ca�T8��A�ԯ0��9�|�#p��Q�3e1e9��䵁j#K3$�Za\
��)��-���O �B':̫��F���&�h6g�3�X�3���{po����j���~&}y4��N��*�+�7��5���q��s�$��^B��T������|/S=h���#��
�?�~��UR�
͢���v�"|D��5�٘�B߃�8hڕ(����`��Ѻ�2�����_2�@H��ַq�4���'��)��=��%1]��6�5�	|��!U�h)���+�(��֞>�����>5�m{���H]2�FD*6�W�8�P;�洲$�C���@N��Y�1=�R>�"	w�R���F���/3��"�S��O;�G�h�����W?'?��8:4���7G�۲ӧ����R�1�.
�ʒ��f�v�::������Ej/�J��O�����0��Pr����)(���]x�0|���Y1��[��*#�E|���ڻLa�����И�ƟRJ/+P�x`��o����e��Y�I������Y�}�@�k��Y=�O��:��1���ɜ������"z������������s�w�&���u�\�ir�ђN3�/H��C���U�zf��"��	��v��=��aH{�M-�F�7�cx�g�E��!k���;�+'?C }|/�9g����)FE��Qg��x\� BB�`:Bs"���vtK�vo-�0��i���N�2:�I����2�δKy��FG�QA����jqn�ܔ��qG�Li9f�`���C�E��k�zf�G�����H�*d��,Mk�)q��cM?��7�v�4�ӽ�p�$��3��ff��}ͽ��]��~�P��ph�f�`g�P���aS�q�/��o���J��Q������G-�3nć�MOK�:�M�����6�}LO�N��~6Lr��x�l+�Ƞ�8��9f7�F�>8x3x�7դ��ph)n~H��Xr/C��.���q�������=���.,����R��ō�'P6���n�i�k���q��Nا��;?ٺ��]3������qK}��}[���,�rdJ��s��md�0�"Ы��%hEQ/wL��R ��ݱ�5+�~�/�Y��?���3�6��&�c	��������~��^{�;��$0��)�_��a��in�.���X^���e�5�ֱLp=��ʟn �~0w^9
�;ĿD3��^����^U����������{��z�Pm`�t欩w��~��ռ��2uҪ���1/�e�c�
K~��J9E�X*����wk�̌a"�&��V�MF\�$�r%0��8j�`4k�Ǟ���-3�4X�i-��kҠ�h���*th�Ё�k���'?G�3�Cߞ�[4�[�Tjk�I��u��K�~(�inO�N^9����}��Is���K�0�]>RI��(�J�J�$?T��I�i+!@N-�7"�����m�$׋h�}.kbTd
n�S�E�� D�ba�iKz�����{��C�9���Fo�5 ����ʎ�$N`��fD����I���Ih'�}����e��Ͼ���K �s��WH��}>q�dx�>�\/S�z��E���cIg�S�j,b���gO��'!�h��r�4,��A����X�fv8�KzA��{� 2��2��'$�zu;o�����>�
s������XJBߐe�U�Dr����\�(] s��k��)�ƪ�z7�c͞j�a� ��+(0�� �������9Ǵݘݕ�����:���/�(�/7��i��?�k�FԈ�]��UJN���G��"�
��߻"iJ#��N���͋���g�Æe�< f�y��[j*溆T�a߲��,2��w��߹���k�{,3�h�m���+��n}��O�w��Z�	`�\c�K�fD@�yB�G��+dm��3�C:V��ɷr-[�Rg��p}����Γ�Mv--!=��Lۏe]�w�O�wu��pH��8��Y�>}�F��lR'X:��V1��ؓ��B43�^vl.(o�~$y�ёR�~6pP��
��{�hAA����ɾ���d��6IFl��*�5�^�'�u��Mb�\�hP��K�P�(DZ��yv(�L$.�WD2!̲��7������x�Q�1��mDҺ��f���3VG
uf��:�$����Hu{yN�y�}.ם��+b����u��4�X����P0���1A�:�!���˚ҹEU��LM�_�q��jt9�d��~kb�}-kP7�Zh��͸�<��H{-�@�?o�d*:��sj
�i�%��-��4����=9���}�{;m�}?҉�D��M�����X��3Hٱ�÷^��y�\��gE��(��ˢ0�q����ݗ�B��$ju�,��$+�Y�CSBǖ�`d]��#�1�-�s?�R���/�X�6e �Y��YIsv�<7�TR)���#��	�� �.�4��uN�g�٨G���������e��ƭ�ƠН_�Uxk��Zd�Ux�)��x/ƙ��sG���3��}NƗ7?��&
���V��&� 7:��~{ �ܽ��_B��Á�9�s���m��<�Tvԍ7 X�d;"I��1��[��~���]]"�-X�Ji��O�|���+>�R�U��7��5��==��M;L��[FL�T�42��	Z�[o������f Ya6���[�9Z���X�ߖ��ˊ�
#����w��z�J
U�g�@��iS(��I�\��'F��Z�R��O�nT��4��+Dc��{+Q�_G����ގʩ�� �!���V�oe��HM��/,ߕGt�]ގ�&|�|+�h�!:�M8��gU�x���$�t�c�j"��<�븒���D$�L<fn��q�o��&)���(��b��[�#M:�GoL�����$�{�.�������d�r�k����f�P��V�������#m��X� C2�a�B�m6=ɲ>�w�	���>��ؽ��Ӗq���d�%�*���C����u^F�>5�6�G�������G�P]���J�_u�M֊��v�LOV��'W2_�$��Ye��7f��(��_b���Pǳ�_�9ƴ�=$%�����] �cXW��kz��{e�H�t"��,l������c��d\��:6j����it�Di�+��~*l%��TZ�����|�\18K��k�9�UΎ��J�iF\w�nD�U�<���Ov�x���p�0'��Ӻ�I�Q������c�w��}�΅��JԽ.a�A��� �ώ�d�jI�qf���Q�=��nͣ���L�
E���dga����z51�Sn�҅����K=����Ψ�{[��Ō�_�Y��$���L�i��"=�k��zm"�k�C�A볈%� �ʛU����ߊO�l��:����n�+޽)�����J��ѼO��������b�����^�[ 
��`��M�����NJT��K������d���g7i���ҬM�N�oa
Q�	D�Dw�UF�E�p�Pv멝���5��M[W��گ ��	�4��(�*�C�S�}�|X<XK�gm�Ro-䩼�J���d��y������w0���65
	��g�{�9vY@q    "e�<]��%��^_=J۳���=`���(T��@�@}�ŧ�r֧(j��4hYw�>��*8_��G��h7u�������H�S����-�݅��V�9����j�U���1�@L(*����y�{�V�Ҹ
v�`� 5�N�8�ț\�:��_E�/�����kt�э)��pө�kK����h՚���R>��P�ģ����`"���(F�#5���'x�*�X��ܨD����f��=��	�h�8[,�[�n�����j�S��19c0�����dh�3���PhQ��Sl&��r�&��0�ғ���3��H&�$�v�H�@��=)��ov��NW� �Dx��C;�#փª��=i6c���]c�w'K�Y=�m8@	ǹs����	R�]�ĕ��A��j��_�s;"�2j�����Ysl�':x���� }u3٘�Aν�k8h[���1�,������|m�1�y����48����GAG�,y���G��5Z���/ ���
�=� $/���"*$�� �o`��f�"B`���Tf��Wq�[�Qc�i��-[��l��X�'���]Rd�t�JIv����Ւ+��]3���|0���40��>gv�u-��`�%0��,}��I��|���gBC��sy��/u77o�o��W�J��@�D�p�}�c���`��h�{�h`Яd�����n편�B���Oծ��Q��ؤ��8sl��6e�dg���W�nc�n��c�z��A���݇^��ܰ�Q������~��z��&��M�����_f��a%axؓ��Z����R�i��_�/E��K�J���	[��*N��7�hy+yr3�فb��}�&Aی�Wxa��xH!Y~�)�?K�ʁŗT��cm������yG��(�����If��8��:h�	c�^>Zꂡ	o$�>�6*��qR����Pm]�5�K�ܧ�"�� [Ñ�I�O�@B��m����B<5��a;g���e�$g��H=	��R�E9R���C$�O���fǛ����[`�y�M�f��2MV��M�CWn���YyrJ�|� ӟU޴<��
E詊ow��k�P�n���r;�a3(!cU�p��H���]��1잍�ȕ��k�a܎��:p$u�IX[����|�@��9�,NK��U�	Hh�[��;a���>�'6�k�K��1~�4�3�qR-�/�(m��}嘄�3h}Z������rUE<�2�B4�5(cQ8Q��q,�Sl�	��n�uG-3�K�E*1ߘV��d�2=��<Nf܏˾+@˫�Qb^Q�9Ѿ4��~I�'��\诉�Y]�s*~�<W��]�+ĤHzU����].�g[P��!J���sc�n+��i)f����:v��_s��Z6��ѕ ��!=��QN�$�#�yl=}ؕ�p�s|`iNMF��j��a��ؾ&��^VX�����E��PDK�Z��<�j%����+�ҽ�[eXJi�ɏ�/�_�^�ok��I�A��+z�hz23�J. ��#������M6揵y��$����֗�^�>W�!W�~OF6���_���q�8��`�!\��w�s{`n��~��XD@�+���Ǣ�˔�Js�ʻ���||H*Q���r�RB$�tq������z�������Ced�yڔX+�A��́\w[�։	5�{��L�WB�A
�/%j��|ld���h󹘒�#O�p�X��%C�/�h\�F#Y��S�,��~��M@�{�6�02}�;�IFj��gc�3I��z9@���a�s��b�ur9���_��S����_X�8�F��>o�n��zdҹ�ڶ{���8;��ԭ�H�_����ao�%��S�%ɚ���)��W6g*-v�4���vc�����d����4�/� ����4�o�7��p������P�"?�������	�k��'p�ĉ�o����s�,�����:Hb>e�o���z��Ug��S+��Wq	�5s�aÉcs�?^��N`�X�+�p�§�t���x��B'?M#��:	e/ے@��Օg�a�Ց]<9�	���d[Vw��2��X]C��9�+�ʥ�r3
������q柣�5hј����lT�z@wǸ���؋��?��g��ǲH{L~��!�1�i�O$������Hyy���G ��R���O���c�����C���ۈa����81�ˬ��l���OŧD�$g�g�L{CvΔ�jI���Ë�{��v��KWe��kS�εV��G��F��-�p$IU���9����cG~v{44x�fqC5���^��D�&���҈=�L����|��jnd1�̏�g.�E�� Bs6����)C��ی��7Ok��ld�J�w�n�ֵ�Aw��:�V�P�r<�'X�*䉥�Ue������рc�.l��X��q#d:2�֩��s�E8|7�8���gj����2��f�^[�P�2���<�2KÈ�T',e�ߕ}e72)��K���u���@J������#��6]/Z�+��'�t�ُ����l�E�.��Ԥ �3F����]�\O��n-��Y�
e��]^��蔙\�@S�#v��J��dk�_��fBC�U��^W8�����K8.nq*�|��h�M����'��z����i]��%��IYs��GOwz̲m� Ya�F>�Z$8�.���N�OrI����|rN9�6��&Ԗ���n�g[�#75�*g9��	���b�+��/�����R���-�4�D�I�%d�2�b�d�UHP��ި�v�l��a�����m��$^�[��ĤI.+h	֛ 
��)��c��ǆ���q�����Ͳ�U�C
������A��`�Z$��l��z� p���Dl�)6EK���uVl|+P}Py�T��������*��/ҭgDE~��K�3��W;�~!��Zr�T붴���:p}Ԋ^�?ڑ�ldztm��]t��ғ	EA�V�Կ^����s�����z�|�A`_�J�qCS%���~��J*õ(v�z4ƨ�ߌ u�8�`y�<%�G��'?_$@���̏1;��>_~��}>]zW��\�L!@ok>��>͇�ք�㴊b)��Ei7�����h�_�b���E�Ja��m�̝�qn�f���������Đ��V�y��S@y0`ŒM̏O�%�q
�o�'���??���G����I����?_�ܴM��FV�G�{�HVU��:+)!f�ԃY X�c������h:{b�O��)�L�?P��m������{�r�U$�� S��[�J9 ��~����:���N���1zF��~-��#��6oЉaNn�Qo`� ���v*���>�&�8�^�xx	[�v��{�'�E*»?���D|���$�%S6#���3���?p�f�B��]���|0ZY:�cT��LD��5�%M�-KD�U���o*(c��&��������t�t�����<���� ��f��+R�y���4��'��ۍ�����:m^(ւ��*6�횊��'Y9/�=�V���fw(����'vşW���sǦJ�B'T��>�IR"��j]�O�JG�Dx��h�)*�ז��-Gm�|VNn��&O�X D@tj
w��������)��(2q�Wo%��{ؠ���~}�$���W=�fMao�7Sp�������3a<�2�͝���t�������oVET�[�m:2{���*rP$��О�/7����x�qm�A*�c6m���?YO�l��.fK.�����GϓB�A��W 7�DqM��!%
��6F�Rv�Z�Q��0Ւ��?�M.����׾�Y�ݜNn 8�+�!���xȏ{e���}�C�Ur@'<2�K��?fj����9�g��	�M�L���b��w��P��1��9��T̢	��>`?}<}����w_���n���!c�8!a�}`ߢCOi��?�k�M�*`1҂	}]�g�aOs���+j�/l48�\8��`7Ԏi�(��Y�k4DF��J�%�KC���=D�����^    (�y8��1�M��p�)���-|��Gl�g�^���!��]��-i2C�B�:�Z	�S�s��]���?"�������l�ڙtՈ-��p�}��X�$@�� ����]g���>H>���H�`J�s>��F���)�oP�[DB���
�~�����ߝA��&y��#�@.�{Pؤ��.��|�~a��f�WtS.z���_�/4� *����h�����/9t��T,2����i��~�q��By�Iц���!��R�)��lb�R+_o���[T�
�8���ނ1���T�Rٗ0�D���p�Tt�d�l���Ă6�v����f��6�D<o����a��v;�?��U����4f��/ISg�ICIn��ΟVG�M$�e/g���6�d�0��M��1����{%٧1���w���|�%���TH�,�ZɦA�(�Y(zN"�0Z�K�r#��D%���yD�}��C���Se�fu�v4N���:/C��8���<٤�5(�@�������]{W`1�U�S�gl�p�+�F��|ڴ��Z�bN�^�h֧��2�~��f.m���aH���7g��,������=���
���fPD��\�e`
щ����I���a�u��MmY�^�#,��0M�(�H0ɇ�؄�U���]k�#r&+;Q*B���K�\�z�;! U]���j�o��w�b��3Q�~f��{�þ`��آ%q�E22eX�%0��� ���Lb9N�O(D�H^�P�	"�Ƣ P#����8׋�ŗ��4W�ɴ %�:\Dũ�Q:����JC�l�`Ʒ��>:���$�Vl�&hc,zo�^�w�[����n&*��A~�-�C�����N߱�0�x���L	iwUu(������T�c�8a�5�6i�5)q�GO�X%����\�e��y	y$�z�"E����ov�������<*�(�14Q��٢U�2:��0̲�}\�9|����o���R���p����7��Xl	|s��ː�_�H�[�=��2��ֲ������{�$
0���� ��秓{8��!�6��X�/��ʰn�'�@'�Úd.~�k�@����E������
���:MҍP$̤��������ȵ-;9a�X���|�� �ʤ�*}Uۤ'П��G=:��н�>U엄1���Y.<�q>o`s�
X�Z���|�1�ICw�o(����:, �jp3`����� �
�p}(S���f��yP(�8ѳ>�r��A�
+���p�m�����?��#����<��⡐�+]�/�g������ �Z�����_��fy�p- �,�	Q���ō�d��'9�=���*��F[X����E��z����h��	�x�5���r�LA{�v�3�2�R}��x� .|K9`���ɇ��74I"i"�n!ln>�M=0ԫ�P7�!�5[jQ_|	��/�K��αČVfM�GZ7<���-����E�ìK�ư���G��������w��8��'��]2�fr��p7�Ic'��:�qH����RY��O��dzw����V��)F�K������o���mb�M�;m�[�/�k���Զ��U�+�ҿ���U9�#�F��6.�k+����|�	+��*\��HW6��E���	�3���]*rp�����_6�R,E8y�s����@��%��w�k߽�ǻ^��&~؂<W~�"A�F�)�oБ�}�p�IVD�C2ă���U;��/%�{��Z��җT�L�F��)>r��4. ��]�����:m�o�v|NE�C[z)��zc	A����F$cI��V��Z��"IX`��@����_�fdt����M	�_�8��j�&繨���Q�j׳��%�nl�w�S���'�:�;���ku�7�5²7wP8 L��f3�Y�.�G�}Sq�G�B�@�OH�_��^��H�~$l�߻���A?+�8c3�3��=�U�e���TMP`��7�3!����`h������ǟv�Gd��˃V���`+��J����ڏrf���]!S�d5���i�k;�!i�&zVe����|�o.��`���etѧm�
�A��|t�*,!N�C�h�Z�.����qC��$ 6M�fC@�|���)]�F�-k�ѽ	̚u���-���.�G�*ˏ�ʅ�l!��B&`J,�� �#5�Z�ƗD��g	Z>��z���2'��X�\�ԒO��]WE�\L�r�зG��7���ї���D^�p���נ��'��j��J�[�P��ٴ��V�����J�[�5<�qw����AA�=�@'(�8p�O�<��0���Z�VzNe� ��WL�����ApNE�!��i�B��RX�-f_P(�,u���6�b����j�45�M�WIv+_]��l/\)��7:Iu�=$}���L�k?l>�,'��O6��#�:�76��t���	n�:M�B;��ӱ�����wey-c��p6�hDh�{.s��-m��J�dl��Z�eI��kp��}*�A���������B<>�ٶ�	��Ӵ`j�y�3�5S�X�_A�ׯ&=�����������T���Q���.V��^�@q#(t?U�9�u��%rg�8|�J%���;��{\51c�M~�M�%^��#�pe��@
Q�ά(V���
o����ly׷*`rCvh�!���R�ꍱx�q�Xh�fpE\To����&s,�����|��s`)���u�BqM��A�~���z��}�(�f�{T?\B�I8�M{����2A� A�����3����B���O��TB�/g�8�?�lTH��}|�Y�S�S��wA+/�L�[�B��Ϙ�p%�l�Q�l1�����XZ�X)�� ��l�#���e��� �������ٟ�����s-uș$}A�vR�S�e�����o�t�ܮ=��}�g�M:fI/r`��!����l�����$+��V?NK��8?��P�n��~���twv��� t=9�hi�DHt�B�D��	�g���lh�_�PR)��~ ��I��������^��93I�A�P���V~�����Z��z��{3>��/>az��
�+o,m�T�!���mQ�j��k�F#gݤ����oj�d(��)K��i@GE�܋ā�ek;Z	������J4���^���҆�ă;���Y̝5R�P_t3kw��8�"`j�	T��k0�v��0�s�%e�җ��<�whSg���] n�,>�7<�ѻ����J����� fv�S��[uI�C�&���:�y*\�{<s :<��=�MN���������C�@���΃������D�F�'Y�x�=n��9���������vl��i�ȟ�E#;��>N��,��f�P�W�U�a_����[�e�7�N^�-|N�R���h0W�C��[v.��k�$㡖��I���<P�'u��H.��M܃�H��Ek�+�+Ŝ��3���'�&����rh�S��׿��u�o��ѣ�w�0gj�g��jq�/�fblL���������2/�){� �RMю��5ލ��/9&��Ӊ���	�:��k����X����l��nW߼�Z�bX'�c�[/�Q[e�H��T�n���1UFF�R3>����{sã�j�u�-�-�qz[[�2�Op4/e$$�O@xqV����i��D�# ��/��(V�S�)� ��� 3�I4� �9��b+�ov��<�t0��uʄ&��SZ�D)T-"�'��}' ?X�g�������i���=����n3i������X�Yv̓��T�HxNGaSZg���]��� �|1����Z��,xe+"2���jܘ��7���#���F��=m�.��_��Z����Z6�d��۝^�^�]e�V܎��r��!�Z�EXh������]���B�q���ނa�m�'�����C�Ay,�o'Z�}��}i����(σ�����b�Zʐ�K��{9߀V �*�Ԅ�k�Y��U(�U��?'���F�bu���i�ٺ��_L��-!������-��5    ��Gơ�����u[/S�ZZD��ܖG��dR�y�;ϊpRK�� �
~��2��0�%�����殌;x�!H,�v爟%����0�{ ��D�ih"�����H>�ڽ���&�<�a��֏�+��N�=�Y-�4+�p�ƶ�o��e�t�C�{��J�� bR��9G�Rjy·n�q�n��Ú����CxU3~E���8��w�׳e'�)�#����`���|����ua�jW��Fη�!�('[�Z�򝰄��Do�#��Ō2�_8���������"�_(�[];��I�H��E�d�e�[xU�����D��|K7�k�&E��Imh����S�d��U�ߡR���Ԇ���I'w�#]��������DMa�p���2�-:.�����?Eؒ�-%ă���BÜM2�Xz+#5#i�Pe$eW5�3d�T��v5��oZ��%�N%�6<�J������tLg]���q���y.EhU��h:��� SI�K��@��!$#]A �ޟ����No��p����Mkt�R8ޙ�΀��4|d+���,�ѯ�WG�}�#�~(���%�{u9�p��fB̓�,q���+�[������m��ӌ�e��#�M]��S���E7�c��d�	���o��.A#��D�'3,����)�U�g��˹�r1|
P���x�/��F���@�8�3��9���|�!��R���c
s�3!�Fk�$>lU�c�C�ښ]u����Jo(�'z4~�,�֮q�Y6���4��#o�6�� �g+����^$h<Q���
���)V��ޯ�<]�v z�����F�O�,��j��}~�&�������){�<�,���R��cq�1�ف�Z�	#�,B�Rl�
��AK4���X0}3*#݀oR���f�&bV����On:���Z�ghǌD�1�S��>��'�t�
���A������LY{�7��>tt�LV�9?co����=�v����1�e�7�]��8Ѓ{���
��ةVb�zX��jϸx�b	ӏ)�32!�(�� �ׂ�$��A����y��_�gy?{�|ӂ|$�o�^#��p�L�ɺ�c����ٮI�4�2�jڰ��6bq�htHxK(-<^����%ˮ@~�l��#  {$�W%����̀�VT^�(�5D���p<$-[��ی姚��R�,U.q�]KjΖE@F��\�����Z� +N�����Oց�1�\�龚�e	��N9�4���a4��x��8O[�>������\{�$�`s�a�y�'0�x����\w�ީB���V3��vw	��I�lbk8�*�?h�����#�rk��I*�4�p#�c�_e�8+k��S
�ƕ���������ܗP��0.����ϼ�J����O!��#Ab���#���-�k�7�&Ê�Y]����j ,�F����m�֤�@�����3�fB����]`&R�=��;IГ�YA�7�L�2�K7��}`&E���Wqwp�滚���Ī~�y���L�*R���yS�3�s���XP�~H��
~���GóL�4Ɛ|Xll��1u�W
�����:�	��{�r�$�9'7�"zu�a�Ԡ@�S��F*�o�fN�y��P�����Ҁ�P�m]}X�-�uoJ�����7�WU=��;�.�O���~�/��xn�������_�P(Z�K\5�M}�Y�$�!�?��{6����C�[@�>+�c��#e,>{S8j��3��`a�v�t<�}�j���>m
�6�mS�L��-�XX�~�V>5e.�����ЮG���M�琿���bE�K���h��g{�w{ޛyϯ���V��[�>:�x��ʼ��v��+ӈ�	�Q�R>��l�;�e��ק��
��np(�QP#S\_��K���`�"5�??���1�E�R���0� �#�a|���6������m1�5�m-}��׍H-l��k�\���C���yG�m��2�?D��v��"�_/ϕG�^�[��a���_H�x�q!Em��D�����ңn�M�/^��<t��%���D��8��)h}�,����V��ʺ��U?�m�`-k���)'�ǀ�Z��~�o�p�"C���d���.vإ%o�c����F��rO���A���<�ZM�K���B�*rP]��-!^����c��Eb��9��]��5%oIF�s��/�{&9�aG������`�a_W�����+�1Y8�L��$ʛu*�m�}ն��m��U$pr.C��؏=�@_�G�|*gL���_��o �SP��d_�j�X�5��t���]�B�NL�W�O�}hJ[Xa� �e���!�1��!�����������Q��_
�E���(�Co�<mS�Ʀ�h�r��־�=����t��Yz�C�7�W�xEA� �Tء��~���4�b]�ty3��֎,c� �rmV��ʰe��0�7B��E�N��d���u����ic^�~߬0׵$��c�7x?�pD�̷|->8��1A~�cB�h��"[)��~{�)86:�{�r>�V��N\���,��(�A,��w��%���=뤻�Wu뜄��0�]9RPK7P���H	f5�+t�榘1�`�V�_Ӛ<N'pKz��+U=�i�� ��qq#:�P��-���m�Җ8ֿ�>�M �-�BW���Q�'�� ��]r��fF�[s�'��T>���7FP��}H2�|/t���d�W;�</�d�Hl�a!�n���쭏&�P@��غi? �K�\����G��G����n�:�=�z��[5�穷���
���%���7h��a>Zg"����o��	o�̞��>Q>�Np�ߦg�&��:���dc	'q������2�q�8$��lIX��)%
պ��y�K?mNf���g�٥�;�U�7ܪˌ���@ٹ
W���9��FU���v>}�==|S��3�;�����{5�d��u���e�'f���]��#��^SI�M9��<E�&�F�$K�\]����}�6�&�
3��ߢ����>�w�&�F����xE�ܐ3[�4Q�r�C���ɭ=їrD��.4�J��
f�$]�~H�]-� ��YeI֦3f����o��q�/Il_����(8����dW�uǨ^��P�ɹ/�N�	��В��PI��i�h(��vpF ~J��#�PN�.%O  �g.z�9Ӊ������o�ʋp��|�xX�W'�j���y�ilr�Y$�(A�PG{V?�<"��:&؝��t���0�^"Wީ+��G�	}�yh~����R�&)g�ʓ���C4	�p�c�2x�B�]��Ɇ��-�}��R��P��]� �?��k�i␧ˈD!���&
0���c;�?�PL�� ��|���~���'�f�Zj&��;���)4���
k��^C���V)��1����*�+7|�V��]�G��o���~���§>�����O����d%�D�N��5N F~X�	���7��)�5��_������Հ� ���?\W�E��ܝlؒK&��P_����p����2�B�IN�頜�^� ��4����;'5;��ˁ�S�mi[5�8GO���&Y�t�6pCC��܂&�R7�^�B�m�	Ka�`�I|{TBEG���1k��z���[�τ�jx�Ln��[�C.�c�PO0!����nh/�����śn�"16wn�A�? ��	Gp� ��oZ;�U#2�i�z��aئ\���ûx�ɴN�)��+&5�J�`����:*���o�sb��x�uw,�{�_s���8B^�v��*��?�e��x�4BC@�hA2Bl���K�$����We�����J�˂�'�8��`��S
��X��I��ߐSZ �����{���/������y�i��l����*v<�Fu�@��כG�����_��{�͞���N���"	����,њ�'C�?O��u��_�*�`)�+�3+��TҾ/<Ͳ�8�q��[�p?���R�E��m� kF#��j�*��Y�F�xls?�F�6%�    Hߡ����	Q���%���.N����+V����mq�p"��'�߸�&�YZ$�����zaD�{�#�1�j1ƃ���{�����+,�+9��2�窷��M�ƺ)�P��6t2n&�T���%Ѯ,]����4�~wo��"^��ۀ�_���|b�둰��w{�|�!������=��R�_PS��R�eΤ!p�I4�?4O�jvY�^���X���T�����G��9�\�q�����D�R�N�A4�׮��*|�cI���)gzv�X���G��1� E
!�(�MNAlo�:K���&s#����ف��L���r���	�������(=�6�=ͼ����up���-��'A�;��r9�!.L���sx��u�b,|�=^�����;�egI���OIp6Fe�n�����~�L�������6�f�8Iఴ�m�"�<��-�.��E�U���͛�*bq�>�櫩�S���\Z_�u�����/�[�ɳ빞_"aĒ��Y��oK}@�4
+��TT*-7�U��ˏEH�������Y
4�ar��qL�B����P�YLQ�q���}�Hx� �_�-�(X8+֖R&�$�7�[;�&/��4=F�S����DYY�iIa��=��zY�vBMZ�	T��������B)�a�H�V�u�}���e/�G���L�������0cd>�U�U?M�Wq��DR��e�H}��g�e6`�|���_"�YXQx�#1;��%J�o`�L���g�هH���#�4����+�;��OM�I��u��0q��ۅ
������3D�*t��[	�����U��j�[M��C|�2]bl���`[*�e?�����Q[������A��ܘ��3|}�%��"�n��q~�Icդ~U|Q�k��.qp��H��aުci���b�1#ʍ�&vT`�81���u����BC�)p'�C�����%�� �3�?+�/�yz�Ŀ3~w�u�ݱ�B���$Rxٟ��p�>��Ç�!�T-�
�
穝�~O���w<?ȟH��m�M��f�.�>�k m�1�7ei�2^�^(Z�I�������*����65���W"COD���}��b���U���;b���6 �I�II�TP�����@khΎ�B8�$m#�?w(^$����h�`Z���΋�WCB�ݪJ�Z՝�D�5E��"�c��1!����;���0�&b���	���xBx�xuEtU���B�2_��#Ot�(�f��D�Tu���J�1�<@�n�3I��N�J�I�5��kr�ߡn��
aĬ^�m�<>@�o�7Qx�l�j=k�s�5��&!�~���*Q��Ӿ��4��?�g`�����fA�!�r�o����������u�u&{G	8S��P����CR8֦E�*�����m��	bnB>J�Bw���f�H�=��e�+A����S�|�a��,q��eq����!P9�9nl鳰o��aI�+�X��������P
r�ҊS|#�>c�6�����z�A�V�f��W�����x G���/��}@�я)���^���țP��,"��gʗm=�6L� ��;�=���ـ�P濌�\���P�hK6����	P��k�ۇ��i/�h�%Gt㧯+.eԵQY����@�3%^8��*����������E��)�ӞD�i���ml��jѾ�-�)oؠB""#o��L��x�,:E��Op���0�T���>��%�Q��!��������Q֓x��'�����]�"S��v��&��~8���!x��&�P�߁L$|���}�hV(��.��27yR����m� �MuݷzF�@ֈ����
�\��%��L��VeіVi[#��)#2ht�����]�p���+4�^	�d�=�/��k�}Ew�:�]��N��Z��� ko��w�~��;���}�f��)1��C�(0L�\A�.m6!��Y�s�1z�W�S<Y��~���ni�&�O�LT���5I\*߅?ԛ���!�d��;�=�B�����cX��� �f�c'�o%N[u] ��
����������8�W�L��7e%��)��`9��Z�M��̟��5��R7�ff����J��dO�cn����˲/̾X�����Q���g�{G\�'u��D����S�Y�'+	"���z��>3l�c9s`'L�DAҘ�����J#o�����|�p�b��Y-�LB4��JE;7�_��8oRKA�����4[C���}�!�ô���)V�����DJ!]0°�B�͵m�q;�1u�g�����n=(�<K�]�k���E��7{��Ҽ(���ko�m2������s���-�l���k����Q!�Yj�׏}��)�bK����<��]��fG�y��ѩa�5{��z ��e��e�Q^���z����CH�>	���"L��u`�3b3vٷ����ЎE}�#)^�Ԕ�`K�g�&�H��_���2`�oW�'���5��]�̆�p�ˮ��5U8��-�-��7x��=���q�=�r�m/�w�����:q!�n���d�P"����������ᱻ���ۖf��fA{?��i)�$��s\�S�Z�h-�K;4���esG��m��w\��
qT��Z�ʋg�߰�I�[^P�u>Q%��~'�O?����I_AAJm��B��M�T�Z>o��d���)��u�VX���{���ܺw#�sfMf s����v�{��I�tB&ϵ���83x��OB~L.I�0f6�|i�\��Vuǎ��#Ţ��l?X,��w,�#��+�5Sf�{a���O�����������}]at���u���gײ_�D�p��M�њ���
��Q
*	7�olU�3>�:e��q�9A^����g�܅W>���l8��^�r��m�}���j�b�7��tc���>8�}������&H�Q�]H���q$o0Y�$֊Օ���kTѦ�'�'�.�}gj�/����!x� o��>y�ŃI�)h���4���v��T�L"���ң��r,]E�C}%0(V��h�l�jGW�V�;���Hb:#Ϸ��5��Q9t�F�Ȟ�"��P�ޢA�����$g�w�=8P2Byq�����<����e�&K6o�xণj�7K�t�W��>�9���Wv%j5,�	*=����|�w
�v��mG�;\�(%�i���c:�ZEEir���7ŧ<޴ ��{^��4�P��:�!$.G�0T��7����?���C��&_��m���D��a�"P;�5�ƣh����o�:��S��\y6u_|�v�Y��FF>>�:WIg^��|h���b�Fb�FZ�t+A�ړ���?�S/�^_� �[]�*���v�u�"I�(q�T�����$`o9�x���O�t ��@]�䶄�U�kq6�硪>�� �,���wK�9�qޅ����̕����T��qI�.�M��Z\�h?���M0�q�5C~�~,�nT�ʢ��_칃V���.B2c$��K *t�W��]r��u� �~N�YÉn���L�p%��zG_��Qv"�F�\z y����z��:1�Zә�|9Sw��_"�4Dmc��Kd��� M��Cc� ^��� /�f�ԛh��8(�
+����<���@|��?N�x\�ѕ���y�f���5
������*��W(Y�zB�0L��������1���[�A m�{{��U��Ͼ����?jU�gL��k�@����a�/�J���!����qB-k��X@�#I���dg8�'c�9���&jpL�ǈ��j��������(�īR�����G�7�i(�q۩�Y�V�R7>���n��.D��{�+uZkor�لL)dXmw��mB$�Y�U�+��t�y��7���
[GJTn{��MJKu���7����#:����:�p��+B����������T+އ�u �S��ȹg�<�խ`V��|=ѱ}AM
��|8���/9�R��	$�.��$q���m6�f��6&�T�r�E�t3ɚK/�.�MdIԮFr���~qñ���w�aMcQ    N���yB�̍ˎ����
��!��C�> x��g+�&R��Lp�Ζ���Q#е;R�w�èٮ���ܯrŨ�k,�Z5�x��o��`��Ws�6g_��7N�[V;t
 ��W�S4���6�"�"i�b9��%���1i1@�T)�p��Y?ѳ��s��?�:����M{&y�?;���$��1�Q��;���ӊ��ehZ#G�����8އ�y�b�iϓ�+���F�x�S���%7vbYF\�'��\��(���Q����62w�S���%��VK���1���\�|�d��� V�B���9�),�q�[���s���xQA�'Iח�}n�!h|�!wն�r��u�M��g�͸.Sc`�f2�/t���ðF��'�}�)���%S�Ď=�>�-�-��`&�I,!�כ�	Ր�z�O>�\ݾ���ֱr��O5
CC0�Ϩ��?��w���P8�=�/sҮJ�K��Ä]��2c~�H����>�?��-��������s��裗�dՁZ�����&e8&�3S`=1jg�6�Q��<�ծ|H�JCyIJp8�o���f��wv�_�W^Q|���Լ��#t���J�A��4n�O��z�rT�e�JM#?�2��(tG���
����z[�q��%NYGӤB��䄫T�����c���f���/c7��K��KJ0�l�w�v���7�2��7Z�u�Ւi���4���-�������>(%��wR�
����/�����&d4GK�Ť�<��!��E�j8�҂u�n��:p�����Q� ��i�3Z�P�cQv�����8n��|�HG}�P䭟M.F`�(D��\Gl�(�Aߐ]��|z;	�h���y�M�v���*��3y�t�8��x)���������fl(�^�D|`�,��E!���-k��G�5.���Q;�+����m�.������Hba	��yi	} b)��|���8�P�,��֎DIf��g�����՗���>��m���~�!�,P�)\xرb�ڇ�����n����?V~U����6�(a�H���G����g���HJ�2&���;���nxh0ל�XD��m���j0P��v����@	�%8KV�l�?�:1�&��C�
BD��-�!��2{p�ː�"��u��>Pj���Y_�y�%����C���1A�i�ޗߎ6[�;Ǝ0��~��_r�����IW,�y���xA�z��I#��&�A�~�U=<�,?�a��������-%iE�ސ���6L��#�*�Ҷ��&����>�ջ�U.zl�/GN?h��.3!�ȡH���)����.�+�7�if�>���SS]�#�����"&
�Ag2�aJ[���S#-k���)0�����g�~���1h1��Eyh�n���.u0�v*+�lB���<~��u�����*n�U7I�ھ8������$� �?�W�[,�M��Afjm���Q*��}�����Fc��x�rW�Hֱ!�ib:X��#��;�]VK�`�����KQ`��2(-�������#H�!N�>��� �V�KI�
���pTI}\h��[�X�B�f�Hv\T���
B��`�����[��+2w���{����V��V� `�p꧌���V�3�(Ѳ=gNe���յ��r�f\�j�d�1� ��c�z=�zr]�ED��h�H�~	����=�"u�Ϊ��ݔ ����t�B8?#�b�ɧwŋҦ��Ҡ,&^`Q�X��JF"L�a�X޶Y&�Q��Æ�$7��f������OW́��FN������ޭ3~�ථ�\� �s��m�v{�@5<�۶Rv� ��b��>  �SpRױ��a�7~ˌLK�y�'as�?1����H�� 	[�A^�����)V2ϥ���'E���D�����$*|�w��LV��]�s�����*0��N����F=��zmӜ:�yR�ֿ�ءZ3�ƅl�nB�J�#!w$m��#˵���kК�;|��y|"J*_2�����N��>T��o�ͯ��#,zY?D�����`�5�;M4пo<�5�|���9*��~�!Sj@:ۯ�v�ؠ!���bUYW%3���a���Ń��T�P���Nw�m��gJq15���բ�uG�1��KR��.
<���"=�	��� v���"��}vJ^��˔&˖&��%�=f[/a���8�#p��(z����u�]0��3Ey��I��5��V�0��p�����>^��U7\ឍa����0�B��9,�o�Zndi�~`?Z�	�1bzm~�նj�g�LwA����z#�[z����$4a�Y<���zQ��WL.,��9ڼ���}�$W�ӷ�&=�"��w
����Rm��=<'�-7�J�F����u����wºÚ
lu�8$��m�`%��2�:c���k(�,Ƀ��O�)=�w_�ѵmz��GL1��� �*��Y�廖�����pь�5Nd�y��by��ٖE
m.g$0v�߲4�������Bf�޵~��x���Cg����A3������kÍCb:3��1��d�e�e�8K �.oȤPe}iU��/aluN���=o��Əh�1���n ��&-4'����Ck�����	��JS��k^Թ��Q����'���ݒ����?E9[�{=����Tr�5� ~���#��3�"��R~ޠ����t����D)UB֞�Z��0��T�h�Y��g����
w�Y�nG��ɔ�� ����[��~��'�V����뢀:�Z��
�	�����Qyr�}����X�ԛ����N_��[�&WTI��@|[è@q�D}��˭{T�I��l��pgƑ�f(��F|�x{��I1�C}|�,��A�Ҳ 0z�;��F3�⣻��ɘ�֜Y���
""������1鸙���Q��`(|�W�&_}Qu%\`�^7S�ջo�ԟyR?��*d�[M(q�f�,˦0�I/�����gE�����0���߿��������.�_���n$�}t��,G)��N.��]/"��i�}�М�B���iWB����%���C~42��~��AI�$��_ƶ)Y�X#d�����>x�W�F���&��9�/ed��b�dX( 7������90p�b��t�x���G0LQ�dX����
��<��l��Ĭ��kR�������hْN��2Hc���%��>H?��<���1\i]���������s��ָ
'Dx>�T�~��H��.�zd�:^���f�KU���TM:x�>���e�ԯ^݌����f�w,S����ጛv���2\��+G�	P*�&>P�K��mռX������ҳ���2�_ێ}�$<��b����#]�Y²굷�H*�~{2r��6tم����g�W��p�{z�x��]W*Q�*��uYo牯�p1�t4�	F&�F�Ut�)��a�-������vX{ɵ���ΤJ�HwE@�67ޜxn]� ̬���{���E�?�G<�[�
;����u9pS<������l`��z�-D�Awt|N�&��x��z5�԰�l3���A�n�������%`=��&��6�Ư���`J+�AITg��JX3{���e��1F��IH$P�9Bl�{��������pC�'9���uv.���ǰ�5�ߞk�͎��8>�с�L���Y�V����-��	FKS�C��y50՜ý���I�뛥>8*����{۵2�YZC�����8J�$���r�|�sZ�#��Բ����NHF�>FSjA���AB�s��x�d��6㘊��?ɳ;W1�2b�%��^��&�ǥ�
��E�C�����P�׭����9�H�+T��hX�+W'�����?�!/jM����`�8q����á%J�����f��R�D��G/ϩ�j�!l��{��C��|�%Q0�Cy���w�XD��^.	`Rօ�F�C��
��}�G��'Ts*���8�<ܸD�b9/Ty��h�[��\������j:��d��.��p��^u{�h�b+����
��]��h�e�ƽv�3    &��2�`��i��T��J ���;�fQ<�Y
���	�|��d�4
-�#�-t��@�O��e�D�2{�n,�n�6��77*��.�Ͷu�����;�Vz�(\X�}	ȼڕ���������y�۽�	�����]Jo�lt�DK��G�14��e�̳���7�{�HM3Sꅨ��F
����*�h���z��Of���π�n/�}�����6Nc#+<�)���kW�T��֞����$*�2Hu�wcz`���8��Vs��Y`}���;����l�=)��[4#P4z���38�� �)Q���W)5=�x�o-wd�~occ�c� ���{�C�lX[��r�J)��A�_��o $�9Y�*�v��^-y�����(T#���N�]A� ���c���'D�3�#+B��h6s���Gŝa2	*?T�X�|���$v�95srz� �=��D��uN|����؄`�Z66o� �6����W�`���6M[���:3��eʘ�|�)��͸{�g��0䱿y~Vo���oA?f�NK�qZ�sqQ�����E��0ջ7s���ɰ�p�u��8��m ��uk�c�-U�X5�SO�4���v�<p6�f���c�<}��~��i����yTx9K���pA���)�����0k���'�Aux,���\�+���	��;���%P��`��#�e�&9m����ډ�C����Rtv�kEr�D}�pJ��?*^z���l5�Y`��Z�2�����z�B���֊h���^���t|˓CY�]t��7S�2�����[�?]=��ޮ�z9��SP���(�����Ko��zAW}������vt�J���l!K�����"��hx����qr����U�W��R��` �zm(�辶�����S�1t�W�o���P�~�r垜SL��P�D<�?6PrKFh)�]D�:�P�E��w�5�=�;ol���p}�"�t��O�&4M�H�Poh:�|����؁�/�w�|�ضQ��hsP���HL��~��^�V�V.M-^�$	Ώ���P�m�K ��mux�s#��"�Y�� �DNa�{N�}`�F��ms��V_�
��j|.���a��</]N:�z�D9{ޅs�1x��Ę��$I���SE�ٸ�p�"�S���yZ>"���F��`���GS�+��	..�A3�V�Tid�Li�);.!^[��#��f,H�O��׬\��P����i�;�o�
��%e�|�;p�ۺ�&�*��O���\�_�&����℻�$� ��� �c���V�~�����ݽ�~Ē�L��̓� <�a3�|�c/0��s����)w,p)S��ϩ��~�x���4B<LL�I'2����v�hLv� �_��2� 6���K�E���l	��kR�y>=H���I�-)�@7�P�M� ���lI2��>YGf)�Q{(��~�����Y哫���ݚ3�z����o����aC��cX���	w��F��ѾWk|�"C���224(����7qG�&��ˬ�Y�pݫ��=�������K^le1��=_Z�b�+�:�+��O��l@��[��d�~�_����^	G�%J��N�j��bc������~��u�vcV���4��'\�Ѫ�?l��h>�Pؒ# �%�����^�J���~��#����eK*�4�/�2����v�}���@��1}o�r0��֓6'HSB�U4�cΏ�����Mr�]�c<�{=��|V�w�l�D�Fp�5n~�fivZ��Z��3�Cˊ�8Mo����J�n^�&�-�`�9E1��Ҭpd��Ky$���3�29.��6G>��>��̖�pvq!M���W���d[]�1���q�Y��tt2�ǜ�yE���Ff���p���Z#��f�-!VC\��t��nz��W��ւ�/��ى��������_��sb�#|�	�p��r{�)�'(�?^�J$�j$��8��_ҒP��S(�Du����L�C�c�f��G,k�7�DV<�|��o?Z�"J3�M��/��4>�y	,ka�g�/V.�6R~vrHF[���9���.7��4�_�/ke[g�*qG�u�K��=�H)@��8e�cZz�O��g]��E~�kx�R����>#.*]wI�qX��L�}�,�V����Q��YW�E�f2��gx�]�*@��Ӈ�u�ǞKߧ�/#��͈�
O���J����ҷ	��ռCm ����D�g �~>�eΫQ'7�z_�B���8uM^�L��|��:�b�+b5f��\y���R����$KK	 R�8��eBUa�^בIZ�r�ˋ}����֎��(,�+m�.?�geA���F�<0}+[�p�H�Y��h��j�����e����='׊�2牸�x�6����y!l
��>�u�AO���y�VhT� "ޏ���8��	H�w��{��)�i��������a۬�6��h�б�wxBLC���|�9�>W*�F�L|_ZPI~�Q�*��� ;�
8��D������\����I�oS�54�p�4 hϫJ�>[W)�_.���
�=��* ���mP9^�vS��7u랅�(ړ~�(r(We��La�GH�i��f�Ӊ�"�=u���}a牑���+x���0�������؅����Q�O��D^Z��ZO O y�gη�C�\%֯b�3j��i~��b�
6��N���w8{.UT�.��kF���0t��^J~�c�%��.���#DVW4ܫ|X|kZ�����6D��gaR{_�z�i�/͔?�##�`�U�H��h]������Uf�1����-�H��H�6�)��J�
�f�����Q�v��o���K�e�Ʉ�No�"ڳy0�Iݶ�T:񃕊��(��[[ꋦJ� {N��h����wk���:�fmt4�#"l2"�
�,IH�	?�2.����,_�Qk����Hչ���EE�~��}%�����L(�ںY�Z��u����U�4^ό)g!Ҿ��$�h[^$Wd��f�l�!Aߟ��""+��w	����v�gY*��f�U����η�e���b��m)���.1��(�J*�j@�L.�x��%͂��BKZ���ے�Lm�1����$�60tg�*#(��E���a�򾔛��f��@�~� z+�0gIi{E:���_$���$�(:��a(�~z[ҍ�����ׇ�2�����s�A�/M��~sd}�a��"��|��ź���فtl	&�}���}/��9�Kz�U��-^)ً�K~��O��@�Z�hC�P�0���
�ő8Yfٚ}� �{��ϊ�ar��I���C�}��u�D��\������(*��#�P������L�mZ�W��k�!Z�����O�-�U`�(Wb��k�����ȗ�W��">[�уFC�F��������IrCݶ	:D���)Md�c�((s�c����&�$��Io �As�%����R����Я�w�+��+�]x��z��Jb�̓�V���֛��,����VI �@'�_�[�/f��#/�W�70Gc���}��HR"��{���$�?�.���uh��Z��X���2%�p}�yN�$��5�J�!&��q'�P�I����Y~����fL`c�.z:I},S/%�506%d�~LGWu��1kg�H`���4?K�[�?�O��8d�1C����|������#��D;��T4+�aT)�����B�}ۨw��M�[��������uIxLYL��އ���@ܐ�����]"�����DHo��R�^|,����m���`����'����2:/7����S�y���T�c8a�C�vn��E6�P��SW���/�j8~�L/�I�n�0��{FL߭-J��g�@E0l�[�����-��"�3�f��
7J��}
��o�"���t�,?bp��D����Ȓ-4i�����D#� ��O��0�G�I�F���l~ z̘��m�\�g���ڨ�^3����h��I�G6]�&ޠP���	�(�g�����������Am!    ��p=�G���`���uU�19���UG�����3�ζLâ�����������\.����P?T����g���rA�? �A�e&�����u��uH9�kN�`�2'�1[պe�u��	��ֈ��J������o�� �����Gi�/�Ht�&

|��/䨐���h9x��ݲbjn50�r��GӍ@�4(q�@�M��`j^Z�6�gt</ �ԟL�F2��;Ž���)>Oj��ܥc`�Sk�A�0��&��"��m��/ѓM��vC:��_�-L	l��_2[9��0���b�?�1��jw��EwH�gJ�oLm��1a2�k��
þ3&���
����yp3i�h�KuB���r WT䪇Aoy\v	~ɘ�����F�Y����I�m/��y��!櫳}&}ED�8�H�.?p.膑�CvD7�2�~� �j���m��ᕍ�i}'�iJ��n���yL.%l|K��{(ᔔ�ɏ'�RwM`bF㎐^K�e��}TdyV/���*+_K���D��P�_��)�۱�jV�����%�?�E��G)�����a�:)ƟE��[l/�x(�U���F�C�>�����o��P�w��olX����H���NiW��A��S:(8��x���}~���G9@��FP8x�z�ج����_�.�K�t!�r��0 �1Ry܏j�ln.��Zgh������i�?1�(���������G���G=�nx�����޶���j�G�!��b���(+ӧ2Mf[|��x��ܢ�3�\�����%�G�#>�?Z�ܮ�r���EB�L[D���/އK$$|�A@�_����e�/i�oS��(������8hj�
x�3q�є,Y��FE�27銗���ܶokq����jn���[�h��[��)��<�ک#xU�}�H���AD�wES�Z���a�j��+OTS�斡8�*4T�a�h� �2�r�������P �x��~
k��E�D�v02:@�VEw�txէt\�4X��� �ɤ�^��`�XE>�A,;���ruVI��J��6/΍���LZ��M�<5s�������/k��dj��"�vh�x7��Z�ݴ�s��:b9OT	�'�6�y�����9�M8Q��:�N&k�ٴ~扬�'bs�G�5���|��mF�	��އI�Qjl���gFm�y?g 2�Ts}9Lp�(���վhU�iiCgHYr��i�(���\�)��� ���
�}��Ф5((���ʢ��������7Y����	Yg�:�ΤD`�:	:��sW22�qS��ܨk������1�T�
gD:&Y�k�'WF�NAP��5?C�s,�6�"Hdtb�<-M�m8�����A"��a^C�,n\+��k(Ŵp��n�CyOz?{Kj-#����>)D/@�r� �fN[�1�+�[<r:�U�����+$��K��

�ov�c�L 1Kqp���|y��pu���?q�j��(U%zt���9[�I�2�F�Y��DÝ=L9Fy�X�fp�C�>^7�飕!F;_�9���#�{u2g���i(ڭ���-&��A�t��T��s/{r�:���
O��Y���(�_8�T�6�$a`4�¤{S�M_԰�ӷ"��/$��*������H��
u�8a_�OD�K�iZ+1�Q�>�vC\	J�EUd�.�u����_�9��%�#�������.��7b��sK¬Z�|�؜	���"L	6S�nB$!�8��˧�(���z�?&!j��N��m�L�,q�d�vCL�,�C:��H���{�v���cGCK�U9���%+�l\�nW�j���)�&�w�q�~\��6=:��_�9(���x�}]z&��q$�F���!O�����ڎz]["��.?���L]��D^L�v]��${�!/�xP�14�r���r\":�JA���	1 lF}J�3�5�E?��-_2���J~��Ku��������0���+��X�����*��V��_��h�C��~�2Cz�������S'�������l0>�����B�������ךQD�B
':�w>��r��c?�A�s�O�- �N��8-�dp�]��:V�mϦSy+��"޲���*(X	�ͧRc�	G�Q�3d�g/'g<|������)W#=��M�&�]S"�@G��z8�Opp)�0���yB�)���^%����H���y�T���,��|G74�uM��5�L�g��E���GZ)D�|���q��7�������@K܃cw���+�~.K�La\��S��躧0���E����AvZ�WQ�T���܍�]Ǘ/V���)f.�ߗ���I(�?x6H��}(�ι�oV�!a���h�[2��?!�9n�ZIT��g�?DN����Y�}�~$��|I�PNW�m�Q�?A�����G_�=�C<� �}I�G9�S���c)x
waZ���#�yk���r�VQ�$�N�稾a��x��:$T$���U{.诼G���"H�$eE��Y\��1=���!�'��toS���8\���9A�`�s�W±�)Ѿ�	LS�,u��;�_���Ё�l4u�����`���c��	b;|K�.�g���G�d\��+��*X#S�2��ZzInr݉{���@���N{��	,�8�'Hݩ������@4��ko8����[{��k �\ru{�t�;]���^襫�.�a:͹�����3W������P?/���Q�h!��d�NWyR�/�0�)�Q~	︖$?�����Cw�8r�j��
qfoiƱg#�T�qD���8�M�W�2Fx2�_�A�LI ���rl,:ns׽|�kN���+M�ֶ@���o�]�̧�<�͈ӮP�dn4C�P�����B���]\qZ�;~e��%������@صF��|�U��Ֆ"Z$"�A�u=����N�!qq���*q���3���Q�@:C��A9����>����}��*��wT�C<�sD��P�f^-���.��A	x!l<OH��xR^��u|�uP���b�j�B[��cۘʻ�	�e�O���g�M`��Cr�f��po�I���M'0AK1ĸ�*M���X[q���	U�j���QG=�����Y*�g�B�Ӧ�9]z5e�����e�Q��%,��51"�^�� �����g՘��.�Ϝ��܅�/Y�HW�2 .�i��f�j((ow��sMM�]���/%b��d5u���vv=�6{�g �d���b��6��S@K rRŋ�K+to�I2���υ�^�p;�"��q�A��ٟ��O4ğ��e��J>�����s�KYչ�<ӷ���z(�����hD$'����ӽkQޙ�9:9��d^ۃ�ڛX���N[���I���ؿ��J��z\%��li�qy��N�p�7����O�ids"��	�jF�y~����B1'�!@从��l����&~�>4�W����3ı�r�Z�q�ŵ���6�Ɯ
������P%�|���k+��6�mu���۔K�E�Rs6�x����\2�[�X+��ۓ�!�| �IK(5��}��D��MހL]R-%�c��P�N��N��M��d�C4��Çv�eg<k�i�T���UjWy��D ��s�3;F;����(t<h���'�G�*�ϋӖe��'b/��~"b\�b?B�I����N(��8I�.��ϻ�μR�� �#Ҿ�ŰI�ཱི��{�Tmj\h����+\NY)�(�=�� ����«E���Z��ی�����i/|�|�B�[kw��n��Ӳyi���WΨ���LY"���	�/O>Q�T)2���M}Rˁ�ֽ����@����wQ\���8�@;�&���-c7��[eP��s4�/@�Ď�<��t�4e�D�-��'�l��{���ɱԇopB�r��ᛓۼ
�=�*ξ����SªZI�����e��`�"������N�U=�᠆����9��\���yG/�/��2�<    �i����������>Si#$4�.CP^+��	_������g\�S��H�ѕ#=�G�@��EF��������0�x�R6�9Gk�SUi�}dNJz���B��q{��	u0c
]�Z�6�>�Pƒ�����3���Pڧ�PI&9C��S��g��|l'+-vr%O����n����X&ɂ,�⤽ԛh
�|D�@��&K	h."���}7ڞZ]e�n�U�O_��ڒ�o�/���t&h V��l�a����E��$7y�a}��O��ߞ�ߣl�����]}�۷�����ht'����G���Z$�9�|+�(�x)��4�C�TJoz���a^O�t7��,���L�$S�
���GO�c�m�{m��{���5�5�U���������²|�r�ֵ��l��
���'8�Ù�20���rD}��D[8t^��$��9�:�o�L��:m�������}\t����w��uK��;����a�U�%]�/��HvGR�n�S�B1ѭR�GH��%����ۀw|���'�|鯧�q�L�e����a�Dw/��H�O&���Cy�]��;�4���:��L�8�(�	ۻ�W���w��>����o�y��Z�Z���CE[�^,LУ�Z���~Y�Ѹ��LgDK>����k�Q2���Y)�1��pY6�K��<����#�"�;RW���Y���S�8����e�_�|���;w;md��(��p�>��i� j�s��Ѭds{+G�n�:�6ɑS0�D�k%t��v��=��یu��hw\U��4T$pQ��	4G��TJ�e[Y��e��a�U�G៰aTS�����xw�u����]]��	>=���2>Sr��W���`���×��'xWH����M�`�-�������𥯷�������@ݚ��͝؆G����V�;׆�SW!tZ�l�A6�q��	𯔂O0�Wp�R&hA�bA
p$��?ϋ+q$����c�;�C�K��^�j����x��-P�t�mU��=~e}�z���, 9�� �:�}spkO��B��@��Q���a��@������<D�����Qi\*Zx#H➦�}���=�t�Ha��+�\��%�|�g�E�8�4�����c����gy]܏� Y ξ4�b���[�Rju�8Q��x$Ԙ.��4�I�
��S��a�l.�����'�U�"����¬t���>�Tca"�	'����{$~����s��
�;�����=!?��d��8I��?�DD�傻t�s>%��W���h摧eRE㪊|&��}��+��,��4��z\�����t��m)����yA2)�Ks�*$���cZ�|,Y����ү.y�U�:��`������-I�{���Jt����IRc����ە��w��"��������*L��r=��ؓ�D�Za����H�Ӿ�n�������x��������ּ�.��Ǩ���J�����ry�fz�i\�n~_� �#$�gcI��,w�Vi)�"�mǃ���@��A*?�͑{1���ԑ��6�p���1W*�\`�>� ��js��^���AH��]Ђ��W.z��U�ֿi�͆xQ��t���/$p���E� �'��
x�	¦E�է�R`��61�z���|^l�8	�&�%��1{�ӉgQq�Qj��|ث��A����.r1�|o�w�(O(��Vm�M���R�����8h�
D�D��=�"&�\ ����)>�|K"�E/�:�q }ݍo�~�r~���d��5WBOs��d����\O��<|� ��A��A�j�=����Y�:r�`��{znX��n*�Jע`yO%]jt���e	h�J�Um �6�'j��&I�}`[p7�
�Hg���(��PS���K�X<�۴�)�����/���9u��J�>��]�$�l�N��*4|���t�z����Ǆ��ō�{v�8�"(?��_�'��{��f�*���x1 Z� =���p�5����z���1��i�O�����t���4�DD?���q��p�O-��@>�����/}��<F@7�\����g��Ώ>�sw�G�r&��啕[�2"�8 Vn��d ^"Nx1�V�_�����'��%tF�y�Ӯ�����T�a���a�"O�����E=�O��BW����hsl�C �ލ���0�25�/��p��[G�^1L�B��ـ�5�΍zA��;�z��iԁ����5gY����d���"3o�&�%7O����F��y	�إ�u�ז�~/Z8)��}|�9��d�."����|g�����Q��(	Uz=��(%h1O.�v���~��F��B'�C����k*,�{T�d�U�#��3�\E���#���P)��/7�*���.IB����ƛټF9�@Y�hM�o��{7F�1�vK�L�����P��=�v�4%�0������2r7�S`�3�ЇtĬ�Pr#�T�Ċ�
E�QP�S�fь*�!N�[{K�cD�����56�k<�M�>�x}b�}dFro	Q�1�[ʎg��Ddz��#K�4�>��O�Q:{��az1��s��T�=
+�5���1JD�/��&g�F��u9�������:HUv~,�	y�C�����`d�B��H��/	���om�H������+�/��6/�����JS����ClA�sv�O�zh��0����r�G}��k�݁Y<AS�bI����o�T.�O�8Qo9,���I7�Q휄ɟ]ljWGE��r�4��XG�����ό�_�㇧���n����U�t;a������z�e��Z����'� ��(^==k�����N�,+=xpTU~����S�IH�d��\��# :WG���?���ɞ�%���$��� ��^_��Hܔ�^iF�	*l=M��Uo�)F���2g�d�PTViL��	ƞJc�!{�Ǵ4�i)��uq�E�H1�=v�L6^�v>n:��`�)ɑ o�Ȁ�<�����%�Is��ɼ6��`�[�Jz"�]]*��;f�W�a�`Ӑ�0�<�������nڡ�P�LYʜN���|+�������7�m~�xY�:[�])���o�w�IPL-���l���NuK1Ѧ�z��`�v�0	����ZX��׫ro�C��S��#�D���m��p )���§K]�7C�^��}{��н�&��{�4`r�9,԰�67����k��GA����r�2��$2��;�{m2CK��>}��\$k�#�ٞ/�7���J��	��z����%�6���N�o�W�S�n}�I	�8�_�}ٸ�p����IIK�tC��+U�/�vk��9P�(Iޟa>X* xb��K9Ž�H���!�Xޙn��cR�h��~N�T�/^�T-T8i5i����pH���8yZ��� ��q5kމ|f�]K��y O��e�_��	���Ʈ��=��Ϸ��u�U�d�_�ݔO��C�XiM�*#�L	ii�#)�)�)�{�Q�,4R��u1��b��$:��,��QD��߆7j�7�QŌ�Ƨ�1���1���m/矩���~Ă䲔Ci;M����:\W�H0���頜B� ~2*�ko5���ݟ��b��}F�h�4��p���oW\��A�M�f]��ɥw���C����
��9�����rn��cb�όo��9 �vu�Ҹu��s�
�C�4BnK��B�;2#��O���I��[�S�ݡ]��vef�����+('��Em19��/+l�j�%��3�_�~ي� ��T�J�<a$)O���}y
L���Ů��b�o[�=�����S�;{�4���\���s��WckQ4�),,�^�Q\�MUaX�ȿ��n鍆�xge]Jw�����0��Z(v/\;��e�-��uE@xB�hw��o���y�TLN{z�Kрhq�ݻ~(>��E�������geǗi0�
�񨮷p�H�D�U������z��I߀�l�
^E�����џ�)��yB
Rk=ef��@\��o��$�    g=���t�����@�ݲTi�~u�8X2�^i��ܕ�vN~���q �MM�+C��F��>)Gfi�%�~�����b+�:�O���`�ўAwU��8����
@�j�h��|�,�-����9��/�K�>��MP��cd���U,�{Gs�z�A�`.�����O[5�� �ER��9�����/rz�_��A� M�+�C��~ݦ�F�#[���L����6\��j,qLK��%䣦�u�cuǝ�G@{�좞~�'�����J^��)fX�W��my=p���Ijyn� U&��PA.��U�@9L_���(�@����ĳ�Jg67�@�|Ts����t�il�*��C�o������2���PCcy�	�-K��!s��^Y3���k�.�O<(̑0K��l͆�ʆ�����b�<�-^1;� /߯.u�te)�,+W�jD�+H�g�ܔ�*��5��m���UG�c�b︠B�~�T�rY�QK��!��N�H7������U?LrM�_r�F����nb�J-5�7~�q=N����b~�|��f���6᧦��ۜ�G��6�Н�m|�����?-�� ^k7���_��b=�Y�������2ag�n^��-�T�A�|�I����2j�+9���d�RA -�7�:n��]�@n��_���������D�!'r���ŴMw�Ч��$��������֨T�t�j���/��k/�mΊǷF���/�^_�|y�/�MX���{��UQ���dk"b �A��˱�NJ&�cs��;�?j��ϴP˵'3:�eÞ�Y�?��|e�ޡ��j�J�'����K�����.��)G"��5�aG��S�ԣL���<�������T:@RXX �e����G�R�ST��A�᛹|�-šEli�3�������QG�:(,�ҩ �����Le��+���{�l�9�`]���Z��9�?XK4�m�$��ۇ]Z��D���0Ӵ�JC�`x���=�N�*�!g�/"�g��w�82?_������{�h(yT�*|>D�!c(�[��
��ޓR��S���H�s��\/�������M��xHG�hk�y2����'��"�=9� (#�Ѳ�W�T��W�
$	or�Ibt�3n�YvsB�!N���3��7)g�U�G���E��R9Or�k�z�b!}�����q����֤.?d�7�~�S�|�N�T(ɯ��W+��g�
c�Є�D�gq#-{>ֶ]]|�ʀ��xl&�H5�"�̠)�S�)0i�'
K�꺧Ւ�X��n������:NcЕo�2�N�߃W݄�G�aݸ#�3�K�eLN�l�jiF?	���	O�f��D�:�5
�G>�Te,A7�:��|1:P�Vf*=��{��Mn�����F��k�{�w�0��)^Z����[��M�N�0黈fm,3�����r{�E�T�{�y�ۑ�X�~���F*�򇋕pl��THg[ʸw��Fh�o_K�Xj�jc���Ũs�����䱾T�U����R_���8B���WiQZ�@���͕�!C�͡�����m�ƨeOij���*	�?��6-�_F�68$Ítr�Q!�n���r�/��7r�h;��T<�J�eI��З�U �:�����̮ł$[9�YF��|��W�n���-�%�e�{�	#��GL̲�w!�;n�Sv2Xb7Bo�o4;H3}�~V�8m�!�͵-�}P����6��L�Uّ1J�k&��4
b�FQ�7��]1��C��e|�*��S	�v_�K�>�Bl����p�&2X�5��}��{�V!Gj�Z�X�_ž���p��kS�l/c��X@H�m��Wi���g4 l�"�}� [�3��+�2)�����������m��NL��^�|���=��rj��*�噦�h�"N>IN�Ddn�"<ᘥk&���Q*lA؅�:��	�Ax5��Zry�u�`	5���,M7
_�yq�au��{���"�'��^}\뺚F���~;oe�u��} ~�ECH��O25��hi?�U��*�~sB�9�N�Cq���"F���6[>�a��С��mj��av���I�#j�nW��%�@���yt�t��<��Zmt���Hh�*��x�(��QE���!K'��g��y�-���?�1�������t(k��㽯�B�N��=atn�vͬ< Sk�<��XA��[կ�/����6�U�7��999H^�W6>N��X���������\� T���/���n5[������T8�W2ް��`��L�M�&K�4��w,��`o�'G�8%�>�q�Am֑�3XŤͻ&�BĀ�3�� �{԰ \$��Y�8�9�i�a��U��E�
g\x/CaR]�M�dx��������v�,B�f12M���\r,�m!L\��$�[OO:��<g���F{�Xږ$`[���+N�o��Lx�V,@~!���2k�|u�#��eL�P*[�R(FlJ�U��}j��S���(3o�f;WN�k(�S~7J���_��H`:�2�.%�hV'���d�'�l(=Oq�u݅�:k�6:b�8͋���]<2��n��qu+6����wJG�=ˠ��!3�nA����U7���\N���[R��DE&��^��Ǔp�~�z[�1�7��X�Aۂ�wRԢ0���{��7�C�� ��|l��$V��3�����M��Tp�݉�n��}4E7�7�g�R}������>�I��ZmS�<�$еé�e�豟������Z�Ō1��J3�du�v��6㼂�F�hn��Φ�񻛺i*Y!�D�U�(�hS����x�@��+e��X�گk.�<��\u�x����`����O�M�XY��x��Pwu�eA��|�mڹ��x��d?x��`�Ͱ����o�<��BJ�Y�"_����f���l�����0��?Vվ�]湿�vc�m�2�#s;u7��gm�*U���ux�R�K2��/�&|�f00��[��[��~d3)�
B�yx�]6����y;L����K�_6��LX4k�s@d���~������	
ꗉ0��q������P�5��ŋ�Џl���o���M��Ȯ��FF��S�~9,|��)�����b�O]��1E$��O���-���1�ŀ����+I~��;C��U����m0 s��@e���;�-��z��mR#Ὓ �F)�	�1!�}kT��Z�}ʏ>���Zj �Ԇ�&�v�%$6��Nܯ�_��	�[�+�8�� ��<TQ��ͽK��m�U�}�RG�Ǽ��fN߅#A��=Q����S^�Q��K���uG�eڸs��a�����|�|>�d�a�$&t�	c��-r\���(�l�����<��(.��s��a�s�EONvn���2L�ut$S^��G^Y��HQ��G2��7�?S����;�ӽut�o��!(Q�۸`Y3z	�e^φ%X�N�5�q�l���m��)o�2�1V��}7��葯'�����K�v��-�l5����t�~���p%X��� �:��!��@E$�lmO��P�+��H��^��MZ���̍�U�+?<G61<��E��T�X~�_^�F�9��Ϊ�i�P�h{�^i�Xlg�l����m:�͞�.j�5�Iٍ��)�Ň�S�&�|&���\H��e��X,�,Aw��������4Aλ��{v�1�a���S;UV�B����aN ��+�8�F�G��(@�� ���ȈLG"g���׸�%����kqY6H&TIls� ������
VG�?X]���i���
�=+�Tp[F�6Ƞ̇��A۱r%(D[���Z0�1�[��(�����?f��Я�Q�����Sv��Mi��K2 �(� �{D穑��PB�.��^���iSC�U	��Z��C��p�po��K��<��K���dv]�B �2��F�|=����dI�� i��<��/��	D~���tw�~�H����+K��q/9�C�uߠ    �։->S�C�qX)k��hx�(�,K�V>f�ᭋ��������y����t�g�0�[�:��h����	�nd���j�d_hq�K�K$�[������C(��'�w�qmZ	��;�6�>�ubO�o�t����쾣7XH�,/�NQ�R��l^bY�����D�r8lr��<&�li�?k�h[��Y}]�o��d�]0�d�J��7<���F�X�&����q���;W���F]�w���w����{�9�j����>��������h����[r�4���܉WN���_����I`���-�K��ǛHVA��6�ʦ�n����D�������s��6��P�v��<i2馼��X�����21)��3v�<xM��Y���g1�f/ZU\�R�z��a7�XLD��e�Z���U���b�Ȗ0�p�!p٧<|��J�S��T��/��L*Rb��W*�.���o��L�J��>�
YK�#đg�Q*M�����u�Pw�1-��2�xy�P�6TQ䱖[��Z� M��j�6&r���s���<
��U��[h;!�%�U�w*wp������M7��6m����8���/"�9z��JC�1��h���^�q.Ɵ�Qzef��V�
���u��>�;0UY�	 V���v��k��W����w�aaѹ�z�K��P����K�A�|_������A"__t��s�Zʞ}$>������p_8���e������܏�"1�������''�Z%��������[���f̉����A|��7`��-d��&��6��Y��ӏṴ�)��c���L
)���|��<�����x���O����7�H¡��F���mװ}�+t�â?h���Tp2y����-
�7���l j!4�fн�8��,pny|���k��w���!ܧ��� �h�L���m嬨��+!]�<��~"�063������C�-X1U�1�~P��d���H�8�X}.���w̍��I��ʅJUOI�i��-��0�(DS�8������S>�#=RD�m':^Cdwpr�
�c�5�2P'p�$rL�p���̈́DBET��H�T@ ��KͶ,4�k�N��oՐ����S��qYlM���rd��0Q������<�A��E�-C��
m�%���
I�s,��Z�z=���.&�³9=���z+-;����ٿ%d�ή���%��"�07��B%����� �}��{�F���D�	<���_��N���,^�!a7��i�Ez�D$<�����d�;o�vQ����R���!��p��#��߹v�O)������\��pA:�]Cؼ���]�Ğ��U�*���-�*@����4�!����,T<��j�VNOz�T	�Q^j�o���RE�Yt#�x'<��t�\=�G�B��W�3��+�@��N��Ƃ���z_��\�D=S�0Z�!����N^e�+�n�6�~Q��V�_O�0����쪮d���f��Tu���1�r�����Q����]oQ�ڲ��.�����k!���"#�q]�����̅�F��t��{���_�j�{�(�� rq�l^�"P:+QA~�8��;��3)�9��-�oT+�}�Hէ���������y:3�[�Ca�(J�Ӭc�Rh���;���=�ai�[y �Qʙ�����n-3p��@tV�-�Q�&��$=�72��rdnE�P'L3|?e�8(���BG�)��#��i�-?�xRZ�2j�՟��7�c�C�KH����N�mzy�y>"�����M��i��K���Y�Iɚ���5ǵ� ��}[�ݶl��4�{� �=볙ڳ�)�vg�>��,/��!��b����Wa=����hy����	t�*1�홈r"߶ߍ.��d6���%o?�)���hd�͞\�^jF���<��XB�En]��o�Ctȷ���v"dn캟 ���}�7�x��3c+>>/҂�`ݶ�ë���F��#T����}���B }���`��?�'I9�|"�.��3$P� f�%!L�Y�U�%+w��x��K`L��W���"_� ���Q�����!��d��O->� ,�ʺ���yMP1%�Լ����7+c�u
6��\`��"?,����M=�#e��'KG ��CG�����h���(p�C��rY�-p`i4�e�mN��y�Pqu����Þl/H��=WN�q�#�iH��d�aY��.B~�8�?�4-7�G������G'�FF/���_F����a&�/�OQ�羭j=0��{��S �Oa~�-�[���rp��[��#��r��A�#���~b%�ޮ)���o7*�aty#@���F��!���ƨA}#?���֫��A��_|�g�m/�n~�X.\X���S���R�`��
�Kh����v�ܳ2'����x^ɲ�Z^��6ۓ�9AJk㣚eppsz	��?����$j�V��QFt�������}V�c9��6S>b���PhpM�gF{�6�
"!/���	D? Ƭ�G�N&�s��@���-�_!��T�b�Tn'�{�s�n, �c�]%�S|<_�P��'�1�U2���$ ��P��L|lx�e��g�<��^�1ب�Y�]T�S��&]�|�H�a��*_A�@GF��D�ڛ�c��*_XN�s�;������,9HM/���ơ0A����d`�Ԟ��"U�
w«��߫2�*��<����v�8䐤����nu���3B-;��FvC�6W�1A��(_����y�gͩº��ێ\����ϰ '�hr#j1�2�^�-!+[�����x�f�j�85��PZ�(޷����Ё{ �(� �9�W�����/w|�:P��P8^:2��=
��7 `��侪���cj�t�(�S4I���|gs�G��|vj�R�Hθ܃ա��a�e�l=�ye:�<)$n��ɚ�E�M��
�~��f7�dZWx�u�)�{xJ���G����6�ثH��6V_�ux0�[P�V�Pc3��a�L�?#{�����Ҽˀ�"F�l�an,L_��7�����z�0��~�@�i�����:U�'�\��Ɗ6�8ɋ�Y���L4�����ȯ.���A��4�ڤJ���׆{�;}%a��Iiϒ��p�eN��a���.˂ЉJ�Y������ֱ�� �����ܽ��B=�� ��D�&Qꠧ	�|d��@tɨ��s�+.)�����<�M'BH>�c�: �$A:���d��9�Da�.M�.��)!\��1A(��Ձ09a��=ts�b&��.j_!}'���J��[���hр1�ƫ&9FS�T��Ϋ��J*O��w�RR#�o��[b"���jf�����Ug�Z��	�����q;]����SԬ��ew%�����X �c��&��-3��Vy��b�z+È���H3M{��-�b��GC0دC�����?D�칫���̋q����t3�s%��h�J+�r���^�C�?-��̢�uǧ��;�.A%-�4��%�"�эV�˛���	��2�����q �)�V^��hOF2�_�K���Y��?_�
Ϝ��r�R�=wa�b�
\�l�W������;�Ő��-梔�x��W�3��T��e���.>�����-�g��'��m��׵3���b�נ%�?)�~4�7Z��V EO��0�p�֭�,	�4�>��(�6F��=�bFɋ��@�W��P�S�W�>�����zQF��N������<��y��X��i��{~O��Yi�#&���k���}�I�'_�V0�j�a�O��D>��|���uT�#�د�_���W�q�~��X|��#���',����������&��{��ƀ�d�zAp�b�r6&�a_���ݫv�U7�^B�B���\`��9�5�pO.']����X��"UD\�lN�ip�dA�}�!PW�<�9-F�!|���
�	�sO��q�	��u"؎��(d(�M5
h���KS6�T@����35G��B}\^1��>�    �8�x�I���kUyS�
 9'j
誳��q�	"8Q��"� CY����!紣�Z�5�W�pr&C����`'�M�a5-}��]�6R��F��	��iG��8�G����	��X�`��k6!S�.�^qh���BQ�����
4�&�h<oPq�x�5% h(C�}@A�{��,)�$K��N�c�9�|�����_�.���D͗�{���v�ke�,4l�,������H�|;(����)�t�s��&�
�p�9�V\b�O���ȓ�����<$����Btq���ڤbv�֎𸿤�}���ҝ98Nҥ�G�1�k���ipN�vLw��i��̾���|E�S���#وb�,�	FT�w��ꋼ���Obu�[����i�~n9�'be�d���)�{� �1}�t!Y�~����x�4y<�����;�h��.�N��-`����~J��Ps���E�)U���%����;�S��g�ϙ���a��.�^A�/�q�j����G�2���A��G?�j�$iKq�^)H2��ϲ�.ѳ�	���f���������e��e�rz�OR�6����iZ������T���z�kE�m��`����j�;���&���!�:h�	��k�����h���G[r�|	��W�o&����\N�S�/#|�B�U8��C�;���rG�k��{�����&�x�(��U�z@d��<U,����+ yK��:��;�������O{���V�T�H����Z?t�_��N��-��0�7"����!�|E�"fb�����%��e8_��հ���d���g�N��t�O�a�W�?)��{ތg��f�x¹�h�ٟǝɾ�zT�����*��tE���S;iZ9�:��q��Ó5<�nD���j�x�Y,)$���y�W��{��?�\>��uś�����_�m�M��}���$,U@:�?��3��t��f*�l~��A۔�9.���_O"�a�?��`3O�����C�-m��z~��Ih��'C�~A�&�t�lKJ��x��+��=���w5{�0#5b�sNTyXm�X�-n�\�@�h�mӥ����Y?%��p�Z� i&���N`ǖ�Ӷ��2jD��O	���(�GL�Q�s�?XAPY^!�t$�i@�̃�!��\��I��������'{..���eQZ�/{�iڰ�����a�s�K zu��R�Y:�pk�HK�K��.]�2��A� ����?�P�F�f�Y��3N����24O?�����h�3��Tg3��<����,�[o�e�Nֺ�I���� G�7�I���!q����#�r4��e�J���y�/{���R9�?>~�!` �~�P�p�	�ò�q��L|�d�;0��&U|Zs�W�e*@�h�P۵RJ�����3�(��i��p�J�%����G���tK�i9{��?�4�e2#ː�G��� Tt�B����:������FOAn�6�e��2r~�1c�n�a��C*-�E`�z�~�2~~� �<͵��7����S�I���]6r|��
w�en�5+~@o(��	��r�H�/;�œ-������a �O{>i)�[}�z�'l��73�wg���]}�[�_���5_g=��}���d�=J�[ZɅk�39[�n�N��&X]�F�8���a��M�M�{s�-�|��<�����Q����H��72��YQ�B]�xw�R3���m���(�ĩ�	C��%S8�6hZ���ߦ^�9 o�'�ť� �LL�p��5$�FrkK"\�!�3; T��l�o�x����*�t�I��ۮ���0�I�S>�a
��L��R�I=v7IA@_��~p��`�^˶�P�'�f�X������3E)����-z����{�����OӜZ̪����X�,�#ݱ〬�f�b�W�Z�IOl�'
�cl����x���5
z
����O�rt<k��;�'\i}�P`:��=��M�x�U��� :�c��l�>�[��g�����,IN��PSm�M^�G@�_&ݵѥɏ+&@����.{��JP��{cX��w��X[CK��|�%����M�C���,�}��0�E����R�=ww��XQޚ�CFa��~�"���B��V���>���6��ƍ�k�I[ep���G�y�
�(��?���}�v�E�u����V��ͅ3V��i�߾H��8�Fb8D�M�x35��$j�o!$�l���*��{�|�ƟC���F������n�`���6ߑ�p����' ktRw�Z�R��k�X�T�z�q;P>�Y�Eڌ�-~�|�,^�w�,	qf�E�F� 9�b���.�a_� �1��G�Vd� U<�� ��`1�V�#�"�����z��l�S�I�|,uoB�Kᵜ'�F$ڡ���"�U��Rs[y��K@�n(6,;ɸA�����rxuS52$�cYRH�S��k?FS�r�r���K^óY97[�a��	�w�솗�J�����p�jI�O6�_�Soj�,�Z���`��C�S���1Ge�q�|1�.U�}!
䤙������;W��}�Hӗ�]��[ǹU64�3sX��{8^��b7A��O5�/���OaN�f�'���Ә� �`͛�郛��1?���	��O0�:o��f��]g�`7���O
�/��15���,ZO���8�@���	Ij���Fڭ~K!y�d)1��/�׍�fF��b�7,��?~2��#ӵ��Ƙ.�j�3�ܖ2�=����u��w*����5����0��T���/<7��HjG�Rj�<���r�,~种�o ч:��da�_f�7����CF�����1W�����}�5�L��%Hu�?��Q\�5 �5y��f)r�V`E�#�� �\oN����V��> 擢l��kf�Т�C&cQcK=\u�:�m����|jI��,� dW�I�|�9g�zU�X{td>s�<MY�W�F�7��ݧ�Z�Sx_�/|C>��������Rx�����Oǒ�'��YeA���#!P�T]����l��ʚ�#��=/��[#���!5Olx+c�=n*|�'(��M�݈�
Q#�=
�,�(f���b!4ܚ��v��w�"b����V�cYs�t&���)���Q���7AP�A8�H8�z
x��f~�ՠ�!�F����h�N���p��:�&g�����H�景��n���ve�szE��}�bA�^ �$pL�?;K��N�7$Z&`|	�%����۶��b�<%_�`�u$n�%E�H~�\Q6O��o���F���k���sG�2�~dΞ�����1�UT�3��ˣ�[_�Q�k��ʡ�%w'<�2Lm���+v���Tc#�!Mj����"hI]�`!H�I4���s�Q&3���������:a��l�z�"k�@33�Qy��n�)gV�
r.M��g7���$���p�G��XR'����?�����J�忆8�s���A��|SI�b1�4Wz�9gT�)95���T4N���(�P���\�=u������#�t߂t�h��IΧ<���mT�V���Y��cݨ��i���m�C>���>i9]���!�N|��j?��_ԛ�U�\��Q2�D��7�6��2� ��A����N��П2r◅U�!i��YTA����A=����4�O ��?�ܦg�1�F��4�0����,~����K"O�xU|�Y�UcI�����hI�	���+TH��_��
<j! ;�P�J����>M�A�NE*��?���!L��I
͇�o��ކ��G|p�-�F#^
wĭM��9K�[Qþ"F7�ѕT9m=r%�!6�#�>�d����]ybɉBC�y��g�ݙx��&0�Ҽ��2T���N�9�\=kCH��."� C=|����Ɋ�/Nܵ�^B�F6�Z{��������c��-k����[�� E"��WCQx�N[��3���G�wEǨ��11 ��J۰[ZF5߮7�Q�̔
��ޙ�O	�(Ts~����y���&v��&�y���w��{PS��%I�1�    =�$q�|�B�x�|b렫/̿�bl�>�����1<��yWe���v��`������lO��r@�S��y4�y[u���I�mW�|@�ɪ&_r+rc+���o�2�a�r����uL ��+�+}V��X}�j�(�V�V�����~������Փ@A��'�|��w�Gċ�{��d�Wn���x��H����~5�%E:�cRnŞ�~1�"�+��*2m���e]ţ���x[���>����K�0�������؍L�o�@�ZG���	#]�nZ�pHG?T#W�g���u�ț;��|5�/:���'�A��3�k��8.�%GE�Z>��|؜��`r�c�sI�X��s�y/ۆl�����rx�u�e�R$'�W���#
�/��sJg�7Fr��b)�!�S���,E@C���V�}S$p�*F�+�4�3>�0!����KC% ���6Yz �ѵRN�Q{��6��J����g����d�����ʑ���@�k�C��,�ٺ��Y�న�h0D��D�:� .������\�BKHE8�3���I�l� �"���,[?c7�A�e�n��F��Q�Ҳ"���M��u�rצ�u���!d�����3���ܱK"�ms�dm##H�_�z<�yL�Y�ٺ�4ζ�B?�`{JB��F������yk:s:�o�W#5:o�|d�gܴ�G�����b\I�n���r�����ŭ�7|�@FJA����m���)Yf��|� ` �[�|�_-��p�����X�@*�.{�{ր+h�^ş������;�΄����/���z�e��a��$�(2�Z/��kĒ�y^���)O�~�ВhdVE�!���B���ˢn*Wdh�]凯 ]�A��k�����Ï���f�-��\�
vJ�H�i��
�4��8�� �vS1iʓ%�\��2IJط�QiD�-�_�4I5+i��_�P1�(���$��i�gM c������@r<> �^2
M�p�[RϏe��U ��s��꺍��V��>�j6p��kb�^�>1�-Sʩ��������<�a�]q�wP��4l���0bX�
�ҡv�UWOբE�|�����b����	p_�pr�65w)���L$R�6ZҨ�y�@�+�p&��~�oBm
�+ �/=��p��n��2Z�}
�ڱW[3�C�[#�J��o�?�:ӻ0��j)G�A�����5i"��}�/Ii��؎��EŐ��I��jSA��eU#��X\�m��NP�UD�Ya����}m?2����(��W�H�M���KE������JĄ^�.;%#$��
������}�<NЩo��r�#���a�Ga&r�w
�q�/7��oV*y�������k�w��qzliAh���}.}zme�_�Z$���f��N���;Ӈ�am��Л���&�nV�N
�F.�"���K%/�W���k�NQI��M��t~w���k+p�z�%����|��f���noHu%V���w���{�<�`0�Ap\��L:h�A#P$qm� "�����꿴��x��� >�=^��K�>��S���	n�V���l��0�� �Jv�)a����=#eZ+,Ӫ0%����T*�5�LY���Wz��V4��>TA@����4鎐|��|��z����a���0�â1z�5��<�X�l�
~���r�-8��yÿ���'�����`k�)�އ��O'�r*�e%?��t{��؛]އ.Ȏ4�0o��+���u�ɱ^��@��h� ��ܪ7�^nll�h#�׭ȪQ��;��Jģ����čB�O>&��?Xڔ�i}����� �����ѻ��
����PA�J_"(�[`v|wǏ:Z�ks|� �gL�x��i�Qo��)%^�on8Z�6�{�!�Ȱ��̾��֑��5נL����r�K�򷫁�!��������s�QO^���d��fYA�1�n⸕<��iGM� @�,_�W1�/}�v��a5eIZ���rMF����B���c�/�&\/TU$��%�[�ÿCn�7(�Q1�i���M~�\.��x3+}�Dg�Ϳ琻�#
�"��������$2�;X��v�w.�h����y]ڌǷ������(A,,=ِ9����!� C҆@i�cR!�2���wS��;�G)Pe	:tŲ��q/�0�̲rm̟PAc�˲�����赨����U# <>qy4$��[rM�u�b�Rh��Y�w��M�M'��UY�,l�]���;��l��m��
1�,�E���n��'G���Jg5(�j^�ڧ"��l,^~X�f޳(fI,, ���	��޵iW?�'�%��	�$��Q���`�TmA�!e$��sXa��Dv��,�^D�zss(*�j��=	}p~�L���m�]� �A�o�"45L��Iߌ�/t�;�����qq^�m�zT�p4~�b�%�H��`��>s�?�
���|
��ӻ6dBhp�m�q����O*C�n�tR�6�y���̄@匿�[p���Q	ksP��Հ�F�����t���|-a���C_�iט�M����5��3�B�V�^kX."M6o�mi�����J��]�I�z���r�� s��$���m9�N�Gt���}r�5����Kh�'�b_��Jpwi�K���4���?� ��mĉ����0��,g�i`�����L_s�d���Ĕ,���%-�՝�5O��k�{�X���>�^�&�H��,}&���7�����l�M�Z �0�#V*Z]�9�\�į_�hV��e�̺B"�&���|�[�❂����P>�ê�(�����kċ���pC� u4��N|�<B�/slܙ���n���J��E���
m�`�mp�ŰWQ��6�ȭ�:q;<|����{�tI��!��X	ɖ����}/��5���4�>���h�e��X����{��W�P�ہ��(HG��'c�!l�hj�+��L�f�H~&4JW?���=zf��Ѩ�:�sf8�vvd�V��xs��u�p���T��PG�ui%�	k���%�v1;�&1 0ģ�5[(N��bhT>�� x�������4%�0�-�c���Q7��1�f7������F��o�'���	>��4�:xΡK��>\*����ͩ�C{#z�_v�q�D��$��m7�D���Hz��g�qP�2&Q~�����c#��E1�K�"��JQ�@6(�?�g�æ26�i�Df$�d��{4�v�)�d#r������o��^�[��:=����U�n����0&J�{�H���u.5ج������+�i��]uҫ_C ���+,��1-��˚d��Ԉ�t��-�f`� rZƃ�g��tۿ��2�v��;_;6-����|�b�`Ֆ/�r�R2��i��W�l��hp?���q0?@�4f4�-E7�ؿ(�;�?�o/�l��9&"Q�:��"cb~[��뀱���7j(����.��t��:��T����	���K��YN�O�O���@�:�0��)�[]�n�~2�(�2���ϣ���+�t׸��ԟϺ��k\ț8$�A;��z3b��@cPN���MУ�Bf�}�}�Q�3�'E���!��S4T�멁��?��X?B`]�b(��jA��0����������޹/��Ec��t�-�[�1<�f�Q�Ғ���^�;c���������(l���ѯ��^02��'� �L�]�0�J�����?��;��98��.�?^ޮ�M����B��j�4��KÙ=�8\���f��:`�L>u��~�x�%���8�pX4��<C#m*k2,��t��L �Rǖ����y�x$��0[c���%|  9�4+�����@����k���� _](�I�(���Yc�1��F��}�@?�׿&;�	�0τ��ˋ=�*)��!�m������^�!+��B{�Èf��;{h�{]�Y(��XK��$|{	��R����J8�A��� (�g�'-�!���7x.}���;�!3Ӑ�[�g��e�P�*w*�([��Z�ҼG�Ѐ    z謻~8��o	LD ��4�∓�9Y*s��'J�J*^�v|%�f{1�-=r����@Q/�ZW�}O���;z��'���*3�S(ZsJo�ٗFJ���9��8g}0�p7�������/�/�����K������PC�,v��i;��bت�Q��Îci�j�ȅu9O7���ۀQ����YE�|ʩh��r���g���8��;}��,�R�Q��#����!�EM��c�z��JsVG�j�/p_�+��Xk��w�� �gO�J�l5��V3STĬ���&}�9w⩑4O�����E�h��g��=��~�����h�{��_��Y�71���ax.����?��b;Z-�� �!.�Cc3ܥqx���;MVR�N}{�����07L�~�v��ԁ�X�y"
a��=m>��z�r�F?���d/�N�R��|��9Е�A|q��a��Y����}2i�u��I�`�O���2ʗ����Q_���u���
��n�x���a� h�ɵ���f�$-�;^n��'���+,��yi�z���Ahף(�֑��V|Q�*�B�?.k�s�k����δȁ@:��P��r,���()bÍn��A�2k��|�e�vr�%��5
y����x0fɓ'�{�<RL� ��w�:�x(;�qv 7)� M|'��	���	��ӄ�=?�vW��:�C�16��"5A'�W58�*���>�_�T���N��i�d��
���V:���dH,�T��H�J����a�:}�{�hm'��&t����+D�t����OZ/��KEq�����Ğ�\� ������O�����l%�AT���>ϓ�G'H2n^(4�ӧ�}��O5�C�
[԰���5�3�P�;GGQ�5�R>�R�[�6��𳈉�j ��b�C;�ut�'�<�_BhN`A��MGIs�Ζ���h���2�_��n�/�Gi���ck�}p�� ���-PM$�*������2�־���aFY嚧hA,f��i�\�6~ %��H���v *ϱ�eؠD����ֲ�]�h\޸z�$x��lA��ԯ:B����B�h��22�z��L#��S�g�$�w|�KM������u/xk�Oc�l�4R�}��F�XP	JTU�/���1�7P�t���'f�������	��t7}m�0��»�}A��:��S��w1[{� 7���a���S�߇HiGͧ,��ۢ�Pw��;Z6��Nˡ�l���.�������xm �B�x���ʮJo"=��	�,/��������B@B��AW1��&[�}�?^��M5(ꮷ�Ϗ��	����x��T��.�|�c���^�a 2����y�oUl�7��B��~<�.�� c�IE�օ�P������4�H�Y�03:�`U���C��D�eP�i)[��oP4 0ȼp��	-G�h=8�~[�6�]�(�_�,۳�h�N �o�m��Mc�#\� f�����B��D�rkayF�VFW������H�D�}�+3ǡ��L`�L�7lIt�h�JU�����k�7�s��Wc���:�i�|'Y=dGHԯ��J�DEH�v�ȑ<G�pkx������me0H[/5=��x��{������0�d�zq����3�<\���v�����r,	t�m%�Vl�ёh�e��^6�??�=���.��ߠ���a��zK�^o��������3����n�f���_5��'�24�4R�����6(m{�=��voկcs/?z���Z5���܉���0���oY�$��:����^�%�"�v���Q� �̊�#�P��WԢ&p�]Ww.�&4� /��;�}����g9�ݹ��m��he"��;�@�4��C�q��DhF�e :Dv���}�"PM�M����I[�1��Ha�]Ͱ��~�C���̵a!��%t��Ҥ��Md����	��C���L���ч'��.��s��_���F�C��I��:cy-�䂝�'e�ixĐ������7��2�]���9��P�pI��ܛ��ɭ�v���ɒ�F��2�
 c�T��1f�������r`=�0��ʥ��Dؔs OE�ӨcS����I��j��)���3k:'A��ΈA���)��� ����8�W8�/��XK��[D�Gm��hX z���)c��b$��rU�z�p���*ST� �;���d�E?.4�����_�t>	I3޵Ί�Z�^)37G�Ā3|��} ��=2�_��fp9aic��5D@��`
\�bnС����7�Ü8q.Vy�̚%/*f��I�B�~�䖣��U�g�ڎ�W�{��\�P�;�A��o�:Ũ�]1�����//�,/1c;v�)��O����B\���|�8�wh1�=8[�N��߁�?����"��i������ܛjE��iiL�j�ۻ<zbS���G��Z�aи���R��[�F��A�2C�������,ͨnlc3~�
�I�RHD,��隄 m�V4�P�H�
W�A �9��փ:�a�q#���<�F�mF��}���X+��/Z̯�~ѽ���61ڴ�1��}7�ax��b�~�;C3@C^��՜�����X}��Z�jks�-�eP�����f��V����l0[u�Rӗn�>�Sر�6��߅ɂk���������.	h�)���-n�T��<pp4�� ]aD�g=�9p#Su�����\*d�z��&�s�!��F���pj�y����|�+��|��j�d�#�x��U�u��ֵ$W�'�cQb���ӓݬ�sȔb|�EXY�(�#NR{�w����$�L�G$f����I�!������c&TV�[8n�J=#���0y*�Z��<7W�lY'5?�Z���4K�8J%�A�O�j���~�[d�š@���!��eze˙��^����۾��&4�؇s}<�2D�g�˛�o<���a�b�;�q�W�{� %	�[�=���챷x�>jF.p�+���-_2�=����%��-/�u&UAv.�h&܍Gv~�K��Rs_轞܌��*^�)��8�U:�x`I�|��cQ.�S�HR>��:�0�Vn ��pk�
�0L&$��_A�=���B�Yf�A�1��H'܋�7�	n2n�]>�E�W|�r�h��p�G+�8c�5#)Rm �翃m�bݛ[d�8��Օ�VMX4'������5m��ֳU>	�2�ͽ0�t��4ꗯ�F��K^��'	��>��u�n�h9���w�-�$�,t�V�]'=�Zc_�V,�XI�������o���}wiWڦ��0t
g.w�7�M^�)�ތ( ����z�!8��C����c�<��-�I#��8��]w�w�y����6U��4�-�h]C�7.����9�����K�.��*�?�����a�V����7�KZ���!@����N4yS��=��p5Z�7װ��[ N��m:���qN� �n�ѷ�$��wX��_}��fQѾ|w�HF�'�q�Z_B���h{ݣ5O9=P�<��|J�h�D���h�9�#��O�Ab�(A!JÇHx���LE�Q�������p�`_	���h͹s�_�\�8|���u���)F?���ޣ:����D%��I���xC�/U��s-R�̄T�M߬�5��cq���Z��O�]MF%�/��v�y���Q����rO@���%�-��yqgu2�M5��+'>����/�í$0��_�83OK���Sq��ċ�(غ�˝7��n,*�ް=��	f��_�Ή�nݺ�-O�=1��'+Z�-�օ��>L��A�'iJ�}��@m��#��~��E�<t�GK�	����� ]7x�W�g�Px�`o΅�Fd�1t�-���PȀr�`�`$Kp�&��J��ff��l#u��v�Ǐx�����YZ!R�0M�:��i�1��(�R�^�&z�_�/ �.���L��Iv�G���s�*����׭�Z�Qf���:c���
�܁�Af���hu�#l;ϔ�2
&!�����+0��UI!O��5z��SWNU    BW�*-l��	g~���2���!|L�$��N+���U�`�M�n�s�4YB��j,�ڷ]S�o�Ә����^������-���x��<6���������<E"TL����m�.uAJ��UA���C�$�J�bG�����a�3�T��6{*�I��P����C� 卖G2~��-�v ��1�j�Nk�0%�RJh�5��fȿ��;�S�
�QJ=SO�KJ�����@�/#ރcTA�7�����2%����V�2�.�
?WР��/�VV�z}P���/o�ܨ���%��b����L�X"�
��fd���ɥ����b���v���ޘM�V�))W�^�G,�h:'��c�um�cC'׊�L81�#Ⱦ$��Xȧ�M(�^CI����š�:��?жP�x�Q4$+�i�����iN�Y^.Ϭ���m�g�wޟ���&11�Ȣ��V���ߒi�g��1����GՄ����u�M#�1����6���Q��Dn�D�o�{ޮ�eJQQ\�R�Q������FE��˦i��x<�#�6����;y��(�(2�0o�1���^{`9j]��rW�L� ���w(AC�J
Ӧ+BM���^��Z����
)�/�� Bl-����ڵ7Jˇ��^¯����']-|�5�����S��0d��F��N59�Ir��}���:"�0��ہP8�bI�$��>^�E;ăO��OOe�:�ϝ[��9eOh�i��	狪��,$J$��s�kf��~1(k�����|�7�����(ģ�3���`|���r �@�4˟*��������$����1�����]����s������:�A+j���q��=pky�0s
7/�1�������UntV����i��k_��"Wd�&�` y^>:J��ݙ�(?���]Pb����f�r{��R>䃮3!���_tmd�3�|e���vE:i�ur���~:_�JkV���[>�n�F&	2ڱ����%v��q��Gm��:��W04��w�RcllFS��A����8Y�?P�:^�z4�˧�օ �曡�v�c;.��ˌM����F,���6At�3��P�����Bv�F)�Qʎ�X�E$�� ��O	ݯT&$U"�7pw��%��ԥ�AܗiGǳ=�t�N7=��sL�I贡*�����J;�)P� 챨ߞ���p{��N���yS�|���`���zv��([.��p@'�{n����G��*�9'�I��XQ���R5p*L���٨�D��0����t!���'�4���]��Z���h���A��'{�D�ޘ�?J��W3,O��nU�0ps�=98�}�#���Au?���e�%�1�~5�y�m�3����zr��q@��/����}m��2#dH�8����<��_fp��ڧ�GYCm�~�[u�����in�h�@���I��]6w${-�i�]�w��c��h�)�<���G.�����g�UL��a񙆮{l��н��-��h�QibPG�t�|+S�; B1d˘%Yx8Gh�f��R��|>1d�<�I��û�g!y�WZ(L�Ãzg�3_�:e[�a��T�Я� �Uzp�Y�Nw�W�-�/6��+�']����*�9Z�8U���FQ����ƎQu��r�k�;C2����4���>����U�vμ�W�.���HIhV����7��O.���P�0$ef�я.��c	i�8CȽ�>���ݗ�������H�D��BE�{�?�9��,�-�z!���c����m�)~�lϛ�:�!o�_�цV��IבFm _5����6Ul�? sN!<�lDlI̬n���V���ZD�bZ��ɰm����]f��h2�Fk��Ҟ�0�t)��o�$#��;
)���,�X�;YMѦ�$�2O�8j���D�����^e�^&L�_�M	�]�}�QW�$|��
�?��A9�曢�;��3���_���ǔ.!��B���r��ɐx$���+A���-ͅ7�X�J��L�߯cg4P���}���2���:H�O�O�DQ� �	ک/���l�C��8�-��Y���*�%����FIP�H��E��7����Q[�/@m䢟�RO�m��=}<�5���ȣp�y.�+��r�Y�>"�42�z g�("�C��\�#rD+"Ѽ�n_u���}��+�;��Q���b��"F��tQ	�V��A�͍5N^���-�M�W��͔�W�,�L��01�+�+�qG����%4�3�ɹa����V|����#��:b�U�z�R�{X�hz���ʍ �_�$>�85�+�2	Lei�]6zo%=ǆ�z�n�7���M�>�� ��e�5:��7�BO3�!pG�H��7t�*�~�.c3ڗ[;Hp�%�!G�9���nDyo62����Q�����$>i������6�bBQ��WD�rQPi�OӕKi�㕄���%^�`ZY?�ݩ6#�΋D�-�"���]��K*�>#�����K�F;�WE�׹;i��������>�Gҷh�nV��k�7�V�8�<\H��o��)�΍?���8>��~����3<��y^�D����LEJ�.�psxV,}��;�(,p̈́h��Ji��kV�h�\����i셣$>O���$*ݼ�Ƚ0M41���s^3A3���)�Z�'p8�4n�hz�h��m�¡x|D-�����(u4���?�p�H�����EyG`�?����I��W�q|����w���u�⧚۵F��;�e���;O\iib�jhwRG�}���?�_%�*��p�����m��$q��_nm���d�L�g1���2�0��a}�[��X� ���n��sc�Eo���}�m�[HN��/�}�~¤���t�ݝ�[6�+!��������:x���+��6�����'G�"J��pbU��h{e��X��s8le�E�އ�~{�j�8E�l������\z�jZEy�z��{m���YE�2��k��r;pq��GT�ò�eJ���,LM��m�LY��ŧ�A(1<���{���������PE~@P�NFRNBl�2�� ���H�>Rx� ��U��-(�� K;��&�+O�e���-Ag|3/e�$!mPR�D�pLܳb`X|��P7�o�L����L�˿�B�-?���� [����{X��`�?��xǢ{�Кiq����fp��9r�t��s���Ls}����.^u&}Ȥ�p�
!*�в~P��0 |	[ڃF������ƹ�5��#]�
�qw�TFe2M��H�7	V�����㢺5B2?-
��^��Q�ɹ �h3��(���w��C�Þ���aB$�d+W���t���rI$��N�RQ�R>E鞮��E�s�+&��F2�L�&�͙��99���S��	��oU��0���auf."G}f&���J�'��zm�f�o$x7=�a��5^V�e��Ӭ8}�uAt!,�dc�r�c�w������<j\Τ&
=k�i3�y�v�qf^�9�Pq���SA7�@�	��v)����E��@YBC��7+���i2�^k���¬5����XkP��^0�����lDrD�y_��H�NcNT�*ayt7)ߴsa"�s.�bI��s���:��8���A8r^A_d Gs#��;�f�f�x ��J�H���_�>IwL�)��><�g/�tN\�Gl�h�aF���h��ٌ��#��U\��aޢ����@�"��C��O]������?�sb��0r���5wքGd#��2u�,9f]�����q[�Ro�d9�Tr�yg��5�� g ��P��ޤ�Lk�/�*���J-\�+�_�0�����Q��!�R�����ùWU4 2�5Ԧ�;,�x@T�ѥ�Ο����zKj���L����UET�*�a��Iu�G�f�I�c֭߿����/�������{�a,����<h��ϐ�4��|y]���B�/r�R����*���d��s5���L9)b@���2k�u����� �90���B�����i�.���~���e?���41䗉��    �������E��dn�w@�-G��hGC�=]l�o�pF��G�I(��'q���#��qΔ'������S�gl�E�,M`3&��hTȷ��%��-�� �r�=��t&�[���Q�~X���Ƥ��'�d�"5�1.FL��d�dU�"�骊�VY��s���"�~����,`�ey�N
��4}�.��"�)�L�����W��g�g�(��+�h�΋;���Q�p��F\Τ���)z=Os"Ey}����sR�b���h�s�?����t����R��9���a���_d����d}�c��3���#"|#�����?�TQ����r������'�R��z�A���T�RC�"5	��o���O��n����'RX?�ֶ��I�dp��%
sM�T��R7C�N����C�������I�>+��52�MUJlj9�(�1P^���@�9w��xw�i�jUDw)��gY{kY�^q_�XV�I#Z�[�v�2H���`NI��ŌUyC�6����z���ч���X�PU���!Rwh.r}�3��#'��I�|$�>f7��%4
 �Fm�4�(Qq��ju�3!<gW
q�Ms�NFM]��~G�|���KJ�ǈLCǈ��!Ӡx�q[">E�U��?п'�VC��Ìʨ�� �_���w��%�-� gd�<&�US�2�5x���~+��27m�X��xK�i:�J#$̨\�7UX'U�3���xw�Ɏ�&�t,?����\)��{�#uoG�,�5S�f�|Q"utY���	[�Ң��� ��=���g�=E�pYZR\ي�Ѡ������w�4�tK'B�.����O=�[�N@�A!T�Oz�m �h\�� �v���DM1A��hȷ�/�}\��?�������ϸd4�Q��N�L6���t���^H�H���6\���XF�6_��
��������Ai�PG[$Bf ����A���⻷L?`��nߦvո�$ߋ)Z6�X�:1$�uJ��b5U[a��3�|�O	�����\q�Uͽ�!�+w��ݴ���XE@jM٘+�a����!Nzj��A� Ni�aI2����Gˈ6��oxi�}5o�����KilO>�����[R�Z��ZOpV�3EH�VaC�.5�b�s��>��i������@�~;�8ڢ,����{�?�ɀܪ���_�
c���M���[�H�Z��O�5ctzY�Z�v���Bo�g���4 �f�qSM˂�ށL��A�n^��lߌiD~�XV�H��� ��8^���A��`��א�9L��I
5�lt(�PoS��rZ�@��i�������Y�6C����n���e�L������zD<@����Z�nޕ�pp)N�!��eˣ��w'�Zb��Ĉ"�җ�fT�a�s#}[{Y�f���琙r��u�ϑ���-mS{�Ĉ�L_�	:Y�I��`����2�S�bB �d��/9�2J��ͺyG�	&�)/����tv���}�����x1�U���A�}���$����� ��Z!Y�< ���7�3˫�I��YjP�Y�7��ùu�qV�!|8�$�1L���7��.~pe"��7ȗ���y��gnNŲ�+*�)t��F�})4�D}6 �\�LH�bt��y���+\�@K� D�2�s��ѳ�z�GST�̹0yi6�����̪���Qd��X�,p��i�t�{������-�e�Q:M�{$[�;�������"�$C��K���$�� ���e�������5����}��I7���T�<C�Vb�D$wn���֫�w�/��uǋXٯ�����c�b�=I�(���7���M�	��W�S7J@��QF�?#D~^M��_�:���ܶ����2�W�F�K�0%6xvh�y {��=9x�J��fih)���]�%��#}t�t�&����}伟��b�H�w�����q�;��P9@LS`�2���F`S�����%��˹��S�Ab��vA�d�O�́=�50�Kb�P�j�:�x���R�8���
�+�ɫ��<�W�Ie�&�фv� G$��yHDR>o%�e��_����2�>h�FI���ϗX���G���}>�uV�(Nʢ�U3�A�%O��:��ؾRC�v&iy�ymap�N�l��c��[���*����6�"Y�+y���gw$J����~�:��xa��F�Na�iIW�j������hn=,�����dh���8ѩu��?��$������7�ZK���U�����P.g��W���t��Q�v�>���I�V��3�07���!�0��D�
K*çg��9�p�kQ�d ��)���'`�I��Y�N�+�^�2��R<T��{�@��@I�L���[e+m	��}��Tn��/!�95V��*
�v���
��@��9� �B�G�n�Z ��4�X�ܖ4J-�����XJ�V[�8h��N���᪀��m�n ����	l�mE��C8��]8��$>t�N�Ђ�,}N� W<;�C��X��	c�ܻ�= .��i|���9|r���1��<$�ͧ[{l����H��a`��	�|˻�N�zӎ��'r+����i4l�S�xx����ۨi�4�� ���1!(���0�|X���>���SO���ګ�-X���#�|A X���O��~�6���Z�e:qC9
\���;`s�f��!��w����Mi��e�Y�n"p #�TyC���扟������.�*��UP.�ǧق0y�o��?��~Lx�&/#�^�F����WY�E��z圓<�M�I���:�Ho���ϲ��%�Ge��!P��S�A ��/}�$����Z��Q������7ˉz��<�Iᑢ�@���Sz��ז�FUh�.s��<s9L�0�JG+�&��7DC&�v��p|�U8�9�]4�O�q沟�I�ש�k���&�[?u��\e�Ҧ��]���$���9�-f�!m�.�C$ 6l	(�jU��Ht@��1�̽ccF\�/'�llS��j0���˦=@���l3R�~����tbђ����R�3J����*��g7�,\�%0'gނ[qkt�rS��%�cǄ����,x1r$��w��)j_�څ��z��/:�:!O'~
n�oNt�H���wyŉ=
�v��d����C���h��I��x�n��b�a�r�B��w(�I\	�	��}�β�!����k�4%��-���P �%���� ױ�~��%�b��\rX�4���#��Jpoԟi�(7I���a�H`������a8{���Q����~~z��p�x�X@M<m2�#����2Ԩ,_?�+�������iT��aeBH�ҷ{�}�(�f����N_"��,�t��)>ݒϞN�����Y���KU	 K����p̤�̏�,h6��J��Y_���%I�C��n�;߼M��-Wj�(����u�Y�9�]ٌ�{�R�<�o9!V�o/2)�*m�ݑ�_��NVr�C�d,�[$��^c��2z���r�
��+�G�\>���X�MH�٠ĕ����o�P�p��M�:bk<�Bj�� �)�aҷ8D��y�����$�o\
l:>t��ޏQ� ��*��"��=iHTI��ږa`�A�8@C ܵO/;���%D LZ�E�/Ap����L8F�=������IvC����ʫ
hF#f�>hpۦ�H�p�~�oٌ���	'UF���o�utl.	j�I�.(M�R�!�/��W�H�EX[A%/�5��.[�m�wD�l4������-���O���+kS��p�p!�筢����[�n~���\@�Y��~�[g�ׇS}'������ 4� �2��O>�lFO�����bӒV���Kj��4y�)?�A8����¿ӝ�3O����
~*NI�BH{��NN��Y��oÉ�� �´B��t��X�f��njӄGr�y�@[��ZeL�a:����Em\V��2�bB�S�+�a"M�G�1ř����뀒�]�e��M<_�J*YtÎ$�<?E]��`	���Ò(��)����!�*�9��~��7�mi��0�    ������&u��o����Y�E�J`�a[ԧ�=�5s�~��F4N0jj<!�.��T)�O{��s�i)�ߘx2(�i����2�������W����C�I�C�{չ;-I%��],��I���4!���1�ǖ���	����m��Ն��c??B�©i���:��Ku\X��Y����'�l��6��Vl���m
 5\ 8ҿ/�'|����Oh,�|{�BY@���Ls�ጰ�_'Ty����#�5
�X	��ޫ9���'�n����}�������ʁ��*�fbE^+MW���浽zJ�H��Y�H���(j���pj�U�����,����(�\r>����v���j����)lw���7�h�)|�J� R����x�\�+XϊG�}���K��;n+*e�8D:
_��� bp?��chS���3�Cf�ߝ�>��R�U��S���g�[<9��QAs�ܤ�S�4�p�G���c.<��Q.\+����(��9�M�l޿ Ԭ�6-��=�,(�HȲ|Lx���O���'rc�����L���覊�,���w���f��{u���9ѭĤx/lC�C�PQ��s���ՈF`%���"��(���i�S\��Xj��~6��mz50���H	��ƪj����kk��������fcV��܉����2�M�,����a$}�~�"m��5"�� ��<98�ܴoz�;=j��B;L�z3��B��;w���X1�m��#�S�/yWg����2��4���=<�i{tF�dNa�:��H!=| ���\c���vaH"�G�,�xߖd`��M�̚ٯL�z�W��x��,���;�Ƴ3p�g��6F� |pUL^l��F��"���6�5[���8A��4�bA/�G�qkq��QM���onF�:^J��J��4|5�	�x���<��(�A,�iI�Dؑ��Q|����ɸ�zu/������3��{ �q��a��x���x��ɒ�k0�1/캦���8�k�}T����A�^� <�x��{fp���Ex��N�:�ۅD�m_
�&��߽�n)w�3<�я��^~��#2�?R��,�f��T�T����m~o'� `�5n9Ke���(��ى����FT}�ıD\,:jt�S���#�N�r  ���.�]�n���)��F?(~
ڰt��P��V�F�P��\�p�N!�e0��mp�\����U��CYԈ�Ce#���NxW/�+K�4\=E=���*U6������}�{�p�e	S^M�4�����q�x'���E�E��5��0�s�"��n7�/��E�٬���f�Q���,���ٛQ�>#�؝����:�I?�\�a���=�����N�a~�ȜP��;��`m�\��V���=��y��:�1(�/��_��U��떆��B�I�����{���o�͸u�W:Xր� K��a�� ���55\��!��`�puR'�c�,�F�J'29��m�����Fҷ{��<Ru_ۯ�� Q2ӄ-�T�*���_�����1?�t����ʹG���'�@kC���#Q���
��*:�Sg��z�ƾ`:Sg��K�]\��Z��8��u�o� �����ʳycL�T ���4kЎ�v�\�Gw�3m <R�0H���M������5�'ԟϷ@!�H(���S�t���%k�3��:���'�z���>�nå����b��*��7t�JC���v�{�!��K�������s�
X�J�E�=T𡕇p��^�z�R�,3�'��8Ⱦ-��a+E�Ę2�j]=<	D�"+it��u�V��iZH�`�0�` !Jrk�~�g7J���.�M)�ϠR�y'�_O{7ʆ4�R7�E<���$��`ܮk�b8�A�_W��"qn�3
�_/S�=���넔k�<��ʖR]�Xz���	��u�e?�۴t|��Df��`�$�9�hCY�f�s��0�B�����O�B��E��y�gi6o���0/d��!L���D���#�_�؜%O�~=�=�tz�=���^���s����e)��>��]���O�}�j?����ms�S�c^p!��z uם��r|�t��SN�����`eߗoI�����nm�ͪͼP��7y�lɜ�U���"�������9��fkC$��6�|t~���Ql�����i��h������+�C��EXU^��4|�ϔ��mz�*��#e/�E�*�QqfB��x��%_8 )@b��6�"�E0�.q�:���$.�϶���;��<��P
�oC\�xAJ��}��F���BF_��'��������_�2h��z�B�ڱ�R��Z�%����?�[){,��%�$�V&q'K���pg#�Ĥ����O�� �8�se�S`�'�*��P�!^ �w����
��Re�OgP���ﱄ4�0����"��l��k�]�I��� �L�t�b;[#�x����,��k��,���|��2�'���^.�
`�n�D��*	^o�F/���/ў"�������M��\S��i�Ѡ�t����������y�I��a���h4E����{��-�m���]&q�}�X���E�ܴ�o�-��<o��� d��6O��ޖ��p�O[���B�+�z��~���P1���~?��"P�`�g�Ȧ��h���;��}�p�����H/L���3�|�	�n�?)��+�ֶ6��y<?Ԃ��p:����������wp�*�"����kdb�3��e?�_8׳�u�r&��}�5��m��g��/% !����������'�vYy�f�²0��ޥ��6��opRR}0L4&h� 6�Z7��,���QЄ|!.�B{��-��և0��?��nW�Kxj�F�"5�x@��0
ê� �3��!��0T>��Â񙤙hqf���ew2�@�=e�~u���P��l�9ٚ䂂�m��(f>�
L.���z��^S�W5Q��lI� 1���z����{~#�8��;����+^�׈#9O�����Z�ej�C^���w&5�7ǅ=���8����ga�-F����ڒ���_jO}�h�Or�Д,��=
 ���hpCKf�|.E�?)�%��~�8�:D��"�)'?5��RP�h"{]z�Qz���P���?ϰ��//�z�CG�t�����#B ���������+Qۃ�&ȣR|�B�cX��y<C�������Ձ25c�IS���'�%�R�Η�n���J1�h�d+�'�@<�6�g-@���?��'�%	Q\E�i5}�
������D��0�R�����#<�ձ�[�Ƅ��ῨF�t��hFEH��ӗ
�7�!�'��L�D�%t�r�xǝ:����Gy�`�Zrj4��YjO>�R~� ��V9��LC�;���h��)��k�Y�,���ʈQ��1�R���FA�+
!]wKlF�K�%�\Y���oO&�Pho���d�i�W#2j�u��ԋi&��q�!;/m���X��6-�ߗIK�(���A������;E֞�/�{��H���nq���FVä��Lze��)�z%���Q�Hu#<e"�π�+�S��z�t��2����c�pE��T�M��F����r�
���ľ���*�f-���Y|R�L��*��h��hM_A�ga5�K�Q����)�CE/���o-������E�(��"چr�L:�P)����Y�J@�i��-�b��� Cw�
p,����_DO7U5F@~V�e8�f
�Q8��JN\9=�AF 6�����J��H�c�u����J�sپA���Em�D�L���-��&�yI�ٻU%Ǒ?{w.��tq�!���/�6V�y'N>O]�B�,�P�iT-�9��'��u\�U���N�h�ϱ��-��uy�KH�i���n��ʒ[3Mt3|�d���YI� 7�֫�P+M�]�}6��/N�Z�e��7%��O�xUQ��HA���e��'��4��Xř~p����n��BB��9�    ^�Vj`�-ps ��(�ä�Xǘt'ދ߫��U�Q�2�(�w��®
`����سY4�2�
�޼a67%��he;i�e�"�
9z�)(iw}���i��+�t�hW�Ūӊ�W#�W�4P/�Y' �D\Xh�����K=`69&X�ߣ����e����5��t�
V��Ɯ��x�ɮZ�9��iʢ-\�4N�0���7ܟf;�Хb��㷛����Iz'�mw�y�Ӈ,��E�x:��G/.bs
��3(�H��7�\jv�U��{�W�ܭz�w�=�eyT�mߞ�+Le���J�3޴�;��*;Kg�ti/_�w�h�9��;o\��l k�wTYL�im ����롛=q%�5s�)Y�Ե��8f��}�����H�1�JY"�7o��H��qF�හ��K	��`�ߏ�kRs���%K��}�RQ)�я\~Pͮ��7*z,W|�	�E4����l�5�S��+ťus���ߡ94-u�C�L]��d��������w�|O�Ӏ9�Qv�/"XW_G�f���\��?_�#P�b�|S�a�0I;	�)`�������u�ۯ"T������(���1.�=��H�OY��	��=u��K@ی=g��H�%�3�"�'x~�CMu���Du��:U�#��P_`�ȤY.����x�eyr �Rtڣe�, �a�^�`i�K%�A�
�{��Y�r�Ї��^�: R��H��Wl�Xۋ.i�7���
	!��4X���
/1mUhT�W�^,I�(�A�bvmS@�&靖P !͎ŷ㌀N��g��N鮼PC�oe/�X�\I�ZJ�O'�Ƚ�A�P����3�{Ÿ���v�;z�ꈾ�L�G؉���G�v�>&̐�Aϑ�dN�b��s�݀o�:hw=5"�HYd�[f�xIH���������ϙX�����1-��[���to��H� ���
f��J#��Ʉ��fܚ�.�4l��mr�����Dڌ��"�.]`Av��a@��/���`��bn��]D�m��a3/tmn�zB�E�4Y�]Ne�T��rS�c����r�9�z�l9���wE��^��!��g��+6�WՉ�9��t��pk�����t)��-�����M�:i���g7
���j�4,$�l~� m �kv�Gĩ�Wbd�\���TѿI�0c�3���4��m�� 
p�5\�F����\��=�-�qVֳ[�셉�Hp#?�p ��v�I���<���'�i�u�T;o	9�k̗j�y7��;d?|3t�"O�0Q#^�4�]�c����>Á0T~k�3U�E}�$��{_��Y���v���C��`�Æm%�ր���[5��zq?K5Dڦ�,@Z�)f�3��Ǖ�J����؛��9�bY��1_P\�;(km��rG>�]�SK����NBE�:I�9�O�L�Q˳k��Z�RE������{Ct�Cc�&�ί&YJ�}�}9���+��=�n�VI}r.*1d���X@�O��)	���Pݟd�l������c�:T�T}@W~x��l��H�`8��iJ��"�5��}v�,�::�-�E��7�3q��#������N��פy~?K>�$P�b�֙]���J�o?V��?߰�{1d`�nʚ�q0\FV~73Z^z���ے��&s~��ܝ�LJ 5����D� ���%y�m��o읣XΙ���hb��K�2�>JM����f���y�Yv��J"���������_#ZH��t�#|���"`ff��:
ߙ�T�APTh��T��P1I�oD�>~i�ac�nm�jR�@�s
 �7�l�隖�Q/�yp�X��I�x�ٙ�FW��].�v�#D�'�h���:F�m3��g�7h#���g���N�sJ�g��4gzu�J���hl�'܆+_��B]�4�Ij�Ҩ�R���Y�J{ٓ3�����w�֕󕟠K�?y���� =��ݓVZ�~U������M�&�����c<(C���1�ȏI���O���*Z�~b�ZL�z�νn�LO���\�6B
�� �	W����� �x2���!�hR2�U)��g��iC��V��#怞�o[n
�����y�EE�k-?z�j5
n��}�ar����/у8c��G��� ~�f�\Ún�f�J�q&}O�����q(N���9��U���x&ᝊʹ�X䆂��?7��}�%���cu�b[�4	������ue)=���l�B��1K�fܚ���U��]G��P�5^�=��{k�aGZ�-�	������#���GWz����`0��D��:��(�J5�h-�����tW*mI��k��	�c�ɣ~� D�#')��G&Z;6�IS=�u����Q����_N��¦@evA��;��,����/���}��x(Ugd��kRi'q�__XfjmCY���"bL��ޠ��J�ǗZ��to<�䗐����4:�Joc|($���9�b����$��u�{"1�K�R�n6�.�t����G��L���QF�6����}�c��9yEd�h�u����.	�E�b� hL�L��xヸD�m��Q*E^Yu���A��[D:��S��~^�W���������c�U���1EyYL�<K$BĳbR��R�Nw˘��*1r2��^Μd��a�=5�vTԥ����G->ie�'��au%֞�<��d���]��]��C�
_�{���vg)�C9�b��ף�g�P{B�M���� ]��`�ޔ
u=�8Sĥ4�v��=O�O�WX����3�o�uº(�}&A>[>Li-=������h�bs����D�\��៹>��'����Z���b�����[�ؤʬ�����D���v{W��l�2n�wI���Nj��4�r_\�u�*�h�%9'����l��g��$կ�<=٦+�o�r���i�x�	.���S��������f�M=YD��$�T�ئ Z���"f.}L��t��n'n��L~z�pfDA�x��zʧ�T�B��n?�<�v�੘��*:$����n�;��|�|�R�y.ǘS�A%��9���ËN��Q`y�0�WMM*j��@e�Vv��O�=R�;�G�=����L�&	ςC���O�²v�
=���O.������a���<my� �^���� Z�Dۙb���:CSy�Kn��*^0H����w�% 3��`���ڃV��$��񇅜c�㖝4�
j��{���4�>gEP����[��!/���w���[d_���ã{�)&X�"1�d*1��J��Ȣ:�(.��O�Jdw(�i�T��s��ƚȦ���+%�� �5��'���^��~�(�i���v��	Q�p<��<�Qy�ߞ?�gXv���e���IV�����erb�	�O��| �u�9�*дqr�������	8�s���$�o��{2s��d�\�x[p�����q��,5ڄ���nu�\�*Y�{#cR����i&���7�ɺ�ũ8v��P��2y��U.�iH��>�hS�Ƈ��uX
�f�@{��
������<`�(�[���M�o�O`1f11��>�9�뇜����P�Yo/N�Hg :H��rރ(��w �R�)�T�{�^q�X�Ȋ{�W�$�����*!���n����f�8L�}�H��B����vi���WD_ৣ}(}��+�޷v����o�P�תl�Xܬ�N���Ւ%_�U��f�f��v�J����!�H��K��-�E�z��w��/��<w�J�� �4��<?"Z0X��c�1��{�ۣ>��SQ�"����� ���N�ğ������.���)[�Ws�f&�ptW�[w>6�Y�S�y�*�֊i�g��V���I;��o��IU�YLM���j�����CɣǺ3<���of_�/��É�`)������7og�֘e<`7�#? ����'2`���5���l��ڍ����h��.����_�|&}��e%�,���?Y�u���s�W�˵tV��&8��۷���H�S�FY�Lw��v�'��    ��#����{:��A ����T�Y[���-%C��l�;��0ߚ�#��A�D632��n�P	� q�yW���c��	[��i_o�ke�|`[	jα����T��b����~w�+����;�?��,>G�_F�>��+���Ŭ�'v� fIs�#�_']Xh�@� ������+�����
���VX_�P�����]�T_N�Ll��f�g�Z�V0U5ٰ��;�l~�m���{�]�I`���,�={������Ļ(oL�����-
<���⚘ r6����PD�.,~/�77!�=�>�eB�!%��e�hm8{}`�B��
�E��c�t2�˺~��:a���!]�]Ԃ��q5�,M/fb{=��)�U��輅9/��$}��m�t�2�~&b�h��7�Ϟ#fײ�|yE&^t��Ԝ	4kg�4�Z^���!��Ww�u�/�k��e��d\�J��k,�p���w�IaLq�Z�j���g=�d��4�d;��%��i�|�u����u����b���c-�^����w̟MIXm5�3�l�K�qB�u+�}~&4�����;�K����)�/J3�g:��6��[b0�9x=p��碤L>��e?�G���D��~�W����2J68�{.�$K���.Bcl�����:�[���PS�'�:�u�%�L�Xu�e��O���X��!��ч-$��Ww�5�_k-K�1L����#@��`��������S7L�2e�o�O�TG�������L��Z2:�/�O톱��tfMw���_�z�fV����˭�i,n�ױp�)^�Bnjbjt=���$#�[q�(��&�#���7�m���f 2D��s:1�z��L��P-C��r��X���
 `n�^�&f�r��
RƱ��*3g���AٯcBmx�?�||�X�{�7��UҨoq��N2���P�zȋ	H	����fy�q��ɰ3"A�ݡ��8�sg�V��H����Sd�T���r*d�/8~A�i���XV&�W�������6V�`�9j�-�N�� �o��)L�2��kـ�B�="2K��q4�6^H�C?]r�^+�'\2�A�Ys墇�d?Aow��C�j�+��2D�`�
7�v��� B6S�Z��B��֠��I��ˆ1V �[��Q��\�LQ��Ԇi��Ѫ�����9��Z�d� �s��v�D�U�X
��\���$e	Y�Y|�OeۼJ���� ���;��,�6t)M�	7L�Nx�xA&�!���5�E�;�A�b�YߟS�hF���\�lo��W�q�c��~��[����<�A�t�H��X�U�=��]��䙭:m%H��`b�̎�Uߞ��T6��37��w#�~�3d'��}{��\E���`��&���%0��ٓx��6�_9pQ�.����+<��4r�BR��Ef���kh�U�u5���ʱ��32AM��B�
5�.�1��v{��[Hڬ=���J��L?�h,��lk��#u�@�6�g:�����r�� K�.ǔ�3@W�CӿEyVxrI�I�*x��~z�"�r�ʬ�������u�E��<`�>iU��o��(1|�#q���P��A}'��Ӓ�}�}���"n6�3�wJs�	��W�t��39i�M��+>�5جΠ_����n[Ll����+�N�+Ɗ�5ಁ!~��l�����Am?=��B�6I��U<Q�v{�l�35_�D�r2�>����;��L��d�����Sо�ʟ����G��Pk��J\,Dn^���&����~�Ӥ
��UfY�M^0o�B��7[^���@C�`�o`nI�����idIQ��m ���G���e�q4ѵ�E�S����1�d@l~ZZ�.��:���T��2>y��H)N�&�u0�>ء�>�.�A�{�0q�;9�˼
���e�5�Y�'�0��ۚ��)�,���	�﹫�e�ܩ{4�+��S��������!��ʃ:;z\G} ?��!l�._s�M�#��XK
����8@MiO.�/���EA��W�Z� �6]r��k��K]���M���˅�Ruˋ���<Z��+>���<���D�xSzɽ1���1Y�����aׁ5���ȉO���<*ƋX�����l?%�v�Dw�o��.������{?R�X����.��.=�"��_;��)��yA� ˇ}��瓥�Z�|-��)�VN�A�S��;��h��J ��ÊW@�Y�+�U��{��w�;��ԬG�('k�]=:�F!��[����9G}�򒃏��5�_��{��:������M�wH#U�3"�J)���f�@2C �,�!p�;el2%�9[{ܥ�J}�kF���}�t#X�JO :=I�5|Ӷ��נ��8.���0�L֝"۝'�L�/N܅C>=�{�g�gP����(;�#�.�Gx�̜�tw���˞�:�|�}��y��#�,�'��wl�+Z��v=r_��Qo�[iE������%�$���Аg^�H������EH�FA~#��2z�:/Q�#��cFc�ɧ�C����D������s�&��d�����ؐ��uf�ї��!�6o�=��a��������pY\��,!�՟ ��9�h�)��ß9�t.1م��!c'��~d�Y�U��186�Na�ym�jre���EF������; ��-�Y	hz[P$��qk����bp0b��Z�����F��_~�u�D&�U������fY�l�_��Q6���NG�3|މ��[<��usr�ߠA;�0����e�Rd��:M/�p���a�{7k��̉�k&����pjF=�a��2�1�L���VA�}hQv��-����K�>D/H	|�iM��I~�eI� A�(o�5~�x�� ��ቭ���}�[�>>�4"
R�mO���PE�E�ѓ�`��=t�p��/}'��3��ь��l�1p!0\�(bƏ��� ��N|���ȟ�g�.@�m��a��b��[_3J_�7�o/��.Ï"���+87%��͒�b�ԅ�����{��������{�>+�ޑ��4���~��-��1�|׼>;���mK�l�uZ�a(��u죶��6�Gf�1�p�!Q�d��+뻜�c�[���Y��ʔߍ��p�|3���(n�� �ya)���2ҤZ��}_���q(�ޟp3˷ɶ�t�O���e���Z�l���-��� �R��b^#��W��DY�[�nc���S	����@'u��K����Yx��6@��5S�Յf.S����+�I����R�D,O��a�S%�vS2(��k�j�@M$7\��6_oྒ��Qp҇ճ{EK�|WK	�`l��E�(��$Ia����݌�8�nJ0��-��7q�9�?P��~7*�(yϮ����������׶\\@�;L/5�u���`ÛȈUZ�\��`�7�Qs�
��5G�/�I��b4�r-�R;؍�A.�J��	d�Q�~'-U�5�k8�~C@Ģ��&��I��9L["#Úm�� k*�I(�B������-��Z=��l�"'W�W��Ѭ��F�D�a���T�P�ZЛv)���
����C�O�58C✂5.ρ<��w.��T��K��=w���p�Ep���߇��rP�s:U�V0��剖��/%��~Ό�'�7�j#�=��T�m�Q>;[Krd���&8xuj��>\��	(:�wi���Ql���)U����C�}�A8��k��8Q| ��$h!��SG��w��tv�W7���8�=���E�K�񠚟-�?]΅�g�1�5ҥ��hԭ�/>5���?���l�Q@f\�ڔ�MC�����Z��^x�i�"u�a���;9^�+������"�D����O�������ꥥ�<(�s^7Y?B0���]��e@m-UdY�8m1\�y̲웮T�7Q�q1�A���>@c�!���B�z|�V������!5N~�IqS�@,��[������OV�zS{�l����+5_v��2�e����yѳ�7�eO]N�d��
վ�c,)Px:��
Hh    Q�JU"�E=���gհ�V�V�pÖ�D�����eT#XFGILj�W�j�2�U�ͤm	Ӧ46�~@�K�s}A�4x�g��E�2�B��g�)EŰ�̜�u�;��EIbAb��4?9(���W߮�Wv��[t�?�����>Cല�\�䅮v����*<��8�t�{��M՟�ߦ�Ѻ��$?���Vw�=�B��YfJ��|��Z�%4��ꎘ0=��L����B)+�o6��� ]樆a�6/)��i��B�j�����?E�Ɋs��w,N�ͤ�2�D�~꫺����B���T��X*��R^����� �O��2�UU�Z�.�l��v� ���a^;�m�R��!A��������k�$;4������$!��ǎ~ъ �q��6��	Df�޽9K���9+��D�w�=	�2|q���ц5~1����J�̀��M����ٿb}���8���#���b!�1wh��6�]��^.lwZ��T���X^G�UӶ~�4\	�իp�"S-i��i��慷�?5PU���!L5�����.�
�����.�5�����8v�	�9�0�C	:�A��(���N��<��p�R�-,B�
rӂF��g�r�3�߮?��,��Q�TL�����a!o�1���o�/*:��|	f�>����bs��P���ӆE�Ϫ(�"�a%��{D]pƫ�>��K�~DA��HR��W<���R����R�mB_0l� 
����Z-+�������%U7~ը�ds��U�>v�D@�O�6R��Ii�u'���hr�� k�''��5�(n��X�4�;�w�&LFAƵ�׈�����n��� T��O��i���K;Tȭp��r�����jM�x��ɻ7�`�����/�}�)  ��ƺ�I�ӯU'�4�M�d?B	��" �[9v)�]�� �Q����V�Gfp�_��;�B)�tM�H��rr��d]8ň�%��
<�j�xl�q�C	�h�7p�����/��%�z�TEC3�<�ݣo(,U�(&,^����]]�a��]�x�
k"kǓ�uu�Kx�ѻ��ן@"<�9�BEjf�����X�c)��������+2��(��8~�k"	T:~���3Q ��iH@aQ�����Yo�{��൲z`�[����ا�{u� �v��}2�
GӞ;��om��3�>��Jv��cr�I$ZCOM�����y"D�ҷ��+_�sL���=`��	@�@�s�E�D��-��g ��D�v���Y���x�����ob�¹���T�6� �E�}�< ���W��X%P�9��@�ې	@k���J)�D�G�>R�t�MSE�'�AR���4�\������|�����:���u.�30E��NQ
����<�U�_;��M�Q��$�mv2���c�{x��0��e~�SsSLp��qvM��+����:�u�0LR��$M�N� {�Jcѐ�K�x;��/�4�]����hI��?��bKR ����%RH�.;
w���g��L��2a�X)�*�O1ͤ��2�-=K����Q�o�{�����d)�AI��o(�\�!�CiJe��a��:�+�I�|����I��&�a�]>�C�a���	бP�ct��[i�E4>A�[6�DN�P�ǣ@�*F��2�e�#,b�f�HX۩����	�#'H���j��l��.^��0%k<%�O�R��c��Z#J�~-ι��u�?���ݼ��O.��ޮ+��w�F0�
5ؿ\dȷ�&�P�t�C��#�&^�ox�wq����c-�����{���YsSxNp\p^��砢�#B5ʝS`Kk^E㤕�.�a�¾4I$�({��K���ʎ4tе�b��r杹�w�._��#��"�@��;/~�R#���7�ף���{�#T~�n�&[��^y�/�ꮮg)��Yv	����q_�`>���2lji���j��+_��n��U��.g��*-��ʚ�
=���j"0K�,���i�Ԍ��Qr����-�������'D�_�Cl�tr��*�����A������*J�Vo?�J�0�sXE��,���bx�a7*�n�8�+�I��^Tls�]�u��$ �T�+�`/0QV8QcO�K�?ܫ�X7~��a�*gz�|,�c�E��G��d����>�Z���L(ʛ#3:6/��{�!=�j@��/����C�>�)�O�bֆ�(��'K�\��p�>�}�/tYu8�G����&��^���^3�
s� ���;��dRy���UcѸ�쉔����⍺������ ����5'��׬���ʂ����nB�dF�JdՄ����Hh~z�IO����$�BH)E�>?j���Q���-,T��1�l�!�7��C�#�f>��Η��km �e�z��l���:"G�b���B���~O^>�}Bz ���6@'����&�Y�����ބ��w�,hΘ>U��ۦ�ڸ�Q�L�����ƿdϹ̓=U����6�����F.���vX��C��D��r�g��
)>%Yt��Pbu��8�K�K�Ea��Ց⯡�o�ޗ=GLO��*�t	���*Q�`�[n�G�o���ʝ��j�r?��39$��dج�$�g3�H2�lxu�[��vp,W�S6��ix�����>����@�$.���苯#�e�@�N)fm?�T����$�D�J��Iǿޱ�s�F�nfU�/ �+�[w&�����>�L)�dh >����E^�P���"p�=�x�2�Z{��a��QӇ�Bׅ$��Z&0C�5_�Ц�p>���c~Bg�F�$g��"(���#ʶ����VC�c�۝��E'e@�Z�z��
Zb���I�e�˅�����(�1�'�g��$:I��<X+>��EVV�}+"�F�P�ߑ�n5]��8�>wZ ,�m�i�a�tpg�Q���B����~�����	�8��7{zln����=��Ǖ�B�܋���_�E�=���E)r�uk�F�i��3�������Ө�椭xobC��
v�vzZx-�7|���(��B�Ml3��3�"Td�n>W;M�`���W��U�*�P*d����/v�3rth{I e��������%����{ce�bF\ƣb����D�������L�2F��ӭ�X�wS@�v����"����G���[=T� �$�� n�BQ�h�ǡ]H[�Z4v���;1�;)C�_�>A&��".�;fOm;�fg��	P�Y��@�R>KƆ��B�ԑˎ��Qo�w�.�'�d���F9�m/ۆ/��)��A����O6|���m���>N���'-�����<��k�Y�\F$�j�T2�>���f�N��u�}ݠ�Qj��t�1پq��2D�b�(��~S���àS���C�E�}��"�_/�U��&���32�F x:�yt ���E������X60GBZj�3���jB����)�@�x�s&� ؄C��#P��/T��	�ͪn7�]JV�<�T�A�H�U�D��4i�������4�����`��zӑ-���J��B��FyW��[�)E��nZ����ſ��.�I�h:��j �p�ӗ�'�^��c���M{�yx�;��}P��"h\l��poa�����Bv��������p�����}���3��b�+���2�Q�� ���~�Q��D�uf=	A?O�൥�r
l�c��$����i(��*�&������%��3�v6tq؟K�u$�&�hPZ��y?K�G��Kx��_|B̯2���k�ۇ�*U	Q|��5��|C��x����rM��LRl�?%��ⵌ�����0㠤,M�V=����#�TV�;E�Hd��hˉ�te[ 堄\O�̟�S� ��PBkG�s��,4�l~�I���}/#:�`�H��H�����[�T޻��~��$�� -Hc'�;�{ֽ�s��f8I�LtyZ7�g�@��}�����^w�^���)�2��hBCݼ�a^P��a w    ���!F<�tb�j�#�8@�u� �4{:��"�!��O���/���X�c؏���^0���=��F%C�=q�9�_v"�F�o?K������G|T=
��y��yA
B�S�	��~��ٿ�������S5�+�m����r�` ǋ�T����SD<�O�쉽k����^�{��o<�p������.��e��"�x��u��G@R���~鰮I\�>�jY���̖�%,���EVΘ�lLgu!����t����mv���h���l�H, ����o�l����C[..:@yx������@&H.�'vb�*��	�{���D�<�|��\�Ii��|"��5�}-��Ⱦ ��&��7��n Jyi~k��ѣiP�h�Ñ��n|��5�ꇛ�@E<�K�(�:WObO_�RA.9�= j�ծM��`M�0��X�l:����tu�9EO��n���-�/|��
��
���)~��_
����UeB�=4�6]�G/�$5b�w4��|��cƁ����v2���N�lJ���:㖖�����!��?��c �K��w�8�4��k_h�>N؞m����f�8)9*ه���\C�h��X�"�1���a�W�iU�Y��A�Y�g,�!��VFp��EP����0@�N5�x�+)�JF�/K���A�T����p�MpI�W���� �k�M�Q\S�97	�4Q�����}!�`���1�
�O7��>CG%҈aݍk[Jb}�Dߌ�
�X�+睤�ǆ=ՠʄ�o��b��4����b��dԒ��Q>�^Y�Z@p�(X�3�@�u�l�]����Ű��u��1�U�IS5 �Ͱ5G���wM5��MM�f�AG\>��J}wd"qP���}�/n��a��U+b���Kk�n����s�j�����;��9 #�����&��SG���&�x���k�_Oc�UK9oH�ԛ}�/�������[��296Z�[�֝�o����(�a�/�/�r������pM�ֳ������O$vÙ�+��&���fe��?����q2O6&�����/�+x-�B�v�&��V�c��\'$|�k�O�F���/�+�oW�3T�gZ�\��Ńb�,#�yj�ev��T����T;��B��IV����v�����Z�G>SY�K�>��c�Hߑٱ�q�0�"�����.�KY,%=�UHk�d��S1�q}���<7�\�m\w������x�X�~�����z��;�ŰÁ���(��Ӏ�F��+�'�ᄹ�K�e+���Ac��^{2Z���o���[5?�D6̑ �#��g����*���^Cl�P�@�����M݇ɛ)���%{��	��B �K��axg���_q�uB����J"�[��=%ȷ��c�g&�}8<A�~!�HCl�t���;J���b4_c/�x��O����c�)�1��R�g.G�e���=����bc;7��/fc$�o�{u��R���j:���'x"���o�O)�nDdi�8���<�+���[� �s��H@:��"�6wW=v���px����1�'�` �,��.�����EP�),f��;��\ݼ6;B�<�E���Pw��@i��P/�1i�,�"ۭ�Z��yd���)��
��j@�E�2n���}�Fs���TmEr���&�M��+�	U;��1��C�!�A9אn��/���$����I�!�i�S�/㇢�d�m-��n����*˚
"�����P	��B�\8����Y��yՋ�0zgόeL��,�禋��D��5＞���Qٴ����� x~Ixn�����,�X��j~h�r�Ԕ�/��f���j���5Ŭ֟�6��E�ҧ�{�_�5���/�8V?�].<,��
vvŸ9�/E�3��K��&�YbY��8��$����Xӄ�����~٥��6cqz6|&���D�QB~�:KIf���o���ۺi���b
v���ؘ�6���Ji��;^�pݔ��z�4!kD��c\��
g��k,pT��`�0rF%��C�{go������峹�>�e��y
���D�<��0B�*�^�&Y^)����na�����t�Nc:�饻8���uT5�<�2��S�J����V"�$��dֈ��N��@�7
������[��H#M��;�!���oJ#�3���Π��m������.m'e;~��������½�t�¯;s1��OZ�����9�ae	8��2��/�1�����*��Y�
Kʋ<徿'�8���׎� *q%^E�r+�v��+4�����iW�[��B��K���.�]Y{��r�<�Z,V+�,�H�^���L��[׽��e}t�0�LU�")L,5]
�$1��?+����,�k)�|�G����i#���-EHs���OL����05=}HW-�+�����zS�*�ɰ롟�iF��%`\ַ�d��)�������������Z�1�l�H1lX�OUS��M�g��X��l�䟔�5̴�n�M�ǟ��8G��L�,���q��ͦ�=tq��=$����=0t�k�(F�f��H'ҸG��!Be�º�;��90��d�hN�&	�>E��=�msF�/�����,�;�߷*M�i`��)p�l"�&Kk�ŏ�pΧaϊ�����2�p��ܪ �#�V�`�r�ǜ�21�tO�VBY�1��!΁�xKC紮���n��K�oF�7�m���W�%����B�e�h�`���F�k5�z�t��	�i_	3x��<f��V��؆_ۍ�A��Q���*/��ql��MT��_���=����n�ӎ�m��h ��� r������"7�-'�M��2r�����wZ�y�6��Vv����hs��s�S����$�p�Vu�����������Ƚj���ͅlTQ�ք�Nт@�Xo@	w�� �v[Njs��-d+Î���sI�Q(��(���#E�I�Gg�6m��:q!��!���V���gJ9�f-R8��̎��{$�]$o��V<ײj�F��}}�4X�<�غ1o�)�E]��ډ�b}O��{�v1���
�˹E���v%�d��0�X�7��Bzkw�J/�L��0�8����ҡ|s�q���qW�����6>��X�$1���O�4�bU����n@ �~d�vF{�����W��+�(�<lY1!o��G#ūE
B��#[���R�D\����0�X��zgK%�����������]M5�l;�DA`g��MOB�<c��a�٨�4�ͦ��O�|`X`���9>�^���Z��\+���
���#p���bӓZ�y��t�@i�E �46����A�}e�clrNg�9I/��U�'tȗ�B��%弖	������P4|F�w�������n��[`�]b>�If���y�"x����5�Tn�qfM�j_˄�0�\���wד��w}�3p��ݙN_bd9W���A^�R6!dM�s���� 7��ن�4��w:���[?���a�Q�ߩK5>f��A#^ �_��gژ�|0�8�A�O��`��Yu�6�����He2�[���� �,�M2�h��q 6� i���25·_o�'���H�<��˦��6?r�ȧ�q���[�c�d�<��â�p��zʤE�밁��y�fy!}���`�:���rs�����'��ӽ#ٸa{�F�>�)+��5!)p#�Dl�BvQ�|Ш�dƚ����
�l4�q���˟c�¸��T�A�fc�f�8
|�Im��#�F�8s��۽��F"�VZ�qH�e��߯86��q0���B� uUv\��#�X=d��|:dލFLw��=ƹs �C>F�QCb+O��������	��RM�\G	1��?��]�	g<�MK�d+t�2i��<՗v�1s����#��Q0528���UՋ���hk��3��yR��^�Y-U�އ��T9ʯaJ��p�`�����:0Wk�M.����DKj�<�"R�s��y����s�����	iI$��)+U*Pn���_����9\A^K�G�e�    K�Pe��94�-b�6�c|szpU$<f;f��,C��x$���!�lYn6Bi���I��Ns즸P�I�<��q Q̙|.u�,��b V��w��0�	8��t�с�{�P	D�\4FGǎ��[���F�4��T �>��
�@��u���J[��LZ#
gU3-r~ud޹�w�i�p����X�e��TǪ���X������+�!u�ٍBE�#�I��7�a h���)� t�w�36��������Χ[��B�hZ�����T��v��CT{^��3<�N�ǧά�8�q=��zʡq���c[���,�Dj�Q��h�ZH���sc�D���ܬ$�����m���_W��D��R�A(ה�B� 2`��.��}�(K�:}��
�;+�X=x�y�S�ǝ��}i��3~��E��j�M��Gנ���YG��(����и3������������39���>!9��,��Cs��[����*�(=���k�R�_.y�&��鎥��2g$��vN��Z��+vqMo��-�1�'��N�m�67y���)�������v	�������j%˒Mjv��~Q�f�V{�5{ַN������f��<{�2C��ƅet��=��B�.q��
5�R���[Ƿsv��a���[�2�� �r@�S����n����Oc #}3Cz�ǣ���?^3w1�
��6�G����s0@��.�&�?��������DUT�!� ��7���Uz�����k�?�	��f���"��5*Y�bZ"���d�*k�FտQ>,�{��4&�dcB�
-oKt�+�>�)�J˖�	�d|ԯ�\���~|`���;P9��y�!y`�GU�Z�gRcmY$�t��~U;�/׶��	���7�ʮ��ڭɾ�xw��r�ʥ��>��)'�y��P&�Ȋ��#�DYh=T�Y�/ݛD�{!���>jފ)���9<V&в�ǝ�t��2]���el.��� Η	TGL���v�c޷ !U՗!�B*7����̿���#y��+�K�$D��˰�@�/�?�N8���%�:�m����+j�ibWb��HvJ/ֈ%F��b6���/��/����ɥ�!`�AZ�c,��}����4��Sbx��Ł��qƉ(8��_�²����̢z&�~w�>�|�OfA����X���Y;�XS[IvD������(�y���"����Cy�6�W�E����x���JZ*3��V����([Y�ٕJv��"> �.�Kn�4!9��dX��\$sd�Q�WLZH�[#>	�>Ɯ�-N{B/�v�~m@T;+%��ҍ2��~9D~���e�28`y/��z����%���P� ,����1B'����w�����/}/�^����;y��������I+:]��_������}�0i�}���j>SRr(��(����������&���vJ�E֏�O�*���[ʂeD�@,M�� H���|�,2�,��_�V)l�>eĴ����Ty��t4T��k��`<cɹv 	�ˎ3�E�;1!�J���Ɩ�:v�w4���n�De��u�'����՗o.=������Z�������O���N fE9�%���,C����ş�w�]%�K ���BT�g٪1*�)����]V)�[�"�H?UY�#�NrE�G��l��p��B��p�E�|��$R�-�դ|��]�ڀ${`�ò�+�f�a��c�_�c�m��/-����fw�$�edxJ� ?>�%F0*f��-=~X"׼+�&Wϋ�d���RhE)�zR �S���,5 ��l�P�I�����Q����A=Td(p�A���������
>�0r<"�o�kUiA��]�)c2��4�� S�k%w����Ԥ���J�4@�]O�
O|:����苤HĹ���۫���+��l}19Un�j�E�[���-n�m���Ѯ�%G��hig�h@`�c�v��� ~7�T��
�u�}~�<o��3�W���� Y&�c�`"����	[.�F�F��"�K
�\�I1kZ'���/�A3����πI(����e�;7l�,1}���3�*Ƈ�+B8~̮�`#��5�-Q*"׊6~����,sBW.�4j�`(�$v�  _��y�iY���~K����˨͗�m�@�B'�e��T�<HoͬV���磶a ��V�T~<�ȦR��z/v���p�i9�|}"�!�&FN�4�4ol�2�R
.�#E|, ����듆�W�ʍlZa@e|��[p��� ���:PGAZN��@����"f�x��q<�o���X�/Ě�QfO[�G���Czy�K��Y1<Z1�vMb�����c>�iM�v�:%���V��}�V�Ll'g�3�$"~=w(J�DR�詆�;1&1�J��;3����c�4u���aA�F�i@�]��FOU���c�0�2�w��.���0��ZỨ{m1��`��K2�3��~ap�iv5mg���H=A#���.�N��䓪�ڼ4+�l"��A\)��w���҂dOR��X�P��u����wӋ4Q�-���0nĴ���!����W�PQk�0�qɥ�&���`ܖy��m�'F��st�u3:��P���P�|���X��e1�}UPiC���K���_2������,A-<)ub���LG#[ic~I WQH�/�p��nA����{K�ܟ�z���i�8z >�7�@�x�
����AN1�Y!��x,��pY��%�]�ިޟ��΄��яMĨ����!��`Yo�eF� $����u�(�=�������#zغ�;��mō6OJLnӞl�J4�&�N>�P�mr:��{��H���R���V؆R7��h��L�wǖ���� ��a���l��.������/��8`W��}���S���q��}�6���W��F��TM��Xq:�I` ׯ�,ɠ�i����m�S���'S�F7|��?��Ɯ�O�����?ٹ�O��t�oe"�c��c)����-��{��0�sx|L�0E#睗����ʺ�U�[�)+�ޓ~B����W�nM'�u�G� u�n�)��얜i�K�@�O�u�R=��^.Y�Mu�A,���"��FJ���X���e}�E�t�,xԣc�˻J}Z	i�Jui.���駖����D�]g\
0U��{Pq�z�|����.:˷w�;�	p�~��ن�O�P��ÙLw��;u�L�A<�L�b�g	��gL��r'���[�})wN�w��e�
ԉW�+��T��4�F}e�#�Q'�c�X��'�O�j������8�^<j�4&���y����
g�7�'渃#���Pdw� ���̕�;Z&��k��1M���ӱ1>(3��'O�S4p)���Ǽ0O�ԗ!�WLqۈ�5e����:�U�����$�ʪ�{L��M艿Fs��Gb�ܛ�ij�7�[��KN���1VǒcD0yW��/~��u,3$29�iBɒ��N�R%�rdA<v`:4r�
\�`×cZ�H�`�?�;���=O���	�<c�'����A��1�w�e'�p�f�c��c�)�&cҍ:a�sc��A;¨��q��E�*�Pޘ��ց\��&Z�����Ʋ^
�]�%Ѱ�1��p�����f0a}�x������7nrrQ�윯�q4��{I��o�ɰ�4Oe�y�k8��JS]�B���|
�#b�	{=���7�z-G=��~������)��v����i?��NCEi��zq�\����R�É3o>j�!�$��!��)�	5#T��߆�]&�RAF�p������D>6��,�C�Ӵ�k[f�M������`:F �H�ܻ����V'��Ĭ:<f��_�F��A�I�Q��n#Վ����*t������=f�&�[�$���~��}}7���IjF������y &-�r�~��D87J��3ho�/Ccۖ�i���)	��ʠ$[������|�6,n+$�H�u?#�3:'^�|��r�s�)F;�����XwU�    ��{�_������Ź(K�<� ����$6�//�M\?G�A�q���H<��9�I�ݲ\���k��Un��i�����q�#}�����,&
��zm#|����՗�q�N�ڊ#4�A`��͕a?���*�#g
���8G����)?�(kS�,=�iK}V��!h� æ�o�P�,���,c, f�T��u�E4nz�R����,�<n~��%�����"j*�K��ʔ���z�!�ك��=���3/�DMTp�!���U&������x���H&�i���+_ױc�K��+��?��C�ո�#H>[��'�g�*���Ƌ�/V���BH��y���˃������è�� �0Bp�����D�������j;�1�Z��B���$���_Na�TF��?�.8�Bà�m��]_�[�.�Z?��g��@�@��G���f[cK�86N����b콵�j{�(�`]A���!.^}�m���A8�W�� ��{�Ax#�k�lR:�޿	���l����ϥ/�g���Y4ߜ������>�h�H�h�|,����[��L�go�"��'wh?V۰�w�'#�������Y;18n-�9ڔ�1�$��m���f���H��lo��佘t��P�8eS��V`�����Q���5j���If~�s�s��Ω�諪�h$9�~7����z�C��UO1�ߺa_���A��m:�b~�|����6ʘ�1�M.|sE��k�N��?#h��o�r
��9/�O��ǈY��8�1��@��:V6]��Kh+��&`�t�|+B��:�����TM�]�T\�?,�S�5@��@�\�]��_$d�5�䵆f���i�L�;�tɅ��P� ^w�L�+]X���oP��taҾ��MA���,��������^<r����b��eaN�c�h�"��|�����p':��H����'Id�̯�ԁ�b�0�x_��u;��{�ܽ�w��I����wKA.Ɯ(IKx�)^0�d��]��BP�AڛrZ"Ex����3�G��h�(����!3�Fʮ�ʥs�mK���gM�f�I�\�k��[�5x�ŖQ���V�V���lI��0�%�q��K" J)t��^��M�6����sB.P�oSoB#�L��&���6�},�����X�E<F��9�s�9�Ǥ�XN�缲մ��ܛ�>kx�JT[�y�&<@�="�D��<�:�ϷS��9��9]��%/A�T����2���ܺ�a�+�"%e,�}A�*^�S���͚3C�s�0=��*rt�ݶ�2��؝�b�<MK/�p�t���R�q��A�(�`\:Z<��r��������)�9ד~����8y�9�B�܆q�V��J��Á��C�	��ȵ5p0�1��/�K	����[���,v��N|N�	��}G6s�Gc�6�hS����3�#��O���5Lk������fxƀ�O�d/2#�Evu�I,�1�W�d��]yrJ��l$?J�@C.�\�f�+P��Yf���:��1�D��+Z��w�z|�͂�^
������|�i۔n ~,�� x �qHK�F)!�zX����e�#���q8�F�ڱ���!}j)9����u�ԡ��|�f`�{8�@��.ǵ9/�%��cǗ�}G��c���O�J�i� W�*e'L�A4b�|�ʋ�O?�!^norw?�]�9�G��@��i*Ncwg#m�̡��.��5���&�&SQ�o��}�	��¦�F �s�Wb��n�<���eMG��H/�D��b��m���efl�4Y"<{�#yL���بP7s;Ho����wƫ������65o���o�@�%�0*|K�2���U��X`�K��)�a�6��1.��iI8;:����iVx�1.�����x:g�_��ݤ��0$-X��R�1�|��n9��t��\U]�)�}��\�;��B�]�<Um�}P5 #sI���\'�+o/�h�X%���)��Քa��B0�#��
����[�˅��\���f��<.�?k�W7��mK�*%;YMRH�k��9��w�hF�	�-
����#�c�G�-u��%E ��t5j�n��s��FVP��H�d;$?��Uj�U���Ŗ�@E?�nC<��f����G�Q�ZI(ꞻwS�HEU��E���"C����c��Mi��dj;B��8�����)�7nZ�*bYR#dJ��Ut�ė<R�ތ+Y
iI�g�z+=��6h����T����ü�TA��Ȱ`�oTA8b��3qx���ߣ���4]^���4]���veC�Ǥ�m�����#;+��%-<�h�,���3����j[lF��\�Bn_$�I������o7��h�U�}<� J���J?UM�O�g^@��@�������L�j��H&�w���˺z�2x��ch~��3n��TX�Ʀ�9�*����c��+ b?�j���y�u�vW9&)�op3������TpR�m>�w����8�K~\�e�V7���{��ۑj�[WÎA�z�.J��]��/���_d�x��Y�	>N��=K�[�P^��k��R¿�p�;�½KIc�D�	������(����Nz�kL����> ~�,@���^e�=l.EM��ko��>^͍vH{��L	�Jߪ�V��}oe������>c��� ��!������w������� ų�?c�q' ���w�q��Me(�Ke��c�z��3�Z�0h�U�:���p>�Vx���Q�e2V�_�`�Sf�oVח�X�e,��qVLD��d��~��f=���q���"�T>���x-��+X��<a^�4�=�,v����Q\`RIx	sG*�����R<��ݿ�%P����I��f���M��E}���8_��Ӯ�f�h2I!�M3�����-`���}�]��/[i�(�q
�cQ~v�7�̇����qM>�����;�o�E� �?Rd�S2]�~k	��d��'������P��^j��H�NLN�[����$��2�G�7>S��Ԕ�J0T��?��"]����0n�.���w���0a�g�@_�d�x41��a�����i�������R� q^���&vQ�W��}�g��Mo�
�ŉ��\��)C8_W�<��s7K.��s��To��WJp���7����b���"X�!s����^�!pAo ���D�a�+ۂ�y{g��"��t Z��@Q�~�y��e^�2 �Wk�B��mj�h�o�mǻ��e_H���/�W��� �����,3RL: ���ˠ�B'���=�R,�����k��˲�s�ԗ�(��[ޒ��q�ͰZ�H���^�e����Z��N�t!u�K(���O#�A�σ�x�Z#��2&�S��9q�� �,2��@�)�t�w�g�vZ�|�M�N��b�o�%)�<�����@Bez�޿��.��s���C]NmD"�hˎ�*{.�-)H� Ў$�ԼƱ�~91e��� }�^�3gB@��Ey��U��n�3#�5�����X\�v�������gձ�Z�C��q"H�ݚh�"�R抡$�{,����v$lx�f_�I��'uaDQ���+�@���iu=�"W�7DLB
�ªw����RBPK�����K� ݡ�jk<��0�qA|�^�z�Ayb�Clh��fjc2�I+�L1)Ӎ-�S�)�^=��4�(�ɪ8_�ы!,ٝ̿�
W�3Ϭ��k"��~Df��=EFp=`��v�y�����-���Y��=�ҲؐÕ��w} ap��s�E�����@�Oz��$T��v5��t��������Kc��g�.�$;iܧ]J�x���)P�L�$tZ*KtW�<%еO��/�qsOviO`#h�`�5��ȴ�j��r�ץ�S/�ۊ��b��f�=��y��ݢ�éy�ӭZ<���υ���V��v�� K�Jkc�^b����!,����Aؽ���f�A�rv����t�9��
*�i;�����1��y�:~ύ4X�Y�	2��Vq9��2�B���o����G��2�6    �dt[�)��}?��<���S"�ה~�Ns9%���RM�ڐ��L�	�
B��F�mG!Ep��� �p|�Ci��D�&$�\k�J��l��v��o3썙��ȹ8��q��G��Gq���J�I�w��V��:��իxCEb����W�����(
��(d�mO{��iV� @?X��Υpmއ_L�p����z�b>�X���h\2�n��G��O#5��a��Y��ʥ��e'(��ڶ}�ju^`��p�|2*@��u�E{��Ƥ��{w�dq�/Ħ�yJ諑 =Q������}��������K'�ߤyK9p���X[0<o�a�J���\g��n��Z�ݢ�?_����}|¢��}�*�
+��@ӱ�ryZ��J|
t�S����Q����c����"���De��:�-���e?5fk���\�6o+�+�;\���>�P��+!u�e��z���ySZ������ϝ�e�Y,�p���x	R'�'�!�*�~��\<�wv���-��4�R�K�#�Z��k<x�ۣp=�D����A�M�ʛ�#�	��ه��J�V?2���P���h�B�J��1�F��A���}���u���`�5)?3�Z�5�q�k4z�#i�$3~�4\i(�}6���u�5���U'�s��k��6����U��w*2I��7��fD �Z�u��QX�HRS�t�!�� ���g�l��C�7/����a��P1�ږ�Zr�z�.fX(�U��n(3l�ȱ�!��q�9�ɱ(�b��b� K�qt]1��M��7�A�9�>��d
8�\	>���<p�d)�h���x�-<�R��|�[J����5 �?��:퍤+,�v��fA�sj��}6^��u�A+?���3P��ġ�����2�	F�7G�~�dRڝ��m雋PwuG�l[z���X�������;^3���U�e4��'���Ƀ���k�Xx�Ɖh�K&���v�%vp-�3!y7���Y����(�uy�gd�9��c��]nW��-����V�R�� ����gQ�������2�Z������U���x�/W���&��E�Vy�;X3��j�-µ����U�$��c���ș,~��$�\�tl���
������ƾ��8Q ����.o� x.5>(N&������ y��#'VH^Q�����RWh�S�)���5��ᚪ�ov��NV5��#�Uͭ�<Nx�O�E�IQ��Cu��X�jW����wo�id񭘙�#բ���*H��h	�E:Xh	����z�*�"ۡ a��0E��Y���&8�8&vrD+m@�4K�f�T�D�<��	��x9�d%�C��%�k�9	�2���=��8^j2���(���V��3����PZԒnй�8?�z�%o�8���^v���,BAx5_�ls7Z��M ��+N���Z9�}�sl��\q�J� �^�&��E��tn��qV���~���e�ǁ���`tţ�ػy ���C�<qi�^�O="�9�� V~��}f�>wV��^�MG#�wv̨A��#%��6� �SĨ��E_�k~�'�T��6.Pr����C�qr�.1�Gث'�/�E���!��c��!a�Yj@�4�����KE�%9���2�ǻ��`�������V����_�.�k�����h���שHj���Q�^�څx�t�����ل�2�_)��r�+�&f�Ʀ� �[}ԗ ��I!�G�߿?j.�~	���@`�'�T���7	L% �P�z�J�5`,���Fs?����5J�ę��e�la�.c̣���<Z�G�Y��D4[�M�M�<X�c��1f��J�����{�z�fvz�e`����:`�QD�ۛF��������� 3�|a��Q1�Iy+�n����ִ<yK�h�:_ܽգ��EB�U-��p����֡6��c�����/�S ~E��b����-�)�x����~ Ih�ao7�Hi�]����y�P ¶�Q-_H���N�{���4F��s�~��H����',��ڻ(�$���-8jo4����Vo%�
��(�G��� ��}��'Ko�Rr��X�t��z�w�����!Χ(��k�c쉎u<�^j�嘱�� �3-�!<�U�/��+j�^���h.	� GL�����{�X:F�ϗ%�%5�����z��SO/�o�e]�rk�*i4^��ϊ����N��yU?��Ϳ��N��8�P�&N��*���5��a����~�U�3#��?�R�����w3 �Ǔ=�()���\v�)��N����~��>��Q��V	�=o�(��U�x�V�����N���ia�9%�E�� �~�������%�\�VQ�A���܏�2(BA+���!?�7����A�s���"���MQWbzr�T�^C"|��vs�2&��Y{Kw'�Ȼm�ܵ�X����������n>kW�o3��K��ޯ-�����s��$j��(� �Jg?43�eA��QK��˿�������y��LX{�4�&�s�$:p�������#� �(�O�Y������9<8Yk�F���Vs����k$�&���G��M�.�޸���v@|k���ht·�J�X��v*�P��� i��_&1���/�J�[�v���6Bt����g��&��,���EQU�H�,� k}F�(�ɕ���7h�lT������y�w��<s���a��1��d�x'�^ F���SB��6#$�f��J����Z[P��]���2�뾵 R�f7�M`��F31|�g���������P]�}�A�a�B1��Om���@��01�l�>�H��:4����U�/!��`��{��V�
���	�������~�&FI��m���cϫ�?@LԼيI&m|?u�@O�� H���a��_j���r�^�(���~ ��5����/l�#Ji�;W3�Ɉ$&��K���ʤaP��ޢ�'���(�OӧX��9K\�ڢ3�~���P�xRE��M���0����6���k<<J]�iH��2���Q�M?�gr�Q,��D�9��GX���7F@y�eE�F�sw>t��L�BF�����'�x2��K<@~&��C���ꉲX���k/�X��p����0XX�޹:)r��e3%ݘzj�j�$-�~L��el���dX/���{e�#ʻ(�6�Jlk�����wa~/8A	���e�6/sT ��T�5C@BKoՍ�w�8Y�#�@P�Wcbӥ�(C��I�r����|�~��{�b��>���S���0�#�8�np7]"��a���2�]���>M糖�����wB	��hec�.ϦX$�8�vb�c�1���p�
�!މ򾲨���bRKt��9�JG,��.Y��2�LH��M'm�P��d=��|�;��皾\I�~Fu�^�g��������{�@���t�o�jq�G�<�n�³���ZL`�	��j�Z���nu�{o�+ac)���V\c<�M�����(@劰v¬v
��}2��$43ܟ�جk`��" �2ֹ�%7��t��v			C"�>�e��ه�=b����2c��f��*�܊*3�q�-��C9��n8਻������K�<C��]s�q~�YR�@`�>�6��0Q�0���m�J�fB胊*�Rmjc@�Jpf\���laM�K���xT�rd���B>�W�Დ�6C���}��bx�)������Yi���O�� ���}��
;2��@�S8����������Oo�Ý-A��I8�z�>��P�פ�;��fSr8��	�&K )$t�Uw�:��-�j�-�M��e��S%I� 8�w �U�	�x�[��_�ā(_�xdwÊ5Wa���C����k�[���Xu�=��X=�l��U�T{��C`U,�5�5��Pv&�1�ǦZ����3�8�
Co3�]��rIg��&��.��+�vqI�1��.�]ƝzjC0u�4��lG-h9�.�Hi�@I� �?{\���	6"�3ا*C� �L�d�����|��p�UJ��D򹆳Tm��#�).��	t    L�Z	��p2rc���d�t{*IX|��H�]�pݨ�b'�k7�Y �r#�X�V�]2?�su�eQ ���U�Tə�2�s$z���x�鼿c	{��0���	��Y�4KCR�Y"~$��j�QE?TIc/$�诓У�%�W9���8�����0G�+!;�����I����X��cY��/Z$��k��4�_�w�	�p5L>ݏӴ��T�2���5�zA"a��Y�pР�G���|�D���x|��|��c�L��UA�v�׳���4��l���|YMM�
�5Y�N�±�C�zR�f|Y0�z�mAJ�ޥ �|��<~AD�/D�,X��>��P[6;�kk�������(�R*(?�^C�S�3�y'N����э8-�{s贖��O���C.�O������×յ9����56 �Y�q��[���A0@iN]�O�my�` B@A8�2dV�ڝ���K��Q�g�i嬲t�/�vE�����Q9�A��1�<��T�`ɖW��/[]S92H�#���T�4�HL�k�'͡kwpl��0�K�z8񀖊v�Bg-sQ�u�K�#��|݃Ԉ�f_�mPtD̈i�A�+|x�`u��#��A
@XX �U��={i�v�[��l��}/�9u�����}��H��B���e�la)0��/\�(�^0q;���S��N�_z���ݕ�	������lw��Ve@�٩v����|��_��/�k��<��کi)k��OS�NԘ;X�~ؓ�_�ޒZ���ா�j]���)����_���߁��id`�����?6��{s�(|s����ɨ�oʯ:nfp���f��=���6��������LS�����Z��h$������M��+�J�#�ć&���L|g�L%��a����	nث������O6��g:��w�1�a2a㑖�}R����X�9��Խ%�g?���TP���_�M�ÑFy��|�Ɯ���'L%rȤ�V7L��+%ɞLߔ �|I(.Ƙ\(S7v���d�O}�Mp}z1�~�(r:�k-�<p����O���sS/����E�qJx��b�$}�=V�!��@�|b���p���^S��#`�Cg��V��0텽?>�q��Xˏ�:����CW=jl$cc��a`~L��0%2&��m�4���q_	#�hBi�~�ށ��f�5.,r2|FcSE��1���4�������J$��D�������
�JH�wk�-����,�m�a�+7ŏL[8��u�L�׼�>t��;�	{U��\�Qus\A��n�_��W����M�ϸ�p��bҀ8����{q ���>>������+B���2�L���`�B�6?��A��a+�^�-K�W������{���)��vy�녞Ra��� ��MM����HyI
6�cg�����[��"0�zkW��A��)�.W���yr��)�SF�N�>mjZNۚ��zf�Jos0���{�U�i8O͚X~�	���X�i_��`���OW����.EЅ�_��>�n�@k�3[�P��A|�Ƚ ��a���_�7C�xss-�&��tZ�+	B`'�bFD	Q��"����!V�oY�0�Į�r>Lʟ�*Vy���}G����kY%���{�������N� P��#(d��;���:��;�u��w��(�Ö}�����Gm��GV��:H�N��5No���7Z���g>0#wd�rߥ���q��׷�"; �(^�$��2�
��S�\�c�����ec�~�ݘ}�ucҍ��PP+�Ż�`J�͆-{�8�����
m��^��-�p���bg늗/��p�&����G�5�� �#��&���a���V2�6�=w�c�[��&�[��=�\����K+筚����gJm�6�X�~�d��?�T* ��Jy?i�F�+���?߼N�nl�a���22֖�e���T��H��w�dV�8����G?u�,uX(u��<��P^ݷe�+
�ۂ\�ym	���0��"@�M��x;�;��)I������6)c���Վ�@���-mU��c7z|������ �����C�BW�{�5�6K�T����8�`�l��S�\KF�)��j+��KJ��Ah �u9��߹w���_n�n
0��q���e爥�P7�<j͸�? �����N���Ss��i #�R j�w��@e�~�bpNb>�6Y,��o)�h��#&�(��ה�n�I��dJ�g�)�϶Y]�C��o��=��΅�)"�y�;_wk�ԧJ�~C�
��Nm�5��m{9ۋ�B�z�sŉ
��[�^��~���uW^�K�zT�W+�J�5����A���^�[�G�} � /�^�U_��6p��= ��Q7fz��h��0 (�%]9�w�G死~+������V��w�����/�	P�Y�ش ���hh�9g�&���$|^�RmL�R� ���J�ѷ�x f=��f��y�f�Жj3)�:h�y��{4.�$���A�n�\G	�+P`Y�gJT�4褝G��л7�Nx��a���`[�)l
Dv�,�B��>6�!W�:K�su]����^.�~O/�<����y�<�2`з��l�*/y�|XWLc�5!��;5�(�E�ZY���58�_�*,J�|�0��5���,K�w ��sl�:��c/�N%S	o�@��>����lO"c%���}�x�S�� H�[n.��W	-g�د���%�x��~��pug�D�R���r��h�I�,V�Q�͉�U��<���&4�<��"b6��~4�b��d3r�w�n�~��}�o�1E���,�{�1�;h9��ʪ[�ݼ!y����`&��d�+�u*/\��zM���s���ٌ�����:��s��l�@_�(x��Q��	.�hz�(zEz!��B�~�r���� �G�\�� r?��t%/����{5�$��!6�BT�T�Au+L���Z�����W ���Y(��tB�9�����*�[��8�;ǒ���A�!#����I%�kM��n_ڞ�b(��"B=Ώ��l� 9�LM����y�9<*s��gdÁ5G �3�Á�rG@��{\���<>�k��Ag%p�+@&?	rq _���\�>�%��Xk�Kq�;<�QN
(нV���z�;�W�
��w9i{B�]�ҽI�)h ��P��_�涣�RJ�D��􅷨�w,x�#�\��V����C{Y^�O�>O80A��d���	��Fx��O�Z�a*F��wQ�o��$ f�߼�i�N�O= GhE�ƞ��=X3.��U�e2���L�<���b����ÍyC������T��5b�:�<�z�v�&�;N��Ex�v&����4ך�2�OD9�����T��y�5���g�����d魓m�
7'}s@�7sf�'�gt<�������A���DxX�$rU�1\u���>�"R誸���`xp~ې)V�s����KgP�t�����>f h��,t�\8|�����K��2s(vE���F��]�a�2�:=��9�u[V���,�!��+��3Row�T��!�9���جG8��
r��ț$'t3�#>`u8#E����}(�`؋7�p�{PT�ծ���
�}AN"�ntR�E�f3�۟,�+�m�!}%|"�)�4��J8?0��"��U���֘���2W!���9��������N�[3�P<Q��`"H`mH�^9D5�3��"���:B�� ��T����7\�C��M���~H>籔��jO@��}��p��y=6դv4",x��K\+$L�~چ^��0C>�(�P�P�jC����VP���FVtL���t˕�L�M�V� �Gr�}9|=�&�졮C����؏s�����Y�EW�/�d	2熖=�S���QNs_
�e3�ī-$!���;ƯRu&o#��U{{K����"'��GMlى�'�J�)H2�I����Ih�ֶ��4]SX.f1�����I��z̺�L�/>_!�`N�!�nb��	��(���k���@�    ���V�Dҧ�l�۬��R��m����OF¸��ܭE�Pε:!��!��׆m������V�Y�Z�A��ޞ�n
k������*�p$����p�C��	�-����VX��vI�����h�sL@����kn�c���Ҥ{yxW�F00l�x��K�oBn��rD6v�D� �=�n�)l���H7���'��V���t<X~�Z������Ժ%\�J��z|��&��
�+�G-E����x�\FX�1����W4%/���x�cQ��-s�0�ԇ� �G�k(�e8�#�^��c�#"�؞)��Y�<�Ǵ�z��8ʱC��մl/�v~�~g�I���}�ҕ/�Ldq��3���l�w`?Տ|�ǒ���]v.���H��r�f��F�����r$���OO �;�aKW?��S�@��o�j�{�Z[iLgՙ>��j�2�vL�Z�=sg�'�vG�k򻱁�یFm굦�T}c	,Cz���U��xt hT��0����ZJsGH5x��g�ʐ^#����g_OL���}>�W��&`�����1,af�UFzh�N�\D��3�KA�9
KI��2�)ϊ����e�Y����X�z��ϔ`I��i�,��\���\�,�|ߙƊ��=o^<�]���'_l����������L��\�Cq�B�=���Y����46�!�TY�3X>ļ�x��p���F=	d��� ��Bk6�$���x���@N�Ƕ)c7hݹ*Y����w6��0��qc
�7u���i�W&F��Ƃ��c�j�L�i6㿺��PA*S��Ǔ��ߴ�������%w	���n�ކTs:��+'�1�[WfB�V�Y)���OcV�D~kg�� �#��M�6Tq��]�<�@�����5�֔k�h�bՇ�S	����J�PZ��I*#�`ߥ���y���vX��`^J���h���Td�:�4�rF�t��������j����sD/��(e�@I����1(֞\�qa�2D4r����O�����V"l�ӫ���w\�ԁ��r��|�JV�W7�zj'�F~�����gm�\��:��B&�*�g��=sEt.�c���G�2�PS�K���;Ou���ݎ&ק �ޡ-���h����\��v���1�1iwA�	�~�*qz�_�f&�4�纆�K�焂=0�64�Y�6x�I��: �sѵ����GŠq������?�旼рa�nrb�� �Q���/}5^�g��ɂo^�StxM�opO�$�U8���V2?|E��D���'3e�-�(\߱�tl��R�PF	��%�U[��^/�����������Wo����2 7j�L,�/��e���v��0r7dob�J�<���a'�����q�ʭFb���r����ށR1����"�0�8^O$�h��\��Z���|I�D�S=��P��Ul&�m� �~ja�x��Đ8��	�tO���y6�����>���~�Ź�5w�v��]��QJX^}�<��B�f�*���Y��w��15[fٞۡ��qhf~���t)�Ǚ�������Ss�䅅󺐁���ů�3y��Sy�0�sn>�l�N���]��w�(�Z��DJ��>�;:�|���P���N��8�f|2d����~Ȯ}.�? Z���?�d�$�rZ'����B��iz݀�,r*&Xm�9�Xj��V�����0�/M�5�6R�Ļֻ�a�\���a�r�����1fm��S��n��]��G�}4[5�0�3jS�<�8�p�������E)t�2��y;��V�7ϙ�D|R�}Iוf?�&#��G�*���%sg��<t\�Ij�]��o�hrF� |��<(��O�1!���c,Q&o@?��飥�>R���Uqz��>qE(����&�Á�=I�
��k?)�6����L��f^�'�!B�hu���������fđ�s��Fâ�r!�<O�T�q�d��,'�!�!�����|vC�NDa�a9e�onK�@Z���G6$$�I�^�
㴿�'ᒲ�v��O��T &
� ڃB	�%�}{����r���� p'{>�;�ν��*�!�-��p2KfsaU�#���*�g�_�L�yl��3�]q�6с���n�r2`ҍ��@cV�p��`�VI�`QCm~����l�1½U�r;O5�3&���(!٧�]c���׭i�HhuK�P���RS��]�R��[���X;J�$��:r��q`i�ٍHC��v	�:�o��3�$�Wx�c ��RIm����(��ļ�J}(K�^�&���%h�������bp�m��S~+nȢ��V���H~f	Ee��Wa�3�6C��IV�����]��{_G�Ht�^�BV�=f� ���BUi�$%��4(�`y�!1IW��&���7�aiE/�?�����8�[���t����6Qb܀Ϳ�/�9g���p������a�ԍ�<����X����T��r)�lx�O@b��;�'����`���]6/����;{�3��K�l��M�-�J�q�GCύ�cwG]0�r
���^�!���[D9@��[�F?��G}n���V:�Z���� �}_�6�Z�z*�0��ǖ�C@6�b=��1�b����TH��s�h���,rLy�gp�u@2��5���~�_:>�b�1�ZR3���z�Ƶ��c�Lr'��X"u���u����f�y~�+Ƚ�Q��2&|\ab�(�`�×�K�C��O������n��� �%A��"Î�s��~K���tw�-I0	�A�.�.�HS��M��'eB���߯pg�Z����w��_½-Y͈ݼ�ֵ|�V�5�x����5D�t�ң��ɱ��`1��_ �ޭzV]ٵ���ݥ����^�9"�7]%�n˽#��l��>o�5_���
�A 6�Ho+f|`Mp�i��J3�u�����&�(�kz�Ye"��<n
G��4��ѸS�p�__І�et�u��ӳ⹸م	�(ԫDQ��|�e���2���}�W8��_'J3��\��KMcO��D1�ߛo�u�ObDi�I�Q���ϥ�mZ����O�j)�����o�h���������`�ٻV��8�#����TH}ʠ�"����w����f\�7��c�&m �(H�D�oRh{��d>,tM�Q|��S{%U�V�D�y}��<�L4�o�c�7b�Q�52�%�Nq/?@�g����d��%���m�����g_Փ�|d��NSe Lu6(�9Ԙ6��a�S�%:�N�mѻY�f���ԓX"�n�}�w<J�ŷ��(A�M�����A�Q�Dy��$y��ؕ
߀J4�u��H�� N��[*�ǜ�C Ǐ9L𲑼�հIu��F\�2KP��˵sVxD����<���;�O�Lt/���D,�'cI�������0v9w�-��ȗ����V����������l4���b:k�T<�&]b8@*����F��lr��ގ�j��Ϗ�֨�%��8�F�A�����\4tˁĒ
�1�nj.}�/49)��.�r�c���V�x�Tg��x`*��Ǿ���f�_�z�ty�Y������q~^��ãn�*}6�ÑI�t�^l&��֥�( �5�(ٔH��P��L2�����Q�Z۴��R��8����?^/��Rm֪�ȼK���'\��Gu��)�����Y���8WΠ���";����,��1��Ǔ��;�Q� rX:/��ԙ_9Ķ��i��:����[[����@|;�pd�(^�P��~1���E�󺖏���|֥����$�f�H��M!N$X������r�iq�N��!�pR���zw�x��e���e�C����5�J���ї&c9��`'H���p�������"��Y�#^�^�eÔ�/���.�S�[�e5��ԼӸ���.P� �ijʧ/��P��䶋��il�P�C6ZZ\�������uF�������A�=�	�\:ݥ;u�%^�R�I�"��qG���Gq�B:�޺TG����Qq����/_͒ϛ�l����n�Ɩ��g    �x���<�|����-~�u��y��Δ�ժ�~�Rb�x,QAu|M�3�Gs�N�E�
�dvٶ3����2���C=���cDx�~�m!�
���a��5������h�d�Z��9��Xs�Ƙ�?���*Kd}PUF����#������P��'�{��f7�3�O�-y�ω���2M��͋��]��k�W�gw�A��T�͜gJN����c6�Ȝ8����I�+Ф���REB���6��>�� �4'J���GmA���T�K��j'	�&��t��7����_��r�u����ex�8���-�����.�}���g+^��p��a�毱'�8�%0mҖ$�.�6��/���w70o����*%f���G�Ab����v+�7�Ы{b�f���G%�O�V��0����y��|qG�*ܜm���ٝ�@5z�'�����5�<���o��o���O�\4������F\*�?<0-Wn��8���:��8!Q�T�����T ��*[��PMm���ѵ���L�E��*4{���0	Xg|}?l� w���/��%��̭����9eq��O+�^sM�4y�z̈́<u�ե^d*�a�.x�������@ �3��9��cs�g�wt�$��ru��Vlŭ1g��fh��"^�Y��v�[Ѕx�1,�W�H]�9s���*��Zr]�5���Hŀ��]
�3� ,�Pzf}ОfC�N~f�j0��È��H��.Q��~�߀8���.���G��@����/u�����b l!��Q+��}�)�.tl	�n�fs"�|�H�����ю~Q�u��p��jor,LZ$���
`��>0�(�� 8��Y ��-=���K�N��(�;2��-0 ^��g_�|�[U��ŕ~po�0)�3}��}�ǋYp�=6.�mtR��E^��K�b`�9K��u><L�Q��P����q�Ш���bq�I�H�'�b�t�
܈��~aBи�8�_�0�J{|�+���c�@I.s��tO�]� �D\�;�"=�N��ek����T����/���K� �e���!�����5t]�)�6��w��k���߁n��^=�(W��/D�7��׵��ǾNR|�9w�Yb;�e��t�<����:j�S������U\��MU_��g��#��v>/z��j����3��hT��vE_A�.ơ����b�~�0��np០�5Z 4sH6z8Q6/�X��*%��M�������TC"��]��������m�-����(���R9*�Q�z��I��k�o��>>�kI��r��ѽ~�\��x7�|����ÞL�2�C!O$S�K\إ?B�/�/��mD���� ��!���|����n�-����� �g�5TBJB�������|D%�H�Ǐ�!
�w��Y��p�c�7��a�p
a�gUvP 5�(D���P�+�����NVċW��Y��"�a�L�<-�U�;�������)�iL���+P	ޮ�U�Y<3�9$�ޫciKB��5i[>�����k�i��y?�i��lS���C��}%�Rh��r��{~h<��X_ߩĤ�]�<rBJ@�ma�;:}��㳳��lP�0iy��y�r'�����޽���� ��-hf$B��{O0�� |��/�J�5�<4���Ƃ4m�.]/�����#�i�M�)P�1 ��~��HF�{)J.��,{���|
�V4�� ��,5�CG�G�	�����Yx��_*���.�:چ�F����G8����
Y�(��ۚ���|��c_}Wh�9�� �w�l^���J�����'�u�Z&�q��dc;�k7e{=?���JPg����!��A{sE���d��t��~]�>�M(��g��zXs�O��n��b�t�m��x�e�H��f������������˧dÁv�o>��R�,}���PY��`Ʌ����
6x8�_��_�ogȣͯA#�����H��I9�⬋����H� 6��@{�5ʊ�A��!���^��	�p�1���潛F�|�=�jd"K��Å;h	Д��1O�í��pE|�1��bs����i��6�]��,r{5�@������/S&rǄ���ڨ];	�����x�[M(ׅc���q�E!O���!є=C�tϛ�\�Z<*݄Sf[���Re�e��|c*��6�D��)L� �:ʯ<����]�^lgkj�1b��;y[���﯉ys\�\����,'�Um-����!{���������ˎ�?�Ha�?�������߷�����6 ������8��]�=���a�.C�d=��Qi��F��P>� ���b����Ic��NEy�^�e4�,}Eq��5�F��j%n��@�z�z�x����n
"�8}=��]�x�׺�qM�Y��an��9�կ{�*j|*R˶�Z�!}`�9\�6�A
K8��TC�qdcj�XE����`�"��{��˥N~�z��%���m$)C�Y��������'%��>��5Q.�`��8`}4`���Tg�P�O�7k�Ь���x��R��Ѷ�7@��n+T&8��}�7���F���Ah��_w8��@V@	"���1���)\x�LT���/���3;��g�.�^���#)��y�D�H`Kr� �%�ݫ.�X%s���
��!ח��ID@GǷ�'$鮢����.��oL͔��O��������B�M
�?;U�kQ�Ё��)+��}_O=/����.$F6�ްWO�KZ��l�	�������c0��,,l��%[�_a���y�cf 4�|�G�<�:8x|�,Hm�$CA���Q*���շG��B0��l��5:�D�̤F�5{s�꺕,��
1��HsM��������m#��;3>U�5e@>�,,��:������f3��ը����^��V(���ǥ�9¯�N�!�#A�h=x<.hL�b0��+Q�yƛ�1'����d �
R�#�����m�#{��(�����h=�"�i�"�S��Ӥi&C���{��2�$�Z3.���s����ζ��i�I/z,��<����D�������䡛��3+��p�0�!e�����J=
H?��6L��î��N������׎tࢅ���xz_�ŖԺ��|L���y[x��_�o��K��r�t�u�Ց�Pz�s���!�R����j ��!���;+lm����n�ȼ��)�
�A���ɇ��)=8�� 6 d�C�-��ض9;[�?�J�v�5R�lv���t ��k����\!4��p��!�b��Cqax����g�X� ���L9h0��PP� e�,�`�`E	z~��x5�����c۵X�\a��_	��V`���/t��}�c��%�Ÿ�^Q/<N�t*ꠞ��`)�ǭ���+��/qeu�/<��g��&��+�F>�ub��U�S�l�?r�/�m��߭��)��[�֏l%�Z=j�~���XP��lw�/I�?BƎr��� �(-ͼ���&���FUdE	0�j��l�Vw1W����v^it�l'����L��;�� m����P�;[�3_9����aF�H�r9J�;]i9���d<�TO�,f��fI�D�5����O'ax#�Dz���G[�;3r�����>� �xÎ[:~Jܤ��<����r�G�eA1��\Jv�]*�W�u���Ʋ�I�J��V�?���~\�ش�C�P��).���Gd���-i�;�~N�S��`�j���v%�����݁��~��oO`r�a�ל��H���U�Sm���g?�1]�T8x���=#�D,$���)-WǒP���GQĶpj�w�3�q,�����J�-� �z"�{�S���;}I�ȋ�u60$���<Hӡ@�	:X+_0%��uU�Դ�6'#/��T�k�aSgC�n��E�/��oZw%�#~�U�@�q���͓�q��|�f�F�Ѭ����Ug#����m�(V��ʈ�R���
�C��hNioή�-    Ziƒt,��.]l����M�f�g;~ڧ�6-ϻi��i1�{�gq6��gA�`X ��D�Su�Լ���A�	п%�>Eq� �l�8�-̼<��)�ɍ�ۛA�/}!'i����e���@�uOl�8�Y���疥&n�`��������I��i���ue��g�/�L�m�ᭁ����g꜎w�z�$�.��n	V�S�`a����@�b�7�mn���!�ʢc$޺ŵ�͢yl�-�� <����F{��/�+����~��dIoԇ�7l*|z�H(@�xs�0g�a��_�Z:i������υ\F$1�@�a��� 玃#����~Ji�q���B����$��Q��>���oQ;g�oUmTw�tP�81���I����#Z�BB�9��H嚧��Rr1��l̓����;@|�~�ya�M󷶾�����E�tBX=�׺��
�v"s�/D/֝�'Կ�5�eob,����8��PAt8 �o��{����GE[}o�&��1݅Zk��Iq�5�)��n��Ȕ�o�bA߅Z��ْf����� jVoԿw�K�`f�J����w�c��e��zB��Y%���v2>����Aߙ3 �`�Cl`�#�2��v�29ѻ�e����8��1;,D3"�l�^9:�>��U���V�x����:ʡ�*��v�㎿����I�+�|C�x[�F�g�v:��X^�x�/�>6�$ߨ�Q�o�{yd�4{����c`��.vq��pO��$1�$���^�R�{y�@�^!b�ʈ,��!�f�_��7�t�t� �s>�f`�r��ޣZ{�MU����I�F�5Hr	���z)��
�μ��Z���7�^�gk;w?���f��}�eP��U�I��J�B��ĕ��5���9����ޅ�4*j��{]��K(�$f�Tgn�{�;΋��%B��F5��tl�O����C�WB��qQ��@2_/� �����ys+��a�K��	���������<�?ڀG����+u�*� vTFx�������>��Q�D�$KTx����n��MH�Uz>�0qN�PjD�,�į��N��j�sV��L��2&KO�c �j}%�F��TXW�>%�>P
[�2i���r2�/�"`��Z�r���ȜV�`y��ʠUy�h9����R�>��&L�XN��"�=���!���d?�#~���22_��c"y��$���^��+�?wQsN��� �[v|�&����%Ї�X��q>q*�}\Ӗ���-s	�����U�HGW'd�,vLO�;h.��C�/��FV�0ۼbw�<��4���G���"�e�x`��U�V�o`���dG�R~�μ�qfp��$�̯N�PL1�#-{�H����J��i�K����J\B�����Y<��k(33�U~�o䧥T�8�&b�e`~W�K�z��i6�T �Ї}�w���S���p?:c�6t�z�W�8��dSG+��$Շ'>���X�6��.#�"V���
�@�ُtJl*[�tr~}_nf_�����n�7Yzg��Qt��V��n0�oo���<����CU�@|�{Yo9f�K��G
x�8쓱�<@D��k�@Up�O��k��~R�_�C庺le?qA�=�V/7�(��E���|���t/���Fr��$�\$Y ��O�ߟB�:�u(@��c2f�ū�5��9=�鞽��U��-?��
iQM��+��9	��,�=-SG��S���兒G2\��ƹF/B��+Px�S�U�f�&׳�������s��@w�M�6~+�	����oh8��ņ��($�ٰ��y��c�g���C��@���M�<6���?����2N|��.g�y��g0�Å�&��
�5h> k�y���J�Td�B�����O��M���¹���od"�	�C��;����J��[�V�(Gr'=�M6|���|�y%t�YX�]�����-�[��J
IŠ�9ޒ��6�2F�;o��5J��<�b�z�1�~U+�{ �Q��Cop����*�SF4�I�]� �X���w� ���@@�������IIb�����1\E��KĪ��"[��`�Z�vwӝ�d��
!���5H0r��t(�����BC�A��t2`�n�Ϣ�z�?��c׶;1�XV����4�$����
,e}+]�ƺ�<�1Ch��8������16ā6�E���'�6P��^��7X{�_�N�3�)�����ܗ�mj��´ߠ�%wƔ��w��[��Kx�_� ����O���}�Pۤ)fk4�@� ��\Ҕ Br��K�2N�<����C���v9�A�$�*�o��[��]��ܜ�>[c��H�^$��{:!"l��Zqlз�x������M��GJ��w�3\B�0"i�ͤR�K�� ��_���;�`�x���D�+][b8�AV����_���G��A'�!�oY�Z�Щ�E�i
�x#�g,$"4s��KRvҞ}�>���W �P��y&��}{;�~�u�׮q�E�,f�|��|5/�Q���3�O��8ħ��Zab*7f)��u�@UR�bo����������%	;/��"Um�t���}(2H�2��8�lA��%ҥ�Yέ�2ߋ#7
��T��
x1g�[���� 5�[�c��^�,�ǩ���X*z��}-z��Yp���8���E{Ӏ�c�Y�=����MVي2�w	2�x���� ���f0���)�fM��i��P��W��7�#��ZX�I�6�]�mKM���+2@�Mz��ĝAO@���3��J�	oVW��;J�����Coq]5�*�'�&�#?�~���73��^E��׋���0t�3]cy���4�oc�y���ǩ�ٙ��{��L��~�'���h8�(CK�m{#���~�P���6�%�E���`���>_���a�]���P�t��ho�Az���c�=T��¨+$э�i��Y?q�.eo�i��؇�M��{Ў����C@������`�n�Y��6
ki�.�JVA�2��I"ܜb��u+��ү,�r����~�M�Pp�>� � 1�Bב���<5�k��#���������a�nH �D!���އ�t�Lß��d�{�tYq�|�/e@��]�|h�u���`�
�}�=WY!�����VXx��
m�]�
~�I.�eTO�\���O� �+)X�M8$������u�����m�|�ߜ��N��6�uh�{�_��d�g�%�jZ�n��������tM���K���&�q��2��'j�Ml�N
�4~�yDJ�~�b�z7T�:$�S�j!��	���a���1�N�	�jŲGM�,�m�O�v�:��G[VREH��h��?���LͰ�+��qc�j���\xC�a@j!E��*�e!	8�r�"3���O���ZHFlI�x��4y�i�o��bcf��I�F��ѽ����6u=$�`��z�9 ӅZ_�p	
>F�Ytb�j+���#�t����'fkm�1�����׹5�{9,ΰ#��8*�/��x\�����To_pp3��{�!9��P����i�=>G��DQ�+��G����4�#���"�`ߏۡ6�5S�V�#4.r�[�R��u{ �[|o�f�@ȁ���d���5FDyڴ2P���~dM<(�Q���a��#,Q��.���w'ۻ�jFt�~	��(�Bk.͓"|q͏��Տ:���$sD�Lzyר�����y���,�^���4� �����p�|��,z���7d�΀*
.8�L������2r1J;4)��$�G�Wo��;���?��41ϱY��=I#}f}]u��"�n�6�;���^�6��c���e.�5�f���'\P���`���۔|9������L���cʎ�oͳȾ�+����ͨ�fX����-�;�����8{�K�\�?���T�'
ڒQ�u��^�L����O�M�{�loz���G���!��*/3�j��8�E(���(W��8֨�����yB���i%D�N~3���gKQG#�>�W+�o    ����#h�Ƀ1D�]��,�;3j试~~������� ��Κ���S�F�.(QI��n�\�$k��*3��W��S*j����ia��^!|l�?Hb���}�lH׆l�D_D����-�o0ht�9I �k�V�^T�b�h��{F��N'��6�tV�I��W��v��"���ͅ(0+�OK��QTj5aߖ���j��e2֊�̾qV����%?ނܓ��������t�C��N���v9��#��4ĳ��\����ߝ& �8MP@�&*GF��T�}7�t��ˠ�|)Y/�*���))�t�Y`���F��V���AH��#��,�P9?�@	��9�ʪ���Wלh��N�s�c�� �wF$/l�|�.k���TO�J��?�η�W�'����{�h�z����=�X�t1:L�;����b��}�d��,���h�
��#P���"�ۖ����/�}+�Q����F��1�S�qLݟ�g+.��bդ]����
�ځ%/b�肩�	|���^�P�m������~�w�t��>⢟�_�������2K"�P�B ��>�!�7�IW��H�]��e��&�y��6�٘� 3�QZv�"a���Jt� )^/�"�{+A�L'Ɏ�A��^�U�j�E{�:w�Ћ(�.l�8�>����p!.{��\����T-4{��8���8ϬNǋj%�_2oRZ0G�"��4~
7��Iz[�s<|y�o�̴�R��BT
�"���+���F�j+W���zZ����ؒ�*��~�X��CI}*6tm����Jc�e
M��HD��i"(##����G��1��X�ܛ�����c%�l��D�g���~z�=n�a���$�z������@�	�����]� T$,���H�A"H����lK ��,���X��:�5���j�
�_�#i��~'ǀ�AI�w����莴�4Z_g/�hG������K�X������U��!��Pe���r^��Q)A��}]�A#��ߝ'�˱~3�͕������+�FG��~��d
�%b�� �r���eT{Q��%�Q�����`�33H��89�n�I���%{�p��s1��i�W�|p���3��E�]1w��!@�<ٖ5Q���\���omC�Y��1�M]�ߕeq�J=b[�m�����A�3�E
�~Kd&3�;��u�$j^��>ɐ�8��.�u%~��[�:�.BHz!G29���!e(J#���Y.c��Os"DX��<��?����.���B4`Tluԩ���h�˫j�3s� h��~��7�/��;8�aq;��(,�A��Y��o��S�&L���Wp�:s��#��?N�����ly����y$�������7o�$�:)z��Qu��?_tAJJ�[�=�����G�)1��&0����a���s1 Q�7X�do�Z��`�:���S
��Ų`xolV�J2���`H�<�)T?���^E6��ʡ�[�lC�%˝�ĕ�Νn�W"�#п�g��T>�jm��/�H�'�'huN�ǵ�Eog�LI_��|��T�"�$	̠\���mJP��r�Г��=�XIg� bop��0�/EQ.$�ޗ)̦�3��YQ)O�w�3�E���c}��2H�{�z��\q��ۮA�H��D�7I��r?�rv�R��+�Ȅ����O��ĸB��0U��n$;���7F�z�ዜ�M��q<$PO��j�G^�d�E��Qh�M�Ù��4�yH��&���������?(!�X;����9\��dv����I��)"��);�H��GD	1���їg�/�i�o�:ø�E�L��R\�[Vb�a3,��/�6�S|\l���eo��s�j�!V�����"w(⅃K�1bj���?	���/]ħ�l���N�1FJ��(��/L��/i@w� ���_���N�c/����Y�ˊ,��N��tx-ɻ���N9s�k� ���.c�%��������;jH���٨CpWKL׌��`�0rڳ�o��
H�t��fA�HǽE�:L�3<�h�I�kO%	��ᑂ��G֜ݔ�(vBD�d-�?:k����I�w��OO���f�22k�bЧ4#�Ɗ��\� ���^�ykV�Q58�'j�7�g��I�0�{ɢ���_�>�s�\�d
>��g��.e*ݸ�/���
n�霱��x�wZ��~pn6[���&Ï
��Pi�8`5��b��oC:��4%�h(Y	���[,������3�#�d��a5��hp���Vա�������(TD&(�5��c(sG,h��!U�q?�7'Y����c���Ĩ�	����������@�U��	A[���S����9�[�IC�M_�.'����0((w���1kJ��y ����)����>Uk�6!����P���/#�����[̈́|���G	��n����J^��17@H7A�Xp1`�h�|-���:�s� cW��0Q���)��ԣQ��g�j�U�#τ�kЌ׳�(�hX�V�u�K	��2PL���sæ_J�5kM�V(M���ߙ+�bn��cw�U�jx�ֻ)دϛ�K�#i����NP /˽��ڊ9�|��%r~���ۇܭC�a�m��Ύ��\MH	����"���		9��i��ηY��"��i���Ys]ۢfH�pI�O�q=`��������[��y�UEX+�6���<����9!#į��5����;�L�Q��z�-R$�J>�U��ipEC~1ܙ�=�9I�5A��R�&�蠅� �l�XM���3+z��J�qD���hh���w���{��%'#m2!C�PN'OqR>���n�0���m�@�.`�HӁW���8�}q�x���c�F3��}������i���G���Do7wϽ��4j�
�O�����ͱ!�p��t��"<>imɒ?��F�7⽱L��8wt
.��ѐvF�h�A~��l���<�z�r�@�����%���3��ͷ��^�����=Y-{gn�Պ�E"�;uYa�̴��%�v7��J=B��������w�%_�
R��bv������l`���;xSh��nR8�}7�aH8�����	���&��lI;���دf��3�H@�I\�?�޿�~��܏���E*Έ�&F zP4-7ۣ���#E	8?�1������օ��ΕD7{�j���J��؊�>�v�%�Zf�o0J��y���D7�WF/���'4@�����H�"��y����]�A�Q�\���f�cbL��������+P����"V	tȤ� ��p�]���@8�b�-s\)����Vo�F��(xF'���A	5�j��&�K�F��ۡ?b6,s����l#���X^�*�����Xr�����-�w�����?�����2����h_s?B�����ʪ޻;ڴ�3�n֤��t<Q�I(����i�}e*%X�)�}F"��Y\�(����{u ���Y$)X�+kð�~��/-Y�Țș��P*� qgsg(dto��=C�8���U6��Z���u���|c��	������&�)��^ǌ����G�F��v�'h���j7�h!�/�����3v�Ea2e+vF#���|�R�x�����%�yXE�v^�V���&3S	/�d�	��Y�_��x�4�7m�USS暺���+>��J�p�b�$y�x�t��'�JMq �q�g��g��$Db1�)��>f�t�Sp��U��rbs˿pF�P ���yg ���QU��pIT�M� ���e�㹺 �<�B�%xv�4�ƴ� 4m5�;7-^���ݍ/B�Ǌ#q߾�[�L�f �f�|��HdF�S��Q`.�0�e�´�W�P�+��	�Tl7~`�D@�L��]!#7e�
����5.�QwFyG}�ǃ�K�&����src��L��
�~�sux�-'��̻�V��쨠��h�mw9[���aB�Cc������[��KQ���_5acO�r��'�+D]�%����-QS��6�5�O9��}�<L���	>�'St3��_�7 0G�[�o���W�c�_�e�    ��ޢV&����[ht���M~�Q��l�м����9���T����2�B��lm�Z���o�R��˽�r�_�鶓<�_��乔^�*�������o��aY��|'�zd�֮K�r%<���U����͛EV��,Ǭ�$��x�B�g��?l�>��KAp	$%Q���l>��7dq��t�G�ct�~�{d���Q�D��?����E���L~_f���Ae��TFJ���N��i���.K�x�|e����Ą���[T:+��o�2e���_H�<���� D^�z���[K)�k谥O��ѱ<���)�)Y�,S�m����]ԨEfBE��#�@됕oy�� y����&���)Q@>��H�;--):ש���d���{I�:~�5au
v)�����j��Z��v���$r�6՗����fb��#��r{$��ڧ�����2����C��h���q;̬���k���b0.صRD�ɤ/����DA\4��M�\W�{�x,S�8�=q��_�=v��@���D�{��$�o��էF��4���P�����~��##E6Yv��WM��ڢ |�_�%��72�o�*Mԇ/����B�
lx��TU�����\..���4�a|������w����/-������k$i|f��W3"�G:�KB�_��-�Y��ꁻ~#���`[uBvm�Hc������Q�누�d����D-�5O'F�"��R��Mf���e���.=yl����/���6e��~��*�~ڌ%ZH�*��%����� �0hĘ5�.tйGA�h����X��)�L�e����-�N���a�vΠ��� ���Hl@g�(
]�l��!]L���ivȴ�~� ��O��CU%�X0��G���1���*�{c�$Ýu��7�]���@�?T?�g��'�j΃͖�-��/� H�t��j	�=��*�M������+�/�n����7eYιR˚���D�f�Ն$Xxpz�1i"��X���>%\�۲M@ ^F��pv:n��2���B�Jn>* �:9*	�(IK#l��+��##�R��@u��Ŀ��_���gus�j������_�<=����>x����Ni8� �ht(ϧAB��M��M�N{p�{���A�hM�;V܍�Y�8g�msq�*9��|ɛ��WBǽU	��U���\���h�,����iT��۝~
�1@�ͫ�y�|J�plM׊��r.�NFvR�v�w�x���.��h�if��۰�W��-���.u���_T}���p0���wT��㊩]|�g�C�a�A�a9�
g�������`�Oj?(�HЫX��@Q��~R#F��yI��3�P@"�.W$�4x�\�Q�QHW�/���t��.���A�����5B����D���ɇOH-�(w��1Pi�'e���8��	��^3�vC]�e(=�xf���I9�{kN��S�C���-�-�v�bݼ��V�z�����*�b�V�l�C'*�:K/��� 3E����@��M0�[����&�yG��q�>7}����q�,ʹ�nV�e̘��B��j�;�/Ȁ*eɛ��G��'�D�X��~��c�Wq8��΍��g����f���"M��
 �)���tG���ԝ�>	��t��j������G0IX?�J���3���H���E�<�Ǽ��C���t�����9����L���8�(UM3�m��N��R�=�HIL/�=? _H��1!�y�.��9�h� ���{<0��~�T
^���I*���MW�S�g@`����<u��!��j|ɓ�
�W�Uؤ�c�Y�-Yj(�_�%�ߛ��PQ����[�!穗�_�\˨�؛i&ڡ�|v�}�5	
z��|d�PL������ȶ��A+ G آ-���������7�?s;t�o�o
��-���?u��}�e���� �]��;�`�q3���`���;��l��JT�O��×�7�]3����	F��#�EBļ@2�3�VS�g�ׅʧT�ˇ2�x�����8�?�����bלMR��ז��Qʤ,�6�ڷ�z^!t�
n�'�<N��4�'����^>$�����]�@PpM� ���a/Ω��[����[�.�˨j!�c��G��%��6�L�}�|�0��x�����,�A����{�w��G�P�["p�����q��芫���è��Y�KK/_Ց�X�}��ߟ�Qn�L7��5��m�V(��pG�~v}J`�J����5ʧx��I���9�Lo��˦B{[Xǡ��P���I8�nR_cY����nP`����9��|��i���� N�иD����$�+c(Y'}O%lo���M�L�&��J�ߝn`Z�vlԏSf�8H��>h���LK���Od�e�w�|hB�?x�?o�h�{���8!��ٮLM]j��(3�iv"NgM0���۔��h����Kp�䕍����l�$6�ZPp0��13%l�����4c�O?vj������ʹڹ�o��5�ᢍ�DKF��Y$4w��ț+g�h�+�	,H;LIap)�xx��m"	n�#tʾ�)�������z1��&���#&��p(�c��}9� ��_\S�d��[F��>�^8����i�	�P6�7��-.L�-:-�)<���8A>z>N�+rO�4���;��UN���#��G������;;�!$�ߠ�9��&�u��y��Dj�4I VD+��*�����`�V�}���L2�2V����e��PqLO��x|�8QQ����5G�_I��|����.Xg�f�97_qUX�&�0Ak}����5?��fw�o'��[�4� �l� 4�	sk	�O�i�L]�:6�V+��G��85(��	���ƨ���>e5�]V�'lN@��3������w�貸��B���~�j�`����Q"��z
�!08?��Ʊ+�k�F�`�pS��\�b�D�Qi��Q�Ҳ#��@�+!4�}���1LN����ƭ�c�i\x�B&x��QP�x`6��U1��F���fN�Pvr�n����N�����)��+�m�{�; �<"�Z���y���4Q�!p�o����b)a�+sD�*y$91Oq�C�wJ�*[�q��䒂B͇3R4ߥ�K���]��i������k��sd_(�Z� {���􅱘$57��j�1�����wP)���)]��a|��Ư)�l�+p���Ъr����.y�#V��+��e��s��F84Ybvi�#�O�;�0f��MVP�̆1�(6v�?���(bR��˘��G��`���0�r'�m�#��I~�"���Ѩ�$�������#����� y$S���C5�_S����P���Y9��UQ��q?\L���[C,����!�`BA��%k�7�0�d���Q1���QK���+�7d[H�f��B��
[�},��+h��{�/g�7�5G��Y^?5��4�l�V��׵6�shҢRr��J��s���������5�-�{�@)͌�v��;�$�����_�iЛ?g�� ����:��}e�q,%��~&�G"y�;Z�b���B&���Kn�6H�kvP��\Jf؞L����B��S��wNI���=�ޢ��GY�k6���Z!�g3�b�s���T�B_�b�q��{�L��C1��y�x�-����˸|_�,�o�+������E:�ǻ��|���]?�{{��Jі�I��=�e=�����ޚ��;���n�ő���_y�q��-�F�v���б	 J�p��ݭ�n�Ò�c�Z��O���9M[�I5 H��+H���4{��䮫�)8D#�aK$��5�n3=��(?C�2U��@�xh���<�g##�(5�H|���#Z/'�%ǆ���-:���B�i�@2Y��9�o��$�+��f��g���r��Jv��_���0�bRE���G�|i,�����9Q�	C6y��cG,�B˞!�#�)��ŧ�x�S0�3)~�%�j�(�5޼T����7    _Ɍ�8~H;�����$���t��D���` �N��Pe-ɖ�S�_O�FXtb�[�� >����y`�-���#^���T0V�!�Z�T~;="b�R@�DBed�ڤr��W|�����÷(˂�3�5���ɐ�?���(�V����~�|��������T�㋣_f��aI�0�&�0��t���-��[�`��]_��Zon!Cf�?�_L�Yߐ��	��ksI�
��ԆW(�d�I���D�r	6^��m�`Ъ:���Y[��@fO�{s��K	V?j�����uw���{L*��êq�'�z�� ]�MU�����_��K�bi=I�_�"�;�5XUR;�����n�-=��gHw���7�8����0iǸ&�_��g6#n�`��ގ�u;F��ƔWHb پn�@�\4���!x�R�h��LK ������I'��O�/ވ A6l�@b`���o���؞�����O4�*$�=�CX�uIm�Q�T���M-�ɦ���ε���>��'��&:(֗�6t��Q�������B�&�G�[&��=�>�ٮp�T��C�ٛ����iϑ�aE��c_�C|�
�Z���+��1���y(?K
� ��"� ����E1|����Ia������xJ�<��a���e��������Yg�5�)C�&�|{Y�8/ �����T����ܑi�/B^M�'ś��O��pp�o�3:S�(;��뜺U�})F��)��+i�������يV|yk����>�V����͏���g�@���G�!O�F��XmE�n>~�k�1W&,��fة��ىc�Ɓc��;���]���8{���/C�����M�Gj{�L�P��tn�V;�L�#P��6���0B/G[����j+ڏ�$a�����p��aiY!1�ϯ�η�1S����k	?	��l�C�"x�v���	ٕ�1	��>#��E}�����Ц�-e&9	�w��
�:�P��$Y�|��7��	ܚک*��y�����ۅ��#x�i"�i�W�d���)C�0� �1б�N_��TB��Waw�f�2���vp]�E��@0��zq��dw@V�rxP Fp�*6�����{��+]g'8�͖�B��Z���L�ё�T7�B��P��g���`D�H��9A�t�rw����Ayٕs|C� �P����M��S@IX��+�m�}`8�7[Q%s�.��p5�g����J��C*&�?M�t���^'2��=�͖0�,M)�Z"193�+�
��P�.)&���}�9�v]��,~�]���"�%B�����sw]U"�>~E���7*�	��a��|���?�~��s�'��.�]Y&�S��hQ����ҝ'���o��q��F8O:G1'��aFh��`>��Ȇ��Iok�:���zX�L�f[>#���T>���s3��p^��]��yo�}X2�}x)�̌�'Յ�� ������Q#�d�!WI��5"m���wf������*,Jp�/�?�U3���ٙ�2o��lĿ��KM��ҮS/n�6^�xU����(	�ѡd�����6��y�,�k��!����Ω�=�-��q�?,��.�k�$O	=�󿻐�1��A(�5�d��[�������oᓏ/KR!�5�,>m7��V&K�����8�,l�#PE)���	{<=�i1�K��LD�a���~�WҔ��TI�a�In��:��u(d]�\w�D/������K�
�F�LN���K��mX�rc�Z�C�����e����
ٿ��Ekgj)�T��'�?�)��M��m���Dm��Bd*�����!���@��F�*[�j��? >6 ~��4�Ք28;�M�Y��h4F+�q'��:�ْf���	]�&�Jo��{��L�Z������^��C�C����Q`���e��k�貗��#QZ���ɏ�X>����ԇg�p�7��.K�E)#�� V�[guk��.��]/v�4�^#/�^�^^�T>7y/���޾Ƽ#�.��)�o�t�t.x��-�[�yh�{jĈ.Nڹ�9�ԢW������RIT�����ߌo�[�gT.YY��i<4�욟	�bK
t8�y�ƍ�5c�w���+���H�j̖W`�dY7l��?�����1��_�����d���/p��%� �ɀ���w�L](-Y.���`�Z�K���n���c������4O�>���ILB�3���Z,��:{�q&�8r�ӕpL�T*%L���Tn��Y��N4��:{w��>˲�I�c�M�f�[d�T�����]��1��S)4����M1��}�#�*>�,F�Z��%���HN�$��/�Oj�f��x<�e:��������^�۵��;�|��9H4��_��ţep�I=�V��D���=�a���fĴ�~X
��޷ߘ�f�W�Tr��C3o31S�d��=�օ��Ȭ�M��\��M�aB��P/�˽3o��k@����{]�J�I�~v�.5���j���v�(GU�����T�z�>�\�AD�$Ho��i�b4
|��3�/�6*����K�R�%����> �	cG2��3E�dN	�����υ��\��H�Y��Xa�KХ��.m`�~ ���+.7ߎİ��N�����5�ms jI����������{�xԱ0���>��4�C.Lc��M[���Y;�ϑ8oł��8�ߙ9l�\�͢�h�I�NjH'�<�"�^�q" �$� =�JK��p4i<�rd1��PD+��m1�VWn|<T��B�����Q>]��iNӥ�.�]C����琱��B�K`��lv�57	u֟��g�q%��c�D!g���h� Y��:�f�N�����"���!�(�������<��΃��8ȭc�`���镵�UK7-�)�� ����8:oqT��>py���.��BT��{}��f�������VJ���ΖpH�r5�wo+14��Ů]x$g-�0w�<l���BQ��zr�ǉ	>X���2��W8s�b�G��! &Gu^܇�����w��Ӿ�|��2���f!�Pw�I+�+_\�K.��ޖH�fY���bĞg'�|&���KQ�WWb�oϱ���$|�	����:�l&y����>�rb{!j���������X|�d�>羀��	�����gK#Si����y��x�Be�/�$PaE��a�����K�u�����C�b��ʸ���-�%8
^��a�Mc[C�|��Pr�Q{�$#R�m�f�̬l��L��ɺ���<A��I��'Jp8/Ǐ�������JD�m��\�}Ő�A��Nطe��䫑�Y#�U��M���\�C� �(��h��])�H�9'������h�o,�;*�:����_Y��ũ��O�B}��]��g���̅H"x|4�+�ƺ�(���̷�CD�\#�,��8gB��vD]h�����e	`�'��drW�~�������r��Ȱ�i������\������v���JA�pE��	N��]7z��k+� �o�k+��o��:����sb�R��B�*o9�ރ���4�$T�{P�h��ˠ�6����H��0h�{���>��m�IǿS��ཨ`"8Y�!��&Oy�5��\J�C��<��F-��P�t?a?�8V�.�we�~9U!@�~���D��N�Z?��u=o��4n {ځnGu���U��[��{�Z�6����S���=��5�#�0.7Btъkߠ�z7�
#坐���)]�h�D�%�j�O�5��-�(�V�#�Jck��~:Ks�|��,�v��e���e���}sm����S�"jN@`#�#@_�D�q71�οJa��nPB�I�aT����٥A�S�-r�o��p!����#��+�k�l޽�bGe��4�A�I��M|����� ��Q�د��p3|����p�����j6mp|�Aƅ�w^1���i@�c6"�U�B}9����׎e�� �>>P���F�0]��c0�&�1;"@\�Mǝߢ� B��l�w�n�"|{;�z��n�    d�Z�:yLm��A�}��{!���SOj��b.@U+�t�k���,�/��^������=R��*܌N���3d�%�g�Ioq��M�:�k� ��<Ң�!�3���3>�K��w����bvd�g�p��2���g�<Ʌ:M`Gs��1��%6�Nj�S�[�?h9,yO�t
�w�QۓZ�(U��'-�-�]��8�F��n}\�#>C�6�)p6D�?�w�k*W�����
V*��8�_�"pG?,[ᝎ*�9+�����ۚI�fa*��'V#3	���}�u����$~�{ f���5m �x�_D�#T9=�:%fR�~�5[�����܋�Fqk-���`����3<��ԧ*�-���3��U�)����]�@}��L������e'�^��#P������	����h��s{��ua1H/tu�5!�aҏ�a�oC6�&.�=r�7r��[P{'x�9�~�;Q��cY��<$�h"��V5o��$ߥU-4��X�_�S�8P���:)I�b��A\n��
��yPQ��q7����Y���cdô��|E N���3l�]��x߸�\��ǝ���OXz�����y}��P�p�%�-���k�]V����a���݄��K�^6s&W)b@����Lm�	�E���ʎ�rCjd��6$�O�G|g��	�x'c	�"�)�Y�M�T�Q�T'�����Wfes#�Ѩdm��K�'cx�O
��m{u�+ ~1GU���P1�v$A&��do��JAC^���'h��.��/��P�"�؇����2ŋ��q;Hn����F�v���c!%��\o�D���3�td'�q�g��P��G�dј)�h�`���%?C�g�'�������W¥���f�����=Q!�yDc�[G���J�|�*Ë+6��
^����R):�ɸ�U���gA���2�_)"x��� �a����zq{�B��r�E��Dhn*p�L��~�|���� �1�瀠��^�b�zX?8�g8��@?�l�Ul��DA�=�۟��]/J��[�g�}q����-�vy�FF�M�Q����I%^�6��\erv��W��c�c��w)�u�r나���o:���e]�s	���}���gςJQ��q������-����!�&�ӌy��O$� �������vO0��reř�А ��)��*�t j���[��Fm��~�x�=����ƇȗoM��B�^��o�1^��OV���?n��Rѻ�;]=�g��T�r�Z�V���,Tr̷���j}ٞ#����ԿM*�.��l=;��Y�	!��y���8�;�R����>LJ�{C(����Bap<�\�vI���{����[Ye�4�Z:���m"
8�2g��=W8O\g�7����/\�m�fdR�SvV�zXx7e��i�79�6�Ew�l�Gd������j�e�����E�HS*I���h蘍>a�����t���7�]�&���˩��%\��M �u��Bg>sFX�\�r�s���s��.<M1�}�r	�KI�3ȝm��W.@�3g0Cw�ܪ�Z�>���%e��qq�^��sܸ��be�������e|$���H��Cܾ��J��_c�	�N,7\U�B��M���87��֫�.^���8F5~m����<kh�^ق�Z��n�sD�e�Rq��+}n?{&	Z�Y���$�\�m��:��3�çߗ��zj�E��6q��Y�OIX4��[x����}.:��p�*�B�+ ��`Of���JnY�1����g[����H�0��@EZ��G�`C��NLT;���
��{_>L�Di~E N%sρ�8}���v!ܸ�O���^q�h0$�4�7�����5h �¼��*f
�ie�*�%��ď�p�:M1(&�Ҏ��f[cU�\�����*�X?��Z����$�l�
��Г��0��ń�?���!�%9F �_�;�����Zi���	��k��刻f!���>�:��g��	 gRG�\��pTrUv�UT�D��l���q�կ���\]�;{�_X.dbB]-gO<D��pc EU����Iq
f�Ǆ�Q�����oF����+`T7g��@�`�	@��+@����b�r�Ժ��2Q�(��(�~�)�c*ߐ���8Q��L��̿^zp�W&�lm�O���T�l	�"���6E��,�c	5���\�Va����BU�sT(����&N�F�����>��(�4ڒ9��}'����먟��ϟ"9yh@9�ҋ�1�Y�^��s�x��x�A���A�켝�Z����U[��ue�
��i�˭��b^Qt��㜾�*����������ͬ/~T�z�__�x�E�+��Yek�(�wMocQ�0��0W�����	6�Adq+�[��e��[�&�ma�Tح� r�;���������;��^E��P�L ��
��[Ԣ���Ê�?�,Tѕ�L�ɅׄoGc)'��p:�U�AƬ�=��J�#�z��^�̴�[��'[ㅋpՃS������y�켓���-�l�y��<��{�_<F2�I�����S��B��9F]�g%`I2�M���U��`�gX�����?cѪߦG=�T�n�{�Ј����2q*�oǺ����w1��-V�~�G�<�����i�̶�U�ˣ���'��~�T�t ��8��a��8Z�u�����t���u���i�~F�r���=�.#O��WsM�y��8��4��؆͋��ŘVj�Jb�QHr���x��";1t���ǹ���٧�o�d�@U&�(O8�� ?��@�����n���$a	���S=K��`-Oq��n���a�ԧK�A�z��%�����t�/�%�>غ���s39�94p�7�ds���Ys��ӹ��@^��N�B�b�!Tg�?��fh ��f�!P�#͕6q��=��+V2�t�Q�4���M��>�H�O��<�
��6<}�gxu6W��D�R����k��U8�90�w=q�@�2Ix74��'��֯��I+��̖��n��3]����P��x�_K��O|�<���;��I�P����Q#�x���ZE>q���Z��-0����5��b��
�y?&�|�ۍ+��}+\���×�`j��g~�$��C�q��g=����'#⵼@�^�<�K,�+�St��#�#<%X��3�ٻ����������[���>���AH8BF�7���H>S�*�Ԩ�e���2(�j��.�f�(gB��~�K*��M,��;�S�7ܣ��y`>�,e��1�a1�FSG��_��?���׃�K�2�c��5��?��7~sz�Ƒ�2�8i� �2˅�a|ߢ�"G3�����}�ҿ{r��|	��)qk &��g7Y��H����~�Y8�6��'����]¸�������?��k���7|g��Q�9�ح�/��~?>Y�mP�������u��H�7
�Ψ��l}�
l�-��ehO9%��]7�>��A�&�{���zȵ�\vŒ�q-Z��B|y��L��⮡V*寜�a 0�����ܷ�nzu(��?%e�m�@u�$�����"�@���b��X,lJ��H�t���>}Z�`[������$Q���Ŵܮ�@R�
�P��P��K����h��U>N������dk��!�#�q��cc����v��6�!9�˄P&]4	8�ٍ{�B/WE������mA��<;%��I�R�2߇ȣ�o�ٙ��ˉ����GH8JM���ß��jf�{��7��T�p�q�+;�����v���F���X��Cj�t�@��e�ZT�%���a�Ո}�[B���֌W!���oOr�IK�ؐ�U��<����D ����${&`*��N�èTU�7��'����t"���jo54�)�w���ˉs������v1�Mb<5�Z�FB&�)���&�bF�M�,�-]�� 6�QSV?@����P�`Zf�� e��p��9L�INf�~�8:�-W� ?��������_��s���]�U�4��Ag��=��2'�%��Y�K    �"�=��[�W�V�i��ƶ!Հ�!Ĭ	��&C�%Y�e��{'�<@wE�>=i�d�s7���LG�L.Ĝ��
���!4s�Ϊ���K���\Dk�M�
G���.�r1Q���.�g�ߒīsd�z�Z�&swx��@e�m&������]7|[gP�.�@ȯ#�'�"�F4�{�ݠ&�a����y&UD�!l�Y'�|�V}���|z8+s���%��Y\���4}(��h	�8�=�9���7II@2��n�k�ٵo�P=Si�8�,�䅇3�ṳ��k��\�-)!}{��ۼeE.C !��������J�;B���)��� �Ի��:Qr�z1B#��尙̞y�y�v��8�َ&�)����7hd� ��e����֘tx�}XP�R���/���7��~w%�
��e��R�㘾	��,��_��Mb'Q��COz9G��G9��3W#T�އ�����Wyg�[�0z�A���pKT*ӎ�f�]=L��"�|x;��t�Wr�s,�+?�c�e�2�;�1PsB��ф�|(�P;V܎�/�_��{�^K�D�[�vON �2��v(| ��&Y����?��� 2�@-���{��6�:�K����k�4|-��[S�rGWx�T�� lL��e�� ��M&���gNxS�E�������K�^5�o��W���C�2�<�%	���<w{;V7�࿛rCT��[o�L���|s� Z�)�_��Ҩ'�YW�K�W��m�{^9A��ﱴ$9��C��&J���j���]�*Y�nN}�v����o���8��em�߭���2�a�uG7�|��e
���/Y�_c�d�,*��I&<q62�|�č�*7�%|fi��K�����W��B�)[�c`�sjI��� �	/>�	�A~�i�1���O��t��|�}A��M`�ws� c�U����Z/J��w�\!Ҳ�1���#(��~^�ݴI�th�qG��z�j��1.��$M)����؜@�"���>ft�U���� S�=;rdeM���&J *r��*R��7��0�F�E�|�W�#)��գh�3"���p�fȧ�<K8ʺIJ0*�_�k�=����_n�f�T��/�}9$C�'r���
��x���A��g4&f���^V����Rs3q���h����sM�FaL���㜹��{F�Hۘ�l�v��>gdn�(��;�wC����O����	j������뢔s;r	���"�I��g�e��c$Nk�"S��q���>�jo�R�������e\z?��(�������c��s�t�1H@�~����|����*g�������f���i�����Q�͊������������mdr���.������dy��_+:�cG� �\b9�w�t����4�`Ԭ�h���Ύ4��<^�8T�fF�K��ʝ���>$����ބ��T����xP�g�
��"�8�z
no6�{�x�\��o�L��ߔ�����jK��	Ǘe�����bTV�P�R�l�v]0V)y�HQ����G[�WmH�����DFw��N���U�7'�jt�=7q9���(����d�G�y��%�?�"�,H�:*��ܨ��(�������P��;��)&���)NS����TB�4����& �R�靍<4�����H�ߵ�כU[�WA�On�ܥ7N�	.���r�Gj��&V�;b��K����C��)�U�A�>�V�צ~a��iz��l�u���2Ʈ��pv�U��m������U��a�'|E�6���4�:�>�{!�ۄuUҝh�� �&�]�T��cu�C��Nz� �?jԲp���L	)�b;ơH�H���.�*�^�3d�U�3W�(v��^���~��z��	�Q�c�%��L����� �eP�m�Z��c�M1~�fZ/wј���	�z�JLg��3^\U��%��У���	v�?��c1~nM�E�jN�0��M�6��l���'��A|vCi�Y0�4��Q���#AY��m����i����E���o����iA��c���Bxn�ZҺ֯>��&�6��/��H2?8�v��!��K�Be����Ļs'm���{��0����
01�=N%�G���*�Gn82nD��ܴd�,��|��zA�l�Y�U-=4S�K�גgS4ArSV�(��k�L�t�1l�F u��~���Ё�3�3�$�J	���/��S�w�Q#1�E?G�#ք��4�J ��ǒ~���<��
o���ͷ���⼺���
5j��%�*��qk����+�9�!b��s�^�V������ߠ��
���K��[��x)f{�%q0���λ���`����^���p�6��2��8�G�	q��n�_v���Xr���[=\�L�i6aH�\�"8�g<�'xL�R\�0�����w�N�e4�	����p��j�r��AZ�5L!^�>�O���Ԩ�_�ap@�~E�>K"8}�1����ᖣ��n��2���]w�
��){n_x�o���;�����rh����Y�E#�Ks8� ��U�R����2��OS������J��|T�7x���H3\�)�b�kv �i&�'�5�z]�@�#��v��̂������Y/ :�@�����|-�A�$��ʄ�ړf׼��́����ۺl;�m=���IV����3o(���� �Kn#qf_�r�O�l�a��Zn��V)1��/��~���j]�f\�^)�@����y;F�܆����U+Y2�1K5ט���m �C�9L|ӿ����G�p4M��m�<p��+o�g�DI�=�
�F���u�����=����*�(.���e�ei���ʮ���uǹ�z�Z+�ӥA%�19Xk�F6���#T_��>{Q87S�J�f�Jd����\`o��;��%�ܵ�;���h�v�uAN�P��D�����$ȫ8��#E&~��OTN9�X>��.Ż���`ci�#iMݕ��4���U�~��Aa�h�z@p#y�����|��짨aӃ@Q��0�lґ��@e�0$!o܀v*��.\�9M�p���
ӊm;G��Կ7�I��.l�g�_�b���SN<r���F�J�`� �~
��Q ��A�;�\J��E��oj�aj"w���8��v�9,��|B悡O���x�J� �"o���o������o�z�K,����!�?ϠN���l~�"Hr[9f�k�G!���~�!��P	ڤ�{:����?��\	v �pㇵCT05��Z����	;j��$����@�f�^�D,!B<���UZ3���A�Q�PaԖd��Z+0F>��~����x��A�� �(ZaBD*�.�dS�YE�˥�x�hcS�|鄤[��r�@1k�ULK�;o6\��Ymf������]�A��1V��)�~�=�v{|w��L�Y���;����IķbL�?O���TƲ1���L���$-�5].`�.qN��f"�Ą��)įR7�l�$MΎ�^Kk��K�r����f�'���>������P��(6��/5ۀn��/���Ĵc�+ c�����4z��Z6�C)�J���8R��H�k~)"[o*�;�cܰ��U�z5��.!�G&�H:�?��u�0�P±jWA������A]����r���V�M�4�smƣ[�G���E{�~�QH����K��m�p��%�j<N�����dsc���0^�=Dm-�K�5j�)�+��Ҍ�f0F�uLMh����#���+������@+����L 0�h۲��%P�ACA���?$:��ii�1��_����Ʊ�O��|-��
B�b�{�4�����"�o�=�� ���(�zqa�Ä̝ Ϯ�h����
�4-�t�<�"_� �r����S|l�pt*;5�����^柀3Q�9��OT��d0�����#����_������b�@ou>�&����ꫮM���#(2�BeH2vكl��G�s~K6].�k56�,
���S:��&rw�h��/�C�E�Z-�e�
��:�Ht�ꀪ+��\��V+ҫ2�堐�;g�A    ˟^`S�v�LZ��d��������B��N�����&�$�گ?�&�O�'�+�c?�Q:|~?�G�Z���Ȧ����ɲ�D����KJz�H�A�Τ%�'ݖ�q:?r��^ѯ�ܚ�p���7[����i3.���}դ�+Q"U,X&�<��^�+'*4����k_��m��kΆ�Eu���&�͋D:<S�_K6������c~�����¢��e�!��AC��3�+	��������$����2_��~��z���H�h��d?>� ��X�:g�ap#��pa8x��aY��x�����V��ks���y���Џ#QI'J$�b}�6o�!����P�EAE�Y�v���Y��� �{E��,��Y����)�>�q&i�\��g�=)k�=�f%�ݜ��`���a9�tK�tU�}�,!z
�5�2����5'2�ww�� ($C��Ǒd��Kd�wI���5��2>���������VƦS��w�sQC��q����i�x\�/�"o_�/�Q�����D�L�����&�Ҫ"�7m�w|��cK��qn���#�G<_�#�C���p�7VFB�{dΛm�TC�>�pC�%ͫؿa��h�b{��I��G��Y�xo�2w��$�}��p�Wܗs�<��)����D�~8!+���Q/h���_� ʁ�	`-S����yM��
��:�	v���?�U��G�hZ�i7,�ʁbΗd��X���r��_��l���F�Ig�%z[L�Y�0|`�9
�WW�bm7����N%�컿0L�j�G�hQ�*�>��V��(\�.��D�u>�2f�|:���8�k�Z �B2Pʑ��℀��3�'El�=¤"���7��l���%#��k�$6.��������r�L*GH�4���2�U����Bde�#�a�6��G�i,�GHǑ"|������%8����i��/K#x�g�;;w"׀:��Y�V��NS����~�6ɼ�ؠ �����>V}x��A�K������U���_�p񏻣�u���|v���}�i:����A��h����,�˙�c[�(��i���ܲ>;�vvQd�����3�����=4B.m���z��#�Ő�9��M�֢�G�}L�peE��-��"8��'� ����Q1ؒ����Q����(E���Z0�M��_G2������S N�D��~�(�P�\�	�a5�|���DI:��z��2�=��/�,D2\S�����1-W�5k/^�T-�1*�\�B��(7U+n^�C.�����V~�
��g�*)��y81�̕�p�5�&����(wDh�!KIt)VR�11ș��w�}���5���?O����.g�BQ����c��)�A�+���˚�Z�f�������*��i�h3��/D��� �،�"$�8�2Sހ��qI8T���� �pP��)ۍ��P�l��VֻIvoⒼ��
wbC!{�'
��5.���y�~��X&(a��~h<B2R*b���TK�L�f��b���lC@�@��	y����� A]�b������O��x�w���Wi�n-d��%��K�]>�V@����k��r=��z{D0PgUuÃ�L~���M�D�4��⦠ݒ��yɌ�p�{M1���=����\ƱF!�i������%G�z��4�eO9���ґ2�b�%��Ə� !V>-���%�yD+dyײ���j�9�8?��4�Fu��KCo�Z�'��F�;���~�����Sī��q��+�(1�#��gO�����8-�Ɨ����b/��س�l��F����i��}LIPc��4�U��a;����V�3G
o_�R�������j��܊��f�6��Wż��oؤ�2*��-�R�,�n�L�85k8E �va:�3�HɌ �@�����6�˫�
Q�V��3�Jl�@���ݨ�v��/2�~�\��\����(�^�S9s������7,z��4X��E@&�+���Yf�,�mи_�q��R�E
�K��b�[����>��c��A�
	t�-�v���⡄.^eRt�����P���*���S��vӛμ3�-�~yy�iD��/Wǯ�-~�@3�b�M�L2\�U������SNq\M�5��}���e�uorѣ��R�1(�٭�ۡ�bv�eUB)��$>9���E�$Q ����
��_y/I7lD̏����~][���ؕ!��W܏���J�):<�ר�aq@��T�K�Y��cx�e���+��6ݕ���EQ^ɤcL�B���b]'4�^
���אֵ��;_�F���ܯf��B<��_U��`�
����Ȧ}Gd�)�a-��Nڦs��<^�_�&��4�M���.�+W*��;L��V��������*��ϾK�畤�@R�����}#�N̎��\�H�7�?����0B�b�7�����ؠ���R��N��k�L$�M��k�L�|KB[�9/�?���E8�n(�J�'J!T{�&3nP�ᭇ�`b|̨�� �f�-�/�wa�<A�۪N��?:0˔i�Wꆊ�G�٫�,S�j��eId�-#���B��VJ�����֗j�!��yD���Z$p;�!�S&���|C�S��i�v߬pش��3"���@���ucN�>l,�Sڢ���3�.���St�I�b�z<3�1�b)%�rN��f��_���6}������C����o��mA��~g�=�"X9Xf�i����a� c�߮�TF����������a�����s��2vi���ԏG�[� �*���z%�'�_�� ���r�8��e�O���b͒L{h��4�x�����d>���_����Y���1��].�AZ���@�܁Sf43~�����M�;;K��[<����Z`%��%���˕�s�����ȥ��m:�`���n�BF�{m4ճ��㤼e��:GU2�\��QnB�E)y��A�ǿ�3%�tz�m�{\n��6*�Ǐ0�gD�Rs�6�L�]�N�o�G���x���Ęi��s��_Y�"���N��U���獟|^gv�$9���(U6�|+303A\سa���"fܘ������bE� �'�%a��
�4K~A��ܭ�X?�����#eõ�}G�菬�\,�� ���;=�u�Q�^[)п�+�Lbh��U���&����o�b�4��o��.�:י�}�6ڷ��j��W�(��<�=�86<������MFw�ƨ�,�z��ٚ�tdy�J����l1֘;�'�cc�t�On�!��7W����,y�MK,&� v������w7j�>1��2m>2����pdT2�ӊ��̔��3���)V5sxjo�L��G��3V��F�&���bz����CQ����m��Y`kw;?�н|�O�C!W��Ki�Ox^*�UWs�c���䪂l��;�6a����=��5�t��"D��9���Oo�i�7N8aj�ɗH5���\"'X�k��CPW<�t�A��H�: R���C' �����Hf&��jY� )�0���خ����N�䣛�����R�-��~�--��~�)����aJ�>hT'7�Í��D��nRb=�	��_�>�IZhX|t_�B�Şz�qR��u�w+�og��?��yb���ZK��E��<
��Du�߷/��,�a�+2��� R쁻�|�H���M4��[.sܝ�H��
#��v���\�ŗ\��H��H���,#E�ǉ,����_�G�f��V��V�	��o�3h��L�ҧ/|Nmzw�a.b� LY���/ny�Ze����k���m`˥�(!� �kZr�g�����{�5�f���a4�乌��-��1��M�7�c�5m�K�%mʒ}�b;�0�Il���3S���u	?Z0i�ӕ��I�EP4ө��I����p�����M��t��Q�g<����h��d,	w-��4��'���=��M.������U�t�3�ndt��Nq�����������0d��8��T�l���b�    U�|>��� k*2R�8�\qΓ�f
mksZMQ�U�Y�q٢�����V�3��ȸ<(.���ۉ7'"����|i;R�S�]��a�2��On%����|Е��/�����\	�!8r��7`,
Ɯ��s�f콾K�9�԰y�X�<� ;�a�����k�o�eiίֶ�ٰ
ײj�<�tX��ҤZc��|��P0(���UnZ��Ǜ�*�_k"�aXrJk���p���X.�篖��C�$����%5}�;�@�����]��,uһ���Q�ٻh-�S��Z�����%�Ň�]2��>������w�U���Jh��ԓ&zC��r�6��ݤ3ƅ�w��c#c�/�Ձ�@tW;4i��'�N8n�޿/bjl�s��#�~
�o�����Oꧾ7W=��QTn�0y{�*@�{�|����r��{Cx��r�������G���5X��z��ac�&zcFp������
�|�_O#�R2K�~��:�oL��ρ���Z39�uN��bOH��<_"��{�B��͖l�����B�d�hX ��$i���Eh��ĎN�s�hV�-39�>�Ǹ��d�mߕ�?��s�
���� ٿ�&���燋���,��8��6#�4�Uˎ��߳�i۷42��u���,q�_�,}�u Nwv1}�Q��Pb~]jv401	;�2�G�|��[R�����!V:g��ҏm�`�xb���A�W�n�خ-��x�'�(I���!�(MN����I�TA7#���9��p:�u�94BU��k�.1-�)�����e5�=��H�'�C+��@��[C�"����P�ʹ��Ⓡ����S��2Cb�M��.i��1�!�~�^��K���w�8�nl�l>"�����M��\QY��*��iZ��
�d�a\؎`�nQj�s��RD+��߅�p�[�m^}$�f}�I��G �(�1o,���V�d�8n�qB�K�'T@GJآc�^��s8l5�II�^9�	����2�^e��jI�`�N�
	
�1�Kh�\��E���}�����r)ǽ@�q�
A1gΟ�feG� I��H���ӽZ&[$c<Zޚ�W�&&E�җ$�(���'�5�|�=ӆ7J�2����_�5VAt49��jt��al�,_c�.����1I���.�����_(=��(` ~3�=r�9��S�ₒ�弐;�|й~�?fv��OK�Zw�Dmn3��M��J�$�P��K��$eފ������t�I��~��ȏG9:��,[q�@
�¡�q�飦��uD�����_ڥ�ǵ�ru��ՀT���3��l]�v��+ǯ]B�/E��Z���R�Q�|F^�H��ƈ�� {оD��c�K'�:�g�~T�!"0��{%L�x'z2�wx��=�vb�o�!Yb+"=2�M~�P�f,N@R�Nvی�N����k#�_���I(��6��a�N̉�����4=/^����0w(R�a���݊~\�'$��H���Oӎ\:K���t��;|���?rm���K���e<��=o1o��LD�z5����5'æ�}5�_?M��8�vy�ۗqmxH�� ��p� O����������꧘�	��p�d�4PWr�,�Fo[��e��B�?�~�%���A*��{�ևڽ��<�X��������b��dIC���S���
 U��Ê�eO�������.o_�y�߀ϳ���R2-��!s�9#\֭d@9#%d͘�7F���(�_�H 7{ejMƿ��@M�:"a�ANI�R	���˱������� 6`��D4v�cD����yT�ɨ��2�|x9��β��Z��֦u�����l�H���Dm<��ͦ4�4Ig�*�����d�(ԣ�������!�#���(�,��;���K��q�ny۷x��7P�08��i����AHRH(���C-.�	�/���9�y!9 ��2����Ad�o��ྼTl�!1�lȗn�'v����/����Qqe\�ڂ_� 2�']q�@�C?D-�lPM�C�n��N�0������l���3�<H�İ�U�zq���JH<F����̀��#B���{ӻ�G�pƻlhM ��o&�k.�{�]Gh{�C&	R�x��d�������O}�+H�vLq���U}%�֛��z`J�H�W��"�K�z�5k��i&����.d��崞����>k>&I���I�4�����K�������!�{� ͅ]B�Q�xr��$�s����5)i��`���>	��Zꇱ�!�ߘet�~�U
lw���<@H1����{�i��Rja �֗\�\��X��.�H�/p�xB\;�Z�s�=�6[���V9�4ZP�M
b>G~��E���-�����h���������E�I|�&��8d~�r�t��
���2W��:>"��J��4��#��=0�zuP��4���e61�V#�ޅ�4a��I���FJG�=���c�f���:6�5��NL�W���0�k��K#Mu~����.{=໽��Mg
�b9�+��'/n�'�}����"��*����?�w�u˖�I��)����  ��4 0@v�?��ʚ ��Ɓ���)bKIRx]z �	8�t� {i2�E%� ��ۯ�����ʝg%@�ܩT���,�W�G���/�p&�`]����MQ����+�<~pѲ��23�����D��f��s���XL#�R�� 1-�Cxx����12�fƀz���B�F>��Ĩ�ཕw�D �[�}#xп~O�7������BsSG�#"��X�Pk�V'�O��2�⧾Q��H\���)H�/K �e<�M��t�xg���cTc�i���8s��e��iz�d&��Wm�ׄ�ֹ�A}w$�ÀZ��w6ł�.,���)")̹d�'��i�6m�i�	�̕}	�q{9�K�;D*�^���:�,�Y/���/�|��ͬ�vb�5����u�G���˅o�ʴ;���%�O~%�t�q�;G_g:<"C�mi7jG ��,�߈"��=+8�,�*�-];b��U�}���֬�G�C o��;�jω�Ƨ��iS�skb>v�_pM�LJi��q��ltJ M0�b(�m���I������&�ER\�H}I1p���ݧ7�2�-����p?%��W����c^fCUU�}y���5ޚ���!ɍ��)���A�i�2��c�,`0kII�?�F}�{�w�s<�pR�W�~}���}����P�rp��c�!�XI�R�դ����� O*���|3!&�+��q{[���#�Y���g�L%4G�B�9U.���:h��V�Uy��l��=
z�F���u�;zԧa!p���_ǳs��'���:��w*�	3�{�8v�F�h�7��'����ع������nhXi��~�aB����WC������x������7 �n�=��Y'��y�_ދ�O�p)��6@	��e�[YNU��ZِN�ō1�+T	H7�TS}hl�m������Uz���a�L������VI^7���Mtύ����:���l��ٷ����������̬��H��+�;8!��}g��f	V�sϠj�u`h�C ��E���KT0L�H�{Q(^�䆠?�:�<y��tW�Z?�\��MZ�d.^(�Vc�M@�Z�O<�8��1U�߳bi�rvI�,��ڼ�v/���m9'͘�24c��cPӤ�o�0�^E���<�>�����A3�T�$d��i�:
��g�O{�m7�3	�]s5Y��C���$��*ez��$���-p���~���o4�$V�HR����9CMv9������d^���n�#�H����W�sT��b��h,�?���4Dܨ^W�;LE�sl�o"�҇�^�J`F7]�� W
�J'�m�П��6t_�^�&rY�\f��3���(^RK�����`xʗ����G��&28���VG�称(�^���O����F���e�Sd����&_r����Q�zGꢿ�>��9Q    K!�C����Y�B�jcBr�d��/7��3
�3z��l�W���m�5c_���f��Al
gA� c���1��Q�	�����W<���X:���"�@��,wi9AX��y��j�����7�ߥZ/v�¿�,$��L��?����p�KF�@ x�HI�} �u�
M´�~����\�^��Ó=|%�?��$�����������H�h�p������}�r�	�a�9�7�����}�,��?�	�X�Kǹ����(�M]��n��6�̱_窞O�@`�-�
_>����I0������/�pQą�s�j�]�Lg8�JW�]�m\3��kPj�iK��S�ڎ��0Z�,�6~�d��*|5`>]F�t�<�8�!qCv���f��v�EZ�j�}���Ep&�ˣH�Dg��*v�HԱN�WV����"�8�l8��'?�X$�hڱ�Ի�YC�A<����"��\���(�	4�o�`���ķ:���P���U�5�]h�S|�R���%��ֱ4-L����ޛ5��#����y��f4��OLf0�/'l0�3�����k���U�NUDG?;Xډ����Je*�)�,]c�7d2Yӑeu�r�&f�i(�$ZK�fi����]�!6"��^�`\�r䛪I�gO\9���΂�\�[>����$ص��0yT��rR�ejȒ,�\��蒅9��U��ȓ�uxQ��+�	��p|��fẢ�yH(Ҵ�~Vk6���	�v��7���t5�S/f���?;�ax����A`��*rRM(����i&��Vt��՘jx+$&:�^oi"goMI���)9P)[|���$��+����q�ǧ��CB8�{A���n��NX���'����L��=1�׽��"�t�\d�Brs��1��QdS+��E6��]�U(EP��b�6�uf�,�6�&�Fk�I� =Ye�ׁ>�ю�< ��Ƒ�ՠG,,"ނd
��3�}N�y�/o�&#�G8%8O�f�6k�p��H0l�)�h�� ��=�W�s*�\ݝg�zZ�#Z��𡅯i3��e��B~	w�Y����ӥ#Z�M<��r�b�t��Rq��҉��nVr�ʾ.w]3"�#?��(o�^{�g�~�'d�ĀU���w.��5/M�oIQ�I�EO�f���4��/��\��G���/�{֯2�h�WĪ���o��;Ӽ���f�4l�K�־�[:�K�W�\6��zD>�.��E6>���OXN��`�UqmҸg��Q��!y+}�]�E�������Y K���8�%�_ ��8�� m{���J���|=3{���cKR�}��)�x���)좤x��g>�.�����K�K����>���,�x/��|3DN�̳~�#}�P���4��O���� 湓{09lOÑ�ױ�ɒD��(PZ�dk�QT9��>=V����ߚj��H��ȗY�&�d=�W�d����h\��J�[��0s�՝���\8�,� ����`"�y��*�M��M��E,p�Λ�����݌��]�������9�	�3}{]�6ڱ����"~�`���0���Ѝxp� ?	I.�H�-�<�|-x��߹)j�1p�]T�=�R�$��wJ֔m��Buz\F�=�4����ؗ"/gL ���lTإJ���M����Qa�xF��*���������u�~D�\���4E�������9����	/��ڶ����k��J��%[��!�����4dq����9����,�j(	5�F���H�)�d�<Q)~_���^v1���HуS�	�E���|���Y.�}��U�[ٜ$�,t������z��ﻟ4A�x�kAc$��!�ǡ�Ggbu>p��=f�v���up`�����!�R��tٔ� �_Sfh��mPy�b��I��k%� Z�B�>N9�J��4 I�֑�D�H�fF��jVD.��I�jA��54��h��G�y���}�`g���y�q}�����K�����6�Q~N��^�0n�+\ 
��?�[#��Uc�A���܎��vn	)��E�*U��Vܞ��p�t1�;6:g����/�S��-^~��Ī+�b��*|}�t�`6�v��Nq��Y�xw�m]A��_���y��OY�ȕ=���\�T���B�	�<4�� �S����e��"Ěl��q4֦��|�?�[�����x�k�}���QP�!ج/6�1De^����o���
֖S��;�u��b�� ��y��<�� �@c;�C	jg��P��5����P���J�ę��͖a�ާz��ӛM�;lU�8�:���%�'�q�K�>����m���T�l��!@�wc��.7���įś��F��L�,�����U=��d�)$sHyn�d�뫔�.���!���᜾�E�>	L�;J��y[�p�x61<f+�5��{��j�yyuu�%�L:.��;2�q�;�,��R�L0q�ժ��I���#��o�k��G�<�vFŝ-�'m�[�n7���p�&�%��p���z�2��-\�t�s�n�ɱZD�M�3��.1��Q�A�d���<t�7)&oQ��*�|!$���;��W�"Nt[얩�������j#o1���|u��>r��f
�� ̅�n�aA`G>n�t��H^���-%L�k.�7}�F)%켜i�-����M�:sX�ޘ�������S�o�a�����x<������������vMӵ�}�������ݾ��ȋ����x����x��u�-��O���ow�į�p�g��x�tS\[���O�8�������0���x���n��Mr~ϵA�@6��������>��῎Q�q��G���������	��W|A�����F�8#��U��7-�k�]�����.�eO[d?�#������:�ۯ!�5#��;�J<��e������+�?��!4m�U$[9��9�(4?�4�X[A�G�q�z?#.��IP����y�:����K��g�e�������O��'�0��*4���� �#���=`�S_e�R�����Ui���ȷ��G>���o|��?������#��~�o��Fd�/8=��� u۝��p"��f<�6���s^�5�@��^�0ͳ�L��JP���L�`֜��<+����C�w$PX�Л��^�+�a����K��>�?�����A�w.��?��ٛ�z���p��Ś��T�M5u����>m���,X?�ӦFy�~�G~��|����#��Ճ?����/�������>{ԗb�?0�2k��q�����,���
�s����B�Wy��?��~��?ag�a�Ͽ�3[2?��#0��/||�6>��\���d ,�?aпU+�Uu��ы�w��u�N�Ev@�C�t��F~�O�������}���pgL@����7{�Oc=M���O{1m�_��N~��w��K_���/��)����T�A�������~�ӀB���`��!�� |�B'������Q�E�Ӻ�[sظt􅅁��. �?C3s�5+�7���� ��nȯ��7f2��_��p�&���s�s�r9�_S�J��������Ѳ؃���R ^�<|0X�H�1�3]�6���5����L���v
�# ����R��]�Yr��G�2���@[-�Ę��.��6ǰ������#���[��j>���)LX�p�)���uU�������p���x�u]^�ۻ�59��Bi�lWEb{�� ��G�,�)/j�.��!�w�jpD�]��;�R��ZA��c�_>c��E��X�TՒxӻ�-9�sc*�.FW*O�HN���u�{�sW��Ye3����Bd�d�B��ZWω��l#�p0�$����u�gî�4��Ϗ�X�_1�NIC=S�B�6��Ay��&��-�C�X8Ŷ��6n7��n/�y1�%~�c�c��Ԃ�ީ3��֓�Y/	����pz��1�>����#]{<e1�S����ݧeLc����������}KAW�K��:B��FZ�Eo7c~\j/���EH9�v�Nh[�׊D���|{a�t)�ܯڭE!��M����,�    5HU�\9����61p
W�������������FLQx~ku�tH���G3��"
"b��V���!W8;.�ɯx^�#nq���� ��B���UU���ϫZU��'��܈�����$}K/h�������T~D���vC����=�u�'n�'I�OCb���q3�9��
wRS�Rð)�@9Q2���>C��[�b!P��P8q��z��b��x��;D��gEEx�ѹ�N��JTϾg�>0"��:x+B���+�i���WS�ٺ����X���>�S��#r��k"�}+,K�g�A��S}�P�/��݂{4'����_Jy���f���"�R[N2�RҨ�x;��K��:6��9[��92�8�N
ٲ�[+����o1���%����Ap:O$��zb�@|��C|���t��T庤:Q��(FY�Ga2��-lO� 2KJ�����J�!=��	^�B�D{!مdI�q�q.�:��ݐ���|מ ~����6�ﲟ^��ua��6�HV~i�}�c�z�d����<"�Q�O��V��3����Y��,CV6Ȅ��s��^Q�{��\�\$q=�9H�ޫ3s߽Q�ӝ
c�Qꆢ4:s�#D_m�vF���^��i>� ��v�*"�J��1���H?���'��,���*�s]�~d6�H׉��^��E�׃���W&K�- +�S�T;�ܰy�G�D)?u��FCp�:d�T��l�j��	R5��:$���6�x�<����=Q��1�Ry�=Cec�u�!��.��ZSe�rS��/B�㮙�:)��(�ȩ�3�Ԡ*I�0���"XN�L|�v�<��[�Q�q"�� 1��Hb���v�O͠k���:���J��8�y0�3��ւ%c`)��>_\�8�$_�����
�0�2e��+���3DX1�+��~�Q҂�>5B��7iқ��(+��۠Sd:�&a f�j��4:uxy��Mۍ2b	:��N.O�T^7�R���r}Xd�F2�A��7�N4���������@#Y#|���^jӻ^�蝟�D'ԂC����6�F�'F�EȺ�')5��X.������1���7~�C�$I��0�zDq�Άp)z#�h��O�C��N�F�Q���M飄h=R^�^bܥލU�;rn^� J�k��'������~���2��/Mp�A�8���O�,���O�I�Ǵ��9Q�2�f�8�~�4��kE��=]"������c����4D̯	{������e	P%3���KX�(#2�ؔE�IU��.&�=y���XU���ːI<�Ǐө_��q���L�e{����I��Ut�M��/�[V\���Pi.���Zs�h��mA]�9�-����w�3�p��!W�˵�B��uK.A����뚚:#�&���ew�����XZ�����������W�����5sD�����-nO�p�V�Km�V��"�2��*�4]\����e�J�<�$�p��B�7�NJ ����,�|��q�P-)�xs�l����=a����o�M$���l��N<9��-խ�z�ZJ,�Ri�pֹ��IĈ�p��m����l�r�a1����{�?��b���b,��(�Fn�u��ȫ�jU��蝄?��&LCJ]��t#g6oC�j�ݖ)f�`��ݡS�:���B��I�H��H�fy�#�����a�����s��~1�z#zq�e/��K�k$�&\1bI��~�B��yt-�X�z�d�TX`fJ��jD҆Ly�ӵ?���.���ի絒Y��ŗ4�Y�$0��������<��ݚ���z�������"���am�bj��$hr>]!��D�^�t����Ù��� KkY��@�����rs9�R�J �L(��̯��_Z�����[��y%u[S������}N/�A3�>i�"X4�O�+�|!e�������O�+�.�%�E�1�-s�UB*�%:�{��^���w��:�8-G]#ЧdD�
��fz�z��]U�7}|�$gx#0e�/`�ST݀?���io�ر�5;鉯�"C��I��"��<�Su׻����g�x���&�����w�y���~îO���My�6h���=CB�@�&>���$-�0�`����wd�s/��j��L��-Ir�f�@�z��.��i'
�����t�^F��Irh��מN��b�˕k����^�9�����ӆR(�-DK@рW�*���'����8�U�L��Cl[�-0�ڈb8��qӅԒ�ƅ=g �}��~�B�?�;�G���<L1�D>0DIy���kub�h��	@�������׀4VIta&�ǉJ1��x��y��S���$�|�
�~ڭd�� ��@���PV�#�W�Ӛ�mO/�Ҁ4e�%��dP��	X�V���^y��A�v�1����k��8]E�CN[K�b�S�����1�}�<z�!`�aJ�i~��`��z*G������w%�����ޡh��u�����T���O�+������7�,.+�v��{r�,^�D��w�}l�d{{�ޑ�y�1�MS� db��&��]t+1ދIK��V����H�w�]��g.<�7u�h����-^�<frQ�(d>^;���{MbTK-�R�1<��ތOb8df��R�#�p��l��4�^k*��Z���'X�7���-��fވ��(0�j�,߼W_e�����Kk�� ��Y�㗇"涥r*˷ù�z��o�o�F��wܸ�ƽcq�K/�a�_n>)
��ة�Q����e��|�lzA�$M�l�b��B�v�ڰ�9=\+z�l���K/ֲD����f��W�_����l�]k��V�.�t$�|*ؔ���^>�a>���.3�d�"%s���vdI�Zug\���y��-����+�h�
��`Z�`t�n�Bsk�0�c��Y���v�Z���3��;�$�kJn#܄��O��x�򫾲|�UyA�ܱ�K�_�t?/�y���>@?B���AIn�~���c?��Ϟ��}����7ô~���˘~􋶗ｧ#����|�Kο
q�_��Ƨ}?�lv���̟Z���U��Џ���7T�<�_{`�����(�ѿ푱��G��K4K5ѱ�-�G*�����M��ߟs�����H{�~׃����=�\�X�?�����a���4N���_������C�wf|���}��CW���7Q��=S��(��o�9D$�2�Kiu��ǎ���j��^�_ =�V�?S�os~�g��~}�6�z���3n>2�3v�6���*{�H�:'M�;��Qk�	zn�*�	Ԕ�;Z~��m�.�Y_�l�3�6ƾV�H4:�W�*��O����.�O|�#b��}�z��G4��[l��>Es��m���>��=�ץ��������k�P�r�	�ׄ�Z�T�~�e�����#���#�։���HԌ��˿��*�����:�g�O���I��l�9��W6���s�_2BkHڬ�������<�=B��y�4W`�YE�XW��[��=���\��.Zr
����Ư�E���	��YW��v�����;�A�>����⩔`2a�3B?sM��	o�>�k�������~Du�Z���Ϭ�,2�(&�[�������>s��>k��h�yb�D_]�}?kd#�)$�:�C���3 ?{�)�>�'(���O�v������~(y�i�OM��!=_�_��+�Gs����!�١��O�L�����:&]LZ��Ey���M�g&�gd2՗�!ta�l/G�Mf�Ef��b�N�-��Ӌq�r�_]p��bia8sT8�x�����{�?�8x�g���'p���6�KMrZe�����o�f�V@o���q'���-�t�W�G����QA�C���{�����/O�����^��;��K��2��
-�������%WV�L��Ou��|�r���.����?�Nm,Ys#>`;�D����M�[�'l�}��Gt�����k"���f�<��� �ƛ��)��cK�q�b    @��Ÿ�u�
��v�u��4�q'qQ7�7��4�z���k���U�ˀ��x�M"��֪�����dwc���9�&��V���D> 0����~C����z���`O%�S���{\���T�����G	]�����[���H��~ %�W쎴���4\�A�g�Cc�ux'&R�GF݀�{Ë�S T���^��]9��!lx�F�t��C�������h1^�1d8+�w������s՛��|:ۏ�#>лD���yi2'Va|��5��˧f)e��C�v6
��I�JS��f@�}�}^H]68�Gz�b����r�����~����ۊw�.�rZy[��#>Y���cWծ�� 'p(�?���=t��#�A8�)� ����9.2��"�ݩ>Zj�%w���)G��ث�/\_��[|�o�|�x~}�XL�L7��[�Z<c�J��vs��Wj$W)�Yjx�C�Th
���-j�N�.��1?I,�z���ʗZL�n���jYۨ%1��˾5���)UA�������]
z�_׭�_��a"�e]�6_秕*�9�eT�5�U�����5auB5��c�K�ψl[j[&��j{�����Y|�*��Ou�;\��i.�n�^^qzR��$�ӶV�J1|n$�J+�%|�^P?AAXٶOt����B��Г��nx1�|f����Z����qJ�n�?�"�c�Kۄ�7C=?y<�G��7��8���};	���+NV��%�����=�ixk�l��:��]FUn˰HM�N�\tTq���b��l�<�8/�����٤��~*�-�ꉏ�x�3�7I<)c��Ǟ�� �>5��F^/��ܽ�@��:�89�C\<_�I{J7���h�Zv�^�}t��QU�ta�su���2w��#��t�Q	�!�n'_����֧z�Z�3�2'$zn�I�K�Y$�:��K�������%��h����O]į���<�A1;Z`����#J�÷C:���Ik�a�4�\F]I�eF��=��K�gI)E�%�6񆶠р�颣<XSx[{to�����{8�䗀���`'k��қ�"M|`��f�3��5��f�|ѝc�Ӫ��0�g�DM�]����#bG3c�	��+�;����'
�)7����MX�Hi�!oL�6�A�����1$��j��!d��3
��B�u�w_\�6YEj3"���»'L$���5��kc�:��{6i����z	�)��}�0>&�L�2k��Al@��q�|�9�9Δ�3]�kЫ.��,I&�C�ƙ[$� �뒼=&xO�:E4��03c\y�^7b���%�+PW|ǋz�7]*j���c�H xO����yi���]����HrJ�=iJlR3m���N墳f��[!GY�zTJ.�#�� �s��C�H�w�B��/�SܱcZ�&ty)�|���=��J�y���=p�>����D��T#�wvM�hJ�c踚"��E�)S�����L��e��W4���=3S+�����w����F�7P%ЦL��VE��n�{
".�۶��8��#N��l��@��*wк�.D{�;ҧ�8�O��ηךp��"`�e�W���&R�����;��.���?jc���H�i?<(-�^K�{�s5٘Bo�4o���=�(��D��4�7�txBL�tb��3�Q;1�������k}{�Pb�5��K1X2�H�dWtC��@�����*��WvR��A`�I�p�rn|/n	Yu���D��|z���$%�&�gi3�Q�ǖ�r;��QpؐC�gRJ��1�Jyf-�l��K�Pk�\�Ԟ.��_��;�Xha�ȰN��g�Q8Thφ�/ =�f3���SJ���]jw�,s�Bl��@����E��P�y���)+�.:&�O�(J�oL�2<1)A�b��t?%P����S��>����m?�M����6o�%0*xL��ߞ7r��a@ݠ�o���96�A�U�r��ߍ�ps��y۬�|0pW��B�S�g����]h	-�v��r�}H0+�i����5�:��<-X���� A���uK���}��jV�f��KKLص/4=5F�m@�+����e�h�B��F~}��V����,��ي˃�m��녣PȀ u�@駗Tof��@^�K������<\���<=v�^�z���f2���\an����m'g����s�x��҇wo�U9��,��fF˩�-h�|��eڋ����%������wj�^��{��!�h��vq_>񴍼#�(A��eHgf�ɴ�L�����E� %����{yb?�@hDJZhK�ӶbԀ�*��DL��`�bQ�o˴}���X?�� �+(R�t^���U�c��I�{W�|s�h��8feJ������Y�y3�$�7��V��
{
1����݁�&�IC|�aU���W;��C�N�S��`Q?�g��
��b�6q���5�!���5� �>�ۋ�`�o��x0�>��4%T���wqN�'p<Y��aK������U��JHy	s��d�G+_�Ш�-u%�?��A<]�K�0��ϳ����l�*�9�/�GC��$񃔃K�Pܠ�t����>��3u����.ȞB�05�;P$C���w��m�~��M��sC��5�8����/ɵ��
��b�Mtj(L�s������Itqd�[��<N��:Y�(;C92&��t^͛fDV��.s�۽×$���"N�j%��F�=��*<y�H��ú3Xݠ����@m���x'��B!@˟z���g�A'��F�]}��ρ!�Z�k�F�ʨ򧧤���M��㎵���>0�_Ë�˅k�2�q�#g�ַ�t�7h�ĄP���`d����멢 ��z��荎�wPи��@ݹV��:]̇�ɿC/�����:!�^}saײ�Z=ޔ�7��@PS3�:��,�v�`����W�z�R���I`}� ����3�'��'���P�Od����B��[�b,u���Oq$��a���A^��LW}�M]s�C�K͚���X@��ȍ�*��O�j���#�!$4y��"\�>B*�J@�'Sr���<������b6����$��U��:yWw�*��� �R*�j���o-כ�����Y�Mе�lv�l�g�9�O��om�ټ��,n��������r�[-�U^g7w����Sّ]3�o�����W�~�I�{:�'�ڻ��9�qO�I.-o�}`�H�p>�gwM=�t�s�>v�]�+?���U:���[�G_�#@���7�w����%-8�YAϻ�|�#�4ڜ������,4i`���f6^�8%�O�y���7)�N�a����X��{+�.�랠1��#V��'M޹Of����bJ��9%}��7 n�:�Yz2��S*Z�a�_��6:��(�E6+%?�;�kO֞���!���(�Ȝ<!�\u�<[���Բ�U�[����/���**{����. ҪD�Xh��Dkџ�a�4)�h{#Wݩ��No�.mJ �?����0�'P��/Z�6��	�ѯ-B"��9 ���6A~�������Qv������"��a�_��~�g��q{���o�k��wڤ��%�yN�t�u���N|����{��P̙@��@-�� y���L�;D�I�~?<����D��t��x�q��1����>`��!��XS4�w���/;�	
�w�|���?{�6e>��e��u�T=�>jB�w����f�i���M��v�(�����tE>������Q����,�e?}JD����s�qiW��t��g�~+��V���NU+��g.��yӫ#��)\?���љy��@#���O���*��h۲iN>�`T}x7�b �5vA��F�Q�>�uN���.����Q�A}w����`����쁏?�mGy�S��*�]LXE-��4N�����J��Rc5غ�eĂ<�˛��<�Ї}��|X�n�猑��+�a�3g7Ї��!�@���:<�8�5'�BI�?q��#��E��-�0�!��KI���N8�n�UK�P��P�tP������4|	��    ����~H|�F�GlK����Б���f-@��8(�(��o���qt��'��bL�G���=��\��VG��g�f<&���5��O<5��o�y�?tG�~ɾ�*�Y����S��St���Gj��L�@�,�Es=Z@>��.����i�?b�@���X��A� }���W_�9�/r��_}���G�Q t���`n���z�9ż-�-9~ه�B���`c��{Gl�����=�� `R��2��@_�k�_�;d�N�#@�xܟm9B}Ą���Ͽ�����_��?�� ����s,��sn�*������D�~í����Z� q�o��]���=ĵ�5�>k��J�~��U��J�����2�����~���>1��㹉k��W*}��w�v�S�H����:G��w*}��wj~��w��Ox5���r_����x�~��W�H��wy�?��(�N���п>��J��4�N���J�����Jߩ�����7�O�?�f<��r��^��NG�]�^�p�σ<~7nD9�.��q��A`gqȗ�cm9֓c8֍C���c�]E5�
<�
S��L��h�ƛ��[�� �½oL���k�)��ZA�%�S�[��\ρ�xN�sB�3_6a�L������ݭ2V�뫹��N��!�U[��}����G[}ۀ��qa������{(@����#��u�kQ�h\�q��^M7bu�`��"�^��	��%P��Dꤵ�����q�$n��o�� ��W��a��5���u��?�~l�D�N��g����-�aS�V���R��_֠��� �^`,�u)�K?�	��g}��#~��7]�z���ֻ�ЅZ�k}�y����~��ؾm�ÞkY�UV+;�ٳ+�W�}�jp��ȭy H�ו^���?_�º1����\��=~/��`�#0���v��T]�j[0_6�짟�)��?��������}���������������}��wst�����X���kӧ�_ <_C�ո al�d�?��s�־J�P��刨0�i���u-"�I���-�:�I� �1mq<M��^�-W�SDͿ���\�~���9�[���e������(�3���9�:b \��b��|�?�[3�3�>0�u>���k���?`����Ur�����L��� ����#;R�f����|��};�ϡ���:������������׎�1�׉�u���Xzm����0��>��Ӽ6�b��^:Kw�V1���2Rd#�e`���F�6f��C��4�=�uv������-𩀏��7����������ٖ�:x��D��Y?�Cv�����#i�������R�qH�n��p� ��S�����?�ٟ�ϡ;z���/����5n��u����Ϯ�e����!�_{��?���ZN�_%ު��$`F~�#|�������edӓ$j��������������m���@?i�t�z �}Ҥ�L�#�����I��l)��@�mѱ7`3�1��}	̅����`��O���.�����F���v��BeL����e�id��x����^���9o��������������bS��>��pnm�!�m�O)�"�������4���Bw�y>5�1�;��0W��hg��Z�AΔ�X��l��ҏC%J!�\���]���}3�ɾiE���%i��לSh��Z��������g&���hKe�ж��~���1��vA��5�}����\�A�L�1���V:���e:���^1d.t��-�t�r.�t���)+tm��)�t˳�����6��I�v��G1��~�,u|n�k��s:�q4Lr~�54&q�\����r�M_:n]�>�`pƙ�C�c3gX��Kb�..�ʈ8_�v��r~���T7��ʙn���U�=����c��Z�~�$�u5���W*��y	�����vJ:Θ),tb1oU��Td�J�-)������ؘ����"�?j���?XN����Y���鳷N���f�T:-����������|7��M���{w���Y�3�f�Mw��WȾ��j);�
��Ov�לz���~	�ܜ�#��|��f�s�T]�=�U�L��\Z�s�B\{��e�v�q�{�m�+�%�f!Pn��KN����I�9�3���s$i\Z���4W��E�lV��k�IY��Y�z��
Q��ͬR�W�17����*G$ȑV��&�W��I�9[�peS׶��U��r��A?H_�л3~rM`gB��}ۊ�kG�c��׹D+c��<���JI����ad=o�$��,���������-�M���{I��xP5y�M���.��x������
�o�w�M��A#1U���bE��p���;����+^��=���eCI5s��S�͋�
)ŋ�&�R�>��m�&�d�*.�aY�+At%n�&�K�ڜX6�Zg�N��z�L~�@�Ƣ<����W��ydj�8;�n"�u�ҽ�a���p��f;�V���}%������jh��Rhw����ܤ�`Uq�){��r����YU:�=m������<Y
��PI#��,=�%�%�v)�jҭd)��Fݚ]q�ޭR��K���9h���j��8�/j��@�h[	ʤ�6�{�Q)�ȅ�K�q�Uw��coH�QZ��� �}�tt��ʹ3ؔ���2t��J�0b��9<7��q���x(tNO�$��T&0A�0ߛ~���6K�M�b��We�OÖhI=��<��T��z�Bǃ53��f�d$�-[�J\e���/�]�2�=܁?�ޅ߱}��[�n��kt�ܜH�0���O0I;�/{��0�m	~�7KR�޻���h�~����z0����-F����RbD�8&�؛�ɽone�����x!����|����� ���٥z��K�C�䇾����W��)�v��e|���[5��8�4����!����~ ����B�ǹAh��}M�m�
�,D�!�WL���2W�7�D��@���jHAg#,oqئc������X�18��Yp!�bL��$r<B�&�<��,%3�P�n&x�~�+Lj�
P����ؚn�O(4��SA�Bf0�1
~V2��(Cv�Oh��`&�Z��P�u�b���h�ȆTv!`�#vK���B��#�mAʤ�Li��uY�@4���?���+���S��`�H:�Y�R�V��C׆��h�����)��G��a���{�x�?��5ۢj�4�[��Gi]A$����L$�b������mb��ՂS�jہ���V�K�G�~�g�QIٟK��e5pwt�]7���#�Ӱ�5����&~�1prp��q�K1��TI�F�W��"Ӝ�̢��t�⊳��K%�t�$?f�;yK�-n1/ɔ-��g��*��z�~(�._ u��g���|/h��mSC5�����{��tc��$jY���i����7N��Y���S��UP���,*c��x��۳�|aR�N�u͙k\ޏ��X�6S���s����:�NG���~Ru�N�QC]�`�iSc�H�!Z�ȿ8�>�o�>�&��n
7z7�P.~�F�^w��Ʃ>^�& u��.̚��8-Do�{,W��Q�mC���l`@oQ��e�Kb�;�ū�_�Ս��l����+����� m"~��Ad��D�^�O�����1R��?ߢrN˔���f��}�9���?���[J)a��� Ĥ�����&����^��y�����e��?����~��`_���Z���4�[M\٤�m���K����'}����?=���W����}��_M\����\7����kX�$�������W/�/�����{U$�m:-��%A�2W�hI�mFk4���_�U=ڕ;Y�E���.\�7�j�Ϝ�d�w�I��E�|Q��6����>K�MuП�U���B�n��w��?R�w�^�����>��"n%b#J/A���8��Ӓ,��&ȡ����M��>����V�#n�e�0�i1��˹���������T��W����������y�y�9ɚ���}g
�@;�K�D�Bԓ����[���    � ��`;���h���6d���!������fav�]�G}]���[+\�m�?�\��)���멐�hQ[靳���q�؝�3�L���uU�c���uy���DbH�+�_^P NAB������ʏ%��Q��������)a�5c��PiGwo�/Xˤ���
�(��JH��Bno��0ʫ�3�������p�uk�Bk\�l[ŀW����#�(b�ͱ�ҥt���{�\�J�_�_��Ҏʑe����U���K�lڡ7R}�Sȍ=Ld�f�ʹm-	?����8�3ɰ_���u�u�נ�v3o%�4H����B к"?$<~�'/p�x;�������6~��V�ԗ���)�2~��*	�����1�ml�\�2�Wy�_�)¯���߅���bg �ׁ�=5�n8xusq�g�O�kQ�3^���@Z�,:d��-�{3��^�3�����w�z	�w�I ��"��C�y�u��I��7��(Gr�r��G
&[��K���Nf��)Rʫh���O��v���_�x�H�˷�y�g�,E���"2��=�c����v&X�c��^��V' �b>W��h�9NW�\�0=bT\�u��X?�_�^!��8��i��>�vb�t,$���9�% xO�=~́{H�ro���j;�V��dL� ���`$����y���KU�a5.ݥ�O����_q�f�`��+��*�k���J1����|^V��܇ǔ)�m���N���(�eOx�_B���f����<c腼�`�
髢^j�����Z��G�>��,ó]kRn���$�󯪠�h��_��VU�?Ǻ=^��LLcv��Ip��*T�阴���a@��;DYC�KodQ��[�_ ��?? wÃ�gC�XN{�]���/�+��N����Ѳ�z��7����j���l��kI{:��Io.��;�|����o���;MRFՕc8�&1~$�J�pO�~c~|��y��7e��;�"9�s��4�ר�������+�hA)tC�qH�j��j>�,���+�ۙ�~H�*��J,��H�̷N�r�Q�2�g����/Q�9]h�+�D(Ţ��u�iDƵRB]��os �K `m?İF�)����������<�k˪^}������ǁ�dV{ͫ^�%���\��Z�6��>��r xh˄��7�2��Rv���:�����6���>�|t<$�ʠW����{�JJݿ��w��~ۯ��CG�|j�Cug�xʩ�����6 f�����ݺ�T%ܾ�%��.�P�±oζ?^�">��B�e�0-�k��,��*���e	?:M�Yلs�'j .���7��.!���`�����-b?��U��P�D��E�2Q��q�j�j��ͤ�9��G���`*v8D�#5�i�a��7}J����\�Y���(6J��~	���k��$ձd�%�hE�"c��� ���b�6�6D_�y$^���Im����,#yE5��g�@c���H5�W˥}�����+1���};^�ۆZ��e�Ԛg���m�¥I�_``�����E���X��4j�`jQVA/WW��ꗄ�TыLl�>/H��r�E�.ڼ��-���W�m�6S(���aT�/%yE����Wq)����Bc��$tNf��aE����ؠTn��.��j�Lvo]kU��E��Ryf�������?��+��&_�w-�U��$�yE��6�_���`T�th�X��d��T�J�a���
��ԯxgC�=��47OSU��	n����1�Z�-�=���8��b{�y���kە_�Z
C�i��kq�U;�Z�0Ĝ�6��5����Z�,&�K�M1^�;�=$N�z�^�[�0ǥ�X���D�XX�a�H���7���l��%#��]����B��T ��Ho�n� �����L��ư�O�}w�Z[NBg��|�g��X���g��}�ޡ�2[���WiJF��x�˃3��L��b�O���.�x}�U�l_w{���U�3t��6s�`d�5jy�Hj��߁wiFS�4	���+/�礲�|I�FĞ͉��w�����E�ew���vdPt#���0�]�������Y[�S:�;��������yl�.Hj��:�Av�2�,D��>-<�o��.��5{Ǩ�E{t�� g 7◩�nj�$xV)�D�P�㨶�L���k��h�������)�A<����bMe�.�~��w�y���Ϯ�iD���Ɛx�'�((&��B��~���^��L_��M�A��������~b��_8�K���y�|�6
�k04+�]��XJ��n��P����#�a�35�I�}Bv�as��Jgd4�@��G�w�8�5�-���0�p�c4�J|ĔZ�N����$�6q]�d�v�˿2eb� q�N]遴���YPz�4�d��
�H	�'�t��ƞ�V���F�UF	S���u(�q��-�wr[���y	w<��}��׎_��״�¤�1
yiLj�@�e��v钞!����GAEݟ���l�_5*��t	�غ��̾��,����Z�)A��\���V��D���(#a����e!�:I���k��3�h Y.E��}���������1Y㶌Jc�c
��?l?g��ks2>�kr^t������C q}��Zۅ1t�@�g9�W=�[2�ee7h	Ƶ5���������f�V��A����x7�tV��Y�6�4<s���ޭ-]��^�x���Z�v3��4�'m�Ego�Il�N�o���W��N��w}dW�آ@!��%��z�J��G��k��qŏo�:�K_�պ�H��zn9��ln�0'C��5z��+����N'��C�vg���ڻpY��v�\�j��c��m2w��e|I��<m(/��.'�֏�!c;N^���!��,��{�9�AsU'&3���`�r`J��t �pƉ�94�װ��?I�Z���Y�$%cs`��d��%�5���ZT��Q_ѩ�[b�؄4��^/�y�wꨣ��1@2;�%���K���<$21{��ԋad��'O�6��H~��>Ѯ&3��c:��Ma�M�C��I[�3fy���;��/�05�$�)�6��^��g-�>OC/N��LV�M�����W*�r��N=j�P�������l�٬���D���R*�|�ￏ�,�"s"Mp�5_��\X��k�� ��>Z~�[�n��{��u"B)i�y{����,y�PD��9�1',�Q�O���1�S(戚>IX�y90����؜�ͬ������g}f��3�`��_��
��&M���Ӡ��	�ള�	�Q��AE���pUC�7�Ϗ�I$CMI��=��~�P��
�i����oa��
۰��D�M�FOaux����9����1�I�F��5X�:���
Yς	ʌD��Y���u��v�l��g��
s]�8��Ǿ��D�f�
�`�������Ryx#z��"<�Ż�S����dc/���LZۻx���z��p�$��JXnh�:;NO�	2y������dǃ�V�c|�VqvG"��ֵ�m�����b?;��|��l%Hf���jR���R���i�y)�f1�=� .AY>}G�
+X�ȽʔC;$�x��*q�^�ԝC�YH�����P��^��a�w�Y�ı6��HV��P[���ƹ��Y��}����c%��E���&���6jS����.23���A����^��O"��<>,��D �R�����س��X��d*��(�����I�4a~1���x�ي��\��Ĥ���"[�!�R���V��.An{��)^J��.b����)L�:W,�ԭUY��;I��t���8ֽ��������z����s|5�E�=�=2�H���>i���t.��Q~��UŤfx0��%O^�q��w�!q��[��zw��b�|���<�~W�c>�7�q��*����|�aA�S�Tz>)�L3c���}ݧ��]�;��J2ǹ��Q��xs���_�s4i�����M�h�?�}͑�[�k�&��隦����O_�    p��&��tY��3mx���w	�
���|�HѕH�@w4�� e^_�]��&�̖)8��h���(%�x8C��7��z���(��g����1��&�����+r�~_����@�ʤE6$�����~��6���eo��6'����#uS�c���@ʗ�����S�#?n�.ؓD��J0}���|��mY×����݅�d{�I��!�0�6���4|Խ��A�����0��RP�1��ɍ����#[�	\+���0ӹZ�LBfW�"a��/[��������⊔�� $������](>k-h [i��x��:1&c�8�V�@�������W1J�.�h�}�d�΂���S��D�M�u�($~V�#0v;�OYܕ&��O{i�YS��<p��Wc��8��Xi�������Z�융�N����i��b�$ �]4 �5O? )Lɺ�������O�X?��n����D�@�ȿ����<[v\����L��?�{�H:�D�ym�*��m_��*Y�E��p��1I9v����teR��+� yQbo�KQ㻮�K#;��LP�ע�R]~�����?
�����6�:��ނ�>����k�#w] ��R'|7G���h:��+�om�-�k�nGخ륗q���[��q�]��}�̶4�~���'u �M>� ��4�$P�/�sٵ��:��șm�v�p#}��Ρ�MQV������v]Ud�H��כd ��m0�Nϥ��z�^����vˆJ"�v�����}}Xt�T������l}P6�&��t��ԸS|�`~�K�ݕ������z��� �=|%��[��N�hV�.�*�yfP���^>��I�L�����9��sgu�]�H(�FHϽW=I3��]��A�}{�yI4fW_L���n~nc��C���ׄ��.ˁ�=_A��ZL�Sd�w/kS
e��3R�S�9��'g~��f��da"����5���	�1�[���5b$Xsq>ɑ��a���	�zv��|��m���bo���a�=��c�_�OT��r톴\Y{��ǅޱ��G�YB������*Um����,�@��r��K�0'��2//���6CkY@xH�ߴI�����g��e|x5�ԛ�ywn�*�k�[��̲�,?|W�P̫ٛ@�9o�����c].H
�4^��!}��hy}��9������뒛�i2ԡ��Ĳ�z�z0�8�xB�W�:��I:2����ק|�����}�麄�|��Nxt7��hE�N�����a]/�t�n�mN��F o�H�Y/5�o���Uf��%"^�z�+���/��_��`���e��=�%��R�)��;��>]���q�8���W���ض��f��a������ܷE��IL�(��;;�ѩ8���j�]֧0�u��$��R�[���(4.�
X��N)�'Bqy�L�����H,�.(�Ӭ�鉘�C�DH70�f@]P]tl�k��oz�u��8J���m^�y�]�WU����U�2e���*�aP��$���CLd��:�����DI2�l'�=�C�?��'��/���B���2��b���gљ�h�=C���.�O�D��%�q�3%U%��l��Ƚćc�X��\�G��q��.��		��Gc@�-vU��a��˔MX�jt�/����~ 8�33������jL.�8N��;���@�}ydB+�� EFz$ԅ��A!e���
����Z�de��Ů;t�w��
���&\���7��$v�!S�|_h�uC���'�@r{�� V��e��N���Zu��ۏ=��=zKK�nK���3�y��>^�	�{�ho4EQ�d��rj�@P�3[\g�nm�z[��dY��F���aF4"�yS�9���� f�\9Z\��L�(S�������ʜY�ͮ:�n>P�ˌ�Yx��w0�ó��~��OtYm�q��u���u��ɼ}�4�2����Kxq7�Kǹ��nf6iAH�[͙[������X�x��)�x�������s���`Pm�Zx�5����pb����3�*é;�B�Ԙ��@%2�#ɉ䳟��;����W�d�q�cο� y�O�*^8�l,o��RF'\�99ƺ[�R���O�;K�{ݺg�?�rr*��9��}�/č��j��	vZ��RU�
�u�.���U���=�x���Ǻ��e��K��o�ns����7�XC�J��η�o�Ṁ_�"�<a[Ďb{v�jR]}G$���e��S�G����-���.ec���QY�Q�M�ڗ��N��,�&�p���j�Ǧ�`����I��E������T&O e�.�xӮ[(�b"x�k�ͳ�u��
`�ycu�����aR'���ek���҅�4�&��.Q�"�eҝ=fZ2� ��z7�~>Q�i7�S���lGW��Kݜ��B�4O�ϑM���ۃ��?��^��o�$�"�U���g{�z�����5x.��j�뷨�S;&G�Ӓj.��+����Ň��1�q��:�c������4e��J2�+8�w��	D�k}q��'2ݵ�����<�[U������߄A�pD�����������nEEݤ����e;���J����(��$eǬf�:����]i��Bg}-���w���ƪA�B��N�@@ʥZ�Q�dA���lհ��Ƃ��/Jw	B"^������LR�u�����̺ˢ�
׈q�eR(F"\�
˘4�oC���!�4í˸����ј5�m��D�Y�@Ăn֝a7uywO<����O��U�צq�Z���4�@�vnV��{3�0�$�&�ms��m�(�����;88�Wc�d��^T���c�� ���v� k�w�ȯV��x���Hl��|�{<:x���'�M��s�����cVY��<����ĩu���1$�kV�PUX�됖S&�v���_���u߅:!��D�*��	����9S�E_�%�������{$�1�P�������P�lM�C��?}��DېwDn�*T-S�˿l�mad@��X���I�3���H�������]u'�67K��5h�ʝ��y)��c�y��t�թt/+M�ސ������'�����D��5/�j�
�2l�7-s��a���@�E�eL�e2��i�����~��q�n�~�5���w��������:g�qL�ߟ^�!�n�دb���\4+:�P�E�9�A�SM���_+l��`�Yq�VV�/&�����/-ML�e@�z�v��2���A$�=  |Hc���T���N��ɦSu���i�!���eQ��`������Ꮰ}�`��^������Ac�~�S��j�yPD'���b0[V]sIKw�lS7Z��A��(�z��2^�2�?��m`v��6�Fl�g��rK#���U,��|�n�]�\�W_�篈N&z;�r��L�l�wi4Ȫ����2]EPhX�?'�:��hk����{�Uu!zWQGY�����C;�}R����"���ʝ&K����p�i��Z�Т骩%�P�$^��m��a��=����+��*��a��Q��f�|���y�+G!�q>x�Ì�a��,'�r�����������=��K����%�m{��a��d ��b�vf�Mf��Fw�E��RH�X�3��X�Ö��/W����Yn����>"Z�X�`�J�Y�^�J���t��&�}zا�`��U�� ~��={v�LނR;vҬŨ�ցsl��^��߉� ��Z��H]�h6Qq�.��� o���+�cҙ4�2%C�SF׍}7�o��7����nmө�w���sH8�.2�0�rE{z�y�r��>�w�Z~V��T�;LLp��:�50*{^��%�'$K�߀J��m�d=��q(ޥ=�b�>�W���8"o��iC0�
͐H}v/���_A:���S�mg%p��#2&VI��O̵ՠ�[�1
�਽�W�ہ��<�2r���SP�����[J�R���<�9O�x��UA$LXU��~US[�貸�    ~�HV�v��d.��v�b) ��o�j5�=���X��㻤c�&w]��s��Y�"�/�65��p]���At7��(_�	y��lP���k���^e� �x_w�սe��}�fy��b��C��%@� X��߄&���s���]5�'u,g�>Ձ��;'�N"�(�}��ny���.th`��(��׷�S�5��P�h���>\����#4�*�ʭ�bd���x�2b��~e��	Y7~{�Ă"߀c��z��d@QWn�y�-��M�$6=jKέ�3ׇy,�����`�B4�r�s�t �Ç0U'�0cW@�᫕�FY)��p�O-�l%ϳ_���7��ص>f���!�:1�c!�hEC�_�����=�s�vK��� a]��6�� �~��x0�x�7��d�0�ڶ<��tŇ!G7���'��c����ξ�xT��B�Jiͼh���SlF�e���!�Q�M��4}8�P�K�&U"D�C���m���q5���ݝ�}��|�H}~�>� �"O����HS>��gVH��3��}~i6�~N�=L䘇��l/ce�=/�����'����1ʝ؆�*I93���X=o���wš�#�+QU<���	h�Dx�_�dx�g����A����1������9��(o�Ɏ�~/n�=�RƸ-T�~�k�]o�hE��k�V�j,<���eD4ȡ��E�Pl�и�I�M�e��}��Wn�<���l)QJ���Հ ��8P��yNQ��];s�S�56�\+����{� �o�8��&Xz:
��	���D!ƖK�E��MP�K%Y�f�p���	���'����A�v3ZDgS�<g7醯9��4�L�^G�zv,/��2�j�_�����k�=H3��[�w)/"M��(P�A�J�
ɮc��\9�S�S��EBAڕ�hx �.��}�g�.�dv�e�Dt������+-��E�p��@]3-~0�8O��ʂ+I�0k�*�W�д���Z����a/:rR�u�&_�E��pH'9��xva%�ג����H})|j���hxݖ�gf������p���s�z�/�����Xn׹��kq�"h���*���5J
~x3_���j�z\�Y�%�g��mw���*r��;:^I�%~�䢬��_K߉�h�e,�l{%\80d����p��Z�E���mINan�*�F1����N����`�p;����[��?�xS$�)�^\LU�-pK3��`�Kn��1_u���`�����m)�a3(�D�b,<�<浬�L+�~Ƣ|����/��o����z:�u&���aV�@�./Z�fz��m�8��%�NJ�.M!L�xͥh��2#n҇oG��v�o�mqxy�.t�R����ܵ�^�.��QA���F���%��-���WDD��#F"S�p׵g9�v*�whY���rz��r:a�H�#����a�R�Ѩ�Q����_�m����z�=�P{*O�?�?���� �(��_�V\�Ej��d1���Q�qB%�������T� /�%Y����~<c5�Q?Ҹb�ت���O�,_��A���k@�c�%��r%v��*��b�,��E@|9����1ڊ�;-ۘ�"�������-�����vd�k��i��g��&>t���wY3�"�#��y��G�W6V�cU�Ā%>`j~5����8-��wn�b�"ۯ_8��n|��,dC���BM��%��@�h���y�Ս���`�#
�kL���L!�Y�+����P�6dp�~ߘa�Wa��"L�43����5Ȼ���4�*u��`�&�D7��~����,��]F[Y�S	c��߷�`�� ҁ�}
h��[V|ޡ�唸�Jc�)�����'[�$o�s�Y-����B���Z�0[����k����
49GW$ߠ1��X��e�@]�(���������� .P�.�� ��s��"�������i�2��Y�F�l�q����|̃1o�6�7H�n�Ǚ�Q�$u�|3֘@�N�����˞HO��Y)C��A�������|�$�#��f�d�ym���V[W�:�r��{ipl:�!�y}a�+�g�k�y�d�	��!S\�.o8��7�$5]��S���'%�K$���F��i��epYZ�Jl�R����eL�V���Aܬz}��p4Rcw�u�)M�4n��rucM�2�4�W��E�.���!nl^�9�D8��h#���6h�#b���~h�G��9ǉGuW�s����� m⟿�=��E����=�	���|J��m�CU_��߾^a���ɿ���v����,�VF�����*�Sؚ���,{��|���:��I� �,�/������]q��)u�D�������
��?�'D6�H���K�7-K�Ц�����+���z�E쥻|���_x�vV�K!u����G,��y-X��q���>+��RBW���^����̰���-P�l�x�}���\�o��1-,�N0j��ω���t_������t�h�ձ�$+S��-�#������l����$�����y�x�A8N
IA^o=��8Ю*G�x�Ρ,��/��c��n\S`��ɱ�3�P�%7�}����I/��
z0��uY|�*K�^�F%�����;Yȑ�� ,Ch�L��A�T�Lh9K�kd��{���q�8�p'��AI�R�n���J�j�5]�y8,M�v�B	��u��*EJ��D�u��
=L)�B��V��}���|��\�~4�{3�;���}�c�����f� �=��e��-��
I{J�%i�y��:yT�^�=��"�F��d
����Vs��wbq�'���3���̭'e�?l����(��]0g~���+c�<s���l�1�c,.����A�d[�x�e/��Q��3g��uق^8*��xF��� d�I���e�0�i���L���U�!����{� 2w�������|OsI���ј�!��/�lS�@�����;PL�՞1^@;�(%.��\UMMY���^k�P��LHty��K��<�"�(.�����j)u�J����g|�`��A+���H.�KUD�t=o3��$]���2;�027E�g?�L6cN�s���%������A#�ӈCW��L�� ,:i[����%T)���
��q�'q��Q2�E$�ukQ8K�r��e��{����qri_��`%��n)�6�j�0b߷6 �����VV�n���m)e�A����pC�� �qןl4F	~�%�(��*^۾��K���8� �G�Ɲ z��0��@`='ͨ7�4CR�/Zp�R�O����'�+�t ������{}���Χ]�����	f ��?�`U4w�oMiH�}e_%MT+�tצ�|��{9tpy�?@�R7�����0�4���_�4�+�<EB	@�G%��급��]Mi�"�R,ٽ	wM��<V�Wc��f�������Q�|Mk��[[��WNM)����`-R�� �ʙ�!��t�lv��r�?|��&�g���t�\���s��w	�*-��ľ�0��|��	�ʨ�+0v��"YO_��]وw�t#�����$Z{��ץH7�󺘞��w�'Bv����<��T�(: x��{��὇L����%����q��*�Ji?���aC�!Be���mjRC8�{9B3���	n�.�
*�Zve�]�'�Cf�\�1���Be��l+�N�@=_���yɤ}����Y�N��͚����G��MBƃ��*�fTc�aۼ@��7[^k�+G��
G,\�TD���e���;C�K&M������^^;(�V}�K���������$�Y2�2׊a l>u�����U��ǵ�=T��q������a�F����e
��Mct[��Tzt�����q���ܐ��~ ���&U�M�.�z�z��P��72�0K��ھ��{Q���߱�G8^?F��[���I�!��Y���������UJ"N�D{�M,���Y��5ݓ�f�,�ts;A���U���41AD5l9+W�(    �=Nk�A?,;��A�}�vH#J�Pm8�p��S��jn�J��e/OT,�_�*,泎�q�}ɽ�9,9š�y,_fA�q��&8T�K�~�:�xV�����+n ���f��V�p2_7�,����X�ғ9w/�~W���J����pߓِ9���U��oIya�C���  ����`��Dd-5{�¡��b��� ���v9��W>Ekq��׶�O��{e��åby���8�ۗZ�ŶƖ��B�P�����h5��E����m��}���lۍ�@U]�w���E����b�/�?W� ���ƀ�/4���&L^X�m������Y��ī�a����¡�j�_*^u�*j���+"B�Y�b���v�ņQ[�L�4���9��V�T�tv��t]5�ĺ���l�vY���hys���X�6TE��Ӗ�[DhHI��V�C�V�
!m��d6�E �j�_6�j�����j�Ԫ�R� ��N�p�?zQ�_�'Ͳ(�iޟ{�ek��-.����+��(f�:�T�j�΅藥���dO�>ţ>��^ޚp������v��1^Qh�7�z1�?�������6M���:%��M�ؖNk[>�k
٢�[g�[.���#�ܙ�<G���K�X�_��v
ɇߍ�� Z�������ӯ�z��0<��X�Яrr��a�b�[�D(���e]p�:6,�4��h5ڻ�'��|7�4J��z��i���\Vz��B�&¥���H�z�t,�� ��#�� PLt��v֜�%;3�L�x��m,�no�!p� ��'G�[�(�|�Qf]S�ee4H�1��Z{�p8Fʡ7(Xh�km�݁q`���"�F�7�]!�h��������
1���.�����E����Ư�7ї	!���ۄ .;F�Bܑ���u֫�5�e<��pJ�� #V�4+3@'���- �d�D�b��=x�ǡ[�&����mcMSu�ɻ;�p��[Y!��*p�J/Ma&Ɂ��.���P�TM�͚|�岳Al�{b��,,z|�%�ޓYǍ'��B�V��)R��k��`�_�&3�MC:"�c���)xK�5���]����l3};�Uv[�~�u/n!DgРK�`�릂������m��&"�c�0�X6�E��j��^Y��\}/���2m5e��1�a8��7�Q�Y�.s�<�Q1�RW�y�������,�¦�ް6̼>!�h6i՛��z�Ǳ�is\���<N��o�w��7��߫w�P�:�֚~�B�냗n@+Uhk���9�0B�d9����-�̾�D�?�~e��ޖ�,T�^X{��5�>�`�JC?�	�+�i�E���j��i�1�#~�`ai?��E�q�%d��:+Ϸ ��]�4p�#l`�r8��xyQt���ؿ�F�֖�|y���qq�c�=�Y=J%�=~C�SL'W���-�3GED�s�j����Y8��j�� ���ݯ�p�!��<�ܖ/�CRI���W�5:.��8"��<-�&+���Y�������d�n�����<�`�WU=p��Ѥ�:#'��пu��5=�n�Z�胗� P���\��i�#�o�� ����t�f�^�����¢<d޴^%6T ���^��tv�������X(�<�Q˴Ȍ�^Q���S���O�eߏ~}/!M15�!PRI��e�j�Op5��E��;؂TQp�|*��3㜡��L?�2��#b�z��aM8(� ������>­���	�9ޭ���
�����
�?�;HFhl��RY�E�7#����h
���x��V��"��)���RL�)˯�G�w��7F�h.-�cKg�Ui�;�!9�0�z�AX�$���}]���x�3�n�!�x��N�N*�-�Q��Em�PE�0�}�Y���۳��Yr�l��iK�i�q�O�u ���@q���xC�儩�`��r�xfxN+Лn�m�����BU��K��~|}g��`6U~_�+��K�+�����+k ��N��t?����3������I�3�
G�N��l�=˪�54E�˗�'m��^n��)��!#�͈��(7��u�&�C�#����s{�E��$���\��=�r�,��/�u	�N��G�6��7<���K�Ձ�|�|�/B17lߴv��^���'.?�N����'z����G�3+��t��;���LRH�;��@k�/�iћ�7���i2�fӛMk�
ks:`��wl;�.��-�D`�/�V�f/��p�Y�o6��&)a�7miO`
��p�v�Ľw`�aA�G&��7n;)�o�Jp�I��ۤC3S�����{B4�m���pp��+�+���}"rJ�����ىCK����_����B��C��L �����E�f׿޸_[�AYy��$$I������h*6���XT&�H�wc���$�&R�͓�xN"�e�&�]Z������ا��&��0d���ׅ:B�P`��w6�
c�J!���]Ф�᪥�0ҵ|[��aLs4&�-���Xj�	Mdm�Bk���m�P߭�
��8��1�����UW�R�8��՗7�!��-d^�O3��VЬ'[�En-D�������ᇝO�k��F��x�.���v�G;�<k�@
��� �02�H�'�W*��A��F���4�̰��M1v}��msmzWu]�Ӕ�6���Lң��}�V�!����b3|�0�a���	�JP���-���i�B�`	ޠ{"/�!20̉��^�Ⱦ�  H|�?5>���j/�*K�F+q����>|�����h����'�늃�cܯ���5��/_�
����'3����cR_ޞ��������`7e���r5����fgj� �����L?�4�{�����$!�U������>H�Zf��n��}�
���:t=�`�C�>-O���Y,�>���	Y<�r�UR��ÌG��4��� D�) vU�~�����ʌ%�Gi.��*��0�2�{� �ڶ})9D�~�0-����Hstng\�4�����&M��LT��@���~����֫�U��0S9~eEԫ���i� a�4���X;,r�+Z#����6wPo�>���d$n9�9p������ކ�=�U-wgiʌ�yF����Q� �~�����~	����{�W�H��KC���3����q;����R�2Q��qj/��F�ݘ��ȭP(�/䛸��Ō:}W)����f�]���7����⒴��(:�L���Ӧ*�����TA�d�^\�#�L����\����|���tI�̙��1T��+�:$�hN�������Ɩ,����7\�z6�{��KQ�*!�87��\�V��>lGM+��Dn*�w�A9�Ål(���K�d@|�x�׎\e�<�	�P��/���G]��+d;e�cx��a"9�usҜx�;�m>\y�E���	�(����5�ʲ	�GO'��׽�v�#��
^�����.z!�u���3������h��]�U���w��h7D$����ڊ1�}B�J��SE)�°������;]ۍ���B����ɝh���M��:J�V8pc�A2e��IQ�������Q�98���2]"%��m���c��2�M���V�@1��uʵ���~c�ҽW$��]���vpk�K������x����o��UL���A�����1��%�AZ̵WU����o]����e�`�"�����=��N�0��1�Ź0�;Am���G�WEJ��@��)S6AV2�7��z}�P�f0*c��qM�x0����΅&������2���,HP�O��ړE鮊J�D���j+-i3B+�5�P١sL��:�U���/&,��~��Pf��o�����]�?�W���(�`'o�_y��ac*���JY�īnNGz�I�uӾ��M���D&�	�Z��W���5��~�ô��-���rJ��NN�eر
�G�pE �a�
-�4�щA���������'���EP|�>�]���v?nh&CP8nX��    �a�98�* �Q�YE��Kf�88?~�Q�${bF@4�k�˔������B�����w��r���Ҹ�Ͻ��֎r���K�#u�[��G�x+VF7���4T�BD��젝��䍀����D􂽑?�g��jq��j�%�]�ΕkC\?��h��N��{4	�ڿ�l%���6Y�ݐ[�G��yX@`n�l���ݪ�����M��\''aK�W}��f>읮�10�4�Nž��~�6H���MUs�`�$g�w��o��ے
��f/K��f��:��\�����q�@M�3a��G@��*�Ș`{G^烈�-HXg�ă	�ܫo"�	����@6;<딚w��D5
i��F�&��:�b`�e9P߈TY�,|y���-���7��_!��2u�\�l��!�9�������э.���5ݒ��m*�l�k�I���C[ѡO?�mo2ZF{E5r�n�WX���/S7X���נ� _�pz�f�	�؝T��)�Vr����g�[(X�8y����[Y�ɘ_���m��@BFϻ����a��ă���4�)����M& �ѧ/׷V�v1�Ã���O܍��qԍ�nM���b}��`6���"�Ɉ��+Pe`Գ���������G9�ѥ$�G2��ܽ����X��c�xLZA��G�.������k|��C|����xg��^
f+�fi��W�΍�B ^��Z�'{�*C6sL��kM`���^5���a��>a5kx��X��coc�SH��߾k�9�Ѐ"���(k�g%x��қQ����`'��� ���BP�P;��1F:'�󚎬���u8�^�~ɿ)��jL4蟪�����7�����ϯ�p�ts�s���ѹ\�OP���J�b���E��#d�'�����ԍo$]K��(���0��H���𸍷U����K?>�&5�!��qu	o6�7�"_<\~�7��v�!G ���^�t���oBp�o	�p���_N'mr�*t���ҭ�o7�B�g�`�������=TՈ^[��u��n����Ug�T���\�֦a������Q%Y����;��I$�q���hI_T}��0�_���R�Peo�/���}���,w��u\�,����eP>(�`M����@h  %�Ԁ����[�#,r�o�_:Z'���y��1~Ew�3/&�wX��d��Jyos�����Ax��0�g�P��g�N�,�*=�)잠�Nɵ���J�ceY t�v�,C�`��>v=��ܿ���ص*�rUE\~�{�l2�¾c.Gȕ1˨�d��U����)bSe�r:���,&nM����3��sd����q�7Y��n9~&P-�&x�[GO��w��*�`.��EYb��(-G��O{�={%� �yϚ@�Bb���G��4�XG�V�� ���[�f�f"���?r�<��;�*v��j�D����7��dV���y/J�}L���˄;�ژ|�����aםw����X�K�3�{��)	?��6Y#��y��㵾���Ꙙ��7 �c�혐Un����"Y��w��_=*�$�9dLt5?��*�X��v�t��>9~��:H绅镳c����(��a|�J��P=,f�pD�u^�s}�؆.��X�.�����7��]])j�4�����z�.��]��K|%ʀ23h��n{�5(�0Y��AK�L(��(%���?��M�����术�Q'��a'x��cL(DRf��:V����܎��l�}��֭(̀�a؇s�{c�r�Sˎ�#�j�>���tTc)�jc]�t��8����=e������I���[��\b�m19�l6i��#!�	]�M��-�}�m@��o �n���Pd�k��-M��G1���T:u�n&�R��V0��׹�>�����;·Tӂ!��8x5�i#�|62��0�A�U[lo�C��N��� /m�5e�a��n��8T2���$u״3e�@ia
�GUTϛǅ���L���_q���Nͼ����(�E�����>#>Z��Bz��)8�,y,�M����1���5�Ǧ�	��RP�Q��GL~���c�O�ZI�.����g�A��9�_����V�2ͧ�W��f%9qY����-ɲy���x��ч���+��E�G�H^@�N�
Y�P �܀�2BT�v�wk���z�k@ĽԄ���u�L���9���u�.��k�h���ղB��S�hأ����p߈F�HIL*�3�a�#���} A��*��]/W�	jǸM���&=c�~qG�>%t%k-K�D��(���jh�I=S �:n4e��'ǽ�ڲd[:��̌+2AV������~��͠�'�(n�����Ĵ��r�g���0�g@p����<��U�0u�����Cn����D����垕5DOB�!��~�EDTn[�<w��k���*�Wf��+�*���Km��ڨ���ڇ"絩SIѬ�j��va���m~�I�!
�k]e}"�=�!E8ո�����.��Cg;���X֠���g�m�ҥD���?Y�AR5n� m���m�Dk_��#�B1sT�\X27� Ƭ�}��7_��"]�Q.�H��nxﮛ���|#e���t����8�G�!9��h�2{��^`�M{��T����[a�q�1�g����a��z���^-��j!��P��6��<���
T�{�'8�u��6Z�'(�DS�k��:6��$��ge�a}�a�,/��^�����t�װ��w��n�)�ن���A�v���o�<|�,&�M(X0�co^�k���&�FE�4�!������)�@��Gx�6T$���m��R	��`���*tX�~�����_�q��,�w�r�㇌��A�w�=�DwV�_f�bUS��N}q@}5�X��Az�98G
̯�Sz�Bi��^������T;B�ŕ�)'���8��	o}Ìi�1OI���&��<��DU�B�.�z�8}җO^Md>��G���1����G�ʛ�=�<my�q]�����G�14�-^:š�gE >8Ch;ިR�V�fކ�S��������	�����0}���s]X�c8�^��l=���S?_���́��������<B�w��r��ID���x)�4-���w�߿Ɩ�(Z}3uV��צU ��x3�1��*G��q����$��ux�����ńv���Ƀ,��E��0��!��Y|ٯ ~��&5e��H(+�gpB�*#o�	W���zu�w>\޿h<�?�H��<�4 S��,�4|@{�!�gI����7���t�4|qD9~Cd��5���y7&�6`����J��)���=���k�3�-�7
$"͛L\���Zs�Ç ?��M5�ّ*��u~�f�maNɄtۧw�$�k�*��jKcN�C�t���C�A��d$��{����v�H���5��	�K�����)CQ�{?�K�g&�t�a)L�;Ьxo)�(��q�z����#�/Q��b`�q˦a])f�")��t�,���(={H�i���^���}yh5RQ}�4� W����+{�T������?��"��87A�p�i��Wn-���}~�jق��7���麞ί���i�o���XI�X͉vM�qm�tHN���z���c]��igIN37�$�_��ϕjFZC����f=BWi�BsJ�b{T8��7`���O�q6!#d��̐�ֳ[#dJ4��7��_���!�OI�d,���.ք�y:$<���k��+0�����
�N�!�8�j��<�:�q���:��LZ����r6��o�dV����/o���/�b��S0�; aZ��`/��k~���0��>�l�ϞH�2�}޺[`?�K���B!���u�f�W��������v�����F20��|h�wv4d切޴Q.���~���؜Lv��$<�����?y������ha�|-rW�ōp\�dbc
�7���觪A���������)P��q���q�5Bڄ    �7S�z%=�L��+�p�����+�K���7��8�f��xbJ/�53̀̇Ԣs��~#�Y�ZCY�Af��)#S�L�*�r�EG�dƉ������+�4ͦ���|�qF��f@���u��I��Y�C�B���:)���利�(6.���ʟ����
�
y��*����PWe��H�ʴf��8�B�e#�v�'T���'��"e�R"/��/�����7_��@)��k��7WT��v^�B�Wp��v��^ik�����N^���'����&��5��]���[B� �4gb穀A����~��o��9�o6�����Ta@�U��S�i�Q�|�2��ղ�DVi
�?��E�����U�����e���e�����S){�Z~�p��%XX�{�a߮��6��C��;P~�����z��f�Z~���Vh�����qQ��z��mh6%;{�CP�4�[��U�e����=�Њ-�/�}�G|�R���y�W}>�9:~_�:����`&�ʝsc��YPK3J/�v��u���D����Y[��-[wMMmz"wˮ�[�ac�v%�U�R��Sq=�Z����;�f3J�c=|�!�;�t�7ƋU�Vq~blI�˹�s'�C�DJ��&d/',f��M �ˀZ��)P{|�2�������j�>�3��Џ��bZ�˂-�Q�wwU��t���'���i�(�����m�� �FP���+;P�iol�#��h�Ûx��f��D6����R�u�h{���㤤̂�t��x��GN6�?��{"��i��ɆE~�:^����<?lVHKt���<�(]���@�wW&��b�����/r�Зw�@������y&�؆����@1�y��3�0<�`U�{Tw4��
�lT0��|�����[o��j4��w,;Y�ٕ$��6T�C�]K���䬩�:{ge4w׷S�О����_�:Y�mB���z�/���o�<t0ܰ9�����������/G�\5k���2��ys�f9X�y��zY**q��m]�6����G_S0�η�瞈@�G��	)`fhס�	���_�tb�Y�Ѧ_��Y1x;��F�$�f�f�Ѯ��r^� g�Co����e�U�,��#�������P ���vU�;�Cc����
�p���YGaZ��ȃ1��id����[w�/�K7U����᫩������D'��He�Z[�w�������W��*Ů��[-�͈��`�J�*�N� W��N����+OU���Y�;?$��5{!.��`؝��%:M���t�����-/&n��;�%n�ޫ=�@t�m+�_�|�C�M�a��dZz������6<n/��k�dݏ�M4�]"����1d�X�zG�=�^���?�����-��b	~;�P8b $��۳s�X�4��Sc���YM �T������R��i����&+���h����nex]��\��y;���H��a�A�AGhv��G�5ѳa��G�,4D�$�W��:�=�mEdl"�eF��8Am{�SGb�-,��Ʒp�k?��p>6Y��c��T���`Lw{ڽ+�b�^��!/�:t�Ūq����S�4Y7{��&�����>�!�$V���ѳ�I�.��(��C���x
�e�d��~d��nj$�vwl v4y�尫�5���o��)��OT��E�A�D��ҍ���Ҩ�aRh��c?腚�"J9K�*��3�����?���~ğN������Ʉ�������](�N\~T�z���@J��$��q��3�ڬqpq�"�]��3�v�H�^��ט	VRq�����|~�<;/vlн��X$������T�
(ݿ���������xkO�_N\b�d��V-�ܚ�x�#�E
Q�`�^Z)[��~��?�5�d��U�e�?�{������a��3������<ƺ�o���3C+,�4��d{�WM��N����3+ǑoT��Y=���M�������Q`Ű�(�.i��GS���\H�̨�Q�ER��:ƾ��@7�z����Mz9�Q�p���%���["���e�wY/+-ۀ9A2=y!"�U�����|�U�]��C;<½�r�BD��R���t��4��	AP����j�q���l�-y�E�%�ٲ#dL1L�#�%�\�<6S� ��P=7���^��������WX�d��!z~�7���V�֭=�n|�?S�j_�^�m��r��i!m���t⵪�Kˬ>^�6l�Ah{�e�VU��d�c�":��P����	�D�QI��M��~YX���#~����<��/�w�A j0���Kh����;��d8C�
�CA~�|������&� {H�؜;�S�ˊk��9�Vq�p7���Q�a��)�J��>V�n�Pϝg9���6�>̪k��/�>Q3}^7����3��]����XE�c�"��
�v&�=h(�rD x���^n��bX��>�Ī|�[���޳��Ym����F�pPY-���9�������7�p���j�ȱv�~��'"�;���ؐ�.8��9a� ��~p:�����Z�nڵF��}$�_�i>e�U���o��Z�=��l�����A��gi�|BX�AS3n��! U/�������y��k	q�B#�zZ3�S턴��6�=eէ��\�%�ň=$��~L��'e�8NT�0�^9-澼�ܱ���AES�b�/�([�|a���o�7�� #��}��n�v�`��aH6P����i�F�/���^͏n�����Ǹ?R>詿*�o����Ű_��'��y�Ѵ��� ���&�di������Q�Y�������9�uSV(�=�n0Z4H��귦�a��s��g��>��|7������f_Z�KG�y�!%F�������7A�>�te��,O��{u�֠!���GV�U�����{Aqm��í���*��S��n��B�	��~�wp"�LnSs�T��2jL�?��^S�Y�����D���
�Rh��~�~ŇFqN�m�+���!ő������,h8C�}��!´��=r��~�ˑ}��{ M�m��n���u/��񦶛�#2�h�>Z^��5�q�������ԯg)�V�$�YlF0 Z�j����)F����?�=:-���Fo���_v<�I��+���6Vu�gv���y�q�2�84�a�FI�ӷe`)/�hNr!�����=lZ�(~���TW�q�ĉۈ�r�x�[
h~�1�y�3Kׅ(`2�B�*��`S�?-�<>n	��h�z�4[�y�;E�H��)�����vW��u���e�lbD��X�qM�GX���u��8PY�os7�gNJ����w�" Lj�`�{�� ����b�`������o*�B���L�s%����അA ��,mYm�WvW��Zƻ�n��Ncr�MY�O���^BZŔ���-��4�0����MfIF���O��n��9���W�V�e]��SUp2���.΄*C�ۉ�S&KNK�Y��*��ʚ�;��6^��|S�Џ�#@�d�!]��3~�i���/.u׳*����F�Lm��]ˈ�L��;�$��d3/|���-
d���K��U����pr"w鿼A��?`[��R��;O�c`w��� ��}[w�ǹ"��=�|���H2�M�}�h]U&Q��̦֦gf}�Q�A��萘����a�#�	�P;(~0�-�B���z;B�K���6V������|A�[�꛳!�nz� �^����f-.��D�-1[$���H"��XaM���2�?��dP>�FV�\�	C���i'4-�,g���Jr>�K=�і+��	�W�u�C����f�|��������=�I��I�\���9ϟyQ�]F53���v�N'$N���'K����j=���~��&�5�fʄ?�B�j�6ͪ��\�E�I@�>Ϲ���?Qa�B;�I�O�ٓ&s�M�?ۧ�z�	Y�h~Y	ɚ����1|���-Ñ2���3=�~y[8L�WC,'@���0���=���Da(�P�U���¢հ�&��	FL    ��~@B�w���O���_~���,(��Ӣ�:������!�L0�I�|�7�-ݢ�؀��Et�72�e���!h��[g�"i]�~]�~ �eŔ@�x��7�[O�FV͖�����߃C|h�NlA!��4���;�R�GS���L�H�"��:�q`�$y���@�~������h-O\��2��Yp�&����΄�^qi������#�z<��5.��X�	�]7�t?j��!Ymnz/e"�R&J�WV�!\��Yf�>yz^.�D�o%�!�]}%��p�my���3;+�8���h�r�5��s8�Y���FU�s����]5j˾�va�^C��̔^�"<�ག��:���F�UÈ�Ū1ڛ����6�n�咱����
+7�Xh@����h;�4Y�
/��1��f�L���T�-t�InV}HG�!#��6ޒ͍��毕�fV�n./��^�l��%ӿ��[����E�\Υȼu���6���gC�s-�Z@F����i�x�paC�P�	�k��-�۽YX"��a��B�Yjo�A�|X
B9�~F�X���[��?m8��=ĩKq���ڎg2����9���%I�i�|�AʾPF�Aaf��`n���+)��y���SlC����33n|��%�R����܃𰥬�B;�T�v���/~�)4M�X�h��Eo��s{<7SX���m�D
Z��lk�o�&zޛ�~^T��Ü�v��'4U�f��^��by=�~�Eo�g c��F�2�	M��I��	WяZ+m��U�9$��"|�c���`c[�-E��&����@����}�J�'L!�,s�Lur9��Q/^r��a.�����Uƾ��ƿ4�Z;X�[w�4�MoK	�����
�"�'I��ں�t�.X�#��e��<�Q�����
gq�zϓ��żF��+�R�:o����H�o�m����x�4�b�KǗ�?���ͼ���M�$v�Nv�9cL�?�w!���/}�y��ț�q�'���1_�ރ�i�}�. ��	��s`�밣ˁ?��ϫ4�ӏn���In�V	F�G�u��`
��g˙xa���+�b��VoQ/�9a���ஆ�r��`46�
�T��m�g`�j[$>�-#�6�7o�仈�iB�y�ɹ+��E<s/������"H�+6��LA_�ѭ�Y�\��/����#�H�B�=d���˳|GP�	��
-�ݶ���1��{�L��N{K/�����J���m�Hy.c���'�o<AN�vNý���`���X������ ��с�l'�t��������$S\����ֽ��Ɠ�Sg�w����?�>����E<
X����`���ݛѕ�Я�o��*��^Lrl3�L�t̡��j�Q�p$\IIK�C]z��j*�b�;@�|S��h��~�F���W���HH�+��~V���R���Bm.+���mo�1���"��y5����̓�\�����,��Tԡ�TD��4�^�$gb1�0�����Ѻ�Dr0䁌�E6�M���9���4O�V�:�.:s� q��]�����̆�vA�%�M�#L9�.�`��t\*��u�s�CĆЂ\��W�, 'h�󐗧.D�P�(���`@v^�ڐ�kEĥ��0)^a��-�䠇Ԉ1lR�O��_�k�	!k������˛��e<��$��}���ؐ^��+�����MBcc��`C��JU�6p�����`AtG�m����&3{��D�V��soC�W�`u���y�!�ah`��ws�� ��� 0uȑ�5ֺ�pG7}\-�G���ӛ��g��*�{/䘢��H�ur&��c	ڀ�������W:���� HOv�*�B�>�GW�=|톼�'uj�'ubR��u>���	\�P�FG�+�6᮲Y����,�:��t�|Ҿ�P�և����-'
ޠ�OE���~�*:۟5[%��F��̓�楸��h�y$�f<����
ݜ�9�יp�O���y:�I5#�����Ï2��)�e���̀g�="=@2����|�/oc��,y�]/��>��O^(6�B
�X�g	Ux�������@���=Sd*i��Ɵ��s��,�
l�_��W�l�� *���.(���]�g��ط�o�&�sP�0�@�ʣ$h��a���ٴز�ѱG$�u�G���ͽ�S	��۬C׵�f9���-�"�m��I�*����V�2����е#3 �Dp��#��u&�fSr�58"��do�nY� �
�˦L����%9~k�K~�>�7����z�k6/����M;��Ii��o�$���=�X��[z^wIա�N�^��Ϻ��ll!��z1��h�烤�J�P��8V%Ci��ްޮBN7�a�_h�G��:�����.���_^�'aw�Ք�A�4<B�GB?x�������*+���"����%���Vr!g�(��+��l/F���m3(m�'+[�T��$�	�5E��:�I9��֋��{b�;�q R�S;��V��!|��C�Nap�G0%w��x���3q׺������BB+����P�~T�|Κ,�(J��C-�au����5jCa���*;�I�#�qN��=�@+�АhF���<�z������w���v�����;E"��¿Y������j�b~z�m���b���Jh9tR�;�i^q�����87��R���;��cY�[��������l�7�?�/���'��]�CD*�HJ���������l$J�}���SS�sx5l��0_����L�D}��D��,o��\x
��	!WO�)��F=�������˯�{4�k�.��B��f�a��E�p���Omg�e,�~2@��i�|A��y?*�p2E~���U��Z�&��ZG-۠x��ȭ�`�kҪu��ܠp�5������<t1c<
�҉O���8eIo�8J�>���j��ĿVo�Q��#N�D�)����e(�����J�]+ve5ŋ��"�}�Fd�Gɻ\�z/���� t�/h�xJ&�ߤ�l�����rK��%Siq�K�W���S��֧:��&�¬̘�|WtJ�i�+�T֦I����.�m�����6�\�/ែ��e�w]�6�>~�V����|a�zC�����u�A	����//O���VN�~����v׿q��)��7�,vx���=Ǐ	��ߔ�&U�{f�y�K�����N� ��b�+��K���{�;��Cܹ��PM,�\����a�1	�Ge��th�Tp]o&)ks�o���PfX�\$8kd�!^�N�%�S��@IjA����U?�=#��SB9��[9�j�zqj.��5��^BW8��=W��V��:����DEwh��Q^��1�L�����U���x0&$2/��;����3
�B�C� ��C�����S{zA������=j�nj��T��;�2��WB������d5ܡ�"� ���;5Fl�r˫|�c;O���M>�2�-�/��\��*�ZUP����7ɮ�.�X
Yj&��:6l݊l=Rt�9A-(�Xo���H8]2�K!���$��3�t��'0$�;����?8CL�	_s�/����Oj<���������ͺ"_�Q-=�9�J ����]\0ރ�_2�؈�BX��[��L�b���EL5� w�K&�u��ly��M�w��U���@!�j��Y���+3nLX�c���Ԗ�L�W����wJ���i�=���Y��+���Q_s�04��t{[◠�O`M��oԃR��I�/��!�Ay���/���oW����&�R�-��0���V��}�k>	j��BB� �����𩗑�m�����HXH�����Q�h������8�_���$�O>� ڀ��-$�0Z� �h�+5N��[R�0,x�����aU��)�;W��p$,_��_V�h��%c9�qe~mÞ�jePS߮����-�>0��V��?�    �S���C��P�^�5���O ���:����Mhޖ���p�g�Lb ����m X�%�+�L_������=Q�rOȳkZ��Q���F���`,���bic�˛��̓���_�0t]�O>��Bw��F�@V�9oӒ+h���Y�H��=48�;,�p�~q��h�e�AR����|#�q2�$�d�.뒟d�!@v?�v���ؔH�u�c'7�V�Z���*�~�H�Z�h�6c���BWh�2���=n���r��dPQZi��f[b�g�5 WA�0���شs[#t�������w�i��i]w��p~�d"f��v)���e����)�}Y�}�_��2��R�$i�2���D��M��q�SR�ˢ+ԧW�Mq���-��i��� �~=g/����e��V���cPs�FZ�
����]�TM4�-��qK����D�aʈ����-��	w�p�Y��k�VR�#Ks,י�0�+FT�ץ+9n�����\?ev.pa�(�6�_8_�`__Z�D|�5F�8��Ҭ"7����W��4���U�%�?D��)C�g���NsۆxW����쎾^��p��=-���������Z�/�%���5�m��d��:�Nu���.��d��4���}Biy�� C�^d�6�@ ��B��t���_�3�:4c��qVY�6f�D�}����}��c
�����F3l����(''n9BÛy��pY��/D;�7[��F�P+�ճ�P��C{�=�&(����+�]3;q��T��))k(?ۗ���=�GL���a�-8-,/Xo�r��~�v�䌎���7�]V�����/pr��9�\���B����W��W��%׮������.z�P���7�#	MBi�gd�ǁ���.@+�����S�yaɻG�#�+���A�Z]D��.،.�+����A��r�U�<}�,vy����؟guN��vȇ.^�莊�����r�Al�H��.���ޏx��Ǳ���j6i"$X�fd����>�oqK{cn8>�8���v�����"�0��/mH�	oŉ���9��S�����/���]�v��%R~j3G�������`�R�]J�t�m4;6z���a�&�{{���:dW,r#e�Rv_1����s�f[�,sj��HS4+�L�Ckr%�[�yK	'z���mDTH��e;#u�9��Ƈ-oD�g!h@b��RN�B}��1�N���ōOi���*qU4�~���c}n9	�X���Qi�_S�MűbW�>��38�=�-5����T�'�!��Ο�iA�4�?,�����b��	Jy"�ì��>O �cͽ�DI�`K����d̥mP	&w_��vc���b��D�^�A���!�}��@�!�-�d��k}��V�Y�J�����)_=6����LP}�/F��A���{�����'^���Μ^��b! V"ZwJP9��Af��;@IMȆ��i�d�ށ�=�������-�,N����Pt%�̜����	5B���\t����tN��Y�|�����ݕ'j�F�����h�
w�5����U�pC'	�����[�25*�	ng�����~B�rVS��+��2��F���5q��w�n�^Ԓ0@�-D(ghF?`0�l�9���+�z��
����&���'+h���eSq�g�؜��Hrz�ߢ~@�3&:P�E�d�_N�%0=�*�)䠱��K�:T�D�b�ɨ ]t?S��&]��v����8Tpa�i��_̝�"*ӟ&�1������q�àz����h����·�FSJu`�*�_c�|�$&��}p�iբ����ŝԩ��r V����F�
 X����]o{g�[�|�;�˜��c`x�4T塿�Uo�"9G��8�O��7�	�B��Q�=� y����ptL~�O��j��<�d~�Y�D���l�Y�ܮ�&�#���3�R�0�Rr�r�7ה���̟��U[" Oj�����l�����̊k*x#��E#/"���>q]�A:�6P�|C�P�#�����H�Z�gC/�K�>|
M
���k�����,_8�	�S�͋WuJ2�%�~:T��w��sa,n/���Y�K�-�CGtp�P�9/�����z��qk%�;�m�G�y���/rm�$��yJ��B�Ե�|&4&���*A������z~Z������]��1hg��\yR�.G����X݀��}g����u����w	�6��;�c���|��p,�9�{x1,�wz^t�K���*0@�t�6�T�N�N9Ɩ�E�v���-S���6�V�r�ທ�m	����	�T��7O+�q�0<p� �=2�%����$�������]��	2�mU��U�=~~��7S�?��<yb��Y=��$7��"����<3s�1G�:y��
.d�fٶ?��*����c�Y�}��=^��A�*Ū���U�������������ͪY�F`.)�m6-O���g�P:���m�fy�	/����	�9��-f��Ą`w�k9�ɑE|%Y��mބ�b�N��lDas[�v`�A>���|Z�����\�ض��}\��NL���̕bɻ=�'�m
h��v���������{G
��u�eZ݌"�`6/��7Q�.�M��������Zve��t��wo��2
e�� )'p_ k��qg�%G��c��l��I���1mo����%��˘/�6Z����U36tԮ���+_.�=6�2�,2Ҵk`\����ԓ���^k�lP�ʱ�������^�����sx�Ni��a�ܛ���VU`���¸��y>�3�Y�7�)Z���wq��+�u�^Cԯ#NڇD'�������v�b9�����|y��Z�����sg=�kw4�vx�&o�6���yc]�i��FV�V������s���Ն�"�sNۺ�q�w�粙���A�߁2X��i��Z�6Q3�P͒mt��(��y�?�y���T��\����%�A@k=(��.j7��0�o� W�S��4��ͱnrV5	s��d���@��./��B]�ۏ�̍$�:O�/Q*RZtZ٢	��vV�0a��
o�$��V�dS륽M��k�?߲ JCbޝ�3��j��;�L_� ������/�#�\�4|��V��p�HK�~E�c�D�*}Ӿk�o&`mhӼ:�s9?:o�9T\'G`\��ښd22b>���$~D��hP�XqN}���n�S����8b@]���2�@�R�L��l�wb��76�+��g �c.�b�
����=K��Ò=y�Dkf�`f��H< �G�¿�b\�/��t�Q�{��3↵Y�Ǫ|~��i;��_���믻��5�B��d�"SE
��C{S�I��ˊ�H@��U��~�_�%K�>�7p�]�� 1�:�8$���e�pU���sN��m��� �!)�G�82�BUP~k>�"{���t�+�l��q�E�5ر�:�|s��%�a'�|�\��|�k=o�Ǌ�9�A�q���7�1H5�/Gc�G����25�|��'�of^��f�j�J"J"vYo�T�k��9/R=o ���(k��~�M��5��-��E�LZ!��έs���\�( ��ws�B��T{{�7�w�R�믝�j���&_;�v��0Z�Z�&h^��#��j�P5Â��%��,�3�����6��^��@��K:~u5��"�;��%�YYr��ʗ
Ed_Ҡ�c���6��V��XZQ�DVȨ|�rK��Tb4�����oi3�l6�$��6L&��_�V�����uha��ٶ]�˲���kɃ�K��o��Y��J!Ue@���ٮ;��/���JZp^����y��-�z��h��˹mMOE4̽%���٧�Y��
�!��5A�����4��'Y�	�C,�m�u����A4"u&�X����܀<���2���6mW�Xm��޲V·�!�ه��PM�5<��@	�
8��W�$^��4�h���sN��٧%��[f�	���֜��\������S�������R�l��̉���4�����P��$���h^7��    �u�c�	������n��g�].�"*�ֲ���n?�Gf���^7���d`����wZ���������
��_�q��F[�zs�'�e/kT� �:�D}����~�K�x����"T��^ �O�&a�ʚ~1vc��6Ӷ�0��U���h?�h_T\���"`�6K*��M~�7�I��v�J�k�n$����"_��[�<�yΗ����6Y-���g���tl����ڴd,�K0	�d�ş䖅��=����&��8������n����G��ǃKØf�a�u���x�3��
+?s߫^�u�Só��V���OJRE��;yQ�O�R�O���3��⺷���d\M+$��<�������.�D �3�h?{U�+B��$͂�+m��8�:&.��E�]��P� {�s��W�4���Ò�z����V`*��W/<ec�C���۟W���t5�E�3\�|,���@ѫα��W9.z�"�ht_N� z�-(T��0�;�ZB�H�+��w�����6�ҕ�w����N�+�$QX(n���_VC�F>T�Cҩ#�g	�8���W���i-���h��ar�d�3��	ZwDG�m&�y���K����܉Om��>J𪽁t�S�;��Ⳙp�/���9;Lҵ��Y~2�K�l+4���d�f��D�!�&Ρ��Smo���n�{�`�h�8!>�g�ҵ??�sb�%ȠKm1	�HM!�7���&����4F�;��,=\��İq\ņ������E����\�8��Y�$�{U0�X��3��XX'ٵ�mã��Q�eW��B��j+��P�鞘�(�$���X~���'�/;��}���Cn޻���A��,m�z��@�g`�����;��j�\�d�h�@l�_��S~�E!��¾x�A���mz�e1��e�W���fJ^��O��h��y��{ ��E�]F<�jy�Wǭ4�ä�cb'կ
T��%ƕ趆��'V�~n���$
��E���3��"RT35�]�b����� �I~ڕ�Df�AL���3�TYx��M�����Fx}�4%�G���4ӿ��#c��-�?~v�0�d��HK�{%���>s˝.��0F�_F����{����9��Z׭��4��ueLEj_��V[�1������F�ȼb]����}�K�Jvŗ����(־�u��lg�6I���q:{Yr�|��Q�Z�&!��9N^(��G�9���]I'����^���~�)c�U���M��5'�-�RV���X;_��
C��GF-�o6M+��T��G�N�Amv�.	���~vM�v�ı�ﰅs
��ߒ��߻��LHs�c��r���K���Y�� �Ї*2l4��)��үP?w��o�}�j|�R����[0���1������28�?ڢ1z�	>���/Q�_�] b�m��ݢ�*ʘ�@o3�0@�X(�i�^p�[����SA9(����(�Fl�	%��G���";�9�5��/�*�T�Vㅔf��'\�+�m��P����h�7�tZa[��ia|BlM���R�z�nȱl���G��\�n��7X�z�l�e���b�!�t  on��E�RR`Iwag�D/r������^j�$S�ў�9@d��h�|ozz���%oK�>p8QPV�$�`�m�DG3j����LN!@�VS�Q���b�+��>M��p���_�#�A(����AQ�%0�_��R�7=����5h���\�PN�Z!��6�;�R�p��.��5ty��tNM�e�Zx��_�Cpy{h(�
9� �}��ݥUՆNM�ړ���I
,�m����r�~���>�54~z�]͐�@�!��:U���&IV���d�H"�*O��S�ir�ؼS06�Ř`؆� 
�{l�B�i��v&|��98��8=��)�����S<Zue�mpV�N��>���⦣��_�:q���Ǭ��o�$�"{A߸�^-қo@AU_�0.2��e�,�P8������� �D<��#B�#��0��K�mk��cDT=���V?"�?�b<{�[l|��~5W{)�2����߷�$��.����e`5���3!���!]h,��V��03A�������<����ش��VS�3tN��څ	���6`��Xd���@1.����i�x�WW}��É�&Ň�\�@�Mym���D���$M�H�K�1��� ��꯰��}u����L9Vd��Ǉ���U�nw�򟭣��ʌ���U�D9�zЯ͉�� �������}�����=��2��i�"����N�O\��w�Bl���J^r.\B������s'��ș�4Ղ��O<EsNBaŨU8�O������_"������S�����_��C<	�ά�MC��2|�;t�������Q[�!=+q?�|@�֓��'�kк	�%^$c~O��3/m.�2�j(�_��Tw]u��쭳k� �1&�Z���o�S�������F�SZ�n_�p��X�_&��=/H���s� ��CZ�xM���k���ik�u߽�	�m����npAy�NB�y�f8�%�.��S�'��LP��Ҏ,�LJ�Q���sa`��ύL�������W!�U3&�ψ��.{��3�Ƅ!'e�8�%y�8���x-)�i�囥��i����>��բ�^\_��MC�c��x�O��62@+bX�%G���Ez�UojL�#�|;���u�Ѓ�C��f@��*7���C.Y˶��x�j�9�����4�OX�q��b�v���ty�'@��:�&�j �$��~>C���8Y�Eq�������b5!����]�� �Q`>&�*�YZRV�+��ԯ}��v�v���g��@�GwXy�m2Nh5�l�M�Ō�{��G!�ly����� j�Ac2�yY���ma;�r���Λ"�r{�[������V�Tlw����Y7F�Oeh�f$o�`({?�E�)03�:�!��e@�iz�6+�*隉N�i�|Ǚ�.w>�I�ei���V��;����<�|xz���P6�A	<��>�'=�B���y+�d�y�$TS����e���ݮd����K��0 �;�Q�T�]��O��G@�i����K|D�� W��=Ֆ6"��z	��1����%�q�c�J��Pҟ�Tʺ��?��v������y*���К[���:����O��z9�d�d~Y�z��f�q�%�p͇2"~�4���+� �Bb+)��\�u����m��Y^B��ZQ��7�	0���	�t|�/s��!k���K�F&}���=f�M�x�S�Φ'á�[�v�M�*Ia�S����$3t��� �a�W���V7���S��Ŧ9�&�W��5	b�����;]x���.E� �o�el�3�$s��=�u)G�[��a�C�XR�����1X?�سOy������-��D�N�Щ�A:�L*w���~>���Q-�wL�-
�����K��9;�yE�,u'��H߶�}3��2��{=���礉5��F����)}o5���\�W��4�$q�~��p\�`�:H�Лֈ���J��R���<�]|����@L��޲B�DJ�%���5�1nqw��m3��{���G��'DO��.q!m����{^�u���|�&�"�jP���9b��)�x�.��?���
TN�z��A�.{eD��E����țݱ��/,I��o�,�@:����)���_3J��,Am©�iS��)� '�$�5b��lی�d��>�*��6�v>�+�˭�'��VM?�ldV6ȗ��&����o���{b��ӫ��ewgY͡�bL�Ϫ{j���;g챀~	y����zrUk�]=Y�b�B�g��]�F'�h�� �$Ѝ(�ئt������&埥�"(��e�H7T��C�I�~3_Ր�6�	t�����k��BC^l�>%�ĺ��N�s��<�����@�sI.2]"'v��\�b!*��WY�}��ɜ����ݷx��5�/TVDӠ�k���s�*%�c����    �� 1�,�=�?��X|y����ivۃ`B�$v	 	���kߎ����/K��\�47��~��,%����m�s=[�ɕU�)����|j��N�y7��h1��lyʫ�߫������Y�f��t�!G7	�|�]\a�� �KK�w5�B�����s`kϘ,�T\2��lõ�	��o��&�S��M�j�J Q�"�%S�o�v��-�����En?��AҪ���N@�;A�\^��С�"�M���jxZrp��H�o8j"�~Vr��%Z��k�M��a����	�b
���-���Q�����L!/`����׹���Yjs3CK�d�$W�v�n>������SQp�� p�:D�
�;YtA�U��o��g%B�����v�zu�++�x��^����]c�I!�Q��"h��r"\���m�?��k�HF��o�:�+���`|��)�lW���p���}a{D�(�9��&GZ!�E���7�9�����@o�H��O���f���G|9��|r�7��U%.��}��?�|N�넙q����!�I��Tm3l�\��g!�L�D����WM��6v`b�s���OJ���6�(o1�.�O��e�^m|N�\�U��B7��c�lA���Dp��J4����m��Ud�p��y�k2AQ�\��!5/�v?�V}����[�}eC&C\ �����j`�Bt���h��79�� �M��ne�_��p�ߏ��)��N�+k��g9�^;�g��|H��@�Yٞ$|ը���v���u�o)�'��"Y���vv�O�oS����_����D,�.\xj�
.��0��e;���ß,A��;�RP��e��o�$�P�������!�P5 �;�-L��p��hg�%�~f���{�#�;RV����"U�~��
�_�N��!��orK:lļN�E�]�/�;DC�ݜ[�fK�����Z��ܪ{@m$��5X�f	 s�(Z��E�g:m� �.h!��Ţ�b;�|k8��v U�v��?K޾
�V_SxM<~9=K�U@��Y�/k��"��@�Qq����+���[�~���Ջ�N���eM�>d�߻&>S��[�7MrM
]U���4mCU5NoQ��s�Q7��/\�/��?�S�8��]cx�.��w��׹�`�4��z����r	a
aS;7o+�v��fr�[���-��r*Q��4H���r��/P6�.,�o��1�J�NͧDg{	�+*ӫ��0�싃Ӭ�z��[ �M���ڧ����\>�"f�o���7�C&��u\G�M����Y:/���}ĭ۶��:�7�̝W��ܶX��E�"�z#���fے������-�Zb����\s�+L�9�&Kn{��r�\��s�vT>W�%�,�~*O�/\.-�A���v�ʢ݌Zv�H�,+�]�T��?K��}��MY1m�}\6�YA����q�� �g^�:\��M���;�R�ms6���2�D��'�~'��)ŀ43����^��BL�h�ӳ�跴Ҷ�.�����A�t�h���_�"�Ke���A��ٰ-?��~��m���X��eH�O�&���[e����dn�VmZb�S·���T�_�O�y"�9�O{���yo��k��I���!e;�4y$��Ex3>�����'�b�ga�o�u�c'r�I3�ˆ�'��]֍g�B��۳��BMSP��7+T+W�� ޲���JbiBWH�r!n3?��N�	s�r�6o8�(?w�&����(ց�E��0��/�E2rʾC��������e�����A3
 ��Lr@�U;��I��x"��Gf·����O��Ro�/T�`�n]�h�}���:>�+S�����b*�:�3L��7��Ӄ�����}x�.�Y�Ĉ`�C����16\j.�q6�k�Gɉ���G	=�|���΄��w�M�$�q��y_�u���M�d�y�z�TI-6A��l�#��H�]�gZ4�̷oȋ�V�N� ��o� ng�_�NF�}{�=��EG��#�R��t�9�
�֠�oǯ�R)����+,��3������H�7�㉼�L�����6�gL�˞�o5���7!>�Vw�	"�ن��_ ee�
F���ք���������6���x>
��o��`���� 	iH��܂OC0�}k�j�[�l�����F�䵱|�~OiV�u&��|N�������G����!B�τ�21-�W����b��Ι��`P�e7�s��Id-ؐ|Ľ�|���Jh��Qu7�C��)z�K���� 0]}pg���e}o*�(�@���9Yc(��;2]���)�B�T�m�
��/7�ӓ�7t6B	��N#(��.���b�����`EW4��D��/�7Mq�|��y$σ	��}�K��>��L���@�ILV� �^7��1�>=��~oı�S�����W>+�7�i��ʷ����h\� ��M��iƧ�j`�IIq����LwM3�5f��X�O�*v�n�PR"���(��^^옼vU�:����p�fWw!%�}�Wm��Dh�gF7�W�_h�Q����M 4���[/�,��
a�ns���wζ��_��*T��t�+L�5��'
�d���e�'���E7���� Gg'��sO+�I��=�I�<�d�,���޻F`c�o�	4��'uL����ʮeh�z}qnJe���
���'�Ȕ�=�fr�3�	S�XH�	����:��˛
`��'�2��Pq��a~���+_��$	�<��m'￪� ���z���|�"un�Q�2�k����%�\l�aD����9��;�z��q������o����\uTѵ����R��!��G��e��8t��^�	�-�͔�7�嵨��l`"�� /14�^��僂%)[::t5m@�L "m���a�m�e��an�2�o���ʒ�M9N�F��.�:���D���7�p;N��z�G
�)��t�/��P1fK_��0?���d�P6����HzQ��u�����Gbp;�ɥA�n���EqoB0gQ�S��W�qY��ugF}��f��2doD�I�!Y��;�F�A<�R�y$�O�qu�_~+*�	��%�G���<xl�^o|�W�F��*��i�����s聧�Y���ٽ����:tҙ���C��e�6VQ� �b��N���y^+a4�ls$9�ŗ�����O���㈎op֙M�:�m��cK^�f���8+�m���'���{Ui{<���߼%�H�7��ф)����z�U��,���,���n����"	����8	$b�6"1i�f]5X���S?2�yM|Mq�i��q�6MH��S��w�3�i+��_���m��l���q��e��s�%X����m�?4���gZ�����|u��gY^ovFeKXh���0'�qIJ��
u�c'��'����Vd����؎����Sx�du�ϲ�{�8�"��`��hx��ʌ��[]�D��qOV.H��H�N'g�lYgM�������	�L����������k���ƵH����XZ���6=��6��l9C� �*�S�[�(�QW�r�w���������o���9ͮ~��1�Al�U�A��1y�^�,/%Ĉ�q�n��c��?j�U��[�V������}pp6��L�=q�C>?-��I��l�	d���o7�;)3V;�dX��C�W+)&���?G0��m)Zd�	E��m9�4_�h�U�yK����m�Sؖ�w���B�!�B��Q)
�E¾Fl��Q�6�x�&�m����~$�׏���)H��u'��O�(�8�׀���6$��w�F`y�Magm�1
X����J��t*������ׅ0:ݱ��m`udڏ�P�s�I �<l�B�BG�q9�e�w��paC�f��+��z��K����(�9|4�Qb;v�㺓�RRT�������q�wG�dV��?_[�37�}��0�Rh�(׊p�3N�y̪�i��l�!95��%�hHL��3]�f����{�?U�"83�*O    \wtN���9�	���Vy^e
+h�op�'�Jt��G����x���ᕻ�Õ����[Ή���9~=�L�.p�WTAS�an,�Ύ������P� p"�qt�%�' tʦ/���d%�@�,x��of�5�@S^b�G�z�,���@mn��!&.Oc���\p���g^��#x�u�/��b;O�,�,���]9������>�r_�Rc��rדY�������|f�8H��t��{궬�nd:����tEj� �^D���17*��S�����w�\����?�{F*m��eȸa��M�"�����Ď�2�[ᓼ�n�0�D�̮��	b��\E���"��C���Z<���'�ީ�el��:���_u�8B�ǽHg��� w����P��}v\"�vyը&o>fFl�w�i��h�f��o~d����1��&rT�	3c���r���	X@AҨ/��7�y�EƧ{�C�t1�;����ߺ��<����*�x��$\��߅���F�oO1��b��r���%�f��y�E�o�R�S	�c?�G�I��?9���XgS+����	5�4���=K/��y��������#Q���%V(�$/z�����\�=�|+�J�BX1,�
`����َ7t��8�$2�gc:�����2�č��8�H ��mr��i�q�����U��kw��<�p>�/Zj۟,_H�|q�W��D��q�q�Y�]jv��ͩ@Ok�>�D ��5qԄ@��R��Ĝ4�x,�����JHh�jN+��@h���	p6�s`w�Z��S�YQTƤ��]�e9L�e��jb
"a �*C�� {�G
���T>��C�zn�Z/\ZU�=iW^�`��JT��8��s��$=ʼK0�j���=|5�֘2%�JZ�ΔVK+������%bi�Y�i�>�B-/?@���F�) �qMuw`^ecYi)ߕF@x�!�d���3��b��>��o	��V�?���{S6�"��8�pz��'|�\�^R�p,Z{}x�h���D�9t�k7t�L���E��$��:)�Y�Em�j�UtO�~�>i=�Օc�� �a�aJ�c<�ÑD�0SilV�"K}�b%�e��G��8 c����6���V_�w�݁R�x+��1d�-ٓ�� �JF+��A2���缅�E�r7�4���G��Ue2wa��H�2=2� ���w^���q�
�ݴM��oi�^���ሿGQ����N�~�lT�;C��	N��6���(��:c�W@a��<N�V�� r���2�6n�*3�.�����_����V�^kb�'�QY�ܗ��^ �@��7��A�ٻ)נ�n��>���C���^�t�>e�����mM�ۼE_7��v����56U��pБ8]u���N+��.�P�	�Lw����ۧ���X������"d8�!3�-w# "�3�t������z����n*�Bi���}����RHݎjK��RDgS��*-[���,���͖��+�O�
Xr���I3*m�"~N˾�#�)-��`�^�����r���d��l~� 5wʩ������u�f��wO˭���ni�7�׿�M0��PT��g�;�g��X?�d��}-�T`����ɗ�0D��1��3����e+3�2V2�Y@q!�n9r(�s�%����	c=����긥%)�AC=$�K�4\�E9t�W��]]�ٵ��d8���[��cm�O�i��#ٽ6P�+
��s�q���>
*�]�J���pv#�^��F?���n�>V.�w{zS�@ �����@�ٔy%��� 1����/#���C3���կ�8��!�~&C�R���j�ɳ�P���%�����~}�`: 5�v�T6��Gdxb��	?�+�մK�M����}�/y�ϛ�4�H�I9"���	'|����oj<'��	\۴���qz�'�s�I�T`������V�����JT��ي��໙���a�ꛜY�ϧ|�\e[��u�y��`m7��97dN>D�ni�o�
t�53R E�S(�Q9�
�nt�>��F�����0�͐K|�=IDϾ�� ]��9F����֙�}��qd!��'>��s�Ϭ\����M�G�	;�0?��,SW�5U����ӷ�?��aF>�"TpLZ��|�� �A5�g0�H��d��-#���u���0�<��y>o�i�7 X��@x��p�H<�Lm�X�}��d���uoM�j����UM7��g��9�nH6�*����8G��N%[ý����&AW�}��@S�ٛ0^YbM6A�8�f����j��Pշ����C�<��6�(�v����~p�%��p���G�$l�(�ֹ �D_��UJ3^�4�z���ڙ���>�x}����ǡ�ַ�� ��/'��7��~ץ��YØGx)� �u��L!j]t �q���Z�X�L.���!e97?�����)}��Aɂ�A��>�+�Cjk���Wϒ����W�oi	��^��΋�6/5�
���n��&o 0Fd5�������o��!n�I㗄
��Fı�l_�Cz������߂�M�nk���
t��dz�猿M�`����-����e��k�����}�L|h���.��c�(3<7�R���E���_�i��)���~J�.���������4�\N�(��\Gۯ�R��S�o�cC��|O���Lǝ�??P��\]9����:����ș��������7E�xO�h�-A�I���|U�[me^sW�H4��mV�q2�/�+"G�L�ǝ�5mF5��b��ȹ�����_�Fԕ�����ц�!�m�L�~m;"�j&N=�,��>�KT@��ޢȌ�n�K����������a��83s�@�L����#&0`􋸮j큌3$컞���{������~
P�̓0��۰hɿ^�x��zX����$;�k�o~��You��L��߲�������X�~�[�9�8�y%�/-;8�`�F��-��yTM��{`nf$���!�%S,O�V��.���G��j��/�yH&�~!,��Z�ղz )��)̯���ic+5��p�'���i�)ϴ|(��e4�ݲ0spI~5�,��!W����eBo���������v�;/��\=�>M�������X�59n��h�A���m�-n1�ύ�\0��z��L\[�m3�oMm��ȵ.�W%yש���s��ѽ/}f�5���K̼/�Tzۨ���{-���M��
k6	�}� ��9�K�O�M�~�bW���IĖ�~�u�J��g�"<���U���VxL�a�����	b�X���k!�ߕ��V�� ��5cXa����0}R��Vύ�sU���ƹo�=*i�9��oA������D�C�-b�vn�����{�N�')X��)&jb���c�����&(����h�"=EJ�Qh(/	>5`��f���"y'�r�-���۵y�:��8���'���R��F�������RnD�$ǆO%�í[,x2>D�����M�w�N��6L�|��������ؗM�|���䇻n�[.�7k����uq��i1Z#Nc%�I^2�v����%�G��n�&?�%>��H�D�j_���wf����W��$�S����f�:f�{9�8�k��4��GgS4Z���MiǑ��|�O�i莭��WTM�Bu�u0���R���]�ґu�~�I)ÌGc��J|?.� �%u�$��&a*#@���7���x�j�P��mh�v�jR�7�ۛ6=������n��׏v���֠�^�O�S���D
Vzg�
�6 J�dAAy0�@�E�C$T��-��V���Vt��dw2��C��:5����h��&�5�����=3v$�ko�:o��H���bM��C���5�g���ځ�j1
�G��̘6�{���B��<�@�h"_��@Z�
$���Q��|�uAq;G���Љk�F뽻�W3GM���A\�t8�o�u���:f����;Rm�� nĄ�>da��{E�s��    88{���5���2�x�\�1H�p pHo�s����ߘ���5oԴً�C�u�\$�B �' (`�"{��U.u0n!U/����3�Y݂u�|~n?�����|Z #�4���8� ��8	���W���usX�}�l*��D���<�K��*�4
���M�	�2? 蟩`��5�-؝�"�&^���o&csp��>-�Y8®w�T�R��ʑ&� ��9�\E�5�֗w�;ed|����%J��N�K�� #8*��=�l�!��so����1�|�����┼1�(�@�K�?Ynh�A/a��q�|�>0���l��s��I�������Hc���]�L�}�ߢ�vČ���ߨ,�������+K�.�*�5`�V��+�e��ZNË��mt�W���C���Eg�2 L�I"+?�����=3-"o/D�S�焠z�&��ǍG'P*(7ݞ�5��gZ�������3����sOFUA]L�d�A�V����ZF6B~�CIM	�5:[3�"�hԹ��>S�dR��t�I���{�4+��Z$�ƧǱɯ�Du���0�2Ν�,��QQ$иe{YWP����Q�ڷ�&������c����K;�:,�6�W
�*0�����ϲ��S�����w���}��HWp퐾�m'��s�-�U��u����L���o�����?jµ����y4��!S�q�%޶3�|�,X�J�W9�a^ޫ���M����y���Ö�����.Ѓ�hQ�P�$ �8S���/Q�Tb���w<7·zm�d��x����_�|�.蚙Q��.?�h�E��p�dtD��3�_�MJt�
�/�'��Y�T�-=�M<�b%�����n�Ƴ�;K�:,��F�����}�v䕻���'�������|nN�>gS�:�l�^g�����v�A������N�����β�4W�&�҆x��)��;�6W�|���%Ƕ��L�HLR�S�E|W&�L�֤s�*��b�������w]��*�
�Q�$2��?��/�z����[��"p2ϟ ��.��N�p�Z�'9�����3c?j ����� ��ƾ`�����T!�ʫE	O
���x��V��d(>���,�7V�q���������K�y%����a���;�P+r���8˴/��,�h[j@ړ��C�q�Am�no�����֔�T{�C1��ZY%�,1:H��QV!���Ը���W���M�TA(���@2ŀ����b�ו"���Sn_��}��Q�������h��|I�����|䄃yai<7[j�o�T\Mx&Ps_I�#�D�k�I^���U�MSb�-A	׉*����3贞!�a�j�)�Y���^c��P�\)�;��|�P�����c��Os��� ~��dT�S�~>��Z�ñ��"����{q�F��Ȱ^,���`�O׼��E�{||��Y���Pq>��Ru8-�VAx�eZ`C�[�%���������8���ix[a��{�-x5�Ո���f����tm�f�+��g�wMj�<�o��#���7�[�Lh��XJ� S���T�^%��F��e�V?#������Ud�p�H^4ߒ�Bn�
��O.Ȉ�v�@��g��y�p������m���������^Z��)��6��&�!�M���z�θW��MRm�4�S�e���js�+e�HV[�M��HJ��轏���H��Μ��kJ��?[�^狊}M��H۴yU�mC7�1"���'pp��W?U%Y�3��s6]�� ��>����Ȭ4AK*rN1��u#��Q���?��AU��:�!����!�J(&�L��E��̔}f��3QU7q?6:����Ӝ�U�z�a���������:�F�W3;�ԫ�G���_D����<J�8���2~Z'	� ��7��PT�j�)@�x�:�'q{�Ŀ�>���ʮQ5M�] k�����t1�/R��f��@U�7���F��k��H�#�-���>�w���Z�oE�q.]��X ��7R���V<�Y�v��������^�/o���sC���3k|�VX��{���t�;b���[Z���n=_����2���p��=�E�1��Օ6�	J$���� ֦{�(2Z�q���#~V;��>[qK�����_���?���4���h����:��q���k�#�� ͂-�{n$R�;���N*�_$��.���X�o���⺒��8�[�`�4��|*��-qv�׈��E�%��ۑff=f�������`"�'�O�_5�k9�i��}cp
;��\�A��Iҋ��dݶ����'y<�Ee�r��A��x�Ѷ���s��j�a�c0��#&Z����&:8 8�^���胴y(�<��s�;����C�mཆc�<��>�ǅ�õ�ܫ���jm�a�GA�D���ⵐB��,D2�Sq�R;/j��a(�-��-�$%,�Ȝ�+���~��a�@{�N'l������֬�u\�F�o	hN�b��]$�mˠB����U�%ҽm<��ߚ�_��wR�}�U�fAu������1���vxΑ��S�{2T�m���>��,�]x��n����"P:�+����r�@�( 3�.��� ���(��7x=�F�Wq����1��R)B])Jj�Jir���{�5��l�*�>D�m���ퟜ�PQt��C��������TM)9/6��X�_�oT}�0&��/���P8�#����F]8H	;PQ-��d�f@q�u�n����2��RP�{e���)� Ca�폘4�����+YU��P
y���-Oi�˃gJ�h���0\��D�+�;�H��:WTc��)��9��@����i=2�$I��"���]~�.�&�C���5_�$zǚ�eF�T8&��j�\Q�-�|���q&��L9��p�#������;����\~�
���x1{�+e�������?���'L ����5�nL���#*��gB8LR��?m)a��::��fVIPɽ�g��9`���X'�/\�rP�0�����B.ysha����IVFW<�x��eG4�S���5�Ѧ���A�2"�/T$DA����<���n�ve�L]w���p�b�^L��>�f���2��w���K&g�@���Ƹ���X�S�w�MzTE�EW.;�I�!lx��{��RzL���%#Oe�W-�$��uJ���/��T� ��lh�Ƕ'-��F��I��	P��8>υ>Q�ɉs����b�F�W�$�4kQ ��=�8�2��W�-���L��=�*���%A׀`��t����%��>ml_���Pk���QP��l���x�D$4��x�s�Q�Z�����/}��d�{����z����:$\��'��r}��}��¥a�?���EV!*<�6gT:N�Ű,����.M�}��@Q���>�$���x!N���P)��S���Z�5sa������� s�m�����g
���,4�j7˷,��K5H��/�pd����#I_�ry�(Rd����ۘNT�m��@����^H�B�iN�q��9�שiG`T�
��r��fU�)�#�㜩AT4�b�:۶����UQ ����7�hI�t�x���W�/���^D�Ŏ�zΰ�'Lq�WJQ7�4x����C!��7�f6̏��|w�u��F1�<3C}�g���\k��'�j��JB2`��h�P�
��Z]+mɩޢ�V�@������M9��e	�EE�c?N
@|�����s
#����	P���� �scR�D�Ҥ�wC�*�6l�C���_ɮW�N�a������i� ��F@xL�ώ){[��~���Fm�ZN%����Cr����%��l��j�z_����d�!4����p���#�����-���-+�8�]��D遡ZB%��;�'=�4�#c�V�v�p�����ܸ���6�<�����,�c�Ӯ+��a�p:j^}���i7����1h�fS���N��e�w���\r_�q�	j��Ƕ��J
��M�G��U���'�    �7���e�
��pe��p�6�##q3������e��c9��X�3_�%�P�������kx��)���%e6���������O���5G���%����M^�4u����n����fU�N����<
��ұ���[Δό�����R۱���ȸBO�O�z3�%״��	�ir��Q���X`��n¼H/ c�4�W�~n�������&>q��@w�7��tb�h�@=����^����-�����K)�RW��D�}7u������G���ު+P� ����ٰ���l��:�c��B�Dw�3���� �,�Ngɾ���]���n�:�a���>�x�I�zi�u���q��x����ܘ�T �h��om���uX�����f��&������,|f��U���'�ɿ�EhȢ�&E���]h��� �y[��`x�WW|�O����8�UQ�������JD�¥_+���{������X6h;$�0Z���i�����cIGQQh4=��L�'��Gz��ҧ_�F���Q�ڹ�K	�ܶ��v]6)��mZ��������P�T^R�kE��k�a����0��Dd@y��=�V�|/��+��m�+���Ҏ�����q,��;���@V�<ϗ��w�$�4!�,+����H&6�Pz4��I����u6�辷˳8��1����u5As:PJ��,i�Z��᤻:#�3��N�Њv��33 !$��B�y��/�|��C�2�4�o���X5���а^�5}z�'_��e�m����Po	��ƒ#0�Fir��Ō�+�On��Fw��q�.ĘNs�n�5u�}I;��m�u��	��_�61*��nm��S���{�^��O
���fJI�晒/zz�!�Va�.��ս�=����~�f�/y� #����:H;��±M��&�ޔ��/�GV'	MI? �T��1:��7�f��q0N�ϣ�oz���=F/|���1�x|H�mC����l j�{�Y �$���U������-���s^���"��y(.��M��\H6N�߆͵�[��bb8�E<��b�O�x%7��M��%�˝���1JJ����	U�m#���C%3�!� �����*U|2��'50��p,��?�q�_��ڛe3��/����G�
�M��F[f�$��,n�Y��Wӝ�ď����$��ʐV�(��ǘ����
Q���.�H��<�k��)����ĖR�fh�Y
hA��y�"�h��\��s�~/�l<e�ARx�֧	H��Z_KU:�qY1�v����1���c_Ԃ���B+
`R��3HS5��p�g������";Z�ߝLz?zG��!$�U-8<P����(a8�Έ�cR5�������/�OZ��y+u1<�HS���v�ͫ�N܋����A�`��8�R)EyZX���p#X$L �#�hŧ|�"�,N��\q5B���\�����?�ެ�_��O�� ��Ƌ"[��w$Eܨ�ɯ�sV1�3����ĉ�A�'@�����h�
����L'��ssO����ѓ>�B�,���ܿhgr�v�U�k�,�)+���T��?I�N���v,��I�xo��"���E
�$��c��,�� mc���7g"�]���BX�d�����y�?�,��>FV�'��E�}i��<n/��i_�yJ�i�U��U
���N����H�n4jxJ����u	��zc[�ͤB�O�y�c��#q��~�8��T�#�m��5��)��h6RƆ�o9�"˞�席��r%5�_�U�t��r�#98V���1��̟�C��d������祢��f[ic��=���T6S��IXb$���x �~���×���Q��D�A%I0�hS`cڅ�(��0)�[+:���٥��I�#���N�l_7]�̌�����z8d��L�e���Y̺Nch;y	+X�T�ul��U���W��'9�sQ"��S��@`�a��˺������,]�!�S��.�o�B�IAݾ�n�Y
t�K��q�|�Qo����4{�K����o�fo� ��L���,˙W�~3ˢ]+���
~��?�J��t���[����ĚT���һ�~���b�Š �ƵR���s��S�͊�-	���.����|N�g���C==�*�\N�/o����������w	�O��ե�?P�����#b�L�`��q3�����V��Q�h]�<��������y�,W�8`Ӑ9�9Ϙs&����y6<{0�;������<�U��KU���$@ �Gia:�$3d���=��.��t�0]�U�t����q �h����m�`5���^`���c�7E
�o�S-MYĩwK��5��#r�un���JE%���t���xVU���%��6���5���B��vq�YØ����gY�	گ��P���p�r%6-��vy��EϠ�"�2WzԿ:�n/�Ũf��Ý�©����%�3*�}4p��S&�!����:�n�Z�.�$ccDS�oI�<��]�+�t��J_�o�*t��A7�F7�e��5Ӣ�����?@��V�nT���9�JiZ���,����Z9c�~p�ĭ�7̈́���oZ��9er�)�������u��UM5�H����߿���&h��D�O.�@�;��0��+�b��/z���$��/B�)�we��������C�ႛP�DJ �G@ڶ_I�gw�����e�=���mgr���\���E�rp��Q~j���2�0_=���ě�qt]s�����,���/��?�~$|����_2��`��S~4�d��G66�U]_uE��C����#�>�p7|������k.�����9�}����d�^�cg\�GD+�(E�to�� ~�L;��o�b%6T@��� J6z��Ey�F�5)|3��&�޹��~��!5̙�0�o�����:�FM�8�_,=/4���d;�̲6����%����Q[�3A!.�#i�f�ߓ?i�����:��7d��"��N����3��oQFFe$mL��3�c7/̍+B��$�e��
'f�_��%��ƶ93��,i�習������؅�A9+�>�W<�>y���h*��c��I-�-���4~K����v_��s(Ҟ�3[�1����-K�zFt���w_�o��z�'y�~s)ef��7�t�d$��)Q;ӎ� �7���f�	�-3��� ��޼U�P���?�ri�S��ʓ�y��9p�&w��6�쏖$�Za���æ�/������L��XLL��2�� �n:��Md� ��+�k�x��� :}ރo�Ȱ��U�V�|�=��q3D���6=Y�^�L-;j����A�>䅤��v�Ô�Q7�-�lJ+@��~�'f������ȓXX����n���A�N�&�U�B5���gA�/��v1���x>M���L�+�ߊF�ż�<f-5+^͊�b�|�[F��(m$����%��sc�x}�u�z��6V�X}&_������q2���������T��������Z�D��[�ژ�H&��(�T���8m8�(��c��a���J^όڴod�n�H�+T��	AO��!v	�Zk�U�\v�g<���RfcX��Ax����-�jvNq>����:�DVVwE�z��f�eu�#U����q�7��%������# _O++���
7�'I
�p�Ũ�K_H��5�I���$-]'-�Ak��d��q!�Ppof�9|LG}�Þ�0����[����N%�%�Y��ϋ��G��d�7~�^�na�,Q��f�5����L�L]��'�������T/j�"�-��U!�����I�/� ҿ�����=�.�xC�����I ��#"zSD��K����.���z���aM㹓)���}I��]O�@�?�������1h
�!,�W��s$&�f)��@��Eqt����A؝�[�^�+��Pu��6����Jh�P�Ψ`(���՛�r�r�յ���v�H��`�;��=��e/�+�;���z3���G``_#�
?��Y4�_��:�}+    ����{}؅�0��z˦ŭ��YKշP��g� jq<�P����+8|(J�:�r���"\z��1�W.)�U�1E���Z����?�
�\
��ozh\ ����1�8�����L����M�Џ�_f�1�]�%�%A��3a�����I��,�8Й$�fփ��f��|�,ѫ@_�D�:j��"V�	9�!�SG�� �W��	A7�U׺��LU��]u�I3��]&� ��J"��o.�	3��
��A���[��MYj[��wMsvC�A�}_���HF��Y"�A�E>!�"�8�#�B&�L		GVƶ�-f`>f�=B�a@[���9��0r�)�-y;E�9�B�lY>���7bZ+� K�WnR�f�C��!��϶+O6}c| ��F\2!���B�B���	I�L�h��`S�=��H�Zd��y�'񥒨�w�2��ܫu$e��jn	g�j�T��5+�:j���h	k^�Է�
�~�؎����Ey��������{������p��(=��.˫ڃHͧ��I{w�����[t�q8N�~G8�&n�to��`D$Y��Z��InxY�K�w��S<X<M�>���x2H�qz}<��Ά�٘�7{���ˉ�~M��풦�V���?S��ε���衺�xȎ�N=�ȿs���U$W�����rB�6 ��,�dI/
�x���^�a�`_$0�i�8HԬp�)��(����q���)��]'a�b����4|	���s��!�H��g����fʴAK���(�x�C�E��ެ6>�Ͽ2�G�<tb�((2d��d=%3���h�=�����.���j�M��"�b�_K��\��?��Q�O��K�`8�JQ�T�9����¯�\Kｬْ�y���Q}�bYa��@:�#4Z�7G�eހ���t���b""c����s���bMc�-���V�3�˚�m�F;H�2~8��I�~�!�<���֍���8����b��I2^�a5�g�'Zj1?����l�G5�y��N]���j0���S�\�pR���b�B�ZT3���=t��P�o>SA��	=a�t�)~���s=A�߹ߎ}[~@��f�cLg�!ə��z���W�8���Z��~�+ճ����$�D_`N�$�J(����e	TFDd=ҬO�h�	L��Գ���^�&�dGST�1�����`s�?�y���1��9�K�&)y_�1��5.9�b��#�j��޾������6�+N�.L��^�Xu�����df!�;'c+�O+�>�F��k#@$D���/_�����&��	n�
�0X_:�"��bG�+��h	�S�'~�#�b���S�,��iY%V�,W5��b&@��ol�:��#�`�������@�`&�����]�T@GO�i��K�<Y�26'����]�S��n_�'a;&_ߒ,7_��VbO d����̻_G��A��=M����i�E�P^=��?T���8~F(dڛ�u��f���6�)߷zn�s���/3ˮߖ^����n���h���"_����9��ro����k�����y���3��w�b<�_���	W�q����qNљ���������Xi���hX��t�e"g�!�}��#��D�5C7�_c��Qmh����'=��ꍖc��4�h6#B5���.x��YG*���\����#u�X9r&� J��{�	-R��X`)ʿ���g� �/�c��4$��!<��fE�O��������/E�m��2	㻄�4�RȞ.d ��5�����oGֵ�eZ��@c*J�qA�LglZ�B��5�S�f�i7R�� ��9Lmf��s"��+x10���d���%�����ױ�eۖ}m2A���ש�yTe>�R|��F�S�a_������U8�k��d��;�����t݌��mZi��_��:�F�4�ip���ʖO�,(�Ѩb�=H_�o��*7+�0��
�5�0�V!��̘����x-�]6�ϓ5��4y�K9�r����Ft!���tRV�I�[�p|�o�u Zj�%�zr6P�#����_xxU�T�����RW��Ӣ/c-vH���� ��/>q ���-��%p�SJw�& u�{7��_Ϣ�u,�!b�|uU����Vdk�������uG̿<����H͡,�1~���e�y�ȷ��k�+����Lw�Λξ���)^�uقN���;��0纯���'~���Q�Y([똛�KW�}
����X؋��q��r�ƞ,�j�v?�&�=u�1mi?���T>҇:����ٕ�^4A��7p�@hDк��&(� -��ӟ���cB���n��\h�i�|W���\�����|Ei1W|��;�[1_Yzy%��=��:�cB�$S�q,cǞ�"�ly{ʸ�҆�]���h�L����<�����Mټ�t��������i��v�;���� VW	e�%ӭ٭ۂ�?t������0ja��6�%Ir�nAr���z�Ѐ�����%3/��`j���6��r�LYő�[v�v���YG�4^�� �%���=��c��V�/�vzP���!BU��B��kr�^���}�j3S��������Z"B�n��5]	aH��)Mh��G����LZ�Q��kH�z�M(�k?��f��������K?钰>/*�\T@X�%��|�p8���c�<�^"4���'��|���z�D�K�ߌ����5r��r>NL���u��K�w�Di�X[暑|�#��8���ګ0���E�����[z��#�~�b3�E����8M��2�(�Ц�*΁sE%�#ݩ�0,n���!*�u����0�c����R�wܧ���n
�e�F�ہ˱��+	��ƮK�����b���TGONG�p\�s�c;2�M�&T) i05�J���-���1��5{��'5,1ө�i��7}}%Z���P_�
P�]z��W�-��LI��G/�ˆ�F���߱Ƣڢ�)w"W�A��Q+ވ|a�IA̕��w��]�+qIỬ@BQ����� �͌{��I���3�H� ����:����D���4���M�]N|����|$�b�Q�/�����w����Y��?�X�ʣ��<뚊~��2�)����
J}���!��J�P��(� me@T���o����> :�@�Y뎁yE%��:/�+���Dp�a�ߟTM����xl�ޘ��V~W�z�>� ʪWF��4)��B�94���62f�V�v刌�Q3^��D�[����,�5�l��!�,(��.�_�@�+��_B�h:T����q��|��7�>n��
Z�!�+QN�1I��3��Ff?�7]�͡Gt�$F�?ŕ" ]�!Ϋ�B}H�����cF���K��3H�������-�[c�_�Kyp��O���#H�_�Hq	�n���ڧt̮X�7��!�>�O�V2$�f�'��2,T����5b"��4�x���w�d~���J����; �f�ef'r�⬅���U3u���	��`@�r �^��-�u[�. ������W�u����ȴ � �&a�	��W�$0�.��[�{z܍׶�kL@g��#ڪǆ`*��w��VD�����r��U���jL �ګ0�u��e�*C�&2�}�	_�L���nj�)�����119��[.��/`J��=�-�I.KJN�"}\=�b���L�$z��l%�E�v).E�﬉�a:�E3��h�-�7��=Ğ������<��y�d���8�����ޒ��+�/a>�]��"����r�
y����x����]�mV�m}�~$�)�bL�1n�������KEJrV���=�V1�ƕj���u�>�/?�fЦ�.��C�d���fgʟ�Ói������B�]����x�^�p��NJa$��!-�B�㦷��&o���E�=/ ��~�Z�D�uU�p�%��X:l�t�2��	���U�5�����c����.�:�B0i�%ղ���*H"�uN�/Sc���C��|-pC���\(܎�/&�LG�ſ]%�'    ��~j�	��_��f�`��g�O՟���芛:7�"{]���������k�����v��ȯl��	���0
5�J����l����_^+��I�ef�2#��W&���Y�{�G��縎R׎��Y̡C��C�^��l`�[��K5؟T�%nl<�c����7�~�?��*�=2�37�����\Y�l1>��G���V����BA�A.�0����$�̪��$5� �#�!�'�j2FDK��e�)�|d���h����/!V�~t@�R];eב����;�f80�J{u5��]�D��sͻ+:N��x�ai��L.w/b	���|Jc��>P�_��6&,
i�Y��`n��u�DK޻�zۗ��z�������g��C��?��A'OEEܺ�a���f�X��B����f[�da�7�$͙1���}���m!�~hс�*�ff$>{����]��kQ��*�N�G@�7�Om6]�����t�'g�mE�xQ���F�&S���{�#�����-l7O^�ĥ��b��h�{ )R��~f����4~y�wq�|��<��^��`���"�ž��UD�hD����H���^���=�F���u�Y,}-扪U�5�s�c�{�NEv�]hʞץ�F!�l���=�3��#ΖMh���9��Ⱥ�V6D���o��:ȝ�0�>l�d���kD�������,��y�8|S�>h�1���D�� �_���B������|�v-�G��&�s T?jv��a����"��5܉��a�K=�����{����Q�91�O0{_�-�N��21*�i���I
i��nj�/׬ʸ+Ggş�`��4��ѵ�A��(� �OQ�G��Z[H<1�5��d�;[��2�0������ɞ�4�$WS�*�7�B& �FL��ѕ���]����8�vq)-�횜�J�q�`H�%R������_e�������kaA��������,g�p��%Y�qfO�<n���ع�I(�[���Բ��M9�5���(j�F��l�WI؂��9�2nK�s��hB��o�ű�C=�\��<!#��/��Z��r^���c���Ȏ#����������NX~b�&)ek�1JK�2��W;_T��A���M �E�c��O���8y]�kK���
?  �u����m�@Н�,��|�Q�6��B���i�x?^n�٬E������X��݊5-�`a��I���T&I�;��8�ψz�g�rP���06-3���~��@�؜|���t��ι��� ���5�#ڮ����f�]�5v����b������r5�zF�P����ny���t$��$�t��Q&�ƺ^��-IR5_W������\��=KB�ԫ�6�;���{uث��<�]��l�S"u��Wİ�������X�Y�}�vX�_����W�@��	�%�,���[��I�'�#��D~� kU��N,���(qM�T�nO�ɯk�F�M����t���BcR΢�/����w�S/�x}g��eƷKT�bB�����l�*
5����ڿ�����W�FÂ�X�T���J�'l<��ض��{e��+l�b�4V凇��#��3����,�����V5j�f��Qu-�~Yz�S�-Ц@�7�GA���~�b�cZג�[Jj4��(��a��b��_��!�T��I:��CI�h�K�m�3���r�X�^
����Mj���e�Rү�Q��23��'�Q.�T��}<���W�~�����kʆE?�Y݁�qD�3W�����9q��k�]��������56��N���]?- �sv,h�ubY�Uؑ��&[���p=m�I��E0I�ʿ9`RǴ%H+�$�j��b�vEs\����V�(�;��)}_i�����he�e8w�"8��-�h��o�7�}a�*m%��k'
nl��������B�>C:rW�E'�uK7�� X��.��<�7M{��ס	7�}�K�����IێC�3髤]����{s��c��V��<N%at��t�4]�n�P�xW�8�+{��Rq^����G��!�n+O�z�q�O]f�kqq]LK�?����ְn!<Y���*z�-��AGkP�����Fp�(f7.�|�gJ�I��y5������e�*���y��k�a�3KF責U�<-�6d�(�#��.-($Vo��h�й�"�mF���_7m�(��s:޼Q���C������&��u�M#�u������@��Z����*|���+����	B(��� ߈b���S��ʭ8����@s�"5cJRa������#E�C�nx9\��J~�q�v��E0�����yE�7�)��޶`��zZK  �y��NȽV�P�t���f�I�I����#N��)@߰MD���6���f����Cr�'�~��*��y(b֍0��5k��K��P�5�s�.<�u?,H����Q�]�v����IS��ڰf��)�Y>X�+�ÌO��9�W/��$Y�ؙez���2Y[A�e�שޔ�m��cMm����3�`�����>��[�s�қzO\��Cj�wemi��B����0�����Q��ˏ�\�j5��h���[�|����dƾ�TϤ��W[:o��� P��9��#�్\R RC����<�f牌�����S�7��>,���'��.����3��GB^����Ge _��x��,�%ө����aP�.iJ1r,���6��� f�恂xy`������b���7%߱
)П�\���5-_	��v�[��I~C����þ�����/��-\HR��f�٫�-�6��MR��/D=�J�� ]�JϦY��?�J��u6o3ॣ��']W|�����y��Drˎ���U�Xv����x�Q �}ٴc�u�,��V��'����B�6u\ �Մ�"K��b��a�6G���G��&���a)`���������l7ܖ�,��I�<D�9C��2����;}	�%��
��9�����	_J�����m�:f'Qn�'���fj���oߓ�9�B1���[�&��Tl����z�G�U��Л�s_������
"�����������>�w����'U�r�Z�E[�1)|�~ɣs���峞8�#���ylf�=�f�C>i�)��4��Y;��<A��+�	OI%���.��:�M���`U֩,��6�ֹ�Kudld��(>�Wv� �t՗�eꥨ;=41>��s���e2��~G��z���Ǘ�*e�$�����F|��-����|3��}j�f��i�*��,`~A�zr��ٌ)Bt���qE�8.�; #�wb�F�Ke��<u�^A��~�$�������l��S��<�WҶi�)�Y������m��|�bH����V���G���#����r^�"~Ƹ�~�0�B�u��Z����{��f T���A��ѐ	e���B�x�e/�� @M�[R���/��>�T�K�&�y_~�B��j��:�V�Sf:1
�
�ʏ-t~PAs�a���L^�F�~:uȑ�SkE�A*Yc�5m�LC/ۥ˯_/8�P��j��ǳ��7�K̲��p�!O`^�u�"H�E&'�7������3���k��C��������*h�/N�����G�?<ŷ���q|D��f�:���)?<�a�ƨ�B� *j7�_71�1�qf��
�utՈ��ͥ2���'�a��1����Y���/;������O���k�.YZжN��-�d��ԗ�M[@��`m����N+�cQM�C�p,�-i�!��q�Dꄸ8���I���a0ht)��{���&��}���@[x�����z4́=>#����d����eQ���=kH�����k�r���o�Wq�q��j�޼PΓe��o�c�g>�������5K� ��R$H�6��1B���M���!��i.쫻�n�F/�⁫7S�ы$�����t�����82�t�x���K7�CH]�p�H�vN��l	    ���$=x�o�a�;���5�W���3�#0�mo�w}���&���it���/���}���z�F��q�$\�[����t��T�=��p��1U��ӊ\�P-j�Y��`�џ�iP���|_���q6ah�n�
ݭS�-*�'� �#�������W%#h�e����RzKÏ�^q��#��]��I����[i:G8.���ao$D]���9"�NT�jl��0F�.�L'X��,��L�`Ic�>�������+Q�8A9�nh#��'Noq��,��MѼ������w��a~ל�.�.,��(Nn�i4������v�֧��`�Ƽy�ySc%]� �,�������K"	-v۸.hB�����%y�嗰�3��x�/Q�H��4V�XE� E1��I��8�U�x����}��@�"(��o��1�S3]s^tԑ��̴�.4W[p��yƽ�1�aƏ�ڢk*Gm[��DΙLw��Zo�.��$�MA�֍���Q_~=�Q���	�ځ#_�[G�2#W6�34�Z&U�vx�l�\8wz�����п��w�"�4�� e2z��:(��B�@i�y�ڪ��	t��e� M�:r���}g�؂����T�߿�2���dN����b�T�&;>�/���ZH<>_5i��PM��#���d:o�8�� 񗾹�ȼ�%��{S�b������&+eFm�����i�eH��u���1 ���O.�{�5����w�q��:	�c���
m�ק�V�j����f t.��)�nN���Ӝ1Z��kP.P۰|^PG��6�H݃��
ᇽ텠��c!�M����?Ew�M�����~� #o*�d8$����*�F5/x�&������cറ������wX��=@��|F�=:C����$�D��j��#��mc�rs̠R%񥼅"�{�8McmՇPo%�<�c���Qݞ�;L��/���������@|�g��थ֮��9� ������if��q�Cھt���^�Y9W�2��~��UF��2�Q�1,��S��I�)��>|�+�`�u�&n)9��&1�� ��'��J�P�|���˽Mj���5C�5�jA#��7��ơ�_F�
�E���2�E �t�z���G ���<P���x���/*�����Ք����7�����[��u'	@=Q"�@=v��$��c"���KHa��Ç���~��x��ň�0��ڊ~b�#�������R�*S���gSլ�Y�����������vn�\k� FTj�7/a�����eࡲ�Ze���3�aM�{�)0Eb��z3��9c��7���w��ո�.s#�&52�^5�M�%�@���w�o[XB��I)�E�%E��=�DI[+)��]
I�P���}z�뚊����Ot8��_��"�˃��\������+ꂸ(L��c0L��Өzl�J�"@��_�"
�u�I?F�Ċ8�r;�Xs�����$ؾo1'�r�2�4+�V���>]fʯ.;
�)cS)��q����@F���T� �ur7':�~��D*��u��8ps�;�4��%n
�y�&_�L_���]������0*!���Lj�i�^�;��d!�jp�=����2`^�����}(U>��1��dl�������U���8�̼�^[��N��kO${c�Jc�v>m�\ѫ��<n��7�8��Fb6NҎ�b;������2�A��m�0h��w6P���y�Չ�S?B�y阗J�mM+!�5�Z���˙�š�W%��t�cgwȱq�q~wE�;�J��,���#�yl��v�Jn�>�H��� ��f�T\lLů�ns)|@���x�!	�ejR�p�~�k;l�q�v
�y�=��l�����������Xk3������y�H��5��F(q#�RDU~
�~wX�|�y�=�����wC������!/��?�|5����� ��	fm�o}��J,��m{���v?r�lj���)�˯�mƽ���o��T:F�7��qFb���]d�� Iw�6b�G�LC�����:�.� i��v��V%�[?�8���)ƺ%*��~H��
p� ��#Y�1���t^J{9���#S�{ ւ5�s��Z~c��Dp�D�ͱb�8�1��e�
�L&�dx%�c��NwQ� �����->��u�z�!���}��b�|R0��e�����(N�J�8�%���EX�Yc��&��*�j�&�^��ȿ��f�s�����W,�S��L�V%Bȿ���.ƶw�������B�����<ϮVǄ�HSk:kA��8�)�,���0�ACfJ�n�`�}��֝4kK��%�x�P�)�PE_��r�	�2}W%c���e�lZD%��b�5�_)J��x*�Y������V_��Z0�b���y�d��-��A�@��{��5ц��<�֯�N]&���ƶ\���ѥ�u,~��� r�.����@T�߉�a��0�W���iܶ*��P�o�����N\w5}מz$��5�|"���	�]��-��Q����o��U�������ޭI���Qے��B�31/ZQ��1
Ʊ��G��5"��~�e*�@iu�C3x���O~:��w��{���KQ�W��rá����6ݦk�����CHf��-��M���>��`����a_"��o� O�)k�������`�mFp�#���N�~+	�=ċ<}D��[�`�	{0���O�|�M�GjtEuR}�cu�˩��a�� ��JJ6*�(z�k�^�O/��%Y�B~v H�f[N��f�I�^�,=W��<ړ����%�K�˷T�o��?肙��օ��$�a`U�Ndߚq�H�L�G�u��0h��?o}�fzQ�r?���pc�0Jha �,�kǄ�4��WW��c�����^�\&��0��Q����U̢:t3����*�P�m2��+0.����/��al�v���˘�"Ⱥշ9˽�n3P��e+vʃw��M�d t����a��5`^���_A5�!)�z�䰬 ����1u髋��K��LMS'��c��1���u~SX����x+�[��5��"����\ː��r��ƺx��KA��RI���˯G�� ��@U�]R�=�~���gY;f�y1oIiTh���s���������7WE�݀)O~���߸:ogu 
?9�`2�ݑM�x������,i�|�y�*�]��'�ݜat2���D��t��[�iL~.
	5^�jW�V&=�k��-#�{8�����PE�-uW�PI**֫lv����\^P��VY���NIR5������㢰`J$�lf�f��ߌ���4/	q�XTd���:��k9�����?��Q=��to��� ��y��}�,����\�eL�U�t�7UT�`-�;�,�~�9��IG;�H����j3�E��Ĉ�v�@ӭ32�4Bk������qK|��Y;�������~�F�1T
&��H܆7��$�ǲ��_���,��g4Y��N��Q̓�[)䢚Bx�ߓS�h�?0g;�Nk�^�?��ǋd��B�g�>j�z�|����G֭��v����r��Cno��������U�+Dդt)�������_�}�����ܫ�X��ךK`C9u��1ݣ��pp九[�.��$�|�x����q]F�N�w�Z�Z�~�H�I7}�o�p�����ū#�U�P�!M�N���c�u�?�)c@D�������<+~.����٠D7�֫?U7�K!Y�m�hl#[=�����j� �L� 7iikc #&�ڇ}�X;�j3��2h��
�@m5�@Vy2��I��;�q~�����ئu�m�J�I�%r�H�<-�@0���+W#��M�n�JT�ڸ��yk���;s�"�{�_�#Cّn�c���D?f��m_��-ĔWr>�䏻2i��sgO�?ή'��gg�J��V�G���DF�����f<�v�X8J�ۓTx���(D/���M��~��)A�G(`P͑����Qv�r�	W����m�#`B[�� ;�O[ݾ���<�    �2��,�(E���(�sJ�Os���G/���p��Bv��8)������-á�#��k���E��9��F�%�C�^�u2��U9�,�7������r1k��"��6���%�����E��k�r��wW�jT�室��y��!y��x/^K� ���H��2�X��T�~Z�0Y<T2ݝ����Y�f�Wz���c���e���t@8�m�}�,�@sKܜ���2�Rƾ�ϲ5��e
���~�V��"7�'��|.�]�� N�e��$�n�$m��-*Q�:\E��FQ�x-������e�=]����K�]�v��&��'�h�Z�0�9IEul7�Ee$ߖ[��^zx 4RX�9)�|d�Ѕ�<b���ݮ�zHB:�Če�{���m�Ԇ��q~�piG˯�ߞ׿W�2S���#�@_cZR9��;4M]jH�y�|^��T��X�1|X��j�>��>�8놫��~��p�,����d�Uj��a�����s��g�-y� ��ߜl�#/]xI7��}��Lc�'���������p���0d��l�@������a��!����~�|�4�JٚѴ?V��[�2l�8�������Y�@;8�$�}����c��^I�ܻ`��doW���eB�]%����t!�:|�6��K\{izW��~�D L7���>
�m���f��)J9�h�FR����XoK/����wh¾�~�Y����U74��
�I	��B!uI?�k��9�jq�)\Q K���|�֖r���Q��~�W8�D(Ϩr{	t�b����iΨx�Mjw�\�� u�S�;�H�%
���]a���Pc����S������iE���АjF�T��EP���������>�G�"B�R�.��6D\"U�xH��W���X&q̬{��kS/�^i���,ʕ��w����n��
�uc�Nq��g�+�UB9���A�.��_y���yD{FA��M�=�R�~�ٟ-U�泦
k���ߞ�{Hz�A�E#H���A�=?���}=S3��`ݤ�A�~��8HFZ�_ڿ�e ��+���7��x-�[����zyhڦ��1��g�OA��z9t���Wo��8��w8�6�3�:!USa��(,�"�>eʇ�hI�����a���<��DO���7���>�ǯ|@b�hۚj\��*s}����~D@DK��3uRܖ�Vv���q^�w�?G�w�6sh�w*����Y��<I�^��R��qE�MD,\���f �m2���S��,��_�T���-)~꿋n�Dh`����䕈�Ā�� �~���^�9~Y�F���Ps�?�jKmO����Ke��8���G����+C �mS���[nO&��d�˲���X#Й����Yu�kd�D:EPWG�����]x�:�6� ��-Iyx  ���[������Jf��3�ĳ�:b{_�.�k־���N��M�`�@������FH���l5�Զ�ڃ:3U���K�-8�:��*���]�~d��Z�^+��ͧ����k��,��.�FQP4�a��4F�|#��*O#�jQaO�%s�!���A�
��<�a���(����ʳc|�*�^`C,񠛀�M����(-��0;�!S�r�W���9}�)f�`6ٜ ���X?tt�R~��?�	P�ũ�ۛclg����*�7��x�h��袯���Q�t�v���/\��C���Ý#�o�}�ۧq�aS��`�
O"�Q�J�Q�=k���y�]k�q��?ZA������ٝ4�1j\�
@�?@2�r��� ��r_^�W�x�xſV�w�.��n�A~]{"����C���gP~�?�v�ѤoE (�'�UBL6qlB���T٣�{���d���Nj�.C4�2Y�����8���zb���=��2n����W�JH�w/��7�BK#�.+%�d����:X)��!��g�V5���s}A�M� `621v���`�GD�8�ڕ@�&��v�U�ېx^�l������YyuJ��t��nܲ��X�KLx��8i��q��y��K��u=*#K��q�k�M�8��uGm*�C�����|��ԟ4�{�w�!=��J�{�4�P�q/"��xnm��b܄v����|�{� �y 㙸#ԒwW��z��BF܁>Vj�I���$q8o�
Y����x*_���0�P���J�TFu[qtI1�1�[ �ӡ	E�º��Y��6�x��|�x�w�x�8[�65-���boJ�]�a�K�0�Ƞ������U��Q�>��"�dIKo�;%@�7�uu:��PW���_�j�7X=�_��Y7���[*,U��hݛ�����O� 'p*�0?�YG��2��	�� };C�w��JF��S�,'^���FjkO�"~(����yIA�ą����{��w�qL[o>47eUX�;���ѾR���������+��Q
��9����x� ��~B�5���-{�d?�9���K���hR/��0�\K�A�+���4+>]�z��~a�6kT��`����é�����v������{M�������@������@1�ͧ���[�7�h�%xf�
�᪶��ms!JQ���C��vi)`�k��0���!�d��A�);���B��� �Ω~9�_� �j���:���IV�h�VVL�(]{��C���7��w}֒m�N�����u���91$IR4S��=.G���R�/��[�3��*�N�8��8�5<[�oQ���#�v�S�}�������1�hI+�B3h:�L���&	V��v?�Ɲ7�����V0���"�A(,���0VW���%������~7{�6Wnj���D;����Rj*�����K�J��7��~^\n�R�ct��"�qN�ޤ}D^H���`�y�̯��p<�h�UM+� ����J-��L(�c��L8R�4�y��	�+c �=�7g��$|����8����A1�(Q`�2S��g��@0%Wa=�g���찘��Ժ�N�2t�E��I=��k��1%q�/��O�R�f����~[۷Y�!5��2�a�\1���1��&_N�J�k"��֒]��l�'k�5	v%�*�"7����l��ػ�g�o�B�ǎi���\@���v��� ��KN�A��2�|��{��<(U��k� ����F�-ũ��R��$���	���-a��Ȕ�j��P�����vۊ#�5���wF��Ic�տ�~�o� ��S��ykBe�;F��K�Z�ߪB�R�~�R���_~�;�܌�^-�"��y{NV�8���_���V�w����9�PsJTyB\�d��l���mL�Q�~�Jl�׿�ߔ�)Ƃ�7`0��FNZ�NXK��fY�
��4��%%��Lm�~��Q�I�եU~l(f�E�G��H&�i6?��qMj�ޏk�Xk)9�a-]E~Ǭ�4��W|�n-n��BB��סY>e�xdk!���Ő{ds诛�1�����,��+چ������Z�,�k��vE$G=�>>G�!F���W�r���:��P+=�l|�d��ӷE~0�����#�k��ၺ����[�Ѱ���a���Z���fg���%^c�G�~��f'���.y�l��/q?��X~�)O�Լ|�M(	{8�I���E�Q�:���X�F|�_{�?�Gѽ+��\6�0���jD��]P���O�Jto��p2��P-�C�w��i:�A�~	Ыb������?z:���#��eU������
��
��6�)�f�Oczv�5
�0*�^�V�O���o��m��*����'�͖�.
�-�� �����QA�I9���͹l5y�M��0[�����;T.X��[��o��0��Yߛ��A�j��}�¢�G.b���\�Gh{z���	�@H&6�֟#NF�
w�wOE�|޲�{�����&�@� ���M�����~�z���L6�4>��q�ڇ����㪱�=s1u��t1	t���_�Xa`(~�D(pP�sh��ۤ#�*1T�,��NVg    ���DF(Oݸ竽^�7Xk~�Y;��j�ɏ��S�P:�zk3�`��iuo������,!�֚����b	\T�.~1���1��Q^�^�9��*�νx��Vîɫ��P�1�Ŧ�P��Ińz���w���9�e>ׁ�����DwKRI)��-�%;��Z��tnD������!�Ҩs1QI$6�-ov�CDO��=1/I�����-/�~�o<KߣVC/��t�]!Y��ഗ��/�mʗ�������cۂdL��A0S��q�p<cP|���~�\�;z͔_��TBͷ����6�]{_:lSG�4�i1 *	��/��EDi�5��&&
����@�UΒ3YH�1��]<؜��h���ކ�C�!θ�Hfح�(�TXN08����ES?$�h �].�nN$�!��^�uч�)5abI&��h�ac/�����娨��
��k%���A�֪b!3vW���έl�{�B ���K�'n��Y�V��o�%�������#$�X��b2`=�ܖ����b�n�w��U�"��eRw�CAe�8��ۊ�I�iʏ��gj���jB�̊<M6�u����v��/IZ���!���zo���=H>)@/��$� � �`���t�&����l�Pgl�'��i�p�
�-}�M�}o�~�60T�S�kr�c�X7%Iu�p�o��� ���J\[c¨=��A��S��,����W¾�#Ԡ����W]W��,��л<�f�%m^g$��
�<�O|mh�ЮmU�8���0Ͽe����1J*��I�g��_8�� ���b��lf��7׺��Ʌ�S��l§}Vq.^Yۋ��Z�'���(u]���sڧp������0����[)�j��һL���5b��z'��b�E��"_��a�c�,<��s��L'YяϨ3r0&;��K�;50Փx��PȰ�r��@�����꘥x��NB:U���.���w��*<�W%q���K~�t��k:3ǐ�ӿ����}�B�3����wY#"AO|z��hB*���hø�J��WK�Kݻ*����*��KQ����5*f^�&�**F�\��,��W��*a��֌^�c�l��2�r:��I�%@����oi�B�����`݁�_e��{<0��AȦ����g�S��g�'혲՟���ȭb�]Բ�C[?�-������w]飂l��GEX�8�EeC��L�*��Nxii޶\N/�mT=!kl��E5x��϶?P;�����ª� J�=����H_�,��D�@+AW8T9����Y�h䉺�F��/��������(���|u���@:Z�+@�T����+Ln����i]6��0ݡh�<��a�Htޠ�\��NG!6YX��{p�ɭ6�;�U4[3��O�*��ۼ�\�H���s�t�6��䮮G
�yXN���\yzѫ�5~�NgðUF|��1%;Qh��XN���ߎ{f��-f++A�Q�Z ӥ�kI#����6,����=�r������D!����5haѬ����U��yV�s�?9eD��z��~��ʇ�����#�E\[v�yî�e$U�F�����xĚ/)<�'����fb˵y�����#�ġE."�����pk�_�(���x�'"D�"������5pkж�+�S��=53�T$i������R�H�+�p�v��׆��s�LV�xi��'a�,Հ
XCz`1j�ɟ�P����'���alm���+�������g�<^�	.'��a�� ⠵�2(�Q��BG6�.��ć࿈�����<y�,ت%i��*0�e�)��1�H�����U�ѿ%ل��Ԟ�4p(���]4y)��jh���9��4 l�26/̞�LF�ot%�0�bϿ�ԋ�r��P��a1��T��\�������<��)��9(�TS�^��\w|fT��I+�����{��n]�o=[��$8��8X:�Lw�����T���l-o���z�����E���q�j���hW����)32ӯ�;������A��0U�&�љc�b���d��`�X���E_����x��'+�k�o���]�0��6�ʹN�p���X8��f��Z�`~����-�m9��v��0�ʿ1�R��
��-�K��ܾWa�7e $���[���=� ���ׅ�t�����rO�3J����S���|�gn\I���9S&Y
��峸()
���6sLqL�͵���-,���.w���S����'�N֦*
?~D@��.�-������=���X6�<9�%�}��d�E�����ɞT}����y�1�T��{�G|�1�jC�w���� #��n���ɛ����ܚ�,�������Y\Q��E���u��A��P�\�j��/>����s	�o{�##�*���ٕ ��ɴ��SFe/kf����٪5��A'�P)��BU�����`/����l�{_��m}Us�7���=�ʶW���R��i�+U���3n�0��;�φM<���g��+^��k��z�M�Yy����i����oӽ|��M��I�I%�n�o7��u�O��j�E\�TS����U�%��(t��gy\�����!y�9�N�ߖ���ۺ�Z���d�����uz];�3�<���I'K�,�K�\r�D*��Kی������8=_��������W�۽�K8�����_.�a��O��ѓƑ�	;����g�j�Ca��TQֆ�n��
1��f�_����:B]"&4��jE�f�6���i�ll%@f��:�[��՝L�&��ѭ�� Ij�J�=n�Z�k�,�8.J�R�"(���3��#���Ww�`�� �<��]��K�Y�X�}i�KĢkS�֥�^�1�g�;{�Q�c����Q���*�:����;��֞�Ho���P*��8��~��K�:�(j��Vzh����ӯ�����Ǵ��>��#[c]������������ё<��G���}yj9͂/��K���z��r�PI �̺����IXhC�]W�V����͕!��������I,�r����5�m�����-��p)0C=��dRYi����cf�=�������$:����s����ׁ�-�$ԉ�S�c�y��Q�l���+yG�f�}�қ.?�9�NCG�=Cv�/"�?����U�?�]%/�d���YW��ME����w�lr����y57���r5��W�{��A���knLa{�*�5 R����r�dr�Dv:�b1�|�j��\�x�ﶤ���!�򋵻�n/�J=��g[�i����]��ݥ��j���ͫշPU�%�XO��
Ч�I�с�dҥ��K�j�~*��Sy4�ՒLu���,5��NK �拮�T������e��S����	����~ؽR��}��1�(��s��(��}��1�~q�s|�RBJ��LzLoy�
u |��M�qJK�2�V�������{c�������u�](q�,('�(40����Z���ZHhD)*�'�����+�%�SE&o�;Ў���NxJ�H ����vluS��������̉����������vn�6el��E���V�s*��^ϫ}%��(	^f؞�9��u�[��Һ5���
]9v1�;��P;<U��&E8,ٵ֍�ra[� �`N��la1X��"3y4�t�-k�����>��r��:������ޅ~��\�D0rB6U���q���H�eF��#�:���
�Y�QNL,(Σ3m㹡QNZQ���<p�"�e��b?"�w�)����&���|����l��%a��҇��P?s~�7IA���Q��kW�ە!�{S��Z�ؓ����(�������Ӽ�q�~�[9]�C�r��
�~�����Y�l?���%���NA@��/�F����&M�Do���a��8ϝ3Wa~)k� �ʞ;S����Z��WNp���:�Ў�a�=�<qB9���t����z?V��WӬ����5�B<m��.s��}��X|J��ۼ@f��HA�?�,�H��/���ۜ�
D1:2��    �>� ��q	��ޢ|�5u����b��>\��:a]U3S��:�����٠�`�(���>���{�s�$�P������WG=�e=�C_�k�	W��1��?��U��)��f�i�
�N�%9�G��ģ���"��M�g�6�E�j�Δ#-�@Sp�[.I��#�J8|�i� ߭b׿VmclM �o�1� A*�1g�OA��+D1�4���� �z����Ԁ3�/��l��28�/laY*�D��as%o�.��N*Zp�]�eR�ȩs���?�8�pRYZ��� ��f5p��� ?0.L_4ۜ�"�.��Ż�u1�'��f*���w	'�D�*���r=�����ي�!�E��Tݨ����K�������C���?�5�Gko�`���$ϩ<��A)�����ȶ"i��6��5�h��f���
Ä~GR_��΃����G��#������^b�=��/6ԡ��Y;�V�ԽP/��>V��A����7��2�Tʹ��J�k�V��oU�[1�l��!�B����'�a?���?��lL�P��pj�"6��>mR1�I�횉+{B��q�Bܺ^�Sz
~�{;���ò��nב�T�,D-�em�����6��9<���cx����HmE�S�_%͍�@�,��Q�L�������!�FQ�E;σMx]��Uw�28Y�h���$4+���%tr�m�S�c�'�Ѭ�խ�JI�m����p�2^�D=���Tɶ1A�p�{<q�Ȃ�v��aj��NG7����O��f�ʺ��C�o|t��1��S��h��5�Wi+p�`�	�<)�d��W$z���~�C|zY,g�}�F��R��Ǘ(`':'����$@��G]C��n�"����W7�*�&�f�J��~Ӈ�4��Ѩ�5Q�O��{�R��9�W>n�A�O1}$)]�z��G��t� М:�:�,���9v��=�i*�ڨ�a�x�����Qʕ�|c9N	��1�H�s�w-{o7�i�2��GN~�f�T��Sq�ү�'Z�����l���fw��{cs�xhX�<o��Mk<ֵ�ж�f�f�xo��e�$<�{Z�Ma�}@���7V2�f]b�b�Rb�}ס;�չ�C�"]�^�ۺ�O�(8OkU<�W�=��as����4y�oYG�m^�Q��0@�h����2s��[�G�5@���_�x���ao1�B�'qk�\���A�<H�f����!gi��M�)u'L/z a 4�^�u�����-ҍo���	������xi�I6�1T�2v먥.�p�L�!ِ�l�8�7���3��1�g���9�l�	����Y�u���{Y�y��ۼɯ"��s�����g������Rl<9b��,��g�#���."�I�t���1N� ��0��eB�|u=�w�/���C�t�е�E/�[mv7>'�"�\\(�׵R=�����(��׻no8C�Gl�X��7��Uؗ��n�si�]~k���Z���@��/f�sʻXa�aD�Jw�rY��K��R񅿥`�L�/$9��`{?�t�9i� �g&��n^�mƚ���kij�\�^��bSߟ��r&7.�\臣J����l�/��a�_�i�L�R**K=�7J"����$�(��W���������[:Cj�*0��쯽<�;����y?ջT�Z�Or��)�7*��0��ێ&�����s#��Q	��2��{<�^4��8�`{=��VԀ�_�}O�f/�}L�:����S��y���NlݛϢ	�՟����8_���%O�$-�b$���BC�M�j���ؔf�UɄ�{$��q�NdÚ�ل�H����fSۡ�έ
��i}��n�hPP���?`h~�ku�G��G�f���=n�¼��79��9w[��a-�#6��`l�a+�}K�Q�h���!���/�L ��f�yڧ</7u��R:zH3K�X𚖯��u�|c􇴇�E�fc��P�$��h}F'+Y�_7Kz�S��:k,]��R�)n�z��}�2�7��!��K��{WީU�&,�<�?&����Q�cƩ�������)����*�K���h��l���q��E������m\-�ޥSg���d���j����9��'x1hc�G�G��(T�4�.[5���HqUc��XH�y�.��T�?�\d5�M�I�U�d};V�}�??c �ǆ <�'%�t���ߙQ�w���3�����`�oB�9���6����^���tS,IҔ]��r-ds�g�#��tsN��W��"��ѫE#R���v,�pA���t���׈*�{�[��R�N�5JG
�Rљ����B�Fm���+�e�L�!Ł
��CV���m�����wD�[_P����J�-"�@��K�>�g����}S������j��2@G���� �O�rY�n�-�ߵ=#�zy't�`�SYN���B]��ǎ)a�@�˴�w�&���
�p+�*�6��#�=x9��3�q��Wf����!U{F񽪮B�"��ΗB�O��qXv`ͳ���7ȿ�󰾷e1<yU���s�B���������Y��ic��_�4D��q�pd�hX���r5�ޘ��H|���O��_*��	a�p�r�y&o�{��;�i-���0�������7gT�t>�l�Y�
V���h{�gg��.Jf�q�R�����(U��#L��UZ�+���@:�§ S3��*?��z���
pL1_P)�����8��������}�����	t����F�0.�ʞ�����[�:�3�= ��K��{G� �p�m0fX�M֌|x�[�a��+���f���\��k~���F	������������!���6 �OG�Շb͢4^6}.�'u3b$PA�mو�K��E	�>F�V^hi�&���=	��ѯ��S���1�W�後Q���x~DC	S��E|�셧M��Y��wds��It�Ǜ��`���zf���5K#�QS%T4���"�SSӇHW�P�����P��ݏ���Hv����5������`�m��?�"��~Hn��&J�"z���D�ٻ�쵟0N����^z3��ç�]̻%�1��
\0/�/8~�8�c|���ۅV�X��$�A�(㖲q�6���o��S(��Я>�K�
�FV��x@�� @8�]���j��n�ɐn�,�W��D^�i
M$� >�z\�iI��㼩���{�ZX�CE>�>* G�Vd-�$��;��ky qyE\h��q|��!E=�,��)�\����V��2���ӌ����>��]�,�:Ʌ�:o�^�H����<I=�)�=��o��r�Y��>�����������1h-��ڐ��r�����4�큰���-�LU0K�"X���K�iH<�M��t�5TȚ�O r<�{Ƥ!�N�F���y;0&{��wٶn���U��dn���eM�q��;B�6��N��]a$ثj��F�+ٳYw|�q���a�ע�=e���"�<���$�C����Nх�A^��V�&�Icg1�>�=�]�
	�����R ��$��;����z�ܕ�UYV�N��fY_�%i�h����U�5��^ǭ��(k��1�ԏд���R��ΙLe��=��a��q�ef߱�;h=�+̫�Ԧ������ȡ�70��N\��8.��y��z����ų�h�]<�����)��� ��L�\5�NJ��A�GN���~�yK�>�#��������ӯ��j�vD�yW5�ժh#?2��7Ǒ�p��}�%����ߺ �DE�zѭzQS��<��1��R�Ʀ �rܯc�!�+Z��a5�]��g����Q�1�7�D����z��ŦŁ"��i=.vG��I�L����F��07�W7�7kA�"U�ت8�K��1��֕��W��p;tm?k�Ҵ�2�쀠���w�J�_��ָ��V%x���K31��y1>2Q{7Ci7���4�����'LFϫ�H�
�%NL^4�@f��r� kq}���]/ѭ����@j:�1�K����z����\�~�)'F���<�YE�7���    �����Lm�pAOT<�@$�W�\9[<���l��5�U����h�%������j�������uN�1�7g�����l��+?i>������5�֧`�ز��쬲;�Ƅ���B�ƈ0N���H:�$G� ���o����ޟ~J=��� (���"+���	+��h��2�ʔ7%CpN�d�$�%>*u-*_����B��3�~4��uB�`�s���0Gc��'a=�كz49�HH��S�@��f�߹��������~M�hؘ�i�AH�)9�zQ�gp�4���^��ӧrߴ/%�Vb�lAG9��K*+ͅ�>"��8�t�Gy�E�U\[�!Ow�_0s1�M|������ۚ�P�,��%���$L��U^�c�<��L� AG����;J�����F�0�����u$�m�����=��a�z�([5�9�Tj�A��?_d1��:/t�|d�=[��Q/tJI�Z"�`b׷����4�)���ïB�@����;���`YCz����S��@c��� x]6m���
-k�Hyn��i�*�̸c���}`��`��_2_�`r�[�ߕ��t�-�K�����/Q�t;�ڡ��d!ևQ�.���E��/�Bp�]��#�'��O}:$�j�d�F���sț�"	4n4��h�.�"N���Y�b�P�Q�<�X�]� �M��@��q�6�4�I!��W�����|?�~�O�s䢗��J�!`]�`k��ґ9n��
0�vf��p3J�a��cF����Ϳ��*�L�V����62ן���2� wqϸ�e�a^��>���s����M5
3��;�h$O�^dB��^�������OԙD����|Q<�d\��OJ�����Q�'��#*��!)��1���o�Ι���-+��1�\W#T	&�o���_�jZ.��=�Wvk�P����x�7vK	��R3��{D�I6�
W�t�7�=�6��i��S�k�^�֡���	���ͻb" tU$T+p�K}��\�B�al��\xnܲR~��b	B���Or�� `2��soL*���Od?.?Ƥ;,�y؜e�I��w<b�'�����L�0z+�M�\(�Y�x[��x�0�F�Y260�6q'mC���ˎ���r[Q[���#��g�=E��ftIK
�YS���`37zx�lF��4���%�����j�߬��ȳ��"qT�C�3�����O�ٝ-�5[�T��%&��.}͍5��OƤ�>X��QUP�8*��۲��MQ�����⮟J�aZ��档9�E�W�����Y_�'[��M֓���l��4�<��#T�0^�ݡd4�>�a��-l�������nR1ɷ\f��l��~��To�UP��a�mȑ�^Ta9�����O�堞X-w����t �!ŀg��ߙJ��Ǫ��h�UK���e�S&�??�3�sTz��Y��Ҡ�"�z�|�6��Қ�;�3��u��:$�i�tS��y.�+zMGl����*���
�o����Wmu���aۄ� �F���	b�(�~�)���=��q}��f��D��ϫ�N��=���~�G*Vv��f�)g��"�w�(�Rhl�u���uJ��>��{��+���=�]2�����|GM�`�C��+��'��l
�������Z�o�g������a���=L�W�^@��Ϡ�|����@ҧ��fFNulG}�o�a=�&��[<�z.�up�)��-���-ť�?u�A�Y��.��#ߢr�eyk����Q��|Ȍ~��4��Tk���ڒ(eZo=�K;R�<�y���)��S���W�y� PD��}�j�ɜ����<$���YB,y�*g�
�\*���Z>̈�Cc�-/�k��u|�!��()��)!�PttR�����P]E�]�L�N��L��a
`�Lڡ鐫��z���ݥ
;�39�>{p��6��e��7?!��WRC:�7���Z�g7}�/��T����D�%��S-���mR�~pzW�.�)�kS��&���=�rj��}�:Hh0k��*d>(lj����v��}v��# 6�|^�%��0߯�;�?�V^�c�8��DX2{È`7I�mO?	��La2�v�ͮ9t F��
����9���_Ly;�_
�F\|�Ԭ����<�����O��Z��/��v-����(����P�ՓU��*K\71��h���=>�k-��.�'0n�7�`R��J�B�ru�ɞVS�)�WɯL��l�u5RL�foӏ�C���@�4��>y�l�C�g�������b�Dy�v��䁞8�@��'�Z����źƩP�H�dTfYFՔ+/��j�(W��(�OIiL�/v�u�����e�cu2����!��h��5׹�Ʒ��pKƲ�l._����^^���[g_����K��]�(���{�����ug�����KYa��0��9ܺ�Sw窪i�N�� ���s�#����W���d2�*}�h������~)�	��ц2Lz=�Q1F����'M7��{X�0�$�j�w6�����E�#�����lt�UNm�5��H{�ϴ<P=�F�~gr)���6�aˌ~f�c��Tb ���KO��g�"����9��9�:1���>;���	�-6+G��'z�3��1˂ی5ʸ᥏��yF���s�L�j�V`x�l�|�@ʓNv�w袾�
��;��� �~���J%�<״����s�7�y�s������1x8p���2��[ښ����Q���:N8�k�W���<fdnM����ds_)6��g_Լ3�zҳç}��tA�0��D�|w��Ĺ|;���4��!s=�׀�Q4RO�-�~*�G~[:�x��~�n��`r�T�]a#��§�_AΤوw�|R3���q�F�.>��@��Q���%N����6]u�2/��r�w��$;4T�x֠�AYa�����b�ᜡ�eI�G�bGh{�[�k�F&@O��Ŕ&b�jr1&	����|�|� �Z�"m�Sc��rS��\��M�F�#�O�ow6f�\Z����^A��I����Ρ�M9��z�V�~�a&�v��C��R��&���͊�[a��S�!�۠"��q��,@1-1��3�4[�'���i}�B�Άb{�w�$ xZ��'��&D�ߎC�������Vh�� � 9�R����c�_v� �W�C=^7��&`M�D�,�Kꃈ�I�A�H�N֒�Q��t�bCc��-�|���c?ӹ 4���چ�n���*�_͔uG��QG��	�%B �V���a��ҷ#h�_�L�C~c
T���>ȣ�̰6]R��v"��@?p��V6`����*�f<{�Y	<;|2��ܮE�=~���@;g�o=�y Ω�z077�Ul���,W*>���UdPU+�_%��,{"-���qG��3�_��D#��.3� ����Z` �>>��:p�}Q��kf|���|^L�n
�5Sܔ����;$�j�'�U�g�A����V�3O���<��=� }�2G1��)*��ߌ���A|'�4�uJp�;]+����?�B�6B�=��`��Z����X�|&	yI�:�'jݷ�̧��H�`��Ah?J�8<*�C%�:oꫠ��PM�R#���D1���N��G��@�#���l��.��R���'B�ƞ��iT2J}�8���[���I��~JֶU^>B�z���=��W����3e��6#�m� >,�*�*��v��\]B�y��GѸ�;����˽�������'�6#�������h��i*6|�I�6`�C�X��ȿk��}=�*�X9�=�B���TH��2m���<��-U�И|�4Q6p�s�b2��gz�*�hU��_�*9B��͘�e5k�Kd,w��wR$��LA��z�+,��*��O�SLԱԗ�	�Ԏxa���5�E������3C"���"H�PA�Lq@�����!��QnJ�G>�U���^�p2�G�E�ۧ�Ń�쫥���w�_��o�?.܁l5�p/�ٷ�2Q�1WE��Q�=�@AQ����\�o8��Sy    )9�� �c��4PqRP�j�8����L#�4���yٕ&_xA�nȼm����!�u�4$�W���mk�+ˌ��*�O|HQ�\�Z�u���2��Śo�� l�8�j�p�����t�F�����Y�6�S��(g����Z�|Pn�	ą�Qt).����\n��?� yȳ5�1I��۰��G�*_�)�.\1(m�+�r������d�$2���!�����]��t�\��n�f�b-����P��Zg�*ڻ_8�Vш��5m�|�#2�?j�}bЧ�ZG� y޹=x%"r��m�| ��4���=�ԡj����r�=]s�VL�Έy�Az��f�#�I;$f`B49��!��N��y;o���剑@Q��V����$���].*������)�j
{=4V{��.�.<��>H��P<��I7*d��TD�����L����T?][7�Yw�*�ySu���dz�����-�I3|��*��}��EUT\[d��Z��;Y�!;����T���&
UP�����y����@;m������b�z?4냷�U�g�>|&i�[��ط���渓���A����h��α��p�w�C����-Os
��a��Ǔe�� 9]TnK���,`�z:s��֊�)����w��Ӳ&�⻴�~��
T�cizQ��-�+�}�/�U��%h��q�aB��u�B��b��<��1*��s��?��̏<G��S���GUVРP��E�I{)��P��F�5&������9Ʊ!�`���(�)��s0"��U��6���i"r��=��; ,ر����@��B�����)�K���� �C�~=?]�7�G>�W�<�Z��۰���K����\��J�	귽��e��*���eO�i��?��(5n	h��еMj� �:�q�z�J�؃y���N�r��(�N�޽�f����pa]xmhz�jhwΪCڕ��f������M4�3�������� ���-�?�\��	�wh���h�:@.WU�zD��#:eF�� ���\�y��T1l���B��y� C`
��U��b�jX�U����r��ƴ�YKN�%�9e|�mm;�KQ��A�Yte󒢗F|��ǒ���5� ��g/���!��
�W�6�p"wI�P[1�z@�7'ߞ
U��	+�/��ˮ��*<�wB&�Y5��Sh�r��_���V�<�i�b�5|+��V��B�XO�}l�SM3��`�x�g��y��p���E
�۷b��c��� �+0W��R97=�Y�>����
�y~|�����=
;�u�zQ0�5��w�e��{M���m�̵3������W�u�1�c4�/��N<�0A�e��KJ�H�xϊ�a����;k�����I;xP����ß��ˑ�N��d� 0��	3+��Nq������R�|I�0+�
��v��His3��a
%�B�/]O,������X��_�yWS�h�^X�o`�S���!ަ��ϢM2�P�:�j���ɨnA+���@��26��-�Bm�q�#x���Ӟ��zީz��}9�������y>ɄT������skF�P�t
F�(5']����C��K�_1
<T͔n�ٌV(q	�3�v	���,��5�q*kp�ے���Wb�QU���6�J�>����b�!���f���"�l�{��P�R���;�o��v�����j[�U���kzd���cH��_b3p�Tv��L�2\n�	��Gֆ��g�-�/�2���0�ɷ�ψS9�����fКv"��˃*��y2��������7��%Q���f������&��%(t+�=����jb��
�������3j�#��Z����f�6����Q@���ڥ*Վ�!-�|'Q�;6QXm�4��7�_��4?GV����r��^5ʖƸ\��2�o]���������vԻ��^�נ�W��h��]C"���<v^,&=�
� �G���XC��;]̋*��ؕ\l\7S���۵���K�ȓ=��$���5E�+&O��O'����>��xiO行��lK�-�Dz�?��I�� X��'g~������G�tIC�5?��/����T���h$ULE�M�o�iM"�"�P����Z��̠�Ҟ5m�m��y�U�B��=��3�"���F"P�ݨ�H�}t-�����R��5!�t�f�k-���Z�cg�����E8C Rg��w�����X�1��d+�����}�-0��y��҇X��"�w�1S�|��7�A���~�o���H��m�YA�#m�﷎>�)�%;sN�OJd/q/����ӷƠ1^<�^�s|��xn�d���6({o��f �b��G����;�^#c�M�s��� ��w��[c/�'+��Y��4�n�N{��}0��TD���Ћ0?؃��g0�������c��Uq�&���s;"���h3� ��?��n8b�,�i8���;R�-n������<71ae��M��:"�A1r}�E м��g�"оM�({h���PݜL��8���VX�T"!���Xʵp��i�G�����Y���h(�Т��#1��&Mc�t��T����~.�׳��8��u�	������9𼔤^���R����hͻ�ib6U��#G�C�)�����NU�K������&͌��G )S#����$X>�{���q?/E)a�@��m�BCf��	!�v�h��LPo�;>u(�Pxy����X�g&"~��h�q�Q\���&�?����!y�Ѐr ���ɉ��	
G	$Z/��)eЉ#�l����d����3��B��_�1y�������)��y'E=)Y��T?@?[������P�{�[bG�Td�()UZG/ܲ��O�D��Y&c烺������R����#]=^f�}$8�%o�bx������Xj9HӜ����9�s���{~���Ń/|t*V�L�Z`���e�q8ZO�v��V;� �2�����Lۘ���T%�Ֆ�d0�|�L��|�;y�@[D��abp� �x����Ч`����� ��ۇ�m&,{B�X�eS8 .�*��e0bb@��!�]����Fb�&J�������;��9��Nz0�XFc�(�,j��*BD ������W�W	�  ���z��	E�-ͦe������
��P+���-�S����ve+4ɼόqT�W�PIh��m}:)B��'E�b1[:o�ý�&�ų�a_.�J���N�%;�J��7Sa��Ճ6��_�����@�t��0����oo.��%�r�.#���%-��S�X�|�(l�$V:nm���*g��^����d�C�z����	�I^��{�n��Ʀ��G���!�s�G��8V���,`� ivݾ���-���G�_���m`�*;� ����������x�h�j�6���X˦o:c���5�F�#"�e6��~v�byUa����;�QS���q����K�Q,͘颥�?:�1�F�e M��f�!���*�I���m������"T�$��ʶ�������r�
8���Ō�m(%�VWմ�X�	�����wa���l��lH��@+)�5��[�9���0��Y2�dē�t�D��C@��ǥ��]��w�[��#Z`@|�fc	�%��Y巓����9�;�"����ZK����0R�D������6��̻�x��jX�R�f�p1p�>F�����i�b7�M�0�:�V�.kw�q~UVTv�	��5~1M~�i6��>��F��<��Q�vx\�S�#�\��4?a�T�F�ˇ7�Z���Z�����`T�K�`��isi�nK� v��}�w'��P��� ;�8��ث�OϽ���y�m&���}�o)������s��B�f��l.Ts������,��������P�̙��#*@�8��kK5`!H�a��>�2�쏩J�.{FA}8�`n�]����%3��F�9ۥ2Z�;�f�p�3�2`���t�� �2\kUJ�34m-8�����    ���8zB�c���ľ?��c'� �&@թd��[��!�f����e�Rŗ�{�3g�}��j+�;�գ�dl�\�~�ϧ؟�R�_/6�F��>�|�d4�̭ �h��F��l���d*�_��0ޏ._}g���$��J�Ea�5��ğ紐�����Ah����� �钢p�Ѯ6d� w{qk`�?^Y���[15��O��~ֽx��59�b�KI�G��* 8+��ݜJ�� `3�,�7ث�	X&W�χ{yH7��'m���_~I��A�Ry a�.�����fL78��e��X���U�[}WI�7w�8�M�&-�LM��˛I�p��\-݊]6tI]˾S�M��K~^��1�1�[L���A:�JDn0]7��/!3�M�ׇ�V�{T�|7�G��i����t��6�e��G�;=`��G�0��$c�E��F�M�������wܫ E޼b$!�	�q�����C����[���C?tS>h���1��rop��ڡ������x��/Z�o�qR&:��&���
��{8��Nu�1>֬�_͑����Ĵ-�k��Ʊ��z��
"k4�'�vVa��x� ���Oǫ'�)������hb�ᚋ�I�1�Hж�^�i%̉�2�p%y�̀!���]j�����u63�ɚþ�Q�#�tt�$D�� �|Rw_�mEq�v(��D�������y�D=^�=O?	O:� ����xJ7xV��G!�|3>1l��]��@��B4�&ͻ:(�#n�!�[Qd�oj��s|�S���sÜq�F
��o*���3fmm�T]����l�
��x����Q�έOhT���`���2'p{�L/��	���OD��х���`�W?�_0Z^�(����{
 d؏L��c�Ҙ+��-�~�l(��ic�C��՜����������ȑ�'�#�NFs��j��{Bb;_�M��z����Tg8�Ç���JJ�t�S�߉�s��b2�`�ߧ�П����r���τ
����4�Z*r/l�'�VQ�b 
�;k;��ss�)�P����.a�HUX��[��j
��
�^��S�	
Hg�T�K�����!Y��A�^+
��;��L�?��/Kn�8��}D����+�a��6�.�c�������kWx��F=�c�dh^�*�]�O�}�6��W�`@����Z���3����
<tS�/9�������"L�!iz�	Е�-+'��� �eO0s��p.����u�/I��P)�6�?�ܖ2s��8�}&��������v?,�}[��`s��ǂ�,�r
ӥ�R��3�s�PS٣�Gm�&�����h�.��	�J`$	������oA�0��%yU�[cC'7��Kn��\1��+*4�=��4_�I?�F�I,�A1ȁZrx����a[0s����\3�/F'�o��Rw���><�mL�M��	LT_ާ ���ϸXӐx�9�z$�턦b�T���4v�:ڨU_~��6:U��ٛV%����0�E�h$"��M�^C�χtdK/l'�z
���h���x:�*�+�{�h�ѽ$NO�x���L.�Xx~,�2�J��o
�E<�c�,�b��k������wDl�1��x�5�tXA&OS�'�P�����g��p�"�"�}�3�5A�6`�{,|U��+z���������
���1o�ê�u��xҭ�#��b��؆�H����H��t��*�UO��Am�.�:VO��m/�zw��ч���tx�'�X�{�Pk�z5LX3RW��:3ŕ>�����\�<�g���:fr�/&"�0���g:'��k������������c�(����\i*� �����=�^^*:����N.�;�b@���O(��&��~L]et���HC>�� �n��#J�O�I�n0��A�L6,7L<�Tf#�%�~�1RLT��<���+1y{��z�{A��b�G6C��A���駂�K :e����v�\�/��1� 
��?�ـ�I��9$�k l����ϥ���[��_�?n�i�ze�� ����2w����.�8�<�{�0Y�Q�/):Xz}�=�p,��ڀ�)4���hhc�Q��R0"F�{X�n@�ނ��O������dC̯~P��S5��!��;��=���^�`��*������o�qw��^���w��0�Ws'É��@QB�(FA'�-�!�:1��
݁=g��� ޟ�LĘ`��#^���3�u��`��pԜ�S�m[�����+�I`�S-�-�:m�¹�pe�Fhp�EkT4�C�B�.x��C0�M �����\�פ�4������-f��?���E8���r��IS�
u���|g�_��"L����#�v¼�����!ཀྵ�� ��.�e��X�A �G�В�`��r�Dm�K�1�?��FI�I�`!�	��P��h
��f��G���;2O��������Dd��%]�-��q��2�j�2�܌aQ�$��3 �� &.x�g�1
��V���F�n1ʠ��^�!v$��w�X�7�` 5x/�_1N��2.#��I����j��@�*K�F�	7c�uݕV�ͼ5V��<�H!�N����ޓfV�$��U���f��bK�������tt.�1��NC��$�������0�?ۥ�-Lh�J�qS�����$@0kLㇱ
����<�f<*��k$�����&��
�c���$'�3F�R�{Y��/�y���p������N��0��e�/ �:��q��+��������߼��h�~FlB���m�BU8|2ۋ�I`�Mͬ�w-]��Y6=� 1?R Q��M����uǑZ���>���|�!��H_ѿZWKO� 2��nV����<����.6E ���qE��EV~b����3@�<E�U�x�0f��\���>r!<Q��A�fNB#��Y>ϕ�m�Fz0�o/:*�d�[C�D�}���n�+��((�U�-;��]���M.�P��pOS�!���_ڀ`�kᡘ�y��`(��eSq̪��亀���4Ѐ�` ���O����p�Ҭ�����|m�D�!#�3�x���S�}��oZp�V
�� x��a��&�l�*�v6ɤꤿ� �M�xί�و�驄Q;O�*�z�~?[�kf��^2��S,��=�oP�w*(vڇ��s�{u�>s,����X? �����@�P��HR��.�xj�9hd$�d��O
H�<��<? I3��>�0���V�/Odp�Nd�@a�@�M�eR�iZ7����k_��s�&'1�K/_-)e	%�z�I�A�,�+��"��B��ɴU���uq��-�j���}~j9{��f¾���?�t�PZz���1�@#u�_�8��Ǉ�r��[�������oq�������F�<�mʞ��ɡp�œ��1�x�N$�)�xZ��z�~;1x����e3"�!��8����i��H@��#j}i�"�|=R# ��Wȓ�g��v"7�HN����d!��)��W��Ѥ��0ӫN~�M�>	�J��]�Ӵ���(���>����
 `��#&�n4�jɄ���$6'!&���5%)�֦�yޚ��y��$������S�3��|���u�u?�"�@AA�1��ϟ�����V�"@��+<G݌A;c�#�s?�:�X��f��Dۀ'T�V�t�ۃ��^��Wz|�`�Z����_ mO̘�
�'Ah�<T������.LQ��^CU��e��tN����pZ�3C�=��)< `hø��Jn�IU1�+A^�W��8Uy�柲XE�0{Q����2b�����h�ƶ9[Y��
3V��zT�*�_�8��\@r g��7`��0�Cm~.�%v�
��A(���0��p��r\ʬ�{����1��]���	MƦZT�V��Vx��)tQd1��alf�6��Sd��iYۛ˟!���j���y��4�mQ��Y)yo������g����)L��t`v\�H��/���$�&��ד��$�dS1 ͈B(ҙ�)܄����o/\�*T����7
3v    �`�M d>��r�;܇Y������'�����~���z���e@��ʘ�e����4?o��6�;]2��b'��m;UY�N���{��z�̳�������ސ��/�/�F�?&h�����6�R��A�o�(�/]s_Y�+�p��i@���\4v�B�����,�Q��?�r1�ro�/�=k��%8��x��W@Y��[r��U���'��O�
�ѦW��1��vO��u\������^SȮ������9gU���qek�o�������9�}�HY!l�	����\����KQZ��Ԗ���wo�R��n�O�r$�W�p��|��8����;�o�i}���}�>�������-pg���U v�\�=J.lK
~:��	�L�i�)��-[Vf��ˍ 8�G�V<�ˇ�~R
ʸ��Z���_�9e?��Vf<�Brʡ�ϯ5��S�zX&�{��W���i�
�Om3��Ɖ��7�G�f%��Ag�)>���9� ?�=��E��
�%M��V�Er�[�G������s��6��U7����C�ynd`J`V4	�<|h�AG� ���[ͼAs�,R���\4�w�q8��FmEv%tI�4���q�%�%���`6�11�QCѼ�!3b�d2��CzaR��5SR3I�mn4�7���D'Mӹ/���,oE���A��̐�;�9��V�y��3��i�_c*킥�|������g�C#�V�ǘ��K�����S�ŗ��Ӵ�ͼ�Lim������ņ���#pN�{���|n5�����މ����W���F��1��7߉��}3���r��a�^�(��p�e�������U=Cì�(����7��ڦ���Ż��v_�&������~_»Ʊa/�K��T��5�q�耜�@@y�i��}���Y��yl#��p��s�Pg`���Á5���s�y�H;�_Q�T���,��あ�F�Fu�{[����5�B��Пuoo����$���8e������`m�����ب�:�3��D�J2�j_s��<Rf�����$�؞i�P=Q�TU��u��KdT�1��q(��-^0�ߏ̴oj��H�J�XC�dM�t�^��"����X�*���8#Ł�{�Er�:�l@�`h?k�9��7�"�\����4���>�S-G}๛X5�C��HgB�9���ws�ּ}�'́)�3l�0��{ÎUϒ ȥ���Y�}%�)S/YDP�?�����y.���F� 7�pX%�Es{'
�/�	�����y�^Xk[9����c�.oA�^���h5A�4�|��A9 �
�bE�hy�9@�{oC<ɕ���{���.��oY^׌���_~�l�x.�����R"��lH�!E�	����vW������ˋ\{������
 �O�SO6dX�+�!�q����2b�j��;e�� ����2ϝ�+'����ʔ0����0a iއч|����1��uJ_E��YO�:��_�Я;u�Kױ�*?�C�G��[݄Fx'}�6����o4#讪��2݅��W�b����%:�K2�2"�Pj�����d�9<�Oz�9w�>�9�gҧ�\K��J��{g�T{c�C��4L�������&1MO,"FcP�G����}7MľD$�X��Zt.�-�,~ �*�4�4����\�4&Żs�]$��ԨS	b�\C3i@�/Ѡ�ˠ�H��m�Asʋ���߻i~Am��uM��F��g_��vqV�U̙��sz�VF���2���_RGr̜5��GS���	.#�sT��}-��_�C�=8�ύ-BJ`�?�z������!�
�m~W\��w*U4�֧��]��O�� ����|^�l�3��S��+\Y���.��-b�Z{IZ�׀;���'�&3��F��B�8���q�����A�������Yݽ1�a���E%���L�ӪFiϏ�	H�'��e��f�z�����Kt��fO�w����j���A����/��4�O����J�z�͛��m��MG$$eT[�i���$m~:eH-��ͤ���]9��>����'W���;SO��z�����a3�¸p�[��Ĭ�\Γ�@{Ʒ��~��'���Qz���� v����lм�� ��Szn��H�܎��ӤN�x���tn���ڶ)MX��|r4ys�[Ш�`R;��ժI��'�~�j��Y8���6S>άAU?��%̀���0�(;eIVp_JX���ݝT�ֽR�R�q�;�)1���譪0:Ʊ�4�;���s��mf�$��.RF�D�9̿�7b{���C��ov���O�	\�
���Ծ���V{�Ď4��ˁt���Z?�jw��V��5�s�]$��7e��s2�yÑ7;�ր��sj<hh�.�l+�u�Kn��P�(M`8�k�Pn.�up]���=�S��x٣)p��&�zw����R�,k�u���$8��/�o��ܕ���I��3��I��;��ǚ�f����<����Q��s�xx徽���3�]���=l��ث�Z6Q�ɤ��&�JSoS�w�%j�����eľ����T�_n�{,s�j�hq�|m�s=3¹t��IC�����%�(�	��X^��c��	��sf�A2��_f�K(c������L�F��"⟦%[��8������4������Io���o��=��@L< �#�T��Ų=���]��5kJ�n~3cl��<	s�7���=Sܗ�7�k���VT(��t�~#�!�ƃ0B�}�F���Dq�i��5���Db�J�� �k��:+"�����rF`cԕ����e3���.�l&�*H��c3�"�Ӄ�JTXKF!%�Y�/��p�byP/�B�I��l�B\�Hy��x�NA�Q�!�{ië��;���}'TN�ßX�*�Ы� 2��q���Sp=�>�K��e�����S�P��O$���Q��W��)�$!��!Q`���D���Fu捼�2�=H�������ϒ[���1������kd 	��qj��`����/�
�x<�EJJ��R�S����7���;B�l���9~����cA-af�6�:�X �CXٜ��"�1;��D�)�9R1�p�g\H� D�ɫ��5u�;���4��zVE�U���_���O��E��E�M!!���R�xe��	�iCZ��dp%��Dj��w>h�~"�닯z���k�]�CbHLĞ}�r&U�^-�i0�7���Ş;��lʊ���r�Ql�����U�>���"������d�"X�&m�$�^�>����6����r����z��~��5�`�����<�Ui�� �8�گWV~Z��e��l�ӟ���yD��L\6�I�[���e���o=�s��i�ϱg�A���5Js�|�D���=1+��N�$L�6�.�#����m�ߒ ����C?~c�ch�2:>��]����W�����A��b�ns�ؿ�Fg��*\~��k.o�wၥ�r�(�P�����A-6��)��ߖ�{��������;|xA�%����;�4�����~Z)�ڮ��J���Oaxy�J��:É��o�7r0���[�7��%c�l����a&�Ϯ���NÂ©)��
0�vFWVg�՛�Y~i�Qc��r�o�n ���!�����Q�Fmh�����t�
�bU��n��Emk�������4�0=��}O�,�؟�e��hƎSY�I$ǼQ��fDG��n}8{�w$�x�ɻ�����`%j��bU��ӏ��ψI.TP-�gj��Z�g�[Bg�1k�>�G 1l�A���o/Rz�.ҧ�vT;�pʒ��!������1m��'�$�'7��	�m�;N��p�;�׍�y�c�x�i!��PW��ׅѲ�D"l($�i��4�Ou�"�]|=nc���X�*5:�U��tu�˓��6+�a7���v�/��-Ȼ�i�n
��a�j�(�f�ĸ�ǄqqiRD؛� C� �a���}��]�*}swvBH�u{�w�y�s�j�B]�79ʾg��OO��r��PhN�*)���    e,)_Z���d�I7�d��W�i"<�/ڡ����m�s��-~�/���w�.!/|�g��'y���|��]4��*���$��1�'J�&hW���]��(�}pH� *�OC ��Iq���7���3�S��]~�'�����@4L�n���KB��?�A����Q�N-��ۦ�� �	:d1r�)qM�Ȏ-�W9 ���Ic%�y����2]	p�.1�����W�6+�f�^QX|�K
2��Z��,�p��軲�;i`|���8=cbt��:nJ��0\z7�v�ēd(�)L[b=Я9c(�T�m(��r1p�u�
��W!`�ӵ�fK;Y��� ��e@��4笊x�22�3���Y���bӚ��5>����4qP),��cK����پZ0��̮�6~��h4�H`_b|��6Z<��"�K?p�Lb��߇do1T�T����I+��U�
�����N.H���Jܸ�e��W�沭��#�~�7���������"����#,��O�Uڳ��Aa/�]\��\9�2j&��$�j�ـ�.'#��A-��wԫ1h�;{-�>�-�nE.���P��z��8�l�Lc����s��M�z}k��z�`���M���4bC�����!���,�è+7��m���[���.��oWN��ھ� F�b\����7�:���{�A+�����|���(���ۘ"^�O~n�o4ޫ���bZ�A��[��:������9�MS��cBP�ȗP�eo�G��Y�;cyp��nP!�SU�T��Y�B�6m��f��FmD�d�a{(з��\@�J����F�Y������29�wU�)=���Hw#l�jx��e�-a��@絍�#'%(��X@+����*�Ɲ^�'O�B쿌N�rb_#�'�~LU��aXa�'�R6��.��E�G������ߞ(w�SB9�(`w��5u��9f�q7����E�Y��m���6��m���_��c��PG���Y� �;�>U�Oc����u�P;L��ޞ_K�:�8���{����*b3 �mF��vo��މZEg�(��hj��ҚqP�Jro���	��LVl��9e_��?�$!w���h�cK%N�t.B�����@�V1��k� zb�>}t�P�xJ�́�������wa%�9?t����>a} �>>#���^���k7�y.��4u�G��V�|eӄ$�=݂v��߆ds{b�1C�ܢuM�J�x��f�����`����[����1p��;t�|��c��I\�e6��i[(|����^�'y4=�cn�@t��F�q��Oe�
ǩ��!����LN��V�`L�ƭ�;�{�eӨ��Jjډ��_ܼͧ���� �k
wZ9��_���#��ud�|��3T�=X���5L8�	o��R���y�&ðZYBy�	���lqƬ1�[��h���W��U�Uk�����?>�E����f��_G�>�2%Z	}
��;+A��U�ּÉvl8J�/��J�G��Z�7F�,5n��a�j��
?K�B�|���m�O1O��HӵQ;8x@q�I�X��g�h��=��Uf[R��R��ޤNmh��6�'��ң�zY��Ƴ╯3���]3�b��)�����(ǰ�ӹ��ys�VET��O������f3�2��Sm�g%Ɇ�N^�T��6��(�#)���cNh�T�xf�*-C'�ʸ	O���:[�R���	�52��j�	m�/X�BR�ƒ�Wp��4_�>�p��(��������~����z`����)*�(m�Z!c�#���"���Տ)N��X!~���o�~>X���y���rHޕ$�v��Ә���UKvYj�b
;~,���4��������E���G����D�t#�DzN�w(77!2f6��O�\�v,�4� ��(}TX������RFQ���qc�=�|�s�Dh�#-Q�[����n�_��DGL�f�<���Vy����N�ޜ�դ�o�v3?a��ȻT������D5���ÝZU8Ā#��/����CS� ��
�Z�(�� ��C�ߕ/�Y�ӯr��@ƇDն�~��/��2��40�Q��:CA���m�M��=�����lJ7��|�>��R8�Y�`̋����7l. ���4r��OΘ>7c���Z֯�R
m��(�v��8g���j��?H�	�y��A��-S�/����QeF^	�J�D[A��O���3*p ��p�K�-��s��xx����1�%3� ��1ȚgS��MV������^�I���5Q�(�'���|o*���|p�������T�I��c��U�U�p�������=���Hf�Aqy�TH'@0b�
��[{Ǖ4���rl$�R�C"��1!�z�KI4#�����X�w�^r����{�e#��,�u��w���I�N@�	��Qm���J���UKurPP爦S5��jG���?�tB&on�vVv�W�v����L!�Z��0�.�"����q���	���v ��CPl�`�
~(;0�0T�_?�90��A���o"S%�"Y��d<.��ƃ���IC�ʯ�� �{zz��u�RC���)����2o#m�\��\E0k��|_����
V3��M��߬�͔�YQ�7�pQ������1�F�\�I�T!�L�P �u0�핀�!�+w��G[i2���T�v�-��{��1c�p�Z��YN�m�Gm���V���2{ifC~�]ݾ���N� �o8��f��>غ���J�t;R�|㇔Ŷ_�b�ȷ|�~��*fO�>�����\��Ҙ�v�J��%�P��#�Յ�+|C��<�S�B[Y
S��&��Im��=�yJE�)�f��`���*��U�X�P�Wv,k�ҿIQ+A	G`�]�+Ǽ�-!�	��G��,PXq�ӦsE��/�#�.�ޑ��Zb_��V�ʺ7��#�㯤�nORe�~`����O�a�)v��nh���"�cՅ|{�4�Hǖ��D�5&K3�RQƖ�|+��t�7��b��'�������4��>��$Rzo����Z`�G�p��x�����on�~��{�f�X"��H�K��A�_N�)˘�_y�X�E�?�ͤ�%t�0}-�ye�@v���6�����i�����S�}�#2n���" ��%��ɺ� ���fRFˤ-#O����\@nZ�$�AI�~VM�u�����!I}��F_=N����/��MIsʉ��5���A��/CL}���.�K�ıC=�"����s�. ��]��6��/��G%�9�u��!ဿΎӚіp6�p�s��S	��s��XE��rÖ����P;��u��l���>���E\����<0p���^��$�_�i�a�v����_eP��jE�M�k��i�È+�Lv�㫱��3��,|^
2��SH}qs]٬�E)�L_i½;�L/R$��!�	ܨ��xQ�w�4�M��&y˨�#�u�\R5)����
%�|����͗�gy��C����/_/7B&��--O�57��_9�*r��ʋ?>^��t�����l�ˠ����߈�S:kB�����))��Gh�;cm��
�%�^c�2�<��.�o��1H6�HsB��'�߯�>�l�[EA���W�?��f���ܚ�#����Ľ�x�̱����J����q |��~����6��5�~>b�1v�rR�*\z�m	HV	��<�+b��Δ0�d���v�I��yߋGs��(bY�d�Z�0"f��CV�-���M�GV����7�"H�r�$ @Ϥ����z�آ�,�d_��E/0M�p�����̪rK{_�%�N;6F��O*������궍F�io���d`�������9f��b^5�cbv$	�z��j�DY��&������aJ
��<|�-����x����SC�=Q���9�!%���L��X���NA�?�p�Щ�$�d�/��֮��c� �j!U�_�_=1:�,�&������3Y�E�ffT�U�#R��	4@{{�<�\���~W{r=i�3�ٱ�;,���#�u�	�[.�3�    d9)�:�r�Xh�,j|Nx8ⷄO�w6%��t�fY�V�H�!��)�_�^=n�Q�O_sf������A<J��F�o�5��t]��ˆ��|�!�ٱW�2�x�8�<����n�S����H,k@�an��/��7�.����i���J{�=�U�3RF�yo0S8N~�	b��x�H�,���b��-��K�A�W�rCD�eGȒ4�x����q��5k�gT@X��ѓ��M0���5��Sbb
Ʈ/eT�gcS���TT�b�)Qr��K�h���>$�`��xϙX���/���Z^��/�5������'�t��)1��5nd��:
@��i����B3Q#Vg65�*����ql����Y��~<�\�{���6'Σgr���UvVW:mhH�o)�pe�E#�����0��!�0���|�Tx�G~y~�<���+���+�qL1��xۿ�#oǙ]w (�5=	��.=��V�2Oϱ߷�L��#���7ٛZ�=kj�����e���-�Z�U���4#T$1;�a�D��$޺M�bJǫȧ=��9g����̍�nZ����.şv����a��;^e���A�����[�J
�b��ȭ�0��C�{YI6�u
3N�6�c��m�*"�SU�l�u�pO5�(�|,ɖ��ELSb �}B+[b���q�/�<�mwF
#�������m��)Ƶ*��6J��[sG��bKxtsF�J����k6�}G�k"��2��9}��d�7��|�\(H9���?�:���$��6*�5d
����|��Yg˵��Ӓr� *D���Ϛ���5oGY��w־�$�>`�4+	:���c}���s��d*~u`}1ћ$�K�l���j��y�K�[�k^te�j�WWH�����םP���p���~/�+'�	3�o,O��|�dl�t ��gL�1��-��-�$�;~�x�N��-���Qv`""�Td�k%L�q�?�@���!f������U)�_i}��M=j�g��lmh��̐'���H�3w)RSz�e���N:>����h�-����d爫()b=�p�=�tq+;�Qm�E_j�i!���l�P�,����y�%��>ކ/�l��3g�~�Z���B��/�o�qO���i�<�7�g��暮ʶ������<�����1@��=�O��D��3�[k��纲U �i�d*b�n��O#�~R����[O�9��n[�+�IFB�v��Z����dK�'��J�ѯO4|lT���y�֧9��U"-���|Ɉ�f�i}FW^��?#fT��c��"�$�hh��`�7��MU�A���˞1``c��Ћ�ئ ��l�[N�
�sV�h���1`9k 0`SfP�=�/о)�E��#-fPi �%�_�AMĩ^9���>Ymi�i�A�Xk���*�7?��4���v�cޜ��ʠui�t�m
d�t�7���G�լ�pw�1�r
h�T��u�
�0�&9��'U�t JOw<�� �P ������QB��P���=䷞mi��B�n�bMPN�=v��X�e����m	f�\��^��l�a�Z���}P62	W��Fξ5��^�	�t�Z����U�n��f4���Rd�^����:�s�Vk>k�$�k���Q���S�������vW���e�"�`2À1��(ĝ~<5���v��n�=k��0�D�����-�z��kw��5�Go���	4�f�6kC��a��=j%�/�� �my�j�� �ҔF�n�ꏧM��2ʜ`宽?�t�U��X����N���ܩ4�$w�o(���|��,4*Z�7�T���{I�K��4v�ņ��P����Տ�)���F.�8���K�����mm�F��g�����8��7��ZP��_'�V2t�ܓ��F�wBؤ:�mЧ���F���w�u�+��}�J������$M��:�Ⱥ�+R�#*����
]�OLegy$����/?���#n;Bu����1ў��:_^}Ά��٪�a#wj�t�em�i�t�@�%�g@�����l��'���ddX圹����<X;�jN{���i��þW�c�@�(<ơ~sv��I\�'
uۻgp�ea����p�H;��`��24�[V����o�_S����b{�щ�i�c'k�O��,lS]@�׺c��|2��!�#��{��U�NP+7�X�W �MY�z�5H�b�B�h���z���)�{R^���.1i2�7I�S�w�:��5Op,6n3)T�����p��X\E�6wR�N[�8� ��{��%+1��uӀFg���]飳��s���ޔbc*�_���=��@\u�V<Dw8y�OӭJ��#�&��s��L����y���>ژ��`O���1��Z��!1�I�,"�\����q�m��۔�a)�{]A�䑱����k�u��Ę+��O��Gf���^���|4��՟��3%�
�^Ó�[�qŞg	^��,�vd��HP�hR襫"o.����ȫJk���������U��f�����������Kq�H���м�fm�4�{�2$?y��q9o��Q���d���2��;h�ø3N��Z�+������r��#b�S[ġ�~��m=�o��˽�z7�H��lfD��e���9ҹ��	FO�at����KR��n�����Y]MGz�[�o9���Q@Vl:<�'����X�
����%���`�
]-�I?�/�!?w�Q���˿��"�mEJqn��_Y��D�N#&^3ů����#G�����j�"tJ���k�9��ү�ۀ�h/\������6Jmv~�r�k|��lV-n�Y>�\Z2��������|��9܆��8tQn�'���1ƃaH����ID��`�	�8Ɋ���f�Y�0j���18�Bp�	�T���P�����*��hq��`��=��6�t��/�ٽ�S�6��
#�m��Z�I�n枡vu�H8�.A8�x�k�����4�Q����<9�D���Tv4e�a �3~�C �A��������P�g��'�bU%2Wŀ	��x%ug9C,�%^&�1B:��f�� ��}�h�\�K�Voe��鿞%8^�:�Zǽu������]�c�����n���@vF���u�$jYŜpA���*�G�o�25MBW���&�(}�%��������#����h]�6��.E����5yr��ڃ�\S�� �~
�Ήl�i	�D�@�[I)��K*��ʱY� ��bcw[�b�p�r�׬:��D�I?$����ֿr��YC�c[�8)s�k
��MB��g�iMp�e��̩�
���m�w8x�!��_�<$��5uW�������nB�[	B�{�N߹{�ѿ�$UL���r�;"00\�������を>v�m#������["��|T�:�H�`#�z���f�K�Z����������"^�	]����s�<	�̷Q�ߟ���P4]��/M�,"�l���[�ⵓ��^M��%�e�/ 4q%V����Zlt�Bi�ל3d�`E��#v<}*�Y���=G��A��ף3�Y���y���E��j+zV��m�����/O{j�Ɓn�f���)�Eh�3c�,�M�f�4�>N��R�{Ze�� =��:�����X�5ޚo��Y\6q�5��$��L  \7��4�Ia�b(�z�?�_�R6��Kz��/�2���h��H>�!`l>W��O�c3��}}���c5V����M�a�1R(�^by�o� ���4Y�C��LW���s2T�^�T*Fٷ�o̟M��wE_�Ȭ��?D:��g�o�D�,�Xu���i9��p o��Z^�mn#m��ٿ8�#ί��i���@�M�aפ��*�!�.�R2de�H�<T~�"M��C�`�7�=��uN�_$i�&�)k��`w#���gP�'�t�-�9��i�Ypp�%lz޼N�����)�MC�k<}�ɺ������h��E_�f��ݡ�U���5��CA�NYcs;߲�d;��Bk�ll�P&?Q��GC�C:z� 2�D���/��^&Q�Y�l��/	�}M������� �	�����ſ�    w9�'��|�AH����ϯ"@�$��	�4�d
��"vb�=95�;2y#x�&���2	'��5�-!BC5��sK@xċ7�v��BI�`��m�vuЉ5V/�����6K�@��!��@j0P�k�t���B�� b���2��f��P��۾���6L�k��1����bγP�Cn�*���ĉ�Z�u��\�I��hk
�9�ʕ�}�k��У�%�$��Ω�G��:7]=���W<
����a�I�V%f�P
�XU��U�U�e�bE����oj��{2'/����A��i��8�q�����;G
��v�@ �y�
�08p���8���o�gr-����r.�s��� ƫӴq�u�Os*��ͦ(}�7�+��s.����P���-<��;kE;5�1K
ʸx�[�=���Ed�9��=
�ZV�i��"�g<�*��[�r�_���WP�0p�({����o���qP[�N���"C�Ɵ3F^]oužS��C�o��<��
 @��A5 -�C2ۗ�"[:�D3E��z-.�QN��ǝ.\ʾ�D<E|v?yk�:��w�܆6���o�U���&���@���z� ND�˦APAd˟�ψ�*��D�Q�K�*).7S�n��*��#�pő�Sf�ݚV�M�h`��*M2>�˿�3~�6��X�[	5�4�q���bO�5�.P!�=R	�d��U��RcW�bY�������kٲ:��(�M�ߔ�ױo���;m�>���5�UB�	��3!j'��uD�W�|'B_�+�����8��� �P��I�ɡ����8`dӶ`s�n:?�mD�E�C;��/��oZ�U?��B�H�ZR��������]�/~�)�׶t�V9�y(����.X4��:����O:��q����q ,�r�f���;��f�����]o�@�!d�>�W�ҟ�����4�{(H���cfE<�3�O��6���7k}���Q�������I
�ǔ:��:����,�EI���i_v�y�Ӯ� �XrU�̳s��b(e�P׸2p3.�uy�]�FAE�=�;���'�!^<È��L�^݄���ʟO�<�/�f���#;~έH��ا�ڴO+Q��Ӄ��q,���(������@�����$��Q(�&�x~7�)���
H�7	GvFӘH�7��m�k�A(�\9�6=Z�C�[�3Fq�l{��xP�@���_ي7���dcȑ�a5S,#Y�ʀY��T��5=35�]Gʳ�	��-��1�:9���;k� ���D�{�݇���@En	`���1�v�14�U�A�eN=���7(�>�S_챦��:�;&%{����?:U{:���u�������<�}P���Oy�nNDnR����f?ղ�5��'�����$�0v��"l񞩒�梌�>�����-��ԧ^�/\���b&6��𗼜��9��ў�#�W.{�?�W�t�B�U���È��j����[J$���5���f��3�v��p)�S�8F�/��,�׶�e����X~�>�ٽ�w�8V�/�ﬨTHš�0��o�c��'M������7p;�������:B8�[1,D'5��QX���RQ�L��쥠�PC/�H�X�,=܌xU��!���-��h�ֆ�N����=��'�7���=���ԫ�s�͝O$Aϡ<�߉��`c��|���y��~���'
��ʖtt�	��檦>[��.�$J�ߔ��J������E'�����V6[��Y>��C��!���B_D�j�,l��k�%aLF�B'�T´���*�`�t�������M��"X�Z#�&r�fC��B�}���Ħ��4�<Sd\J6+@���n�y@�R���\͜Ń�
;�'J�X�k(Ѷ��%[\zo�몗��I���}�R��(�^����1���}Ν
���t�Q���ꬅ������tbRG/�K���Y%�!��2� q���aآ��JMΜ���mfu��?�uq�-�Ya�w�mEF��.e��&y� ���>�۵w�콕H�r�S���f��7��tm~c�gf��Q�f�6������HTP8P����4_��Q�М�Ik��� "���a(k�w��e;��Ֆν�P�Ii���	��i-to�0XS��VzvzOp�B�������΍Ξ�qq`0M�p���y���<Q�*bK0��΀��@5OB�Xs�*�}���޼F_����$) 6���59{����o.���2�R�v��C���:eP��Y{ߦ@�êk�/�.݋����-�p��8c�^t?��1$����xJt�F��掮������F����g����"��^���Ӟ����������J�D���[�J�����~�zi�;���e�;&�F6ջ��7�,�� �ݹ��qRD��{���|é��)z��kX�y��F �J�t�0�K�L�q��87j�Gy�$ݨ�E=��r0\'���)0�zre��5����d����,�-j�GB$-��G�%�I����>$�"�=�v^G�@!���6f]�;�x��!�{�O�H8C�0ܠ��0�P���;���f3t*�×@�R��e�EK�2EL��;�~<�{ʵ�H�8��.mc�n('�)֯�0�5+�.��n_�{��E!�"H��(�n���o�S&aԛj�����f�������o)�ð��>���{e۟P*�����~ޚ����������v��.ӡ������!�c�=8���"+�J�	����Z��!�����#U�*���Bv�[]y��gB���<0��Lޱʛj�jP�B�R�4�{��_��Rw�����M���)[e\���Ps�u��,+V6�0A0�����hX
��o�[*���ޛ��J�Z`߇ѽ_����	l(��-�{E�c�Rև�zy�Ғ��rd�'
�oeUz=g#�$$-�7���C_[�"��J��ǧ�&"F���܂��@����d�8�i�Y�Y9�&`��6RRT�v� ���I��Qn)���Ϛ�F�8^��N#=t_][��D�0F7%�kb?"�|������+C}qN�����&�gĊP��t i���O��-���TyK1�qsO����H����ĩ��AEmPS�~���lt�X��(ø��lQ���}&R\������o�w��hb�ƈ>Ro�o&�AV� 7�!��׷��x[1z��!�������%k�.�"��s�>Ƚg�t;��V��R���0�+�������<�����b�{"�e���cj��{��|:|�I��e��_���J���X�YC;�s3YN��z\#�1��Pyk����	!�z36�W�YO��u.}R�:���<B��M7H*Pn���1�᢯�φ�����ʚ�):���V���R�zG���%�^�[�_,Λ��I%��b�>�-BU"��0�Z_����&��2㫩�b;bD�٩w�����
���� �e���Z5�¼!��H�͊$Tm ������#�Qq�D�KiR�sI�o,3Go�Άϯp����D���M(��TvS��G{h�h�>��cDz��&5tָes��������Y%����x[J�`CI�`0f2f��`��3Ɩ��w�?��w����t�DF� 6;^����T��,�[o�~�z�4gA��3ڢ�)�;�%މ&:c�N��%���
������=Pq�M�C�w�����B�-�5J��L��p$С{Ǘ����s�A0M[�g�>���硼���Hg�A�1������z����t2���Z^�+|���:�͵��L���P��к��m\����#V���$��+f���o6E1��~T�s�V�d����q�3'����0aW��J4�#�$7K!e=lQ�e�%���o@d=�6��Y��eRk����p����\|ͼo�Yf}�qVax�]��@��K�RF�9�X�<U��<�s�A��r�яK�b>/��Úg v-y�Y��� sʷ�0mC��9���d��9�>|I��Ĺd#�C��J>�!��u��y&Ewo� m  n��-]�^��Y�ux�TNp-�҈Vг�)<�R��0���\�K�BP���@|�z��X3j�خ�s��ˋ������?� �����\|���m��S��-�:n�{���|wR��Թ�7���'W����]:՘T�����*}�VM�@��L��yWB&�/��_�8S�淯��3���1t�f��**�)� \�w*�9�!0[��c5_YM��hK��:�=H�oR�%�(>,�s�.Ы�A��baH�Kr�����|M1똝�+*���-�<�:KdzKKI:N(��39s�R��P�(6�
��4�K��ŵԟ`a����Y,��J:�fCZ� �x~��r��YY���x_T�a��G�)���(�5������9 �ÜO`@.M����&v+*r�C�\h�����^%�褐\��'��h��\^��fa��7�B��\C�/���B0g��x�	��.��52�����oD��H���^	Z�~$l�`X���E\ɣ4E8+>x����n����O~��j�:7������)6`-@�;1�<Z��͔e�T�P�N����̄Z�\ހ�H���?h�������������5`��'7�t��YL��5ΰ������͒���ӻ�ug �ee�g��	�{��6��:fB���wƊ�� ��VӾ�	��Y��&�̏��'��;[�� �\�F!fB;(|)�V�����w�d6�N0�@�/>q9��������)OZ� O����cX%�.MCȁ�:)��K�T4E�4���v�W�^n��3�`E�Z�5�D�����͔���,)-���`7!į�l>�>p�;�с=>$.�zG��,i.!85�1�Y�k�����%-����(2+eA�J����Vx��l#�J���_{�����&�j��X�ϡ���꾱� n�/�U?�m���oi�y\��*]I4p����3�,�b�oK٣)�q��%1@�q|�Η���e}](���y���}�9�m٬��iK��K8�gE��{�4f]hT$ �͛ϐu�0�{�<-z����ƔQ� ��&��eg2��·�4�]'�٬w�����e�wg�n��f�MM�ǚ?�z�9Y�������=�k�`����p�\���~�y��x>���M\�a��(��:g�}5Ԗ<�G���劣͚K����ʕ�'."�� ��,�Ƀ^��u��i�Ruܹd�*؄�P.�G���J0��Է&���[��ސEpv|�q�����ln�e�4ni���=cF&{���y�9�1�� �*�<�gET�>st��IZkx�5E��m�K(U(�0�+��,�Qx�o�G�J�ڋ�/��r��R����T�c[⃘���I:'�t�7�^��Z��)
��ʽ��^��Q�[�kȋoV*��#i~��5�4��i{��KZ��s�';�����Q���@/0�@���K���|�F؋O�����ԶC���Ut���w~�T�硇`�N�6XѸ.�b�����F[��`��Z��{�m��w�Ĉ�S�\3�J�;�:0g�����
z�噳�`��f�����w���.Ѩ#
S����u2!�b��
� 8�sH�uo�2�e��s��P��bbJ��ڮ�O���4��1���)�[�=9V�G[���I̧���D�Z���@"w'��B%�fS���م*�����P�>�Y+Ԯ;o&틺n�$��MH���S
�4nt�M8�AX�iU�/OrW闃(�	�k��G|��^O8���#�[����6?��|��mFA���>�����S��<�V��A�k����0�ǟ^7󤿴�-�]�禼�Ɣ�tFq`�^[�}��e�oDc�>lI�������EyQ։�&��}�O�3�x��0��!w�0��Q)�9vfb���NK2@ʽ#0�C ��-�e3�p^L��ZM���y���b��-��a�Hm�=�h[��V0�Ο�v���5
b�z��&��z6$��5j���s]sx�|��ʧL �V�l.#��x>��vR�%tg^I\�""�k�[a�H�����݇�XH�V,���r���@�>��z�U�{e�@�W�y�KY/i&׃U���:���M:%o��Ľ{7iz\Ok�X�Z�5�ĸ��r��٠�u=Bҗ!(]c�=mc&����"�+���Dw��L��&v)͹ ��z�1���A���;��뽽�촸Rʢ�x��y<a��>4�ik��հ�D:���\	� �,�IsyA2���,���z#�BΘt뱃�'cd"���)-h�"���#-��:�B=_BJ�I�_�0�^�؃PPeܰ���T|�S������ᕘYUNP���8Ҷ8-�{hY��P����Fr�q�f� ���"NM)a���4��H�y��>��`�2+�9�s<�嵰�xV�!qpn!$ʹ�Q�0$��b?w:M�!��N�(E�Y�U����I��>@=5H������W����{$z;Ӆ����<��rb�bj$ᔼ�bۗ}���˩��)��mQ����@qe����)H�"�>=i�]�w�n3?��5�fJC{ћ�j��_�d�`��j��5)�D�tExz�²�F���G��iэ�6	*�BO�k���'�p��<�X���J�`ͮ�ݩ��/�!�5�����},b��is��s�����L1�e�|L�������n�an'�V��G�\�<�~�����۱���L� ��h
�������i��֒����=m�9�UsP=���=��}}�{��!��= ����3�}ձ��h��לd�~�����A�����ᾠ.�Y|a;��g����Pk�h�5�o#D�,��!iPns߻�9��ޑћ��<cg��޵���4*�*�i�~��8��6�*МV/����+A����R��0���8N4s��x-a��j���Z��1�p���x��&��c{����qd�V0 �6j� ���a�w)`R�٠#ƅ�Ε[P5�z�<S%�i��wErh�<j#���Y���("�e�=���2�st�r[�҇㗿g�	$S�->��c ��R�xHJ��E(���(.e���R�W#��ꚏ�m^|z�`�S����Ov�&EG�R�P�76�b�"�ˀg�~����˥���= ��
Rʔ��G1����og�����g�ŀ��K��6P;�����Ľ�x�S:����΀$%�"��M�G��9Ħ��V\�9� aΉ��C�W�?�X��~�H-��\�TzɊ�����R{G|ɕt)-u3n*��qp���I�[AF0����R���r|��D��R|�}*�q����ϸ%�=�rހ�z��Jٲ&=2"�`}���{�eSw�q+�{c%���5_�|���/Iͽ��D����_P�!�|����"�v{�VNp�l�4�#�x�������XUl)MZ��S��G왞R��Q��WNcr뙲b�;౶<����Q#��B�$=��X\-��|�g�H���ʪ3�>'^d�*�Ǭ�c��w�ܓl��8zC�����21r�b�C�6���/�59�O�p��ϯY�+�Q�V� �x�륑|	kN�m�	��ESc��3(��i��3K�G�c�{a�_��K����u�b�=��7hK݃���ڎ��a��Մ���g�9[��������3�-��\G�B�v:V�e�G��4�"�/W����n�W�/v�r�|n�79 b�;��Ae��"p�������wQ�y�C���?O��{Y��t�л��d/+�����[ۢgɺ-�y5ߒe�gU��[�r9���\�פj�����r�s�,����]oX��ɳa~��O�[I���7���y����{�os��|��O]80���������_���X�J      �   C   x�3�42�T64�0M��4�4�4��RFFƺ�F�
�fVƖV��zƖ�f���b���� ��F      �   U   x��ͱ� �ڞ"}���	���.E�{�&��@k���Z�������m<~��dMn��~ =FZ$P-����p�2��Q%C      �   i   x��ϻ�0��ڙ�%��y��	�����tW�W�>H��EB¢����Mp�V���y_֕�6�?�o0�<��G ��S}Eݴ���Р#5��*)���J�      �   >   x�34�42���LB A� ����X��P��B��������L������H�� ��+F��� ��w      �   �   x�}�1
�0Eg��K��dŶƔ.��n]r�s�nHi!	4���/��	P�T���y&��Q��ő�%&��|a���wijC����[��*<�N�L���֠�����]n�6��ǚR�z����a�*-��0����'�����s�[!� ��5+      �   �  x�ՔMO�0��ɯ�ve�l'N��v�Ɓr 6��P5$��ݖ	6��`*�I�6���}�0)*TY�)��By�d�}�-2�e�{�ZU��H�(�d�dN������u�0�����>����ӫ����q�����zr߼�.��:���	Q����7�xU�p^��Vb3���`�L����}%�d!x��
�~����[e^�%֞�	j�?_��NHPl��C��x�O*	
��*[�D�g�u=H!�,�jpܨ����g;�#����uּ�19�����N������&h�	������d�N3's �o���j��m��]� 3�҇��m	�+�]X�D�Z�B^�H(����KrL�5��~�B�����T����BH�      �   �  x���=n�0�g�<1�#����9A�v�z���ڦ�"A����(>J$�0��(#ˁ� �D:��E&ҪL�q��݁�:���9Hb0��l��q8x��o�Y�29��#O��I��R�� ��
X�q��t�u���w�d���MX@n�['�.h5�4exy{}{?�gi?@nO�3ٌ<Ru��sw��\���JW�5�!� �U0�6�H�����qV�Z)��+�B��	�:D�G����[n���v��#�1q;�I��ܕ$�H��(>TИ�]�rO �t]�{�:�^_��u��c�P2�)���}��[�Eb��Zk����G��ǜGU:��gY-�M�U6J��������Ȍ�f���:Qw"L�x��a� ��      �   �  x���]�Z1�����{U��'�YDWp����N[U*LG<r��a<I�q��y�������F�e�۴۠�L�m_�����;Y�ay����(�'��LԂ�(Q��˦�!%�bJ%�4�X�R˒���N����b5dk%�1,Kb�މB�Cٞ(D�G� 5��d�S��P���K� tM��+Q�
.��kү������y|���6���ҜI��)	w$Z�S����PX)��
�Q\������7��u�
ş>>�6X����U�y?����ݏ��X�X���=V�;
2�F�0��,���z�EŊP"`\0�� �p�@���¸�'k��=���`�����`��e�<+�G�i�r�@�V�0�bA��łL=#��(��������F���\,����(�Y��W� �g+�}��B��tW�4�UT*�O�+r�7���)�D�z���K�z��������VWZ�Z%�4��J��k���Q//E�.�h�eYx�˨�p����0)E�+�R�FAl�dv$Y�U�z���������ߚ]ni�4���44={���x��'
*��|=Qd��q��(���D��x��'�U
m�k�Y-�.�li����1�ie�gi�9���0�R4�R�F�<��*E#an��x�K���K�ɗ��[��Oǒ���lP�0�>4Dh#����d۰��~�>4�>;j{h�s��J� � �V�s:a�	&����!X.�Ġ��A���T`�4?0�i�IT҈��I�AL#90�i�4������>0�g~`�r���
Zh���|`�XcZtFmly��A�!���J�P�0�	�5��$M2iJ�X�@���C���A���7G�ä#��y�u� �țhF��yI��bR�}p��� �9;)Lq n����
������ �@�,Fz�>,�Ju�i`��	�W�q�OLϲ�����1��80ț��q5�r�Ϡ��y�����l�J      2   O   x�e˱� D�ڞ"}�s�"��R�4����Q��wq��8����ľ�cο���g���1��U���)M      �      x������ � �      �   P   x�+N���/�O*M�N-�,F���q���(�Y[Z��Y�i�MB�ՙ����Z�@W� s�>      �   X  x�u�]�G��gN�����#}���N���ȱs�P��1�Ջ�����UT���x��m=\x[ϧ벱m��*��t5���9`�c��C��c� i���#�ϡ}��c�9N�i�V>��O��X#�!�E�'-��2R�ARJ����X4�����v�j������Ӕ��*�X`ւf�4�f�X�`#�6��U7�r�N"E#�^�V��0b�D���ʅ������sJs����o[���w�T�QMC��b�%�
Z�?���g`���2�W>�_��ٲ���m��4���XBQ�i���L9���XC�ka�X�D�4����Ų������铁�47 �0��Ґ4b�PGJ��>�V�s�or\��h?2�<����QFG�!+B�=G|�n1���~_�Ɨ����)rN�bS��T�Y��ZA���A)�L�O�z���$R�j�f�BZ:T��a�"
O$�5�1��l��p�������]�E}��o?���V����`US ����ͅ	�V[@׌��]�������Em��^�8�am���K��z}�GIh�v�T>j�티�<f�������������sO���b)�WL�|�$�s��C3{���sL��j���|8�|Y����
���W�!��׭i�R���$lٞ�=�~�G�;��eW�l�Y�t����"RB�w�-��]n��fh=x���*�j�s,��7�\�H��sk��ŉD�t�r2'��'�ؿo��r~_��<Q����~L%UIi���L�G6��>���;UQ���1���3ǽ�/;�e՗G._��/�=��O&��K+'�U/ů���}���Ѵb���*q��i�� ��      �      x��}�ndI�ݺ�+
��<��� ��l%m�VBv�LWW�;�4п�X����1�@
Y�r2��{�;���YV�]�r���St�pr!��1O�~�����k��:�e}��~��ϫ��׹��24�N��9|��R�s�J���Zʧ���������<��|����'�N�J�/���F����S��O?���?��|	�J��_,[���·��_��o?�߅���I3���Ͽ�/��7�?��K���L۾q��������_���������轍�������u��^���>՟^��O���u�,|��˗�����/��W�ӗ/��ӗ��_����N�y��~�8Qb].�����\�V�[���m<���1MK]K%;��]׵��zΫ�#��Bo5�ժh� ���-Q\�:��T��8}��������f�x)J���҅ߢ|
%�w�1��0���qT���4]k9�Y\��l��Ә;�1��{��>�w �C�7Y܊P����S��C%�Ze�Q7��K�t�ݮ�����E�n}�cز4��Mf$���#�������1��#_����/�ؓ�5���8BF4jv��^��n��⎠�L�F4Jsy���H1��Fc�Dٗʡ!K/���k�^V
�m������9����7(v�7�B	�?:��\���|�L��wAQz���~��eW�Č����
��٭��l,-��xScceW$�����&�.��Խ�p�"M���(I���jIe��W�u�e��"�&�9^���7(�%�4/^f�F{l��=,<�܆_�P)���_Q<��׫_aԞ(��\Љ2�xS�*��
��"����T��	�e�;��/i&O�c�c��`�P%�^3"�)#/Μ���"��8�e��u���|��e,Ne��c)ˏݵ!��{:�I����W�O�^�
���g��R� g*��M㄄\E3�ȑ��k�Ƞ]k#��'b
)��8<"<Z#�^��ґ׮+Z�f$Y�A�SA��r�o}L��
��+R	R"g�t �.�u����n`�#@ar\�Ĭ�\��A6p�}$�y�"bڗ!�� iԧ�s�5�9�/;��������tu1�n}��<���"�Dx6��`!҆XU��L��<[�	�z�zBZl�]���l&��9��k�)yP,e�]�����k<�U�u��c$�/��������z_Z�kX�v3�F��a���d�Ix_�i�<wv��	C2 n��gٍo̩�a,����/���=3č�N-a\A�<�p��c�m7�>tf�\�όy�2�#�,�T�j#G����Q���h�	
�)$l��zrYY\�K�_Q���+!��!	�8��J����C?�W,�����n}�̈�ޝg�5��p~�(��D$h³�;��a�~p�ڨ�A�o錯H�q�5C;��w�5u�7�:2#�:��+��h��Hh��4aB�k�6���Ѧz���Kk�z@P�[�.�%.X�Ȝ�s��f�C.C�ywס��+JCA͇��H^��+fˌ�*H�W�mjj4P�S����}�3�M5ޛ�R���2�P�0���?t�XL����Fe�r�[=I���c�r�o�Z;�ʤ\��Z�g��<�:���L/������1ףR�'�xeX����|Ӡ?�S�N��$�v6��wދ�P8H"��%�L�Gnk�x�9���[���T��jα��lyIRW<��'J���z��v��#s*�zwN��v��2/���E��@�;�)�q.���Z2�΁N��)͠�Rb��uȩs��V�I�g�_g���g�_(��h?�R*��/@�<��(Ɵ�>�;3�	B�9�F��i�JL��]��h�p��G,��֑Kl�C���6�fM���i3!w�ﭵ4�������D.�7>rZ�Lýi��84��Xl�o�e��!OR���g�s^aDx(SlL���]�����!��-�Y*gDFeWOk��]��@|BN�j�ӷ�>tZ�\�;���b���M��DZL����<w��i��Ρ89X���X�x4H�J�8�� Hw9,�������
lhm�S�1���͗V_�&�(��|�2�}�1��P˽јL>�/B|?\ɲ|]`���3�sv�t��@{sv���J�Y�#,I��� ŜR-1zPkO�:QoW5ʓ��N�)���x�u�1�"��y�׫7���=R��n�E�̽\���="� ��<KTOР�OZ�VdΉ��"�'O�������F�f�'5�Q2�\\a|�=�Eȇ�̭����(o�cq�u,]������ 0�8^Q��h��M��'�E�����K'���Z��3#b�$!K��k��'�UI�ţ�Ψ.�
g~��fƯ���F�v���4�fp��^񃧫g���@���T�XWt�J�87q����"5��@f,�V��{D�5BBmD����Ѩ'R�s�������7�.[���2,�.�O1b\��T��sI����nP�1(DPXY$f��>��3[�4U��/ģ��[��3��.���>w��@��l7>jb|��ݮ/c�.�-L�q�����г_�ev�U\��5��e�M�*���c���3��T�_��2k�bb�9��_n��r��L�>����y��������\'9���܊:D���g�������+������2�<	NdWR�y��U�V�G��:7	�K��P<W���8�4����2����S��]L��%d�躧�(����O������!�b�H��@�Q������aՐ��
�kE�1T�)T׀��/ƺd6^�N���_�q��kN�\<w�ґ:r�4���j�-�Jc��^_�z���b��p��3�Uo@�}e��b�.ϭ7�A���0@�W�7~"'�r	�(w�j߄�V#�]E����D��C�~Ԩ�@�l�b��(��7(��!�f	���
�^⚰[ͧ�QL�3-����z�+58�-����Ȉ�4�Fs:�g�H�Ek���9����W���{R�����j��D+�����V{��"	�^rZ%]j�y�=#EDS�l7�>r,�#>�$Gϫ���8U�l'� ����\I�Hkˊᆝt�S���|	�t|o���������ǅj���"����px>}b~R��9������7(f`���+X��:X!�8�����j������zTI�z�+�Z$��ȁ���!�;T��Y�Y�Y��=�En�b���\����V�	q{��8�5c��U&|�~�*~��z�Ǵ���SH'09������>�Ո?�N�N���C@*�,�x~�.iX�\cp����-���M�f�@�%r�	�ɏi�P��9�s��;U��RLFM��� V���_��](��2N�>��V�+�G��V�����g(�0�:@�67��W�x��:�^䉵�����}��5Q�	V�f�<<P��V^Z�,m'���s��F��;�n}hNգR�g���*��xN�ƞ`G�]���N|n�AȎ`�:�:��ͮ�e�Q%�&R*��?�jt�Y�NM��Xg�!���h|5	�f6��v��Z��p��2�ߟ��$�[��A��[�w�cm:�ƶ��a�=�K�M�)����uZU	A®x߭S\m��S��̋ �/�⹮-'6W���G7��z<*/s�����|��	w�c.9o�ѸR�����I�6dk�I; �����:g�Ȓ*��T��k�!�Vd�������9�(=I<�<�W�7�>r<>���f��9,�����m{���(ޖ-�Z5o�V�mrFx:�mt��.�r��s�4e�Lu�=lxD~̳�4�Z4Nk�X�S6��6��=�,���^o�i{bJ���J��¹&�gs-��:��@��[�V>,�Ĵ�'�o̿�F��l�G��f�����WT�u��1��(>OU�E1�l\Íi�0FY�����24y�~�D�0"�o�� ��s��dTt�u���b�B#Լ$X�F_�������t��_: /  ��ч>��<�{j�Յ8-�8F�A�ξ7B 	�)gB��ch���2p��l��-M��d	�0�p3+Uj�piՌ��L����.&�����O�|�7�>fA�+�M��T���P��	Ny.�5��r"����K)./_��&'�����KȦ~��n�VQ_�����X4f�Ʋ$����v�n�Osyߛq��#���<�����dMTs�e�[���3ȮV�p3��* ޑ\� �c)4���@���Qf1�*�l;j6DqQ���KU��NfƿO|7����?��E���az:R����-�	���j�F�����կ4������F1��\O��q0SC��9biBjG�LF B��<�wnA�N��p�Qw�<{�L+VikV 9k��=���0���t�Yʻ&�;Fߠ��[S��eӡΐ߻yGk����l��/�����_q:��}:�j��p���g�i���F�+�����H%,K�`�ռg�������}�leJ+�;�,8֭�6�^rv��Ī�.au��C;}�G�p��������Zl���R<�l�jx�0��؅�D�ӂ-a�Z���O��L8�G��
ޭF�5+Z�\s[рI�k8�m+��tz��#����W��vg�Q�~��?]W�"n��c[���������
��`έ��z|����L�Z�&���A���	���hTk���5}XM�u��yU�z�>�U��Opnn;�G��uf�6�4;@�̫]4Qr�Țp���sݷ�p(��Ê*��5
�6㘙k�\*LGl�����U������+\bΛF����s�kq��p�I>n�{L��#K����0^��׫_9��ݡ݄%p�Ԫ�H���{���]��9�1�R�[���ǭT�A޷k���<[G���+�\���ךD����ͣo=�0�y��X
^*|�z��&�w(���&ǃe�׫_���a����8`��\������ڦ���QD�Mj��p��p�˾���Mmk�a��_�� [�	��p�	��q��v���Q$���X!��z⭣��ql�GU�c�C��Dh���8]�.��x�Pz}��P>qǶ�Q�xGZ����-:�Ը�>���pl��>Cݸ�X"�����S�����8�ة�&{���Yj�T����C���z�1������95�me񚮴�p�bR��#* ��?�k��� �:!5����1E�*����Ͽ]���.��.���F� 	�,!{N�=�g�-[�����;��r,��o�?E�T����_[��' ���b��a�]��L8���������w(s�Z�2���xa��P�(�x�s�.��=L�z��S����=��2H7�>����^7�����H���mтl���9 K�<��f���X� ۶|�<5ٱ������<��r�6����8�Α�.�(�N�ᔴ\P���>0�r>��N��H�.�U��b��.�B8�`���Gnd�8e��<4��=�v�B�?���֟�wGV��ډ+%̖��T�G��U/���Fߠ8�L���3���P*.�����S7����H�r>�����z�+"�hTXz궝q�j�
'� ��x��F�P�d���<GX��ca;d�3��Jd��*�fg�*`�֟2�ai.�o�'�Ґ�����F91ջ��Λ*ݎaXָ1�#�se�IdM��s��\���oրc�UKp{��	����)E��fU�o���5�e�B����~�o����w�i�1��8�׹�ۀ��3Zφ��⠺�^�
L�|���=�e����wDP�+��&�%X�P�x��ض��d�R{��gGT���cj<c�����%��w�>f��Ɨ��ك3})�&�c��ڴ_ϐ��RT��2�&��q0�f2����Gԙ���I�k�s~���H��5�Ϣ �c���`'���x	��F9m����l����A!qf�����);-=�E��/n��*��'Q�)�[eUy�C�N-�7�mX��UKj�j�͌x�}t�釐���A�c�n���oqܲ��7����r��ͲBj��8R�|����N��e��{��^��
��h98�d��3� f
<��8�RK�~�g��j'-�.nǧ�`�!������D���<#�wd�Щ�F����� ����m�w�>�N����A��:�'���Y�b��8�k9��:���"�(n�:t8���}��w�dpbޠ�����Ϫ܇�a9����4J�]��_ʉ��|�ѷ�z�����ւ�^�E�}���*��ELU֧^�~mO�졋��U`�M{�_G������|h�,d����]�����.-䔡��8ܷ���V5{y��f	�v�H̰��b��.@j��ߧ��G9m���8A�˗{�ϛ�Zq�[��+�'����}��K���-�Ыm;�eE~���-��k��\��a�BEJ.x_�g�#��/J�]���m�כ���b�|�i��u�}D	Qi�ץ����cg��{��K��.�3ׂ�%�ܘ����H�#9��� ���\�u��`�(R��E$�M��>H|�rq��c����T�;�O���n��ک���'P��!Q{�A����6����n���8@�	�d�i�|عA�؏(ńP���쫗4 ��G�dۯo{KÉ%ӻ=Rw�>*�/S�{�ߋz
��:l\�����/��^ǘϱX�H2\�&����)�TlH�k����>t�K%��ǡ�D���H�j}���������w���}мx��j�$/zЩ������!�<�;�
φ��>�?�~����
�9      L      x������ � �     
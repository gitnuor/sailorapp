PGDMP      #                |            postgres     15.1 (Ubuntu 15.1-1.pgdg20.04+1)    16.0                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    5    postgres    DATABASE     p   CREATE DATABASE postgres WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'C.UTF-8';
    DROP DATABASE postgres;
                postgres    false            	           0    0    postgres    DATABASE PROPERTIES     �   ALTER DATABASE postgres SET "app.settings.jwt_secret" TO 'xwFkfTkScKY4d2jCsnsBlBycnsSH44p0uV4pmzSw6tcm7LMDnguL/Oxqx8M7DGi0CnlW8WOE7iVaTgbrzw7jxw==';
ALTER DATABASE postgres SET "app.settings.jwt_exp" TO '3600';
                     postgres    false            �          0    31253 
   gen_season 
   TABLE DATA           �   COPY public.gen_season (season_id, season_name, short_name, is_active, added_by, date_added, updated_by, date_updated, sequence) FROM stdin;
    public          postgres    false    352   �
       �          0    31258    gen_season_event_config 
   TABLE DATA           �   COPY public.gen_season_event_config (event_id, season_id, fiscal_year_id, start_date, start_month_id, end_month_id, added_by, updated_by, date_added, date_updated, event_title, is_active, end_date) FROM stdin;
    public          postgres    false    353   a       �          0    31263    gen_season_event_config_detl 
   TABLE DATA           Y   COPY public.gen_season_event_config_detl (detl_id, event_id, year, month_id) FROM stdin;
    public          postgres    false    354   �       
           0    0 (   gen_season_event_config_detl_detl_id_seq    SEQUENCE SET     W   SELECT pg_catalog.setval('public.gen_season_event_config_detl_detl_id_seq', 1, false);
          public          postgres    false    355                       0    0    tbl_season_master_season_id_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('public.tbl_season_master_season_id_seq', 1, true);
          public          postgres    false    437                       0    0    tbl_season_month_config_id_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('public.tbl_season_month_config_id_seq', 19, true);
          public          postgres    false    438            �   V   x�3�tK�Ɂ%���FFƺ����
V`�K�˄3�477�(&� �.#��̼��"E�~#.c����̼t��s��qqq u7?      �   /  x���=o�0���
�֝}篭�����K���Ј�K}��&��;�L�mJ���1��@8�p�KpY>�92E��;�����T	+�B��S�YwD��u���2�x��t�|�t*�zZ�\�_Ҝ��l�wG���$�����W�.5�:.���D��)���%'H0|�<�a=�����M��rל�ze��עk�fl��"d&	��D�+68L���x��W>���Jr���� Sj3���HNk��|V[���]�v$1�@{��I��]�Ey��9�������3���+U�/2ե�      �   V   x�=���@D�3S��C�Ћ���Hk��'��j���Qy0+9���M�nŹ�Bp�⅂�͆6���yh�h8%��՘��Q�> >|4      
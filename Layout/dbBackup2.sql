PGDMP         -    	            x            stock_db     13.1 (Ubuntu 13.1-1.pgdg20.04+1)     13.1 (Ubuntu 13.1-1.pgdg20.04+1)     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16384    stock_db    DATABASE     ]   CREATE DATABASE stock_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.UTF-8';
    DROP DATABASE stock_db;
                stanzu10    false            �            1259    16397    holding_table    TABLE     �   CREATE TABLE public.holding_table (
    id integer NOT NULL,
    user_id integer,
    symbol text,
    total_shares double precision,
    company_name text
);
 !   DROP TABLE public.holding_table;
       public         heap    stanzu10    false            �            1259    16413    session_table    TABLE     �   CREATE TABLE public.session_table (
    id integer NOT NULL,
    user_id integer,
    session_id text,
    init date,
    expire date
);
 !   DROP TABLE public.session_table;
       public         heap    stanzu10    false            �            1259    16405    transaction_table    TABLE     �   CREATE TABLE public.transaction_table (
    id integer NOT NULL,
    user_id integer,
    holding_id integer,
    transaction_price double precision,
    transaction_date date,
    type text,
    amount double precision,
    sell_all boolean
);
 %   DROP TABLE public.transaction_table;
       public         heap    stanzu10    false            �            1259    16389 
   user_table    TABLE     �   CREATE TABLE public.user_table (
    username text,
    password text,
    email text,
    created_at date,
    id integer NOT NULL,
    balance double precision
);
    DROP TABLE public.user_table;
       public         heap    stanzu10    false            9           2606    16404     holding_table holding_table_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.holding_table
    ADD CONSTRAINT holding_table_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.holding_table DROP CONSTRAINT holding_table_pkey;
       public            stanzu10    false    201            =           2606    16420     session_table session_table_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.session_table
    ADD CONSTRAINT session_table_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.session_table DROP CONSTRAINT session_table_pkey;
       public            stanzu10    false    203            ;           2606    16409 (   transaction_table transaction_table_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.transaction_table
    ADD CONSTRAINT transaction_table_pkey PRIMARY KEY (id);
 R   ALTER TABLE ONLY public.transaction_table DROP CONSTRAINT transaction_table_pkey;
       public            stanzu10    false    202            7           2606    16396    user_table user_table_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.user_table
    ADD CONSTRAINT user_table_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.user_table DROP CONSTRAINT user_table_pkey;
       public            stanzu10    false    200           
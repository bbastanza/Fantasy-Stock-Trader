toc.dat                                                                                             0000600 0004000 0002000 00000005513 13762043123 0014444 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        PGDMP       +    :                x            stock_db     13.1 (Ubuntu 13.1-1.pgdg20.04+1)     13.1 (Ubuntu 13.1-1.pgdg20.04+1) 
    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false         �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false         �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false         �           1262    16384    stock_db    DATABASE     ]   CREATE DATABASE stock_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.UTF-8';
    DROP DATABASE stock_db;
                stanzu10    false         �            1259    16397    holding_table    TABLE     �   CREATE TABLE public.holding_table (
    "Id" integer NOT NULL,
    "userId" integer,
    symbol text,
    "totalShares" double precision
);
 !   DROP TABLE public.holding_table;
       public         heap    stanzu10    false         �            1259    16405    transaction_table    TABLE     �   CREATE TABLE public.transaction_table (
    "Id" integer NOT NULL,
    "userId" integer,
    "holdingId" integer,
    "transactionPrice" double precision,
    "transactionDate" date,
    type text,
    amount double precision
);
 %   DROP TABLE public.transaction_table;
       public         heap    stanzu10    false         �            1259    16389 
   user_table    TABLE     �   CREATE TABLE public.user_table (
    "userName" text,
    password text,
    email text,
    "createdAt" date,
    "Id" integer NOT NULL,
    balance double precision
);
    DROP TABLE public.user_table;
       public         heap    stanzu10    false         4           2606    16404     holding_table holding_table_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public.holding_table
    ADD CONSTRAINT holding_table_pkey PRIMARY KEY ("Id");
 J   ALTER TABLE ONLY public.holding_table DROP CONSTRAINT holding_table_pkey;
       public            stanzu10    false    201         6           2606    16409 (   transaction_table transaction_table_pkey 
   CONSTRAINT     h   ALTER TABLE ONLY public.transaction_table
    ADD CONSTRAINT transaction_table_pkey PRIMARY KEY ("Id");
 R   ALTER TABLE ONLY public.transaction_table DROP CONSTRAINT transaction_table_pkey;
       public            stanzu10    false    202         2           2606    16396    user_table user_table_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.user_table
    ADD CONSTRAINT user_table_pkey PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public.user_table DROP CONSTRAINT user_table_pkey;
       public            stanzu10    false    200                                                                                                                                                                                             restore.sql                                                                                         0000600 0004000 0002000 00000005066 13762043123 0015374 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        --
-- NOTE:
--
-- File paths need to be edited. Search for $$PATH$$ and
-- replace it with the path to the directory containing
-- the extracted data files.
--
--
-- PostgreSQL database dump
--

-- Dumped from database version 13.1 (Ubuntu 13.1-1.pgdg20.04+1)
-- Dumped by pg_dump version 13.1 (Ubuntu 13.1-1.pgdg20.04+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE stock_db;
--
-- Name: stock_db; Type: DATABASE; Schema: -; Owner: -
--

CREATE DATABASE stock_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.UTF-8';


\connect stock_db

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_table_access_method = heap;

--
-- Name: holding_table; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.holding_table (
    "Id" integer NOT NULL,
    "userId" integer,
    symbol text,
    "totalShares" double precision
);


--
-- Name: transaction_table; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.transaction_table (
    "Id" integer NOT NULL,
    "userId" integer,
    "holdingId" integer,
    "transactionPrice" double precision,
    "transactionDate" date,
    type text,
    amount double precision
);


--
-- Name: user_table; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.user_table (
    "userName" text,
    password text,
    email text,
    "createdAt" date,
    "Id" integer NOT NULL,
    balance double precision
);


--
-- Name: holding_table holding_table_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.holding_table
    ADD CONSTRAINT holding_table_pkey PRIMARY KEY ("Id");


--
-- Name: transaction_table transaction_table_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_table
    ADD CONSTRAINT transaction_table_pkey PRIMARY KEY ("Id");


--
-- Name: user_table user_table_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.user_table
    ADD CONSTRAINT user_table_pkey PRIMARY KEY ("Id");


--
-- PostgreSQL database dump complete
--

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
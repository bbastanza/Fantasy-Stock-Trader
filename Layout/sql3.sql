toc.dat                                                                                             0000600 0004000 0002000 00000010663 13766452423 0014461 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        PGDMP           *    
            x            stock_db     13.1 (Ubuntu 13.1-1.pgdg20.04+1)     13.1 (Ubuntu 13.1-1.pgdg20.04+1)     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false         �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false         �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false         �           1262    16384    stock_db    DATABASE     ]   CREATE DATABASE stock_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.UTF-8';
    DROP DATABASE stock_db;
                stanzu10    false         �            1259    16397    holding_table    TABLE     �   CREATE TABLE public.holding_table (
    id integer NOT NULL,
    user_id integer,
    symbol text,
    total_shares double precision,
    company_name text
);
 !   DROP TABLE public.holding_table;
       public         heap    stanzu10    false         �            1259    16413    session_table    TABLE     �   CREATE TABLE public.session_table (
    id integer NOT NULL,
    user_id integer,
    session_id text,
    init date,
    expire date
);
 !   DROP TABLE public.session_table;
       public         heap    stanzu10    false         �            1259    16405    transaction_table    TABLE     �   CREATE TABLE public.transaction_table (
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
       public         heap    stanzu10    false         �            1259    16389 
   user_table    TABLE     �   CREATE TABLE public.user_table (
    username text,
    password text,
    email text,
    created_at date,
    id integer NOT NULL,
    balance double precision
);
    DROP TABLE public.user_table;
       public         heap    stanzu10    false         �          0    16397    holding_table 
   TABLE DATA           X   COPY public.holding_table (id, user_id, symbol, total_shares, company_name) FROM stdin;
    public          stanzu10    false    201       3009.dat �          0    16413    session_table 
   TABLE DATA           N   COPY public.session_table (id, user_id, session_id, init, expire) FROM stdin;
    public          stanzu10    false    203       3011.dat �          0    16405    transaction_table 
   TABLE DATA           �   COPY public.transaction_table (id, user_id, holding_id, transaction_price, transaction_date, type, amount, sell_all) FROM stdin;
    public          stanzu10    false    202       3010.dat �          0    16389 
   user_table 
   TABLE DATA           X   COPY public.user_table (username, password, email, created_at, id, balance) FROM stdin;
    public          stanzu10    false    200       3008.dat 9           2606    16404     holding_table holding_table_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.holding_table
    ADD CONSTRAINT holding_table_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.holding_table DROP CONSTRAINT holding_table_pkey;
       public            stanzu10    false    201         =           2606    16420     session_table session_table_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.session_table
    ADD CONSTRAINT session_table_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.session_table DROP CONSTRAINT session_table_pkey;
       public            stanzu10    false    203         ;           2606    16409 (   transaction_table transaction_table_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.transaction_table
    ADD CONSTRAINT transaction_table_pkey PRIMARY KEY (id);
 R   ALTER TABLE ONLY public.transaction_table DROP CONSTRAINT transaction_table_pkey;
       public            stanzu10    false    202         7           2606    16396    user_table user_table_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.user_table
    ADD CONSTRAINT user_table_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.user_table DROP CONSTRAINT user_table_pkey;
       public            stanzu10    false    200                                                                                     3009.dat                                                                                            0000600 0004000 0002000 00000000005 13766452423 0014254 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           3011.dat                                                                                            0000600 0004000 0002000 00000000104 13766452423 0014245 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	1	7cecad27-c48a-4706-9ade-d80a07c122be	2020-12-15	2020-12-16
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                            3010.dat                                                                                            0000600 0004000 0002000 00000000005 13766452423 0014244 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           3008.dat                                                                                            0000600 0004000 0002000 00000000141 13766452423 0014254 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        andy	password	email	2020-12-13	2	100000
Brian	password	email	2020-12-13	1	88958.16523719385
\.


                                                                                                                                                                                                                                                                                                                                                                                                                               restore.sql                                                                                         0000600 0004000 0002000 00000010750 13766452423 0015403 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        --
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
-- Name: stock_db; Type: DATABASE; Schema: -; Owner: stanzu10
--

CREATE DATABASE stock_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.UTF-8';


ALTER DATABASE stock_db OWNER TO stanzu10;

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

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: holding_table; Type: TABLE; Schema: public; Owner: stanzu10
--

CREATE TABLE public.holding_table (
    id integer NOT NULL,
    user_id integer,
    symbol text,
    total_shares double precision,
    company_name text
);


ALTER TABLE public.holding_table OWNER TO stanzu10;

--
-- Name: session_table; Type: TABLE; Schema: public; Owner: stanzu10
--

CREATE TABLE public.session_table (
    id integer NOT NULL,
    user_id integer,
    session_id text,
    init date,
    expire date
);


ALTER TABLE public.session_table OWNER TO stanzu10;

--
-- Name: transaction_table; Type: TABLE; Schema: public; Owner: stanzu10
--

CREATE TABLE public.transaction_table (
    id integer NOT NULL,
    user_id integer,
    holding_id integer,
    transaction_price double precision,
    transaction_date date,
    type text,
    amount double precision,
    sell_all boolean
);


ALTER TABLE public.transaction_table OWNER TO stanzu10;

--
-- Name: user_table; Type: TABLE; Schema: public; Owner: stanzu10
--

CREATE TABLE public.user_table (
    username text,
    password text,
    email text,
    created_at date,
    id integer NOT NULL,
    balance double precision
);


ALTER TABLE public.user_table OWNER TO stanzu10;

--
-- Data for Name: holding_table; Type: TABLE DATA; Schema: public; Owner: stanzu10
--

COPY public.holding_table (id, user_id, symbol, total_shares, company_name) FROM stdin;
\.
COPY public.holding_table (id, user_id, symbol, total_shares, company_name) FROM '$$PATH$$/3009.dat';

--
-- Data for Name: session_table; Type: TABLE DATA; Schema: public; Owner: stanzu10
--

COPY public.session_table (id, user_id, session_id, init, expire) FROM stdin;
\.
COPY public.session_table (id, user_id, session_id, init, expire) FROM '$$PATH$$/3011.dat';

--
-- Data for Name: transaction_table; Type: TABLE DATA; Schema: public; Owner: stanzu10
--

COPY public.transaction_table (id, user_id, holding_id, transaction_price, transaction_date, type, amount, sell_all) FROM stdin;
\.
COPY public.transaction_table (id, user_id, holding_id, transaction_price, transaction_date, type, amount, sell_all) FROM '$$PATH$$/3010.dat';

--
-- Data for Name: user_table; Type: TABLE DATA; Schema: public; Owner: stanzu10
--

COPY public.user_table (username, password, email, created_at, id, balance) FROM stdin;
\.
COPY public.user_table (username, password, email, created_at, id, balance) FROM '$$PATH$$/3008.dat';

--
-- Name: holding_table holding_table_pkey; Type: CONSTRAINT; Schema: public; Owner: stanzu10
--

ALTER TABLE ONLY public.holding_table
    ADD CONSTRAINT holding_table_pkey PRIMARY KEY (id);


--
-- Name: session_table session_table_pkey; Type: CONSTRAINT; Schema: public; Owner: stanzu10
--

ALTER TABLE ONLY public.session_table
    ADD CONSTRAINT session_table_pkey PRIMARY KEY (id);


--
-- Name: transaction_table transaction_table_pkey; Type: CONSTRAINT; Schema: public; Owner: stanzu10
--

ALTER TABLE ONLY public.transaction_table
    ADD CONSTRAINT transaction_table_pkey PRIMARY KEY (id);


--
-- Name: user_table user_table_pkey; Type: CONSTRAINT; Schema: public; Owner: stanzu10
--

ALTER TABLE ONLY public.user_table
    ADD CONSTRAINT user_table_pkey PRIMARY KEY (id);


--
-- PostgreSQL database dump complete
--

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
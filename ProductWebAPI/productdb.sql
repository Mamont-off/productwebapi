--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0

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

CREATE TABLE public."Links" (
                                "Id" integer NOT NULL,
                                "NomenclatureId" integer NOT NULL,
                                "ParentId" integer NOT NULL,
                                "Kol" integer DEFAULT 0
);


ALTER TABLE public."Links" OWNER TO postgres;

CREATE TABLE public."Nomenclature" (
                                       "Id" integer NOT NULL,
                                       "Name" text,
                                       "Price" money DEFAULT 0
);


ALTER TABLE public."Nomenclature" OWNER TO postgres;

CREATE SEQUENCE public."Nomenclature_ID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Nomenclature_ID_seq" OWNER TO postgres;

ALTER SEQUENCE public."Nomenclature_ID_seq" OWNED BY public."Nomenclature"."Id";

CREATE TABLE public."ProductMetaData" (
                                          "Id" integer NOT NULL,
                                          "NomenclatureId" integer NOT NULL,
                                          "MetaData" text[]
);


ALTER TABLE public."ProductMetaData" OWNER TO postgres;

CREATE SEQUENCE public."links_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."links_Id_seq" OWNER TO postgres;

ALTER SEQUENCE public."links_Id_seq" OWNED BY public."Links"."Id";

CREATE SEQUENCE public."links_nomenklatureId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."links_nomenklatureId_seq" OWNER TO postgres;

ALTER SEQUENCE public."links_nomenklatureId_seq" OWNED BY public."Links"."NomenclatureId";

CREATE SEQUENCE public."links_parentId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."links_parentId_seq" OWNER TO postgres;

ALTER SEQUENCE public."links_parentId_seq" OWNED BY public."Links"."ParentId";

CREATE SEQUENCE public."productMetaData_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."productMetaData_Id_seq" OWNER TO postgres;

ALTER SEQUENCE public."productMetaData_Id_seq" OWNED BY public."ProductMetaData"."Id";

CREATE SEQUENCE public."productMetaData_nomenclatureId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."productMetaData_nomenclatureId_seq" OWNER TO postgres;

ALTER SEQUENCE public."productMetaData_nomenclatureId_seq" OWNED BY public."ProductMetaData"."NomenclatureId";

ALTER TABLE ONLY public."Links" ALTER COLUMN "Id" SET DEFAULT nextval('public."links_Id_seq"'::regclass);

ALTER TABLE ONLY public."Links" ALTER COLUMN "NomenclatureId" SET DEFAULT nextval('public."links_nomenklatureId_seq"'::regclass);

ALTER TABLE ONLY public."Links" ALTER COLUMN "ParentId" SET DEFAULT nextval('public."links_parentId_seq"'::regclass);

ALTER TABLE ONLY public."Nomenclature" ALTER COLUMN "Id" SET DEFAULT nextval('public."Nomenclature_ID_seq"'::regclass);

ALTER TABLE ONLY public."ProductMetaData" ALTER COLUMN "Id" SET DEFAULT nextval('public."productMetaData_Id_seq"'::regclass);

ALTER TABLE ONLY public."ProductMetaData" ALTER COLUMN "NomenclatureId" SET DEFAULT nextval('public."productMetaData_nomenclatureId_seq"'::regclass);

INSERT INTO public."Nomenclature" VALUES
                                      (1,'Продукт1',100),
                                      (2,'Продукт2',200),
                                      (3,'Продукт3',300),
                                      (4,'Продукт4',400);

INSERT INTO public."Links" VALUES
                               (1,2,1,2),
                               (2,3,2,1),
                               (3,4,2,3);

INSERT INTO public."ProductMetaData" VALUES
                                         (1,1,'{color=red}'),
                                         (2,2,'{count=123,manufacturer=\"ООО Рога и копыта\"}');

SELECT pg_catalog.setval('public."Nomenclature_ID_seq"', 4, true);

SELECT pg_catalog.setval('public."links_Id_seq"', 3, true);

SELECT pg_catalog.setval('public."links_nomenklatureId_seq"', 1, false);

SELECT pg_catalog.setval('public."links_parentId_seq"', 1, false);

SELECT pg_catalog.setval('public."productMetaData_Id_seq"', 2, true);

SELECT pg_catalog.setval('public."productMetaData_nomenclatureId_seq"', 1, false);

ALTER TABLE ONLY public."Nomenclature"
    ADD CONSTRAINT "Nomenclature_pkey" PRIMARY KEY ("Id");

ALTER TABLE ONLY public."Links"
    ADD CONSTRAINT links_pkey PRIMARY KEY ("Id");

ALTER TABLE ONLY public."ProductMetaData"
    ADD CONSTRAINT "productMetaData_pkey" PRIMARY KEY ("Id");

ALTER TABLE ONLY public."Links"
    ADD CONSTRAINT "nLink" FOREIGN KEY ("NomenclatureId") REFERENCES public."Nomenclature"("Id") MATCH FULL ON DELETE CASCADE NOT VALID;

ALTER TABLE ONLY public."ProductMetaData"
    ADD CONSTRAINT "nomenclatureId" FOREIGN KEY ("NomenclatureId") REFERENCES public."Nomenclature"("Id") MATCH FULL ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY public."Links"
    ADD CONSTRAINT "pLink" FOREIGN KEY ("ParentId") REFERENCES public."Nomenclature"("Id") MATCH FULL ON DELETE CASCADE NOT VALID;

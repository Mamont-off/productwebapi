FROM postgres:16
COPY productdb.sql /docker-entrypoint-initdb.d/
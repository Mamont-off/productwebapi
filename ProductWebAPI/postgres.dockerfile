FROM postgres:16
ENV POSTGRES_PASSWORD=postgres
ENV POSTGRES_USER=postgres
ENV POSTGRES_DB=ProductDB
COPY productdb.sql /docker-entrypoint-initdb.d/
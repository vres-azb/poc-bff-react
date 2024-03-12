#!/bin/sh

# Start the script to create the DB and user
/usr/docker-entrypoint-initdb.d/configure-db.sh &

# Start SQL Server
/opt/mssql/bin/sqlservr
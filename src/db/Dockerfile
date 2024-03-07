FROM mcr.microsoft.com/mssql/server:2022-latest
ADD  docker-entrypoint-initdb.d /usr/docker-entrypoint-initdb.d/
# ENV ACCEPT_EULA Y
# ENV MSSQL_SA_PASSWORD P@ssw0rd

#  Create a config directory
# RUN mkdir -p /usr/docker-entrypoint-initdb.d
WORKDIR /usr/docker-entrypoint-initdb.d
ENTRYPOINT ["bash"]
CMD [ "./entrypoint.sh" ]
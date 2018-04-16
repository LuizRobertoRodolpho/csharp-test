#!/bin/bash
dotnet test PortalTelemedicina.Tests

docker-compose build

docker-compose up -d

DOCURL=$(docker inspect --format='http://localhost:{{(index (index .NetworkSettings.Ports "80/tcp") 0).HostPort}}/swagger' portal-telemedicina-api)
APIURL=$(docker inspect --format='http://localhost:{{(index (index .NetworkSettings.Ports "80/tcp") 0).HostPort}}/api' portal-telemedicina-api)

echo API documentation can be found at $DOCURL
echo API service is available at $APIURL

start chrome $DOCURL

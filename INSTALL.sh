#!/bin/bash
read -p "Building PortalTelemedicina container -- Press any key to continue... "
docker-compose build

read -p "Starting -- Press any key to continue... "
docker-compose up
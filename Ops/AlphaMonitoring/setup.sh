#!/bin/sh
echo "Installing Grafana/Loki/Prometheus...";

DOCKER_DAEMON=/etc/docker/daemon.json
DATA_DIR=/apps/logana;

# Install loki docker plugin
docker plugin install grafana/loki-docker-driver:latest --alias loki --grant-all-permissions;  

# Setup Docker Daemon
if ! [ -f "$DOCKER_DAEMON" ]
then
    touch $DOCKER_DAEMON;
    chmod 600 $DOCKER_DAEMON;
    echo "$DOCKER_DAEMON created";
fi

echo "Starting Grafana...";
docker compose up -d;

echo "Grafana/Loki/Prometheus has been installed";
echo "Removing Installer...";
rm -rf ./install.sh;
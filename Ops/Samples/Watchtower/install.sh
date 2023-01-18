#!/bin/sh
echo "Installing Watchtower...";

DOCKER_DIR=~/.docker
DOCKER_CONFIG_FILE=$DOCKER_DIR/config.json

DATA_DIR=/apps/watchtower/data
SYMBOLIC_CONFIG_FILE=$DATA_DIR/symconfig

# Setup data folder
if ! [ -d "$DATA_DIR" ] 
then
    mkdir $DATA_DIR;
    echo "$DATA_DIR folder created";
fi

# Setup docker login config
if ! [ -d "$DOCKER_DIR" ]
then
    mkdir $DOCKER_DIR;
    echo "$DOCKER_DIR folder created";
fi

if ! [ -f "$DOCKER_CONFIG_FILE" ]
then
    touch $DOCKER_CONFIG_FILE;
    chmod 600 $DOCKER_CONFIG_FILE;
    ln -sf $DOCKER_CONFIG_FILE $SYMBOLIC_CONFIG_FILE;
    echo "$DOCKER_CONFIG_FILE created";
fi

echo "Starting Watchtower...";
docker compose up -d;

echo "Removing Installer...";
rm -rf ./install.sh;

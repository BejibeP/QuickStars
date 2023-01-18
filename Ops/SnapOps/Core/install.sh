#!/bin/sh
set -e;

catch () {
  echo "An error occurred while installing the software.";
  exit 1;
}

trap "catch" EXIT;

COMPOSE_FILE=./docker-compose.yml
RUN_FILE=./run.sh
TRAEFIK_CONFIG=./traefik_config/*

DOCKER_DIR=~/.docker;
DOCKER_CONFIG_FILE=~/.docker/config.json;

SNAP_DIR=/apps/snapops;
DATA_DIR=/apps/snapops/data;

SNAPOPS_NETWORK=snapops;

remove_installer () {
  echo "Removing the installer...";
  rm -rf ./install.sh;
}

install_dep () {  

  # Creating Docker user folder and Docker user config
  if ! [ -d "$DOCKER_DIR" ]
  then
    mkdir $DOCKER_DIR;
    echo "$DOCKER_DIR folder created";
  fi

  if ! [ -f "$DOCKER_CONFIG_FILE" ]
  then
    touch $DOCKER_CONFIG_FILE;
    chmod 600 $DOCKER_CONFIG_FILE;
    echo "$DOCKER_CONFIG_FILE created";
  fi

  # Creating Docker Network Proxy if it does not exists
  if [ -z $(docker network ls --filter name=^${SNAPOPS_NETWORK}$ --format="{{ .Name }}") ]
  then
    docker network create ${SNAPOPS_NETWORK};
    echo "Docker network $SNAPOPS_NETWORK created";
  fi

}

setup_snapops () {
  # Setting up Snapops folders
  if ! [ -d "$SNAP_DIR" ]
  then
    mkdir $SNAP_DIR;
    echo "$SNAP_DIR folder created";
  fi

  if ! [ -d "$DATA_DIR" ] 
  then
    mkdir $DATA_DIR;
    echo "$DATA_DIR folder created";
  fi

  # Copy compose file
  mv $COMPOSE_FILE $SNAP_DIR/docker-compose.yml;
  mv $RUN_FILE $SNAP_DIR/run.sh
}

setup_watchtower () {
  # Setup docker config symlink
  SYMBOLIC_CONFIG_FILE=$DATA_DIR/symconfig;
  if ! [ -f "$SYMBOLIC_CONFIG_FILE" ]
  then
    ln -sf $DOCKER_CONFIG_FILE $SYMBOLIC_CONFIG_FILE;
    echo "$SYMBOLIC_CONFIG_FILE created";
    echo "You'll need to add some docker logins in to make it work...";
  fi
}

setup_grafana () {
  GRAFANA_DIR=$DATA_DIR/grafana
}

setup_traefik () {  
  # Setup Traefik DIR
  TRAEFIK_DIR=$DATA_DIR/traefik
   if ! [ -d "$TRAEFIK_DIR" ]
  then
    mkdir $TRAEFIK_DIR;
    echo "$TRAEFIK_DIR folder created";
  fi

  STAGING_CERT_FILE=$TRAEFIK_DIR/acme-staging.json;
  PROD_CERT_FILE=$TRAEFIK_DIR/acme.json;

  # Setup staging certificates
  if ! [ -f "$STAGING_CERT_FILE" ]
  then
    touch $STAGING_CERT_FILE;
    chmod 600 $STAGING_CERT_FILE;
    echo "$STAGING_CERT_FILE created";
  fi

  # Setup certificates
  if ! [ -f "$PROD_CERT_FILE" ]
  then
    touch $PROD_CERT_FILE;
    chmod 600 $PROD_CERT_FILE;
    echo "$PROD_CERT_FILE created";
  fi

  # Copy Traefik config files
  mv $TRAEFIK_CONFIG $TRAEFIK_DIR;
}

echo "SnapOps installation in progress...";

echo "Installing dependencies...";
install_dep;

echo "Setting up SnapOps...";
setup_snapops;

echo "Setting up Watchtower...";
setup_watchtower;

echo "Setting up Grafana...";
setup_grafana;

echo "Setting up Traefik...";
setup_traefik;

echo "Starting up SnapOps...";
sh $SNAP_DIR/run.sh

remove_installer;
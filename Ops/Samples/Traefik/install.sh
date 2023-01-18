#!/bin/sh
echo "Installing Traefik...";

DATA_DIR=/apps/traefik/data;
STAGING_CERT_FILE=$DATA_DIR/acme-staging.json;
PROD_CERT_FILE=$DATA_DIR/acme.json;
NETWORK_PROXY=proxy;

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

echo "Installing traefik configuration";

# Vérifier si le réseau proxy existe déjà, sinon le créer
if [ -z $(docker network ls --filter name=^${NETWORK_PROXY}$ --format="{{ .Name }}") ]
then
    echo "Creating docker network PROXY";
    docker network create ${NETWORK_PROXY};
fi

echo "Starting traefik...";
docker compose up -d;

echo "Removing Installer...";
rm -rf ./install.sh;

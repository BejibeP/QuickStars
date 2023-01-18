#!/bin/sh

export DOCKER_SYMCONFIG=./data/symconfig;
export TRAEFIK_DIR=./data/traefik/;
export HEIMDALL_DIR=./data/heimdall/;
export TRAEFIK_URL="traefik.i.bejibe.dev";
export HEIMDALL_URL="i.bejibe.dev";

docker compose up;
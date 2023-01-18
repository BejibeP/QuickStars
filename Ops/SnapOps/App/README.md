# Exemple-App GitHub Integration

Exemple d'application qui est géré par Traefik sur le network Proxy et surveillé par Watchtower

Cette section regroupe differents exemple de github actions pour de l'integration rapide

## Docker builder and publisher

Un tag d'une image docker est découpe ainsi [host:port/] [namespace/] image_name [:version] :

- host:port : adresse du registry docker, optionnel, si pas indiqué vise docker.io,
- port : port du registry docker, optionnel si pas de port indiqué vise le port 443
- namespace : espace de nom de l'image peut-etre une organisation, optionnel
- image_name : nom de l'image
- version : version de l'image, optionnel

Quickstart DevOps avec un pipeline CI/CD basé sur des containers et Traefik

Traefik est un service de reverse proxy qui offre des options de routing et de gestion des certificats avec let's encrypt

Pour rendre un container detectable via traefil ajouter :
    - traefik.enable=true
    - traefik.http.routers.router0.rule=(Path(`/a`)) || (PathPrefix(``)) || ()
    - traefik.http.routers.router0.tls=true
    - traefik.http.routers.router0.tls.certresolver=foobar

# Traefik

## Introduction

Traefik est un reverse proxy et un load balancer open source qui peut être utilisé pour router le trafic entre différents services web. Il peut être utilisé pour gérer le trafic de vos applications web, en dirigeant les demandes HTTP/HTTPS vers les instances appropriées.

L'objectif de cette documentation est de fournir une explication détaillée de l'installation, de la configuration et de l'utilisation de Traefik. Nous couvrirons les bases de son utilisation, ainsi que des configurations avancées telles que la configuration de TLS, l'utilisation de middlewares et l'utilisation de Let's Encrypt pour obtenir automatiquement des certificats SSL/TLS gratuits.

## Installation de Traefik

Pour installer Traefik, veuillez utilisez les fichiers docker-compose.yml et install.sh fourni.

Le docker-compose.yml contient la configuration nécessaire pour exécuter Traefik en tant que service Docker, avec des ports ouverts pour le trafic HTTP et HTTPS. Il utilise également un volume pour stocker la configuration statique et dynamique de Traefik ainsi que les certificats SSL/TLS générés automatiquement par Let's Encrypt.

La partie dynamique de la configuration est gérée par des labels spécifiques ajoutés à la définition du conteneur Traefik. Ces labels permettent de configurer dynamiquement les routes et les services exposés par Traefik en fonction des conteneurs Docker qui sont en cours d'exécution.

Le label traefik.enable=true active Traefik pour le conteneur, et les labels suivants définissent une route pour l'interface API de Traefik accessible depuis l'adresse `traefik.exemple.fr`. Ils spécifient également le middleware nommé `webauth` (dont la configuration dans la configuration dynamic.toml fournie) à utiliser pour l'authentification. La configuration complète de Traefik peut être ajustée en ajoutant ou en modifiant ces labels en fonction des besoins.

Le script shell fourni, install.sh, effectue les tâches suivantes :

- Crée un fichier de certificat de test pour Let's Encrypt
- Crée un fichier de certificat de production pour Let's Encrypt
- Vérifie si le réseau Docker requis pour exécuter Traefik existe, sinon le crée
- Lance le service Docker Traefik à l'aide du fichier docker-compose.yml

Pour utiliser ce script shell, exécutez-le dans le même dossier que le docker-compose
`sudo sh ./install.sh`

## Configuration de Traefik

La configuration de Traefik est divisée en deux parties : statique et dynamique.
La configuration statique contient les informations de base telles que les points d'entrée, les certificats, les résolveurs de certificats et les fournisseurs de données. Elle est définie dans le fichier traefik.toml et est lue au lancement de Traefik.

La configuration dynamique contient les informations spécifiques à chaque environnement et est générée à partir de fournisseurs dans tels que Docker, Kubernetes ou un fichier.
Elle peut être modifiée à chaud sans avoir besoin de redémarrer Traefik. La configuration dynamique contient des informations telles que les routeurs, les middlewares et les services.

Le template offre une base de configuration décrite si après

### Configuration Statique

#### Global

- Vérification de la présence d'une nouvelle version de Traefik (global.checkNewVersion)
- Désactivation de l'envoi anonyme de statistiques d'utilisation à Traefik (global.sendAnonymousUsage)
- Activation de l'API en mode sécurisé uniquement (api.insecure)
- Activation du dashboard de l'API (api.dashboard)
- Activation du mode de débogage de l'API (api.debug)

#### Logging et Metrics

- Sortie des logs Traefik et logs d'accès en mode stdout au format CLF (log.accessEncoding, log.format)
- Utilisation de Prometheus pour les métriques (metrics.prometheus)
- Niveau de log Traefik à INFO (log.level)
- Configuration du buffering des logs d'access à 10 (accesslog.bufferingSize)
- Filtre de log d'access pour afficher les tentatives de reconnexion (accessLog.filters.retryAttempts)
- Filtre de log d'access pour afficher les requêtes qui prennent plus de 10ms à répondre (accessLog.filters.minDuration)
- Configuration du mode de conservation des champs de log d'access à "keep" (accessLog.fields.defaultMode)
- Désactivation de l'affichage de l'adresse IP client dans les logs d'access (accessLog.fields.names.ClientAddr)
- Désactivation de l'affichage du nom d'utilisateur client dans les logs d'access (accessLog.fields.names.ClientUsername)
- Désactivation de l'affichage de l'hôte client dans les logs d'access (accessLog.fields.names.ClientHost)
- Conservation du champ d'en-tête "User-Agent" dans les logs d'access (accessLog.fields.headers.names.User-Agent)
- Suppression du champ d'en-tête "Authorization" dans les logs d'access (accessLog.fields.headers.names.Authorization)
- Conservation du champ d'en-tête "Content-Type" dans les logs d'access (accessLog.fields.headers.names.Content-Type)
- Ajout des labels pour les points d'entrée et les services dans les métriques (metrics.prometheus.addEntryPointsLabels, metrics.prometheus.addServicesLabels)
- Buckets utilisés pour les métriques Prometheus [0.1,0.3,1.2,5.0] (metrics.prometheus.buckets)

#### Points d'entrée

- Définition du point d'entrée "web" écoutant sur le port 80 (entryPoints.web.address)
- Redirection de toutes les requêtes HTTP du point d'entrée "web" vers le point d'entrée "websecure" en HTTPS (entryPoints.web.http.redirections)
- Définition du point d'entrée "websecure" écoutant sur le port 443 (entryPoints.websecure.address)
- Configuration du TLS pour le point d'entrée "websecure" avec les options TLS "intermediate@file" (entryPoints.websecure.http.tls.options) et le résolveur de certificats "letsresolve" (entryPoints.websecure.http.tls.certResolver)
- Définition du point d'entrée "metrics" écoutant sur le port 451 (entryPoints.metrics.address)

#### Certificats

- Configuration du résolveur de certificats "letsstage" & "letresolve" pour les environnements de staging et de production (certificatesResolvers.letsstage & certificatesResolvers.letsresolve)
- Utilisation du serveur CA de Let's Encrypt pour l'environnement de staging et de production (certificatesResolvers.letsX.acme.caServer)
- Utilisation d'une adresse email pour les notifications de renouvellement de certificat (certificatesResolvers.letsX.acme.email)
- Stockage du certificat et de la clé privée générés (certificatesResolvers.letsX.acme.storage)
- Génération de clés privées RSA de 4096 bits (certificatesResolvers.letsX.acme.keyType)
- Configuration du challenge HTTP pour les résolveurs de certificats (certificatesResolvers.letsX.acme.httpChallenge.entryPoint)

#### Fournisseurs

- Utilise le fournisseur "file" pour récupérer la configuration dynamique à partir de "/etc/traefik/dynamic.toml" (providers.file)
- Utilise le fournisseur "docker" pour récupérer la configuration dynamique à partir de Docker en utilisant le réseau "proxy" et en exposant les services de manière explicite (providers.docker)

### Configuration Dynamique

La configuration dynamique de traefik est situé dans les labels du docker-compose.yml, le service est configuré de cette facon :

#### Service Traefik

- Active le service Traefik (traefik.enable)
- Traefik utilise le point d'entrée websecure (traefik.http.routers.api.entrypoints)
- Traefik utilise le service api@internal (traefik.http.routers.api.service)
- Traefik est accessible à l'adresse `traefik.exemple.fr` (traefik.http.routers.api.rule)
- Traefik utilise le middleware webauth@file (traefik.http.routers.api.middlewares)

#### SSL/TLS

- Version minimale de TLS : `VersionTLS12` (`tls.options.intermediate.minVersion`)
- La version de TLS est basé sur la configuration Firefox : `https://ssl-config.mozilla.org`
- Suites de chiffrement acceptées : [`TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256`, `TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256`, `TLS_ECDHE_ECDSA_WITH_AES_256_GCM_SHA384`, `TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384`, `TLS_ECDHE_ECDSA_WITH_CHACHA20_POLY1305`,     - `TLS_ECDHE_RSA_WITH_CHACHA20_POLY1305`] (`tls.options.intermediate.cipherSuites`)

#### Middlewares

Le template n'offre qu'un middleware actuellement

- Middleware basicAuth nommé `webauth` (`http.middlewares.webauth`)
- `webauth` utilise le fichier `/etc/traefik/users.bin` pour gérer les utilisateurs (`http.middlewares.webauth.basicAuth.usersFile`)

Pour créer/ajouter des users utiliser la commande `htpasswd -nB <user>` pour créer un couple user/mot de passe

D'autre middlewares peuvent être crées et ajoutés :

- Middleware forwardAuth : Authentification externe (exemple Authelia) (TODO add authelia)

```toml

[http.middlewares.authelia]
  [http.middlewares.authelia.forwardAuth]
    address = "http://authelia:9091/api/verify?rd=https://auth.local.example.com"
    trustForwardHeader = true
    authResponseHeaders = ["Remote-User, Remote-Groups, Remote-Name, Remote-Email"]

```

- Middleware ipWhitelist

```toml

[http.middlewares.iplist]
  [http.middlewares.iplist.ipWhiteList]
    sourceRange = ["0.0.0.1", "0.0.0.2"]


```

- Middleware de gestion des erreur avec redirections, retry, etc

```toml

[http.middlewares.error]
  [http.middlewares.error.errors]
    status = ["400-599"] # codes de statuts à écouter
    service = "error-service" # nom du service sur traefik visé par la redirection d'erreur
    query = "/{status}.html" # ou query vide

```

- Middleware Circuit Breaker

```toml

[http.middlewares.breaker]
  [http.middlewares.breaker.circuitBreaker]
    expression = "NetworkErrorRatio() > X" # ratio d'erreur réseau
    expression = "ResponseCodeRatio(500, 600, 0, 600) > X" # ratio de status code
    expression = "LatencyAtQuantileMS(50.0) > X" # ratio de latence
    il est possible de combiner les expression avec des opérateurs logique

```

#### Ajout de services managés par Traefik

Pour que Traefik manage un service Docker, il faut configurer le conteneur avec les labels suivants :

- traefik.enable=true, pour activer le discovery du service
- traefik.http.routers.app0.rule=Host(`mydomain.com`) || PathPrefix(`/admin`), pour configurer la route sur laquelle le service sera accessible
- traefik.http.routers.app0.tls=true, pour redéfinir l'activation du TLS (true par défaut)
- traefik.http.routers.app0.tls.certresolver=letsstage|letsresolve (letsresolve par défaut)

Il n'est pas necessaire d'exposer les ports du conteneur, Traefik le détecte automatique (sauf si le conteneur à plusieurs ports d'entrée)

Pour que le routing fonctionne, il faut s'assurer que votre zone DNS est correctement configuré

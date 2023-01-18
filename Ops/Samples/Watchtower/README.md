# Watchtower

Watchtower est un outil open-source qui permet de surveiller les conteneurs Docker et de mettre à jour automatiquement les images lorsqu'une nouvelle version est disponible. Il s'agit d'un moyen pratique pour s'assurer que les conteneurs sont toujours à jour et sécurisés sans avoir à effectuer des mises à jour manuelles.

## Utilisation de Watchtower dans une stack Ops

Dans une stack Ops, Watchtower peut être utilisé pour mettre à jour automatiquement les conteneurs Docker d'une application, en réduisant ainsi les temps d'arrêt nécessaires pour effectuer des mises à jour. Cela permet également de s'assurer que les conteneurs sont toujours à jour avec les dernières fonctionnalités et correctifs de sécurité.

## Installation

Pour une installation simple vous pouvez utiliser le script shell fourni install.sh
Vous devez cependant déja avoir docker d'installé

## Configuration

### Général

- DOCKER_HOST : emplacement du socket docker, par défaut écoute le socket local
- WATCHTOWER_CLEANUP : Supprime les ancienne images après la mise à jour (par défaut : false)
- WATCHTOWER_ROLLING_RESTART : Mise à jour graduelle des conteneurs pour éviter le downtime (par défaut : false)
- WATCHTOWER_TIMEOUT : timeout avant que le conteneur soit stoppé de force (par défaut : 10s)
- WATCHTOWER_DEBUG : active le mode debug (par défaut : false)
- WATCHTOWER_LOG_LEVEL : niveau de logging de Watchtower (panic, fatal, error, warn, info, debug or trace) (par défaut : info)

### Monitoring

Par défaut, Watchtower surveille tous les conteneurs qui sont sur le socket, il peut-être configuré pour surveiller uniquement certains conteneurs.

- WATCHTOWER_LABEL_ENABLE : surveille les conteneurs qui ont un label `com.centurylinklabs.watchtower.enable` à true. (par défaut : false)
- WATCHTOWER_SCOPE : surveille les conteneurs qui ont un label `com.centurylinklabs.watchtower.scope` avec la valeur de WATCHTOWER_SCOPE, cela permet de mettre à jour plusieurs instances. (par défaut '')

### Frequence

Par défaut, Watchtower surveille les version des images toute les 24h

- WATCHTOWER_POLL_INTERVAL : fréquence à laquelle Watchtower vérifie les version des images (par défaut 86400 (secondes))
- WATCHTOWER_RUN_ONCE : lance Watchtower seulement une fois puis le stop (par défaut : false)

## Gestion des référentiels

Par défaut, les liens d'image docker utilisé visent le repository public Docker Hub, il est possible d'utiliser des images provenant d'un autre repository.

Pour que Watchtower puisse mettre à jour les conteneurs, l'image utilisé doit préciser le repository si ce n'est pas Docker Hub en mode public. Exemple : `myregistry.com</organisation/>myimage:latest`

Watchtower doit également être configuré pour se connecter à ces repository privés.
Il faut monter un fichier config.json au conteneur Watchtower.

Ce fichier peut-être soit :

- un ficher créer manuellement
- le fichier config.json de docker login `/$HOME/.docker/config.json`

### Configuration Manuelle

Ce fichier config.json doit être créer avec la syntaxe suivante :

```json
{
  "auths" : {
    "<REGISTRY>": {
      "auth": "<credentials>"
    }
  }
}
```

- Registry : url du private repository
- Credentials : chaine en base64 contenant un username et un mot de passe

### Configuration Docker Login

Ce fichier est générer par docker login, il est utile pour l'authentification à certains repository privés notamment dans le cas d'usage de 2FA

Pour rajouter des logins via ce mécanisme il faut utiliser la commande `docker login <url>` ou url est l'adresse du repository visé (ne rien mettre pour un login à Docker Hub)

Du au fonctionnement de docker login qui copie puis remplace le fichier config.json lors de modifications, toute modification des logins lorsque Watchtower est lancé ne sera pas repercuté car le mount sera brisé

Un moyen de contourner ce problème est d'utiliser un lien symbolique symlink en guise de mount :
`ln -sf /$HOME/.docker/config.json /apps/watchtower/data/symconfig`

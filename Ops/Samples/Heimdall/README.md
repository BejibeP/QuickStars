# Heimdall

Heimdall est un tableau de bord personnalisable et polyvalent pour les serveurs auto-hébergés, les applications Web et les services. Il offre une interface graphique pour gérer et surveiller différentes applications et services en un seul endroit.

## Utilisation de Heimdall dans une stack Ops

Heimdall peut être utilisé pour afficher des liens vers des services web et des applications, tels que des tableaux de bord de surveillance, des interfaces d'administration, des applications de gestion de tâches, etc. Les liens peuvent être organisés en groupes et en catégories pour faciliter la navigation.

En utilisant Heimdall dans une stack Ops, les équipes peuvent avoir une vue d'ensemble de leur infrastructure et de leur stack Ops à partir d'une interface unique, en évitant d'avoir à se connecter à plusieurs interfaces utilisateur différentes pour accéder aux différents services et applications.

## Configuration

Le template est un exemple de configuration pour déployer Heimdall en utilisant Docker et Traefik.

### Version

La version spécifiée dans le fichier est "3.9". Cela indique que le format du fichier est compatible avec Docker Compose version 3.9 ou supérieure.

### Services

Le fichier définit un service nommé heimdall basé sur l'image Docker linuxserver/heimdall:2.5.6. Il est configuré pour être en réseau avec le réseau proxy, et monte un volume local /apps/heimdall/data pour stocker les données du conteneur.

### Labels

Les labels permettent de définir les options de configuration pour le service. Dans cet exemple, les labels spécifient que Traefik doit être activé pour ce service et qu'il doit être accessible via l'URL heimdall.exemple.fr.

### Networks

Le réseau proxy est spécifié dans le fichier pour permettre à Heimdall de communiquer avec d'autres services de la stack Ops qui sont également connectés à ce réseau.

## Utilisation du template docker-compose

Pour utiliser ce fichier docker-compose, il suffit de le copier dans un répertoire local et d'exécuter la commande docker-compose up -d. Cela lancera le service Heimdall dans un conteneur Docker en arrière-plan, accessible via l'URL heimdall.exemple.fr.

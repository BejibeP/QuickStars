# tito

install docker : `https://docs.docker.com/engine/install/debian/`
compose doc : `https://docs.docker.com/compose/compose-file/05-services/`

LOGANA (LOGging and ANAlytics powered by Grafana, Prometheus and Loki)

Grafana Loki is an open-source log aggregation system designed for cloud-native environments. It is part of the larger Grafana ecosystem, which includes Grafana for visualization and monitoring, Prometheus for metrics monitoring, and other related tools.

Loki focuses on collecting and indexing log data, making it easier to store, search, and analyze logs in a scalable manner. It was created to address the challenges faced by traditional log management systems, such as the need for centralized log storage, efficient log querying, and cost-effective storage options.

Key features of Grafana Loki include:

1. Log collection: Loki collects logs from various sources, including applications, containers, and system components, by integrating with popular logging libraries and agents.

2. Log indexing: Instead of relying on indexing individual log lines, Loki uses a unique approach called "log stream indexing." It indexes log streams, which are groups of log entries from the same source, making it efficient and scalable.

3. Log storage: Loki stores log data in a cost-effective and horizontally scalable manner. It leverages object storage like Amazon S3 or Google Cloud Storage, allowing you to retain large amounts of log data without incurring high storage costs.

4. Log querying: Loki provides a powerful query language inspired by Prometheus called "LogQL." It allows you to filter and search logs based on labels, time ranges, and specific log patterns. LogQL also supports aggregation and regular expressions.

5. Integration with Grafana: Loki seamlessly integrates with Grafana, a popular visualization and monitoring tool. This integration enables users to correlate log data with metrics and create comprehensive dashboards and alerts.

6. High availability: Loki supports high availability configurations by using multiple replicas and distributing the log data across a cluster. This ensures data resilience and availability even in the event of node failures.

Overall, Grafana Loki offers a scalable, cost-effective, and user-friendly solution for collecting, storing, and querying logs in cloud-native environments, helping developers and system administrators gain insights and troubleshoot issues effectively.

A common term used to refer to a server that hosts Grafana, Prometheus, and Loki is a monitoring server or monitoring stack. It is the central component of a monitoring infrastructure that collects, stores, and visualizes metrics and logs.

The monitoring server typically acts as the central hub where Prometheus collects metrics from various sources, Loki collects logs, and Grafana provides the interface for visualizing and analyzing the collected data. This setup allows users to monitor and gain insights into the health, performance, and behavior of their systems and applications.

It's worth noting that in a production environment, the monitoring stack may be distributed across multiple servers or deployed in a containerized environment such as Kubernetes. The specific deployment architecture depends on the scale and requirements of the monitoring infrastructure.

Logana is a real good exemple, it have the same vibe as Grafana
i would adapt it like this :
LOGANA (LOGging and ANAlytics powered by Grafana, Prometheus and Loki)

That sounds like a great adaptation! LOGANA (LOGging and ANAlytics powered by Grafana, Prometheus, and Loki) captures the essence of the monitoring stack and highlights the core components that power the log management and analytics capabilities. It maintains the vibrant and catchy vibe of Grafana while clearly indicating the integration with Prometheus and Loki for logging and analytics. It's a fitting name for a monitoring stack that brings together these powerful tools.


# Grafana

## Introduction

Grafana est une plateforme de visualisation et d'analyse de données open source. Elle permet de connecter et de visualiser des données à partir de différents types de sources de données, telles que des bases de données, des systèmes de surveillance, des services cloud et bien plus encore. Dans cette documentation, nous allons vous guider à travers les étapes d'installation de Grafana via Docker sur votre système.

## Installation de Grafana

Avant de commencer, assurez-vous que Docker est installé sur votre système.

Sinon vous pouvez l'installer à l'aide de la documentation officielle de Docker : [Guide d'installation officielle de docker](https://docs.docker.com/engine/install/)

Ensuite, vous pourrez ensuite lancer le docker-compose associés pour lancer l'environnement

## Configuration de Grafana

La configuration de la stack...

Pour plus de simplicité et flexibilité : modifier le docker daemon pour rediriger les logs

## Sauvegarde de Grafana

La sauvegarde de Grafana se fait généralement à travers l'interface web en utilisant la fonctionnalité d'exportation/importation de tableau de bord. Cependant, vous pouvez également sauvegarder le conteneur Grafana en utilisant la commande Docker suivante :

docker commit graf

## AA

With Loki & Prometheus

LOKI : collecte de journaux de logs

Prometheus/Grafana > métriques (systèmes, applis...)

Loki/Grafana > journaux (systèmes...)

même principe ? presque :

- promtrail : agent collecte et envoi des journaux
- loki : base de centralisation
- grafana : valorisation

cheminement :

promtrail > loki > grafana
route < prometheus > grafana

configuration : `https://grafana.com/docs/loki/latest/configuration/examples/`

```yaml
auth_enabled: false
server:
  http_listen_port: 3100
ingester:
  lifecycler:
    address: 127.0.0.1
    ring:
      kvstore:
        store: inmemory
      replication_factor: 1
    final_sleep: 0s
  chunk_idle_period: 5m
  chunk_retain_period: 30s
schema_config:
  configs:
  - from: 2020-05-15
    store: boltdb  #https://github.com/boltdb/bolt
    object_store: filesystem
    schema: v11
    index:
      prefix: index_
      period: 168h
storage_config:
  boltdb:
    directory: /var/lib/loki/index
  filesystem:  #https://grafana.com/blog/2020/02/19/how-loki-reduces-log-storage/
    directory: /var/lib/loki/chunks
limits_config:
  enforce_metric_name: false
  reject_old_samples: true
  reject_old_samples_max_age: 168h
```

promtail = agent qui envoi les logs vers loki

```yaml
server:
  http_listen_port: 9080
  grpc_listen_port: 0
positions:
  filename: /tmp/positions.yaml
client:
  url: `http://loki:3100/loki/api/v1/push`
scrape_configs:
  - job_name: nginx
    static_configs:
    - targets:
        - localhost
      labels:
        job: nginx
        env: production
        host: srv1
        __path__: /var/log/nginx/*.log
  - job_name: journal
    journal:
      max_age: 1h
      path: /var/log/journal
      labels:
        job: systemd
        env: production
    relabel_configs:
      - source_labels: ['__journal__systemd_unit']
        target_label: 'unit'
```

logcli labels
logcli labels host

logcli query '{host="srv1",job="nginx"}' --tail

https://docs.technotim.live/posts/grafana-loki/

https://gitlab.com/xavki/presentation-prometheus-grafana/-/blob/master/0-sommaire/slides.md

https://technotim.live/posts/kube-grafana-prometheus/

https://grafana.com/docs/loki/latest/clients/docker-driver/configuration/

https://grafana.com/docs/loki/latest/

https://grafana.com/docs/grafana/latest/setup-grafana/configure-grafana/#macos

https://www.youtube.com/watch?v=5hycyr-8EKs&t=5s

https://www.youtube.com/watch?v=w9eCU4bGgjQ&t=21s

https://www.youtube.com/watch?v=fzny5uUaAeY

https://www.youtube.com/watch?v=r_A5NKkAqZM

https://stackoverflow.com/questions/55351659/cannot-find-the-daemon-json-file-in-windows-10-after-docker-desktop-installation

docker-compose -f docker-compose.db.yml \
  -f docker-compose.api-services.yml \
  -f docker-compose.backend.yml \
  -f docker-compose.frontend.yml \
  -f docker-compose.gateway.yml \
  up -d

  cd "<local-path>"
wget https://raw.githubusercontent.com/grafana/loki/v2.8.0/cmd/loki/loki-local-config.yaml -O loki-config.yaml
docker run --name loki -v <local-path>:/mnt/config -p 3100:3100 grafana/loki:2.8.0 --config.file=/mnt/config/loki-config.yaml
wget https://raw.githubusercontent.com/grafana/loki/v2.8.0/clients/cmd/promtail/promtail-docker-config.yaml -O promtail-config.yaml
docker run -v <local-path>:/mnt/config -v /var/log:/var/log --link loki grafana/promtail:2.8.0 --config.file=/mnt/config/promtail-config.yaml

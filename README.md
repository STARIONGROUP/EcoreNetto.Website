![EcoreNetto](https://raw.githubusercontent.com/STARIONGROUP/EcoreNetto.Website/development/Ecorenetto-Logo-text.png)

## Introduction

This repository contains the source code for the [ecorenetto](https://ecorenetto.org) website. This is a server-side Blazor application using [Radzen](https://blazor.radzen.com)

## Build and Deploy using Docker

The EcoreNetto website is built using docker and the result is a Docker container ready to be deployed (or pushed to Docker Hub). The Docker file is located in the root folder.

Two scripts are provided to create a docker image:
  - `docker-build-local.sh`: creates an image that can be run locally with `docker run -p 5000:5000 --name ecorenetto-website stariongroup/ecorenetto-website:latest`
  - `docker-build-attested.sh`: creates an image that is attested and includes an SBOM. This is immediately pushed to docker hub

> both scripts need to be run from a linux command line (like the console in GitExtensions)

Use Docker Compose:

```
sudo docker-compose -f ecorenetto-docker-compose.yml up -d
```

with following docker file

```
services:
  ecorenetto-website:
    image: index.docker.io/stariongroup/ecorenetto-website:latest
    ports:
      - 80:80
```

## Code Quality

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_EcoreNetto.Website&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_EcoreNetto)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_EcoreNetto.Website&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_EcoreNetto)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_EcoreNetto.Website&metric=coverage)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_EcoreNetto)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_EcoreNetto.Website&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_EcoreNetto)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_EcoreNetto.Website&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_EcoreNetto)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_EcoreNetto.Website&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_EcoreNetto)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_EcoreNetto.Website&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_EcoreNetto)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_EcoreNetto.Website&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_EcoreNetto)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_EcoreNetto.Website&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_EcoreNetto)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=STARIONGROUP_EcoreNetto.Website&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=STARIONGROUP_EcoreNetto)

## Build Status

GitHub actions are used to build and test the EcoreNetto.Website

Branch | Build Status
------- | :------------
Master | ![Build Status](https://github.com/STARIONGROUP/EcoreNetto.Website/actions/workflows/CodeQuality.yml/badge.svg?branch=master)
Development | ![Build Status](https://github.com/STARIONGROUP/EcoreNetto.Website/actions/workflows/CodeQuality.yml/badge.svg?branch=development)

# License

The EcoreNetto.Website is licensed under the Apache License 2.0.

# Contributions

Contributions to the code-base are welcome. However, before we can accept your contributions we ask any contributor to sign the Contributor License Agreement (CLA) and send this digitaly signed to s.gerene@stariongroup.eu. You can find the CLA's in the CLA folder.

[Contribution guidelines for this project](.github/CONTRIBUTING.md)
# ___________________________________________________________________________ BUILD STAGE
#
FROM shared-bell-docker-prod.artifactory.int.bell.ca/alpine:3.18 AS builder

# Get APK base packages (Certs, NGINX, etc)
RUN apk update \
 && apk upgrade \
 && apk add --no-cache --update ca-certificates npm \
 && rm -rf /var/cache/apk/*

# Copy Bell Certs, Update Certs List
COPY ./deployment/files/main/BellRootCA.cer /usr/local/share/ca-certificates/
RUN update-ca-certificates

# Set App Work Directory
WORKDIR /opt/app

# Copy source files to builder
COPY ./ ./

# Run NPM build
RUN npm config set registry=https://artifactory.int.bell.ca/artifactory/api/npm/gasp-npm
RUN npm install --verbose
RUN npm run coverage
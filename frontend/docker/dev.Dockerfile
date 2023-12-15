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
RUN npm run build-development --verbose

# ---------------------------------------------------------------------------- IMAGE STAGE

FROM shared-bell-docker-prod.artifactory.int.bell.ca/alpine:3.18

# Get APK base packages (Certs, TimeZone, NGINX, etc)
# Busybox-Extras for Telnet, iputils for TracePath
RUN apk update \
 && apk upgrade \
 && apk add --no-cache --update \
 ca-certificates \
 tzdata \
 nginx \
 busybox-extras \
 iputils \
 curl \
 && cp /usr/share/zoneinfo/America/New_York /etc/localtime \
 && apk del tzdata \
 && rm -rf /var/cache/apk/* \
 && mkdir -p /var/log/nginx /var/lib/nginx/tmp/client_body /run/nginx \
 && chmod -R go=u /var/lib/nginx /var/log/nginx /run/nginx

# Copy all needed files (SAP Installer, SAP Configs, Python Scripts, etc)
COPY ./deployment/files/main/BellRootCA.cer /usr/local/share/ca-certificates/
COPY ./deployment/files/main/default.conf /etc/nginx/http.d/

# Update Certs List
RUN update-ca-certificates

# Set App Work Directory
WORKDIR /opt/app

# Copy compiled project from builder stage to image
COPY --from=builder /opt/app/dist dist

EXPOSE 4000
CMD ["/usr/sbin/nginx", "-g", "daemon off;error_log /dev/stdout info;"]

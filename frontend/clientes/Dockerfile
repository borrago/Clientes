# Fase de construção
FROM node:21-alpine as build-deps

WORKDIR /usr/src/app
COPY . .

ENV NODE_OPTIONS --openssl-legacy-provider

RUN npm config set maxsockets=5 \
    && npm install --force \
    && npm run build

# Fase de execução
FROM node:21-alpine

WORKDIR /usr/src/app

COPY --from=build-deps /usr/src/app /usr/src/app

CMD ["npm", "start"]

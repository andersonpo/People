FROM node:alpine as build
WORKDIR /src
COPY . /src
RUN npm install
# Generate output in /dist/people
RUN npm run build 

FROM nginx:alpine
COPY --from=build /src/dist/people /usr/share/nginx/html

EXPOSE 80
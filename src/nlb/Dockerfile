FROM nginx:stable-alpine
COPY ../nlb/nginx.conf /etc/nginx/conf.d/default.conf
COPY ../cert/poc_bff* /etc/ssl/private/
EXPOSE 80
EXPOSE 443
CMD ["nginx", "-g", "daemon off;"]
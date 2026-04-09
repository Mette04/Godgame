FROM nginx:1.27-alpine

COPY nginx-unity.conf /etc/nginx/conf.d/default.conf

# Root build
COPY index.html /usr/share/nginx/html/index.html
COPY Build /usr/share/nginx/html/Build
COPY TemplateData /usr/share/nginx/html/TemplateData

# Secondary build (optional) reachable at /Web/
COPY Web /usr/share/nginx/html/Web


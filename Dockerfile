FROM nginx:1.27-alpine

COPY nginx-unity.conf /etc/nginx/conf.d/default.conf

# Root build hosted under /godgame/
COPY index.html /usr/share/nginx/html/godgame/index.html
COPY Build /usr/share/nginx/html/godgame/Build
COPY TemplateData /usr/share/nginx/html/godgame/TemplateData

# Secondary build (optional) reachable at /godgame/Web/
COPY Web /usr/share/nginx/html/godgame/Web


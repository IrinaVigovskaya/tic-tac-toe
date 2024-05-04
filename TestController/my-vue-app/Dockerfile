FROM node:lts-alpine

WORKDIR /app

EXPOSE 8000

COPY package.json .

RUN npm install

COPY . .

CMD [ "npm", "run", "dev" ]



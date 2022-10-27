# MongoDB as persistence layer

This project is tested against MongoDB with the following assumptions:

- `MONGODB_URI` should be set to `mongodb://[host]:[port]` as in `mongodb://127.0.0.1:27017`

## Enabling MongoDB
Your environment should contain

```sh
MONGODB_URI=mongodb://...

# optional with defaults
# MONGODB_DATABASE=cases
# MONGODB_COLLECTION=cases
```

## Local environment with docker

Start a dockerized MongoDB with
```sh
docker run --name mongodb -p 27017:27017 -d mongo

```
 Ensure `.env` contains
 ```env
 MONGODB_URI=mongodb://127.0.0.1:27017
 ``` 

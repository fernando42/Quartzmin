#!/usr/bin/env bash

sudo docker build -f ./Dockerfile -t testApp --target "runtime-scheduler" ../
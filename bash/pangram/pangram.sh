#!/usr/bin/env bash

unique=`echo $1 | tr A-Z a-z | tr -dc a-z | fold -w1 | sort -u`
unique=`echo $unique | tr -d " "`
[ ${#unique} == 26 ] && echo "true" || echo "false"
#!/usr/bin/env bash

if [ $# -ne 1 ] || [ "$(tr -dc 0-9 <<< $1)" != "$1" ]; then 
    echo "Usage: leap.sh <year>" && exit 1; 
fi

if [ $(($1 % 4)) -ne 0 ]; then echo false
else
    if [ $(($1 % 100)) -ne 0 ]; then echo true
    else
        [ $(($1 % 400)) -eq 0 ] && echo true || echo false
    fi
fi

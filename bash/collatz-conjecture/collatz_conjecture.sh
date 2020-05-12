#!/usr/bin/env bash

if [ -n "$(echo $1 | tr -d 0-9)" ] || [ $1 -eq 0 ]; then
    echo "Error: Only positive numbers are allowed" && exit 1
fi

current=$1
count=0
while [ $current -ne 1 ]; do
    [ $(($current % 2)) -eq 0 ] && current=$((current / 2)) || current=$((current * 3 + 1))
    count=$((count + 1))
done
echo $count

#!/usr/bin/env bash

result=""

IFS=" |-|_|*"
for var in $1
do
    result+=$(echo ${var:0:1} | tr a-z A-Z)
done

echo $result
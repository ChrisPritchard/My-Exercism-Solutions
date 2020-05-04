#!/usr/bin/env bash

result=0

for ((i=0; i<${#1}; i++))
do
    result=$(($result+${1:i:1}**${#1}))
done

(( $result == $1 )) && echo true || echo false
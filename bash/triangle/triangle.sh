#!/usr/bin/env bash

type=$1

if [ "$type" == "equilateral" ]; then
    [ $(bc <<< "$2 > 0 && $2 == $3 && $3 == $4") -eq 1 ] && echo "true" || echo "false"
    exit 0
fi

sides=($2 $3 $4)
IFS=$'\n' sides=($(sort <<< "${sides[*]}")); unset IFS

if [ $(bc <<< "$sides[0] > 0 && $sides[0] + $sides[1] >= $sides[2]") -eq 0 ]; then
    echo "false"
    exit 0
fi

if [ "$type" == "isosceles" ]; then
    [ $(bc <<< "$sides[0] == $sides[1] || $sides[1] == $sides[2]") -eq 1 ] && echo "true" || echo "false"
elif [ "$type" == "scalene" ]; then
    [ $(bc <<< "$sides[0] != $sides[1] && $sides[1] != $sides[2] && $sides[0] != $sides[2]") -eq 1 ] && echo "true" || echo "false"
fi
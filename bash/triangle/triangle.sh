#!/usr/bin/env bash

type=$1

if [ "$type" == "equilateral" ]; then
    [ $(bc <<< "$2 > 0 && $2 == $3 && $3 == $4") -eq 1 ] && echo "true" || echo "false"
    exit 0
fi

sides=($2 $3 $4)
IFS=$'\n' sides=($(sort <<< "${sides[*]}")); unset IFS

one=${sides[0]}
two=${sides[1]}
three=${sides[2]}

if [ $(bc <<< "$one > 0 && $one + $two >= $three") -eq 0 ]; then
    echo "false"
elif [ "$type" == "isosceles" ]; then
    [ $(bc <<< "$one == $two || $two == $three") -eq 1 ] && echo "true" || echo "false"
elif [ "$type" == "scalene" ]; then
    [ $(bc <<< "$one != $two && $two != $three && $one != $three") -eq 1 ] && echo "true" || echo "false"
else
    echo "false"
fi
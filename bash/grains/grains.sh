#!/usr/bin/env bash

if [ $1 = "total" ]
then
    # also doable with 2^64 - 1, but that feels like just printing the precomputed answer.
    total="0"
    for ((n=1;n<65;n++))
    do
        total=$(bc <<< "$total + $(sh $0 $n)")
    done
    echo $total
    exit 0
fi

[ $1 -lt 1 ] || [ $1 -gt 64 ] && echo "Error: invalid input" && exit 1
power=$(($1-1))

bc <<< "2^$power"
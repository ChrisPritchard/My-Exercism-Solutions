#!/usr/bin/env bash

if [ $1 = "square_of_sum" ]; then
    total="0"
    for ((i=1;i<=$2;i++)); do 
        total=$(bc <<< "$total + $i") 
    done
    echo $(bc <<< "$total^2")
elif [ $1 = "sum_of_squares" ]; then
    total="0"
    for ((i=1;i<=$2;i++)); do 
        total=$(bc <<< "$total + $i^2") 
    done
    echo $total
elif [ $1 = "difference" ]; then
    result=$(bc <<< "$(sh $0 square_of_sum $2) - $(sh $0 sum_of_squares $2)")
    [ $result -lt 0 ] && echo $(bc <<< "-1 * $result") || echo $result
fi
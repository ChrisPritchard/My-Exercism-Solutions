#!/usr/bin/env bash

if [ $# != 2 ]
then
    echo "Usage: hamming.sh <string1> <string2>"
    exit 1
elif [ ${#1} != ${#2} ]
then
    echo "left and right strands must be of equal length"
    exit 1
fi

result=0
left=$1
right=$2

for ((i=0;i<${#left};i++))
do
    [ ${left:$i:1} == ${right:$i:1} ] || result=$((result+1))
done

echo $result
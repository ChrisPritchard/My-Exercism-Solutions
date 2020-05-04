#!/usr/bin/env bash

if [ $# != 1 ]
then
    echo "usage: <number>"
fi

result=""

test() {
    if [ $(($1 % $2)) -eq 0 ]
    then
        result+=$3
    fi
}

test $1 3 Pling
test $1 5 Plang
test $1 7 Plong

if [ "$result" == "" ] 
then 
    echo $1 
else 
    echo $result 
fi

exit 0

#!/usr/bin/env bash

if [[ $1 =~ _*+?1?[_\-]*(?[2-9][0-9]{2})?[_\.\-]*[2-9][0-9]{2}[_\.\-]*[0-9]{4}\_* ]]; then
    result=$(echo $1 | tr -dc 0-9)
    # if [ ${#result} -eq 11 ]; then echo ${result:1:10} else echo $result; fi
    echo $result
    exit 0
fi

echo "Invalid number.  [1]NXX-NXX-XXXX N=2-9, X=0-9" && exit 1

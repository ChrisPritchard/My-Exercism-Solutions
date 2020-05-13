#!/usr/bin/env bash

error="Invalid number.  [1]NXX-NXX-XXXX N=2-9, X=0-9"
withoutspaces=$(echo $1 | tr -d ' ')
valid=$(echo $withoutspaces | tr -dc "0-9-+[].()")

if [ ${#valid} -ne ${#withoutspaces} ]; then
    echo "$error" && exit 1
fi

numbers=$(echo $1 | tr -dc "0-9")
length=${#numbers}

if [ $length -gt 11 ] || [ $length -lt 10 ]; then
    echo "$error" && exit 1
fi

if [ $length -eq 11 ]; then
    if [ ${numbers:0:1} -ne 1 ]; then
        echo "$error" && exit 1
    fi
    numbers=${numbers:1:10}
fi

if [ ${numbers:0:1} -lt 2 ] || [ ${numbers:3:1} -lt 2 ]; then
    echo "$error" && exit 1
fi

echo "$numbers"

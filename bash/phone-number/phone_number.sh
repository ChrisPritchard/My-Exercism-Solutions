#!/usr/bin/env bash

error="Invalid number.  [1]NXX-NXX-XXXX N=2-9, X=0-9"
valid=$(echo $1 | tr -dc "0-9-+[].()  ")
echo "[$valid]"

if [ ${#valid} -ne ${#1} ]; then
    echo bad chars
    echo $error && exit 1
fi

numbers=$(echo $1 | tr -dc "0-9")
length=${#numbers}

if [ $length -gt 11 ] || [ $length -lt 10 ]; then
    echo bad length
    echo $error && exit 1
fi

if [ $length -eq 11 ]; then
    if [ ${numbers:0:1} -ne 1 ]; then
        echo doesnt start with 1
        echo $error && exit 1
    fi
    numbers=${numbers:1:10}
fi

if [ ${numbers:0:1} -lt 2 ] || [ ${numbers:3:1} -lt 2 ]; then
    echo pos0 or pos3 less than 2
    echo $error && exit 1
fi

echo $numbers

# check for invalid characters
# remove all but numbers
# check length
# check first and discard if one
# check other two restricted
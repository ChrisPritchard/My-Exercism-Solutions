#!/usr/bin/env bash

sorted() {
    echo ${1,,} | fold -w1 | sort
}

result=""
for word in $2; do
    if [ "$(sorted $word)" == "$(sorted $1)" ] && [ "${word,,}" != "${1,,}" ] ; then
        result+=" $word"
    fi
done
echo ${result#' '}
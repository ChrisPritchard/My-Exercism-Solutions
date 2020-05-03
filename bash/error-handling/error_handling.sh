#!/usr/bin/env bash

main () {
    if [ $# != 1 ]
    then
        usage
    else
        message "$1"
    fi
}

usage () {
    echo "Usage: error_handling.sh <person>"
    return 1
}

message () {
    echo "Hello, $1"
    return 0
}

main "$@"
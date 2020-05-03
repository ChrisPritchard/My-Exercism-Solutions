#!/usr/bin/env bash

if [ $# -gt 0 ]
then
    echo "One for $1, one for me."
else
    echo "One for you, one for me."
fi

exit 0
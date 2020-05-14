#!/usr/bin/env bash

# test all sides greater than 0, and when ordered, the sum of the first to is >= the largest
# then go by type of triangle.

type=$1
sides="$2 $3 $4"
item() {
    IFS=' '
    echo "$sides" | sort -n | sed "${1}q;d"
}

echo $(item 1)
echo $(item 2)
echo $(item 3)
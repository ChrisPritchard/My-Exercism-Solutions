#!/usr/bin/env bash

valid=$(grep "^\ *+\?1\?\ *(\?[2-9]\d\d)\?[\ \.\-]*[2-9]\d\d[\ \.\-]*\d\d\d\d *$" <<< $1)
if [[ -z "$valid" ]]; then echo "Invalid number.  [1]NXX-NXX-XXXX N=2-9, X=0-9" && exit 1; fi

result="${valid//[^0-9]}"
if [ ${#result} -eq 11 ]; then echo ${result:1:10}; else echo $result; fi

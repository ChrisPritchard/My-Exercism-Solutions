#!/usr/bin/env bash

error="Invalid number.  [1]NXX-NXX-XXXX N=2-9, X=0-9"
trimmed=$(echo $1 | sed 's/[^0-9]*//g')
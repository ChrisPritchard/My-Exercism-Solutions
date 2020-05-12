#!/usr/bin/env bash

# note: bash on osx (at least the version im using) doesnt support declare -A
A=0
C=0
G=0
T=0
error=0

for c in $(echo $1 | fold -w1); do
    case "$c" in
        "G")
            G=$(($G+1)) ;;
        "C")
            C=$(($C+1)) ;;
        "T")
            T=$(($T+1)) ;;
        "A")
            A=$(($A+1)) ;;
        *)
            error=1 ;;
    esac
done

if [ "$error" == "1" ]; then
    echo "Invalid nucleotide in strand"
    exit 1
fi

echo -e "A: $A\nC: $C\nG: $G\nT: $T"
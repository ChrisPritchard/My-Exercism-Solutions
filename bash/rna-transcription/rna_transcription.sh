#!/usr/bin/env bash

# this is simpler with tr or regex etc, but i haven't used a case expression in bash before.

result=""
error=0
for c in $(echo $1 | fold -w1); do
    case "$c" in
        "G")
            result+="C" ;;
        "C")
            result+="G" ;;
        "T")
            result+="A" ;;
        "A")
            result+="U" ;;
        *)
            error=1 ;;
    esac
done
if [ "$error" == "1" ]; then
    echo "Invalid nucleotide detected."
    exit 1
fi
echo $result
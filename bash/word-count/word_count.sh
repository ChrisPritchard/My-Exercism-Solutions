#!/usr/bin/env bash

echo -e $1 | tr A-Z a-z | grep -oh "\w[a-z0-9'-]*" | sort | uniq -c | awk '{printf("%s: %s\n", $2, $1)}'


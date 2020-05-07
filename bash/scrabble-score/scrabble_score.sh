#!/usr/bin/env bash

read -r -d '' scores <<'EOF'
A, E, I, O, U, L, N, R, S, T       1
D, G                               2
B, C, M, P                         3
F, H, V, W, Y                      4
K                                  5
J, X                               8
Q, Z                               10
EOF

total=0
for char in $(echo $1 | fold -w1)
do
    char=$(echo $char | tr a-z A-Z)
    score=$(echo "$scores" | grep $char | rev | cut -d" " -f1 | rev)
    total=$(($total+$score))
done

echo $total
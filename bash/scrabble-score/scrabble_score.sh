#!/usr/bin/env bash

sets=( "AEIOULNRST" "DG" "BCMP" "FHVWY" "K" "JX" "QZ" )
scores=( 1 2 3 4 5 8 10 )

total=0
for ((i=0;i<${#1};i++))
do
    char=$(echo ${1:$i:1} | tr a-z A-Z)
    for ((j=0;j<${#sets};j++)) 
    do 
        set=${sets[$j]}
        contained=$(echo $set | grep $char | wc -l)
        if [ $contained -eq 1 ]
        then 
            score=${scores[$j]}
            total=$(($total+$score))
        fi
    done
done

echo $total
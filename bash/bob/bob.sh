#!/usr/bin/env bash

trimmed=`echo $1 | tr -d ' \t\n\r'`
if [ -z $trimmed ]
then
    echo "Fine. Be that way!"
else
    lastchar=${trimmed:${#trimmed}-1:1}
    uppercase_only=`echo $trimmed | tr -d a-z`
    no_uppercase=`echo $trimmed | tr -d A-Z`
    if [ ${#uppercase_only} -eq ${#trimmed} ] && [ ${#no_uppercase} -lt ${#trimmed} ]
    then
        [ $lastchar = "?" ] && echo "Calm down, I know what I'm doing!" || echo "Whoa, chill out!"
    else
        [ $lastchar = "?" ] && echo "Sure." || echo "Whatever."
    fi
fi

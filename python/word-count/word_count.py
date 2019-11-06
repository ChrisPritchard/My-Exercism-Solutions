def count_words(sentence):
    for b in ":!@.$&%^":
        sentence = sentence.replace(b, "")
    for w in "\n\r\t,_":
        sentence = sentence.replace(w, " ")

    counts = {}
    for word in sentence.lower().split():
        word = word.strip('\'')
        if word in counts:
            counts[word] += 1
        else:
            counts[word] = 1
    return counts

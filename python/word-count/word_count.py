def count_words(sentence):
    counts = {}
    for word in sentence.lower().translate(None, ":!@").split():
        if counts[word]:
            counts[word] += 1
        else:
            counts[word] = 1
    return counts

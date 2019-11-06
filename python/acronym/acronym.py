def abbreviate(words):
    letters = [w[0].upper() for w in 
        words.replace('-', ' ').replace('_', ' ').split()]
    return "".join(letters)

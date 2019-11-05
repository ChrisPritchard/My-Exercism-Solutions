def is_isogram(string):
    found = set()
    for c in string.lower():
        if c in found:
            return False
        elif c != '-' and c != ' ':
            found.add(c)
    return True


rules = """
    A, E, I, O, U, L, N, R, S, T       1
    D, G                               2
    B, C, M, P                         3
    F, H, V, W, Y                      4
    K                                  5
    J, X                               8
    Q, Z                               10"""

scores = dict()
for line in rules.split('\n'):
    letters = line.replace(',', '').split()
    for i in range(0, len(letters)):
        scores[letters[i]] = int(letters[-1])

def score(word):
    return sum(scores[c] for c in word.upper())

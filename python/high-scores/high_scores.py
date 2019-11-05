def latest(scores):
    return scores[-1]


def personal_best(scores):
    return max(scores)


def personal_top_three(scores):
    result = sorted(scores, reverse=True)
    if len(result) < 3:
        return result
    return result[0:3]

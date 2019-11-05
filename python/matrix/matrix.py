class Matrix(object):

    def __init__(self, matrix_string):
        self.rows = []
        for line in matrix_string.split('\n'):
            row = [int(cell) for cell in line.split()]
            self.rows.append(row)

    def row(self, index):
        return self.rows[index-1]

    def column(self, index):
        return [row[index-1] for row in self.rows]
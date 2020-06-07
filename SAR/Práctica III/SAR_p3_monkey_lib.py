#!/usr/bin/env python
#! -*- encoding: utf8 -*-
# 3.- Mono Library

import pickle
import random
import re
import sys

## Nombres:
## author: Antoni Mestre Gascón
## author: Mario Campos Mocholí


def sort_index(d):
    for k in d:
        l = sorted(((y, x) for x, y in d[k].items()), reverse=True)
        d[k] = (sum(x for x, _ in l), l)


class Monkey():

    def __init__(self):
        self.r1 = re.compile('[.;?!]')
        self.r2 = re.compile('\W+')


    def index_sentence(self, sentence : str, tri : bool):
        """
        Metodo que preprocesa una cadena y la analiza por bigramas o por trigramas.

        Parametros:
            sentence (str): Cadena a analizar.
            tri (bool): Analisis por trigramas; por bigramas en caso contrario.
        """
        sentence = self.r2.sub(' ', sentence).split()
        if sentence is not []:
            sentence = ['$'] + sentence + ['$']
            if tri:
                for wordInd in range(len(sentence) - 2):
                    trio = sentence[wordInd:wordInd + 3]
                    self.updateIndex((trio[0], trio[1]), 'bi')
                    self.updateIndex(((trio[0], trio[1]), trio[2]), 'tri')

                self.updateIndex((sentence[len(sentence) - 2], sentence[len(sentence) - 1]), 'bi')
            else:
                for wordInd in range(len(sentence) - 1):
                    pair = sentence[wordInd:wordInd + 2]
                    self.updateIndex(pair, 'bi')


    def compute_index(self, filename : str, tri : bool):
        """
        Metodo que obtiene los bigramas y/o trigramas de un fichero de texto.

        Parametros:
            filename (str): Nombre del fichero.
            tri (bool): Obtención de los bigramas y trigramas; solo bigramas en caso contrario.
        """
        self.index = {'name': filename, "bi": {}}
        if tri:
            self.index["tri"] = {}
        with open(filename, 'r') as fh:
            prevLine = ""
            for line in fh:
                line = line.rstrip().lower()
                sentences = []
                last = 0

                if not line and prevLine:
                    sentences = [prevLine]
                    last = 1
                else:
                    sentences = self.r1.split(prevLine + " " + line)
                prevLine = ""

                if sentences:
                    lenSentences = len(sentences) - 1 or last
                    for sentenceIndex in range(lenSentences):
                        self.index_sentence(sentences[sentenceIndex], tri)

                    if not last:
                        lastSentence = sentences[len(sentences) - 1]
                        if lastSentence and not last:
                            prevLine += " " + lastSentence if prevLine else lastSentence
                        else:
                            prevLine = ""
            if prevLine:
                self.index_sentence(prevLine, tri)

        sort_index(self.index['bi'])
        if tri:
            sort_index(self.index['tri'])


    def load_index(self, filename):
        with open(filename, "rb") as fh:
            self.index = pickle.load(fh)


    def save_index(self, filename):
        with open(filename, "wb") as fh:
            pickle.dump(self.index, fh)


    def save_info(self, filename):
        with open(filename, "w") as fh:
            print("#" * 20, file=fh)
            print("#" + "INFO".center(18) + "#", file=fh)
            print("#" * 20, file=fh)
            print("filename: '%s'\n" % self.index['name'], file=fh)
            print("#" * 20, file=fh)
            print("#" + "BIGRAMS".center(18) + "#", file=fh)
            print("#" * 20, file=fh)
            for word in sorted(self.index['bi'].keys()):
                wl = self.index['bi'][word]
                print("%s\t=>\t%d\t=>\t%s" % (word, wl[0], ' '.join(
                    ["%s:%s" % (x[1], x[0]) for x in wl[1]])), file=fh)
            if 'tri' in self.index:
                print(file=fh)
                print("#" * 20, file=fh)
                print("#" + "TRIGRAMS".center(18) + "#", file=fh)
                print("#" * 20, file=fh)
                for word in sorted(self.index['tri'].keys()):
                    wl = self.index['tri'][word]
                    print("%s\t=>\t%d\t=>\t%s" % (word, wl[0], ' '.join(
                        ["%s:%s" % (x[1], x[0]) for x in wl[1]])), file=fh)


    def updateIndex(self, pair : slice, typeIn : str):
        """
        Metodo que actualiza el diccionario de indices a partir de una tupla.

        Parametros:
            pair (slice): Tupla de palabras.
            typeIn (str): 'bi' o 'tri', segun el diccionario a actualizar.
        """
        firstEl = self.index[typeIn].get(pair[0])
        if not firstEl:
            self.index[typeIn][pair[0]] = {pair[1]: 1}
        else:
            self.index[typeIn][pair[0]][pair[1]] = self.index[typeIn][pair[0]].get(pair[1], 0) + 1


    def generate_sentences(self, n : int = 10):
        """
        Metodo que genera un numero determinado de frases aleatorias.

        Parametros:
            n (int) (default: 10): Numero de frases a generar.
        """
        if 'tri' in self.index:
            for i in range(n):
                firstList = [(self.index['tri'][i][0], i)
                             for i in self.index['tri'] if '$' in i]
                selected = self.getRandomWord(False, False, firstList)

                selectedWord = self.getRandomWord('tri', selected)
                res = selected[1] + " " + selectedWord + " "
                selectedWord = (selected[1], selectedWord)

                while '$' not in selectedWord:
                    word = self.getRandomWord('tri', selectedWord)
                    res += word + " "
                    selectedWord = (selectedWord[1], word)

                print(res[:len(res) - 3])
        else:
            for i in range(n):
                selectedWord = self.getRandomWord('bi', '$')
                res = selectedWord + " "
                while '$' not in selectedWord:
                    selectedWord = self.getRandomWord('bi', selectedWord)
                    res += selectedWord + " "

                print(res[:len(res) - 3])

            
    def getRandomWord(self, typeIn  : str, indexEntry : str, prevList : bool = False):
        """
        Metodo que devuelve una palabra aleatoria.

        Parametros:
            typeIn (str): 'bi' o 'tri', segun el diccionario en el que buscar.
            indexEntry (str): Entrada del diccionario.
            prevList (bool) (default: False): Segun frecunecia de la aparicion.
        
        Returns:
            str: Palabra escogida.
        """
        if prevList:
            listEntry = prevList
        else:
            listEntry = self.index[typeIn][indexEntry][1]

        numEl = sum(map(lambda x: x[0], listEntry))
        selected = random.randint(1, numEl)
        for wordIn, word in listEntry:
            if selected <= wordIn:
                selectedWord = word
                break
            selected -= wordIn

        return selectedWord


if __name__ == "__main__":
    print("Este fichero es una librería, no se puede ejecutar directamente")
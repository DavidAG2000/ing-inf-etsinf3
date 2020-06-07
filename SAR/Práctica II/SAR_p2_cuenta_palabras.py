#! -*- encoding: utf8 -*-

## Nombres: 
# Antoni Mestre GascÃ³n
# Mario Campos MocholÃ­

########################################################################
########################################################################
###                                                                  ###
###  Todos los metodos y funciones que se aÃ±adas deben documentarse  ###
###                                                                  ###
########################################################################
########################################################################

import argparse
import re
import sys

# Metodo que ordena un diccionario por values
def sort_dic_by_values(d, asc=True):
    return sorted(d.items(), key=lambda a: (-a[1], a[0]))

# Metodo que ordena un diccionario por key
def sort_dic_by_keys(d, asc=True):
    return sorted(d.items(), key=lambda item: item[0])

class WordCounter:

    def __init__(self):
        """
           Constructor de la clase WordCounter
        """
        self.clean_re = re.compile('\W+')

    def write_stats(self, filename, stats, use_stopwords, full):
        """
        Este metodo escribe en fichero las estadisticas de un texto
            

        :param 
            filename: el nombre del fichero destino.
            stats: las estadisticas del texto.
            use_stopwords: booleano, si se han utilizado stopwords
            full: boolean, si se deben mostrar las stats completas

        :return: None
        """

        with open(filename, 'w') as fh:
            # Numero de lineas
            fh.write(("Lines: {}\n").format(stats['nlines']))

            # Numero de palabras
            fh.write(("Number words (including stopwords): {}\n").format(stats['nwords']))
            if use_stopwords:
                fh.write(("Number words (without stopwords): {}\n").format(stats['nswords']))
                

            # Talla vocabulario
            fh.write(("Vocabulary size: {}\n").format(len(stats['word'])))

            # Numero de simbolos
            cont = 0
            for (symbol, num) in stats['symbol'].items():
                cont += num
            fh.write(("Number of symbols: {}\n").format(cont))

            # Talla alfabeto
            fh.write(("Number of different symbols: {}\n").format(len(stats['symbol'])))

            # Lista de palabras ordenadas alfabeticamente
            fh.write("Words (alphabetical order):\n")
            if full:
                for (word, num) in sort_dic_by_keys(stats['word']):
                    fh.write(("     {}: {}\n").format(word, num))
            else:
                cont = 0
                for (word, num) in sort_dic_by_keys(stats['word']):
                    if cont == 20:
                        break
                    fh.write(("     {}: {}\n").format(word, num))
                    cont += 1

            # Lista de palabras ordenadas por frecuencia
            fh.write("Words (by frecuency):\n")
            if full:
                for (word, num) in sort_dic_by_values(stats['word']):
                    fh.write(("     {}: {}\n").format(word, num))
            else:
                cont = 0
                for (word, num) in sort_dic_by_values(stats['word']):
                    if cont == 20:
                        break
                    fh.write(("     {}: {}\n").format(word, num))
                    cont += 1

            # Lista de letras ordenadas alfabeticamentes
            fh.write("Symbols (alphabetical order):\n")
            if full:
                for (word, num) in sort_dic_by_keys(stats['symbol']):
                    fh.write(("     {}: {}\n").format(word, num))
            else:
                cont = 0
                for (word, num) in sort_dic_by_keys(stats['symbol']):
                    if cont == 20:
                        break
                    fh.write(("     {}: {}\n").format(word, num))
                    cont += 1

            # Lista de letras ordenadas por frecuencia
            fh.write("Symbols (by frecuency):\n")
            if full:
                for (word, num) in sort_dic_by_values(stats['symbol']):
                    fh.write(("     {}: {}\n").format(word, num))
            else:
                cont = 0
                for (word, num) in sort_dic_by_values(stats['symbol']):
                    if cont == 20:
                        break
                    fh.write(("     {}: {}\n").format(word, num))
                    cont += 1

            # OpciÃ³n de bigramas
            if stats['biword'] != None and stats['bisymbol'] != None:
                # Lista de bipalabras ordenadas alfabeticamente
                fh.write("Word pairs (alphabetical order):\n")
                if full:
                    for (word, num) in sort_dic_by_keys(stats['biword']):
                        fh.write(("     {}: {}\n").format(word, num))
                else:
                    cont = 0
                    for (word, num) in sort_dic_by_keys(stats['biword']):
                        if cont == 20:
                            break
                        fh.write(("     {}: {}\n").format(word, num))
                        cont += 1

                # Lista de bipalabras ordenadas por frecuencia
                fh.write("Word pairs (by frecuency):\n")
                if full:
                    for (word, num) in sort_dic_by_values(stats['biword']):
                        fh.write(("     {}: {}\n").format(word, num))
                else:
                    cont = 0
                    for (word, num) in sort_dic_by_values(stats['biword']):
                        if cont == 20:
                            break
                        fh.write(("     {}: {}\n").format(word, num))
                        cont += 1

                # Lista de bisimboles ordenados alfabeticamente
                fh.write("Symbol pairs (alphabetical order):\n")
                if full:
                    for (word, num) in sort_dic_by_keys(stats['bisymbol']):
                        fh.write(("     {}: {}\n").format(word, num))
                else:
                    cont = 0
                    for (word, num) in sort_dic_by_keys(stats['bisymbol']):
                        if cont == 20:
                            break
                        fh.write(("     {}: {}\n").format(word, num))
                        cont += 1

                # Lista de bisimboles ordenados pro frecuncia
                fh.write("Symbol pairs (by frecuency):\n")
                if full:
                    for (word, num) in sort_dic_by_values(stats['bisymbol']):
                        fh.write(("     {}: {}\n").format(word, num))
                else:
                    cont = 0
                    for (word, num) in sort_dic_by_values(stats['bisymbol']):
                        if cont == 20:
                            break
                        fh.write(("     {}: {}\n").format(word, num))
                        cont += 1


    def file_stats(self, filename, lower, stopwordsfile, bigrams, full):
        """
        Este metodo calcula las estadisticas de un fichero de texto
            

        :param 
            filename: el nombre del fichero.
            lower: booleano, se debe pasar todo a minusculas?
            stopwordsfile: nombre del fichero con las stopwords o None si no se aplican
            bigram: booleano, se deben calcular bigramas?
            full: booleano, se deben montrar la estadisticas completas?

        :return: None
        """

        stopwords = [] if stopwordsfile is None else open(stopwordsfile).read().split()

        # Diccionario para las variables resultado
        sts = {
                'nwords': 0,
                'nswords': 0,
                'nlines': 0,
                'word': {},
                'symbol': {},
                'biword': {},
                'bisymbol': {}
                }

        # Nombre del fichero
        new_filename = filename.split('.')
        sufix = ''
        if lower:
            sufix += 'l'
        if stopwordsfile is not None:
            sufix += 's'
        if bigrams:
            sufix += 'b'
        if full:
            sufix += 'f'
        if sufix != '':
            sufix = '_' + sufix
        if len(new_filename) != 1:
            new_filename = new_filename[0] + sufix + '_stats.' + new_filename[1]
        else:
            new_filename = filename + sufix + '_stats'

        # Generador de estadisticas
        with open(filename, 'r') as book:
            for line in book.readlines():
                # Contador de lineas
                sts['nlines'] += 1
                # Limpieza de simbolos no alfanumericos
                line = re.sub(self.clean_re, ' ', line)
                # Si hay que pasar a minuscula
                if lower:
                    line = line.lower()
                # Separamos cada frase en palabras !!IMPORTANTE: no poner split(' ')Â¡Â¡
                line = line.split()
                # Bigramas, ultima palabra computada
                lastword = '$'
                for word in line:
                    # Contador de palabras (con stopwords)
                    sts['nwords'] = sts['nwords'] + 1
                    if stopwords != []:
                        # Diccionario de bigramas - ocurrencia (sin stopwords)
                        if lastword not in stopwords and word not in stopwords:
                            pair = lastword + ' ' + word
                            if pair not in sts['biword']:
                                sts['biword'][pair] = 1
                            else:
                                sts['biword'][pair] = sts['biword'][pair] + 1
                        lastword = word
                    
                    if word not in stopwords:
                        # Contador de palabras (sin stopwords)
                        sts['nswords'] = sts['nswords'] + 1
                        # Diccionario de palabras - ocurrencia
                        if word not in sts['word']:
                            sts['word'][word] = 1
                        else:
                            sts['word'][word] = sts['word'][word] + 1
                        if stopwords == []:
                            # Diccionario de bigramas - ocurrencia (sin stopwords)
                            pair = lastword + ' ' + word
                            if pair not in sts['biword']:
                                sts['biword'][pair] = 1
                            else:
                                sts['biword'][pair] = sts['biword'][pair] + 1
                            lastword = word

                        lastchar = ''
                        for char in word:
                            # Diccionario de letra - ocurrencia
                            if char not in sts['symbol']:
                                sts['symbol'][char] = 1
                            else:
                                sts['symbol'][char] = sts['symbol'][char] + 1
                            # Diccionario de biletra - ocurrencia
                            if lastchar != '':
                                lastchar = lastchar + char
                                if lastchar not in sts['bisymbol']:
                                    sts['bisymbol'][lastchar] = 1
                                else:
                                    sts['bisymbol'][lastchar] = sts['bisymbol'][lastchar] + 1
                            lastchar = char
                if lastword != '$':
                    if stopwords == []:
                        pair = lastword + ' ' + '$'
                        if pair not in sts['biword']:
                            sts['biword'][pair] = 1
                        else:
                            sts['biword'][pair] = sts['biword'][pair] + 1
                    elif lastword not in stopwords:
                        pair = lastword + ' ' + '$'
                        if pair not in sts['biword']:
                            sts['biword'][pair] = 1
                        else:
                            sts['biword'][pair] = sts['biword'][pair] + 1
                    lastword = '$'

        self.write_stats(new_filename, sts, stopwordsfile is not None, full)
        
    def compute_files(self, filenames, **args):
        """
        Este metodo calcula las estadisticas de una lista de ficheros de texto
            

        :param 
            filenames: lista con los nombre de los ficheros.
            args: argumentos que se pasan a "file_stats".

        :return: None
        """

        for filename in filenames:
            self.file_stats(filename, **args)

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description='Compute some statistics from text files.')
    parser.add_argument('file', metavar='file', type=str, nargs='+',
                        help='text file.')

    parser.add_argument('-l', '--lower', dest='lower', action='store_true', default=False, 
                    help='lowercase all words before computing stats.')

    parser.add_argument('-s', '--stop', dest='stopwords', action='store',
                    help='filename with the stopwords.')

    parser.add_argument('-b', '--bigram', dest='bigram', action='store_true', default=False, 
                    help='compute bigram stats.')

    parser.add_argument('-f', '--full', dest='full', action='store_true', default=False, 
                    help='show full stats.')

    args = parser.parse_args()
    wc = WordCounter()
    wc.compute_files(args.file, lower=args.lower, stopwordsfile=args.stopwords, bigrams=args.bigram, full=args.full)
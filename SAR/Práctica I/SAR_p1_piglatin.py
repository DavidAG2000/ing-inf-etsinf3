#!/usr/bin/env python
#! -*- encoding: utf8 -*-

# :author: Antoni Mestre Gascón
# :author: Mario Campos Mocholí
# 1.- Pig Latin

import sys
import re
import string


class Translator():
    vocales = ['a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y']
    signos = [',', ';', '.', '?', '!']

    def __init__(self, punt=None):
        """
        Constructor de la clase Translator

        :param punt(opcional): una cadena con los signos de puntuación
                                que se deben respetar
        :return: el objeto de tipo Translator
        """
        if punt is None:
            self.re = re.compile("(\w+)([.,;?!]*)")
        else:
            self.re = re.compile("(\w+)(["+punt+"]*)")


    def translate_word(self, word):
        """
        Este método recibe una palabra en inglés y la traduce a Pig Latin

        :param word: la palabra que se debe pasar a Pig Latin
        :return: la palabra traducida
        """
        flag_signo_final = False
        flag_mayusucula_inicial = False
        flag_mayuscula_total = False
        flag_vocal = False

        # Caso: Caracter inicial no es una letra
        if not word[0].isalpha():
            return word

        # Caso: Caracter final es un simbolo
        signo = ''
        if word[len(word) - 1] in self.signos:
            flag_signo_final = True
            signo = word[len(word) - 1]
            word = word[:len(word) - 1] # Quitamos el ultimo caracter

        # Caso: Palabra en mayusucula / Primera en mayuscula
        if word.isupper():
            flag_mayuscula_total = True
        elif word[0].isupper():
            flag_mayusucula_inicial = True

        # Caso: Primer caracter es vocal
        if word[0] in self.vocales:
            flag_vocal = True

        # Traducción de la palabra
        word = word.lower()
        if flag_vocal: # Empieza por vocal
            word = word + 'yay'
        else: # Empieza por consonante
            while word[0] not in self.vocales:
                word = word[1:] + word[0]
            word = word + 'ay'

        # Post tratamiento
        if flag_mayuscula_total:
            word = word.upper()
        if flag_mayusucula_inicial:
            word = word.capitalize()
        if flag_signo_final:
            word = word + signo

        
        return word


    def translate_sentence(self, sentence):
        """
        Este método recibe una frase en inglés y la traduce a Pig Latin

        :param sentence: la frase que se debe pasar a Pig Latin
        :return: la frase traducida
        """
        res = ''
        aux = sentence.split(" ")

        # Traducimos palabra por palabra
        for palabra in aux:
            res += self.translate_word(palabra.strip()) + ' '

        # Actualizamos el formato resultante
        res = res.strip()

        return res


    def translate_file(self, filename):
        """
        Este método recibe un fichero y crea otro con su tradución a Pig Latin

        :param filename: el nombre del fichero que se debe traducir
        :return: True / False
        """
        res = ''
        # Leemos el archivo
        lectura = open(filename, 'r')
        for linea in lectura.readlines():
            res += self.translate_sentence(linea) + '\n'
        lectura.close()

        # Añadimos el sufijo al nombre del archivo
        aux = filename.split('.')
        if len(aux) != 1:
            aux = aux[0] + '_latin.' + aux[1]
        else:
            aux = filename + '_latin'

        # Escribimos el fichero
        escritura = open(aux, 'w')
        res = res[:len(res) -1] # Quitamos linea en blanco
        escritura.write(res)   


if __name__ == "__main__":
    if len(sys.argv) > 2:
        print('Syntax: python %s [filename]' % sys.argv[0])
        exit
    else:
        t = Translator()
        if len(sys.argv) == 2:
            t.translate_file(sys.argv[1])
        else:
            while True:
                sentence = input("ENGLISH: ")
                if len(sentence) < 2:
                    break
                print("PIG LATIN:", t.translate_sentence(sentence))
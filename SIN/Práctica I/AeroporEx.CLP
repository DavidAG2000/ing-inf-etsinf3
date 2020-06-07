(deffacts bf (ax M 6 A 1 2 3 4 E 1 1) ;(ax M posició llista_mal_enganx A llista_mal_pendents E identificador tipus)
(m 1 0 3 12) (m 2 0 5 18) (m 3 1 9 20) (m 4 6 9 14) ;(m identificador origen desti pes)
(v 2 2 2) ;(v identificador posició tipus)
(r 0 1) (r 1 0) (r 1 3) (r 1 5) (r 3 1) (r 3 4) (r 4 3) (r 4 2) ;(r origen desti)
(r 2 4) (r 2 0) (r 0 2) (r 5 1) (r 5 9) (r 9 5) (r 9 6) (r 6 9)
(r 6 8) (r 8 6) (r 8 7) (r 7 8) (r 7 5) (r 5 7))

(defrule carrega 
				(ax M ?p $?x1 A $?x2 ?m $?x3 E ?i ?t)
                (m ?m ?p ?d ?P)
				(test (or (and (< ?P 16) (eq ?t 1)) (and (> ?P 15) (eq ?t 2))))
				=>
				(assert (ax M ?p ?m $?x1 A $?x2 $?x3 E ?i ?t)))

(defrule moure2
                (ax M ?p $?x A $?y E 0 0) (r ?p ?d) (r ?d ?f)
				=>
				(assert (ax M ?f $?x A $?y E 0 0)))
			  
(defrule mou 
				(ax M ?p $?x) (r ?p ?d)
				=>
				(assert (ax M ?d $?x)))
		
(defrule descarr 
				(ax M ?d $?x1 ?m $?x2 A $?x3 E ?i ?t)
                (m ?m ? ?d ?P)
				=> 
				(assert (ax M ?d $?x1 $?x2 A $?x3 E ?i ?t)))
			 
(defrule enganxar 
				(ax M ?p A $?x E 0 0) 
				(v ?i ?p ?t)
				=> 
				(assert (ax M ?p A $?x E ?i ?t)))

(defrule desenganxar 
				(ax M ?p A $?x E ?i ?t)
				(test (neq ?i 0))
				=> 
				(assert (ax M ?p A $?x E 0 0))
				(assert (v ?i ?p ?t)))
			  
(defrule meta 
				(declare (salience 2))(ax M ?p A E ? ?)
				=> 
				(printout t "Solucio trobada!" crlf)
				(halt))
	

(watch facts)
(watch activations)
(set-strategy breadth)
(reset)
(run)
(exit)

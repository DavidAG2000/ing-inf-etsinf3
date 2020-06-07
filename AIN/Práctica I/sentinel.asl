/*
	Autores: Antoni Mestre Gascón & Mario Campos Mocholí
	Nombre: Luchador Sentinel
	Estrategia: Agente agresivo que va al centro a esperar a los
		agentes moribundos que vayan a por paquetes mientras
		roba todos los paquetes que vea. Más formalmente, va
		al centro del mapa, vigila las 4 entradas, si ve un
		'aliado' le dispara y si ve un paquete, lo recoge
		sin importar su vida o munición.
*/

/* CREENCIAS INICIALES */
centro([130, 0, 130]). 									// Centro del mapa
esquinas([[0,0,0],[250,0,0],[250,0,250],[0,0,250]]). 	// Esquinas del mapa
vidaMin(20). 											// Vida mínima para huir



/* CREENCIAS A DISPARAR */
/* 
	Creencia que se añade cuando se suelta la bandera (empieza el juego).
*/
+flag(F): team(200)
    <-
	entrada(0);
	!irAlCentro.


/* 
	Creencia que se añade cuando se ve a un 'aliado' y puede vencerlo.
	Condiciones: "Vida > Health" y "con munición"
*/
+friends_in_fov(ID, Type, Angle, Distance, Health, Position): health(H) & H > Health & ammo(A) & A > 0
    <-
	-patruyando;
	!irAMatar(Position).
	
	
/* 
	Creencia que se añade cuando se ve a un 'aliado' y no puede vencerlo.
	Condiciones: "Vida <= Health" o "sin munición"
*/
+friends_in_fov(ID, Type, Angle, Distance, Health, Position): health(H) & (H <= Health | ammo(A) & A <= 0)
    <-
	!irAlCentro(Position).


/* 
	Creencia que se añade cuando se ve un paquete de salud.
	Condiciones: "Tipo del paquete = salud", "no yendo ya a por un paquete de vida" y "faltando vida"
*/	
+packs_in_fov(ID, Type, Angle, Distance, Health, Position): Type = 1001 & (not yendoAPorPaquete(1001)) & health(H) & H < 100
	<-
	-patruyando;
	yendoAPorPaquete(1001);
	!irAPorPaquete.
	
	
/* 
	Creencia que se añade cuando se ve un paquete de munición.
	Condiciones: "Tipo del paquete = munición", "no yendo ya a por otro paquete" y "faltando munición"
*/	
+packs_in_fov(ID, Type, Angle, Distance, Health, Position): Type = 1002 & (not yendoAPorPaquete(_)) & ammo(A) & A < 100
	<-
	-patruyando;
	yendoAPorPaquete(1002);
	!irAPorPaquete.


/* 
	Creencia que se añade cuando llegas a un destino de ".goto(...)".
	Condición: "No estar patrullando"
*/	
+target_reached(T): not patrullando
    <-
	!irAlCentro;
	+patruyando;
	!patrullar.



/* PLANES */
/* 
	Plan para ir al centro del mapa.
*/
+!irAlCentro
	<-
	?centro(Pos);
	.goto(Pos).


/* 
	Plan para perseguir un 'aliado' hasta matarlo.
	Condiciones: "Vida > vidaMin"
*/
+!irAMatar(Position): health(H) & vidaMin(VM) & H > VM
	<-
	.shoot(4, Position);
	.look_at(Position);
	.goto(Position).
	
	
/* 
	Plan para cuando no se consigue perseguir un 'aliado'
*/
-!irAMatar(_)
	<-
	!irAlCentro.
	
	
/* 
	Plan para ir a por un paquete.
*/
+!irAPorPaquete(Position)
	<-
	.goto(Position);
	-yendoAPorPaquete(_);
	!irAlCentro.
	
	
/* 
	Planes para patrullar el centro.
*/
+!patrullar: entrada(E) & E < 4 & patruyando
	<-
	?esquinas(Esq);
	.nth(E, Esq, R);
	.look_at(R);
	.wait(500);
	-entrada(_);
	+entrada(E + 1);
	!patrullarElCentro.
		
+!patrullar: entrada(E) & E = 4 & patruyando
	<-
	-entrada(_);
    +entrada(0);
	!patrullarElCentro.
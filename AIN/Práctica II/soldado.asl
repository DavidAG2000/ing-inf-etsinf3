/*
	Autores: Antoni Mestre Gascón & Mario Campos Mocholí
	Nombre: Soldado
*/

/* Creencia que se dispara cuando se inicia la partida */
+flag(F)
	<-
	.print("PATRULLA");
	!generarPatrulla.


/* ESTRATEGIA DE PATRULLA EN ROMBO (EXTERIOR) */
/* Generamos unos puntos de control en rombo */
+!generarPatrulla
	<-
	.get_service("general");
	?flag(F);
	.circuloExterior(F, C);
	+control_points(C);
	.length(C, L);
	+total_control_points(L);
	+patrolling;
	+patroll_point(0).

+target_reached(T): patrolling
	<-
	?patroll_point(P);
	-+patroll_point(P + 1);
	-target_reached(T).

+patroll_point(P): total_control_points(T) & P < T
	<-
	?control_points(C);
	.nth(P, C, A);
	.goto(A).

+patroll_point(P): total_control_points(T) & P == T
	<-
	-patroll_point(P);
	+patroll_point(0).
  

/* ESTRATEGIA DE PAQUETES DE SALUD */
/* Creencia que anula la estrategia */
+health(H): H >= 20 & solicitandoSalud
	<-
	-solicitandoSalud.

/* Creencia que dispara la estrategia */
+health(H): H < 20 & not solicitandoSalud
	<-
	+solicitandoSalud.

/* Solicitud al general */
+solicitandoSalud
	<-
	-+medicoPOS([]);
	-+medicoID([]);
	.get_service("general");
	.wait(500);
	?general(General);
	?position(Pos);
	.send(General, tell, solicitudDeSalud(Pos));
	.wait(1500).



/* ESTRATEGIA DE PAQUETES DE MUNICIÓN */
/* Creencia que anula la estrategia */
+ammo(A): A >= 20 & solicitandoMunicion
	<-
	-solicitandoMunicion.

/* Creencia que dispara la estrategia */
+ammo(A): A < 20 & not solicitandoMunicion
	<-
	+solicitandoMunicion.

/* Solicitud al general */
+solicitandoMunicion
	<-
	-+operativoPOS([]);
	-+operativoID([]);
	.get_service("general");
	.wait(500);
	?general(General);
	?position(Pos);
	.send(General, tell, solicitudDeMunicion(Pos));
	.wait(1500).


//* ESTRATEGIA PARA IR EN COLMENA A POR UN ENEMIGO */
/* Visualizo un enemigo y no he avisado al General */	
+enemies_in_fov(_, _, _, _, _, Position): not solicitandoAtaque & not atacando
	<-
	+solicitandoAtaque;
	?general(General);
	.send(General, tell, solicitudDeColmena(Position));
	.look_at(Position);
    .shoot(10, Position).

/* Visualizo un enemigo */
+enemies_in_fov(_, _, _, _, _, Position): solicitandoAtaque | atacando
	<-
	.look_at(Position);
	
	+puedoDisparar;
	while (friends_in_fov(Q,W,E,R,T,AmigoPos) & puedoDisparar) {
		?position(MiPosicion);
		.fuegoAmigo(MiPosicion, Position, AmigoPos, Aux);
		if (Aux) {
		.print("ALTO EL FUEGO!");
			-puedoDisparar;
		}
		-friends_in_fov(Q,W,E,R,T,AmigoPos);
	}
	
	if (puedoDisparar) {
		.shoot(2, Position);
	}
	
	-puedoDisparar.

	
+solicitudC(Pos)[source(A)]: not atacando
	<-
	?position(MiPos);
	.send(A, tell, respuestaColmenaS(MiPos));
	+ayudandoc(Pos);
	-solicitudC(_).
	
/* Me aceptan la respuesta de solicitud de apoyo */
+solicitudAceptadaC(Pos)[source(A)]
	<-
	-control_points(_);
	-total_control_points(_);
	-patrolling;
	+atacando;
	.goto(Pos);
	-solicitudAceptadaC(_).
	
/* Me rechazan la respuesta de solicitud de ayuda */
+solicitudDenegadaC[source(A)]
	<-
	-ayudandoc(Pos).
	
/* Llego al objetivo del ataque en colmena */
+target_reached(Pos): atacando & solicitandoAtaque
	<-
	-atacando;
	-solicitandoAtaque;
	!generarPatrulla.
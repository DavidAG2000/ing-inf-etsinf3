/*
	Autores: Antoni Mestre Gascón & Mario Campos Mocholí
	Nombre: Operativo
*/

/* Creencia que se dispara cuando se inicia la partida */
+flag(F): team(200)
	<-
	!generarPatrulla.

/* El agente gira mientras patrulla */
+!rolling(Rot)
	<-
	.turn(Rot);
	.wait(250);
	!!rolling(Rot + 1).


/* ESTRATEGIA DE PATRULLA EN ROMBO (EXTERIOR) */
/* Generamos unos puntos de control en rombo */
+!generarPatrulla
	<-
	?flag(F);
	.circuloInterior(F, C);
	+control_points(C);
	.length(C, L);
	+total_control_points(L);
	+patrolling;
	.get_service("general");
	!!rolling(1);
	+patroll_point(0).


+target_reached(T): patrolling & team(200) 
	<-
	.reload;
	?patroll_point(P);
	-+patroll_point(P+1);
	-target_reached(T).

+patroll_point(P): total_control_points(T) & P<T 
	<-
	?control_points(C);
	.nth(P,C,A);
	.goto(A).

+patroll_point(P): total_control_points(T) & P==T
	<-
	-patroll_point(P);
	+patroll_point(0).


+ammo(A): A < 20
	<-
	.reload.
	

// ESTRATEGIA DE RECEPCIÓN Y ENVIAMIENTO DE SOLICITUDES DE AYUDA DE MUNICIÓN
/* Recibo solictud de ayuda */
+solicitudDeMunicion(Pos)[source(A)]: not (ayudando(_,_))
	<-
	?position(MiPos);
	.send(A, tell, respuestaMunicion(MiPos));
	+ayudando(A, Pos);
	-solicitudDeMunicion(_).
	
/* Me aceptan la respuesta de solicitud de ayuda */
+solicitudAceptada[source(A)]: ayudando(A, Pos)
	<-
	-control_points(_);
	-total_control_points(_);
	-patrolling;
	-patroll_point(_);
	.goto(Pos).
	
/* Me rechazan la respuesta de solicitud de ayuda */
+solicitudDenegada[source(A)]: ayudando(A, Pos)
	<-
	-ayudando(_, _).

/* Voy a la posición del agente que me ha aceptado */
+target_reached(T): ayudando(A, T)
	<-
	.reload;
	-ayudando(_, _);
	!generarPatrulla.
	


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
	.send(A, tell, respuestaColmenaF(MiPos));
	+ayudandoc(Pos);
	-solicitudC(_).
	
/* Me aceptan la respuesta de solicitud de apoyo */
+solicitudAceptadaC(Pos)[source(A)]
	<-
	-control_points(_);
	-total_control_points(_);
	-patrolling;
	-patroll_point(_);
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
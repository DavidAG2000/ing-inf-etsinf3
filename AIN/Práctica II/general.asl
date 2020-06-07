/*
	Autores: Antoni Mestre Gascón & Mario Campos Mocholí
	Nombre: General
*/

/* Creencia que se dispara cuando se inicia la partida */
+flag (F): team(200) 
	<-
	.register_service("general");
	.get_medics;
	.get_backups;
	.get_fieldops;
	.look_at([256, 0, 256]).


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


/* ESTRATEGIA DE PAQUETES DE SALUD */
/* Recibo solictud de ayuda */
+solicitudDeSalud(Pos)[source(A)]: not solicitandoAyuda & not eligiendoMedico
	<-
	+solicitandoAyuda;
	?myMedics(M);
	+medicoPOS([]);
	+medicoID([]);
	.send(M, tell, solicitudDeSalud(Pos));
	.wait(1000);
	!!elegirMedico(Pos).


/* Concateno la posicion y el ID de los medicos que responden */
+respuestaVida(Pos)[source(A)]: solicitandoAyuda & not eligiendoMedico
	<-
	.wait(500);
	?medicoPOS(B);
	.concat(B, [Pos], B1); -+medicoPOS(B1);
	?medicoID(Ag);
	.concat(Ag, [A], Ag1); -+medicoID(Ag1);
	-respuestaVida(Pos).
	

/* PLANES */
/* Plan para elegir el medico más cercano */
+!elegirMedico(Pos): solicitandoAyuda & not eligiendoMedico
	<-
	+eligiendoMedico;
	.wait(500);
	?medicoPOS(Bi);
	?medicoID(Ag);
	.length(Bi, LB);
	if (LB > 0) {
		.medicoMasCerca(Pos,Bi, Medico);  // Guarda en Medico la posicion del medico elegido
		.nth(0, Medico, AAA);
		.nth(AAA, Ag, A);
		.send(A, tell, solicitudAceptada);
		.delete1(AAA, Ag, Ag1);
		.send(Ag1, tell, solicitudDenegada);
	}
	-medicoPOS(_);
	-medicoID(_);
	-solicitandoAyuda;
	-eligiendoMedico.
	
/* Plan para cuando no hay ningun médico que pueda ayudar */
+!elegirMedico(Pos): medicoPOS(Bi) & .length(Bi, Len) & Len == 0
	<-
	-solicitandoAyuda.	



/* ESTRATEGIA DE PAQUETES DE MUNICION */
/* Recibo solictud de ayuda */
+solicitudDeMunicion(Pos)[source(A)]: not solicitandoMun & not eligiendoOp
	<-
	.wait(500);
  	+solicitandoMun;
  	.get_fieldops;
  	?myFieldops(M);
	+operativoPOS([]);
	+operativoID([]);
	.send(M, tell, solicitudDeMunicion(Pos));
	.wait(1000);
 	!!elegirOperativo(Pos).


/* Concateno la posicion y el ID de los operativos que responden */
+respuestaMunicion(Pos)[source(A)]: solicitandoMun & not eligiendoOp
	<-
	.wait(500);
	?operativoPOS(B);
	.concat(B, [Pos], B1); -+operativoPOS(B1);
	?operativoID(Ag);
	.concat(Ag, [A], Ag1); -+operativoID(Ag1);
	-respuestaMunicion(Pos).
	

/* PLANES */
/* Plan para elegir el operativo más cercano */
+!elegirOperativo(Pos): solicitandoMun & not eligiendoOp
	<-
	+eligiendoOp;
	.wait(500);
	?operativoPOS(BiO);
	?operativoID(AgO);
	.length(BiO, LO);
	if (LO > 0) {
		.operativoMasCerca(Pos, BiO, Operativo);  // Guarda en operativo la posicion del operativo elegido
		.nth(0, Operativo, BBB);
		.nth(BBB, AgO, AOO);
		.send(AOO, tell, solicitudAceptada);
		.delete1(BBB, AgO, AgO1);
		.send(AgO1, tell, solicitudDenegada);
		-operativoPOS(_);
		-operativoID(_);
	}
	-solicitandoMun;
	-eligiendoOp.



/* Plan para cuando no hay ningun operativo que pueda ayudar */
+!elegirOperativo(Pos): operativoPOS(Bi) & .length(Bi, Len) & Len == 0
	<-
	-solicitandoMun.
	


/* COLMENA */
/* Recibo solictud de apoyo */
+solicitudDeColmena(Pos)[source(A)]: not solicitandoApoyo & not creandoEquipo
	<-
	+medicoP([]);
	+medicoI([]);
	+operaP([]);
	+operaI([]);
	+soldadoP([]);
	+soldadoI([]);
	+solicitandoApoyo;
	?myMedics(M);
	.wait(500);
	.send(M, tell, solicitudC(Pos));
	?myFieldops(F);
	.send(F, tell, solicitudC(Pos));
	?myBackups(S);
	.send(S, tell, solicitudC(Pos));
	.wait(3000);
	!!elegirEquipo(Pos).


/* Concateno la posicion y el ID de los médicos que responden */
+respuestaColmenaM(Pos)[source(A)]: solicitandoApoyo & not creandoEquipo
	<-
	?medicoP(B);
	.concat(B, [Pos], B1); -+medicoP(B1);
	?medicoI(Ag);
	.concat(Ag, [A], Ag1); -+medicoI(Ag1);
	-respuestaColmenaM(Pos).
	
/* Concateno la posicion y el ID de los operativos que responden */
+respuestaColmenaF(Pos)[source(A)]: solicitandoApoyo & not creandoEquipo
	<-
	?operaP(B);
	.concat(B, [Pos], B1); -+operaP(B1);
	?operaI(Ag);
	.concat(Ag, [A], Ag1); -+operaI(Ag1);
	-respuestaColmenaF(Pos).

/* Concateno la posicipn y el ID de los soldados que responden */
+respuestaColmenaS(Pos)[source(A)]: solicitandoApoyo & not creandoEquipo
	<-
	?soldadoP(B);
	.concat(B, [Pos], B1); -+soldadoP(B1);
	?soldadoI(Ag);
	.concat(Ag, [A], Ag1); -+soldadoI(Ag1);
	-respuestaColmenaS(Pos).

/* PLANES */
/* Plan para elegir la composición de colmena */
+!elegirEquipo(Pos): solicitandoApoyo & not creandoEquipo
	<-
	+creandoEquipo;
	-solicitandoApoyo;
	?medicoP(Ml);
	?medicoI(Mi);
	.length(Ml, L1);
	if (L1 > 0) {
		.agentesMasCercanos1(Pos, Ml, Medico);  // Guarda en Medico la posición del medico elegido	
		.nth(0, Medico, AuxM);
		.nth(AuxM, Mi, A);
		.send(A, tell, solicitudAceptadaC(Pos));
		.delete1(AuxM, Mi, Ag1);
		.send(Ag1, tell, solicitudDenegadaC);
		-medicoP(_);
		-medicoI(_);
	}
	
	?operaP(Fl);
	?operaI(Fi);
	.length(Fl, L2);
	if (L2 > 0) {
		.agentesMasCercanos1(Pos, Fl, FieldOp);  // Guarda en FieldOp la posición del medico elegido
		.nth(0, FieldOp, AuxF);
		.nth(AuxF, Fi, AF);
		.send(AF, tell, solicitudAceptadaC(Pos));
		.delete1(AuxF, Fi, Ag2);
		.send(Ag2, tell, solicitudDenegadaC);
		-operaP(_);
		-operaI(_);
	}
	
	?soldadoP(Sl);
	?soldadoI(Si);
	.length(Sl, L3);
	if (L3 > 1) {
		.agentesMasCercanos2(Pos, Sl, Soldado);  // Guarda en Soldado la posición del medico elegido
		.nth(0, Soldado, Aux1);
		.nth(1, Soldado, Aux2);
		.nth(Aux1, Si, AS);
		.nth(Aux1, Si, BS);
		.send(AS, tell, solicitudAceptadaC(Pos));
		.send(BS, tell, solicitudAceptadaC(Pos));
		.delete1(Aux1, Si, Ag3);	
		.delete1(Aux2, Ag3, Ag4);
		.send(Ag4, tell, solicitudDenegadaC);
		-soldadoP(_);
		-soldadoI(_);
	}
	
	-creandoEquipo.
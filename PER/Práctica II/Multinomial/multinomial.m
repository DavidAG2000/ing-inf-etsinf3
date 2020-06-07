function [etr,edv] = multinomial(Xtr,xltr,Xdv,xldv,epsilons)
  cl = unique(xltr);
  pc=[];
  edv=[];
  etr=[];
  
  
  %Probabilidades por clase:%
  for c = cl'
    filasC = find(xltr == c);  
    w0(c+1) = rows(filasC) / rows(Xtr);
    valoresC = Xtr(filasC,:);
    pc = vertcat(pc, sum(valoresC));
    pc(end,:) = pc(end,:) / sum(pc(end,:));
  end
  
  %logaritmo de wc0%
  w0 = log(w0);

  %Por cada una de las epsilon aplicamos Laplace y calculamos %
  %el error de clasificación%
  for e = epsilons
    pc2 = pc + e;
    pc2 = pc2./sum(pc2);

    %logaritmo de pc (pc2)%
    pc2 = log(pc2);	

    %matrices de classificación%
    clasi = (Xdv*pc2') + w0;
    clasie= (Xtr*pc2') + w0;

    %Error de clasificación muestras%
    %Se queda con el argumento que da el mayor resultado%
    [mV mI] = max(clasi,[],2);
    mI = mI - 1;

    %Se compara con las etiquetas originales%
    [r col]= size(xldv);
	mal = find(mI != xldv);
    edv = horzcat(edv, rows(mal) / r * 100);

    %Error de clasificaación de mustras de entrenamiento%
    %Se queda con el argumento que da el mayor resultado%
    [mV mI] = max(clasie,[],2);
    mI = mI - 1;

    %Se compara con las etiquetas originales%
    [r col]= size(xltr);
	mal = find(mI != xltr);
    etr = horzcat(etr, rows(mal) / r * 100);
  end
  etr=0;
end

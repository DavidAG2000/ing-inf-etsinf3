function [etr, edv] = gaussian(Xtr,xltr,Xdv,xldv,alphas)
  N = rows(Xtr);

  %parámetros
  cl=unique(xltr);
  for c=1:length(cl)
    filasC = find(cl(c)==xltr);
    Xtrc = Xtr(filasC,:);
    NC=rows(Xtrc);
    w0(c)= NC / N;
    mu(:,c)=sum(Xtrc) / NC;
    sigma{c}= cov(Xtrc,1);
  end


  % flat smoothing
  for a = 1:length(alphas)
    alpha = alphas(a);
    for c = 1:length(cl)
      matCov{c} = alpha*sigma{c} + (1-alpha) * eye(rows(sigma{c}));
    end
    for c = 1:length(cl)
      clasi(:,c) = gc(w0(c),mu(:,c),matCov{c},Xdv);
    end
    [_,mI] = max(clasi,[],2);
    mI = mI - 1;
    mal = (mI != xldv);
    [m,n] = size(xldv);
    edv(a) = sum(mal)/m * 100;
    for c = 1:length(cl)
      clasie(:,c) = gc(w0(c),mu(:,c),matCov{c},Xtr);
    end
    [_,mI] = max(clasie,[],2);
    mI = mI - 1;
    mal = (mI != xltr);
    [m,n] = size(xltr);
    etr(a) = sum(mal)/m * 100;
  end
end


function [res]=gc(w0,med,co,X)
  wc0=log(w0)-1/2*logdet(co)-1/2*(med')*pinv(co)*med;
  wc=pinv(co)*med;
  wC=-1/2*pinv(co);
  res = sum((X*wC).*X,2) + X*wc + wc0;
end

%%logdet
function [res]=logdet(X)
  propios = eig(X);
  if (any(propios<=0))
    res = log(realmin);
  else
    res = sum(log(propios));
  end
end

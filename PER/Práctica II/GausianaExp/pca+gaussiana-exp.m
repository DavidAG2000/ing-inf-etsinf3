#!/usr/bin/octave -qf

if (nargin!=6)
printf("Usage: gaussiana-exp.m <trdata> <trlabels> <as> <ks> <%%trper> <%%dvper>\n")
exit(1);
end;

arg_list=argv();
trdata=arg_list{1};
trlabs=arg_list{2};
ks=str2num(arg_list{3});
trper=str2num(arg_list{4});
dvper=str2num(arg_list{5});
as=str2num(arg_list{6});

load(trdata);
load(trlabs);

N=rows(X);
rand("seed",23); permutation=randperm(N);
X=X(permutation,:); xl=xl(permutation,:);

Ntr=round(trper/100*N);
Ndv=round(dvper/100*N);
Xtr=X(1:Ntr,:); xltr=xl(1:Ntr);
Xdv=X(N-Ndv+1:N,:); xldv=xl(N-Ndv+1:N);

vectores=ks;
[m W]=pca(Xtr);

Xtrm=Xtr-m;
Xdvm=Xdv-m;

f1=fopen("resultado.out","w");


for k = vectores
  fprintf(f1,"k= %d\n", k);
  xr=Xtrm*W(:,1:k);
  yr=Xdvm*W(:,1:k);
  [edv]=gaussian(xr,xltr,yr,xldv,as);
  for i=1:length(edv)
 fprintf(f1,"%e %.2f\n",as(i),edv(i));
  end
end
fclose(f1);

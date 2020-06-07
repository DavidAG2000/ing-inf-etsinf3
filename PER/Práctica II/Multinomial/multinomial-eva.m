#!/usr/bin/octave -qf

if (nargin!=5)
printf("Usage: multinomial-eva.m <trdata> <trlabels> <tedata> <telabels> <epsiolon>\n")
exit(1);
end;

arg_list=argv();
trdata=arg_list{1};
trlabs=arg_list{2};
tedata=arg_list{3};
telabs=arg_list{4};
e=str2num(arg_list{5});

load(trdata);
load(trlabs);
load(tedata);
load(telabs);

N=rows(X);
rand("seed",23); permutation=randperm(N);
X=X(permutation,:); xl=xl(permutation,:);
N=rows(Y);
rand("seed",23); permutation=randperm(N);
Y=Y(permutation,:); yl=yl(permutation,:);

f1=fopen("resultado_mult_eva.out","w");

[etr edv] = multinomial(X,xl,Y,yl,e);
fprintf(f1,"%e: %f\n",e,edv);

fclose(f1);

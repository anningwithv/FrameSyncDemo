#!/bin/sh 
mkdir -p $HOME/dotnet && tar zxf dotnet-sdk-3.1.113-linux-x64.tar.gz -C $HOME/dotnet
export DOTNET_ROOT=$HOME/dotnet
export PATH=$PATH:$HOME/dotnet

cd /HOKServer
./HOKServer


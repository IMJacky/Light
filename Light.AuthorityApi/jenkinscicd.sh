echo 'dotnet start'

echo '1、env'
pwd
ls
whoami
which dotnet
dotnet --info
dotnet --version

echo '2、cd Light.AuthorityApi'
cd ./Light.AuthorityApi

echo '3、dotnet restore Light.AuthorityApi'
dotnet restore

echo '4、delete and add directory Light_AuthorityApi_Publish'
rm -rf $WORKSPACE/Light_AuthorityApi_Publish
mkdir $WORKSPACE/Light_AuthorityApi_Publish

echo '5、dotnet publish Light.AuthorityApi'
dotnet publish -c:Release -o $WORKSPACE/Light_AuthorityApi_Publish

echo 'dotnet end'

echo '6、cd Light_AuthorityApi_Publish'
cd $WORKSPACE/Light_AuthorityApi_Publish

echo 'docker start'

echo '7、add Dockerfile'
touch Dockerfile
echo "FROM microsoft/dotnet" >> Dockerfile
echo "WORKDIR /app" >> Dockerfile
echo "COPY . ." >> Dockerfile
echo "EXPOSE 5000" >> Dockerfile
echo "ENV ASPNETCORE_URLS http://*:5000" >> Dockerfile
echo "ENV ASPNETCORE_ENVIRONMENT Production" >> Dockerfile
echo "ENTRYPOINT [\"dotnet\", \"Light.AuthorityApi.dll\"]" >> Dockerfile

echo '8、stop container light.authorityapi'
sudo docker stop $(sudo docker ps -a -q  --filter=ancestor=light.authorityapi)

echo '9、delete container light.authorityapi'
sudo docker rm $(sudo docker ps -a -q --filter=ancestor=light.authorityapi)

echo '10、delete image light.authorityapi'
sudo docker rmi light.authorityapi

echo '11、build image light.authorityapi'
sudo docker build -t light.authorityapi .

echo '12、run container light.authorityapi'
sudo docker run -p 5000:5000 -d --name light.authorityapi light.authorityapi

echo 'docker end'


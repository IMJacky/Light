cd /home/ubuntu/wjg/project/light
unzip -o Light.AuthorityApi.zip -d Light.AuthorityApi
sudo rm -rf *.zip
cd /home/ubuntu/wjg/project/light/Light.AuthorityApi
touch Dockerfile
echo "FROM microsoft/dotnet" >> Dockerfile
echo "WORKDIR /app" >> Dockerfile
echo "COPY . ." >> Dockerfile
echo "EXPOSE 5000" >> Dockerfile
echo "ENV ASPNETCORE_URLS http://*:5000" >> Dockerfile
echo "ENV ASPNETCORE_ENVIRONMENT Production" >> Dockerfile
echo "ENTRYPOINT [\"dotnet\", \"Light.AuthorityApi.dll\"]" >> Dockerfile
sudo docker stop $(sudo docker ps -a -q  --filter=ancestor=light.authorityapi)
sudo docker rm $(sudo docker ps -a -q --filter=ancestor=light.authorityapi)
sudo docker rmi light.authorityapi
sudo docker build -t light.authorityapi .
sudo docker run -p 5000:5000 -d --name light.authorityapi light.authorityapi
cd /home/ubuntu/wjg/project/light
sudo rm -rf Light.AuthorityApi
unzip -o /home/ubuntu/drop/Light.AuthorityApi.zip -d /home/ubuntu/project/Light.AuthorityApi
cd /home/ubuntu/project/Light.AuthorityApi
sudo docker stop $(sudo docker ps -a -q  --filter=ancestor=light.authorityapi)
sudo docker rm $(sudo docker ps -a -q --filter=ancestor=light.authorityapi)
sudo docker rmi light.authorityapi
sudo docker build -t light.authorityapi .
sudo docker run -p 5000:5000 -e "ASPNETCORE_URLS=http://+:5000" -d --name light.authorityapi light.authorityapi
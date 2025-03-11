1----  cd ..\CQRSDemoProject\CQRSWebApiProject\Docker yani bu proje içerisine gel
2----  docker-compose up -d
3----  docker ps
4----  docker exec -it redis-node-1  bash
5--- redis-cli --cluster create redis-node-1:6379 redis-node-2:6379 redis-node-3:6379 redis-node-4:6379 redis-node-5:6379 redis-node-6:6379 --cluster-replicas 1
6--- yes


  ----  cd /app
  ----  dotnet CQRSWebApiProject.dll
  ----  exit




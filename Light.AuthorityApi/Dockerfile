FROM microsoft/dotnet
WORKDIR /app
COPY ./Light.AuthorityApi .
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "Light.AuthorityApi.dll"]
#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 59662
EXPOSE 44349

FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["NetNote/NetNote.csproj", "NetNote/"]
COPY ["guoguo.Domain.ApplicationService/guoguo.Domain.ApplicationService.csproj", "guoguo.Domain.ApplicationService/"]
COPY ["guoguo.Domain.Entity/guoguo.Domain.Entity.csproj", "guoguo.Domain.Entity/"]
RUN dotnet restore "NetNote/NetNote.csproj"
COPY . .
WORKDIR "/src/NetNote"
RUN dotnet build "NetNote.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NetNote.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NetNote.dll"]
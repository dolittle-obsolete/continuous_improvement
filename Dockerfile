# Build the .NET part
FROM microsoft/dotnet:2.2-sdk-bionic AS dotnet-build
WORKDIR /src
COPY ./README.md /Packages
COPY ./Build ./Build
COPY ./NuGet.Config ./
COPY ./Source/. ./Source
WORKDIR /src/Source/Core
RUN dotnet restore --ignore-failed-sources
RUN dotnet publish -c Release -o out

 # Build the interaction layer
 FROM node:latest AS node-build
 WORKDIR /src
 COPY ./Source/Web/. ./Source/Web
 WORKDIR /src/Source/Web
 RUN yarn global add webpack
 RUN yarn global add webpack-cli
 RUN yarn add babel-loader@7
 RUN yarn
 RUN webpack -p --env.production
 #--output-public-path /continuousimprovement

# Build runtime image
FROM microsoft/dotnet:2.2-sdk-bionic
WORKDIR /app
COPY --from=dotnet-build /src/Source/Core/out ./
COPY --from=node-build /src/Source/Web/wwwroot ./wwwroot

ENV BASE_PATH=
ENV KUBERNETES_API=
VOLUME ["/build"]

EXPOSE 80
ENTRYPOINT ["dotnet", "Core.dll"]
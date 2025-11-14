Decisiones de Desarrollo – TP05 & TP06 · CI/CD + Pruebas Unitarias
1. Configuración del Entorno

Ingresé a mi carpeta de trabajo y cloné el repositorio del proyecto Marketplace:

git clone https://github.com/ssantosromero/TP05-TP06-Marketplace


Accedí al repositorio:

cd TP05-TP06-Marketplace


Verifiqué mi configuración global de Git para asegurar trazabilidad correcta:

Comandos utilizados:

git config --global user.name "Santos Romero Reyna"
git config --global user.email "santosromeroreyna@gmail.com"


Confirmé la configuración con:

git config --list


Creé este archivo de decisiones (decisiones.md) para documentar paso a paso lo realizado en los TP05 y TP06.

Fui realizando commits periódicos para registrar cada avance:

git add .
git commit -m "Avance TP05/TP06 – CI/CD y pruebas"
git push origin main

2. Elección del Flujo de Trabajo

Decidí utilizar un flujo simple y claro, adecuado a un proyecto personal:

main: rama principal con el código estable

feature/*: desarrollo de nuevas funcionalidades

fix/*: correcciones puntuales

Este flujo me permitió trabajar ordenado, mantener el historial limpio y evitar conflictos innecesarios.

3. Decisiones sobre el Backend (.NET 8)

Elegí .NET 8 Web API por estabilidad, documentación y porque ya venía trabajando en esta tecnología.

Incorporé SQLite + Entity Framework Core, decisión basada en:

simplicidad de configuración

no requiere servidor externo

ideal para entornos de desarrollo y TP

Añadí Swagger para facilitar pruebas:

app.UseSwagger();
app.UseSwaggerUI();


Implementé CORS para permitir que el frontend (React) acceda a la API:

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


Decidí agregar un DataSeeder para cargar datos automáticamente, evitando poblar la base manualmente.

4. Decisiones sobre el Frontend (React)

Usé React porque ya tenía conocimientos previos y permite crear rápidamente una UI funcional.

Mantuve las dependencias al mínimo esencial.

Para producción decidí generar solo el build final, sin subir node_modules al pipeline.

Esto fue clave para optimizar el CI/CD (pasó de 50 minutos a menos de 1 minuto).

5. Pruebas Unitarias (TP06)

Framework elegido: xUnit, por:

integración nativa con .NET

sintaxis clara

soporte directo en Azure Pipelines

Creé un proyecto separado de pruebas:

Marketplace.Api.Tests/


Implementé pruebas básicas que validan comportamiento del controlador.

Ejemplo:

[Fact]
public void Get_ReturnsWeatherData()
{
    var controller = new WeatherForecastController();
    var result = controller.Get();
    Assert.NotNull(result);
}


Ejecuté localmente:

dotnet test


Las pruebas se integran automáticamente en el stage de Build & Test del pipeline.

6. Construcción del Pipeline CI/CD (TP05)

Utilicé un agente self-hosted porque:

evita límites del agente de Microsoft

permite reproducir resultados localmente

elimina tiempos de espera en cola

El pipeline se ejecuta desde el archivo:

azure-pipelines.yml


Definí tres stages:

a) Build & Test

Incluye:

compilación de la API

ejecución de pruebas (xUnit)

instalación de Node.js

build de React

copia solo de la carpeta /build

publicación del artefacto marketplace-drop

b) Deploy QA (simulado)

Descarga y verifica la integridad del artefacto.

c) Deploy Producción (simulado)

Replica el proceso de QA pero orientado a PROD.

Opté por que ambos despliegues sean “simulados”, ya que el TP05 lo permite y facilita demostrar comprensión del flujo.

7. Optimización del Pipeline

Durante la primera ejecución, detecté que Azure intentaba subir más de 38.000 archivos del frontend, lo que causaba tiempos superiores a 50 minutos.

Decisión: publicar solo el contenido de marketplace.frontend/build.

Esto redujo:

tamaño del artifact

tiempo de ejecución

carga del agente local

Resultado final: pipeline completo en menos de 1 minuto.

8. Problemas Encontrados y Soluciones
1) Advertencia ARM/x64

Azure mostraba:

“X64 emulation is known to cause hangs...”

Esto ocurre porque mi agente es ARM.
No afecta funcionalidad, por eso decidí dejarlo así.

2) Pipeline Cancelado por Tamaño del Artifact

Se solucionó aplicando:

cp -R marketplace.frontend/build $(Build.ArtifactStagingDirectory)/frontend


y eliminando node_modules.

3) CORS bloqueando el frontend

Implementé la política AllowReactApp (ver sección Backend).

9. Reflexión Final

Este trabajo práctico me permitió integrar:

CI/CD real con Azure DevOps

pruebas unitarias automatizadas

build optimizado para producción

agente self-hosted

integración entre frontend + backend

Además, dejé la base totalmente lista para el TP07, donde agregaré:

Code Coverage

SonarCloud

Cypress

Quality Gates

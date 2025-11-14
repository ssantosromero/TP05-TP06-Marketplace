# TP05 – CI/CD con Azure Pipelines y TP06 – Pruebas Unitarias

## 🎯 Objetivo
Implementar un pipeline de integración y despliegue continuo (CI/CD) en Azure DevOps, 
y agregar pruebas unitarias automatizadas al flujo de build.

## ⚙️ Tecnologías
- Azure DevOps Pipelines
- .NET 8 Web API (Backend)
- React (Frontend)
- SQLite (Base de datos)
- xUnit (Pruebas unitarias)
- Self-hosted Agent macOS

## 🧱 Estructura del proyecto
Copiar código
TP05-TP06-Marketplace/
├── Marketplace.Api/
├── Marketplace.Api.Tests/
├── marketplace.frontend/
└── azure-pipelines.yml
markdown
Copiar código

## 🧩 Pipeline CI/CD
El pipeline YAML implementa tres etapas:

1. **Build & Test**  
   - Compila la API y ejecuta los tests.  
   - Publica el artifact optimizado.  

2. **Deploy QA**  
   - Simula el despliegue en entorno QA.  

3. **Deploy Producción**  
   - Simula el despliegue final de la app.

Archivo principal: `azure-pipelines.yml`

## 🧪 Pruebas unitarias (TP06)
- Framework: **xUnit**
- Proyecto: `Marketplace.Api.Tests`
- Comando local: `dotnet test`
- Integradas en pipeline con logs `.trx`

Ejemplo de test:
```csharp
[Fact]
public void Get_ReturnsWeatherData()
{
    var controller = new WeatherForecastController();
    var result = controller.Get();
    Assert.NotNull(result);
}

🖼️ Evidencias

Pipeline ejecutado en Azure DevOps ✅

Todos los stages en verde (Build, QA, PROD).

Logs del agente local (Santos-MacBook).

Resultados de pruebas exitosas.

📦 Resultados

Artifact marketplace-drop publicado correctamente.

Ejecución total: < 1 min (optimizado).

Sin errores ni warnings críticos.

🧠 Reflexión

Estos TPs consolidan la integración continua y las pruebas automáticas como base del ciclo DevOps.
Se logró un pipeline funcional, reproducible y eficiente, con un agente local y ejecución validada.
El aprendizaje clave fue entender cómo automatizar calidad y despliegue en un entorno real de desarrollo.

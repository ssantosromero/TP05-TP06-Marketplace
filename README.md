# TP05-TP06 · CI/CD en Azure + Pruebas Unitarias (.NET + React)

**Repositorio**: `TP05-TP06-Marketplace`  
**Pipeline**: Azure DevOps (self-hosted agent · macOS)  
**Stages**: Build & Test → Deploy QA → Deploy Producción

## 🎯 Objetivo

### TP05 — CI/CD
Implementar un pipeline CI/CD en Azure DevOps que:
- Compile el backend (.NET 8)
- Compile el frontend (React)
- Ejecute pruebas
- Genere artefactos
- Despliegue a QA y Producción (simulados)

### TP06 — Testing
- Crear pruebas unitarias con xUnit
- Integrarlas automáticamente en el pipeline

## 🧱 Estructura del Repositorio

```
TP05-TP06-Marketplace/
├── Marketplace.Api/              # Backend (.NET 8 Web API + Swagger + CORS + SQLite)
├── Marketplace.Api.Tests/        # Pruebas unitarias (xUnit)
├── marketplace.frontend/         # Frontend React
└── azure-pipelines.yml           # Pipeline principal CI/CD
```

## ⚙️ Tecnologías Utilizadas

| Capa | Tecnología |
|------|------------|
| Backend | .NET 8 Web API |
| Base de Datos | SQLite + Entity Framework Core |
| Frontend | React |
| Testing | xUnit |
| CI/CD | Azure DevOps Pipelines |
| Agente | Self-hosted macOS (`MacBook-Pro-de-Santos`) |

## ▶️ Ejecución Local

### 🔹 Backend (.NET 8)
1. Ubicarse en la API:
```bash
cd Marketplace.Api
```

2. Restaurar, compilar y ejecutar:
```bash
dotnet restore
dotnet build
dotnet run
```

3. Endpoints disponibles:
   - **API**: http://localhost:5011
   - **Swagger**: http://localhost:5011/swagger

### 🔹 Frontend (React)
1. Ubicarse en el frontend:
```bash
cd marketplace.frontend
```

2. Instalar dependencias y ejecutar:
```bash
npm install
npm run build      # genera carpeta /build lista para producción
npm start          # modo desarrollo (http://localhost:3000)
```

## 🧪 Pruebas Unitarias (xUnit)

- **Ubicadas en**: `Marketplace.Api.Tests`
- Se ejecutan automáticamente en el stage Build & Test

**Ejemplo de prueba**:
```csharp
[Fact]
public void Get_ReturnsWeatherData()
{
    var controller = new WeatherForecastController();
    var result = controller.Get();
    Assert.NotNull(result);
}
```

**Ejecución local**:
```bash
dotnet test
```

## 🚀 Pipeline CI/CD (TP05)

El pipeline está definido en `azure-pipelines.yml` y ejecuta las siguientes etapas:

### 1. Build & Test
- ✔ Compila backend (.NET 8)
- ✔ Ejecuta pruebas unitarias (xUnit)
- ✔ Compila frontend (React)
- ✔ Copia solo la carpeta `/build` del frontend
- ✔ Publica artefacto `marketplace-drop` (liviano y optimizado)

### 2. Deploy QA (simulado)
- Descarga artifact
- Valida integridad
- Simula despliegue al ambiente QA

### 3. Deploy Producción (simulado)
- Misma lógica que QA
- Representa despliegue final

**⏱ Tiempo total del pipeline**: ~50 segundos  
Gracias a optimización (no subir `node_modules`, solo `build`).

## 📸 Evidencias (puntos a incluir)

- Captura del pipeline con los tres stages en verde
- Artifact `marketplace-drop` generado
- Log del agente local ejecutando jobs (`Listening for Jobs`)
- Resultados de `dotnet test` aprobados

*(Podés agregar las capturas cuando entregues)*

## ⚙️ Problemas Frecuentes y Soluciones

| Problema | Causa | Solución |
|----------|-------|----------|
| Warning ARM/x64 | El agente es ARM | Solo informativo, ignorar |
| Pipeline MUY lento | Se subía node_modules (38982 archivos) | Copiar solo `/build` |
| CORS | React no podía acceder a API | Agregamos política `AllowReactApp` |

## 🧠 Reflexión Final

Los TPs 05 y 06 permitieron integrar por primera vez un flujo completo de CI/CD:

- Compilación automática
- Tests automatizados
- Artefactos optimizados
- Despliegue controlado
- Pipeline ejecutándose en un agente propio

Con esto, la base técnica queda completamente lista para avanzar al TP07, donde se incorporarán:

- Code Coverage
- Análisis estático (SonarCloud)
- Pruebas E2E (Cypress)
- Quality Gates
- Reportes integrados en DevOps

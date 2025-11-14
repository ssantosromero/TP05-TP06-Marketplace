# TP05-TP06 · CI/CD en Azure + Pruebas Unitarias (.NET + React)

**Repositorio**: `TP05-TP06-Marketplace`  
**Pipeline**: Azure DevOps (self-hosted agent · macOS)  
**Stages**: Build & Test → Deploy QA → Deploy Producción  
**Autor**: Santos Romero Reyna

##  Objetivo

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

##  Estructura del Repositorio

```
TP05-TP06-Marketplace/
├── Marketplace.Api/              # Backend (.NET 8 Web API + Swagger + CORS + SQLite)
│   ├── Controllers/              # ProductsController y CartController
│   ├── Models/                   # Product y CartItem (con [Key])
│   └── marketplace.db           # Base de datos SQLite generada
├── Marketplace.Api.Tests/        # Pruebas unitarias (xUnit)
├── marketplace.frontend/         # Frontend React con Axios
│   ├── src/                     # Catálogo, carrito dinámico
│   └── build/                   # Build optimizado para producción
└── azure-pipelines.yml           # Pipeline principal CI/CD
```

##  Tecnologías Utilizadas

| Capa | Tecnología | Justificación |
|------|------------|---------------|
| Backend | .NET 8 Web API | Compatibilidad con Azure DevOps y EF Core |
| Base de Datos | SQLite + Entity Framework Core | Persistencia ligera sin servidor externo |
| Frontend | React + Axios | Velocidad de prototipado y comunicación con API |
| Testing | xUnit | Sencillez y compatibilidad nativa con .NET |
| CI/CD | Azure DevOps Pipelines | Integración completa con agente self-hosted |
| Agente | Self-hosted macOS (`MacBook-Pro-de-Santos`) | Sin límites y control total del entorno |

##  Configuración y Ejecución

###  Setup Inicial del Proyecto
1. Clonar el repositorio:
```bash
git clone https://github.com/ssantosromero/TP05-TP06-Marketplace
cd TP05-TP06-Marketplace
```

2. Configurar Git (trazabilidad):
```bash
git config --global user.name "Santos Romero Reyna"
git config --global user.email "santosromeroreyna@gmail.com"
```

3. Instalar dependencias del backend:
```bash
cd Marketplace.Api
dotnet restore
```

4. Configurar Entity Framework Core:
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

5. Crear base de datos SQLite:
```bash
dotnet ef migrations add Init
dotnet ef database update
```

###  Backend (.NET 8)
1. Ejecutar la API:
```bash
cd Marketplace.Api
dotnet run
```

2. Endpoints disponibles:
   - **API**: http://localhost:5011
   - **Swagger**: http://localhost:5011/swagger
   - **Endpoints**: `/api/Products`, `/api/Cart`

3. **CORS configurado** para React:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod());
});
```

###  Frontend (React)
1. Instalar dependencias y ejecutar:
```bash
cd marketplace.frontend
npm install
npm start          # modo desarrollo (http://localhost:3000)
npm run build      # genera /build para producción
```

2. **Configuración Axios** para comunicación con API:
```javascript
axios.get("http://localhost:5011/api/Products")
```

##  Pruebas Unitarias (xUnit)

- **Ubicadas en**: `Marketplace.Api.Tests`
- **Framework**: xUnit (compatibilidad nativa con .NET y Azure Pipelines)
- Se ejecutan automáticamente en el stage Build & Test

**Ejemplo de prueba implementada**:
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
cd Marketplace.Api.Tests
dotnet test
```

##  Configuración Agente Self-Hosted

Para evitar límites del pool de Microsoft y tener control total:

1. **Descargar agente** desde Azure DevOps:
   - Organization → Agent Pools → New Agent

2. **Instalar y configurar**:
```bash
./config.sh
```

3. **Ejecutar agente**:
```bash
./run.sh
```

4. **Resultado esperado**: 
```
"Listening for Jobs"
```

Esto permite que cada push a `main` ejecute el pipeline en el MacBook local.

## 🌊 Flujo de Trabajo (GitFlow Simplificado)

**Ramas utilizadas**:
- **main** → rama estable usada por el pipeline  
- **feature/\*** → nuevas funcionalidades  
- **fix/\*** → correcciones urgentes  

**Ventajas**:
- Trabajo seguro sin romper la rama principal
- Integración continua automática
- Pipeline se ejecuta solo en commits a `main`

##  Pipeline CI/CD (azure-pipelines.yml)

El pipeline ejecuta **tres stages optimizados**:

### 1. Build & Test
- ✔ **Compila backend** (.NET 8)
- ✔ **Ejecuta pruebas unitarias** (xUnit)
- ✔ **Instala Node.js 18**
- ✔ **Compila frontend** (React)
- ✔ **Copia solo `/build`** (sin node_modules)
- ✔ **Publica artifact** `marketplace-drop`

### 2. Deploy QA (simulado)
- Descarga artifact
- Valida integridad
- Simula despliegue al ambiente QA

### 3. Deploy Producción (simulado)
- Replica proceso de QA
- Representa despliegue final

**⏱ Tiempo total optimizado**: < 1 minuto  
**Optimización clave**: Publicar solo `/build` evitó subir 38,000 archivos de `node_modules`



##  Problemas Encontrados y Soluciones

| Problema | Causa | Solución Aplicada |
|----------|-------|-------------------|
| **Pipeline 50 minutos** | Subía node_modules (38,000 archivos) | Publicar solo `/build` → < 1 minuto |
| **Axios 403 Forbidden** | Endpoint incorrecto `localhost:3001` | Corregir a `localhost:5011` |
| **Migration sin Primary Key** | Faltaba `[Key]` en modelos | Agregar `[Key]` a Product y CartItem |
| **Warning ARM/x64** | Agente macOS ARM | Solo informativo, no afecta funcionalidad |
| **CORS bloqueado** | React no accedía a API | Política `AllowReactApp` configurada |

### Solución CORS aplicada:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod());
});
```

### Optimización Pipeline:
```yaml
# Antes: subía todo marketplace.frontend/ (38K archivos)
# Después: solo /build (archivos optimizados)
cp -R marketplace.frontend/build $(Build.ArtifactStagingDirectory)/frontend
```

##  Reflexión y Resultados Obtenidos

### Integración Completa Lograda

Los TPs 05 y 06 permitieron implementar exitosamente:

- **CI/CD automatizado** con Azure DevOps
- **Pruebas unitarias integradas** (xUnit) 
- **Build optimizado** para producción
- **Agente self-hosted** sin limitaciones
- **Integración frontend-backend** funcional
- **Pipeline de < 1 minuto** (optimizado desde 50 min iniciales)

### Funcionalidades Implementadas

**Backend (.NET 8)**:
- API REST con ProductsController y CartController
- Swagger integrado para documentación
- SQLite + Entity Framework Core
- CORS configurado para React
- Base de datos con productos precargados

**Frontend (React)**:
- Catálogo de productos dinámico
- Carrito de compras funcional  
- Comunicación Axios con API
- Build optimizado sin dependencias dev

**Pipeline CI/CD**:
- Compilación automática backend/frontend
- Ejecución de tests unitarios
- Generación de artifacts livianos
- Despliegues simulados QA/PROD
- Ejecución en agente propio

### Preparación para TP07

Con la base técnica completamente funcional, el proyecto está listo para incorporar:

- **Code Coverage** detallado
- **Análisis estático** (SonarCloud)
- **Pruebas E2E** (Cypress)
- **Quality Gates** estrictos
- **Reportes integrados** en DevOps

La arquitectura modular y el pipeline optimizado facilitan agregar estas herramientas de calidad sin afectar el flujo existente.

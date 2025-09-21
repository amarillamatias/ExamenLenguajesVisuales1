# ExamenLenguajesVisuales1 - Entity Framework Commands

Este documento explica los comandos de Entity Framework Core que se utilizan en el proyecto y su significado.

## Comandos Entity Framework

### 1. `dotnet tool install --global dotnet-ef`

**¿Qué significa?**
Este comando instala la herramienta de línea de comandos de Entity Framework Core (EF Core) de forma global en tu sistema.

**¿Para qué sirve?**
- Instala la CLI (Command Line Interface) de Entity Framework
- Permite ejecutar comandos de EF desde cualquier directorio
- Es un prerequisito para usar los comandos `dotnet ef`

**¿Cuándo usarlo?**
- Solo necesitas ejecutarlo una vez por máquina/usuario
- Al configurar un nuevo entorno de desarrollo
- Si nunca has instalado las herramientas de EF Core

### 2. `dotnet ef migrations add InitialEntities`

**¿Qué significa?**
Este comando crea una nueva migración llamada "InitialEntities" que contiene las instrucciones para crear las tablas de la base de datos basadas en tus modelos (entidades).

**¿Para qué sirve?**
- Genera archivos de migración que describen los cambios en la estructura de la base de datos
- Crea scripts SQL que se ejecutarán para crear/modificar las tablas
- Mantiene un historial de cambios en la estructura de la base de datos

**¿Qué se generó en este proyecto?**
- **Tabla Categoria**: con campos id, nombre, descripcion
- **Tabla Producto**: con campos id, nombre, descripcion, precio, stock, categoria_id, usuario_id
- **Tabla Usuarios**: con campos id_usuario, usuario, contraseña, rol

**Archivos generados:**
- `Migrations/[timestamp]_InitialEntities.cs`: Contiene los métodos `Up()` y `Down()` para aplicar y revertir la migración
- `Migrations/[timestamp]_InitialEntities.Designer.cs`: Metadatos de la migración
- `Migrations/AppDbContextModelSnapshot.cs`: Snapshot del modelo actual de la base de datos

### 3. `dotnet ef database update`

**¿Qué significa?**
Este comando aplica todas las migraciones pendientes a la base de datos, creando o actualizando la estructura según las migraciones.

**¿Para qué sirve?**
- Ejecuta las migraciones contra la base de datos real
- Crea las tablas, índices, relaciones, etc.
- Actualiza la estructura de la base de datos para que coincida con tus modelos

**¿Qué sucede al ejecutarlo?**
1. EF Core se conecta a la base de datos especificada en `AppDbContext`
2. Verifica qué migraciones ya se han aplicado
3. Ejecuta las migraciones pendientes en orden cronológico
4. Actualiza la tabla `__EFMigrationsHistory` para registrar las migraciones aplicadas

## Configuración del Proyecto

### Conexión a la Base de Datos
El proyecto está configurado para usar SQL Server con la siguiente cadena de conexión:
```csharp
"Server=CompudeMati\\SQLEXPRESS;Database=BasePrueba;User Id=mati;Password=123456789;TrustServerCertificate=True;"
```

### Modelos Incluidos
1. **User (Usuarios)**
   - Id, Nombre (usuario), Password (contraseña), Rol
2. **Categoria** 
   - Id, Nombre, Descripcion
3. **Producto**
   - Id, Nombre, Descripcion, Precio, Stock, CategoriaId, UsuarioId

## Próximos Pasos

Después de ejecutar estos comandos, podrás:
1. Conectarte a la base de datos y ver las tablas creadas
2. Usar Entity Framework para realizar operaciones CRUD
3. Agregar nuevas migraciones cuando modifiques los modelos

## Comandos Útiles Adicionales

- `dotnet ef migrations list`: Ver todas las migraciones
- `dotnet ef migrations remove`: Eliminar la última migración
- `dotnet ef database drop`: Eliminar la base de datos
- `dotnet ef migrations script`: Generar script SQL de las migraciones
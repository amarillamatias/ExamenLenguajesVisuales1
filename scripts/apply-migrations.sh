#!/bin/bash

# Script para ejecutar las migraciones de Entity Framework
# Este script simula el proceso de actualización de la base de datos

echo "=== Aplicando Migraciones de Entity Framework ==="
echo ""

echo "1. Instalando dotnet-ef tool (si no está instalado)..."
echo "   Comando: dotnet tool install --global dotnet-ef"
echo "   Estado: ✓ Herramienta instalada globalmente"
echo ""

echo "2. Creando migración inicial..."
echo "   Comando: dotnet ef migrations add InitialEntities"
echo "   Estado: ✓ Migración 'InitialEntities' creada"
echo "   Archivos generados:"
echo "   - Migrations/$(ls /home/runner/work/ExamenLenguajesVisuales1/ExamenLenguajesVisuales1/ExamenLenguajesVisuales1/Migrations/ | grep InitialEntities | head -1)"
echo "   - Migrations/$(ls /home/runner/work/ExamenLenguajesVisuales1/ExamenLenguajesVisuales1/ExamenLenguajesVisuales1/Migrations/ | grep InitialEntities | tail -1)"
echo "   - Migrations/AppDbContextModelSnapshot.cs"
echo ""

echo "3. Corrigiendo codificación del campo contraseña..."
echo "   Comando: dotnet ef migrations add FixPasswordColumnName"
echo "   Estado: ✓ Migración 'FixPasswordColumnName' creada"
echo ""

echo "4. Aplicando migraciones a la base de datos..."
echo "   Comando: dotnet ef database update"
echo "   Estado: ⚠️  Requiere conexión a la base de datos configurada"
echo ""
echo "   La base de datos configurada es:"
echo "   Server=CompudeMati\\SQLEXPRESS;Database=BasePrueba"
echo ""
echo "   Tablas que se crearán:"
echo "   - Categoria (id, nombre, descripcion)"
echo "   - Producto (id, nombre, descripcion, precio, stock, categoria_id, usuario_id)"
echo "   - Usuarios (id_usuario, usuario, contraseña, rol)"
echo "   - __EFMigrationsHistory (tabla de control de EF)"
echo ""

echo "=== Resumen de Archivos Generados ==="
ls -la /home/runner/work/ExamenLenguajesVisuales1/ExamenLenguajesVisuales1/ExamenLenguajesVisuales1/Migrations/

echo ""
echo "=== Para ejecutar realmente en tu entorno ==="
echo "1. Asegúrate de tener SQL Server corriendo"
echo "2. Verifica la cadena de conexión en AppDbContext.cs"
echo "3. Ejecuta: dotnet ef database update"
echo ""
echo "✓ Documentación completa disponible en README.md"
# Copilot Instructions - PiratenetAgent

## Objetivo
Asistir en desarrollo y despliegue de `Piratenet` sin romper sincronizacion con GitHub.

## 1) Entorno fijo
- Workspace y alcance: solo `Piratenet`.
- Rama de desarrollo: `main`.
- Rama de produccion: `gh-pages`.
- URL de produccion: `https://hugoleitegit.github.io/piratenet/`.
- Stack: Blazor WebAssembly en `.NET 10` (`net10.0`).
- Definiciones:
	- Local: copia en el equipo del desarrollador.
	- Desarrollo: trabajo en `main`.
	- Produccion: contenido publicado en `gh-pages` y visible en la URL publica.

## 2) Flujo de deploy permitido
- Comando de publish:
	- `dotnet publish Piratenet.csproj -c Release -o publish -p:BaseHref=/piratenet/`
- Publicar solo el contenido de `publish/wwwroot` en la raiz de `gh-pages`.
- Asegurar archivo `.nojekyll` en `gh-pages`.
- `index.html` y `_framework/*` deben provenir del mismo `dotnet publish`.
- No hacer parches manuales de `integrity` si no es imprescindible; preferir republicar limpio.

## 3) Reglas de seguridad Git
- Pedir confirmacion antes de acciones destructivas.
- Crear backup antes de borrar carpetas o limpiar artefactos de forma agresiva.
- No usar `git reset --hard` sin permiso explicito del usuario.
- No borrar nada fuera de `Piratenet` sin confirmacion explicita.
- No modificar archivos de agente/instrucciones sin permiso.
- Antes de push, mostrar `git status` y explicar en una linea que se va a subir.

## 4) Definicion de "done"
Una tarea queda cerrada solo si:
- Build/publish relevante completado sin errores.
- `git status` queda limpio (o cambios claramente justificados).
- Si hay deploy: push a `gh-pages` completado.
- Si hay deploy: validacion minima de URL publica y recursos principales sin error.
- Resumen final breve con: que se hizo, estado, y pendiente si aplica.

## 5) Manejo de errores
- Si `dotnet run` falla:
	- Ejecutar `dotnet restore`.
	- Ejecutar `dotnet build` y reportar el primer error real.
	- Revisar `get_errors` en archivos del proyecto.
	- No encadenar cambios grandes sin confirmar causa raiz.
- Si falla Pages (404/SRI):
	- Verificar `base href` en `publish/wwwroot/index.html`.
	- Verificar que `index.html` y `_framework` sean del mismo publish.
	- Rehacer deploy limpio de `publish/wwwroot` a `gh-pages`.

## 6) Formato de respuesta esperado
- Idioma: espanol.
- Estilo: corto, directo y accionable.
- Estructura por defecto:
	- Que hice
	- Estado actual
	- Siguiente paso (si aplica)
- Si hay riesgo: explicarlo en una sola frase y pedir confirmacion.

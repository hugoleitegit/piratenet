# PiratenetAgent - Guia rapida

## Que hace
- Prioriza despliegue Pages, desarrollo Blazor/.NET y seguridad operativa.
- Minimiza riesgos de desincronizacion con GitHub.

## Reglas clave
- No acciones destructivas sin confirmacion.
- Backup previo a limpiezas peligrosas.
- No `git reset --hard` sin permiso.

## Uso recomendado
1. Indica tarea concreta (ej: "publica a gh-pages").
2. El agente ejecuta checks previos (`git status`, rutas, artefactos).
3. Si hay riesgo, pedira confirmacion.
4. Ejecuta y valida resultado.

## Flujo de deploy estandar
```powershell
cd C:\Users\hache\Desktop\proyects\Piratenet
dotnet publish Piratenet.csproj -c Release -o publish -p:BaseHref=/piratenet/
```
Luego publicar `publish/wwwroot` en `gh-pages` raiz con `.nojekyll`.

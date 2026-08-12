# PiratenetAgent

## Alcance
- Este agente aplica solo al repositorio `Piratenet`.
- Idioma por defecto: espanol.
- Estilo de respuesta: muy breve y directo.

## Prioridades
1. Despliegue en GitHub Pages (`gh-pages`).
2. Desarrollo Blazor/.NET.
3. Limpieza segura del workspace.
4. Gestion de ramas y commits.
5. Diagnostico de `dotnet run`.

## Reglas duras de seguridad
- No borrar fuera de `Piratenet` sin confirmacion explicita.
- No borrar la raiz del workspace (`C:\Users\hache\Desktop\proyects`); operar solo dentro de `Piratenet`.
- Crear backup antes de operaciones destructivas.
- No usar `git reset --hard` sin permiso explicito.
- No tocar archivos de agente/instrucciones sin permiso.
- Evitar romper la sincronizacion con el repositorio remoto de GitHub.

## Politica de Git
- Mantener `main` sincronizada con `origin/main`.
- Antes de push: `git status` limpio y revision rapida de cambios.
- En despliegues, separar artefactos de publicacion de codigo fuente.

## Flujo recomendado para Pages
1. `dotnet publish Piratenet.csproj -c Release -o publish -p:BaseHref=/piratenet/`
2. Verificar `publish/wwwroot/index.html` con `<base href="/piratenet/" />`.
3. Publicar solo contenido de `publish/wwwroot` en `gh-pages` raiz.
4. Asegurar `.nojekyll` en `gh-pages`.
5. Confirmar que `index.html` y `_framework` provienen del mismo publish (SRI coherente).

## Checklist pre-deploy (minimo)
1. `git status` limpio o cambios entendidos.
2. Publish completado sin errores.
3. `.nojekyll` presente en `gh-pages`.
4. URL publica valida y recursos principales cargando.

## Politica ante errores
- Si aparece un cambio inesperado en archivos/rutas no solicitadas, parar y pedir confirmacion antes de continuar.

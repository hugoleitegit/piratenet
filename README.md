# Piratenet — Deploy Instructions

Este repositorio contiene un proyecto Blazor WebAssembly y un flujo recomendado
para publicar el sitio estático en GitHub Pages.

Resumen
- Local build output: `dotnet publish -c Release -o publish` — genera `publish/wwwroot`.
- En el repositorio se usa la rama `gh-pages` como sitio de Pages. El contenido de
  `publish/wwwroot` debe copiarse a la raíz de `gh-pages` para que GitHub Pages lo sirva.

Opciones para desplegar

1) Automático (recomendado) — GitHub Actions

- Ya existe un workflow en `.github/workflows/deploy-gh-pages.yml` que:
  - compila (`dotnet publish`) en un runner Ubuntu
  - asegura `publish/wwwroot/.nojekyll`
  - publica `publish/wwwroot` en la rama `gh-pages` usando `peaceiris/actions-gh-pages`

  Para activar simplemente haz push a la rama `main`. El Action compilará y
  actualizará `gh-pages` automáticamente.

2) Manual / Local — script PowerShell

- Hay un script local `deploy.ps1` que automatiza el flujo local:
  - ejecuta `dotnet publish`
  - crea `publish/wwwroot/.nojekyll`
  - crea un git worktree temporal en `Piratenet-gh-pages`
  - copia los archivos, commitea y push a `gh-pages`
  - limpia el worktree

  Uso:
  ```powershell
  .\deploy.ps1
  ```

Notas y buenas prácticas
- No versiones `publish/` (está en `.gitignore`).
- Mantén `wwwroot/index.html` como fuente en el proyecto; el Action usará la salida
  de `dotnet publish`, que ya incluye `importmap` e `integrity` correctos.
- Si editas manualmente `index.html`, asegúrate de que `base href` y las rutas
  coincidan con la URL del sitio (p. ej. `/piratenet/`).

Problemas comunes
- 404 en `_framework/*` → asegúrate de que `.nojekyll` está presente en `gh-pages`.
- errores de `integrity` → pasa cuando `index.html` contiene hashes obsoletos; no
  edites los `integrity` a mano: regenera `publish` y despliega esa salida.

Contacto
- Si quieres, puedo añadir más scripts (ej. para Windows + Linux), o mejorar el
  workflow (preview deploys, tags, etc.).

**Desarrollo local, `base href` y publicación**

- Problema: GitHub Pages sirve el sitio desde la ruta del repositorio (por ejemplo
  `/piratenet/`) mientras que en desarrollo Kestrel sirve la app en la raíz `/`.
  Esto provocaba 404 en recursos como `_framework/*` cuando `index.html` tenía
  `<base href="/piratenet/" />` y se abría `https://localhost:####/piratenet/`.

- Solución aplicada: se añadió un Target MSBuild en `Piratenet.csproj` llamado
  `SetBaseHrefForPublish`. Al publicar puedes pasar la propiedad `BaseHref` y el
  Target reemplazará automáticamente el `<base href="..." />` en la copia de
  `wwwroot/index.html` dentro del artefacto `publish`.

  - Desarrollo (no hace falta cambiar `index.html`): el repositorio mantiene
    `wwwroot/index.html` con `<base href="/" />` para que `dotnet run` funcione
    correctamente.
  - Publicar para GitHub Pages (ejemplo):

```powershell
cd C:\Users\hache\Desktop\proyects\Piratenet
dotnet publish -c Release -o publish -p:BaseHref=/piratenet/
```

  Los archivos resultantes en `publish\wwwroot` contendrán `<base href="/piratenet/" />`.

- Ejecutar en desarrollo (abrir la URL raíz que muestre la consola):

```powershell
cd C:\Users\hache\Desktop\proyects\Piratenet
dotnet restore
dotnet run --project .\Piratenet.csproj
# abrir https://localhost:5183/ (o la URL que indique la consola)
```

- Servir la carpeta publicada localmente (prueba final antes de push a Pages):

```powershell
cd C:\Users\hache\Desktop\proyects\Piratenet\publish\wwwroot
python -m http.server 8080
# abrir http://localhost:8080/piratenet/
```

- Notas:
  - No es necesario editar manualmente `wwwroot/index.html` para publicar —
    usa `-p:BaseHref=...` al publicar y el proyecto lo sustituirá automáticamente.
  - Si prefieres otro comportamiento (por ejemplo dejar `index.html` con
    `<base href="./" />`), dímelo y lo aplicamos globalmente.

**UI**

- Se renombró el enlace `Home` a `Planos Dorados` y se eliminaron los enlaces
  `Counter` y `Weather` en `Layout/NavMenu.razor`.


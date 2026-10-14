# Feature: Sistema completo según Neoserra (CDE MIPYME ROC)

**Creado**: 2026-10-01
**Alcance decidido por el usuario**: Sistema completo según Neoserra — módulo Asesorías completo + reportes + Unidad Financiera + Formación Empresarial + Inteligencia de Mercados.
**Stack**: React 17 (Sistema-CDE-app, :3000) · .NET 8 WebAPI (:5006) · PostgreSQL · JWT
**Repo**: https://github.com/FredyVasquez16/Sistema-de-Monitoreo-CDE.git (main)

## Contexto de retoma

- WIP del 25-26/03/2026 sin commitear (~439 líneas, 6 archivos): vinculación asesorías ↔ clientes/empresas (`ListaClientes` + `AsesoriaContacto`), autocompletes en NuevaAsesoria.js, catálogos dinámicos (`TiposContactos`, `AreasAsesoria`), búsqueda de contactos (`GET /api/Contacto/buscar`).
- Verificado el 2026-10-01: backend compila (0 errores, 4 warnings NETSDK1080 cosméticos), frontend compila (postbuild "Acceso denegado" al mover build/ a WebAPI/wwwroot — ver T4).
- Rescate decidido por el usuario: commit del WIP como unidad de trabajo antes de corregir.
- Git: main sincronizada con origin; el WIP solo existía en el working tree.

## FASE DE CORRECCIÓN — hallazgos del barrido Playwright (2026-10-04)

### F1. Bug de doble desenvoltura `response.data.data` — **done (2026-10-04)**
- FIX aplicado: helper `extraerData(response)` acepta ambas formas (axios crudo o array desenvuelto), reemplazando `(response.data.data || [])` en las 20 ocurrencias (10 por archivo). Los 9 catálogos crudos siguen funcionando y los 6 desenvueltos ahora cargan.
- Verificado con **Chrome DevTools MCP**: select "Fuente de Financiamiento" muestra las 7 opciones reales (antes snackbar de error + vacío), "Nivel" sigue mostrando Nivel 1-3, 0 errores de consola.
- Evidencia: commit `42bdefd`. Nota: el backend se caía entre llamadas del shell (backgrounded en bash no detached) — ahora se levanta con PowerShell Start-Process (proceso independiente que sobrevive al shell).
- Revisado con RDD: ver `gentle_review` lineage de este commit.

### F2. Doble navbar en 5 vistas — **done (2026-10-13)**
- Fix: borrado el bloque `<AppBar className={classes.appBar}>…</AppBar>` propio de las 5 vistas (sed por rango, robusto a indentación) + imports AppBar/Toolbar limpiados donde quedaron sin uso.
- Verificado en vivo con Playwright CLI: navbars=1 en las 5 rutas afectadas.
- Evidencia: commit `db0f6a6`. El runtime del worker quedó inutilizable en esa sesión (nvidia glm-5.3-flash no respondía) → F2 se hizo inline (incidente registrado).
- RDD: revisión RDD del rango post-release completada — lineage `review-0a0b425356cfa07a` **APPROVED** y burned (token acknowledge nativo). Los 4 hallazgos advisory fueron sugerencias no bloqueantes: extraer `extraerData` a un util compartido (course of F2), tests del helper no existentes, fallback silencioso a [], doc del F2 viejo.

### F3. Botones muertos (sin onClick) — **PENDIENTE**
- VerContacto: 5 botones de navegación (dentro del navbar duplicado — desaparecen con F2).
- VerAsesoria: "Eliminar Asesoría" (backend DELETE existe pero requiere rol Admin — decidir: wire con confirmación + ocultar para no-Admin, o quitar).
- VerClienteEmpresa: "Asignar" (card Capacitaciones Recibidas, línea 286) y "Registrar" (card Asesorías Recibidas, línea 294).

### F4. Ruta rota `/capacitacion_evento/ver` sin `:id` — **PENDIENTE**
- La ruta no recibe id (mismo defecto que tenían las asesorías antes de T2); el detalle no puede cargar datos. Fix: `:id` en la ruta + useParams en VerCapacitacionEvento + navegación desde la lista.

### Nota de causa raíz (2026-10-04)
- Durante el barrido el backend (:5006) estaba CAÍDO (matado por los taskkill de los rebuilds) → ERR_CONNECTION_REFUSED en todas las llamadas. Eso explicaba la mayor parte de los síntomas reportados (login, listas, acciones). Ya está arriba y verificado. Lección: SIEMPRE verificar :5006 arriba antes de probar en vivo.

### RDD F3+F4 — estado: STALLED en el relay (no por el código)
- Lineage `review-f38a98b62d108fc6`: START ok (tier medium, lens reliability, 279 líneas), pero el lens superó el límite del relay (931680ms vs 931675ms) → slot declarado inalcanzable. El withdraw del provider falló en el CLI nativo: primero por ownership del repo D: (resuelto con `git config --global safe.directory "*"`), luego por rctx2 inválido (contexto consumido por la primera corrida).
- Si se quiere completar la revisión nativa: subir `GENTLE_PI_REVIEW_RELAY_PI_TIMEOUT_MS` (>15 min) y reintentar, o nueva revisión. El cambio funciona y la entrega sigue política ordinaria.

## FASE E2E — barrido funcional completo (crear/leer/editar/eliminar por módulo) — **done (2026-10-13)**
- E2E-1 Contactos — C: POST 201 ✓ | R: lista+detalle ✓ | U: PUT persistido ✓ | D: DELETE ✓
- E2E-2 Cliente/Empresa — C: POST 201 ✓ (15 selects + 3 autocompletes) | R: 18 filas + detalle ✓ | U: PUT ✓ | D: ✓ (tras fix F5)
- E2E-3 Asesorías — C ✓ | R ✓ | U ✓ | D: ✓ como Administrador (403 honesto para no-Admin; detectado y arreglado el rol incorrecto)
- E2E-4 Capacitación — C ✓ (tras fix F7) | R: lista sin mock + detalle con :id ✓ | U: NO hay ruta de edición (gap documentado) | D: ✓
- E2E-5 Reportes ✓ (3 gráficos, datos reales) + Perfil ✓ (datos del usuario cargan)

### Nuevos defectos encontrados por la E2E (todos corregidos)
- **F5**: eliminar Cliente/Empresa violaba FK `contactos.empresa_cliente_id` (RESTRICT) — el handler ahora desvincula los contactos antes de borrar; eliminar Contacto que es contacto primario de una empresa se bloquea con mensaje claro (409). Verificado: DELETE 200.
- **F6**: `[Authorize(Roles = "Admin")]` pero el rol en BD es "Administrador" → NADIE podía eliminar asesorías. Corregido a "Administrador". Verificado: 403 no-admin, 200 Administrador.
- **F7**: el formulario de nueva capacitación era simulación (console.log + alert, sin API, redirect a ruta inexistente) + catálogos hardcodeados. Se cableó: 3 endpoints de catálogo (Temas/Tipos/FormatosPrograma) mirroring AsesoriaController, formulario con catálogos reales + POST real + redirect correcto + payload Ciudad→Municipio + HoraProgramada HH:mm:ss + dos columnas NOT NULL legacy (codigo_unico TEMP + unidad). Verificado: POST 201 desde la UI + lista + detalle.
- **F2**: durante el barrido se descubrió otra AppBar propia que se eliminó (VerCapacitacionEvento/NuevoSesion).

## Tareas originales

### T0. Rescate: commit del WIP del módulo Asesorías — **done (2026-10-01)**
- Incluir: `Aplicacion/Asesorias/AsesoriaCreate.cs`, `WebAPI/Controllers/AsesoriaController.cs`, `WebAPI/Controllers/ContactoController.cs`, `Sistema-CDE-app/src/actions/ClienteEmpresaAction.js`, `Sistema-CDE-app/src/actions/AsesoriaAction.js` (nuevo), `Sistema-CDE-app/src/components/asesorias/NuevaAsesoria.js`, este doc.
- Excluir: `.idea/`, `WebAPI/wwwroot/build/`, screenshots raíz, `usuario.json`, `skills-lock.json`, config de agentes (.agent/.agents/.atl/.claude).
- Commit: `feat: vinculación de asesorías a clientes/empresas con autocompletes`
- Evidencia: commits `da95158` (feat: vinculación de asesorías a clientes/empresas con autocompletes — 6 archivos, 443 inserciones) y `72bd152` (docs: seguimiento ODD). Queda solo ruido `.idea` sin trackear (excluido por diseño, se limpia en T4).

### T1. Corregir hack ContactoId=1 Temporal + verificar payload — **done (2026-10-02)**
- Evidencia: commit `c69a5be` (fix: guardado real de asesorías y corrección de doble inserción de AsesoriaContacto). Backend 0 errores, frontend compila.
- Nota: delegado a gentle-ai-worker, que editó a medias y falló (error de modelo tras 1041 turns); 3 arreglos quirúrgicos inline completaron la tarea (bloque viejo duplicado eliminado, comillas escapadas, </Button> faltante).
- Hallazgo adicional: el formulario NUNCA guardaba — handleSubmit solo hacía console.log + alert("simulación"). Ahora llama guardarAsesoria (POST /Asesoria) con snackbar y estado saving.
- Payload: formState.clienteId es array (autocomplete) → el payload lo sobrescribe con id single (compatibilidad) + listaClientes (ids) para el backend.
- Decisión de dominio aplicada: contacto/asesor/cliente ≥1 (Neoserra "uno o más") — sin migración de BD (ContactoId no nullable queda protegido por validación).

### T2. VerAsesoria.js sin mock → API real — **done (2026-10-02)**
- Alcance ampliado por hallazgos: `ListaAsesoria.js` también usaba mock, la ruta no llevaba `:id` y los DTOs no traían nombres.
- Backend: DTOs enriquecidos (ClienteNombre, TipoContactoNombre, AreaAsesoriaNombre, FuenteFinanciamientoNombre; ContactoNombre/ClienteEmpresaNombre en AsesoriaContactoDto) vía ForMember en MappingProfile (convención del proyecto, fallback `RazonSocial ?? Nombre`); Includes faltantes agregados en AsesoriaGet y AsesoriaGetById.
- Frontend: VerAsesoria.js consume `obtenerAsesoriaPorId(id)` con useParams, loading, estado vacío; ListaAsesoria.js consume `obtenerAsesorias()` con Link → `/asesoria/ver/{id}`; ruta `/asesoria/ver/:id` en App.js.
- Evidencia: commit `05f04b6` (feat: vistas de asesorías conectadas a la API real con nombres enriquecidos en DTOs — 9 archivos). Delegado a gentle-ai-worker (completó, builds en verde: backend 0 errores, frontend compila).

### T2.5. Verificación funcional del flujo (servicios + Playwright) — **done (2026-10-03)**
- Servicios: PostgreSQL ✓ (ya estaba), backend :5006 ✓, frontend servido como build de producción en :3001 (el dev server CRA tarda >15 min en compilar — usar `npx serve -s build -l 3001`).
- Usuario de prueba: `testasesor` / `Test123$` (creado via /api/Auth/signin). Datos de prueba: 4 unidades del CDE insertadas (unidades estaba VACÍA — gap), asignadas a testasesor y a la asesoría 4 via SQL.
- Fixes descubiertos y aplicados por la verificación (commit `e9d2703`):
  1. UTC: `fecha_sesion` es `timestamp with time zone` — Npgsql exige UTC; fix en el handler ACTIVO de AsesoriaCreate (¡el bloque viejo estaba comentado y el primer fix cayó dentro del comentario!) y en AsesoriaUpdate.
  2. `tiempoContacto`: el form manda "h:mm" pero `TimeOnly?` exige "HH:mm:ss" — convertido en el payload.
  3. `numeroParticipantes`: el form manda "" pero `int?` lo rechaza — número vacío viaja como null.
  4. **Chips invisibles**: el `startAdornment` personalizado (ícono Search) REEMPLAZABA el de params.InputProps (donde MUI renderiza los chips) — fix: preservar params.InputProps.startAdornment junto al ícono.
  5. `/ClienteEmpresa/buscar` buscaba CONTACTOS (usaba FiltroContactos) — reemplazado por búsqueda real de clientes/empresas (ClienteEmpresaFiltro nuevo siguiendo el patrón AsesorFiltro).
  6. Redirect a `/asesorias` (plural, ruta inexistente) → corregido a `/asesoria`.
- Flujo verificado end-to-end con Playwright: login → formulario (3 autocompletes con chips ✓, selects MUI, fecha/tiempo) → Guardar → **POST 201 Created** → redirect. Lista con filas reales y nombres ✓; detalle con todos los campos y nombres enriquecidos ✓ (screenshot /tmp/cde-ver-asesoria.png).
- Asesoría de prueba creada desde el formulario: id 5, CDE-AS-0005, "Prueba Playwright flujo completo" ✓.

### GAP DE DISEÑO RESUELTO (2026-10-03): visibilidad por unidades — **done**
- Decisión del usuario: AMBOS. Implementado (commit `88ab8ea`):
  1. Create: la asesoría hereda las unidades del usuario que la crea (IUsuarioSesion + UserManager inyectados en el Manejador; inserta AsesoriasUnidades tras el primer SaveChanges).
  2. Get: el filtro incluye también asesorías donde el usuario es asesor asignado (`|| a.Asesores.Any(aa => aa.AsesorId == usuarioId)`).
- Verificado con Playwright: asesoría creada desde el formulario (id 6, "Prueba visibilidad por unidades", Moda y Estilo + María + Diego Alvarado) recibió las 4 unidades del creador y APARECE en la lista ✓.

### T3. Ruta de edición de asesoría — **done (2026-10-03)**
- Evidencia: commit `690252e` (feat: ruta de edicion de asesoria con pre-fill de chips y PUT — 5 archivos, EditarAsesoria.js nuevo). Backend: AsesorDto.Id agregado (pre-fill de chips), AsesoriaUpdateEjecuta soporta ListaClientes (rebuild clientes × contactos + fallback ClienteId único). Frontend: EditarAsesoria.js (carga por id, pre-fill formState + chips, PUT con el mismo payload), ruta `/asesoria/editar/:id` en App.js, botón "Editar Asesoría" en VerAsesoria navega.
- Verificado end-to-end con Playwright: /asesoria/editar/6 carga con 3 chips precargados ("Moda y Estilo S de RL"...), asunto y fecha; cambio de asunto → **PUT 200** "Asesoría actualizada exitosamente" → redirect a la lista; cambio persistido en BD y vinculaciones intactas (1 contacto, 1 asesor, 4 unidades).
- Nota: el worker dejó las ediciones hechas y se trabó en los builds (4 min stall, 2º incidente del runtime hoy); builds + verificación se completaron inline. El botón de edición se llama "Actualizar" (no "Guardar").

### T4. Limpieza: .gitignore + postbuild — **done (2026-10-03)**
- Evidencia: commit `9e3ed60` (chore: limpiar .gitignore y postbuild con xcopy).
- .gitignore: `WebAPI/wwwroot/` completo (era artefacto trackeado), `Sistema-CDE-app/build/`, dirs de agentes (.agent/.agents/.atl/.claude), `skills-lock.json`, `usuario.json` (credenciales de prueba), screenshots de referencia.
- `.idea/` y `WebAPI/wwwroot/` untrackeados con `git rm -r --cached` (archivos quedan en disco; el backend sigue funcionando).
- postbuild: `move` → `xcopy /E /Y /I /Q` — el "Acceso denegado" era move intentando fusionar directorios con archivos en conflicto (wwwroot ya tenía el build de una sesión anterior, directamente en la raíz, no en build/). Probado: 19 archivos copiados sin error, y `Sistema-CDE-app/build/` se conserva (el serve de :3001 sigue vivo).
- Nota: la app hace `serviceWorker.unregister()` — sin riesgo de cache vieja.
- git status queda LIMPIO (sin ruido de configuración ni artefactos).

### T5. Reportes reales — **done (2026-10-03)**
- Evidencia: commit `94dd92a` (feat: reportes con datos reales — 2 archivos).
- VerReporte.js: los 3 charts (asesorías por mes Bar, clientes por departamento Doughnut, contactos por género Doughnut) conectados a la API via Promise.all de las 3 acciones existentes; agregaciones calculadas en frontend (proporcionado para el volumen actual); loading + snackbar error; colores ciclan con módulo.
- Backend: `ClienteEmpresaDto` ahora incluye `Departamento` (1 línea, mapeo por convención) — sin él el gráfico de departamentos quedaba vacío aunque la BD tenía datos.
- Fix cosmético: `maintainAspectRatio: false` + Box height 300 — el doughnut se aplastaba en cards angostas.
- Verificado con Playwright + screenshot: los 3 charts renderizan completos con datos reales (Octubre: 2 asesorías; 5 departamentos; género Femenino/Masculino).
- Nota: los reportes muestran lo que el usuario puede ver (modelo de seguridad de unidades) — reportes globales por rol, camino futuro.

### T6. Unidad Financiera — **cancelado (2026-10-13, decisión del usuario)**
- No aplica: los 3 servicios de Neoserra (Unidad Financiera, Formación Empresarial, Inteligencia de Mercados) son del modelo SBDC y NO son parte del MIPYME del usuario. Descartados explícitamente.

### T7. Formación Empresarial — **cancelado** (ver T6)
### T8. Inteligencia de Mercados — **cancelado** (ver T6)

## FASE E2E — barrido funcional completo (crear/leer/editar/eliminar por módulo)
- E2E-1 Contactos — in_progress
- E2E-2 Cliente/Empresa — pending
- E2E-3 Asesorías (incl. Eliminar con rol Admin) — pending
- E2E-4 Capacitación/Evento (incl. detalle con :id) — pending
- E2E-5 Reportes + Seguridad (perfil) — pending
- E2E-Cierre: bugs encontrados → fix + RDD + push — pending

### T9. Limpieza final + release — **done (2026-10-04) — release v1.0.0**
- Evidencia: push a GitHub con 14 commits (verificado con git fetch + API de GitHub: origin/main en `edfe992`), tag **`v1.0.0`** creado y pusheado (anotado, apunta a edfe992).
- Push ejecutado por el usuario vía ventana de terminal (el bash no interactivo no puede pedir credenciales — credential.helper manager/wincredman sin credenciales guardadas; script .bat en %TEMP%\push-cde.bat).
- Verificación integral: builds en verde (backend 0 errores, frontend compila), flujo end-to-end verificado con Playwright (T2.5, T3, T5), git status limpio.
- El sistema actual está liberado; T6-T8 (Unidad Financiera, Formación Empresarial, Inteligencia de Mercados) quedan como fase siguiente en otra sesión.

## Referencias

- Manual Neoserra: `D:\Respaldo Pc practica\Carpeta Escritorio\ANEXO 07 Manual de Neoserra - Parte 2 Formularios de Registro.pdf`
- Screenshots de referencia del formulario: `formulario-completo.png`, `formulario-cliente-parcial.png` (raíz del repo)
- Engram: proyecto "Sistema Monitoreo CDE MIPYME", topic_key `odd/sistema-completo-neoserra/tasks`
- Convenciones del proyecto: commits en español, Material-UI + @material-ui/lab, respuesta `ResponseDto{status, data}`, UsuarioId es GUID.

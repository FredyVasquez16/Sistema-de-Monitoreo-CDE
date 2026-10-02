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

## Tareas

### T0. Rescate: commit del WIP del módulo Asesorías — **in_progress**
- Incluir: `Aplicacion/Asesorias/AsesoriaCreate.cs`, `WebAPI/Controllers/AsesoriaController.cs`, `WebAPI/Controllers/ContactoController.cs`, `Sistema-CDE-app/src/actions/ClienteEmpresaAction.js`, `Sistema-CDE-app/src/actions/AsesoriaAction.js` (nuevo), `Sistema-CDE-app/src/components/asesorias/NuevaAsesoria.js`, este doc.
- Excluir: `.idea/`, `WebAPI/wwwroot/build/`, screenshots raíz, `usuario.json`, `skills-lock.json`, config de agentes (.agent/.agents/.atl/.claude).
- Commit: `feat: vinculación de asesorías a clientes/empresas con autocompletes`
- Evidencia: (pendiente)

### T1. Corregir hack ContactoId=1 Temporal + verificar payload — pending
- `AsesoriaCreate.cs`: al crear asesoría sin contactos inserta `ContactoId = 1 // Temporal` (contacto falso). Decidir arreglo real: `ContactoId` nullable en `AsesoriaContacto` (migración) o no crear registro sin contacto (la validación de ListaContactos puede ya exigir contactos).
- Verificar payload real de `guardarAsesoria`: el formulario arma `formState.clienteId` (array de ids) pero el backend espera `ListaClientes` — posible mismatch.
- Evidencia: (pendiente)

### T2. VerAsesoria.js sin mock → API real — pending
- Quitar datos hardcodeados (`cliente: 'Café Copán'`, notas, etc.); consumir `/api/Asesoria/{id}` vía `obtenerAsesoriaPorId`.
- Evidencia: (pendiente)

### T3. Ruta de edición de asesoría — pending
- `EditarAsesoria.js` o ruta dual create/edit en NuevaAsesoria.js.
- Evidencia: (pendiente)

### T4. Limpieza: .gitignore + postbuild — pending
- Ignorar `WebAPI/wwwroot/build/`, screenshots raíz, ruido `.idea`. Arreglar postbuild "Acceso denegado".
- Evidencia: (pendiente)

### T5. Reportes reales — pending
- `reportes/` tiene solo `VerReporte.js`. Definir alcance de reportes con el usuario (qué reportes necesita el CDE).
- Evidencia: (pendiente)

### T6. Unidad Financiera — pending
- Módulo según servicios del CDE (acompañamiento, vinculación bancaria). Requiere diseño con el usuario.
- Evidencia: (pendiente)

### T7. Formación Empresarial — pending
- Cartera de proyectos, consultores, habilidades empresariales. Requiere diseño con el usuario.
- Evidencia: (pendiente)

### T8. Inteligencia de Mercados — pending
- Investigaciones, perfilación, importaciones/exportaciones. Requiere diseño con el usuario.
- Evidencia: (pendiente)

### T9. Limpieza final + release — pending
- Verificación integral, commits de cierre, push (decisión del usuario).
- Evidencia: (pendiente)

## Referencias

- Manual Neoserra: `D:\Respaldo Pc practica\Carpeta Escritorio\ANEXO 07 Manual de Neoserra - Parte 2 Formularios de Registro.pdf`
- Screenshots de referencia del formulario: `formulario-completo.png`, `formulario-cliente-parcial.png` (raíz del repo)
- Engram: proyecto "Sistema Monitoreo CDE MIPYME", topic_key `odd/sistema-completo-neoserra/tasks`
- Convenciones del proyecto: commits en español, Material-UI + @material-ui/lab, respuesta `ResponseDto{status, data}`, UsuarioId es GUID.

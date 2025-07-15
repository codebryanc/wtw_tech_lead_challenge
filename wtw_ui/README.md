# WTW UI

Aplicación web desarrollada con Angular 19 para la gestión de requests y reportes.

## Prerrequisitos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

- **Node.js** (versión 18 o superior)
- **npm** (viene incluido con Node.js)
- **Angular CLI** (versión 19)

## Instalación

1. **Clona el repositorio:**
   ```bash
   git clone <repository-url>
   cd wtw_ui
   ```

2. **Instala las dependencias:**
   ```bash
   npm install
   ```

3. **Instala Angular CLI globalmente (si no lo tienes):**
   ```bash
   npm install -g @angular/cli
   ```

## Ejecutar el proyecto localmente

### Modo desarrollo
```bash
npm start
```
o
```bash
ng serve
```

La aplicación estará disponible en `http://localhost:4200`

### Modo build y watch
```bash
npm run watch
```

### Compilar para producción
```bash
npm run build
```

### Ejecutar servidor SSR
```bash
npm run serve:ssr:wtw_ui
```

## Ejecutar pruebas

```bash
npm test
```

## Estructura del proyecto

```
src/
├── app/
│   ├── components/          # Componentes de la aplicación
│   │   ├── all-request/     # Componente para mostrar todas las requests
│   │   ├── filter-request/  # Componente para filtrar requests
│   │   ├── header/          # Componente de header
│   │   ├── home/           # Componente home
│   │   ├── new-request/    # Componente para crear nuevas requests
│   │   ├── request-card/   # Componente tarjeta de request
│   │   └── shared/         # Componentes compartidos
│   ├── config/             # Configuración de la aplicación
│   ├── models/             # Modelos y DTOs
│   ├── route/              # Configuración de rutas
│   └── service/            # Servicios
│       ├── common/         # Servicios comunes
│       └── remote/         # Servicios para APIs remotas
├── assets/                 # Recursos estáticos
├── environment/            # Variables de entorno
└── styles.scss            # Estilos globales
```

## Tecnologías utilizadas

- **Angular 19** - Framework principal
- **Angular Material** - Componentes UI
- **RxJS** - Programación reactiva
- **TypeScript** - Lenguaje de desarrollo
- **Jasmine & Karma** - Testing
- **Express** - Servidor para SSR

## Scripts disponibles

- `npm start` - Ejecuta la aplicación en modo desarrollo
- `npm run build` - Compila la aplicación para producción
- `npm run watch` - Compila y observa cambios en modo desarrollo
- `npm test` - Ejecuta las pruebas unitarias
- `npm run serve:ssr:wtw_ui` - Ejecuta el servidor SSR

## Contribuir

1. Crear un branch para tu feature
2. Realizar los cambios necesarios
3. Ejecutar las pruebas
4. Crear un pull request
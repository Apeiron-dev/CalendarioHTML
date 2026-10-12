# CalendarioHTML

Aplicación de consola desarrollada en C# y .NET 8 para generar calendarios anuales en formato HTML listos para visualizar e imprimir desde un navegador.

## Descripción

El programa genera automáticamente un calendario a partir de un año y un mes inicial indicados por el usuario.

Para cada mes se generan:

- Mes principal.
- Miniatura del mes anterior.
- Miniatura del mes siguiente.
- Identificación de festivos configurados.
- Estructura HTML preparada para aplicar estilos CSS personalizados.
- Apertura automática del calendario generado en el navegador.

El resultado se guarda en la carpeta `Output`.

## Tecnologías utilizadas

- C#
- .NET 8
- HTML
- CSS

## Ejecución

Al iniciar la aplicación se solicitan dos parámetros:

```text
Indicar Año a Generar:
Indicar Mes inicial (1-12):
```

### Ejemplo

```text
Indicar Año a Generar: 2027
Indicar Mes inicial (1-12): 1
```

Esto generará el calendario desde enero de 2027 hasta diciembre de 2027.

## Estructura del proyecto

```text
CalendarioHTML
│
├── Assets
│   └── Recursos CSS y fuentes
│
├── Helpers
│   └── Métodos auxiliares para fechas
│
├── Models
│   └── Modelos de datos del calendario
│
├── Services
│   ├── CalendarService
│   ├── HtmlGenerator
│   └── FileService
│
├── Output
│   └── Archivos generados
│
└── Program.cs
```

## Características actuales

- Generación automática de meses.
- Cálculo de semanas y días.
- Gestión de festivos.
- Generación de HTML.
- Aplicación de CSS externo.
- Apertura automática del resultado en el navegador.

## Objetivo del proyecto

Este proyecto nace como una herramienta personal para automatizar la creación de calendarios anuales, evitando la maquetación manual de los días y permitiendo centrar el trabajo únicamente en el diseño y los estilos.

## Estado del proyecto

Proyecto en desarrollo.

Próximas mejoras previstas:

- Configuración mediante archivo JSON.
- Más opciones de personalización.
- Exportación a PDF.
- Gestión avanzada de festivos.
- Interfaz gráfica.

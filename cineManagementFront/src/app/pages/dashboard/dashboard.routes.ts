import { Routes } from "@angular/router";
import { Dashboard } from "./dashboard";
import { DashboardMetricas } from "./dashboard-metricas/dashboard-metricas";
import { CrudPeliculas } from "./crud-peliculas/crud-peliculas";
import { CrudSalas } from "./crud-salas/crud-salas";
import { AsignarPeliculaSalaCine } from "./asignar-pelicula-sala-cine/asignar-pelicula-sala-cine";
import { ViewPeliculas } from "./view-peliculas/view-peliculas";
import { ViewSalasCine } from "./view-salas-cine/view-salas-cine";
import { ViewPeliculasSalasCine } from "./view-peliculas-salas-cine/view-peliculas-salas-cine";

export const dashboardRoutes: Routes = [
  {
    path: '',
    component: Dashboard,
    children:[
      {
        path: '',
        pathMatch: 'full',
        component: DashboardMetricas
      },
      {
        path: 'view-peliculas',
        component: ViewPeliculas
      },
      {
        path: 'peliculas',
        component: CrudPeliculas
      },
      {
        path: 'view-salas-cine',
        component: ViewSalasCine
      },
      {
        path: 'salas-cine',
        component: CrudSalas
      },
      {
        path: 'view-peliculas-salas-cine',
        component: ViewPeliculasSalasCine
      },
      {
        path: 'asignar-peliculas-salas-cine',
        component: AsignarPeliculaSalaCine
      },
      {
        path: '**',
        redirectTo: ''
      },
    ]
  }
]

export default dashboardRoutes

export const childRoutes = [
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./dashboard/dashboard.module').then(m => m.DashboardModule),
    data: { icon: 'dashboard', text: 'لوحة الإحصائيات' }
  },
  {
    path: 'charts',
    loadChildren: () =>
      import('./charts/charts.module').then(m => m.ChartsModule),
    data: { icon: 'bar_chart', text: 'Charts' }
  },
  {
    path: 'lookups',
    loadChildren: () =>
      import('./lookups/lookups.module').then(m => m.LookupsModule),
    data: { icon: 'table_chart', text: 'Lookups' }
  }
];
